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

using com.seagullrobot.EMIS.Contract.Db;
using com.seagullrobot.EMIS.Contract.Entity;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Tools
{
    public static class Generator
    {
        static string GetAssemblyNameFromProjFile(string projFile)
        {
            var doc = new XmlDocument();
            doc.Load(projFile);
            return doc.DocumentElement.SelectSingleNode("PropertyGroup/AssemblyName").InnerText;
        }
        public static void Generate()
        {
            CreateHostBuilder().Build();
            var targetProjects = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".."), "EMIS*.csproj", SearchOption.AllDirectories)
                .ToDictionary(f => GetAssemblyNameFromProjFile(f), f => Path.GetDirectoryName(f));

            using var tenantCtx = new GenTenantDbContext(new DbContextOptionsBuilder<GenTenantDbContext>().Options);
            using var manageCtx = new GenManageDbContext(new DbContextOptionsBuilder<GenManageDbContext>().Options);

            string GetOdataConfigOutput(Type t)
            {
                var dir = targetProjects[t.Assembly.GetName().Name];
                var name = t.Name.EndsWith("Audit") ? t.Name[0..^5] : t.Name;
                var filePath = Directory.GetFiles(dir, $"*{name}.cs", SearchOption.AllDirectories).FirstOrDefault();
                if (filePath == null)
                {
                    filePath = Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories).FirstOrDefault(f =>
                     {
                         using var reader = new StreamReader(f);
                         var content = reader.ReadToEnd();
                         return content.Contains($" enum {t.Name}");
                     });
                }
                return Path.Combine(Path.GetDirectoryName(filePath), "OData", "gen", $"{t.Name}ODataConfig.gen.cs");
            }
            //string GetAuditOutput(Type t)
            //{
            //    var dir = targetProjects[t.Assembly.GetName().Name];
            //    var name = t.Name.EndsWith("Audit") ? t.Name[0..^5] : t.Name;
            //    var filePath = Directory.GetFiles(dir, $"*{name}.cs", SearchOption.AllDirectories).FirstOrDefault();
            //    return Path.Combine(Path.GetDirectoryName(filePath), "Audit", "gen", $"{t.Name}Audit.gen.cs");
            //}
            //string GetAuditInterceptorOutput(Type t)
            //{
            //    var dir = targetProjects[t.Assembly.GetName().Name];
            //    var name = t.Name.EndsWith("Audit") ? t.Name[0..^5] : t.Name;
            //    var filePath = Directory.GetFiles(dir, $"*{name}.cs", SearchOption.AllDirectories).FirstOrDefault();
            //    return Path.Combine(Path.GetDirectoryName(filePath), "Interceptor", "gen", $"{t.Name}Interceptor.gen.cs");
            //}

            void GenOData(DbContext ctx)
            {
                foreach (var entityType in ctx.Model.GetEntityTypes())
                {
                    GenerateOdataEntity(entityType, GetOdataConfigOutput);
                }

                var enumTypes = ctx.Model
                    .GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType.IsEnum || (p.ClrType.IsGenericType && p.ClrType.GenericTypeArguments.All(a => a.IsEnum)))
                    .Select(p => p.ClrType.IsEnum ? p.ClrType : p.ClrType.GenericTypeArguments.First())
                    .Distinct();
                foreach (var enumType in enumTypes)
                {
                    GenerateOdataEnum(enumType, GetOdataConfigOutput);
                }
            }

            //void GenAuditEntity(DbContext ctx)
            //{
            //    var enumTypes = ctx.Model
            //        .GetEntityTypes()
            //        .Select(t => t.ClrType)
            //        .SelectMany(t => t.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            //        .Where(p => p.PropertyType.IsEnum || (p.PropertyType.IsGenericType && p.PropertyType.GenericTypeArguments.All(a => a.IsEnum)))
            //        .Select(p => p.PropertyType)
            //        .Distinct()
            //        .ToArray();
            //    var comments = ctx.Model
            //        .GetEntityTypes()
            //        .Select(t => t.ClrType.Assembly)
            //        .Union(enumTypes.Select(e => e.Assembly))
            //        .Distinct()
            //        .Select(ass => Path.Join(Path.GetDirectoryName(ass.Location), $"{ass.GetName().Name}.xml"))
            //        .Where(path => File.Exists(path))
            //        .SelectMany(path => { var doc = new XmlDocument(); doc.Load(path); return doc.SelectNodes("//doc/members/member").Cast<XmlNode>(); })
            //        .ToArray()
            //        .ToDictionary(n => n.Attributes.GetNamedItem("name").Value[2..], n => n.ChildNodes[0].InnerText.Trim());
            //    var entityTypes = ctx.Model
            //        .GetEntityTypes()
            //        .ToArray();

            //    foreach (var item in entityTypes.Where(et =>
            //                et.ClrType.GetCustomAttribute<DisableAuditAttribute>() == null
            //                && et.ClrType.BaseType.IsGenericType
            //                && et.ClrType.BaseType.GetGenericTypeDefinition() == typeof(BizEntity<,>)).OrderBy(et => et.ClrType.Namespace).ThenBy(et => et.ClrType.Name))
            //    {
            //        if (!item.ClrType.BaseType.IsAssignableTo(typeof(IActiveEntity)))
            //        {
            //            Console.WriteLine($"Lake IActiveEntity\t{item.ClrType.Name}");
            //        }
            //    }
            //    foreach (var entityType in entityTypes.Where(et => et.ClrType.GetCustomAttribute<DisableAuditAttribute>() == null))
            //    {
            //        GenerateAuditEntity(entityType, comments, GetAuditOutput);
            //        // GenerateAuditInterceptor(entityType, GetAuditInterceptorOutput);
            //    }
            //}

            //GenAuditEntity(tenantCtx);
            GenOData(tenantCtx);
            GenOData(manageCtx);
        }

        private static readonly Dictionary<Type, string> typeDic = new()
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(nint), "nint" },
            { typeof(nuint), "nuint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
        };

        static string GetTypeStr(Type type)
        {
            string ret;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments().First();

                if (!typeDic.TryGetValue(type, out ret))
                {
                    ret = type.Name;
                }
                ret = $"{ret}?";
            }
            else
            {
                if (!typeDic.TryGetValue(type, out ret))
                {
                    ret = type.Name;
                }
            }
            return ret;
        }
