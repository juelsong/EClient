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
    using ESys.Contract.Service;
    using ESys.Notification.Entity;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// email构建器
    /// </summary>
    public interface IEMailBuilder
    {
        /// <summary>
        /// 可处理的通知类型
        /// </summary>
        NotificationTypes ProcessedType { get; }

        /// <summary>
        /// 构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notification"></param>
        /// <returns></returns>
        EMail BuildEMail(CultureInfo culture, NotificationV notification);

        /// <summary>
        /// 批量构建email
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="notifications"></param>
        /// <returns></returns>
        EMail BuildBatchEMail(CultureInfo culture, IEnumerable<NotificationV> notifications);
    }
}
