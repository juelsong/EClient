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

namespace ESys.Contract.Db
{
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using System.Collections.Generic;

    /// <summary>
    /// 数据库拦截器提供者
    /// </summary>
    public interface IInterceptorProvider
    {
        /// <summary>
        /// 获取拦截器
        /// </summary>
        /// <typeparam name="T">数据库上下文</typeparam>
        /// <typeparam name="TLocator">数据库定位器</typeparam>
        /// <returns></returns>
        IEnumerable<IInterceptor> GetInterceptors<T, TLocator>() where T : DbContext where TLocator : IDbContextLocator;
    }
}