//        public static void GenerateAuditInterceptor(IEntityType type, Func<Type, string> interceptorOutputFunc)
//        {
//            var keyTypeStr = GetTypeStr(type.FindPrimaryKey().Properties[0].ClrType);
//            var configTemplate = @$"
///*
// *        ┏┓   ┏┓+ +
// *       ┏┛┻━━━┛┻┓ + +
// *       ┃       ┃
// *       ┃   ━   ┃ ++ + + +
// *       ████━████ ┃+
// *       ┃       ┃ +
// *       ┃   ┻   ┃
// *       ┃       ┃ + +
// *       ┗━┓   ┏━┛
// *         ┃   ┃
// *         ┃   ┃ + + + +
// *         ┃   ┃    Code is far away from bug with the animal protecting
// *         ┃   ┃ +     神兽保佑,代码无bug
// *         ┃   ┃
// *         ┃   ┃  +
// *         ┃    ┗━━━┓ + +
// *         ┃        ┣┓
// *         ┃        ┏┛
// *         ┗┓┓┏━┳┓┏┛ + + + +
// *          ┃┫┫ ┃┫┫
// *          ┗┻┛ ┗┻┛+ + + +
// */
//namespace {string.Join('.', type.ClrType.Namespace.Split('.').Take(type.ClrType.Namespace.Count(c => c == '.')))}.Interceptor
//{{
//    using com.seagullrobot.EMIS.Contract.Db;
//    using {type.ClrType.Namespace};
//    using com.seagullrobot.EMIS.Utilty.Interceptor;
//    using Furion.DatabaseAccessor;

//    /// <summary>
//    /// {type.ClrType.Name}拦截器 自动生成，请勿修改
//    /// </summary>
//    public partial class {type.ClrType.Name}Interceptor : AuditInterceptor<{type.ClrType.Name}Audit, {type.ClrType.Name}, {keyTypeStr}>
//    {{
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name=""repository""></param>
//        public {type.ClrType.Name}Interceptor(IMSRepository<TenantMasterLocator, TenantSlaveLocator> repository) : base(repository){{}}
//    }}
//}}
//";
//            OutputFile(configTemplate, interceptorOutputFunc(type.ClrType));
//        }

