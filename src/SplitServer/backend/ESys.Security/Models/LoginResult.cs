using System.ComponentModel.DataAnnotations;

namespace ESys.Security.Models
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required, MinLength(3)]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 密码是否过期
        /// </summary>
        public bool PasswordExpiried { get; set; }
        /// <summary>
        /// 首次登录需要更改密码
        /// </summary>
        public bool ChangePassUponFirstLogin { get; set; }
    }
}
