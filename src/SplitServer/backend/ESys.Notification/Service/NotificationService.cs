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
    using Furion.DatabaseAccessor;
    using Furion.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// 通知服务
    /// </summary>
    public class NotificationService : INotificationService, ITransient
    {
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> repo;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repo"></param>
        public NotificationService(IMSRepository<TenantMasterLocator, TenantSlaveLocator> repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        /// <param name="notificationTypeName"></param>
        /// <param name="userId"></param>
        /// <param name="siteId"></param>
        /// <param name="equipmentId"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        public async Task<bool> AddNotification(
            NotificationTypes notificationTypeName,
            int? userId,
            int? siteId,
            int? equipmentId,
            params string[] msgs)
        {
            //  Moda.BusinessObjects.dll->Moda.BusinessObjects.DataEntityHelper->public static void AddNotification(IRepository repository, NotificationTypes notificationTypeName, int? userId, int? siteId, int? sampleId, int? deviationId, int? equipmentId, int? mediaId, string title, string message, string username)
            var type = await this.repo.Slave1<NotificationType>().FirstOrDefaultAsync(nt => nt.Type == notificationTypeName);
            if (type == null)
            {
                throw new NotImplementedException();
            }
            var notification = new Notification()
            {
                UserId = userId,
                SiteId = siteId,
                EquipmentId = equipmentId,
                Messages = msgs,
                NotificationTypeId = type.Id
            };
            await this.repo.Master<Notification>().InsertNowAsync(notification);
            return true;
        }
    }
}
