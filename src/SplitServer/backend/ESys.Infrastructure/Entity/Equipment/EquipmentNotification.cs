namespace ESys.Infrastructure.Entity
{
    using ESys.Contract.Entity;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESys.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 用于数据库 单项处理 邮件或者短信发送
    /// </summary>
    [AuditDisable]
    public partial class EquipmentNotification : BizEntity<EquipmentNotification, int>
    {
        /// <summary>
        /// 主键
        /// </summary>        
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        /// <summary>
        /// 电话号
        /// </summary>
        [Column("phone_address")]
        public string Phone { get; set; }
        /// <summary>
        /// 电话号
        /// </summary>
        [Column("alert_mode")]
        public bool IsAlert { get; set; }
        /// <summary>
        /// 设备主键
        /// </summary>
        [Column("device_id")]
        public int EquipmentId { get; set; }
        /// <summary>
        /// 关联设备
        /// </summary>
        public virtual Equipment Equipment { get; set; }

        /// <summary>
        /// 配置
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// </summary>

        public override void Configure(EntityTypeBuilder<EquipmentNotification> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            // 设置唯一约束
            entityBuilder.HasIndex(p => new { p.EquipmentId, p.Phone })
                .IsUnique(); // 设置为唯一约束

            entityBuilder.HasOne(p => p.Equipment)
                .WithMany(e=>e.EquipmentNotification)
                .HasForeignKey(i => i.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasIndex(p => p.EquipmentId);
        }

    }
}
