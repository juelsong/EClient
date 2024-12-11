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

using ESys.Contract.Service;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tenant = ESys.Contract.Service.Tenant;
using TenantEntity = ESys.Security.Entity.Tenant;

namespace ESys.Management.Service
{
    /// <summary>
    /// 租户服务
    /// </summary>
    internal class TenantService : ITenantService
    {
        private readonly IRepository<TenantEntity> tenantRepository;
        private readonly IMemoryCache memoryCache;
        private string currentTenantId = null;
        private readonly IServiceProvider serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantRepository"></param>
        /// <param name="memoryCache"></param>
        /// <param name="serviceProvider"></param>
        public TenantService(
            IRepository<TenantEntity> tenantRepository,
            IMemoryCache memoryCache,
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.tenantRepository = tenantRepository;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取当前租户
        /// </summary>
        /// <returns></returns>
        public Tenant GetCurrentTenant()
        {
#if MIGRATION
            var tenantId = "Emis";
#else
            var tenantId = this.currentTenantId;//?? Furion.App.HttpContext?.GetTenantId();
#endif
            var tenant = this.memoryCache.GetOrCreate<Tenant>(FormatCacheKey(tenantId), e =>
            {
                var entity = this.tenantRepository.FirstOrDefault(t => t.Code == tenantId, false);
                return entity == null ? null : new Tenant()
                {
                    SlaveDbConnStr = entity.SlaveDbConnStr,
                    MasterDbConnStr = entity.MasterDbConnStr,
                    Code = entity.Code,
                    DbType = entity.DbType,
                    Limits = entity.Limits,
                    Name = entity.Name
                };
            });
            return tenant;
        }
        /// <summary>
        /// 租户作用域
        /// </summary>
        /// <param name="action"></param>
        /// <param name="validOnly"></param>
        /// <returns></returns>
        public async Task ExecuteInTenantScope(Func<IServiceScope, Task> action, bool validOnly)
        {
            // TODO cache
            var entities = this.tenantRepository.AsQueryable(false);
            //因为postgre时区问题，导致无法比较和存储Datetime 所以只能判断Master数据库类型
            var isPostGre = this.tenantRepository.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL";
            if (validOnly)
            {
                var now = isPostGre ? DateTime.Now.ToUniversalTime() : DateTime.Now;

                entities = entities.Where(t => (isPostGre ? DateTime.SpecifyKind(t.Limits, DateTimeKind.Utc) > now : t.Limits > now));

            }
            foreach (var item in entities.Select(entity => new Tenant()
            {
                SlaveDbConnStr = entity.SlaveDbConnStr,
                MasterDbConnStr = entity.MasterDbConnStr,
                Code = entity.Code,
                DbType = entity.DbType,
                Limits = isPostGre ? DateTime.SpecifyKind(entity.Limits, DateTimeKind.Utc) : entity.Limits,
                Name = entity.Name
            }).ToList())
            {
                using var scope = this.serviceProvider.CreateScope();
                var innserTenantService = scope.ServiceProvider.GetService<ITenantService>();
                innserTenantService.SetTenantScope(item.Code);
                await action(scope);
            }
        }
        /// <summary>
        /// 设置租户作用域
        /// </summary>
        /// <param name="tenantId"></param>
        public void SetTenantScope(string tenantId)
        {
            this.currentTenantId = tenantId;
        }


        private static string FormatCacheKey(string tenantId) => $"Tenant:{tenantId}";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IServiceProvider GetTenantServiceProvider()
        {
            var tenantBackgroundService = this.serviceProvider.GetRequiredService<TenantBackgroundService>();
            return tenantBackgroundService.GetServiceProvider(this.currentTenantId);
        }
    }
}
