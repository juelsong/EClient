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
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using MimeKit;
    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 电邮服务
    /// </summary>
    public class EMailService : BackgroundService
    {
        private readonly EMailConfig config;
        private readonly ILogger<EMailService> logger;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="configuration"></param>
        public EMailService(
            ILogger<EMailService> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.config = configuration.GetSection("Email").Get<EMailConfig>();
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

                        try
                        {
                            using var stc = new CancellationTokenSource();
                            await this.SendNotificationEMails(tenantScope.ServiceProvider, stc.Token);
                        }
                        catch (Exception ex)
                        {
                            var tenantCode = string.Empty;
                            try
                            {
                                var tenant = tenantScope.ServiceProvider.GetService<ITenantService>()?.GetCurrentTenant();
                                tenantCode = tenant.Code;
                            }
                            catch (Exception)
                            {
                            }
                            this.logger.LogError(ex, "PlanAutoFillService error Tenant:{tenantCode}", tenantCode);
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

        private async Task SendNotificationEMails(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            var repo = serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            var tenantService = serviceProvider.GetRequiredService<ITenantService>();
            var configService = serviceProvider.GetRequiredService<IConfigService>();
            var writableNotificationEMailRepo = repo.Master<EMail>();
            var readableNotificationEMailRepo = repo.Slave1<EMail>();
            var emails = readableNotificationEMailRepo
                .Include(e => e.User)
                .Include(e => e.Attachments)
                .Where(e => e.SendDate == null).ToArray();
            var emailConfig = await configService.GetConfig<EMailConfig>(
                                    tenantService.GetCurrentTenant().Code,
                                    ConstDefs.SystemConfigKey.EmailConfig)
                              ?? this.config;

            var names = new string[] { nameof(EMail.SendDate) };
            if (emails.Length > 0)
            {
                using var client = new SmtpClient();
                using var trans = writableNotificationEMailRepo.Database.BeginTransaction();
                try
                {
                    client.Connect(
                        emailConfig.Server,
                        emailConfig.Port,
                        emailConfig.EnableSSL ? SecureSocketOptions.Auto : SecureSocketOptions.None,
                        stoppingToken);
                    var credentials = string.IsNullOrEmpty(emailConfig.Password)
                            ? CredentialCache.DefaultNetworkCredentials
                            : new NetworkCredential(
                                emailConfig.Address,
                                emailConfig.Password,
                                emailConfig.Domain);
                    await client.AuthenticateAsync(new UTF8Encoding(false), credentials, stoppingToken);
                    foreach (var email in emails)
                    {
                        if (string.IsNullOrEmpty(email.User.EMail))
                        {
                            // TODO 是否要加标记废弃邮件？
                            this.logger.LogError("can not send email without user EMail {userId}", email.User.Id);
                            continue;
                        }
                        var builder = new BodyBuilder();
                        foreach (var attachment in email.Attachments.Where(a => !a.UsedInBody))
                        {
                            builder.Attachments.Add(attachment.FilePath, stoppingToken);
                        }
                        if (email.IsHtmlBody)
                        {
                            builder.HtmlBody = email.Body;

                            foreach (var attachment in email.Attachments.Where(a => a.UsedInBody))
                            {
                                var content = builder.LinkedResources.Add(attachment.FilePath, stoppingToken);
                                content.ContentId = attachment.ContentId;
                            }
                        }
                        else
                        {
                            builder.TextBody = email.Body;
                        }
                        using var message = new MimeMessage()
                        {
                            Subject = email.Subject,
                            Body = builder.ToMessageBody()
                        };
                        message.To.Add(new MailboxAddress(email.User.RealName, email.User.EMail));
                        message.From.Add(new MailboxAddress("ESys", emailConfig.Address));
                        try
                        {
                            await client.SendAsync(message, stoppingToken);
                            email.SendDate = DateTime.Now;
                            await writableNotificationEMailRepo.UpdateIncludeAsync(email, names);
                        }
                        catch (Exception ex)
                        {
                            this.logger.LogError(ex, $"send email {email.Id} error");
                        }
                    }
                    client.Disconnect(true, stoppingToken);
                    await writableNotificationEMailRepo.SaveNowAsync(stoppingToken);
                    await trans.CommitAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    this.logger.LogError(ex, "send email error");
                }
            }
        }
    }
}
