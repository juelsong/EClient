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

using ESys.Contract.Entity;
using ESys.DataAnnotations;
using ESys.Infrastructure.Entity;
using ESys.Security.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ESys.Notification.Entity
{
    /// <summary>
    /// 通知实体
    /// </summary>
    [AuditDisable]

    public partial class Notification : BizEntity<Notification, long>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        private static readonly JsonSerializerOptions defaultOptions = new();

        /// <summary>
        /// 通知内容，可自行设置，也可通过 Messages 设置为json数组
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 执行通知的时间戳
        /// </summary>
        public DateTimeOffset? NotificationDate { get; set; }
        /// <summary>
        /// 通知类型Id
        /// </summary>
		public int NotificationTypeId { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// 创建此通知的用户Id
        /// </summary>
		public int? UserId { get; set; }

        /// <summary>
        /// 创建此通知的用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 采样点Id
        /// </summary>
		public int? SiteId { get; set; }

        /// <summary>
        /// 采样点
        /// </summary>
        public virtual Site Site { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
		public int? EquipmentId { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public virtual Equipment Equipment { get; set; }

        /// <summary>
        /// 关联电邮
        /// </summary>
        public virtual ICollection<NotificationEMail> NotificationEMails { get; set; }
        /// <summary>
        /// 电邮
        /// </summary>
        public virtual ICollection<EMail> EMails { get; set; }
        /// <summary>
        /// 序列化的内容，已json数组格式设置到<see cref="Content"/>属性中
        /// </summary>
        [NotMapped]
        public string[] Messages
        {
            get
            {
                return DeserializeMessages(this.Content);
            }
            set
            {
                this.Content = Serialize(value);
            }
        }

        internal static string[] DeserializeMessages(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var jsonArray = JsonSerializer.Deserialize<JsonArray>(str, defaultOptions);
                return jsonArray == null ? Array.Empty<string>() : jsonArray.Select(i => (string)i).ToArray();
            }
            return Array.Empty<string>();
        }

        internal static string Serialize(string[] values)
        {
            return values == null || values.Length == 0
                    ? null
                    : JsonSerializer.Serialize(new JsonArray(values.Select(m => (JsonNode)m).ToArray()), defaultOptions);
        }

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

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        #endregion interfaces

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Notification> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(n => n.NotificationType)
                .WithMany(nt => nt.Notifications)
                .HasForeignKey(n => n.NotificationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder
                .HasOne(n => n.Site)
                .WithMany()
                .HasForeignKey(n => n.SiteId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder
                .HasOne(n => n.Equipment)
                .WithMany()
                .HasForeignKey(n => n.EquipmentId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder
                .HasMany(n => n.EMails)
                .WithMany(e => e.Notifications)
                .UsingEntity<NotificationEMail>(
                    ne => ne.HasOne(nei => nei.EMail).WithMany(e => e.NotificationEMails).HasForeignKey(ne => ne.EMailId),
                    ne => ne.HasOne(nei => nei.Notification).WithMany(n => n.NotificationEMails).HasForeignKey(ne => ne.NotificationId)
                );
        }
    }
}
