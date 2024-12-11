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

namespace ESys.DataGenerator
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [Generator]
    class ODataEntityBuilderGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new EnumClassSyntaxReceiver());
        }
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not EnumClassSyntaxReceiver receiver)
            {
                return;
            }
            var compilation = context.Compilation;
            foreach (var enumSymbol in receiver.EnumCandidates)
            {
                if (CreateODataEnumCode(compilation, enumSymbol, out var enumName, out var code))
                {
                    context.AddSource($"{enumName}ODataConfig.gen.cs", SourceText.From(code, Encoding.UTF8));
                }
            }
            foreach (var classSymbol in receiver.ClazzCandidates)
            {
                foreach (var item in CreateODataCode(compilation, classSymbol))
                {
                    context.AddSource($"{item.clazzName}ODataConfig.gen.cs", SourceText.From(item.code, Encoding.UTF8));
                }
            }
        }
        private static bool CreateODataEnumCode(
            Compilation compilation,
            EnumDeclarationSyntax enumDeclarationSyntax,
            out string enumName,
            out string code)
        {
            var fields = enumDeclarationSyntax.Members.OfType<EnumMemberDeclarationSyntax>()
                .ToArray();
            var enableSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataConfigAttribute");
            var ignoreSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataConfigIgnoreAttribute");
            var enumSymbol = compilation.GetSemanticModel(enumDeclarationSyntax.SyntaxTree).GetDeclaredSymbol(enumDeclarationSyntax);
            var attributeSymbols = enumSymbol!.GetAttributesWithInherited().ToArray();
            enumName = code = string.Empty;
            if (enumSymbol!.BaseType != null
                && attributeSymbols.Any(a => enableSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                && attributeSymbols.All(a => !ignoreSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability)))
            {
                var fieldStrs = fields
                    .Select(p => $"            enumType.Member({enumSymbol.Name}.{p.Identifier.Text});").ToArray();
                var namespaceName = enumSymbol.ContainingNamespace.ToDisplayString();
                var ret = $@"/*
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
namespace {namespaceName}
{{
    using ESys.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {enumSymbol.Name}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            builder.Namespace = ""{namespaceName}.OData"";
            var enumType = builder.EnumType<{enumSymbol.Name}>();
{string.Join("\r\n", fieldStrs)}
            this.Config(enumType);
        }}

        partial void Config(EnumTypeConfiguration<{enumSymbol.Name}> enumType);
    }}
}}";
                code = ret;
                enumName = $"{enumSymbol.Name}Audit";
                return true;
            }
            return false;
        }
        private class PropertyInfo
        {
            public PropertyInfo(string name)
            {
                this.Name = name;
            }
            public string Name { get; }
            public bool IsRequired { get; set; }
            public int? MaxLength { get; set; }
            public int? Precision { get; set; }
            public int? Scale { get; set; }
            public bool Ignore { get; set; }
            public bool IsEnum { get; set; }
            public string TypeString { get; set; } = string.Empty;
            public bool IsNavigateion { get; set; }
            public bool IsCollection { get; set; }
        }
        private static string GetEntityConfig(string namespaceName, string className, bool isView, IEnumerable<PropertyInfo> propertyInfos)
        {
            var hasPrecisionTypes = new[] { typeof(DateTimeOffset).FullName, typeof(TimeSpan).FullName };
            var hasScaleTypes = new[] { typeof(decimal).Name };
            Func<PropertyInfo, string> getPropertyStr = (info) =>
            {
                if (info.Ignore)
                {
                    return $"            entityType.Ignore(entity => entity.{info.Name});";
                }
                if (info.IsNavigateion)
                {
                    return info.IsCollection
                        ? $"            entityType.HasMany(entity => entity.{info.Name});"
                        : $"            entityType.{(info.IsRequired ? "HasRequired" : "HasOptional")}(entity => entity.{info.Name});";
                }
                else
                {
                    if (info.IsRequired || info.MaxLength.HasValue || info.Precision.HasValue)
                    {
                        var configName = $"{info.Name.Substring(0, 1).ToLower()}{info.Name.Substring(1)}Config";
                        var sb = new StringBuilder($@"            var {configName} = entityType.{(info.IsEnum ? "EnumProperty" : "Property")}(entity => entity.{info.Name});");
                        if (info.IsRequired)
                        {
                            sb.Append($"\r\n            {configName}.IsRequired();");
                        }
                        if (info.MaxLength.HasValue)
                        {
                            sb.Append($"\r\n            {configName}.MaxLength = {info.MaxLength.Value};");
                        }
                        // odata中DateTimeOffset TimeSpan TimeOfDay 有Precision， decimal还有Scale
                        if (hasPrecisionTypes.Any(t => info.TypeString.StartsWith(t, StringComparison.OrdinalIgnoreCase)) && info.Precision.HasValue)
                        {
                            sb.Append($"\r\n            {configName}.Precision = {info.Precision.Value};");
                        }
                        if (hasScaleTypes.Any(t => info.TypeString.StartsWith(t, StringComparison.OrdinalIgnoreCase)) && info.Precision.HasValue)
                        {
                            sb.Append($"\r\n            {configName}.Precision = {info.Precision.Value};");
                            if (info.Scale.HasValue)
                            {
                                sb.Append($"\r\n            {configName}.Scale = {info.Scale.Value};");
                            }
                        }
                        return sb.ToString();
                    }
                    else
                    {
                        return $"            entityType.{(info.IsEnum ? "EnumProperty" : "Property")}(entity => entity.{info.Name});";
                    }
                }
            };
            var tableTemplate = $@"/*
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
namespace {namespaceName}
{{
    using ESys.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {className}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            builder.Namespace = ""{namespaceName}.OData"";
            var entitySet = builder.EntitySet<{className}>(""{className}"");
            var entityType = entitySet.EntityType;
            entityType.HasKey(entity => entity.Id);
{string.Join("\r\n", propertyInfos.Select(p => getPropertyStr(p)))}
            this.Config(entitySet);
        }}        
        partial void Config(EntitySetConfiguration<{className}> entitySet);
    }}
}}";
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
namespace {namespaceName}
{{
    using ESys.Contract.Entity;
    using Microsoft.OData.ModelBuilder;

    /// <summary>
    /// OData配置 自动生成，请勿修改
    /// </summary>
    public partial class {className}ODataConfig : IODataEntityBuilder
    {{
        /// <summary>
        /// 生成OData模型
        /// </summary>
        /// <param name=""builder""></param>
        public void BuildODataModel(ODataModelBuilder builder)
        {{
            builder.Namespace = ""{namespaceName}.OData"";
            var entityType = builder.ComplexType<{className}>();
{string.Join("\r\n", propertyInfos.Select(p => getPropertyStr(p)))}
            this.Config(entityType);
        }}        
        partial void Config(ComplexTypeConfiguration<{className}> entityType);
    }}
}}
";
            return isView ? viewTemplate : tableTemplate;
        }

        private static IEnumerable<(string clazzName, string code)> CreateODataCode(Compilation compilation, ClassDeclarationSyntax clazz)
        {
            var enableSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataConfigAttribute");
            var ignoreSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataConfigIgnoreAttribute");

            var auditSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditEntityAttribute");
            var disableSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditDisableAttribute");
            var requiredSymbol = compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.RequiredAttribute");
            var stringLengthSymbol = compilation.GetTypeByMetadataName("System.ComponentModel.DataAnnotations.StringLengthAttribute");
            var ienumerableSymbol = compilation.GetTypeByMetadataName(typeof(System.Collections.IEnumerable).FullName);
            var classSymbol = compilation.GetSemanticModel(clazz.SyntaxTree).GetDeclaredSymbol(clazz);

            var attributeSymbols = classSymbol!.GetAttributesWithInherited().ToArray();

            if (classSymbol!.BaseType != null
            && !classSymbol.IsAbstract
            && attributeSymbols.Any(a => enableSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
            && attributeSymbols.All(a => !ignoreSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability)))
            {
                var method = clazz.Members.OfType<MethodDeclarationSyntax>()
                    .FirstOrDefault(m => compilation.GetSemanticModel(m.SyntaxTree).GetDeclaredSymbol(m)!.Name == "Configure");
                var isView = classSymbol.BaseType.Name.StartsWith("BizView");
                var configedProperties = new List<PropertyInfo>();
                if (method?.Body != null)
                {
                    foreach (var item in method.Body.Statements)
                    {
                        var nodes = item.DescendantNodes().ToArray();
                        var propertyNode = nodes.OfType<IdentifierNameSyntax>().FirstOrDefault(s => s.Identifier.Text == "Property");
                        var maxLengthSymbol = nodes.OfType<IdentifierNameSyntax>().FirstOrDefault(s => s.Identifier.Text == "HasMaxLength");
                        var precisionSymbol = nodes.OfType<IdentifierNameSyntax>().FirstOrDefault(s => s.Identifier.Text == "HasPrecision");
                        var isRequiredSymbol = nodes.OfType<IdentifierNameSyntax>().FirstOrDefault(s => s.Identifier.Text == "IsRequired");
                        if (propertyNode != null)
                        {
                            var lambdaProperty = nodes.SkipWhile(n => n != propertyNode).Skip(1).OfType<SimpleLambdaExpressionSyntax>().First();
                            var propertyName = lambdaProperty.DescendantNodes().OfType<IdentifierNameSyntax>().Last().Identifier.Text;
                            var propertyInfo = new PropertyInfo(propertyName);
                            if (maxLengthSymbol != null)
                            {
                                var maxValueSymbol = nodes.SkipWhile(n => n != maxLengthSymbol).Skip(1).OfType<LiteralExpressionSyntax>().First();
                                if (maxValueSymbol != null)
                                {
                                    propertyInfo.MaxLength = int.Parse(maxValueSymbol.Token.Text);
                                }
                            }
                            if (precisionSymbol != null)
                            {
                                var precisionArgSymbol = nodes.SkipWhile(n => n != precisionSymbol).Skip(1).OfType<LiteralExpressionSyntax>().ToArray();
                                if (precisionArgSymbol.Any())
                                {
                                    propertyInfo.Precision = int.Parse(precisionArgSymbol.First().Token.Text);
                                    var scaleArgSymbol = precisionArgSymbol.Skip(1).FirstOrDefault();
                                    if (scaleArgSymbol != null)
                                    {
                                        propertyInfo.Scale = int.Parse(scaleArgSymbol.Token.Text);
                                    }
                                }
                            }
                            propertyInfo.IsRequired = isRequiredSymbol != null;
                            configedProperties.Add(propertyInfo);
                        }
                    }
                }
                var propertySyntax = clazz.Members.OfType<PropertyDeclarationSyntax>()
                    .Select(propertyDeclaration =>
                    {
                        var propertySymbol = compilation.GetSemanticModel(propertyDeclaration.SyntaxTree).GetDeclaredSymbol(propertyDeclaration);
                        var propertyTypeSymbol = propertySymbol!.Type;
                        var propertyAttributes = propertySymbol.GetAttributes();
                        var needConfig = propertyTypeSymbol.IsValueType || propertyTypeSymbol.Name == "String";
                        var required = propertyAttributes.Any(a => requiredSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                        || (propertyTypeSymbol.IsValueType && propertyDeclaration.Type is not NullableTypeSyntax);

                        var lengthAttr = propertyAttributes.FirstOrDefault(a => stringLengthSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))?.ConstructorArguments.FirstOrDefault();
                        int? maxLength = (int?)lengthAttr?.Value;
                        var info = configedProperties.FirstOrDefault(p => p.Name == propertySymbol.Name);
                        int? precision = null;
                        int? scale = null;
                        if (info != null)
                        {
                            required |= info.IsRequired;
                            if (!maxLength.HasValue)
                            {
                                maxLength = info.MaxLength;
                            }
                            precision = info.Precision;
                            scale = info.Scale;
                        }
                        var isNavigation = propertySymbol.IsVirtual;
                        var isCollection = propertyTypeSymbol.AllInterfaces.Contains(ienumerableSymbol!);
                        var isEnum = propertyTypeSymbol.TypeKind == TypeKind.Enum;
                        if (propertyTypeSymbol.TypeKind == TypeKind.Struct && (propertyTypeSymbol is INamedTypeSymbol namedTypeSymbol) && (namedTypeSymbol.TypeArguments.Any()))
                        {
                            var arg = namedTypeSymbol.TypeArguments.Single() as INamedTypeSymbol;
                            if (TypeKind.Enum.Equals(arg?.TypeKind))
                            {
                                isEnum = true;
                            }
                        }

                        var ignore = propertyAttributes.Any(a => ignoreSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability));

                        return (symbol: propertySymbol, typeString: propertyTypeSymbol.ToDisplayString(), needConfig, isNavigation, isCollection, ignore, isEnum, required, maxLength, precision, scale);
                    })
                    .Where(item => item.needConfig || item.isNavigation)
                    .Select(item => new PropertyInfo(item.symbol.Name)
                    {
                        IsRequired = item.required,
                        MaxLength = item.maxLength,
                        Precision = item.precision,
                        Scale = item.scale,
                        TypeString = item.typeString,
                        Ignore = item.ignore,
                        IsEnum = item.isEnum,
                        IsCollection = item.isCollection,
                        IsNavigateion = item.isNavigation
                    })
                    .ToList();
                var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
                var needAudit = attributeSymbols.Any(a => auditSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                    && attributeSymbols.All(a => !disableSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability));

                var ret = GetEntityConfig(namespaceName, classSymbol.Name, isView, propertySyntax);

                yield return (classSymbol.Name, ret);
                if (needAudit)
                {
                    var auditConfig = $"{classSymbol.Name}Audit";
                    propertySyntax.Add(new PropertyInfo("Action")
                    {
                        IsRequired = true,
                        IsEnum = true
                    });
                    propertySyntax.Add(new PropertyInfo("AuditTime")
                    {
                        IsRequired = true
                    });
                    propertySyntax.Add(new PropertyInfo("EntityId")
                    {
                        IsRequired = true
                    });
                    yield return (auditConfig, GetEntityConfig(namespaceName, auditConfig, false, propertySyntax.Where(p => !p.IsNavigateion)));
                }
            }
        }
    }
}
