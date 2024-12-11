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

namespace ESys.Infrastructure.Entity
{
    using ESys.Contract.Entity;
    using System;

    /// <summary>
    /// 采样点视图
    /// </summary>
    public class SiteV : BizView<SiteV>
    {
        /// <summary>
        /// 采样点Id
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 采样点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 采样点描述
        /// </summary>
        public string SiteDesc { get; set; }
        /// <summary>
        /// 采样点类型Id
        /// </summary>
        public int SiteTypeId { get; set; }
        /// <summary>
        /// 采样点类型描述
        /// </summary>
        public string SiteTypeDesc { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 区域描述
        /// </summary>
        public string LocationDesc { get; set; }
        /// <summary>
        /// 区域Id面包屑导航
        /// </summary>
        public string LocationBreadcrumb { get; set; }
        /// <summary>
        /// 区域路径
        /// </summary>
        public string LocationFullName { get; set; }
        /// <summary>
        /// 级别Id
        /// </summary>
        public int? ClassificationId { get; set; }
        /// <summary>
        /// 级别描述（名称）
        /// </summary>
        public string ClassificationDesc { get; set; }
        /// <summary>
        /// 第二级别Id
        /// </summary>
        public int? SecondClassificationId { get; set; }
        /// <summary>
        /// 第二级别描述（名称）
        /// </summary>
        public string SecondClassificationDesc { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset UpdatedTime { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