//        private static void GenerateAuditEntity(IEntityType type, Dictionary<string, string> comments, Func<Type, string> auditOutputFunc)
//        {
//            string GetPropertyStr(IProperty property)
//            {
//                comments.TryGetValue($"{type.ClrType.FullName}.{property.Name}", out var comment);
//                var sb = string.IsNullOrEmpty(comment) ? new StringBuilder() : new StringBuilder($@"        /// <summary>
//        /// {comment}
//        /// </summary>
//");
//                sb.AppendLine($@"        public {GetTypeStr(property.ClrType)} {property.Name} {{get; set;}}");
//                return sb.ToString();
//            }
//            var keyTypeStr = GetTypeStr(type.FindPrimaryKey().Properties[0].ClrType);
//            var namespaces = type.ClrType.GetProperties()
//                .Where(p => p.PropertyType.IsGenericType)
//                .SelectMany(p => p.PropertyType.GetGenericArguments())
//                .Union(type.ClrType.GetProperties()
//                        .Where(p => p.PropertyType.IsGenericType
//                                 && p.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>)
//                                 && p.PropertyType.GetGenericTypeDefinition() != typeof(ICollection<>))
//                        .Select(p => p.PropertyType.GetGenericTypeDefinition()))
//                .Union(type.ClrType.GetProperties().Where(p => !p.PropertyType.IsGenericType).Select(p => p.PropertyType))
//                .Where(t => !t.IsClass)
//                .Select(t => t.Namespace)
//                .Distinct();
//            var existNamespaces = new string[]
//            {
//                 "com.seagullrobot.EMIS.Contract.Entity",
//                 "Microsoft.EntityFrameworkCore",
//                 "Microsoft.EntityFrameworkCore.Metadata.Builders",
//                 "System",
//                 type.ClrType.Namespace
//            };
//            var appendNamespaces = namespaces.Where(n => !existNamespaces.Contains(n)).ToArray();
//            var appendNamespacesStr = appendNamespaces.Length == 0 ? string.Empty : $"\r\n{string.Join("\r\n", appendNamespaces.Select(s => $"    using {s};"))}";

//            string GetPropertyDefaultValue(IEntityType type)
//            {
//                var properties = type.GetProperties().Where(p => !string.IsNullOrEmpty(p.GetDefaultValueSql())).ToArray();
//                if (properties.Any())
//                {
//                    return "\r\n\r\n" + string.Join("\r\n", properties.Select(p => $@"            entityBuilder.Property(audit => audit.{p.Name})
//                .HasDefaultValueSql(""{p.GetDefaultValueSql()}"");"));
//                }
//                return string.Empty;
//            }
//            string GetPropertyPrecision(IEntityType type)
//            {
//                var properties = type.GetProperties().Where(p => p.GetPrecision().HasValue && p.GetScale().HasValue).ToArray();
//                if (properties.Any())
//                {
//                    return "\r\n\r\n" + string.Join("\r\n", properties.Select(p => $@"            entityBuilder.Property(audit => audit.{p.Name})
//                .HasPrecision({p.GetPrecision().Value}, {p.GetScale().Value});"));
//                }
//                return string.Empty;
//            }
//            string GetPropertyConversion(IEntityType type)
//            {
//                var properties = type.GetProperties().Where(p => p.FindAnnotation("ProviderClrType") != null).ToArray();
//                if (properties.Any())
//                {
//                    return "\r\n\r\n" + string.Join("\r\n", properties.Select(p => $@"            entityBuilder.Property(audit => audit.{p.Name})
//                .HasConversion<{typeDic[(p.FindAnnotation("ProviderClrType") as IConventionAnnotation).Value as Type]}>();"));
//                }
//                return string.Empty;
//            }

