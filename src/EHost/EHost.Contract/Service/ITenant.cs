using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Contract.Service
{
    /// <summary>
    ///处理多租户  管理数据库配置连接
    /// </summary>
    public interface ITenant
    {
        // 租户的名称，可能用于显示
        public string Name { get; }
        /// <summary>
        /// 连接字符 
        /// </summary>
        public string ConnectString { get; }
    }
}
