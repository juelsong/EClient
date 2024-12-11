namespace ESys.Security.Models
{
    using global::System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 修改自己密码
    /// </summary>
    public class ChangePassSelf
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required]
        public string OriPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
