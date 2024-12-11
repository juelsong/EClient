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

namespace ESys.Notification.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Notification.Entity;
    using ESys.Security.Entity;
    using ESys.Utilty;
    using Furion.DatabaseAccessor;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 处理订阅服务
    /// </summary>
    public class SubscriptionService : BackgroundService
    {
        private readonly NotificationConfig config;
        private readonly ILogger<SubscriptionService> logger;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        public SubscriptionService(
            ILogger<SubscriptionService> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.config = configuration.GetSection("Notification").Get<NotificationConfig>();
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation($"background service start: {this.GetType().Name}");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = this.serviceProvider.CreateScope();
                    var tenantService = scope.ServiceProvider.GetService<ITenantService>();
                    await tenantService.ExecuteInTenantScope(async (tenantScope) =>
                    {
                        if (!stoppingToken.IsCancellationRequested)
                        {
                            await this.BuildNotificationEMail(tenantScope.ServiceProvider, stoppingToken);
                        }
                    });

                    await Task.Delay(this.config.IntervalSeconds * 1000, stoppingToken);
                }
                catch (TaskCanceledException) { }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, $"{this.GetType().Name} Error {ex.Message}");
                }
            }
            this.logger.LogInformation($"background service stop: {this.GetType().Name}");
        }


        private async Task BuildNotificationEMail(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            IEMailBuilder GetBuilder(int typeId, IServiceProvider serviceProvider)
            {
                var type = (NotificationTypes)typeId;
                return serviceProvider.GetServices<IEMailBuilder>().FirstOrDefault(b => b.ProcessedType == type);
            }
            // TODO 根据订阅用户设置选择
            var cultureInfo = new CultureInfo("zh-CN");
            var repo = serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var notificationSubscriptions = this.GetNotificationWithSubscription(serviceProvider);
            var notificationWithSubscriptions = notificationSubscriptions.Where(item => item.Subscription != null).ToList();
            var emailNotificationIdDic = this.config.GroupByNotificationType
                ? notificationWithSubscriptions
                    .GroupBy(e => e.Notification.NotificationTypeId)
                    .SelectMany(g => g.GroupBy(e => e.Subscription.UserId.GetValueOrDefault()))
                    .ToDictionary(g =>
                    {
                        var group = g.First();
                        var builder = GetBuilder(group.Notification.NotificationTypeId, serviceProvider);
                        var ret = builder.BuildBatchEMail(cultureInfo, g.Select(ne => ne.Notification));
                        ret.UserId = group.Subscription.UserId.Value;
                        return ret;
                    }, g => g.Select(item => item.Notification.NotificationId).ToArray())
                : notificationWithSubscriptions
                    .ToDictionary(e =>
                    {
                        var builder = GetBuilder(e.Notification.NotificationTypeId, serviceProvider);
                        var ret = builder.BuildEMail(cultureInfo, e.Notification);
                        ret.UserId = e.Subscription.UserId.Value;
                        return ret;
                    }, e => new long[] { e.Notification.NotificationId });
            var writableEMailRepo = repo.Master<EMail>();
            var writableNotificationRepo = repo.Master<Notification>();
            var writableNotificationEMailRepo = repo.Master<NotificationEMail>();
            using var trans = writableEMailRepo.Database.BeginTransaction();
            await writableEMailRepo.InsertAsync(emailNotificationIdDic.Keys, stoppingToken);
            await writableEMailRepo.SaveNowAsync(stoppingToken);
            var names = new string[] { nameof(Notification.NotificationDate) };
            // 有邮件
            var notificationEMailsssss = emailNotificationIdDic
                .SelectMany(kvp => kvp.Value.Select(notificationId => new NotificationEMail()
                {
                    NotificationId = notificationId,
                    EMailId = kvp.Key.Id,
                    CreatedTime = DateTimeOffset.Now,
                })).ToArray();

            await writableNotificationEMailRepo.InsertAsync(notificationEMailsssss, stoppingToken);

            // 不管有没有邮件（即有没有订阅），都算处理完毕
            foreach (var notifycationId in notificationSubscriptions.Select(item => item.Notification.NotificationId).Distinct())
            {
                var notify = new Notification()
                {
                    NotificationDate = DateTime.Now,
                    Id = notifycationId,
                };
                await writableNotificationRepo.UpdateIncludeAsync(notify, names);
            }

            await writableNotificationRepo.SaveNowAsync(stoppingToken);
            await trans.CommitAsync(stoppingToken);
        }

        /// <summary>
        /// 获取所有没通知的通知和电邮列举，如果没有订阅，电邮为空
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private IEnumerable<(NotificationV Notification, SubscriptionV Subscription)> GetNotificationWithSubscription(IServiceProvider serviceProvider)
        {
            // moda逻辑在ModaTaskService\ModaServicesHost.exe->ModaServicesHost.NotificationManagement.NotificationManager.GetNotificationEmails
            var repository = serviceProvider
                .GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var notNotified = repository.Slave1<NotificationV>().Where(nv => nv.NotificationDate == null).ToArray();
            var list = new List<(NotificationV Notification, SubscriptionV Subscription)>();
            if (notNotified.Any())
            {
                var subscriptionViews = repository
                    .Slave1<SubscriptionV>().Where(s => s.IsActive && s.UserStatus == UserStatus.Normal && s.EMail != null && s.EMail != "")
                    .ToArray();
                foreach (var subscription in subscriptionViews)
                {
                    if (!this.config.LocationUserSecurity
                        || IdBreadcrumbHelper.IsIdInBreadcrumbPath(
                            subscription.UserLocationId,
                            subscription.UserLocationBreadcrumb))
                    {
                        // MODA中 Notification 的NotificationTypeId可空
                        // 所以比对了Notification和Subscription 的NotificationTypeId是否都为空
                        foreach (var notification in notNotified
                            .Where(n => n.NotificationTypeId == subscription.NotificationTypeId))
                        {
                            var addNotification = false;
                            if (notification.SiteId.HasValue)
                            {
                                if (notification.SiteLocationId != null
                                    && IdBreadcrumbHelper.IsIdInBreadcrumbPath(
                                        subscription.LocationId,
                                        notification.SiteLocationBreadcrumb)
                                    && (!this.config.LocationUserSecurity
                                        || IdBreadcrumbHelper.IsIdInBreadcrumbPath(
                                                subscription.UserLocationId,
                                                notification.SiteLocationBreadcrumb)))
                                {
                                    addNotification = true;
                                }
                            }

                            else if (notification.UserId != null && notification.UserLocationId != null)
                            {
                                if (IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.LocationId, notification.UserLocationBreadcrumb)
                                    && (!this.config.LocationUserSecurity
                                        || IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.UserLocationId, notification.UserLocationBreadcrumb)))
                                {
                                    addNotification = true;
                                }
                            }
                            else if (notification.EquipmentId != null && notification.EquipmentLocationId != null)
                            {
                                if (IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.LocationId, notification.EquipmentLocationBreadcrumb)
                                    && (!this.config.LocationUserSecurity
                                        || IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.UserLocationId, notification.EquipmentLocationBreadcrumb)))
                                {
                                    addNotification = true;
                                }
                            }
                            else if (notification.MediaId != null && notification.MediaLocationId != null)
                            {
                                if (IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.LocationId, notification.MediaLocationBreadcrumb)
                                    && (!this.config.LocationUserSecurity
                                        || IdBreadcrumbHelper.IsIdInBreadcrumbPath(subscription.UserLocationId, notification.MediaLocationBreadcrumb)))
                                {
                                    addNotification = true;
                                }
                            }
                            else
                            {
                                addNotification = true;
                            }
                            if (addNotification)
                            {
                                list.Add((notification, subscription));
                            }
                        }
                    }
                }
                // 如果没有订阅
                var notSubscripted = notNotified.Except(list.Select(pair => pair.Notification));
                foreach (var item in notSubscripted)
                {
                    list.Add((item, null));
                }
            }
            return list;
        }

    }
}
