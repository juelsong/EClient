using EHost.Contract.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EHost.Db.Service
{
    /// <summary>
    /// 用于多租户
    /// </summary>
    public class Tenant : ITenant
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>

        public string ConnectString { get; private set; }
        // 可以添加更多的租户特定属性
        // public string Configuration { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectString"></param>
        public Tenant( string name ,string connectString)
        {
            Name = name;
            ConnectString = connectString;
            // Configuration = configuration;
        }
    }
}