//            var configTemplate = @$"
///*
// *        ┏┓   ┏┓+ +
// *       ┏┛┻━━━┛┻┓ + +
// *       ┃       ┃
// *       ┃   ━   ┃ ++ + + +
// *       ████━████ ┃+
// *       ┃       ┃ +
// *       ┃   ┻   ┃
// *       ┃       ┃ + +
// *       ┗━┓   ┏━┛
// *         ┃   ┃
// *         ┃   ┃ + + + +
// *         ┃   ┃    Code is far away from bug with the animal protecting
// *         ┃   ┃ +     神兽保佑,代码无bug
// *         ┃   ┃
// *         ┃   ┃  +
// *         ┃    ┗━━━┓ + +
// *         ┃        ┣┓
// *         ┃        ┏┛
// *         ┗┓┓┏━┳┓┏┛ + + + +
// *          ┃┫┫ ┃┫┫
// *          ┗┻┛ ┗┻┛+ + + +
// */
//namespace {type.ClrType.Namespace}
//{{
//    using com.seagullrobot.EMIS.Contract.Entity;
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Metadata.Builders;
//    using global::System;{appendNamespacesStr}
//{(type.ClrType.IsAssignableTo(typeof(ITraceableEntity)) ? string.Empty : $"#warning {type.ClrType.Name} 没有继承接口ITraceableEntity，审计实体无法记录变更人\r\n")}
//    /// <summary>
//    /// {type.ClrType.Name}审计 自动生成，请勿修改
//    /// </summary>
//    public partial class {type.ClrType.Name}Audit : AuditEntity<{type.ClrType.Name}Audit, {type.ClrType.Name}, {keyTypeStr}>
//    {{
//{string.Join("\r\n", type.GetProperties().Where(p => p.Name != "Id").Select(GetPropertyStr))}

//        /// <summary>
//        /// 配置
//        /// </summary>
//        /// <param name=""entityBuilder""></param>
//        /// <param name=""dbContext""></param>
//        /// <param name=""dbContextLocator""></param>
//        public override void Configure(EntityTypeBuilder<{type.ClrType.Name}Audit> entityBuilder, DbContext dbContext, Type dbContextLocator)
//        {{
//            entityBuilder.HasIndex(audit => audit.EntityId);{GetPropertyDefaultValue(type)}{GetPropertyPrecision(type)}{GetPropertyConversion(type)}

//            this.ConfigCore(entityBuilder, dbContext, dbContextLocator);
//        }}

//        /// <summary>
//        /// 从实体转存
//        /// </summary>
//        /// <param name=""entity"">实体</param>
//        public override void Dump({type.ClrType.Name} entity)
//        {{
//            this.EntityId = entity.Id;
//            {string.Join("\r\n            ", type.GetProperties().Where(p => p.Name != "Id").Select(p => $"this.{p.Name} = entity.{p.Name};"))}
//        }}

//        partial void ConfigCore(EntityTypeBuilder<{type.ClrType.Name}Audit> entityBuilder, DbContext dbContext, Type dbContextLocator);
//    }}
//}}
//";
//            OutputFile(configTemplate, auditOutputFunc(type.ClrType));
//        }
        private static void GenerateOdataEnum(Type enumType, Func<Type, string> outputFunc)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException();
            }
            var template =
