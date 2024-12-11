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
    using System;

    /// <summary>
    /// 订阅视图
    /// </summary>
    [AuditDisable]

    public class SubscriptionV : BizView<SubscriptionV>
    {
        /// <summary>
        /// 订阅Id
        /// </summary>
        public long SubscriptionId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 用户所属部门Id
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 用户区域Id
        /// </summary>
        public int? UserLocationId { get; set; }
        /// <summary>
        /// 用户区域Id面包导航
        /// </summary>
        public string UserLocationBreadcrumb { get; set; }
        /// <summary>
        /// 用户工号
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 用户职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户电邮
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public UserStatus? UserStatus { get; set; }
        /// <summary>
        /// 通知类型Id
        /// </summary>
        public int? NotificationTypeId { get; set; }
        /// <summary>
        /// 通知类型名称
        /// </summary>
        public string NotificationTypeName { get; set; }
        /// <summary>
        /// 通知类型描述
        /// </summary>
        public string NotificationTypeDescription { get; set; }
        /// <summary>
        /// 通知类型是否启用
        /// </summary>
        public bool? IsNotificationTypeActive { get; set; }
        /// <summary>
        /// 属性Id
        /// </summary>
        public int? PlanGroupId { get; set; }
        /// <summary>
        /// 订阅区域Id
        /// </summary>
		public int? LocationId { get; set; }
        /// <summary>
        /// 订阅区域名称
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 订阅区域是否启用
        /// </summary>
        public bool? IsLocationActive { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
