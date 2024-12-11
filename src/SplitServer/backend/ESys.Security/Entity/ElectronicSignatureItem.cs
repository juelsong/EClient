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
namespace ESys.Security.Entity
{
    using ESys.DataAnnotations;
    using ESys.Contract.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 电子签名数据项
    /// </summary>
    [AuditDisable]
    public partial class ElectronicSignatureItem : BizEntity<ElectronicSignatureItem, long>, ITimedEntity, ITraceableEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 被签名数据主键
        /// </summary>
        public long? PrimaryKey { get; set; }

        /// <summary>
        /// 电子签名Id
        /// </summary>
		public long ElectronicSignatureId { get; set; }

        /// <summary>
        /// 电子签名
        /// </summary>
        public virtual ElectronicSignature ElectronicSignature { get; set; }

        /// <summary>
        /// 最后审计实体主键
        /// </summary>
        public long? LastAuditKey { get; set; }

        #region interfaces
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }

        #endregion interfaces

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<ElectronicSignatureItem> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(i => i.ElectronicSignature)
                .WithMany(e => e.ElectronicSignatureItems)
                .HasForeignKey(i => i.ElectronicSignatureId)
                .OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasIndex(e => new { e.TableName, e.PrimaryKey });
        }
    }
}
