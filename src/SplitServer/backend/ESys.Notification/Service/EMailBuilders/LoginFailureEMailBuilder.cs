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
    using ESys.Utilty.Defs;
    using Furion.DependencyInjection;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 登录失败email构建器
    /// NotificationV信息为用户账户、错误码字符串
    /// </summary>
    public class LoginFailureEMailBuilder : EMailBuilderBase, ITransient, IEMailBuilder
    {
        /// <summary>
        /// 登录失败详情
        /// </summary>
        public class LoginFailureDetail
        {
            /// <summary>
            /// IViewEngine 泛型方法会抛异常，禁止使用new操作符
            /// </summary>
            internal LoginFailureDetail() { }
            /// <summary>
            /// 登录失败的账号
            /// </summary>
            public string Account { get; set; }
            /// <summary>
            /// 签名的内容
            /// </summary>
            public string ErrorPrompt { get; set; }
            /// <summary>
            /// 日期格式
            /// </summary>
            public string FormatedDatetime { get; set; }
        }

        /// <summary>
        /// key:ErrorCode.User值字符串，value ErrorCode.User属性名
        /// </summary>
        private static Dictionary<string, string> ErrorCodeDic = typeof(ErrorCode.User)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .ToDictionary(f => f.GetValue(null).ToString(), f => f.Name);

        /// <summary>
        /// 可处理的通知类型
        /// </summary>
        public NotificationTypes ProcessedType => NotificationTypes.LoginFailure;

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
    <th>{this.GetString(nameof(Resources.Resource.HeaderLoginFailureAccount), culture)}</th>
    <th>{this.GetString(nameof(Resources.Resource.HeaderLoginFailureTimestamp), culture)}</th>
    <th>{this.GetString(nameof(Resources.Resource.HeaderLoginFailureMessage), culture)}</th>
  </tr>
@foreach(var item in Model)
{{
  <tr>
    <td>@item.{nameof(LoginFailureDetail.Account)}</td>
    <td>@item.{nameof(LoginFailureDetail.FormatedDatetime)}</td>
    <td>@item.{nameof(LoginFailureDetail.ErrorPrompt)}</td>
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
                Subject = this.GetString(nameof(Resources.Resource.SubjectLoginFailure), culture),
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
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderLoginFailureAccount), culture)}:</td>
                <td>&nbsp;@Model.{nameof(LoginFailureDetail.Account)}</td>
              </tr>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderLoginFailureTimestamp), culture)}:</td>
                <td>&nbsp;@Model.{nameof(LoginFailureDetail.FormatedDatetime)}</td>
              </tr>
              <tr>
                <td align=""right:"">{this.GetString(nameof(Resources.Resource.HeaderLoginFailureMessage), culture)}:</td>
                <td>&nbsp;@Model.{nameof(LoginFailureDetail.ErrorPrompt)}</td>
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
                Subject = this.GetString(nameof(Resources.Resource.SubjectLoginFailure), culture),
                IsHtmlBody = true,
                Body = body
            };
            return ret;
        }

        private LoginFailureDetail GetDetail(NotificationV notification, CultureInfo culture)
        {
            var dateFormat = this.GetString(nameof(Resources.Resource.FormatterDatetime), culture);
            var ret = new LoginFailureDetail()
            {
                FormatedDatetime = notification.CreatedTime.LocalDateTime.ToString(dateFormat)
            };
            var msg = notification.Messages;
            if (msg != null && msg.Length == 2)
            {
                ret.Account = msg[0];
                ret.ErrorPrompt = this.GetString($"MessageLoginFailure{ErrorCodeDic[msg[1]]}", culture);
            }
            return ret;
        }
    }
}