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

namespace ESys.Security.Service
{
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using ESys.Utilty.Defs;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal class UserServiceFactory : IUserServiceFactory
    {
        private readonly UserManagementMode userManagementModel;
        private readonly IServiceProvider serviceProvider;
        private readonly IOptions<SecuritySettingOptions> options;
        public UserServiceFactory(IServiceProvider serviceProvider, IOptions<SecuritySettingOptions> options)
        {
            this.serviceProvider = serviceProvider;
            this.options = options;
            var tenant = this.serviceProvider.GetRequiredService<ITenantService>().GetCurrentTenant();
            this.userManagementModel = this.serviceProvider.GetRequiredService<IConfigService>()
                                    .GetConfig<UserManagementMode>(tenant.Code, ConstDefs.SystemConfigKey.UserManagementModelConfig).Result;
        }
        public IUserService GetUserService()
        {
            return this.userManagementModel switch
            {
                UserManagementMode.ESys => new EmisUserService(this.serviceProvider, this.options),
                UserManagementMode.LDAP => new LdapUserService(this.serviceProvider, this.options),
                _ => throw new NotImplementedException(),
            };
        }
        internal static string FormatUser(string tenant, int userId) => $"Security:{tenant}:LoginFailed_{userId}";

        internal static async Task<UserDashboardLayout> GetUserOrDefaultDashboardLayout(UserProfile profile, IConfigService configService, string tenant)
        {
            var layout = await configService.GetConfig<DashboardLayout>(
                tenant,
                ConstDefs.SystemConfigKey.DashboardLayout);
            var component = string.IsNullOrEmpty(profile?.DashboardConfig)
                          ? await configService.GetConfig<string[]>(tenant, ConstDefs.SystemConfigKey.DashboardDefault)
                          : JsonSerializer.Deserialize<string[]>(profile.DashboardConfig);
            return new UserDashboardLayout()
            {
                XS = layout?.XS,
                SM = layout?.SM,
                MD = layout?.MD,
                LG = layout?.LG,
                XL = layout?.XL,
                Margin = layout?.Margin ?? 0,
                Height = layout?.Height ?? 0,
                ContentCode = component
            };
        }
    }
}
