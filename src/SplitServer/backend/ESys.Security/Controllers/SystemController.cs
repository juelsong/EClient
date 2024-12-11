/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Security.Controllers
{
    using ESys.Contract.Service;
    using ESys.Security.Service;
    using ESys.Utilty.Defs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    using System.Threading.Tasks;

    /// <summary>
    /// 系统控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("[controller]/[action]")]
#if DEBUG
    [AllowAnonymous]
#endif    
    public class SystemController : Controller
    {
        private readonly IConfigService configService;
        private readonly ITenantService tenantService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantService"></param>
        /// <param name="configService"></param>
        public SystemController(
            ITenantService tenantService,
            IConfigService configService)
        {
            this.tenantService = tenantService;
            this.configService = configService;
        }
        /// <summary>
        /// LDAP管理用户
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<bool>> UserManagementLdap(LDAPConfig config)
        {
            var tenant = this.tenantService.GetCurrentTenant();
            await this.configService.SetConfig(tenant.Code, ConstDefs.SystemConfigKey.LdapConfig, config);
            await this.configService.SetConfig(tenant.Code, ConstDefs.SystemConfigKey.UserManagementModelConfig, UserManagementMode.LDAP);
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// ESys管理用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<bool>> UserManagementEmis()
        {
            var tenant = this.tenantService.GetCurrentTenant();
            await this.configService.SetConfig(tenant.Code, ConstDefs.SystemConfigKey.UserManagementModelConfig, UserManagementMode.ESys);
            return ResultBuilder.Ok(true);
        }
    }
}
