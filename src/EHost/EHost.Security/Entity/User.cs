using CN.Metaura.EMIS.Contract.Entity;
using EHost.Contract.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml;

namespace EHost.Security.Entity
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        Frozen
    }
    public class User : EntityBase<int>
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
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }
    }
    /// <summary>
    /// 配置
    /// </summary>
    public class UserConfiguration : EntityTypeConfiguration<User, int>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder); // 调用基类的Configure方法

            // 添加额外的配置
            builder.HasIndex(i => i.Account);
        }
    }
}
