//namespace ESys.Infrastructure.Entity
//{
//    using ESys.Contract.Entity;
//    using Microsoft.EntityFrameworkCore.Metadata.Builders;
//    using Microsoft.EntityFrameworkCore;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;
//    using ESys.DataAnnotations;

//    /// <summary>
//    /// MN 设备 一对一控制
//    /// </summary>
//    [AuditDisable]
//    public partial class EquipmentMN : BizEntity<EquipmentMN, int>, ITraceableEntity, ITimedEntity, IActiveEntity
//    {
//        /// <summary>
//        /// MN码
//        /// </summary>
//        public string MNCode { get; set; }
//        /// <summary>
//        /// 对应设备Id
//        /// </summary>
//        public int EquipmentId { get; set; }
//        /// <summary>
//        /// 对应激活设备
//        /// </summary>
//        public virtual Equipment Equipment { get; set; }

//        #region interfaces
//        /// <summary>
//        /// 创建时间
//        /// </summary>
//        public DateTimeOffset CreatedTime { get; set; }
//        /// <summary>
//        /// 更新时间
//        /// </summary>
//        public DateTimeOffset? UpdatedTime { get; set; }
//        /// <summary>
//        /// 创建人
//        /// </summary>
//        public int CreateBy { get; set; }
//        /// <summary>
//        /// 更新人
//        /// </summary>
//        public int? UpdateBy { get; set; }

//        /// <summary>
//        /// 是否启用
//        /// </summary>
//        public bool IsActive { get; set; }
//        #endregion interfaces

//        /// 配置
//        /// </summary>
//        /// <param name="entityBuilder"></param>
//        /// <param name="dbContext"></param>
//        /// <param name="dbContextLocator"></param>
//        public override void Configure(EntityTypeBuilder<EquipmentMN> entityBuilder, DbContext dbContext, Type dbContextLocator)
//        {
//            entityBuilder.HasOne(p => p.Equipment)
//                .WithMany(e => e.EquipmentMNs)
//                .HasForeignKey(i => i.EquipmentId)
//                .OnDelete(DeleteBehavior.NoAction);

//            entityBuilder.HasIndex(p => p.MNCode);
//        }
//    }
//}
