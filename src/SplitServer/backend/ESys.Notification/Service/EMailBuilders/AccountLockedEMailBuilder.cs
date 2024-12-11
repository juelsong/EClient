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
    /// 账户锁定email构建器
    /// NotificationV信息为用户账户，尝试次数
    /// </summary>
    public class AccountLockedEMailBuilder : EMailBuilderBase, ITransient, IEMailBuilder
    {
        /// <summary>
        /// 锁定详情
        /// </summary>
        public class AccountLockedDetail
        {
            /// <summary>
            /// IViewEngine 泛型方法会抛异常，禁止使用new操作符
            /// </summary>
            internal AccountLockedDetail() { }
            /// <summary>
            /// 锁定的账号
            /// </summary>
            public string Account { get; set; }
            /// <summary>
            /// 尝试次数
            /// </summary>
            public int? AttamptCount { get; set; }
            /// <summary>
            /// 日期格式
            /// </summary>
            public string FormatedDatetime { get; set; }
        }

        /// <summary>
        /// 可处理的通知类型
        /// </summary>
        public NotificationTypes ProcessedType => NotificationTypes.AccountLocked;

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
    <th>{this.GetString(nameof(Resources.Resource.HeaderAccountLockedAccount), culture)}</th>
    <th>{this.GetString(nameof(Resources.Resource.HeaderAccountLockedTimestamp), culture)}</th>
    <th>{this.GetString(nameof(Resources.Resource.HeaderAccountLockedAttamptCount), culture)}</th>
  </tr>
@foreach(var item in Model)
{{
  <tr>
    <td>@item.{nameof(AccountLockedDetail.Account)}</td>
    <td>@item.{nameof(AccountLockedDetail.FormatedDatetime)}</td>
    <td>@item.{nameof(AccountLockedDetail.AttamptCount)}</td>
  </tr>
}}
</table>
</body>
</html>";
            var body = this.viewEngine.RunCompile(
                template,
                notifications.Select(n => this.GetDetail(n, culture)).ToArray(),
                LoadAllAssembly);
            var ret = new EMail()
            {
                Subject = this.GetString(nameof(Resources.Resource.SubjectAccountLocked), culture),
                IsHtmlBody = true,
                Body = body
            };
            return ret;
        }

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
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderAccountLockedAccount), culture)}:</td>
                <td>&nbsp;@Model.{nameof(AccountLockedDetail.Account)}</td>
              </tr>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderAccountLockedTimestamp), culture)}:</td>
                <td>&nbsp;@Model.{nameof(AccountLockedDetail.FormatedDatetime)}</td>
              </tr>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderAccountLockedAttamptCount), culture)}:</td>
                <td>&nbsp;@Model.{nameof(AccountLockedDetail.AttamptCount)}</td>
              </tr>
            </table>
            </body>
            </html>";
            var body = this.viewEngine.RunCompile(
                template,
                this.GetDetail(notification, culture),
                LoadAllAssembly);

            var ret = new EMail()
            {
                Subject = this.GetString(nameof(Resources.Resource.SubjectAccountLocked), culture),
                IsHtmlBody = true,
                Body = body
            };
            return ret;
        }

        private AccountLockedDetail GetDetail(NotificationV notification, CultureInfo culture)
        {
            var dateFormat = this.GetString(nameof(Resources.Resource.FormatterDatetime), culture);
            var ret = new AccountLockedDetail()
            {
                FormatedDatetime = notification.CreatedTime.LocalDateTime.ToString(dateFormat)
            };
            var msg = notification.Messages;
            if (msg != null && msg.Length == 2)
            {
                ret.Account = msg[0];
                if (int.TryParse(msg[1], out var cnt))
                {
                    ret.AttamptCount = cnt;
                }
            }
            return ret;
        }
    }
}
