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
    using ESys.Contract.Service;
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 通知类型
    /// </summary>
    [AuditDisable]

    [DeactivateCheck(nameof(NotificationType.Subscriptions), nameof(Subscription.UserId))]
    public partial class NotificationType : BizEntity<NotificationType, int>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 通知类型标签
        /// </summary>
        public NotificationTypes Type { get; set; }
        /// <summary>
        /// 通知类型名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 通知类型描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 英文
        /// </summary>
        public string EnName { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        public string ZhName { get; set; }
        /// <summary>
        /// 英文
        /// </summary>
        public string EnDescription { get; set; }
        /// <summary>
        /// 中文
        /// </summary>
        public string ZhDescription { get; set; }
        /// <summary>
        /// 通知类型处理器类型，此类通知都由一种处理器处理
        /// </summary>
        public string ProcessorType { get; set; }

        /// <summary>
        /// 属于此类型的通知
        /// </summary>
        public ICollection<Notification> Notifications { get; set; }

        /// <summary>
        /// 属于此类型的订阅
        /// </summary>
        public ICollection<Subscription> Subscriptions { get; set; }

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
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<NotificationType> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            var id = 1;
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.Deviation,
                Name = "Excursion",
                Description = "Excursion",
                EnName = "Excursion",
                EnDescription = "Excursion",
                ZhName = "超限",
                ZhDescription = "触发限度时发送通知",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.LoginFailure,
                Name = "Login Failure",
                Description = "Login Failure",
                EnName = "Login Failure",
                EnDescription = "Login Failure",
                ZhName = "登录失败",
                ZhDescription = "登录失败",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.InvalidESig,
                Name = "Invalid ESig",
                Description = "Invalid electronic signature was attempted.",
                EnName = "Invalid ESig",
                EnDescription = "Invalid electronic signature was attempted.",
                ZhName = "无效电子签名",
                ZhDescription = "无效电子签名",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.AccountLocked,
                Name = "Account Locked",
                Description = "A user account was locked.",
                EnName = "Account Locked",
                EnDescription = "A user account was locked.",
                ZhName = "账户锁定",
                ZhDescription = "账户锁定",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.SampleNotCompleted,
                Name = "Scheduled Samples Not Assigned",
                Description = "Work items in their first stage that have been scheduled but are not yet assigned.",
                EnName = "Scheduled Samples Not Assigned",
                EnDescription = "Work items in their first stage that have been scheduled but are not yet assigned.",
                ZhName = "任务未启动",
                ZhDescription = "任务项目已经添加进检验池如未被分配和领取，则在每日下午3点发送通知",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.WeeklyTestNotCompleted,
                Name = "Weekly Incomplete",
                Description = "The weekly test has not been completed.",
                EnName = "Weekly Incomplete",
                EnDescription = "The weekly test has not been completed.",
                ZhName = "周任务未完成",
                ZhDescription = "测试频率为每周的检验项目如果在周五还未完成，将会在下午3点发送通知",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.MonthlyTestNotCompleted,
                Name = "Monthly Incomplete",
                Description = "The monthly test has not been completed.",
                EnName = "Monthly Incomplete",
                EnDescription = "The monthly test has not been completed.",
                ZhName = "月任务未完成",
                ZhDescription = "测试频率为每月的检验项目如果在当月25号还未完成，将会在下午3点发送通知",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.QuarterlyTestNotCompleted,
                Name = "Quarterly Incomplete",
                Description = "The quarterly test has not been completed.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.MaxTimeAboutToExceed,
                Name = "Max Time About to Exceed",
                Description = "Work items with a configured max time for a stage that are due within the next hour.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.WorkNotYetCompletedForToday,
                Name = "Work Incomplete ",
                Description = "Work items that have been assigned and scheduled for today but have not yet been completed.",
                EnName = "Work Incomplete ",
                EnDescription = "Work items that have been assigned and scheduled for today but have not yet been completed.",
                ZhName = "当日任务未完成",
                ZhDescription = "计划检测日期为当天的任务，如已经启动但未完成的，在当天下午3点发送通知",
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.OrganismFound,
                Name = "Organism Found",
                Description = "Organism Found",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.OrganismAdded,
                Name = "Organism Added To Organism Dictionary",
                Description = "Organism Added To Organism Dictionary",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.WorkflowError,
                Name = "Workflow Error",
                Description = "An error occurred processing a sample",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.EquipmentAboutToExpire,
                Name = "Equipment About to Expire",
                Description = "Equipment that is about to go out of calibration.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.MediaInventoryLow,
                Name = "Media Inventory is Low",
                Description = "Media inventory is getting low.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.UserQualificationLapsed,
                Name = "User Qualification Lapsed",
                Description = "User Qualification has lapsed.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
            yield return new NotificationType()
            {
                Id = id++,
                Type = NotificationTypes.UserQualificationDue,
                Name = "User Qualification Coming Due",
                Description = "User Qualification is coming due.",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = false,
            };
        }
    }
}
