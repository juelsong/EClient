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
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 产品实体
    /// </summary>
    [AuditDisable]

    public partial class Equipment : BizEntity<Equipment, int>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 描述 
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 序列号 用于编辑显示的Id
        /// </summary>
        public string SerialNumber { get; set; }
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
        /// 设备条码
        /// </summary>
        [StringLength(50)]
        public string Barcode { get; set; }

        /// <summary>
        /// 控制编号 用于 粉尘仪编号
        /// </summary>
        [StringLength(15)]
        public string ControlNumber { get; set; }
        /// <summary>
        /// 是否运维打卡
        /// </summary>
        public bool IsOperation { get; set; }
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
        /// 设备类型Id
        /// </summary>
        public int? EquipmentTypeId { get; set; }

        /// <summary>
        ///设备类型 粉尘仪型号
        /// </summary>
        public virtual EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// 设备配置Id
        /// </summary>
        public int? EquipmentConfigId { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        public virtual EquipmentConfig EquipmentConfig { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        public virtual ICollection<EquipmentTPM> EquipmentTPMs { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Equipment> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .Property(p => p.CalibrationValue)
                .HasPrecision(20, 6);

            entityBuilder.HasOne(p => p.EquipmentType)
                .WithMany()
                .HasForeignKey(p => p.EquipmentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityBuilder.HasOne(p => p.Location)
                .WithMany()
                .HasForeignKey(p => p.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entityBuilder.HasOne(p => p.EquipmentConfig)
                .WithOne()
                .HasForeignKey<Equipment>(i => i.EquipmentConfigId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasIndex(p => p.Name);
            entityBuilder.HasIndex(p => p.SerialNumber);
        }
    }
}
