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

using ESys.DataAnnotations;
using ESys.Contract.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ESys.Security.Entity
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [DeactivateCheck(nameof(Role.Users), nameof(User.Account))]
    public partial class Role : BizEntity<Role, int>, ITimedEntity, ITraceableEntity, IHiddenEntity, IActiveEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #region interfaces
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }
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
        /// 权限
        /// </summary>
        public virtual ICollection<Permission> Permissions { get; set; }

        /// <summary>
        /// 权限映射
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
        /// <summary>
        /// 用户映射
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(r => r.Name).IsUnique();
            entityBuilder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<RolePermission>(
                    rp => rp.HasOne(rpi => rpi.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId),
                    rp => rp.HasOne(rpi => rpi.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId));
        }

        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<Role> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            yield return new Role()
            {
                Id = 1,
                Name = "超级管理员",
                IsHidden = true,
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };

            yield return new Role()
            {
                Id = 2,
                Name = "管理员",
                IsHidden = false,
                CreateBy = 1,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
            };
        }
    }
}
