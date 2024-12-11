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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 区域实体
    /// </summary>
    public partial class Location : BizEntity<Location, int>, ITraceableEntity, ITimedEntity, IActiveEntity
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
        /// 条码
        /// </summary>
        [StringLength(64)]
        public string Barcode { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(64)]
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
        /// 父对象
        /// </summary>
        public Location Parent { get; set; }

        /// <summary>
        /// 区域类型Id
        /// </summary>
        public int LocationTypeId { get; set; }
        /// <summary>
        /// 区域类型
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// 可视化图Id
        /// </summary>
        public int? VisioDiagramId { get; set; }
        /// <summary>
        /// 可视化图
        /// </summary>
        public virtual VisioDiagram VisioDiagram { get; set; }

        /// <summary>
        /// 区域扩展信息
        /// </summary>
        public virtual LocationExtra LocationExtra { get; set; }
        /// <summary>
        /// 采样点集合
        /// </summary>
        public virtual ICollection<Site> Sites { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Location> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(lt => lt.Name);
            entityBuilder.HasIndex(lt => new { lt.Id, lt.ParentId });

            entityBuilder.HasOne(l => l.LocationType)
                .WithMany(lt => lt.Locations)
                .HasForeignKey(l => l.LocationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entityBuilder.HasOne(l => l.Parent)
                .WithMany()
                .HasForeignKey(l => l.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasOne(l => l.VisioDiagram)
                .WithMany()
                .HasForeignKey(l => l.VisioDiagramId)
                .OnDelete(DeleteBehavior.SetNull);
            if (dbContext.Database.IsMySql())
            {
                entityBuilder.Property(lt => lt.Name)
                    .HasCharSet("gbk")
                    .UseCollation("gbk_chinese_ci");
            }
        }
    }
}
