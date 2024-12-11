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

namespace ESys.Notification.Service
{
    /// <summary>
    /// email配置
    /// </summary>
    public class EMailConfig
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// 发送者地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 检查周期（目前租户配置无效）
        /// </summary>
        public int IntervalSeconds { get; set; }
        /// <summary>
        /// 是否启用SSL
        /// </summary>
        public bool EnableSSL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Domain { get; set; }
    }

    internal class NotificationConfig
    {
        public bool LocationUserSecurity { get; set; }
        public bool GroupByNotificationType { get; set; }
        public int IntervalSeconds { get; set; }
    }
}
