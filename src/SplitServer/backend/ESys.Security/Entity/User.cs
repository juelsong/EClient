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
    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using ESys.Infrastructure.Entity;
    using ESys.Utilty.Defs;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// 用户状态
    /// </summary>
    [ODataConfig]
    public enum UserStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [EnumMember]
        Normal,
        /// <summary>
        /// 冻结
        /// </summary>
        [EnumMember]
        Frozen
    }
    /// <summary>
    /// 性别
    /// </summary>
    [ODataConfig]
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        Female
    }

    /// <summary>
    /// 用户实体
    /// </summary>
    [ODataExposed(allowCreate: false)]
    public partial class User : BizEntity<User, int>, ITimedEntity, ITraceableEntity, IHiddenEntity, IActiveEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [StringLength(32)]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// md5密码盐
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        [StringLength(64)]
        public string EmployeeId { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(64)]
        public string Title { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(64)]
        public string EMail { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }
        /// <summary>
        /// 上次认证日期
        /// </summary>
        public DateTimeOffset? LastMonitoredDate { get; set; }
        /// <summary>
        /// 初始认证日期
        /// </summary>
        public DateTimeOffset? InitialQualificationDate { get; set; }
        /// <summary>
        /// 下次认证日期
        /// </summary>
        public DateTimeOffset? NextQualificationDate { get; set; }
        /// <summary>
        /// 密码有效期
        /// </summary>
        public DateTimeOffset? PasswordExpiryPeriod { get; set; }
        /// <summary>
        /// 最后一次密码更改时间戳
        /// </summary>
        public DateTimeOffset? LastPasswordModified { get; set; }

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
        /// 所属部门Id
        /// </summary>
        public int? DepartmentId { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public virtual Location Location { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        /// <summary>
        /// 角色映射
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户配置
        /// </summary>
        public virtual UserProfile Profile { get; set; }
        /// <summary>
        /// 用户密码记录
        /// </summary>
        public virtual ICollection<UserPasswordHistory> UserPasswordHistories { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(u => u.Account).IsUnique();
            entityBuilder.Property(u => u.Password).IsRequired();
            entityBuilder.Property(u => u.Status)
                .HasDefaultValue(UserStatus.Normal);

            //entityBuilder.Property(u => u.IsActive)
            //    .HasDefaultValue(true);

            //entityBuilder.Property(u => u.CreatedTime)
            //    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entityBuilder.HasMany(u => u.Roles)
                .WithMany(p => p.Users)
                .UsingEntity<UserRole>(
                    rp => rp.HasOne(rpi => rpi.Role).WithMany(p => p.UserRoles).HasForeignKey(rp => rp.RoleId),
                    rp => rp.HasOne(rpi => rpi.User).WithMany(r => r.UserRoles).HasForeignKey(rp => rp.UserId)
                 );

            entityBuilder.HasOne(u => u.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(u => u.DepartmentId);

            entityBuilder.HasOne(u => u.Location)
                .WithMany()
                .HasForeignKey(u => u.LocationId);
        }
        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<User> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            // 超级管理员
            yield return new User()
            {
                Id = 1,
                Account = "super",
                Password = "l8YyUDcfKuGGgLaMKT4E6mg/8ClZOK8tczolqvakrA8=",//sr160608
                Salt = "f5ZTUveTZ6szf7wR3qCmvg==",
                IsHidden = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
                CreateBy = 1,
            };
            // 内置服务使用  不要更改Id，后台服务使用此用户作为操作者
            yield return new User()
            {
                Id = ConstDefs.SystemUserId,
                Account = "ESys_Admin",
                Password = "gIbsDs1b73xjUyptJmm1RjjX8HiJ3ubnt1F/mS6mzio=",//ESys_Admin_Inner
                Salt = "kCXBD/crXrivLJwcfWEoHQ==",
                IsHidden = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
                CreateBy = 1
            };
            // 供客户管理员使用
            yield return new User()
            {
                Id = 3,
                Account = "Admin",
                Password = "QnVVjchwQROIbFPB7PrnYD3htnV5F4AJrfWnsej4JQk=",//ESys_Admin
                Salt = "tJ48HTFvFBHIWRnO8pxLpQ==",
                RealName = "Admin",
                IsHidden = false,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
                IsActive = true,
                CreateBy = 1
            };
        }
    }
}
