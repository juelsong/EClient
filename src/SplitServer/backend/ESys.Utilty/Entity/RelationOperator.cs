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

namespace ESys.Utilty.Entity
{
    using ESys.Contract.Entity;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 关系处理
    /// </summary>
    public static class RelationOperator
    {
        /// <summary>
        /// 同步关联，现有关联项如果在更新关联中不存在主键一致，则删除
        /// 更新关联项如果在现有关联中不存在主键一致，则添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="exist">现有的关联</param>
        /// <param name="model">更新的关联</param>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="keyFunc">主键函数</param>
        public static void SynchronizeRelations<TEntity, TKey>(
            IEnumerable<TEntity> exist,
            IEnumerable<TEntity> model,
            DbContext ctx, 
            Func<TEntity, TKey> keyFunc)
            where TEntity : class, IPrivateEntity, new()
            where TKey : struct, IConvertible
        {
            var modelKeys = model.Select(m => keyFunc(m)).ToArray();
            foreach (var item in exist.Where(e => !modelKeys.Contains(keyFunc(e))))
            {
                ctx.Entry(item).State = EntityState.Deleted;
            }
            foreach (var item in model)
            {
                if (keyFunc(item).Equals(default(TKey)))
                {
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
        }

        /// <summary>
        /// 同步关联，现有关联项如果在更新关联中不存在主键一致，则禁用
        /// 更新关联项如果在现有关联中不存在主键一致，则添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="exist">现有的关联</param>
        /// <param name="model">更新的关联</param>
        /// <param name="ctx">数据库上下文</param>
        /// <param name="keyFunc">主键函数</param>
        public static void SynchronizeActivedRelations<TEntity, TKey>(
            IEnumerable<TEntity> exist, 
            IEnumerable<TEntity> model,
            DbContext ctx, 
            Func<TEntity, TKey> keyFunc)
            where TEntity : class, IPrivateEntity, IActiveEntity, new()
            where TKey : struct, IConvertible
        {
            var modelKeys = model.Select(m => keyFunc(m)).ToArray();
            foreach (var item in exist.Where(e => !modelKeys.Contains(keyFunc(e))))
            {
                item.IsActive = false;
                ctx.Entry(item).State = EntityState.Modified;
            }
            foreach (var item in model)
            {
                if (keyFunc(item).Equals(default(TKey)))
                {
                    ctx.Entry(item).State = EntityState.Added;
                }
            }
        }
    }
}
