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

namespace ESys.Utilty.Attributes
{
    using ESys.Contract.Service;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;


    /// <summary>
    /// 电子签名检测特性
    /// </summary>
    public class CheckESignAttribute : ActionFilterAttribute
    {
        private class EntityWithNamesJsonConverter : JsonConverter<ValueTuple<IPrivateEntity, string[]>>
        {
            public override (IPrivateEntity, string[]) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var type = string.Empty;
                var data = string.Empty;
                var names = string.Empty;
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.PropertyName:
                            {
                                var propName = reader.GetString();
                                reader.Read();
                                switch (propName)
                                {
                                    case "type":
                                        type = reader.GetString();
                                        break;
                                    case "data":
                                        data = reader.GetString();
                                        break;
                                    case "names":
                                        names = reader.GetString();
                                        break;
                                }
                            }
                            break;

                        case JsonTokenType.EndObject:
                            {
                                var entity = JsonSerializer.Deserialize(data, Type.GetType(type)) as IPrivateEntity;
                                return ValueTuple.Create(entity, names.Split(','));
                            }
                    }
                }
                throw new JsonException("Expected EndObject token");
            }

            public override void Write(Utf8JsonWriter writer, (IPrivateEntity, string[]) value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                writer.WriteString("type", value.Item1.GetType().AssemblyQualifiedName);
                writer.WriteString("data", JsonSerializer.Serialize(value.Item1, value.Item1.GetType()));
                writer.WriteString("names", string.Join(',', value.Item2));
                writer.WriteEndObject();
            }
        }
        private static readonly DistributedCacheEntryOptions cacheEntryOptions = new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) };
        private class ESignedRecord
        {
            public ESignData[] ESignDataArray { get; set; }
        }
        private const string ESignedRecordKey = "ESys.ESignedRecord";
        public string Key { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">是否需要签名配置键</param>
        public CheckESignAttribute(string key)
        {
            this.Key = key;
        }
        private static string FormatParKey(string uuid) => $"ESign:{uuid}";

        private static JsonSerializerOptions GetActionParameterSerializerOptions()
        {
            var ret = new JsonSerializerOptions();
            ret.Converters.Add(new EntityWithNamesJsonConverter());
            return ret;
        }
        private static readonly JsonSerializerOptions DefaultActionParameterSerializerOptions = GetActionParameterSerializerOptions();
        /// <summary>
        /// 执行前拦截
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sp = context.HttpContext.RequestServices;
            var configService = sp.GetService<IESignConfigService>();
            var notificationService = sp.GetService<INotificationService>();
            var esignConfig = await configService.GetPermissions(context.HttpContext.GetTenantId(), this.Key);
            var cache = sp.GetService<IDistributedCache>();
            var request = context.HttpContext.Request;
            var logger = sp.GetService<ILogger<CheckESignAttribute>>();
            var permissionMgr = sp.GetService<IPermissionManager>();
            var injector = sp.GetService<IDataInjector>();
            var log = sp.GetService<ILogService>();
            var needESign = esignConfig.Permissions.Any();
            // 需要签名
            if (needESign)
            {
                var serailizedParameter = request.TryGetSingleHeader(ConstDefs.RequestHeader.ESign.SerialNumber, out var serialKey)
                        ? await cache.GetStringAsync(FormatParKey(serialKey))
                        : string.Empty;
                var signCount = esignConfig.SignCount;
                if (request.TryGetSingleHeader(ConstDefs.RequestHeader.ESign.ESignCount, out var signCountStr)
                    && int.TryParse(signCountStr, out var headerSignCount)
                    && headerSignCount > -1)
                {
                    signCount = headerSignCount;
                }
                if (signCount <= 0)
                {
                    context.Result = (await next()).Result;
                    return;
                }
                if (request.TryGetSingleHeader(ConstDefs.RequestHeader.ESign.Account, out var esignAccount)
                    && request.TryGetSingleHeader(ConstDefs.RequestHeader.ESign.Password, out var esignPass))// 有签名数据
                {
                    if (string.IsNullOrEmpty(serailizedParameter)) // 没有数据
                    {
                        context.Result = new JsonResult(ResultBuilder.Error(ErrorCode.Service.DataExpired, "数据过期"));
                        return;
                    }
                    var parameterStrDic = JsonSerializer.Deserialize<Dictionary<string, string>>(serailizedParameter);
                    var parameterDic = parameterStrDic
                       .ToDictionary(
                           kvp => kvp.Key,
                           kvp => kvp.Key == ESignedRecordKey
                           ? JsonSerializer.Deserialize<ESignedRecord>(kvp.Value)
                           : JsonSerializer.Deserialize(
                                   kvp.Value,
                                   context.ActionDescriptor.Parameters
                                       .First(p => p.Name == kvp.Key)
                                       .ParameterType,
                                   DefaultActionParameterSerializerOptions));
                    // Token过期
                    if (!context.HttpContext.TryGetUserId(out var userId))
                    {
                        context.Result = new JsonResult(ResultBuilder.Error(ErrorCode.User.TokenExpired));
                        log.LogData(new LogInfo()
                        {
                            Description = "Token Timeout",
                            TypeName = ConstDefs.LogTypeNames.EsignData,
                            UserId = userId,
                        });
                        return;
                    }
                    var signedUserCode = permissionMgr.CheckUser(esignAccount, esignPass, out var esignedUserId, out var esignedUserRealName);
                    // 用户错误
                    if (signedUserCode != ErrorCode.NoError)
                    {
                        context.Result = new JsonResult(ResultBuilder.Error(signedUserCode));
                        await notificationService.AddNotification(NotificationTypes.InvalidESig, userId, null, null, null, null, null, esignAccount, serailizedParameter);
                        log.LogData(new LogInfo()
                        {
                            Description = "User Error",
                            TypeName = ConstDefs.LogTypeNames.EsignData,
                            UserId = userId,
                        });
                        return;
                    }

                    // 没有签名权限
                    if (!permissionMgr.HasAnyPermission(esignedUserId.Value, esignConfig.Permissions))
                    {
                        context.Result = new JsonResult(ResultBuilder.Error(ErrorCode.User.NotAuthorized));
                        await notificationService.AddNotification(NotificationTypes.InvalidESig, userId, null, null, null, null, null, esignAccount, serailizedParameter);
                        log.LogData(new LogInfo()
                        {
                            Description = "User not have permission",
                            TypeName = ConstDefs.LogTypeNames.EsignData,
                            UserId = userId,
                        });
                        return;
                    }

                    // 用户已经签过
                    var esignRecord = parameterDic[ESignedRecordKey] as ESignedRecord;
                    if (esignRecord.ESignDataArray.Any(d => d.ESignedBy == esignedUserId.Value))
                    {
                        // TODO notificationService?
                        context.Result = new JsonResult(ResultBuilder.Error(ErrorCode.Service.DuplicateESign));
                        log.LogData(new LogInfo()
                        {
                            Description = "Data signed",
                            TypeName = ConstDefs.LogTypeNames.EsignData,
                            UserId = userId,
                        });
                        return;
                    }
                    request.TryGetSingleHeader(ConstDefs.RequestHeader.ESign.Comment, out var esigncComment);
                    var newIds = new List<ESignData>(esignRecord.ESignDataArray)
                    {
                        new ESignData()
                        {
                            ESignedBy = esignedUserId.Value,
                            Account = esignAccount,
                            Category = this.Key,
                            RealName = esignedUserRealName,
                            Comment = esigncComment,
                            UserId = userId,
                            IpAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                            Order = esignRecord.ESignDataArray.Length,
                            Timestamp = DateTimeOffset.Now
                        }
                    };
                    esignRecord.ESignDataArray = newIds.ToArray();

                    // 签名次数不够
                    if (esignRecord.ESignDataArray.Length < signCount)
                    {
                        parameterStrDic[ESignedRecordKey] = JsonSerializer.Serialize(esignRecord, DefaultActionParameterSerializerOptions);
                        var serialized = JsonSerializer.Serialize(parameterStrDic);
                        var serialNumber = Guid.NewGuid().ToString("N");
                        cache.SetString(FormatParKey(serialNumber), serialized, cacheEntryOptions);
                        context.Result = new JsonResult(ResultBuilder.Error(
                            ErrorCode.Service.NeedESign,
                            new
                            {
                                SerialNumber = serialNumber,
                                Category = this.Key,
                                Total = signCount,
                                Current = esignRecord.ESignDataArray.Length
                            }));
                    }
                    else
                    {
                        if (userId != 0)
                        {
                            injector.InjectCurrentUserId(userId);
                        }
                        injector.InjectESignData(esignRecord.ESignDataArray);
                        parameterDic.Remove(ESignedRecordKey);
                        context.ActionArguments.Clear();
                        // TODO
                        foreach (var kvp in parameterDic)
                        {
                            context.ActionArguments[kvp.Key] = kvp.Value;
                        }
                        context.Result = (await next()).Result;
                    }
                }
                else// 没有签名数据
                {
                    var dic = context.ActionArguments.ToDictionary(
                        kvp => kvp.Key,
                        kvp => JsonSerializer.Serialize(kvp.Value, kvp.Value.GetType(), DefaultActionParameterSerializerOptions));
                    dic.Add(ESignedRecordKey, JsonSerializer.Serialize(
                        new ESignedRecord()
                        {
                            ESignDataArray = Array.Empty<ESignData>()
                        },
                        DefaultActionParameterSerializerOptions));
                    var serialized = JsonSerializer.Serialize(dic);
                    var serialNumber = Guid.NewGuid().ToString("N");
                    cache.SetString(FormatParKey(serialNumber), serialized, cacheEntryOptions);
                    context.Result = new JsonResult(ResultBuilder.Error(
                        ErrorCode.Service.NeedESign,
                        new
                        {
                            SerialNumber = serialNumber,
                            Category = this.Key,
                            Total = signCount,
                            Current = 0
                        }));
                }
            }
            ////无需签名且已经cache过数据了，使用cache的数据
            //else if (SetActionParameterFromCache(parameterDic))
            //{
            //    context.Result = (await next()).Result;
            //}
            else
            {
                context.Result = (await next()).Result;
            }
        }

    }
}
