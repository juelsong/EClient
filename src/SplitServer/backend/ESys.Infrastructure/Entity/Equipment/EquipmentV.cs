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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 产品视图
    /// </summary>
    public class EquipmentV : BizView<EquipmentV>
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int EquipmentId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// 区域Id面包屑导航
        /// </summary>
        public string LocationBreadcrumb { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 设备类型Id
        /// </summary>
        public int EquipmentTypeId { get; set; }
        /// <summary>
        /// 设备类型描述
        /// </summary>
        public string EquipmentTypeDescription { get; set; }
        /// <summary>
        /// 校准日期
        /// </summary>
        public DateTimeOffset? CalibrationDate { get; set; }
        /// <summary>
        /// 校准数值
        /// </summary>
        public decimal? CalibrationValue { get; set; }
        /// <summary>
        /// 下次校准日期
        /// </summary>
        public DateTimeOffset? NextCalibrationDate { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UOM { get; set; }
        /// <summary>
        /// 设备条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        /// 控制编号
        /// </summary>
        public string ControlNumber { get; set; }
        /// <summary>
        /// 设备校准用户名
        /// </summary>
        public string PerformedBy { get; set; }
        /// <summary>
        /// 设备校准人
        /// </summary>
        public int? PerformedByUserId { get; set; }
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

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<EquipmentV> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .Property(p => p.CalibrationValue)
                .HasPrecision(20, 6);
        }
    }
}
