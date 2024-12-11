using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Db
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// 
        /// </summary>
        Postgre,
        /// <summary>
        /// 
        /// </summary>
        SqlServer
    }
    /// <summary>
    /// 租户配置
    /// </summary>
    public class TenantConfiguration
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public DbType Type { get; set; }
        /// <summary>
        /// 链接字符
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
