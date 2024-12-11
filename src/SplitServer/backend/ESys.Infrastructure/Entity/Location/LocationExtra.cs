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

    /// <summary>
    /// 区域附加属性
    /// </summary>
    [AuditDisable]
    public partial class LocationExtra : BizEntity<LocationExtra, int>
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// 面包屑导航，逗号拼接
        /// </summary>
        public string Breadcrumb { get; set; }
        /// <summary>
        /// 路径，->拼接
        /// </summary>
        public string LocationPath { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<LocationExtra> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(le => le.Location)
                .WithOne(l => l.LocationExtra)
                .HasForeignKey<LocationExtra>(le => le.LocationId);
        }
    }
}