@$"/*
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
namespace {enumType.Namespace}
{{
    using com.seagullrobot.EMIS.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {enumType.Name}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            var enumType = builder.EnumType<{enumType.Name}>();
{string.Join("\r\n", Enum.GetNames(enumType).Select(str => $"            enumType.Member({enumType.Name}.{str});"))}
        }}
    }}
}}
";
            var fullPath = outputFunc(enumType);
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using var writer = new StreamWriter(fullPath, false, Encoding.UTF8);

            writer.Write(template);
        }
        private static string GenerateOdataProperty(IEntityType type)
        {
            var sb = new StringBuilder();

            foreach (var item in type.GetKeys())
            {
                var keyStr = item.Properties.Count == 1 ? $"entity.{item.Properties[0].Name}" : $"new {{ {string.Join(", ", item.Properties.Select(p => $"entity.{p.Name}").ToArray()) } }}";
                sb.AppendLine($"            entityType.HasKey(entity => {keyStr});");
            }
            foreach (var property in type.GetProperties())
            {
                if (property.IsKey())
                {
                    continue;
                }
                var maxLength = property.GetMaxLength();
                var precision = property.GetPrecision();
                //var required = item.IsRequired();
                var needVar = maxLength.HasValue || precision.HasValue || !property.IsNullable;
                var header = needVar ? $"var {property.Name.ToLower()}Config = " : "";
                //var appendConfig = 
                //    maxLength.HasValue ? $".MaxLength = {maxLength.Value}" :
                //    precision.HasValue ? $".Precision = {precision.Value}" : "";
                sb.AppendLine($"            {(needVar ? $"var {property.Name.ToLower()}Config = " : "")}entityType.{((property.ClrType.IsEnum || (property.ClrType.IsGenericType && property.ClrType.GenericTypeArguments.All(a => a.IsEnum))) ? "EnumProperty" : "Property")}(entity => entity.{property.Name});");
                if (maxLength.HasValue)
                {
                    sb.AppendLine($"            {property.Name.ToLower()}Config.MaxLength = {maxLength.Value};");
                }
                if (precision.HasValue)
                {
                    sb.AppendLine($"            {property.Name.ToLower()}Config.Precision = {precision.Value};");
                }
                if (!property.IsNullable)
                {
                    sb.AppendLine($"            {property.Name.ToLower()}Config.IsRequired();");
                }
            }
            return sb.ToString();
        }
        private static void GenerateOdataEntity(IEntityType type, Func<Type, string> configOutputFunc)
        {
            var tableTemplate =
@$"/*
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
namespace {type.ClrType.Namespace}
{{
    using com.seagullrobot.EMIS.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {type.ClrType.Name}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            var entitySet = builder.EntitySet<{type.ClrType.Name}>(""{type.ClrType.Name}"");
            var entityType = entitySet.EntityType;
{GenerateOdataProperty(type)}
            this.Config(entitySet);
        }}        
        partial void Config(EntitySetConfiguration<{type.ClrType.Name}> entitySet);
    }}
}}
";
            var viewTemplate =
@$"/*
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
namespace {type.ClrType.Namespace}
{{
    using com.seagullrobot.EMIS.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {type.ClrType.Name}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            var entityType = builder.ComplexType<{type.ClrType.Name}>();
{GenerateOdataProperty(type)}
            this.Config(entityType);
        }}        
        partial void Config(ComplexTypeConfiguration<{type.ClrType.Name}> entityType);
    }}
}}
";
            var isView = type.ClrType.GetInterfaces().Any(i => i == typeof(IPrivateEntityNotKey))
                || type.FindPrimaryKey() == null;
            OutputFile(isView ? viewTemplate : tableTemplate, configOutputFunc(type.ClrType));
        }
        private static void OutputFile(string str, string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            using var writer = new StreamWriter(path, false, Encoding.UTF8);
            writer.Write(str);
        }
        static IHostBuilder CreateHostBuilder() =>
           Host.CreateDefaultBuilder()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder
                   .Inject()
                   .UseStartup<Startup>();
               });

        class GenTenantDbContext : AppDbContext<GenTenantDbContext, TenantMasterLocator>
        {
            public GenTenantDbContext(
                DbContextOptions<GenTenantDbContext> options) : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var str = "Server=localhost;Database=modus;User Id=root;Password=sr160608;";
                var version = ServerVersion.AutoDetect(str);
                optionsBuilder.UseMySql(str, version);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        class GenManageDbContext : AppDbContext<GenManageDbContext>
        {
            public GenManageDbContext(
                DbContextOptions<GenManageDbContext> options) : base(options)
            {
            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //var str = "Data Source=192.168.31.51;Database=modus;User ID=sr;Password=Seagull@2020;pooling=true;port=3306;sslmode=none;CharSet=utf8;";
                var str = "Server=localhost;Database=modus;User Id=root;Password=sr160608;";
                var version = ServerVersion.AutoDetect(str);
                optionsBuilder.UseMySql(str, version);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        class Startup
        {

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDatabaseAccessor(options =>
                {
                    options
                    .CustomizeMultiTenants()
                    .AddDb<GenTenantDbContext, TenantMasterLocator>()
                    .AddDb<GenManageDbContext, MasterDbContextLocator>()
                    ;
                });
            }
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            { }
        }
    }
}
