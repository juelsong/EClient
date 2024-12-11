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
namespace ESys.Contract.Query
{
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 实体查询访问器
    /// </summary>
    public interface IEntityQueryVisitor
    {
        /// <summary>
        /// 访问查询，修改查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="entityType">实体类型</param>
        string VisitQuery(int userId, string locationBreadcrumb, Type entityType);
        /// <summary>
        /// 访问查询，修改关联查询过滤
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="locationBreadcrumb">用户所在区域面包屑</param>
        /// <param name="model"></param>
        /// <param name="navigations">导航属性类型数组</param>
        /// <returns>关联查询条件</returns>
        SingleValueNode VisitQuery(int userId, string locationBreadcrumb, IEdmModel model, params Type[] navigations);
    }
}
