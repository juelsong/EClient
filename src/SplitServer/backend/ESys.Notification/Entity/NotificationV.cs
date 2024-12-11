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
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 通知视图
    /// </summary>
    public class NotificationV : BizView<NotificationV>
    {
        /// <summary>
        /// 通知Id
        /// </summary>
        public long NotificationId { get; set; }
        /// <summary>
        /// 通知类型Id
        /// </summary>
		public int NotificationTypeId { get; set; }
        /// <summary>
        /// 通知类型名称
        /// </summary>
        public string NotificationTypeName { get; set; }
        /// <summary>
        /// 通知类型描述
        /// </summary>
        public string NotificationTypeDesc { get; set; }
        /// <summary>
        /// 执行通知的时间戳
        /// </summary>
        public DateTimeOffset? NotificationDate { get; set; }
        /// <summary>
        /// 创建此通知的用户Id
        /// </summary>
		public int? UserId { get; set; }
        /// <summary>
        /// 创建此通知的用户区域Id
        /// </summary>
        public int? UserLocationId { get; set; }
        /// <summary>
        /// 创建此通知的用户区域Id导航
        /// </summary>
        public string UserLocationBreadcrumb { get; set; }
        /// <summary>
        /// 采样点Id
        /// </summary>
		public int? SiteId { get; set; }
        /// <summary>
        /// 采样点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 采样点描述
        /// </summary>
        public string SiteDesc { get; set; }
        /// <summary>
        /// 采样点区域Id
        /// </summary>
        public int? SiteLocationId { get; set; }
        /// <summary>
        /// 采样点区域Id导航
        /// </summary>
        public string SiteLocationBreadcrumb { get; set; }
        /// <summary>
        /// 测试样本Id
        /// </summary>
		public long? SampleId { get; set; }

        /// <summary>
        /// 偏差Id
        /// </summary>
		public int? DeviationId { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
		public int? EquipmentId { get; set; }
        /// <summary>
        /// 设备区域Id
        /// </summary>
        public int? EquipmentLocationId { get; set; }
        /// <summary>
        /// 设备区域Id导航
        /// </summary>
        public string EquipmentLocationBreadcrumb { get; set; }
        /// <summary>
        /// 培养基Id
        /// </summary>
		public int? MediaId { get; set; }

        /// <summary>
        /// 培养基区域Id
        /// </summary>
		public int? MediaLocationId { get; set; }
        /// <summary>
        /// 培养基区域Id导航
        /// </summary>
        public string MediaLocationBreadcrumb { get; set; }
        /// <summary>
        /// 通知内容，可自行设置，也可通过<see cref="Messages"/>设置为json数组
        /// </summary>
        public string Content { get; set; }
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
        /// 序列化的内容，已json数组格式设置到<see cref="Content"/>属性中
        /// </summary>
        [NotMapped]
        public string[] Messages
        {
            get
            {
                return Notification.DeserializeMessages(this.Content);
            }
            set
            {
                this.Content = Notification.Serialize(value);
            }
        }
    }
}
