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
    using ESys.Notification.Entity;
    using ESys.Notification.Service;
    using ESys.UnitTest;
    using ESys.Utilty.Defs;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Globalization;
    using System.Linq;

    [TestClass]
    public class EMailBuilderTest
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("notificationtestsettings.json");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }

        private static void TestBuildEMail(NotificationTypes notificationType, string[] msgs)
        {
            using var scope = Furion.App.RootServices.CreateScope();
            var builder = scope.ServiceProvider
                .GetServices<IEMailBuilder>()
                .FirstOrDefault(b => b.ProcessedType == notificationType);
            Assert.IsNotNull(builder);
            var nv = new NotificationV()
            {
                CreatedTime = DateTimeOffset.Now,
                Messages = msgs
            };
            var cultureInfo = new CultureInfo("zh-cn");
            var email = builder.BuildEMail(cultureInfo, nv);
            Assert.IsNotNull(email);
            Assert.IsTrue(email.IsHtmlBody);
            Assert.IsTrue(!string.IsNullOrEmpty(email.Subject));
            Assert.IsTrue(!string.IsNullOrEmpty(email.Body));
            email = builder.BuildBatchEMail(cultureInfo, new NotificationV[] { nv, nv });
            Assert.IsTrue(email.IsHtmlBody);
            Assert.IsTrue(!string.IsNullOrEmpty(email.Subject));
            Assert.IsTrue(!string.IsNullOrEmpty(email.Body));
        }

        [TestMethod]
        public void TestBuildInvalidESigEMail()
        {
            TestBuildEMail(
                NotificationTypes.InvalidESig,
                new string[] { "Admin", "{'property':'string'}" }
                );
        }

        [TestMethod]
        public void TestBuildLoginFailureEMail()
        {
            TestBuildEMail(
                NotificationTypes.LoginFailure,
                new string[] { "Admin", $"{ErrorCode.User.WrongPassword}" }
                );
        }

        [TestMethod]
        public void TestBuildAccountLockedEMail()
        {
            TestBuildEMail(
                NotificationTypes.AccountLocked,
                new string[] { "Admin", $"{100}" }
                );
        }
        [TestMethod]
        public void TestCurrentworkspaceNotCompletedEMail()
        {
            TestBuildEMail(
                NotificationTypes.AccountLocked,
                new string[] { "Admin", $"{100}" }
                );
        }
    }
}
