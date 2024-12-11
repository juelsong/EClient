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

namespace ESys.System.Controllers
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.System.Entity;
    using ESys.Utilty.Defs;
    using Furion.Authorization;
    using Furion.ClayObject.Extensions;
    using Furion.DatabaseAccessor;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// 系统控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class SystemController : Controller
    {
        private readonly IConfigService configService;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private readonly IOptions<JWTSettingsOptions> jwtOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="msRepository"></param>
        /// <param name="configService"></param>
        public SystemController(
            IOptions<JWTSettingsOptions> jwtOptions,
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository,
            IConfigService configService)
        {
            this.msRepository = msRepository;
            this.configService = configService;
            this.jwtOptions = jwtOptions;
        }

        /// <summary>
        /// 设置默认首页组件
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("DefaultDashboard")]
        public async Task<Result<bool>> PatchDefaultDashboard([FromBody] string[] components)
        {
            var tenantId = this.HttpContext.GetTenantId();
            await this.configService.SetConfig(tenantId, ConstDefs.SystemConfigKey.DashboardDefault, components);
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// 获取默认首页组件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("DefaultDashboard")]
        public async Task<Result<string[]>> GetDefaultDashboard()
        {
            var tenantId = this.HttpContext.GetTenantId();
            var layout = await this.configService.GetConfig<string[]>(tenantId, ConstDefs.SystemConfigKey.DashboardDefault);
            return ResultBuilder.Ok(layout);
        }

        /// <summary>
        /// 设置默认首页布局
        /// </summary>
        /// <param name="layout"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("DefaultLayout")]
        public async Task<Result<bool>> PatchDefaultLayout([FromBody] DashboardLayout layout)
        {
            var tenantId = this.HttpContext.GetTenantId();
            await this.configService.SetConfig(tenantId, ConstDefs.SystemConfigKey.DashboardLayout, layout);
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// 获取默认首页布局
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("DefaultLayout")]
        public async Task<Result<DashboardLayout>> GetDefaultLayout()
        {
            var tenantId = this.HttpContext.GetTenantId();
            var layout = await this.configService.GetConfig<DashboardLayout>(tenantId, ConstDefs.SystemConfigKey.DashboardLayout);
            return ResultBuilder.Ok(layout);
        }

        /// <summary>
        /// 设置首页组件选项
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("Components")]
        public async Task<Result<bool>> PatchComponents([FromBody] string[] components)
        {
            var tenantId = this.HttpContext.GetTenantId();
            await this.configService.SetConfig(tenantId, ConstDefs.SystemConfigKey.DashboardComponents, components);
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// 获取首页组件选项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("Components")]
        public async Task<Result<string[]>> GetComponents()
        {
            var tenantId = this.HttpContext.GetTenantId();
            var components = await this.configService.GetConfig<string[]>(tenantId, ConstDefs.SystemConfigKey.DashboardComponents);
            components ??= Array.Empty<string>();
            return ResultBuilder.Ok(components);
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("SystemConfig")]
        public async Task<Result<Dictionary<string, string>>> GetSystemConfig()
        {
            var configItems = await this.msRepository.Slave1<ConfigItem>()
                                                     .AsQueryable(false)
                                                     .ToArrayAsync();
            var ret = configItems.ToDictionary(item => item.Property, item => item.Value);
            ret["ClockSkew"] = $"{this.jwtOptions.Value.ClockSkew ?? 0}";
            return ResultBuilder.Ok(ret);
        }
    }
}
