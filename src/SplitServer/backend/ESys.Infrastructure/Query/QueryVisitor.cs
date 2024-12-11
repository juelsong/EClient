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
namespace ESys.Infrastructure.Query
{
    using ESys.Contract.Query;
    using ESys.Infrastructure.Entity;
    using ESys.Utilty.Query;
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Location查询过滤器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LocationQueryVisitorBase<T> : IEntityQueryVisitor
    {
        /// <summary>
        /// 获取Location表达式
        /// </summary>
        /// <returns></returns>
        protected abstract Expression<Func<T, Location>> GetExpression();
        /// <summary>
        /// 访问查询，修改查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public virtual string VisitQuery(int userId, string locationBreadcrumb, Type entityType)
        {
            return entityType == typeof(T)
                ? ODataVisitor.GetLocationFilterString<T, Location>(this.GetExpression(), locationBreadcrumb)
            : string.Empty;
        }
        /// <summary>
        /// 访问查询，修改关联查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="model"></param>
        /// <param name="navigations">导航属性类型数组</param>
        /// <returns>关联查询条件</returns>
        public virtual SingleValueNode VisitQuery(int userId, string locationBreadcrumb, IEdmModel model, params Type[] navigations)
        {
            return navigations.Last() == typeof(T)
                ? ODataVisitor.GetFilterConditionByLocation<T, Location>(model, this.GetExpression(), locationBreadcrumb)
                : null;
        }
    }
    /// <summary>
    /// Site查询过滤器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SiteQueryVisitorBase<T> : IEntityQueryVisitor
    {
        /// <summary>
        /// 获取Site表达式
        /// </summary>
        /// <returns></returns>
        protected abstract Expression<Func<T, Site>> GetExpression();
        /// <summary>
        /// 访问查询，修改查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="entityType">实体类型</param>
        /// <returns></returns>
        public virtual string VisitQuery(int userId, string locationBreadcrumb, Type entityType)
        {
            return entityType == typeof(T)
                ? ODataVisitor.GetSiteFilterString<T, Site>(this.GetExpression(), locationBreadcrumb)
            : string.Empty;
        }

        /// <summary>
        /// 访问查询，修改关联查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="model"></param>
        /// <param name="navigations">导航属性类型数组</param>
        /// <returns>关联查询条件</returns>
        public virtual SingleValueNode VisitQuery(int userId, string locationBreadcrumb, IEdmModel model, params Type[] navigations)
        {
            return navigations.Last() == typeof(T)
                ? ODataVisitor.GetFilterConditionBySite<T, Site>(model, this.GetExpression(), locationBreadcrumb)
                : null;
        }
    }

    internal class SiteQueryVisitor : LocationQueryVisitorBase<Site>
    {
        protected override Expression<Func<Site, Location>> GetExpression() => t => t.Location;
    }

    internal class LocationQueryVisitor : IEntityQueryVisitor
    {
        public string VisitQuery(int userId, string locationBreadcrumb, Type entityType)
        {
            return entityType == typeof(Location)
                ? string.IsNullOrEmpty(locationBreadcrumb) ? string.Empty : $"startswith(LocationExtra/Breadcrumb,'{locationBreadcrumb}')"
                : string.Empty;
        }

        public SingleValueNode VisitQuery(int userId, string locationBreadcrumb, IEdmModel model, params Type[] navigations)
        {
            return null;
        }
    }
}
