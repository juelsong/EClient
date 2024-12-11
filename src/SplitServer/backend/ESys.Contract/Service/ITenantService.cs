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

using ESys.Contract.Defs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESys.Contract.Service
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant
    {
        /// <summary>
        /// 租户代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 租户到期时间
        /// </summary>
        public DateTimeOffset Limits { get; set; }
        /// <summary>
        /// 主库连接字符串
        /// </summary>
        public string MasterDbConnStr { get; set; }
        /// <summary>
        /// 从库连接字符串
        /// </summary>
        public string SlaveDbConnStr { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType { get; set; }
    }

    /// <summary>
    /// 租户服务
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// 获取租户信息
        /// </summary>
        /// <returns></returns>
        Tenant GetCurrentTenant();
        /// <summary>
        /// 获取所有租户
        /// </summary>
        /// <param name="validOnly">只有有效的租户</param>
        /// <param name="action"></param>
        /// <returns>租户列举</returns>
        Task ExecuteInTenantScope(Func<IServiceScope, Task> action, bool validOnly = true);
        /// <summary>
        /// 设置租户作用域
        /// </summary>
        /// <param name="tenantId">租户编码</param>
        void SetTenantScope(string tenantId);

        /// <summary>
        /// 获取租户根IServiceProvider
        /// </summary>
        /// <returns></returns>
        IServiceProvider GetTenantServiceProvider();
    }
}
