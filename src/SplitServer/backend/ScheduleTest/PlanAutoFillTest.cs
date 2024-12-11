/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
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
    using ESys.Db.DbContext;
    using ESys.Infrastructure.Entity;
    using ESys.Schedule.Controllers;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Service;
    using ESys.Security.Entity;
    using ESys.UnitTest;
    using DocumentFormat.OpenXml.Office.CustomUI;
    using Furion.DatabaseAccessor;
    using Furion.TimeCrontab;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Text.Json;
    using System.Text.Unicode;
    using System.Threading;
    using System.Threading.Tasks;
    using MsRepository = Furion.DatabaseAccessor.IMSRepository<ESys.Contract.Db.TenantMasterLocator, ESys.Contract.Db.TenantSlaveLocator>;

    [TestClass]
    public class PlanAutoFillTest
    {
        public static IEnumerable<object[]> HourlyPlan
        {
            get
            {
                return Enumerable.Range(1, 100)
                        .Union(Enumerable.Range((int)Math.Pow(2, 24) - 1 - 100, 100))
                        .SelectMany(h => Enumerable.Range(0, 1)
                        .Select(m => new object[] { h, m }));
            }
        }
        public static IEnumerable<object[]> DailyPlan
        {
            get
            {
                return Enumerable.Range(0, 24 * 60)
                        .SelectMany(v => Enumerable.Range(1, 31)
                                        .Select(w => new object[] { v, w }));
            }
        }
        public static IEnumerable<object[]> WeeklyPlan
        {
            get
            {
                return Enumerable.Range(0, 24 * 60)
                        .SelectMany(v => Enumerable.Range(0, 7)
                                        .Select(w => new object[] { v, w }));
            }
        }
        public static IEnumerable<object[]> MonthlyPlan
        {
            get
            {
                return Enumerable.Range(0, 24 * 60)
                        .SelectMany(v => Enumerable.Range(1, 31)
                                        .Select(w => new object[] { v, w }));
            }
        }
        private static readonly TimeSpan tsOffset = TimeSpan.FromHours(8);
        #region Cron
        //[TestMethod]
        //[DynamicData(nameof(HourlyPlan))]
        //public void TestHourlyPlan(int fillHoursOfDay, int fillMinutesOfHour)
        //{
        //    var plan = new Plan()
        //    {
        //        AutoFillFrequencyTypeId = (int)FrequencyType.Hourly,
        //        FillMinutesOfHour = fillMinutesOfHour,
        //        FillHoursOfDay = fillHoursOfDay
        //    };
        //    var cronStr = PlanAutoFillJob.CreateCron(plan);
        //    var cron = Crontab.TryParse(cronStr, CronStringFormat.Default);
        //    Assert.IsNotNull(cron);
        //    var today = DateTime.Today.AddSeconds(-1);
        //    var tomorrow = today.AddDays(1);
        //    var triggered = cron.GetNextOccurrences(today, tomorrow).ToArray();
        //    var hoursCnt = 0;
        //    if (fillHoursOfDay > 0)
        //    {
        //        var cnt = 0;
        //        while (cnt < 32)
        //        {
        //            if ((plan.FillHoursOfDay & (1 << (cnt++))) > 0)
        //            {
        //                hoursCnt++;
        //            }
        //        }
        //    }
        //    Assert.AreEqual(triggered.Length, hoursCnt);
        //    Assert.IsTrue(triggered.All(dt => dt.Minute == plan.FillMinutesOfHour));
        //    for (var i = 0; i < triggered.Length; i++)
        //    {
        //        Assert.AreEqual((plan.FillHoursOfDay >> triggered[i].Hour) & 1, 1);
        //    }
        //}

        //[TestMethod]
        //[DynamicData(nameof(DailyPlan))]
        //public void TestDailyPlan(int fillTime, int days)
        //{
        //    var plan = new Plan()
        //    {
        //        AutoFillFrequencyTypeId = (int)FrequencyType.Daily,
        //        FillTime = fillTime,
        //    };
        //    var cronStr = PlanAutoFillJob.CreateCron(plan);
        //    var cron = Crontab.TryParse(cronStr, CronStringFormat.Default);
        //    Assert.IsNotNull(cron);
        //    var today = DateTime.Today.AddSeconds(-1);
        //    var firstTriggered = DateTime.Today.AddHours(fillTime / 60).AddMinutes(fillTime % 60);
        //    var nextWeek = today.AddDays(days);
        //    var triggerd = cron.GetNextOccurrences(today, nextWeek).ToArray();
        //    Assert.AreEqual(triggerd.Length, days);
        //    Assert.IsTrue(triggerd.All(dt => dt.Minute == fillTime % 60));
        //    Assert.IsTrue(triggerd.All(dt => dt.Hour == fillTime / 60));
        //    Assert.AreEqual(triggerd[0], firstTriggered);
        //    for (var i = 0; i < triggerd.Length; i++)
        //    {
        //        var sub = triggerd[i] - firstTriggered;
        //        Assert.AreEqual(sub.TotalDays, i);
        //        Assert.AreEqual(sub.Hours, 0);
        //        Assert.AreEqual(sub.Minutes, 0);
        //        Assert.AreEqual(sub.Seconds, 0);
        //        Assert.AreEqual(sub.Milliseconds, 0);
        //    }
        //}

        //[TestMethod]
        //[DynamicData(nameof(WeeklyPlan))]
        //public void TestWeeklyPlan(int fillTime, int dayOfWeek)
        //{
        //    var plan = new Plan()
        //    {
        //        AutoFillFrequencyTypeId = (int)FrequencyType.Weekly,
        //        FillTime = fillTime,
        //        FillDayOfWeek = dayOfWeek
        //    };
        //    var cronStr = PlanAutoFillJob.CreateCron(plan);
        //    var cron = Crontab.TryParse(cronStr, CronStringFormat.Default);
        //    Assert.IsNotNull(cron);
        //    var today = DateTime.Today.AddSeconds(-1);
        //    var nextYear = today.AddYears(1);
        //    var triggerd = cron.GetNextOccurrences(today, nextYear).ToArray();
        //    Assert.IsTrue(triggerd.All(dt => dt.Minute == fillTime % 60));
        //    Assert.IsTrue(triggerd.All(dt => dt.Hour == fillTime / 60));
        //    var firstTriggered = triggerd[0];
        //    for (var i = 0; i < triggerd.Length; i++)
        //    {
        //        var sub = triggerd[i] - firstTriggered;
        //        Assert.AreEqual(sub.TotalDays, i * 7);
        //        Assert.AreEqual(sub.Hours, 0);
        //        Assert.AreEqual(sub.Minutes, 0);
        //        Assert.AreEqual(sub.Seconds, 0);
        //        Assert.AreEqual(sub.Milliseconds, 0);
        //    }
        //}

        //[TestMethod]
        //[DynamicData(nameof(MonthlyPlan))]
        //public void TestMonthlyPlan(int fillTime, int dayOfMonth)
        //{
        //    var plan = new Plan()
        //    {
        //        AutoFillFrequencyTypeId = (int)FrequencyType.Monthly,
        //        FillTime = fillTime,
        //        FillDayOfMonth = dayOfMonth
        //    };
        //    var cronStr = PlanAutoFillJob.CreateCron(plan);
        //    var cron = Crontab.TryParse(cronStr, CronStringFormat.Default);
        //    Assert.IsNotNull(cron);
        //    var today = DateTime.Today.AddSeconds(-1);
        //    var nextNear = DateTime.Today.AddYears(1);
        //    var triggerd = cron.GetNextOccurrences(today, nextNear).ToArray();
        //    Assert.IsTrue(triggerd.All(dt => dt.Minute == fillTime % 60));
        //    Assert.IsTrue(triggerd.All(dt => dt.Hour == fillTime / 60));
        //    Assert.IsTrue(triggerd.All(dt => dt.Day == dayOfMonth));
        //    var firstTriggered = triggerd[0];
        //    for (var i = 0; i < triggerd.Length; i++)
        //    {
        //        var sub = triggerd[i] - firstTriggered;
        //        Assert.AreEqual(sub.Hours, 0);
        //        Assert.AreEqual(sub.Minutes, 0);
        //        Assert.AreEqual(sub.Seconds, 0);
        //        Assert.AreEqual(sub.Milliseconds, 0);
        //    }
        //}
        #endregion Cron
        [TestMethod]
        [DataRow(7, 31, 1, 2022, 1, 1, 1, 0, 10)]
        [DataRow(16777215, 31, 1, 2022, 1, 1, 23, 10, 10)]
        public void TestHourlyTimeRange(
            int fillHoursOfDay,
            int fillMinutesOfHour,
            int fillLength,
            int nowYear,
            int nowMonth,
            int nowDay,
            int nowHour,
            int nowMinute,
            int nowSecond)
        {
            var plan = new Plan()
            {
                AutoFillFrequencyTypeId = (int)FrequencyType.Hourly,
                FillHoursOfDay = fillHoursOfDay,
                FillMinutesOfHour = fillMinutesOfHour,
                FillLength = fillLength
            };
            var now = new DateTimeOffset(nowYear, nowMonth, nowDay, nowHour, nowMinute, nowSecond, tsOffset);
            var range = PlanAutoFillJob.CaculateTimeRange(plan, now);
            var hours = PlanAutoFillJob.GetHours(fillHoursOfDay);
            if (hours.Contains(nowHour))
            {
                Assert.IsNotNull(range.Start);
                Assert.IsNotNull(range.End);
                Assert.AreEqual(range.Start.Value.Year, nowYear);
                Assert.AreEqual(range.Start.Value.Month, nowMonth);
                Assert.AreEqual(range.Start.Value.Day, nowDay);
                Assert.AreEqual(range.Start.Value.Hour, nowHour);
                Assert.AreEqual(range.Start.Value.Minute, fillMinutesOfHour);
                Assert.AreEqual(range.Start.Value.Second, 0);
                Assert.AreEqual(range.Start.Value.Millisecond, 0);
                var ts = range.End.Value - range.Start.Value;
                Assert.AreEqual(ts.TotalMinutes, fillLength * 60);
            }
            else
            {
                Assert.IsNull(range.Start);
                Assert.IsNull(range.End);
            }
        }

        [TestMethod]
        [DataRow(31, 1, 2022, 1, 1, 1, 0, 10)]
        [DataRow(31, 1, 2022, 1, 1, 23, 10, 10)]
        public void TestDailyTimeRange(
            int fillTime,
            int fillLength,
            int nowYear,
            int nowMonth,
            int nowDay,
            int nowHour,
            int nowMinute,
            int nowSecond)
        {
            var plan = new Plan()
            {
                AutoFillFrequencyTypeId = (int)FrequencyType.Daily,
                FillTime = fillTime,
                FillLength = fillLength
            };
            var now = new DateTimeOffset(nowYear, nowMonth, nowDay, nowHour, nowMinute, nowSecond, tsOffset);
            var range = PlanAutoFillJob.CaculateTimeRange(plan, now);
            Assert.IsNotNull(range.Start);
            Assert.IsNotNull(range.End);
            Assert.AreEqual(range.Start.Value.Year, nowYear);
            Assert.AreEqual(range.Start.Value.Month, nowMonth);
            Assert.AreEqual(range.Start.Value.Day, nowDay);
            Assert.AreEqual(range.Start.Value.Hour, fillTime / 60);
            Assert.AreEqual(range.Start.Value.Minute, fillTime % 60);
            Assert.AreEqual(range.Start.Value.Second, 0);
            Assert.AreEqual(range.Start.Value.Millisecond, 0);
            var ts = range.End.Value - range.Start.Value;
            Assert.AreEqual(ts.TotalMinutes, fillLength * 24 * 60 - fillTime);
        }


        [TestMethod]
        [DataRow(31, 1, DayOfWeek.Monday, DayOfWeek.Sunday, 2022, 1, 1, 1, 0, 10)]
        [DataRow(31, 1, DayOfWeek.Monday, DayOfWeek.Sunday, 2022, 1, 1, 23, 10, 10)]
        public void TestWeeklyTimeRange(
            int fillTime,
            int fillLength,
            DayOfWeek fillDayOfWeek,
            DayOfWeek firstDayOfWeek,
            int nowYear,
            int nowMonth,
            int nowDay,
            int nowHour,
            int nowMinute,
            int nowSecond)
        {
            var plan = new Plan()
            {
                AutoFillFrequencyTypeId = (int)FrequencyType.Weekly,
                FillTime = fillTime,
                FillLength = fillLength,
                FillDayOfWeek = (int)fillDayOfWeek,
                FirstDayOfWeek = (int)firstDayOfWeek
            };
            var now = new DateTimeOffset(nowYear, nowMonth, nowDay, nowHour, nowMinute, nowSecond, tsOffset);
            var range = PlanAutoFillJob.CaculateTimeRange(plan, now);
            Assert.IsNotNull(range.Start);
            Assert.IsNotNull(range.End);
            Assert.IsTrue(range.Start.Value < now && range.End.Value > now);
            Assert.AreEqual(range.Start.Value.DayOfWeek, fillDayOfWeek);
            Assert.AreEqual(range.Start.Value.Hour, fillTime / 60);
            Assert.AreEqual(range.Start.Value.Minute, fillTime % 60);
            Assert.AreEqual(range.Start.Value.Second, 0);
            Assert.AreEqual(range.Start.Value.Millisecond, 0);
            var ts = range.End.Value - range.Start.Value;
            Assert.AreEqual(ts.TotalMinutes, fillLength * 7 * 24 * 60 - fillTime);
        }

        [TestMethod]
        [DataRow(31, 1, 31, 2022, 1, 1, 1, 0, 10)]
        [DataRow(31, 1, 1, 2022, 1, 13, 23, 10, 10)]
        [DataRow(31, 1, 31, 2022, 2, 2, 23, 10, 10)]
        public void TestMonthlyTimeRange(
            int fillTime,
            int fillLength,
            int fillDayOfMonth,
            int nowYear,
            int nowMonth,
            int nowDay,
            int nowHour,
            int nowMinute,
            int nowSecond)
        {
            var plan = new Plan()
            {
                AutoFillFrequencyTypeId = (int)FrequencyType.Monthly,
                FillTime = fillTime,
                FillLength = fillLength,
                FillDayOfMonth = fillDayOfMonth
            };
            var now = new DateTimeOffset(nowYear, nowMonth, nowDay, nowHour, nowMinute, nowSecond, tsOffset);
            var range = PlanAutoFillJob.CaculateTimeRange(plan, now);

            if (DateTime.DaysInMonth(now.Year, now.Month) < fillDayOfMonth)
            {
                Assert.IsNull(range.Start);
                Assert.IsNull(range.End);
            }
            else
            {
                Assert.IsNotNull(range.Start);
                Assert.IsNotNull(range.End);
                Assert.IsTrue(range.End.Value > now);
                Assert.AreEqual(range.Start.Value.Day, fillDayOfMonth);
                Assert.AreEqual(range.Start.Value.Hour, fillTime / 60);
                Assert.AreEqual(range.Start.Value.Minute, fillTime % 60);
                Assert.AreEqual(range.Start.Value.Second, 0);
                Assert.AreEqual(range.Start.Value.Millisecond, 0);
                var ts = range.End.Value - range.Start.Value;
                var totoalDay = Enumerable.Range(nowMonth, fillLength)
                    .Select(m => new DateTime(nowYear, 1, 1).AddMonths(m - 1))
                    .Sum(d => DateTime.DaysInMonth(d.Year, d.Month)) - fillDayOfMonth;
                Assert.IsTrue(ts.TotalDays > totoalDay);
                Assert.IsTrue(ts.TotalDays < totoalDay + 1);
            }
        }

        [TestMethod]
        [DataRow(63L)]
        public async Task TestCreateCurrentWorkSpace(long planId)
        {
            using var scope = UnitTestContext.Instance.ServiceProvider.CreateScope();
            var job = new PlanAutoFillJob(scope.ServiceProvider);
            var param = new PlanAutoFillJobParameter()
            {
                TenantCode = "Emis",
                PlanId = planId,
            };
            //2023-06-19 11:24:57,366
            var now = new DateTimeOffset(2023, 6, 25, 10, 18, 40, TimeSpan.FromHours(8));
            var result = await job.ExecuteCoreAsync(param, now, CancellationToken.None);

            now = new DateTimeOffset(2023, 6, 25, 12, 01, 00, TimeSpan.FromHours(8));
            result = await job.ExecuteCoreAsync(param, now, CancellationToken.None);


            now = new DateTimeOffset(2023, 6, 26, 20, 01, 00, TimeSpan.FromHours(8));
            result = await job.ExecuteCoreAsync(param, now, CancellationToken.None);
        }



        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("scheduletestsettings.json");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }

        [TestMethod]
        [DataRow(63L)]
        public void TestCaculateCurrentWorkSpace(long planId)
        {
            //start=2023-05-28T16:00:00.000Z&end=2023-07-02T16:00:00.000Z
            using var scope = UnitTestContext.Instance.ServiceProvider.CreateScope();
            var tenantService = scope.ServiceProvider.GetService<ITenantService>();
            tenantService.SetTenantScope("Emis");
            var repo = scope.ServiceProvider.GetRequiredService<MsRepository>();

            //var plan = repo.Slave1<Plan>().Find(planId);
            //var sss = PlanAutoFillJob.CreateCron(plan);

            var start = new DateTimeOffset(2023, 5, 29, 0, 0, 0, TimeSpan.FromHours(8));
            var stop = new DateTimeOffset(2023, 7, 2, 0, 0, 0, TimeSpan.FromHours(8));
            var cws = PlanAutoFillJob.CaculatePlan(null, repo, planId, start, stop).Select(c => c.ScheduledDate.ToLocalTime().DateTime).ToList();
        }


        [TestMethod]
        public async Task TestInsertPlan()
        {
            using var reader = new StreamReader("D:\\work\\EMIS\\doc\\plan.json");
            var str1 = reader.ReadToEnd();
            var plan = JsonSerializer.Deserialize<Plan>(str1);
            //foreach (var test in plan.Tests)
            //{
            //    test.TestFrequency = null;
            //    test.TestFrequencyType = null;
            //    test.TestType = null;
            //}
            //var str2 = JsonSerializer.Serialize(plan, new JsonSerializerOptions()
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            //    WriteIndented = true
            //});
            //{
            //    using var writer = new StreamWriter("D:\\work\\EMIS\\doc\\plan1.json");
            //    writer.Write(str2);
            //}
            using var scope = UnitTestContext.Instance.ServiceProvider.CreateScope();
            var tenantService = scope.ServiceProvider.GetService<ITenantService>();
            tenantService.SetTenantScope("Emis");
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<PlanController>>();
            var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
            var dcache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();
            var repo = scope.ServiceProvider.GetRequiredService<MsRepository>();
            var controller = new PlanController(logger, scope.ServiceProvider, repo);
            var sw = new Stopwatch();
            Task updateTestOri(Plan plan)
            {
                using var scope = Furion.App.RootServices.CreateScope();
                var provider = scope.ServiceProvider;
                provider.GetService<ITenantService>().SetTenantScope("Emis");
                provider.GetService<IDataInjector>().InjectCurrentUserId(1);
                //provider.GetService<IDataInjector>().InjectESignData(this.esign);
                var msRepository = provider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
                var newPlan = controller.AddPlan(plan).Data;
                foreach (var item in plan.Tests.Where(i => i.IsActive))
                {
                    var getTest = msRepository.Master<Test>().InsertNow(new Test()
                    {
                        PlanId = newPlan.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Interval = item.Interval,
                        MinutesOfHour = item.MinutesOfHour,
                        HoursOfDay = item.HoursOfDay,
                        DaysOfWeek = item.DaysOfWeek,
                        DaysOfMonth = item.DaysOfMonth,
                        MonthsOfYear = item.MonthsOfYear,
                        Randomize = item.Randomize,
                        NotTest = item.NotTest,
                        NegativeControl = item.NegativeControl,
                        Environment = item.Environment,
                        ScheduleStartDate = item.ScheduleStartDate,
                        ScheduleOffset = item.ScheduleOffset,
                        Price = item.Price,
                        NotificationFrequencyId = item.NotificationFrequencyId,
                        SiteId = item.SiteId,
                        TestTypeId = item.TestTypeId,
                        TestFrequencyId = item.TestFrequencyId,
                        TestFrequencyTypeId = item.TestFrequencyTypeId,
                        TestFrequencyOccurrenceId = item.TestFrequencyOccurrenceId,
                    }).Entity;
                    if (item.Limits != null)
                    {
                        foreach (var limit in item.Limits?.Where(i => !i.FreqLimit))
                        {
                            var getLimit = msRepository.Master<Limit>().InsertNow(new Limit()
                            {
                                LimitDefId = limit.LimitDefId,
                                LimitTypeId = limit.LimitTypeId,
                                TestId = getTest.Id,
                                SiteId = item.SiteId,
                                Prevalence = limit.Prevalence,
                                Description = limit.Description,
                                FreqLimit = false,
                                Deviation = limit.Deviation,
                                EmailNotify = limit.EmailNotify,
                                ScreenNotify = limit.ScreenNotify,
                                Reschedule = limit.Reschedule,
                                RescheduleCount = limit.RescheduleCount,
                                CountTowardFrequency = limit.CountTowardFrequency,
                                ExecutePerCycle = limit.ExecutePerCycle,
                                ReportableOperator = limit.ReportableOperator,
                                ReportableValue = limit.ReportableValue,
                            }).Entity;
                            foreach (var group in limit.LimitRuleGroups)
                            {
                                // await controller.InsertLimitGroups(getLimit.Id, group, msRepository).ConfigureAwait(false);
                            }
                        }
                        foreach (var limit in item.Limits?.Where(i => i.FreqLimit))
                        {
                            var parentLimit = msRepository.Slave1<Limit>().FirstOrDefault(i => i.IsActive &&
                                !i.FreqLimit &&
                                i.TestId == getTest.Id &&
                                i.Description == limit.ParentLimit.Description, false);
                            if (parentLimit != null)
                            {
                                var getLimit = msRepository.Master<Limit>().InsertNow(new Limit()
                                {
                                    LimitTypeId = limit.LimitTypeId,
                                    TestId = getTest.Id,
                                    SiteId = item.SiteId,
                                    Prevalence = limit.Prevalence,
                                    Description = limit.Description,
                                    FreqLimit = true,
                                    CountTowardFrequency = limit.CountTowardFrequency,
                                    ParentLimitId = parentLimit.Id,
                                    Period = limit.Period,
                                    PeriodCount = limit.PeriodCount,
                                    OccurrenceCount = limit.OccurrenceCount,
                                }).Entity;
                            }

                        }
                    }
                    else
                    {
                        var testLimits = msRepository.Slave1<Limit>().Where(i => i.TestId == item.Id).ToArray();
                        foreach (var limit in testLimits)
                        {
                            limit.IsActive = false;
                            msRepository.Master<Limit>().UpdateNow(limit);
                        }
                    }
                }
                return Task.CompletedTask;
            };
            async Task updateTest(Plan plan)
            {
                using var scope = Furion.App.RootServices.CreateScope();
                var provider = scope.ServiceProvider;
                provider.GetService<ITenantService>().SetTenantScope("Emis");
                provider.GetService<IDataInjector>().InjectCurrentUserId(1);
                //provider.GetService<IDataInjector>().InjectESignDa
                Assert.IsTrue(provider.GetService<IDataProvider>().TryGetCurrentUserId(out var currentUserId));

                var localRepository = provider.GetRequiredService<MsRepository>();
                var user = localRepository.Master<User>().FirstOrDefault(i => i.Id == currentUserId);

                localRepository.Master<Log>().Insert(new Log()
                {
                    Name = "UpDate Plan",
                    Description = $"{JsonSerializer.Serialize(plan)}",
                    UserName = user.Account,
                    CreateBy = user.Id
                });
                var count = localRepository.Slave1<Plan>().Count(i => i.PlanGroupId == plan.PlanGroupId);


                void ReorderRuleGroup(Limit limit, LimitRuleGroup group)
                {
                    foreach (var subGroup in group.LimitRuleGroups)
                    {
                        limit.LimitRuleGroups.Add(subGroup);
                        ReorderRuleGroup(limit, subGroup);
                    }
                }

                foreach (var test in plan.Tests)
                {
                    foreach (var limit in test.Limits)
                    {
                        foreach (var group in limit.LimitRuleGroups.ToArray())
                        {
                            ReorderRuleGroup(limit, group);
                        }
                    }
                }
                await localRepository.Master<Plan>().InsertAsync(new Plan()
                {
                    Name = plan.Name,
                    Description = plan.Description,
                    Version = count + 1,
                    Status = Schedule.Entity.PlanStatus.Draft,
                    WorkWeek = plan.WorkWeek,
                    Environment = plan.Environment,
                    Barcode = plan.Barcode,
                    FillDayOfMonth = plan.FillDayOfMonth,
                    FillDayOfWeek = plan.FillDayOfWeek,
                    FillTime = plan.FillTime,
                    FillHoursOfDay = plan.FillHoursOfDay,
                    FillMinutesOfHour = plan.FillMinutesOfHour,
                    FillLength = plan.FillLength,
                    FillEffectiveDate = plan.FillEffectiveDate,
                    FirstDayOfWeek = plan.FirstDayOfWeek,
                    LocationId = plan.LocationId,
                    PlanGroupId = plan.PlanGroupId,
                    AutoFillFrequencyTypeId = plan.AutoFillFrequencyTypeId,
                    IsRepeatFill = plan.IsRepeatFill,
                    Tests = plan.Tests,
                });

                await localRepository.Master<Plan>().SaveNowAsync();

            }

            for (var i = 0; i < 10; i++)
            {
                plan = JsonSerializer.Deserialize<Plan>(str1);
                sw.Restart();
                await updateTestOri(plan);
                sw.Stop();
                Debug.WriteLine("updateTestOri\t{0}", sw.Elapsed.TotalSeconds);
            }
            for (var i = 0; i < 10; i++)
            {
                plan = JsonSerializer.Deserialize<Plan>(str1);
                sw.Restart();
                await updateTest(plan);
                sw.Stop();
                Debug.WriteLine("updateTest\t{0}", sw.Elapsed.TotalSeconds);
            }
        }

    }
}
