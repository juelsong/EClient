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
namespace ESys.Utilty.Service
{
    using ESys.Contract.Db;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class DbInterceptorProvider : IInterceptorProvider
    {
        private readonly IInterceptor[] interceptors;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DbInterceptorProvider(IServiceProvider serviceProvider)
        {
            this.interceptors = serviceProvider.GetServices<SaveChangesInterceptor>().ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TLocator"></typeparam>
        /// <returns></returns>
        public IEnumerable<IInterceptor> GetInterceptors<T, TLocator>()
            where T : DbContext
            where TLocator : IDbContextLocator
        {
            return this.interceptors;
        }
    }
}
