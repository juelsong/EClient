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
    using System;
    using System.Collections.Generic;
    using System.Linq;


    internal static class SymbolExtensions
    {
        public static bool TryGetAttribute(this ISymbol symbol, INamedTypeSymbol attributeType, out IEnumerable<AttributeData> attributes)
        {
            attributes = symbol.GetAttributes()
                .Where(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeType));
            return attributes.Any();
        }

        public static bool HasAttribute(this ISymbol symbol, INamedTypeSymbol attributeType)
        {
            return symbol.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeType));
        }

        public static IEnumerable<AttributeData> GetAttributesWithInherited(this INamedTypeSymbol typeSymbol)
        {
            foreach (var attribute in typeSymbol.GetAttributes())
            {
                yield return attribute;
            }

            var baseType = typeSymbol.BaseType;
            while (baseType != null)
            {
                foreach (var attribute in baseType.GetAttributes())
                {
                    if (IsInherited(attribute))
                    {
                        yield return attribute;
                    }
                }

                baseType = baseType.BaseType;
            }
        }

        private static bool IsInherited(this AttributeData attribute)
        {
            if (attribute.AttributeClass == null)
            {
                return false;
            }

            foreach (var attributeAttribute in attribute.AttributeClass.GetAttributes())
            {
                var @class = attributeAttribute.AttributeClass;
                if (@class != null && @class.Name == nameof(AttributeUsageAttribute) &&
                    @class.ContainingNamespace?.Name == "System")
                {
                    foreach (var kvp in attributeAttribute.NamedArguments)
                    {
                        if (kvp.Key == nameof(AttributeUsageAttribute.Inherited))
                        {
                            return (bool)kvp.Value.Value!;
                        }
                    }

                    // Default value of Inherited is true
                    return true;
                }
            }

            return false;
        }
    }
}
