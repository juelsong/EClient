namespace ESys.Contract.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 日志定义
    /// </summary>
    public class LogInfo
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 描述内容
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 关联用户
        /// </summary>
        public int UserId { get; set; }
    }
    /// <summary>
    /// 日志记录信息
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logInfo"></param>
        bool LogData(LogInfo logInfo );

    }
}
