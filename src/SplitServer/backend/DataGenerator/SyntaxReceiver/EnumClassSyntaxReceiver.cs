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
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System.Collections.Generic;

    internal class EnumClassSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> ClazzCandidates { get; } = new();
        public List<EnumDeclarationSyntax> EnumCandidates { get; } = new();
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax typeDeclarationSyntax)
            {
                this.ClazzCandidates.Add(typeDeclarationSyntax);
            }
            else if (syntaxNode is EnumDeclarationSyntax enumDeclarationSyntax)
            {
                this.EnumCandidates.Add(enumDeclarationSyntax);
            }
        }
    }
}
