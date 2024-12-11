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
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 采样点实体
    /// </summary>
    public partial class Site : BizEntity<Site, int>, ITraceableEntity, ITimedEntity, IActiveEntity
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
        /// 区域Id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public Location Location { get; set; }


        /// <summary>
        /// 采样点类型Id
        /// </summary>
        public int SiteTypeId { get; set; }
        /// <summary>
        /// 采样点类型
        /// </summary>
        public SiteType SiteType { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>

        public override void Configure(EntityTypeBuilder<Site> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(l => l.Location)
                  .WithMany(l => l.Sites)
                  .HasForeignKey(l => l.LocationId)
                  .OnDelete(DeleteBehavior.ClientSetNull);


            entityBuilder.HasOne(l => l.SiteType)
                .WithMany(st => st.Sites)
                .HasForeignKey(l => l.SiteTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entityBuilder.HasIndex(s => new { s.IsActive, s.LocationId, s.Id });
            entityBuilder.HasIndex(s => s.LocationId);
            entityBuilder.HasIndex(s => s.SiteTypeId);
            entityBuilder.HasIndex(s => new { s.LocationId, s.Id, s.Name });

        }
    }
}
