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
    /// 地图
    /// </summary>
    public partial class Map : BizEntity<Map, int>, ITraceableEntity, ITimedEntity, IActiveEntity
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
        /// 路径
        /// </summary>
        [StringLength(255)]
        public string Path { get; set; }

        /// <summary>
        /// 地图分类Id
        /// </summary>
		public int? MapCategoryId { get; set; }

        /// <summary>
        /// 地图分类
        /// </summary>
        public virtual MapCategory MapCategory { get; set; }

        /// <summary>
        /// 地图相关的可视化图
        /// </summary>
        public virtual ICollection<VisioDiagram> VisioDiagrams { get; set; }

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
        public override void Configure(EntityTypeBuilder<Map> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(m => m.MapCategory)
                .WithMany(mc => mc.Maps)
                .HasForeignKey(m => m.MapCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
