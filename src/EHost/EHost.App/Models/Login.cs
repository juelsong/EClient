namespace EHost.App.Models
{
    /// <summary>
    /// 登录请求模型
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 用户名，必填，长度不超过50字符
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码，必填，长度不超过50字符
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 登录响应模型
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 响应状态，可能的值为 "success" 或 "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 响应消息，描述操作的结果
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据对象，仅在 status 为 "success" 时存在
        /// </summary>
        public LoginData Data { get; set; }

        /// <summary>
        /// 错误码，仅在 status 为 "error" 时存在
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 登录数据
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// 用户登录后获得的令牌，用于后续请求的认证
        /// </summary>
        public string Token { get; set; }
    }
}
