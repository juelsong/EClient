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

namespace ESys.Utilty.Entity
{
    using ESys.Contract.Entity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.OData.ModelBuilder;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// 控制器odata暴露基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ControllerODataConfigBase<T> : IODataEntityBuilder where T : ControllerBase
    {
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name="builder"></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {
            foreach (var mi in typeof(T).GetMethods().Where(m => m.GetCustomAttribute<HttpMethodAttribute>() != null))
            {
                var type = mi.ReturnType;
                if (type.IsAssignableTo(typeof(Task)))
                {
                    type = type.GetGenericArguments().Single();
                }
                AddComplexTypeIfNeed(builder, type);
                var func = builder.Action(mi.Name).Returns(type);
                foreach (var item in mi.GetParameters())
                {
                    AddComplexTypeIfNeed(builder, item.ParameterType);
                    func.Parameter(item.ParameterType, item.Name);
                }
            }
        }
        private static void AddComplexTypeIfNeed(ODataModelBuilder builder, Type type)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (builder.EntitySets.Any(s => s.ClrType == type)
                    || builder.StructuralTypes.Any(s => s.ClrType == type)
                    || builder.EnumTypes.Any(t => t.ClrType == type))
            {
                return;
            }
            if (builder.GetTypeConfigurationOrNull(type) is not PrimitiveTypeConfiguration typeConfig)
            {
                builder.Namespace = type.Namespace;
                var ct = builder.AddComplexType(type);
                foreach (var pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var config = builder.GetTypeConfigurationOrNull(pi.PropertyType);
                    if (config is PrimitiveTypeConfiguration)
                    {
                        ct.AddProperty(pi);
                    }
                    else if (config is EnumTypeConfiguration)
                    {
                        ct.AddEnumProperty(pi);
                        var enumConfig = builder.AddEnumType(pi.PropertyType);
                        foreach (Enum item in Enum.GetValues(pi.PropertyType))
                        {
                            enumConfig.AddMember(item);
                        }
                    }
                    else
                    {
                        if (pi.PropertyType.IsAssignableTo(typeof(IEnumerable)))
                        {
                            var elementType = pi.PropertyType.IsGenericType
                                            ? pi.PropertyType.GetGenericArguments().Single()
                                            : pi.PropertyType.GetElementType();
                            if (elementType == null)
                            {
                                throw new InvalidOperationException();
                            }
                            ct.AddCollectionProperty(pi);
                            AddComplexTypeIfNeed(builder, elementType);
                        }
                        else if (pi.PropertyType.IsEnum)
                        {
                            if (config == null)
                            {
                                // TODO 多重类型嵌套
                                var plusIndex = pi.PropertyType.FullName?.IndexOf('+');
                                builder.Namespace = plusIndex.HasValue && plusIndex > -1 && !string.IsNullOrEmpty(pi.PropertyType.FullName)
                                    ? pi.PropertyType.FullName[..plusIndex.Value]
                                    : pi.PropertyType.Namespace;
                                builder.AddEnumType(pi.PropertyType);
                                builder.Namespace = type.Namespace;
                            }
                            ct.AddEnumProperty(pi);
                        }
                        else
                        {
                            AddComplexTypeIfNeed(builder, pi.PropertyType);
                            ct.AddComplexProperty(pi);
                        }
                    }
                }
            }
        }
    }
}
