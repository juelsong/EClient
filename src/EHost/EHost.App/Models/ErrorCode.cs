namespace EHost.App.Models
{
    /// <summary>
    /// 错误码枚举
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 无错误
        /// </summary>
        None = 1000,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        InvalidCredentials = 1001,

        /// <summary>
        /// 服务器内部错误
        /// </summary>
        InternalServerError = 1002,

        /// <summary>
        /// 用户名不存在
        /// </summary>
        UsernameNotFound = 1003,

        /// <summary>
        /// 密码错误
        /// </summary>
        IncorrectPassword = 1004,

        /// <summary>
        /// 用户被锁定
        /// </summary>
        UserLocked = 1005
    }

}
