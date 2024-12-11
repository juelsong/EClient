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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 设备类别实体
    /// TODO fhn 初始化dll路径到相关设备类型
    /// </summary>
    [AuditDisable]
    public partial class EquipmentType : BizEntity<EquipmentType, int>, ITraceableEntity, ITimedEntity,  IActiveEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [StringLength(50)]
        public string Type { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        [StringLength(50)]
        public string Model { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 加载方式
        /// </summary>
        [StringLength(50)]
        public string LoadingMode { get; set; }
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
        public override void Configure(EntityTypeBuilder<EquipmentType> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(e => e.Description);
        }

        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<EquipmentType> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            var id = 1;
            yield return new EquipmentType()
            {
                Id = id++,
                Description = "No equipment type",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new EquipmentType()
            {
                Id = id++,
                Description = "Incubator",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new EquipmentType()
            {
                Id = id++,
                Description = "Active Air Sampler",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
            yield return new EquipmentType()
            {
                Id = id++,
                Description = "Particulate Counter",

                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
        }
    }
}
