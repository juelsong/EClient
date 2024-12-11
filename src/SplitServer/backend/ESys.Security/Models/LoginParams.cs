using System.ComponentModel.DataAnnotations;

namespace ESys.Security.Models
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginParams
    {  /// <summary>
       /// 用户名
       /// </summary>
       /// <example>admin</example>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <example>admin</example>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        /// <example>admin</example>
        // [Required]
        public string Captcha { get; set; }

        /// <summary>
        /// 验证码key
        /// </summary>
        /// <example>admin</example>
        // [Required]
        public string CheckKey { get; set; }
    }
}
