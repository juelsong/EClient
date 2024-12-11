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
    using System.Collections.Generic;

    /// <summary>
    /// 电子签名实体
    /// </summary>
    [AuditDisable]
    public partial class ElectronicSignature : BizEntity<ElectronicSignature, long>, ITimedEntity, ITraceableEntity
    {
        /// <summary>
        /// 签名账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 签名用户姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 签名用户Id
        /// </summary>
		public int UserId { get; set; }

        /// <summary>
        /// 签名用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 签名日期
        /// </summary>
        public DateTimeOffset SignDate { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 签名分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 是否系统自生成
        /// </summary>
        public bool IsSystemOperation { get; set; }
        /// <summary>
        /// 具体数据项
        /// </summary>
        public virtual ICollection<ElectronicSignatureItem> ElectronicSignatureItems { get; set; }

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
        public override void Configure(EntityTypeBuilder<ElectronicSignature> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasIndex(e => e.UserId);
        }
    }
}
