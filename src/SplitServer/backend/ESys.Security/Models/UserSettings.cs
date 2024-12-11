namespace ESys.Security.Models
{
    /// <summary>
    /// 用户个性化配置
    /// </summary>
    public class UserSettings
    {
        /// <summary>
        /// 时间日期格式化字符串
        /// </summary>
        public string DateTimeFormat { get; set; }
        /// <summary>
        /// 日期格式化字符串
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// 获取默认配置
        /// </summary>
        /// <returns></returns>
        public static UserSettings GetDefaultSettings()
        {
            return new UserSettings()
            {
                DateTimeFormat = "YYYY-MM-DD HH:mm:ss",
                DateFormat = "YYYY-MM-DD"
            };
        }
    }
}
