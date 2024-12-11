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

    /// <summary>
    /// 可视化图
    /// </summary>
    public partial class VisioDiagram : BizEntity<VisioDiagram, int>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 地图Id
        /// </summary>
        public int MapId { get; set; }
        /// <summary>
        /// 地图
        /// </summary>
        public virtual Map Map { get; set; }
        /// <summary>
        /// 可视化区域
        /// </summary>
        public virtual ICollection<VisioLocation> VisioLocations { get; set; }
        /// <summary>
        /// 可视化采样点
        /// </summary>
        public virtual ICollection<VisioSite> VisioSites { get; set; }

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

        #endregion
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<VisioDiagram> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(v => v.Map)
                .WithMany(m => m.VisioDiagrams)
                .HasForeignKey(v => v.MapId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
