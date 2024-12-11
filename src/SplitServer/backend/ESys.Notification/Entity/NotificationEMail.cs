namespace ESys.Notification.Entity
{
    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using System;

    /// <summary>
    /// 通知电邮关联
    /// </summary>
    [AuditDisable]
    public partial class NotificationEMail : BizEntity<NotificationEMail, long>
    {
        /// <summary>
        /// 通知Id
        /// </summary>
		public long NotificationId { get; set; }

        /// <summary>
        /// 通知
        /// </summary>
        public virtual Notification Notification { get; set; }

        /// <summary>
        /// 电邮Id
        /// </summary>
		public long EMailId { get; set; }

        /// <summary>
        /// 电邮
        /// </summary>
        public virtual EMail EMail { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

    }
}
