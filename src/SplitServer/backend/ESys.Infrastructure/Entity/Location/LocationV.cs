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
    /// 区域视图
    /// </summary>
    public class LocationV : BizView<LocationV>
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

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
        /// 父对象Id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 区域类型Id
        /// </summary>
        public int LocationTypeId { get; set; }
        /// <summary>
        /// 级别Id
        /// </summary>
        public int? ClassificationId { get; set; }
        /// <summary>
        /// 第二级别Id
        /// </summary>
        public int? SecondClassificationId { get; set; }

        /// <summary>
        /// 可视化图Id
        /// </summary>
        public int? VisioDiagramId { get; set; }

        /// <summary>
        /// 面包屑导航，逗号拼接
        /// </summary>
        public string Breadcrumb { get; set; }
        /// <summary>
        /// 路径，->拼接
        /// </summary>
        public string LocationPath { get; set; }

    }
}
