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

namespace ESys.System.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.System.Entity;
    using Furion.DatabaseAccessor;
    using Furion.DependencyInjection;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using global::System.Text.Json;
    using ESys.Utilty.Serialization;

    /// <summary>
    /// 配置服务
    /// </summary>
    public class ConfigService : IConfigService, ITransient
    {
        private readonly IServiceProvider serviceProvider;
        private static readonly JsonSerializerOptions JsonOptions = GetJsonSerializerOptions();

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var ret = new JsonSerializerOptions();
            ret.Converters.Add(new TimeSpanConverter());
            ret.Converters.Add(new NullableTimeSpanConverter());
            return ret;
        }

        // TODO format
        private static readonly Dictionary<Type, Func<string, object>> parsers = new()
        {
            { typeof(bool), (str) => bool.Parse(str) },
            { typeof(byte), (str) => byte.Parse(str) },
            { typeof(short), (str) => short.Parse(str) },
            { typeof(ushort), (str) => ushort.Parse(str) },
            { typeof(int), (str) => int.Parse(str) },
            { typeof(uint), (str) => uint.Parse(str) },
            { typeof(long), (str) => long.Parse(str) },
            { typeof(ulong), (str) => ulong.Parse(str) },
            { typeof(float), (str) => float.Parse(str) },
            { typeof(double), (str) => double.Parse(str) },
            { typeof(decimal), (str) => decimal.Parse(str) },
            { typeof(string), (str) => str },
        };

        private static readonly Dictionary<Type, Func<object, string>> dic = new()
        {
            { typeof(bool), (val) => val.ToString() },
            { typeof(byte), (val) => val.ToString() },
            { typeof(short), (val) => val.ToString() },
            { typeof(ushort), (val) => val.ToString() },
            { typeof(int), (val) => val.ToString() },
            { typeof(uint), (val) => val.ToString() },
            { typeof(long), (val) => val.ToString() },
            { typeof(ulong), (val) => val.ToString() },
            { typeof(float), (val) => val.ToString() },
            { typeof(double), (val) => val.ToString() },
            { typeof(decimal), (val) => val.ToString() },
            { typeof(string), (val) => (string)val },
        };
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ConfigService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tenant"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public Task<T> GetConfig<T>(string tenant, string configKey)
        {
            var cache = this.serviceProvider.GetService<IMemoryCache>();
            var tenantSerivce = this.serviceProvider.GetService<ITenantService>();
            tenantSerivce.SetTenantScope(tenant);
            return cache.GetOrCreateAsync<T>(
                FormatCacheKey(configKey, tenant),
                (entry) =>
                {
                    var repo = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
                    var config = repo.Slave1<ConfigItem>().FirstOrDefault(c => c.Property == configKey);
                    var ret = config == null ? default
                        : typeof(T).IsEnum ? (T)Enum.Parse(typeof(T), config.Value)
                        : parsers.TryGetValue(typeof(T), out var parser) ? (T)parser(config.Value)
                        : JsonSerializer.Deserialize<T>(config.Value, JsonOptions);
                    return Task.FromResult(ret);
                });
        }
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tenant"></param>
        /// <param name="configKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetConfig<T>(string tenant, string configKey, T value)
        {
            var cache = this.serviceProvider.GetService<IMemoryCache>();
            var tenantSerivce = this.serviceProvider.GetService<ITenantService>();
            tenantSerivce.SetTenantScope(tenant);
            var repo = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var config = repo.Slave1<ConfigItem>().FirstOrDefault(c => c.Property == configKey);
            var valStr = typeof(T).IsEnum ? value.ToString()
                : dic.TryGetValue(typeof(T), out var serializer) ? serializer(value)
                : JsonSerializer.Serialize(value, JsonOptions);
            if (config == null)
            {
                config = new ConfigItem()
                {
                    Property = configKey,
                    Value = valStr
                };
                await repo.Master<ConfigItem>().InsertNowAsync(config);
            }
            else
            {
                config.Value = valStr;
                await repo.Master<ConfigItem>().UpdateIncludeNowAsync(config, new[] { nameof(ConfigItem.Value) });
            }
            cache.Set(FormatCacheKey(configKey, tenant), value);
        }
        private static string FormatCacheKey(string key, string tenant) => $"Config:{tenant}:{key}";
    }
}
