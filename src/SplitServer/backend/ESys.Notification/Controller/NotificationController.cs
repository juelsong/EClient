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

namespace ESys.Notification.Controller
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Notification.Entity;
    using ESys.Notification.Service;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 订阅模型
    /// </summary>
    public class SubscriptionModel
    {
        /// <summary>
        /// 通知类型Id
        /// </summary>
        public int? NotificationTypeId { get; set; }
        /// <summary>
        /// 订阅用户Id
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// 订阅参数Id  当NotificationTypeId有值时为用户Id，当UserId有值时为NotificationTypeId
        /// </summary>
        public int[] Subscribers { get; set; }
    }

    /// <summary>
    /// 通知控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private readonly ILogger<NotificationController> logger;
        private static readonly string[] SubscriptionUpdateNames = new string[]
        {
            nameof(ESys.Notification.Entity.Subscription.IsActive),
            nameof(ESys.Notification.Entity.Subscription.UpdateBy),
            nameof(ESys.Notification.Entity.Subscription.UpdatedTime),
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="msRepository"></param>
        /// <param name="serviceProvider"></param>
        public NotificationController(
            ILogger<NotificationController> logger,
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository,
            IServiceProvider serviceProvider
            )
        {
            this.logger = logger;
            this.msRepository = msRepository;
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 设置订阅
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<Result<bool>> Subscription([FromBody] SubscriptionModel model)
        {
            if (!this.serviceProvider.GetService<IDataProvider>().TryGetCurrentUserId(out var currentUserId))
            {
                return ResultBuilder.Error<bool>(ErrorCode.User.TokenExpired);
            }
            var timestamp = DateTimeOffset.Now;
            var repo = this.msRepository.Master<Subscription>();
            var subscriptions = repo.Where(s => s.LocationId == model.LocationId);

            Func<Subscription, bool> isActiveSetter = null;
            int[] existIds = Array.Empty<int>();
            Func<int, Subscription> createNew = null;
            if (model.UserId.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.UserId == model.UserId);
                isActiveSetter = s => model.Subscribers.Contains(s.NotificationTypeId);
                existIds = subscriptions.Select(s => s.NotificationTypeId).ToArray();
                createNew = (notificationTypeId) => new Subscription()
                {
                    CreateBy = currentUserId,
                    CreatedTime = timestamp,
                    NotificationTypeId = notificationTypeId,
                    UserId = model.UserId,
                    IsActive = true,
                    LocationId = model.LocationId,
                };
            }
            else if (model.NotificationTypeId.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.NotificationTypeId == model.NotificationTypeId.Value);
                isActiveSetter = s => s.UserId.HasValue && model.Subscribers.Contains(s.UserId.Value);
                existIds = subscriptions.Where(s => s.UserId.HasValue).Select(s => s.UserId.Value).ToArray();
                createNew = (userId) => new Subscription()
                {
                    CreateBy = userId,
                    CreatedTime = timestamp,
                    NotificationTypeId = model.NotificationTypeId.Value,
                    UserId = userId,
                    IsActive = true,
                    LocationId = model.LocationId,
                };
            }
            else
            {
                throw new ArgumentException();
            }

            try
            {
                foreach (var subscription in subscriptions)
                {
                    isActiveSetter(subscription);
                    if (subscription.IsActive != isActiveSetter(subscription))
                    {
                        subscription.IsActive = !subscription.IsActive;
                        subscription.UpdateBy = currentUserId;
                        subscription.UpdatedTime = timestamp;

                        await repo.UpdateIncludeAsync(subscription, SubscriptionUpdateNames);
                    }
                }

                foreach (var id in model.Subscribers.Where(id => !existIds.Contains(id)))
                {
                    var subscription = createNew(id);

                    await repo.InsertAsync(subscription, true);

                }

                await repo.SaveNowAsync();
                return ResultBuilder.Ok(true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, System.Text.Json.JsonSerializer.Serialize(model));
                return ResultBuilder.Error<bool>(ErrorCode.Service.InnerError, ex.Message);
            }
        }

        /// <summary>
        /// 获取email配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("email-config")]
        public async Task<Result<EMailConfig>> GetEMailConfig()
        {
            var tenantId = this.HttpContext.GetTenantId();
            var configService = this.serviceProvider.GetRequiredService<IConfigService>();
            var configuration = this.serviceProvider.GetRequiredService<IConfiguration>();
            var config = await configService.GetConfig<EMailConfig>(tenantId, ConstDefs.SystemConfigKey.EmailConfig)
                ?? configuration.GetSection("Email").Get<EMailConfig>();
            return ResultBuilder.Ok(config);
        }
        /// <summary>
        /// 设置email配置
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("email-config")]
        public async Task<Result<bool>> PatchEMailConfig([FromBody] EMailConfig config)
        {
            var tenantId = this.HttpContext.GetTenantId();
            var configService = this.serviceProvider.GetRequiredService<IConfigService>();
            await configService.SetConfig<EMailConfig>(tenantId, ConstDefs.SystemConfigKey.EmailConfig, config);
            return ResultBuilder.Ok(true);
        }
    }
}
