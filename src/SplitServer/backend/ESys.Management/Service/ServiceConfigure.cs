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
    using ESys.Contract.Service;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 服务配置
    /// </summary>
    public class ServiceConfigure : IServiceConfigure
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startBackground"></param>
        /// <returns></returns>
        public IServiceCollection Configure(IServiceCollection services, bool startBackground)
        {
            services.AddScoped<ITenantService, TenantService>();
            //if (startBackground)
            {
                services.AddSingleton<TenantBackgroundService>();
                services.AddHostedService(sp => sp.GetRequiredService<TenantBackgroundService>());
            }
            return services;
        }
    }
}
