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

namespace ESys.Security.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using Furion.DatabaseAccessor;
    using Furion.DependencyInjection;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ESignConfig = ESys.Security.Entity.ESignConfig;
    using ESignConfigModel = ESys.Contract.Service.ESignConfig;
    /// <summary>
    /// 电子签名配置服务
    /// </summary>
    public class ESignConfigService : IESignConfigService, ITransient
    {
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ESignConfigService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获取给特定业务签名所需的权限
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<ESignConfigModel> GetPermissions(string tenant, string category)
        {
            var cache = this.serviceProvider.GetService<IMemoryCache>();
            var tenantSerivce = this.serviceProvider.GetService<ITenantService>();
            tenantSerivce.SetTenantScope(tenant);
            var repo = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var ret = await cache.GetOrCreateAsync(
                FormatCacheKey(category, tenant),
                (entry) =>
                {
                    var config = repo.Slave1<ESignConfig>().FirstOrDefault(c => c.Category == category && c.IsActive);
                    var ret = string.IsNullOrEmpty(config?.Permissions)
                        ? Array.Empty<string>()
                        : config.Permissions.Split(',');
                    return Task.FromResult(new ESignConfigModel(config == null ? 0 : config.SignCount, ret));
                });
            return ret;
        }
        /// <summary>
        /// 设置给特定业务签名所需的权限
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="category"></param>
        /// <param name="esignCount"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public async Task SetPermissions(string tenant, string category, int esignCount, params string[] permissions)
        {
            var cache = this.serviceProvider.GetService<IMemoryCache>();
            var tenantSerivce = this.serviceProvider.GetService<ITenantService>();
            tenantSerivce.SetTenantScope(tenant);
            var repo = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var config = repo.Slave1<ESignConfig>().FirstOrDefault(c => c.Category == category);
            var valStr = permissions == null ? string.Empty : string.Join(',', permissions);
            if (config == null)
            {
                config = new ESignConfig()
                {
                    Category = category,
                    SignCount = esignCount,
                    Permissions = valStr,
                    IsActive = true
                };
                await repo.Master<ESignConfig>().InsertNowAsync(config);
            }
            else
            {
                config.Permissions = valStr;
                config.IsActive = true;
                await repo.Master<ESignConfig>().UpdateIncludeNowAsync(
                    config,
                    new[] { nameof(ESignConfig.Permissions), nameof(ESignConfig.IsActive) });
            }
            cache.Set(FormatCacheKey(category, tenant), permissions == null ? Array.Empty<string>() : permissions.ToArray());
        }
        private static string FormatCacheKey(string key, string tenant) => $"ESignConfig:{tenant}:{key}";

    }
}
