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
namespace ESys.Utilty.Query
{
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// odata过滤
    /// </summary>
    public static class ODataVisitor
    {
        private static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
        private static void GetMemberInfo(Expression expression, List<MemberInfo> infos)
        {
            MemberExpression memberExpression = null;
            if (expression is LambdaExpression lambda)
            {
                if (lambda.Body is UnaryExpression)
                {
                    var unaryExpression = (UnaryExpression)lambda.Body;
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    memberExpression = (MemberExpression)lambda.Body;
                }
            }
            if (expression is MemberExpression member)
            {
                memberExpression = member;
            }
            if (memberExpression != null)
            {
                infos.Insert(0, memberExpression.Member);
                if (memberExpression.Expression != null)
                {
                    GetMemberInfo(memberExpression.Expression, infos);
                }
            }
        }
        /// <summary>
        /// 获取Site过滤条件($expand)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="model"></param>
        /// <param name="property"></param>
        /// <param name="locationBreadcrumb"></param>
        /// <returns></returns>
        public static SingleValueNode GetFilterConditionBySite<T, TProperty>(IEdmModel model, Expression<Func<T, TProperty>> property, string locationBreadcrumb)
        {
            if (string.IsNullOrEmpty(locationBreadcrumb))
            {
                return null;
            }
            var entitySet = model.FindDeclaredEntitySet(typeof(T).Name);
            var entityType = entitySet.EntityType();
            var siteEntityType = model.FindDeclaredEntitySet("Site").EntityType();
            var locationEntityType = model.FindDeclaredEntitySet("Location").EntityType();
            var locationExtraEntityType = model.EntityContainer.FindEntitySet("LocationExtra").EntityType();
            //var siteNavigationPropertyName = property.GetMemberInfo().Name;
            //var entitySiteNavProperty = entityType.NavigationProperties().FirstOrDefault(p => p.Name == siteNavigationPropertyName);
            var siteLocationNavProperty = siteEntityType.NavigationProperties().FirstOrDefault(p => p.Name == "Location");
            var locationExtraNavProperty = locationEntityType.NavigationProperties().FirstOrDefault(p => p.Name == "LocationExtra");           

            var itVariable = new ResourceRangeVariable("$it", (entitySet.Type as EdmCollectionType).ElementType.AsStructured(), entitySet);
            var entityNode = new ResourceRangeVariableReferenceNode("$it", itVariable);

            var locationPathList = new List<MemberInfo>();
            GetMemberInfo(property, locationPathList);
            SingleNavigationNode entitySiteNode = null;
            foreach (var path in locationPathList)
            {
                var sourceSet = model.FindDeclaredEntitySet(path.DeclaringType.Name);
                var navProperty = sourceSet.EntityType().NavigationProperties().FirstOrDefault(p => p.Name == path.Name);
                entitySiteNode = entitySiteNode == null
                                     ? new SingleNavigationNode(entityNode, navProperty, new EdmPathExpression(path.Name))
                                     : new SingleNavigationNode(entitySiteNode, navProperty, new EdmPathExpression(path.Name));
            }

            var siteLocationNode = new SingleNavigationNode(entitySiteNode, siteLocationNavProperty, new EdmPathExpression(siteLocationNavProperty.Name));
            var locationExtraNode = new SingleNavigationNode(siteLocationNode, locationExtraNavProperty, new EdmPathExpression(locationExtraNavProperty.Name));
            var locationExtraBreadcrumbNode = new SingleValuePropertyAccessNode(locationExtraNode, locationExtraEntityType.FindProperty("Breadcrumb"));
            // $it.Site.Location.LocationExtra.Breadcrumb.StartsWith(locationBreadcrumb)
            var locationExtraBreadcrumbFilterNode = new SingleValueFunctionCallNode(
                "startswith",
                new QueryNode[]
                {
                    locationExtraBreadcrumbNode,
                    new ConstantNode(locationBreadcrumb)
                },
                new EdmPrimitiveTypeReference(
                    EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean),
                    false)
                );
            // $it.Site == null
            var entitySiteNullNode = new BinaryOperatorNode(BinaryOperatorKind.Equal, entitySiteNode, new ConstantNode(null));

