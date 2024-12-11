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
    using System.Linq;
    using System.Text;


    [Generator]
    public class AuditEntityGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not ClassSyntaxReceiver receiver)
            {
                return;
            }
            var compilation = context.Compilation;

            foreach (var classSymbol in receiver.Candidates)
            {
                if (CreateAuditCode(compilation, classSymbol, out var className, out var code))
                {
                    context.AddSource($"{className}.gen.cs", SourceText.From(code, Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
        }

        private static bool CreateAuditCode(Compilation compilation, ClassDeclarationSyntax clazz, out string className, out string code)
        {
            var propertySyntax = clazz.Members.OfType<PropertyDeclarationSyntax>()
                .Where(p =>
                {
                    var type = compilation.GetSemanticModel(p.SyntaxTree).GetDeclaredSymbol(p)!.Type;
                    return type.IsValueType || type.Name == "String";
                })
                .ToArray();

            var method = clazz.Members.OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(m => compilation.GetSemanticModel(m.SyntaxTree).GetDeclaredSymbol(m)!.Name == "Configure");

            var configedProperties = method?.Body == null
                ? Array.Empty<string>()
                : method.Body.Statements.Where(s =>
                    s.DescendantNodes().OfType<IdentifierNameSyntax>().Any(n => n.Identifier.Text == "HasPrecision")
                    || s.DescendantNodes().OfType<GenericNameSyntax>().Any(n => n.Identifier.Text == "HasConversion")
                ).Select(s => s.ToFullString()).ToArray();

            string GetEntityPropertyStr(PropertyDeclarationSyntax syntax)
            {
                var propertySymbol = compilation.GetSemanticModel(syntax.SyntaxTree).GetDeclaredSymbol(syntax);
                var trivias = syntax.GetLeadingTrivia();
                var xmlCommentTrivia = trivias.FirstOrDefault(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia));
                var state = propertySymbol!.Type.NullableAnnotation == NullableAnnotation.Annotated
                            ? NullableFlowState.MaybeNull
                            : NullableFlowState.None;
                return $@"{xmlCommentTrivia.ToFullString()}        public {propertySymbol!.Type.ToDisplayString(state)} {propertySymbol!.Name} {{ get; set;}}";
            }
            var auditSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditEntityAttribute");
            var disableSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditDisableAttribute");
            var classSymbol = compilation.GetSemanticModel(clazz.SyntaxTree).GetDeclaredSymbol(clazz);
            var attributeSymbols = classSymbol!.GetAttributesWithInherited().ToArray();
            className = code = string.Empty;
            if (classSymbol!.BaseType != null
                && !classSymbol.IsAbstract
                && classSymbol.BaseType.TypeArguments != null
                && classSymbol.BaseType.TypeArguments.Length > 1
                && attributeSymbols.Any(a => auditSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                && attributeSymbols.All(a => !disableSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability)))
            {
                var keyType = classSymbol.BaseType.TypeArguments[1];
                var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
                var propertyStrs = propertySyntax
                    .Select(p => GetEntityPropertyStr(p)).ToArray();
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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System = global::System;

    /// <summary>
    /// {classSymbol.Name}审计 自动生成，请勿修改
    /// </summary>
    /// 
#pragma warning disable 8618
    public partial class {classSymbol.Name}Audit : AuditEntity<{classSymbol.Name}Audit, {classSymbol.Name}, {keyType.ToDisplayString(NullableFlowState.None)}>
    {{
{string.Join("\r\n", propertyStrs.Select(str => $"        {str}"))}

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name=""entityBuilder""></param>
        /// <param name=""dbContext""></param>
        /// <param name=""dbContextLocator"" ></param>
        public override void Configure(EntityTypeBuilder<{classSymbol.Name}Audit> entityBuilder, DbContext dbContext, System.Type dbContextLocator)
        {{
            entityBuilder.HasIndex(audit => audit.EntityId);
{string.Join("\r\n", configedProperties)}
            this.ConfigCore(entityBuilder, dbContext, dbContextLocator);
        }}

        partial void ConfigCore(EntityTypeBuilder<{classSymbol.Name}Audit> entityBuilder, DbContext dbContext, System.Type dbContextLocator);
    }}
#pragma warning restore 8618
}}";
                code = ret;
                className = $"{classSymbol.Name}Audit";
                return true;
            }
            return false;
        }
    }
}
