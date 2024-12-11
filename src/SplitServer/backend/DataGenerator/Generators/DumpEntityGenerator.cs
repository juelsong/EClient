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
    using System.Linq;

    [Generator(LanguageNames.CSharp)]
    public class DumpEntityGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var typeSymbols =
             context.SyntaxProvider
             .CreateSyntaxProvider(
                 static (node, _) => node is ClassDeclarationSyntax classSyntax,
                 static (context, _) => new
                 {
                     Symbol = (INamedTypeSymbol)context.SemanticModel.GetDeclaredSymbol(context.Node)!,
                     Syntax = context.Node as ClassDeclarationSyntax,
                     context.SemanticModel.Compilation
                 });
            
            var entities = typeSymbols
                .Where(static (item) => item.Symbol.BaseType != null
                    && item.Symbol.BaseType.ToDisplayString().StartsWith("ESys.Contract.Entity.BizEntity<")
                    && !item.Symbol.IsAbstract);
            context.RegisterSourceOutput(entities, static (ctx, l) =>
            {
                if (CreateDumpCode(l.Compilation, l.Syntax!, out var className, out var code))
                {
                    ctx.AddSource($"Dump.{className}.gen.cs", code);
                }
            });
        }

        private static bool CreateDumpCode(Compilation compilation, ClassDeclarationSyntax clazz, out string className, out string code)
        {
            className = code = string.Empty;
            var propertySyntax = clazz.Members.OfType<PropertyDeclarationSyntax>()
                .Where(p =>
                {
                    var type = compilation.GetSemanticModel(p.SyntaxTree).GetDeclaredSymbol(p)!.Type;
                    return type.IsValueType || type.Name == "String";
                })
                .Select(p=> compilation.GetSemanticModel(p.SyntaxTree).GetDeclaredSymbol(p))
                .ToArray();
            if(propertySyntax.Length == 0)
            {
                return false;
            }
            var classSymbol = compilation.GetSemanticModel(clazz.SyntaxTree).GetDeclaredSymbol(clazz)!;
            var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            className = classSymbol.Name;
            var propertyStrs = propertySyntax
                .Select(p => $"            to.{p!.Name} = this.{p.Name};").ToList();
            code = $@"/*
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
    /// <summary>
    /// {classSymbol.Name}转储 自动生成，请勿修改
    /// </summary>
    public partial class {classSymbol.Name}
    {{
        /// <summary>
        /// 转储基本属性，结构、字符串
        /// </summary>
        /// <param name=""to""></param>
        /// <param name=""includeId""></param>
        public override void DumpBasicProperty({classSymbol.Name} to, bool includeId = false)
        {{
            if(includeId)
            {{
                to.Id = this.Id;
            }}            
{string.Join("\r\n", propertyStrs)}
        }}
    }}
}}
";
            return true;
        }
    }
}
