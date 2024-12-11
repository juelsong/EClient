namespace EmailNotify
{
    using log4net;
    using MailKit.Net.Smtp;
    using MailKit.Security;
    using Microsoft.Extensions.Configuration;
    using MimeKit;
    using System;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class EMailConfig
    {
        public class SbscribEmail
        {
            /// <summary>
            /// Email 用户名
            /// </summary>
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
        public string Server { get; set; } = string.Empty;
        public ushort Port { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int IntervalSeconds { get; set; }
        public bool GroupByNotificationType { get; set; }
        public bool EnableSSL { get; set; }
        public string Domain { get; set; } = string.Empty;
        public SbscribEmail[] SbscribEmails { get; set; } = Array.Empty<SbscribEmail>();
    }

    public class Email
    {
        private readonly EMailConfig config = new EMailConfig();
        private static ILog log = LogManager.GetLogger(nameof(Email));
        private readonly string tenant = "";
        public Email(EMailConfig eMailConfig, string tenant)
        {
            this.config = eMailConfig;
            this.tenant = tenant;
        }
        public async Task SendEMails(string data)
        {
            using var client = new SmtpClient();
            client.Connect(this.config.Server, this.config.Port, this.config.EnableSSL ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto, CancellationToken.None);
            var credentials = string.IsNullOrEmpty(this.config.Password)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(
                        this.config.Address,
                        this.config.Password,
                        this.config.Domain);
            var builder = new BodyBuilder();
            await client.AuthenticateAsync(new UTF8Encoding(false), credentials);

            builder.TextBody = data;
            using var message = new MimeMessage()
            {
                Subject = "服务异常",
                Body = builder.ToMessageBody()
            };
            foreach (var item in this.config.SbscribEmails)
            {
                message.To.Add(new MailboxAddress(item.UserName, item.Email));
                message.From.Add(new MailboxAddress($"EMIS tenant {this.tenant} State is false", this.config.Address));
                try
                {
                    await client.SendAsync(message);
                    log.Info($"send to {item.Email},message :{message}");
                }
                catch (Exception ex)
                {
                    log.Error($"send email ConnectState error:\r\n{ex}");
                }
            }
            client.Disconnect(true);
        }

    }
}
