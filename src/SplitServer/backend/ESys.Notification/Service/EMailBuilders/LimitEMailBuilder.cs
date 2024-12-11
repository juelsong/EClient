namespace ESys.Notification.Service.EMailBuilders
{
    using ESys.Contract.Service;
    using ESys.Notification.Entity;
    using Furion.DependencyInjection;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// 限度email构建器
    /// </summary>
    public class LimitEMailBuilder : EMailBuilderBase, ITransient, IEMailBuilder
    {
        private const string Endl = "\r\n";

        /// <summary>
        /// 可处理的通知类型
        /// </summary>
        public NotificationTypes ProcessedType => NotificationTypes.Deviation;


        /// <summary>
        /// 构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public EMail BuildEMail(CultureInfo culture, NotificationV notification)
        {
            return new EMail()
            {
                Subject = this.NotificationToSubject(culture, notification),
                IsHtmlBody = false,
                Body = this.NotificationToBody(culture, notification)
            };
        }

        private string NotificationToSubject(CultureInfo culture, NotificationV notification)
        {
            var messages = notification.Messages;

            var frequency = this.GetString(nameof(Resources.Resource.Frequency), culture);
            var limitOverrun = this.GetString(nameof(Resources.Resource.LimitOverrun), culture);
            var exclamation = this.GetString(nameof(Resources.Resource.Exclamation), culture);

            return (messages[0] == "1" ? frequency : "") + $"{limitOverrun}{exclamation}";
        }

        private string NotificationToBody(CultureInfo culture, NotificationV notification)
        {
            var messages = notification.Messages;

            var colon = this.GetString(nameof(Resources.Resource.Colon), culture);
            var leftParentheses = this.GetString(nameof(Resources.Resource.LeftParentheses), culture);
            var rightParentheses = this.GetString(nameof(Resources.Resource.RightParentheses), culture);

            var user = this.GetString(nameof(Resources.Resource.User), culture);
            var sampleBarcode = this.GetString(nameof(Resources.Resource.SampleBarcode), culture);
            var testLocation = this.GetString(nameof(Resources.Resource.TestLocation), culture);
            var testMethod = this.GetString(nameof(Resources.Resource.TestMethod), culture);

            var body = $"{user}{colon}{messages[1]}{leftParentheses}{messages[2]}{rightParentheses}";
            body += $"{Endl}{sampleBarcode}{colon}{messages[3]}";
            body += $"{Endl}{testLocation}{colon}{messages[4]}";
            body += $"{Endl}{testMethod}{colon}{messages[5]}";
            body += $"{Endl}{messages[6]}";
            body += messages[7];

            return body;
        }

        /// <summary>
        /// 批量构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public EMail BuildBatchEMail(CultureInfo culture, IEnumerable<NotificationV> notifications)
        {
            var batch = this.GetString(nameof(Resources.Resource.Batch), culture);
            var limitOverrun = this.GetString(nameof(Resources.Resource.LimitOverrun), culture);
            var exclamation = this.GetString(nameof(Resources.Resource.Exclamation), culture);

            return new EMail()
            {
                Subject = $"{batch}{limitOverrun}{exclamation}",
                IsHtmlBody = false,
                Body = string.Join($"{Endl}{Endl}", notifications.Select(notification => this.NotificationToBody(culture, notification)))
            };
        }
    }
}
