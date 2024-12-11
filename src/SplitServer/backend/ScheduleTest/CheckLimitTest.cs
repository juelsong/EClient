namespace ESys.ScheduleTest
{
    using ESys.Contract.Service;
    using ESys.Infrastructure.Entity;
    using ESys.Schedule.Controllers;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Service;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    using MsRepository = Furion.DatabaseAccessor.IMSRepository<ESys.Contract.Db.TenantMasterLocator, ESys.Contract.Db.TenantSlaveLocator>;

    [TestClass]
    public class CheckLimitTest
    {
        [TestMethod]
        public void TestCheckLimit()
        {
            using var scope = Furion.App.RootServices.CreateScope();
            var provider = scope.ServiceProvider;
            provider.GetService<ITenantService>().SetTenantScope("Seagull");

            var plan = TestAddPlan(provider, this.GetType().Assembly);
            TestAddCurrentWorkSpace(provider, plan, out var cwsId, out var sampleId);

            TestCheckLimit(provider, cwsId, 15.0f, false, false);
            TestCheckLimit(provider, cwsId, 20.0f, false, false);
            TestCheckLimit(provider, cwsId, 25.0f, true, false);

            TestDelete(provider, plan, cwsId, sampleId);
        }

        private static Plan TestAddPlan(IServiceProvider serviceProvider, System.Reflection.Assembly ass)
        {
            using var reader = new StreamReader(ass.GetManifestResourceStream($"{ass.GetName().Name}.Appendix.AddPlan.json"));
            var jsonStr = reader.ReadToEnd();
            var plan = JsonSerializer.Deserialize<Plan>(jsonStr);

            var planController = new PlanController(
                serviceProvider.GetService<ILogger<PlanController>>(),
                serviceProvider,
                serviceProvider.GetService<MsRepository>());

            var result = planController.AddPlan(plan);
            Assert.IsTrue(result.Success);

            return result.Data;
        }

        private static void TestAddCurrentWorkSpace(IServiceProvider serviceProvider, Plan plan, out long cwsId, out long sampleId)
        {
            var msRepository = serviceProvider.GetService<MsRepository>();

            Assert.IsNotNull(plan);
            Assert.IsNotNull(plan.Tests);
            Assert.IsTrue(plan.Tests.Any());

            var testId = plan.Tests.First().Id;
            var tests = ScheduleService.GetTests(msRepository)
                .Where(test => test.Id == testId);
            Assert.IsTrue(tests.Any());

            var now = DateTimeOffset.Now;
            var cws = ScheduleService.TestToCurrentWorkSpaces(tests.First(), now, now);
            ScheduleService.InsertCurrentWorkSpace(msRepository, cws, out cwsId, out sampleId);
        }

        private static void TestCheckLimit(IServiceProvider serviceProvider, long cwsId, float value, bool isLimitValid, bool isFreqLimitValid)
        {
            var limitData = new LimitData()
            {
                CwsId = (int)cwsId,
                ResultDatas = new ResultData[] { new ResultData() { MeasurementId = 1, Value = (decimal)value } }
            };

            var resultentryController = new ResultEntryController(
                serviceProvider.GetService<ILogger<ResultEntryController>>(),
                serviceProvider,
                serviceProvider.GetService<MsRepository>(),
                serviceProvider.GetService<INotificationService>());

            var result = resultentryController.CheckLimit(limitData);
            Assert.IsTrue(result.Success);

            var limitResult = result.Data;
            Assert.AreEqual(limitResult.LimitId > 0, isLimitValid);
            Assert.AreEqual(limitResult.FreqLimitId > 0, isFreqLimitValid);
        }

        private static void TestDelete(IServiceProvider serviceProvider, Plan plan, long cwsId, long sampleId)
        {
            var msRepository = serviceProvider.GetService<MsRepository>();

            if (cwsId > 0)
            {
                msRepository.Master<CurrentWorkSpace>().DeleteNow(cwsId);
                msRepository.Master<Sample>().DeleteNow(sampleId);
            }

            foreach (var test in plan.Tests)
            {
                var limits = test.Limits;
                foreach (var limit in limits)
                {
                    if (limit.LimitRuleGroups != null)
                    {
                        foreach (var group in limit.LimitRuleGroups)
                        {
                            TestDeleteLimitGroups(msRepository, group);
                        }
                    }
                }
                TestDeleteLimits(msRepository, limits, true);
                TestDeleteLimits(msRepository, limits, false);
                msRepository.Master<Test>().DeleteNow(test.Id);
            }
            msRepository.Master<Plan>().DeleteNow(plan.Id);
        }

        private static void TestDeleteLimits(MsRepository msRepository, ICollection<Limit> limits, bool hasParent)
        {
            limits.Where(limit => hasParent == (limit.ParentLimitId != null))
                .ToList()
                .ForEach(limit => msRepository.Master<Limit>().DeleteNow(limit.Id));
        }

        private static void TestDeleteLimitGroups(MsRepository msRepository, LimitRuleGroup group)
        {
            if (group.LimitRules != null)
            {
                foreach (var limitRule in group.LimitRules)
                {
                    msRepository.Master<LimitRule>().DeleteNow(limitRule.Id);
                }
            }

            if (group.LimitRuleGroups != null)
            {
                foreach (var limitGroup in group.LimitRuleGroups)
                {
                    TestDeleteLimitGroups(msRepository, limitGroup);
                }
            }

            msRepository.Master<LimitRuleGroup>().DeleteNow(group.Id);
        }
    }
}