            var conv = new ConvertNode(
                locationExtraBreadcrumbFilterNode,
                new EdmPrimitiveTypeReference(
                    EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean),
                    true));
            // $it.Site == null || $it.Site.Location.LocationExtra.Breadcrumb.StartsWith(locationBreadcrumb)
            return new BinaryOperatorNode(BinaryOperatorKind.Or, entitySiteNullNode, conv);
        }
        /// <summary>
        /// 获取Location过滤条件($expand)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="model"></param>
        /// <param name="property"></param>
        /// <param name="locationBreadcrumb"></param>
        /// <returns></returns>
        public static SingleValueNode GetFilterConditionByLocation<T, TProperty>(IEdmModel model, Expression<Func<T, TProperty>> property, string locationBreadcrumb)
        {
            if (string.IsNullOrEmpty(locationBreadcrumb))
            {
                return null;
            }
            var entitySet = model.FindDeclaredEntitySet(typeof(T).Name);
            var entityType = entitySet.EntityType();
            var locationEntityType = model.FindDeclaredEntitySet("Location").EntityType();
            var locationExtraEntityType = model.EntityContainer.FindEntitySet("LocationExtra").EntityType();
            //var locationNavigationPropertyName = property.GetMemberInfo().Name;
            //var entityLocationNavProperty = entityType.NavigationProperties().FirstOrDefault(p => p.Name == locationNavigationPropertyName);
            var locationExtraNavProperty = locationEntityType.NavigationProperties().FirstOrDefault(p => p.Name == "LocationExtra");

            var itVariable = new ResourceRangeVariable("$it", (entitySet.Type as EdmCollectionType).ElementType.AsStructured(), entitySet);
            var entityNode = new ResourceRangeVariableReferenceNode("$it", itVariable);


            var locationPathList = new List<MemberInfo>();
            GetMemberInfo(property, locationPathList);
            SingleNavigationNode entityLocationNode = null;
            foreach (var path in locationPathList)
            {
                var sourceSet = model.FindDeclaredEntitySet(path.DeclaringType.Name);
                var navProperty = sourceSet.EntityType().NavigationProperties().FirstOrDefault(p => p.Name == path.Name);
                entityLocationNode = entityLocationNode == null
                                     ? new SingleNavigationNode(entityNode, navProperty, new EdmPathExpression(path.Name))
                                     : new SingleNavigationNode(entityLocationNode, navProperty, new EdmPathExpression(path.Name));
            }
            //var entityLocationNode = new SingleNavigationNode(entityNode, entityLocationNavProperty, new EdmPathExpression("Location"));
            var locationExtraNode = new SingleNavigationNode(entityLocationNode, locationExtraNavProperty, new EdmPathExpression("LocationExtra"));
            var locationExtraBreadcrumbNode = new SingleValuePropertyAccessNode(locationExtraNode, locationExtraEntityType.FindProperty("Breadcrumb"));
            // $it.Location.LocationExtra.Breadcrumb.StartsWith(locationBreadcrumb)
            var locationExtraBreadcrumbFilterNode = new SingleValueFunctionCallNode(
                "startswith",
                new QueryNode[]
                {
                    locationExtraBreadcrumbNode,
                    new ConstantNode(locationBreadcrumb)
                },
                new EdmPrimitiveTypeReference(
                    EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean),
                    false)
                );
            // $it.Location == null
            var entityLocationNullNode = new BinaryOperatorNode(BinaryOperatorKind.Equal, entityLocationNode, new ConstantNode(null));

            var conv = new ConvertNode(
                locationExtraBreadcrumbFilterNode,
                new EdmPrimitiveTypeReference(
                    EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Boolean),
                    true));
            // $it.Location == null || $it.Location.LocationExtra.Breadcrumb.StartsWith(locationBreadcrumb)
            return new BinaryOperatorNode(BinaryOperatorKind.Or, entityLocationNullNode, conv);
        }
        /// <summary>
        /// 获取Location过滤字符串($filter)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <param name="locationBreadcrumb"></param>
        /// <returns></returns>
        public static string GetLocationFilterString<T, TProperty>(Expression<Func<T, TProperty>> property, string locationBreadcrumb)
        {
            if (!string.IsNullOrEmpty(locationBreadcrumb))
            {
                var locationNavigationPropertyName = property.GetMemberInfo().Name;
                return $"({locationNavigationPropertyName} eq null) or (startswith({locationNavigationPropertyName}/LocationExtra/Breadcrumb, '{locationBreadcrumb}'))";
            }
            return null;
        }
        /// <summary>
        /// 获取Site过滤字符串($filter)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <param name="locationBreadcrumb"></param>
        /// <returns></returns>
        public static string GetSiteFilterString<T, TProperty>(Expression<Func<T, TProperty>> property, string locationBreadcrumb)
        {
            if (!string.IsNullOrEmpty(locationBreadcrumb))
            {
                var siteNavigationPropertyName = property.GetMemberInfo().Name;
                return $"({siteNavigationPropertyName} eq null) or (startswith({siteNavigationPropertyName}/Location/LocationExtra/Breadcrumb, '{locationBreadcrumb}'))";
            }
            return null;
        }
    }
}
