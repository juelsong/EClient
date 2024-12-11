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
    /// 可视化采样点
    /// </summary>
    public partial class VisioSite : BizEntity<VisioSite, int>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 采样点Id
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 采样点
        /// </summary>
        public virtual Site Site { get; set; }

        /// <summary>
        /// 地图Id
        /// </summary>
        public int VisioDiagramId { get; set; }
        /// <summary>
        /// 地图
        /// </summary>
        public virtual VisioDiagram VisioDiagram { get; set; }
        /// <summary>
        /// 地图位置X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// 地图位置Y
        /// </summary>
        public double Y { get; set; }

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
        public override void Configure(EntityTypeBuilder<VisioSite> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(v => v.Site)
                .WithMany()
                .HasForeignKey(v => v.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            entityBuilder.HasOne(v => v.VisioDiagram)
                .WithMany(d => d.VisioSites)
                .HasForeignKey(v => v.VisioDiagramId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
