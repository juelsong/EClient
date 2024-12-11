namespace ESys.ScheduleTest
{
    using ESys.Contract.Service;
    using ESys.Infrastructure.Entity;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Service;
    using ESys.UnitTest;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    using MsRepository = Furion.DatabaseAccessor.IMSRepository<ESys.Contract.Db.TenantMasterLocator, ESys.Contract.Db.TenantSlaveLocator>;
    using TestFunction = System.Func<System.Collections.Generic.List<long>, System.Collections.Generic.List<long>, int, bool>;

    class KeyId
    {
        public MsRepository MsRepository { get; set; }

        public DateTimeOffset PreNow { get; set; }

        public DateTimeOffset Now { get; set; }

        public int SiteId { get; set; }

        public long TestTypeId { get; set; }

        public long TimeFrameId { get; set; }

        public long PlanId { get; set; }

        private readonly List<long> testIds = new();

        public List<long> TestIds()
        {
            return this.testIds;
        }

        private readonly List<long> currentWorkSpaceIds = new();

        public List<long> CurrentWorkSpaceIds()
        {
            return this.currentWorkSpaceIds;
        }

        private readonly List<long> sampleIds = new();

        public List<long> SampleIds()
        {
            return this.sampleIds;
        }
    }

    [TestClass]
    public class ScheduleServiceTest
    {
        private const int HourToMinute = 60;

        private const int WeekToDay = 7;

        private static readonly TimeSpan PollCycle = new(0, 0, 15, 0);

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext _)
        {
            UnitTestContext.Instance.Initialize<ESys.App.Startup>("scheduletestsettings.json");
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            UnitTestContext.Instance.Cleanup();
        }

        [TestMethod]
        public void TestAutomaticExecute()
        {
            using var scope = Furion.App.RootServices.CreateScope();
            scope.ServiceProvider.GetService<ITenantService>().SetTenantScope("Seagull");

            var now = DateTimeOffset.Now;
            var keyId = new KeyId
            {
                MsRepository = scope.ServiceProvider.GetService<MsRepository>(),
                Now = now,
                PreNow = now.Add(-PollCycle)
            };

            Exception exception = null;
            try
            {
                CreateDefault(keyId);

                TestHourlyPlanByHourly(keyId);
                TestDailyPlanByHourly(keyId);
                TestWeeklyPlanByHourly(keyId);
                TestWeeklyPlanByWeekly1(keyId);
                TestWeeklyPlanByWeekly2(keyId);
                TestWeeklyPlanByWeekly3(keyId);
                TestMonthlyPlanByDaily(keyId);
                TestMonthlyPlanByMonthly1(keyId);
                TestMonthlyPlanByMonthly2(keyId);
                TestMonthlyPlanByMonthly3(keyId);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                DeleteDefault(keyId);
            }

            if (exception != null)
            {
                throw new AssertFailedException(exception.Message, exception);
            }
        }

        private static void CreateDefault(KeyId keyId)
        {
            var site = CreateDefaultSite();
            var siteEntity = keyId.MsRepository.Master<Site>().InsertNow(site);
            keyId.SiteId = siteEntity.Entity.Id;
            siteEntity.State = EntityState.Detached;

            var testType = CreateDefaultTestType();
            var testTypeEntity = keyId.MsRepository.Master<TestType>().InsertNow(testType);
            keyId.TestTypeId = testTypeEntity.Entity.Id;
            testTypeEntity.State = EntityState.Detached;

            var timeFrame = CreateDefaultTimeFrame(keyId);
            var timeFrameEntity = keyId.MsRepository.Master<TimeFrame>().InsertNow(timeFrame);
            keyId.TimeFrameId = timeFrameEntity.Entity.Id;
            timeFrameEntity.State = EntityState.Detached;
        }

        private static Site CreateDefaultSite()
        {
            return new Site()
            {
                Name = "Name",
                Description = "Description",
                Barcode = "",
                LocationId = 1,
                ClassificationId = 1,
                SecondClassificationId = null,
                SiteTypeId = 1
            };
        }

        private static TestType CreateDefaultTestType()
        {
            return new TestType()
            {
                Description = "Description",
                TestCategoryId = 1,
                LocationId = null,
                TestTypeCodeId = 1,
                NegativeControl = false,
                SystemRecord = false,
                Price = null,
                RequireProductSelection = false,
                ProdSelectionTimeFrameId = null,
                TestTypeLabelId = 1
            };
        }

        private static TimeFrame CreateDefaultTimeFrame(KeyId keyId)
        {
            return new TimeFrame()
            {
                ClonedTimeFrameId = null,
                TestTypeId = keyId.TestTypeId,
                TestStageId = 4,
                Sequence = 1,
                Description = "Description",
                MinTime = new TimeSpan(),
                MaxTime = new TimeSpan(),
                MinCycles = 1,
                MaxCycles = 1,
                MinCalAlignment = 0,
                MaxCalAlignment = 0,
                ESign = false,
                ESignVerification = false,
                AutoAssignNext = false,
                ShowPersonnelPanel = false,
                RequireStartDate = false,
                RequireEndDate = false,
                AutoStartDate = false,
                AutoEndDate = false,
                LockStartDate = false,
                RequirePerformedUser = false,
                PrintLabels = false,
                ShowReadings = false,
                ShowOrgid = false,
                UsePreviousResult = false,
                VerifyPrevious = false,
                ShowDeviceControl = false,
                ShowSampleMedia = false,
                ShowSampleTimes = false,
                ShowIncubationTimes = false,
                ShowAddCycle = false,
                ShowEnvironment = false,
                MatchingResultsOnly = false
            };
        }

        private static void TestHourlyPlanByHourly(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Hourly;
            plan.FillMinutesOfHour = keyId.Now.Minute;
            plan.FillHoursOfDay = 0xffffff;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 24;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Hourly;
            test.Interval = 1;
            test.HoursOfDay = 0x444444;
            test.MinutesOfHour = 0;
            test.DaysOfWeek = 0x7f;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt);
                    Assert.AreEqual(sampleIds.Count, cnt);
                    return true;
                }));
        }

        private static Plan CreateDefaultPlan()
        {
            return new Plan()
            {
                Name = "Name",
                Description = "Description",
                Version = 0,
                Enabled = true,
                EffectiveDate = null,
                IneffectiveDate = null,
                ApprovedDate = null,
                CompletedDate = null,
                Approved = false,
                Status = PlanStatus.Effective,
                WorkWeek = null,
                EnvironmentId = 1,
                Barcode = "Barcode",
                FillDayOfMonth = 0,
                FillDayOfWeek = 0,
                FillTime = 0,
                FillHoursOfDay = 0xffffff,
                FillMinutesOfHour = 0,
                FillLength = 1,
                FillEffectiveDate = new DateTimeOffset(),
                LastFillDate = null,
                FirstDayOfWeek = 0,
                LocationId = null,
                GroupId = null,
                PlanGroupId = null,
                AutoFillFrequencyTypeId = 2,
                IsRepeatFill = true
            };
        }

        private static Test CreateDefaultTest(KeyId keyId)
        {
            return new Test()
            {
                Name = "Name",
                Description = "Description",
                Interval = 1,
                MinutesOfHour = 0,
                HoursOfDay = 0xffffff,
                DaysOfWeek = 0x7f,
                DaysOfMonth = 0,
                MonthsOfYear = 0,
                Randomize = false,
                NotTest = false,
                NegativeControl = false,
                SequenceAdvice = false,
                EnvironmentId = 1,
                ScheduleStartDate = null,
                ScheduleOffset = null,
                Price = 0.00M,
                NotificationFrequencyId = null,
                SiteId = keyId.SiteId,
                TestTypeId = keyId.TestTypeId,
                PlanId = keyId.PlanId,
                ParentTestId = null,
                TimeFrameDilutionId = null,
                GroupId = null,
                TestFrequencyId = null,
                TestFrequencyTypeId = 1,
                TestFrequencyOccurrenceId = null
            };
        }

        private static void TestPlan(KeyId keyId, Plan plan, List<Test> tests, TestFunction func)
        {
            Exception exception = null;
            var now = keyId.Now;
            var preNow = keyId.PreNow;
            try
            {
                var planEntity = keyId.MsRepository.Master<Plan>().InsertNow(plan);
                keyId.PlanId = planEntity.Entity.Id;
                planEntity.State = EntityState.Detached;

                keyId.TestIds().Clear();
                foreach (var test in tests)
                {
                    test.PlanId = keyId.PlanId;
                    var testEntity = keyId.MsRepository.Master<Test>().InsertNow(test);
                    keyId.TestIds().Add(testEntity.Entity.Id);
                    testEntity.State = EntityState.Detached;
                }

                TestPlanImpl(keyId, func, 0);

                DeleteCurrentWorkSpaces(keyId);
                var eDt = ScheduleService.GetEndDate(planEntity.Entity, keyId.PreNow, keyId.Now, null);
                Assert.IsNotNull(eDt);
                keyId.Now = ((DateTimeOffset)eDt).AddMinutes(1);
                keyId.PreNow = keyId.Now.Add(-PollCycle);

                TestPlanImpl(keyId, func, 1);
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                DeleteCurrentWorkSpaces(keyId);
                DeletePlan(keyId);
                keyId.Now = now;
                keyId.PreNow = preNow;
            }

            if (exception != null)
            {
                throw new AssertFailedException(exception.Message, exception);
            }
        }

        private static void TestPlanImpl(KeyId keyId, TestFunction func, int funcType)
        {
            keyId.CurrentWorkSpaceIds().Clear();
            keyId.SampleIds().Clear();
            ScheduleService.AutomaticExecute(keyId.MsRepository, keyId.PreNow, keyId.Now, null, keyId.PlanId, out var cwsIds, out var sampleIds);
            keyId.CurrentWorkSpaceIds().AddRange(cwsIds);
            keyId.SampleIds().AddRange(sampleIds);
            func(cwsIds, sampleIds, funcType);
        }

        private static void DeleteCurrentWorkSpaces(KeyId keyId)
        {
            DeleteByIds<CurrentWorkSpace>(keyId.MsRepository, keyId.CurrentWorkSpaceIds());
            DeleteByIds<Sample>(keyId.MsRepository, keyId.SampleIds());
        }

        private static void DeleteByIds<T>(MsRepository msRepository, List<long> ids) where T : class, IPrivateEntity, new()
        {
            foreach (var id in ids)
            {
                msRepository.Master<T>().DeleteNow(id);
            }
        }

        private static void DeletePlan(KeyId keyId)
        {
            DeleteByIds<Test>(keyId.MsRepository, keyId.TestIds());
            keyId.MsRepository.Master<Plan>().DeleteNow(keyId.PlanId);
        }

        private static void TestDailyPlanByHourly(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Daily;
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 2;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Hourly;
            test.Interval = 1;
            test.HoursOfDay = 0xa;
            test.MinutesOfHour = 0;
            test.DaysOfWeek = 0x7f;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * plan.FillLength);
                    return true;
                }));
        }

        private static void TestWeeklyPlanByHourly(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Weekly;
            plan.FillDayOfWeek = (0x1 << (int)keyId.Now.DayOfWeek);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 2;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Hourly;
            test.Interval = 1;
            test.HoursOfDay = 0xa;
            test.MinutesOfHour = 0;
            test.DaysOfWeek = 0x7f;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * WeekToDay * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * WeekToDay * plan.FillLength);
                    return true;
                }));
        }

        private static void TestWeeklyPlanByWeekly1(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Weekly;
            plan.FillDayOfWeek = (0x1 << (int)keyId.Now.DayOfWeek);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 2;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Weekly;
            test.Interval = 1;
            test.Randomize = false;
            test.DaysOfWeek = 0x4;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * plan.FillLength);
                    return true;
                }));
        }

        private static void TestWeeklyPlanByWeekly2(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Weekly;
            plan.FillDayOfWeek = (0x1 << (int)keyId.Now.DayOfWeek);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 2;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Weekly;
            test.Interval = 1;
            test.Randomize = true;
            test.DaysOfWeek = 0x38;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * plan.FillLength);
                    return true;
                }));
        }

        private static void TestWeeklyPlanByWeekly3(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Weekly;
            plan.FillDayOfWeek = (0x1 << (int)keyId.Now.DayOfWeek);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 3;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Weekly;
            test.Interval = 1;
            test.ScheduleStartDate = keyId.Now.AddDays(7);
            test.ScheduleOffset = 1;
            test.Randomize = true;
            test.DaysOfWeek = 0x38;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    var offset = (0 == funcType ? 2 : 0);
                    Assert.AreEqual(cwsIds.Count, cnt * (plan.FillLength - offset));
                    Assert.AreEqual(sampleIds.Count, cnt * (plan.FillLength - offset));
                    return true;
                }));
        }

        private static void TestMonthlyPlanByDaily(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Monthly;
            plan.FillDayOfMonth = (0x1 << (int)keyId.Now.Day - 1);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 1;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Daily;
            test.Interval = 1;
            test.HoursOfDay = 0x10;
            test.MinutesOfHour = 0;
            test.DaysOfWeek = 0x7f;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    var dayCnt = ScheduleService.GetDayCountInMonths(keyId.Now);
                    Assert.AreEqual(cwsIds.Count, cnt * dayCnt);
                    Assert.AreEqual(sampleIds.Count, cnt * dayCnt);
                    return true;
                }));
        }

        private static void TestMonthlyPlanByMonthly1(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Monthly;
            plan.FillDayOfMonth = (0x1 << (int)keyId.Now.Day - 1);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 1;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Monthly;
            test.Interval = 1;
            test.DaysOfMonth = (0x1 << 28 - 1);
            test.DaysOfWeek = 0x7f;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * plan.FillLength);
                    return true;
                }));
        }

        private static void TestMonthlyPlanByMonthly2(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Monthly;
            plan.FillDayOfMonth = (0x1 << (int)keyId.Now.Day - 1);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 1;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Monthly;
            test.Interval = 1;
            test.TestFrequencyOccurrenceId = 2;
            test.DaysOfMonth = (0x1 << 28 - 1);
            test.DaysOfWeek = 0x4;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    var min = cnt * (plan.FillLength - 1);
                    var max = cnt * (plan.FillLength + 1);
                    Assert.IsTrue(cwsIds.Count >= min && cwsIds.Count <= max);
                    Assert.IsTrue(sampleIds.Count >= min && sampleIds.Count <= max);
                    return true;
                }));
        }

        private static void TestMonthlyPlanByMonthly3(KeyId keyId)
        {
            var plan = CreateDefaultPlan();
            plan.AutoFillFrequencyTypeId = (int)FrequencyType.Monthly;
            plan.FillDayOfMonth = (0x1 << (int)keyId.Now.Day - 1);
            plan.FillTime = keyId.Now.Hour * HourToMinute + keyId.Now.Minute;
            plan.FillEffectiveDate = keyId.Now;
            plan.FillLength = 3;
            plan.LastFillDate = null;
            plan.IsRepeatFill = true;

            var test = CreateDefaultTest(keyId);
            test.TestFrequencyTypeId = (int)FrequencyType.Monthly;
            test.Interval = 1;
            test.Randomize = true;
            test.DaysOfMonth = (0x1 << 28 - 1);
            test.DaysOfWeek = 0x7f;
            test.HoursOfDay = 0x8;
            test.MinutesOfHour = 0;

            TestPlan(keyId, plan, new List<Test>() { test },
                ((cwsIds, sampleIds, funcType) =>
                {
                    var cnt = ScheduleService.GetBitCount(test.HoursOfDay);
                    Assert.AreEqual(cwsIds.Count, cnt * plan.FillLength);
                    Assert.AreEqual(sampleIds.Count, cnt * plan.FillLength);
                    return true;
                }));
        }

        private static void DeleteDefault(KeyId keyId)
        {
            keyId.MsRepository.Master<TimeFrame>().DeleteNow(keyId.TimeFrameId);
            keyId.MsRepository.Master<TestType>().DeleteNow(keyId.TestTypeId);
            keyId.MsRepository.Master<Site>().DeleteNow(keyId.SiteId);
        }
    }
}
