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
    using ESys.Infrastructure.Entity;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Report;
    using ESys.Schedule.Report.Model;
    using DocumentFormat.OpenXml.Math;
    using Furion.DatabaseAccessor;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using static ESys.Schedule.Report.Model.RptTestResult;

    [TestClass]
    public class TestRptImport
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
        public void TestImportRptData()
        {
            var rptRepo = this.msRepository.Master<RptSampleMart>();

            var data05s = rptRepo.Where(i => i.Name == "cal05").ToList();
            foreach (var item in data05s)
            {
                var reading = this.msRepository.Slave1<Schedule.Entity.Reading>().
                    FirstOrDefault(i => i.SampleId == item.SampleId
                    && i.MeasurementId == 194);
                var measurement = this.msRepository.Slave1<Measurement>().
                    FirstOrDefault(i =>  i.Id == 194);
                if (reading!=null)
                {
                    var s = rptRepo.InsertNow(new RptSampleMart()
                    {
                        SampleId = item.SampleId,
                        TestId = item.TestId,
                        SiteId = item.SiteId,
                        PlanId = item.PlanId,
                        SiteName = item.SiteName,
                        TestTypeId = item.TestTypeId,
                        TestTypeName = item.TestTypeName,
                        StartDate = item.StartDate,
                        EndDate = item.EndDate,
                        BarCode = item.BarCode,
                        Approved = item.Approved,
                        Completed = item.Completed,
                        Name = measurement.Name,
                        ResultsEnteredUserId = item.ResultsEnteredUserId,
                        ResultsEnteredUserName = item.ResultsEnteredUserName,
                        Environment = item.Environment,
                        //Product1 = current.Batch?.Product?.Name,
                        Classification = item.Classification,
                        ReadingId = item.ReadingId,
                        TestResultValue = reading.Value,
                        TestResultNumberValue = decimal.Parse(reading.Value),
                        ParticleSize = measurement.ParticleSize,
                        UOM = this.msRepository.Slave1<UnitOfMeasure>().FirstOrDefault(i => i.Id == reading.UnitOfMeasureId)?.Description,
                        MostServerDeviation = item.MostServerDeviation,
                        MostServerLimit = item.MostServerLimit,

                        //TODO 补全Limit 相关内容
                        ResultsEnteredEmpId = item.ResultsEnteredEmpId,
                        LocationId = item.LocationId,
                        LocationFullName = item.LocationFullName,
                        CompleteDate = item.CompleteDate,

                    }).Entity;

                }
            }
        }
    }
}
