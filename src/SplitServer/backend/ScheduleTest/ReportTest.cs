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

namespace ESys.ScheduleTest
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Report;
    using Furion.DatabaseAccessor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;
    using System.Linq;

    [TestClass]
    public class ReportTest
    {
        private IServiceProvider serviceProvider = null;
        private IServiceScope scope = null;
        private IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository = null;
        private ILogger<Compliance> logger = null;
        [TestInitialize]
        public void Initialize()
        {
            this.scope = Furion.App.RootServices.CreateScope();
            this.serviceProvider = this.scope.ServiceProvider;
            this.serviceProvider.GetService<ITenantService>().SetTenantScope("Emis");
            this.msRepository = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            this.logger = this.scope.ServiceProvider.GetService<ILogger<Compliance>>();
        }
        [TestMethod]
        public void TestComplianceReport()
        {
            var compliance = new Compliance(this.scope.ServiceProvider, this.logger);
            var reportString = string.Empty;
            var flag = compliance.TryProcess(
                "compliance",
                @"{""year"":2022,""period"":4,""periodType"":2,""locations"":[1, 2, 3, 4, 5, 6, 7],""testTypes"":[2, 3, 4, 5, 6, 7, 8],""environments"":[1, 2, 5],""classifications"":[1, 2, 3, 4, 5, 6, 7]}",
                "zh-cn",
                ref reportString);
            Assert.IsTrue(flag);
        }
        /// <summary>
        /// 需要连接Emis云服务器数据库
        /// </summary>
        [TestMethod]
        public void TestReport()
        {
            var stopWatchAll = new Stopwatch();
            stopWatchAll.Start();
            //this.logger = scope.ServiceProvider.GetService<ILogger<Tests>>();
            for (int i = 0; i < 50; i++)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                this.logger.LogInformation($"Find Num:{i}, DateTimeStart {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")}");
                this.TestComplianceReport();
                stopWatch.Stop();
                this.logger.LogInformation($"Find Num:{i}, DateTimeEnd {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")}," +
                    $"All Time is{stopWatch.ElapsedMilliseconds}"); 
            }
            stopWatchAll.Stop();
            this.logger.LogInformation($"All Time is{stopWatchAll.ElapsedMilliseconds}");
        }
    }
}
