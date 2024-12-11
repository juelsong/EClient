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

namespace ESys.Notification.Service.EMailBuilders
{
    using ESys.Contract.Service;
    using ESys.Notification.Entity;
    using Furion.DependencyInjection;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// 电子签名email构建器
    /// </summary>
    public class ESignEMailBuilder : EMailBuilderBase, ITransient, IEMailBuilder
    {
        /// <summary>
        /// 电子签名详情
        /// </summary>
        public class ESignDetail
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="notification"></param>
            /// <param name="dateTimeFormater"></param>
            public ESignDetail(NotificationV notification, string dateTimeFormater)
            {
                var strs = notification.Messages;
                if (strs != null)
                {
                    this.Account = strs[0];
                    this.Content = strs[1];
                }

                this.FormatedDatetime = notification.CreatedTime.LocalDateTime.ToString(dateTimeFormater);
            }
            /// <summary>
            /// 签名的账号
            /// </summary>
            public string Account { get; }
            /// <summary>
            /// 签名的内容
            /// </summary>
            public string Content { get; }
            /// <summary>
            /// 日期格式
            /// </summary>
            public string FormatedDatetime { get; }
        }
        /// <summary>
        /// 可处理的通知类型
        /// </summary>
        public NotificationTypes ProcessedType => NotificationTypes.InvalidESig;


        /// <summary>
        /// 构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        public EMail BuildEMail(CultureInfo culture, NotificationV notification)
        {
            var template = @$"<html>
            <body>
            <table>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderESignAccount), culture)}:</td>
                <td>&nbsp;@Model.{nameof(ESignDetail.Account)}</td>
              </tr>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderESignTimestamp), culture)}:</td>
                <td>&nbsp;@Model.{nameof(ESignDetail.FormatedDatetime)}</td>
              </tr>
            </table>
            </body>
            </html>";
            var dateFormat = this.GetString(nameof(Resources.Resource.FormatterDatetime), culture);
            var body = this.viewEngine.RunCompile(
                template,
                new ESignDetail(notification, dateFormat),
                LoadAllAssembly);

            var ret = new EMail()
            {
                Subject = this.GetString(nameof(Resources.Resource.SubjectInvalidESign), culture),
                IsHtmlBody = true,
                Body = body
            };
            return ret;
        }

        /// <summary>
        /// 批量构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notifications"></param>
        /// <returns></returns>
        public EMail BuildBatchEMail(CultureInfo culture, IEnumerable<NotificationV> notifications)
        {
            if (notifications.Count() == 1)
            {
                return this.BuildEMail(culture, notifications.First());
            }
            var template = @$"<html>
<body>
<table>
  <tr>
    <th>{this.GetString(nameof(Resources.Resource.HeaderESignAccount), culture)}</th>
    <th>{this.GetString(nameof(Resources.Resource.HeaderESignTimestamp), culture)}</th>
  </tr>
@foreach(var item in Model)
{{
  <tr>
    <td>@item.{nameof(ESignDetail.Account)}</td>
    <td>@item.{nameof(ESignDetail.FormatedDatetime)}</td>
  </tr>
}}
</table>
</body>
</html>";
            var dateFormat = this.GetString(nameof(Resources.Resource.FormatterDatetime), culture);
            var body = this.viewEngine.RunCompile(
                template,
                notifications.Select(n => new ESignDetail(n, dateFormat)).ToArray(),
                LoadAllAssembly);
            var ret = new EMail()
            {
                Subject = this.GetString(nameof(Resources.Resource.SubjectInvalidESign), culture),
                IsHtmlBody = true,
                Body = body
            };
            return ret;
        }
    }
}
