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

namespace ESys.Security.Utility
{
    using ESys.Contract.Service;
    using ESys.Security.Models;
    using ESys.Security.PassValidators;
    using ESys.Utilty.Defs;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal static class Extensions
    {
        public static async Task<SecurityConfig> GetSecurityConfig(this IConfigService configService, string tenantId)
        {
            var config = await configService.GetConfig<SecurityConfig>(
                    tenantId,
                    ConstDefs.SystemConfigKey.SecurityConfig)
                ?? new SecurityConfig() { PassValidators = Array.Empty<SecurityConfig.PassValidatorItem>() };
            //lock (config)
            //{
            //    foreach (var validatorItem in config.PassValidators.Where(item => item.Validator == null))
            //    {
            //        var validatorType = Assembly.GetExecutingAssembly().GetType(validatorItem.Type);
            //        if (validatorType != null)
            //        {
            //            validatorItem.Validator = (IPassValidator)JsonSerializer.Deserialize(validatorItem.JsonInfo, validatorType);
            //        }
            //    }
            //}
            return config;
        }
    }
}
