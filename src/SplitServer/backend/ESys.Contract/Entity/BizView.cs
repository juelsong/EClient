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

namespace ESys.Contract.Entity
{
    using ESys.DataAnnotations;
    using ESys.Contract.Db;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 业务视图基类
    /// </summary>
    [AuditDisable]
    [ODataConfig]
    [ODataExposed(false, false, false)]
    public abstract class BizView<TEntity> :
        IEntityNotKey<TenantMasterLocator, TenantSlaveLocator>,
        IEntityTypeBuilder<TEntity, TenantMasterLocator, TenantSlaveLocator>
        where TEntity : class, IEntityNotKey<TenantMasterLocator, TenantSlaveLocator>, new()
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }

        /// <summary>
        /// 获取视图名称
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.GetType().Name;
        }

#if NET6_0
        /// <summary>
        ///  数据库中定义的 Schema
        /// </summary>
        /// <returns></returns>
        public string GetSchema()
        {
            return null;
        }
#endif
    }
}
