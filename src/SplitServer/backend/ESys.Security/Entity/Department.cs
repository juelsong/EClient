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

namespace ESys.Security.Entity
{
    using ESys.DataAnnotations;
    using ESys.Contract.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 部门实体
    /// </summary>
    [DeactivateCheck(nameof(Department.Employees), nameof(User.RealName))]
    [DeactivateCheck(nameof(Department.Children), nameof(Department.Name))]
    public partial class Department : BizEntity<Department, int>, ITimedEntity, ITraceableEntity, IActiveEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 经理Id
        /// </summary>
        public int? ManagerId { get; set; }
        /// <summary>
        /// 经理
        /// </summary>
        public User Manager { get; set; }
        /// <summary>
        /// 父部门Id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父部门
        /// </summary>
        public Department Parent { get; set; }

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
        /// 部门员工
        /// </summary>
        public virtual ICollection<User> Employees { get; set; }
        /// <summary>
        /// 子部门
        /// </summary>
        public virtual ICollection<Department> Children { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Department> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(d => d.Manager).WithMany().HasForeignKey(d => d.ManagerId);
            entityBuilder.HasOne(d => d.Parent).WithMany(d => d.Children).HasForeignKey(d => d.ParentId);
            //entityBuilder.Property(d => d.IsActive).HasDefaultValue(true);
            entityBuilder.HasIndex(d => d.Name);
        }
    }
}
