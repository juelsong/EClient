/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑,代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

using EntityViewGenerator.Impl;
using EntityViewGenerator.Interface;
using EntityViewGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using PropertyInfo = EntityViewGenerator.Models.PropertyInfo;

namespace EntityViewGenerator
{
    public class ProjectFactory
    {
        private ProjectFactory() { }

        public static ProjectFactory Instance { get; } = new ProjectFactory();

        public Project GetProject(IModel model)
        {
            var project = new Project()
            {
                Schema = new Schema()
                {
                    Entities = model.GetEntityTypes().Select(GetEntity).OrderBy(e => e.Name).ToArray()
                },
                GeneratorType = typeof(ESysVueGenerator).FullName // TODO 其他生成器
            };
            var enumTypes = model
                .GetEntityTypes()
                .Select(t => t.ClrType)
                .SelectMany(t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                .Where(p => p.PropertyType.IsEnum || (p.PropertyType.IsGenericType && p.PropertyType.GenericTypeArguments.All(a => a.IsEnum)))
                .Select(p => p.PropertyType.IsEnum ? p.PropertyType : p.PropertyType.GenericTypeArguments.First())
                .Distinct()
                .ToArray();
            project.Schema.Enums = enumTypes
                .Select(GetEnum)
                .ToArray();
            var comments = model
                .GetEntityTypes()
                .Select(t => t.ClrType.Assembly)
                .Union(enumTypes.Select(e => e.Assembly))
                .Distinct()
                .Select(ass => Path.Join(Path.GetDirectoryName(ass.Location), $"{ass.GetName().Name}.xml"))
                .Where(path => File.Exists(path))
                .SelectMany(path => { var doc = new XmlDocument(); doc.Load(path); return doc.SelectNodes("//doc/members/member").Cast<XmlNode>(); })
                .ToArray()
                .ToDictionary(n => n.Attributes.GetNamedItem("name").Value[2..], n => n.ChildNodes[0].InnerText.Trim());
            foreach (var entity in project.Schema.Entities)
            {
                if (comments.TryGetValue($"{entity.FullName}", out var comment))
                {
                    entity.Desc = comment;
                }
                foreach (var property in entity.Properties)
                {
                    if (comments.TryGetValue($"{entity.FullName}.{property.Name}", out comment))
                    {
                        property.Desc = comment;
                    }
                }
                //foreach (var property in entity.Navigations)
                //{
                //    if (comments.TryGetValue($"{entity.FullName}.{property.Name}", out comment))
                //    {
                //        property.Desc = comment;
                //    }
                //}
            }
            foreach (var e in project.Schema.Enums)
            {
                foreach (var enumMember in e.Members)
                {
                    if (comments.TryGetValue($"{e.FullName}.{enumMember.Name}", out var comment))
                    {
                        enumMember.Desc = comment;
                    }
                }
            }
            return project;
        }

        public IGenerator GetGenerator(Project project)
        {
            IGenerator generator = null;
            if (!string.IsNullOrWhiteSpace(project.GeneratorType))
            {
                var type = Type.GetType(project.GeneratorType);
                if (type != null)
                {
                    generator = Activator.CreateInstance(type) as IGenerator;
                }
            }
            return generator;
        }

        private static PropertyInfo GetNavigation(INavigationBase navigation)
        {
            return new PropertyInfo()
            {
                Name = navigation.Name,
                IsNavigation = true,
                IsCollection = navigation.IsCollection,
                TypeName = navigation.ClrType.IsGenericType
                           ? string.Join(',', navigation.ClrType.GetGenericArguments().Select(t => t.FullName))
                           : navigation.ClrType.FullName
            };
        }
        private static PropertyInfo GetProperty(IProperty property)
        {
            return new PropertyInfo()
            {
                Name = property.Name,
                IsNullable = property.IsNullable,
                TypeName = property.ClrType.IsGenericType // TODO Nullable<T>
               ? string.Join(',', property.ClrType.GetGenericArguments().Select(t => t.FullName))
               : property.ClrType.IsEnum ? property.ClrType.FullName
               : property.ClrType.FullName
            };
        }
        private static EnumInfo GetEnum(Type enumType)
        {
            var ret = new EnumInfo()
            {
                Namespace = enumType.Namespace,
                Name = enumType.Name,
            };
            var members = new List<EnumMember>();
            foreach (var item in Enum.GetValues(enumType))
            {
                members.Add(new EnumMember()
                {
                    Value = item,
                    Name = Enum.GetName(enumType, item)
                });
            }
            ret.Members = members.ToArray();
            return ret;
        }
        private static EntityInfo GetEntity(IEntityType entityType)
        {
            if (entityType.ClrType.Name == "User")
            {

            }
            var keys = entityType.GetDeclaredKeys();
            return new EntityInfo()
            {
                Keys = keys.SelectMany(k => k.Properties.Select(k => k.Name)).Where(s => !string.IsNullOrWhiteSpace(s)).ToArray(),
                Namespace = entityType.ClrType.Namespace,
                Name = entityType.ClrType.Name,
                Properties = entityType
                        .GetProperties()
                        .Select(GetProperty)
                        .Union(entityType.GetNavigations().Select(GetNavigation))
                        .Union(entityType.GetSkipNavigations().Select(GetNavigation))
                        .ToArray(),
            };
        }
    }
}
