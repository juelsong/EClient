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

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ESys.Utilty.Extensions
{
    /// <summary>
    /// IQueryable扩展
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IQueryable<T> IncludeExpandIfNotNull<T>(this IQueryable<T> repository, T entity) 
            where T : class, IPrivateEntity, new()
        {
            var properties = typeof(T).GetProperties()
                .Where(t => t.PropertyType.IsGenericType 
                         && t.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) 
                         && t.GetValue(entity) != null)
                .ToArray();
            var ret = repository;
            foreach (var property in properties)
            {
                ret = ret.Include(property.Name);
            }
            return ret;
        }
    }
}
