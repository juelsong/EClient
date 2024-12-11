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
namespace ESys.Management.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Db.DbContext;
    using ESys.Service;
    using ESys.Utilty.Service;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TenantBackgroundService : BackgroundService
    {
        private record TenantBackgroundServiceContext(Tenant Tenant, IHost Host);
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<TenantBackgroundService> logger;
        private readonly Dictionary<Tenant, TenantBackgroundServiceContext> hostDic = new();
        private readonly Dictionary<string, TenantBackgroundServiceContext> codeHostDic = new();
        private readonly List<ITenantBackgroundService> tenants = new();

        public TenantBackgroundService(IServiceProvider serviceProvider, ILogger<TenantBackgroundService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.tenants.Clear();

            this.tenants.AddRange(Furion.App.EffectiveTypes
                                .Where(t => t.IsClass
                                         && !t.IsAbstract
                                         && t.IsAssignableTo(typeof(ITenantBackgroundService)))
                                .Select(type =>
                                {
                                    try
                                    {
                                        var configure = Activator.CreateInstance(type) as ITenantBackgroundService;
                                        return configure;
                                    }
                                    catch (Exception ex)
                                    {
                                        this.logger.LogError(ex, "can not create {typeName}\t for {interface}", type.Name, nameof(ITenantBackgroundService));
                                        return null;
                                    }
                                })
                                .Where(c => c != null));
            await using var scope = this.serviceProvider.CreateAsyncScope();
            var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
            await tenantService.ExecuteInTenantScope((s) =>
            {
                var ts = s.ServiceProvider.GetRequiredService<ITenantService>();
                var t = ts.GetCurrentTenant();
                var ctx = this.GetHostContext(t);
                this.hostDic.Add(t, ctx);
                this.codeHostDic.Add(t.Code, ctx);
                return Task.CompletedTask;
            });
            foreach (var kvp in this.hostDic)
            {
                var ctx = kvp.Value;
                ctx.Host.Start();
                foreach (var item in this.tenants)
                {
                    try
                    {
                        item.OnHostStart(kvp.Key, kvp.Value.Host.Services);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, "OnHostStart error tenant:{code}\t{backgroundBack}", kvp.Key.Code, item.GetType());
                    }
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var kvp in this.hostDic)
            {
                var ctx = kvp.Value;
                foreach (var item in this.tenants)
                {
                    try
                    {
                        item.OnHostStop(kvp.Key, kvp.Value.Host.Services);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, "OnHostStart error tenant:{code}\t{backgroundBack}", kvp.Key.Code, item.GetType());
                    }
                }
                ctx.Host.StopAsync().Wait();
            }

            return base.StopAsync(cancellationToken);
        }
        public IServiceProvider GetServiceProvider(string tenant)
        {
            if (this.codeHostDic.TryGetValue(tenant, out var ctx))
            {
                return ctx.Host.Services;
            }
            return null;
        }


        private TenantBackgroundServiceContext GetHostContext(Tenant tenant)
        {
            var builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices(services =>
            {
                services.AddLogging(lb =>
                {
                    lb
                    .AddConsole()
                    .AddDebug()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Error)
                    .AddLog4Net()
                    ;
                });
                services.AddDatabaseAccessor(options =>
                {
                    options
                    .CustomizeMultiTenants()
                    .AddDb<MasterDbContext>()
                    .AddDb<TenantMasterDbContext, TenantMasterLocator>()
                    .AddDb<TenantSlaveDbContext, TenantSlaveLocator>()
                    ;
                });
                var configuration = Furion.App.Configuration as IConfigurationRoot;
                services.AddSingleton<IConfigurationRoot>(configuration);
                services.AddSingleton<IConfiguration>(configuration);
                services.AddSingleton<ITenantService>(new FakeTenantService(tenant, this.GetServiceProvider));
                services.AddScoped<IInterceptorProvider, DbInterceptorProvider>();
                services.AddMemoryCache();
                services.AddScoped<ESignDataHelper>()
                    .AddScoped<IDataProvider>(sp => sp.GetRequiredService<ESignDataHelper>())
                    .AddScoped<IDataInjector>(sp => sp.GetRequiredService<ESignDataHelper>())
                    ;

                services.Scan(scan =>
                {
                    scan.FromAssemblies(Furion.App.Assemblies)
                        .AddClasses(c => c.AssignableTo<SaveChangesInterceptor>())
                        .As<SaveChangesInterceptor>().WithScopedLifetime();

                });
                foreach (var item in this.tenants)
                {
                    try
                    {
                        item.Configure(tenant, services, true);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError(ex, "configure  tenant {tenant} error type:{type}", tenant.Code, item.GetType().Name);
                    }
                }
            });

            var host = builder.Build();
            return new TenantBackgroundServiceContext(
                tenant,
                host);
        }
    }
}
