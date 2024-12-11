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
    using ESys.Infrastructure.Entity;
    using ESys.Security.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 订阅实体
    /// </summary>
    [AuditDisable]
    public partial class Subscription : BizEntity<Subscription, long>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
		public int? UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 通知类型Id
        /// </summary>
		public int NotificationTypeId { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// 属性Id
        /// </summary>
        public int? PlanGroupId { get; set; }

        //public virtual PlanGroup PlanGroup {get; set;}

        /// <summary>
        /// 区域Id
        /// </summary>
		public int? LocationId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public virtual Location Location { get; set; }


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
        public override void Configure(EntityTypeBuilder<Subscription> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            entityBuilder
                .HasOne(s => s.NotificationType)
                .WithMany(n => n.Subscriptions)
                .HasForeignKey(s => s.NotificationTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder
                .HasOne(s => s.Location)
                .WithMany()
                .HasForeignKey(s => s.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            //entityBuilder
            //    .HasOne(s => s.Group)
            //    .WithMany()
            //    .HasForeignKey(s => s.GroupId)
            //    .OnDelete(DeleteBehavior.SetNull);

            //entityBuilder
            //    .HasOne(s => s.PlanGroup)
            //    .WithMany()
            //    .HasForeignKey(s => s.PlanGroupId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
