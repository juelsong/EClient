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

namespace ESys.NotificationTest
{
    using ESys.Contract.Db;
    using ESys.Contract.Entity;
    using ESys.Contract.Service;
    using ESys.Controllers;
    using ESys.Notification.Entity;
    using ESys.Notification.ODataControllers;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class DeactivateTest
    {
        //[TestMethod]
        //public void TestNotificationTypeDeactiveChecker()
        //{
        //    using var scope = Furion.App.RootServices.CreateScope();
        //    scope.ServiceProvider.GetService<ITenantService>().SetTenantScope("Seagull");
        //    var notificationTypes = scope.ServiceProvider
        //        .GetService<IMSRepository<MasterDbContextLocator, TenantSlaveLocator>>()
        //        .Slave1<NotificationType>();
        //    var checker = scope.ServiceProvider.GetService<IDeactiveChecker<NotificationType>>();
        //    var ret = checker.CheckCanDeactive(notificationTypes.AsQueryable(), 1);
        //}

        //[TestMethod]
        //public void TestNotificationDeactiveChecker()
        //{
        //    using var scope = Furion.App.RootServices.CreateScope();
        //    var checker = scope.ServiceProvider.GetService<IDeactiveChecker<Notification>>();
        //    Assert.IsNull(checker);
        //}

        [TestMethod]
        public void TestDeactiveNotificationType()
        {
            var notificationType = new NotificationType()
            {
                Id = 1,
                IsActive = false
            };
            using var scope = Furion.App.RootServices.CreateScope();
            scope.ServiceProvider.GetService<ITenantService>().SetTenantScope("Seagull");
            var provider = scope.ServiceProvider;
            var controller = new NotificationTypeController(
                provider.GetService<ILogger<NotificationTypeController>>(),
                provider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>()
                );

            var ret = controller.Patch(1, new(notificationType, new string[] { nameof(IActiveEntity.IsActive) }));

            if (ret is Result<DeactivateAgainst[]> againsts)
            {
                Assert.IsNotNull(againsts);
                Assert.IsNotNull(againsts.Data);
                Assert.IsTrue(againsts.Data.Length > 0);
            }
            else if (ret is Result boolResult)
            {
                Assert.IsTrue(boolResult.Success);
            }
            else
            {
                Assert.Fail($"wrong JsonResult type:{ret.GetType()}");
            }
        }
    }
}
