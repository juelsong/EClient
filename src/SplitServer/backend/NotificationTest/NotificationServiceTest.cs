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
    using ESys.Contract.Service;
    using ESys.UnitTest;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class NotificationServiceTest
    {
        [TestMethod]
        public async Task TestAddNotification()
        {
            using var scope = Furion.App.RootServices.CreateScope();
            scope.ServiceProvider.GetService<ITenantService>().SetTenantScope("Emis");
            var notificationService = scope.ServiceProvider.GetService<INotificationService>();
            var ret = await notificationService.AddNotification(NotificationTypes.AccountLocked, null, null, null, null, null, null, "title", "msg");
            Assert.IsTrue(ret);
        }
    }
}
