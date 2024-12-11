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

namespace ESys.Contract.Service
{
    using ESys.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// 通知类型枚举
    /// </summary>
    [ODataConfig]
    public enum NotificationTypes
    {
        /// <summary>
        /// 偏差
        /// </summary>
        Deviation = 1,
        /// <summary>
        /// 登录失败
        /// </summary>
        LoginFailure,
        /// <summary>
        /// 无效签名
        /// </summary>
        InvalidESig,
        /// <summary>
        /// 账户被锁定
        /// </summary>
        AccountLocked,
        /// <summary>
        /// 采样未完成
        /// </summary>
        SampleNotCompleted,
        /// <summary>
        /// 周测试未完成
        /// </summary>
        WeeklyTestNotCompleted,
        /// <summary>
        /// 月测试未完成
        /// </summary>
        MonthlyTestNotCompleted,
        /// <summary>
        /// 季度测试未完成
        /// </summary>
        QuarterlyTestNotCompleted,
        /// <summary>
        /// Max Time About to Exceed
        /// </summary>
        MaxTimeAboutToExceed,
        /// <summary>
        /// Work Incomplete
        /// </summary>
        WorkNotYetCompletedForToday,
        /// <summary>
        /// Organism Found
        /// </summary>
        OrganismFound,
        /// <summary>
        /// Organism Added To Organism Dictionary
        /// </summary>
        OrganismAdded,
        /// <summary>
        /// Workflow Error
        /// </summary>
        WorkflowError,
        /// <summary>
        /// Equipment About to Expire
        /// </summary>
        EquipmentAboutToExpire,
        /// <summary>
        /// Media Inventory is Low
        /// </summary>
        MediaInventoryLow,
        /// <summary>
        /// User Qualification Lapsed
        /// </summary>
        UserQualificationLapsed,
        /// <summary>
        /// User Qualification Coming Due
        /// </summary>
        UserQualificationDue
    }

    /// <summary>
    /// 通知服务
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="notificationTypeName"></param>
        /// <param name="userId"></param>
        /// <param name="siteId"></param>

        /// <param name="equipmentId"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        Task<bool> AddNotification(
            NotificationTypes notificationTypeName,
            int? userId,
            int? siteId,
            int? equipmentId,
            params string[] msgs);
    }
}
