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
    using ESys.Security.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 邮件
    /// </summary>
    [AuditDisable]
    public partial class EMail : BizEntity<EMail, long>
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 是否为html内容
        /// </summary>
        public bool IsHtmlBody { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTimeOffset? SendDate { get; set; }

        /// <summary>
        /// 收件人Id
        /// </summary>
		public int UserId { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public virtual ICollection<EMailAttachment> Attachments { get; set; }

        /// <summary>
        /// 相关的通知
        /// </summary>
        public virtual ICollection<Notification> Notifications { get; set; }

        /// <summary>
        /// 通知关联
        /// </summary>
        public virtual ICollection<NotificationEMail> NotificationEMails { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<EMail> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);
        }
    }
}
