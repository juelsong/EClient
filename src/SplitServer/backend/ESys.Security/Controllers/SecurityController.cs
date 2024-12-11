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

namespace ESys.Security.ApiControllers
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Security.Models;
    using ESys.Security.Utility;
    using ESys.Utilty.Attributes;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 安全控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("api/user/[action]")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfigService configService;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msRepository"></param>
        /// <param name="configService"></param>
        public SecurityController(
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository,
            IConfigService configService)
        {
            this.configService = configService;
            this.msRepository = msRepository;
        }
        /// <summary>
        /// 获取密码有效规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("password-rule")]
        [AllowAnonymous] // 未登录时可能调用
        public async Task<Result<PasswordRule[]>> GetPasswordRule([FromQuery] string locale)
        {
            var tenantId = this.HttpContext.GetTenantId();
            var config = await this.configService.GetSecurityConfig(tenantId);
            var rules = config.PassValidators
                .Select(v => v.Validator.GetPasswordRule(locale))
                .Where(r => r != null)
                .ToArray();
            return ResultBuilder.Ok(rules);
        }

        // TODO 权限
        /// <summary>
        /// 配置安全规则
        /// </summary>
        /// <param name="securityModel"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("security-config")]
        [RequiresPermission("Security:Password")]
        public async Task<Result<bool>> PatchSecurityConfig(SecurityModel securityModel)
        {
            var tenantId = this.HttpContext.GetTenantId();
            var oriConfig = await this.configService.GetSecurityConfig(tenantId);
            var config = securityModel.GetSecurityConfig();
            if (oriConfig.ExpiryPeriod != config.ExpiryPeriod)
            {
                var repo = this.msRepository.Master<Security.Entity.User>();
                // TODO 批量更新
                var users = repo
                    .Where(u => u.Id != ConstDefs.SystemUserId && u.Id != ConstDefs.SuperUserId)
                    .ToArray();
                var propertyNames = new[] { nameof(Security.Entity.User.PasswordExpiryPeriod) };
                if (config.ExpiryPeriod.HasValue)
                {
                    foreach (var user in users)
                    {
                        if (user.LastPasswordModified.HasValue)
                        {
                            // 密码过期从最后一次修改密码算起
                            user.PasswordExpiryPeriod = user.LastPasswordModified + config.ExpiryPeriod;
                            await repo.UpdateIncludeAsync(user, propertyNames);
                        }
                    }
                }
                else
                {
                    foreach (var user in users)
                    {
                        user.PasswordExpiryPeriod = null;
                        await repo.UpdateIncludeAsync(user, propertyNames);
                    }
                }
                await repo.SaveNowAsync();
            }

            await this.configService.SetConfig(tenantId, ConstDefs.SystemConfigKey.SecurityConfig, config);
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// 获取安全规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("security-config")]
        [AllowAnonymous] // 未登录时可能调用
        public async Task<Result<SecurityModel>> GetSecurityConfig()
        {
            var tenantId = this.HttpContext.GetTenantId();
            var config = await this.configService.GetSecurityConfig(tenantId);
            var model = SecurityModel.GetSecurityModel(config);
            return ResultBuilder.Ok(model);
        }
    }
}
