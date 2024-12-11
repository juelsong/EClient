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

using ESys.DataAnnotations;
using ESys.Contract.Db;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#if MIGRATION
using System.Linq;
#endif

namespace ESys.Contract.Entity
{
    /// <summary>
    /// 业务实体基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    [AuditEntity]
    [ODataConfig]
    [ODataExposed]
    public abstract class BizEntity<TEntity, TKey> : IEntity<TenantMasterLocator, TenantSlaveLocator>,
        IEntityTypeBuilder<TEntity, TenantMasterLocator, TenantSlaveLocator>,
        IEntitySeedData<TEntity, TenantMasterLocator, TenantSlaveLocator>,
        IDumpEntity<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TKey : struct, IConvertible
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }


        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> entityBuilder, Microsoft.EntityFrameworkCore.DbContext dbContext, Type dbContextLocator)
        {
        }

        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> HasData(Microsoft.EntityFrameworkCore.DbContext dbContext, Type dbContextLocator)
        {
#if MIGRATION && !NOSEEDDATA
            return this.HasDataCore(dbContext, dbContextLocator).ToArray();
#else
            return Array.Empty<TEntity>();
#endif
        }

        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TEntity> HasDataCore(Microsoft.EntityFrameworkCore.DbContext dbContext, Type dbContextLocator)
        {
            return Array.Empty<TEntity>();
        }

        /// <summary>
        /// 转储基本属性，结构、字符串
        /// </summary>
        /// <param name="to"></param>
        /// <param name="includeId"></param>
        public abstract void DumpBasicProperty(TEntity to, bool includeId = false);
    }
}
