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

namespace ESys.Notification.Entity
{
    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 邮件附件
    /// </summary>
    [AuditDisable]
    public partial class EMailAttachment : BizEntity<EMailAttachment, long>
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 内容Id，如果UsedInBody为true，则必须与body中id一致
        /// </summary>
        public string ContentId { get; set; }
        /// <summary>
        /// 是否用于body 如果true，则用于body中展示
        /// </summary>
        public bool UsedInBody { get; set; }

        /// <summary>
        /// 邮件Id
        /// </summary>
		public long EMailId { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public virtual EMail EMail { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<EMailAttachment> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(e => e.EMail)
                .WithMany(e => e.Attachments)
                .HasForeignKey(e => e.EMailId);

        }
    }
}
