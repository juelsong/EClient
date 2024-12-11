namespace ESys.ScheduleTest
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Infrastructure.Entity;
    using ESys.Schedule.Entity;
    using ESys.Schedule.Report;
    using ESys.Schedule.Service;
    using DocumentFormat.OpenXml.Wordprocessing;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MsRepository = Furion.DatabaseAccessor.IMSRepository<ESys.Contract.Db.TenantMasterLocator, ESys.Contract.Db.TenantSlaveLocator>;


    [TestClass]
    public class AutoPlanTest
    {
        private const int BaseYear = 2022;
        private IServiceProvider serviceProvider = null;
        private IServiceScope scope = null;
        private IDataInjector injector = null;

        private IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository = null;
        private ILogger<Compliance> logger = null;
        private static readonly TimeSpan PollCycle = new(0, 0, 15, 0);

        [TestInitialize]
        public void Initialize()
        {
            this.scope = Furion.App.RootServices.CreateScope();
            this.serviceProvider = this.scope.ServiceProvider;
            this.serviceProvider.GetService<ITenantService>().SetTenantScope("Emis");
            this.injector = this.serviceProvider.GetService<IDataInjector>();
            this.msRepository = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
            this.logger = this.scope.ServiceProvider.GetService<ILogger<Compliance>>();
        }
        /// <summary>
        /// 测试自动生效
        /// </summary>
        [TestMethod]
        public void TestAutoEffective()
        {
            var now = DateTimeOffset.Now;
            var preNow = now.Add(-PollCycle);
            var name = Guid.NewGuid().ToString();

            var planGroupEntity = this.msRepository.Master<PlanGroup>().InsertNow(new PlanGroup()
            {
                Name = name,
            });
            var planEffectiveEntity = this.msRepository.Master<Plan>().InsertNow(new Plan()
            {
                Name = name,
                Status = PlanStatus.Effective,
                AutoFillFrequencyTypeId = 1,
                PlanGroupId = planGroupEntity.Entity.Id,
                FillEffectiveDate = DateTime.Now.AddDays(-1)
            });
            var planApprovedEntity = this.msRepository.Master<Plan>().InsertNow(new Plan()
            {
                Name = name,
                Status = PlanStatus.Approved,
                AutoFillFrequencyTypeId = 1,
                PlanGroupId = planGroupEntity.Entity.Id,
                FillEffectiveDate = DateTime.Now.AddDays(-1)

            });
            var needEffectiveId = planApprovedEntity.Entity.Id;
            planApprovedEntity.State = EntityState.Detached;
            planEffectiveEntity.State = EntityState.Detached;
            planGroupEntity.State = EntityState.Detached;

            var planstatus = this.msRepository.Slave1<Plan>().Any(i => i.Status == PlanStatus.Approved && i.Name == name);
            Assert.IsTrue(planstatus);
            try
            {
                AutoEffectivePlanService.AutomaticExecute(this.injector, this.msRepository, preNow, now, null);
                planstatus = this.msRepository.Slave1<Plan>().FirstOrDefault(i => i.Id == needEffectiveId).Status == PlanStatus.Effective;
                Assert.IsTrue(planstatus);
                this.msRepository.Master<Plan>().DeleteNow(planEffectiveEntity.Entity.Id);
                this.msRepository.Master<Plan>().DeleteNow(needEffectiveId);
                this.msRepository.Master<PlanGroup>().DeleteNow(planGroupEntity.Entity.Id);
            }
            catch (Exception ee)
            {
                this.msRepository.Master<Plan>().Delete(planEffectiveEntity.Entity.Id);
                this.msRepository.Master<Plan>().DeleteNow(needEffectiveId);
                this.msRepository.Master<PlanGroup>().DeleteNow(planGroupEntity.Entity.Id);
                Assert.Fail();
            }
        }

        /// <summary>
        /// 测试自动退役
        /// </summary>
        [TestMethod]
        public void TestAutoRetired()
        {
            var now = DateTimeOffset.Now;
            var preNow = now.Add(-PollCycle);
            var name = Guid.NewGuid().ToString();

            var planGroupEntity = this.msRepository.Master<PlanGroup>().InsertNow(new PlanGroup()
            {
                Name = name,
            });
            var planEffectiveEntity = this.msRepository.Master<Plan>().InsertNow(new Plan()
            {
                Name = name,
                Status = PlanStatus.Effective,
                AutoFillFrequencyTypeId = 1,
                PlanGroupId = planGroupEntity.Entity.Id,
                FillEffectiveDate = DateTime.Now.AddDays(-1)
            });

            planEffectiveEntity.State = EntityState.Detached;
            planGroupEntity.State = EntityState.Detached;

            var planstatus = this.msRepository.Slave1<Plan>().Any(i => i.Status == PlanStatus.Approved && i.Name == name);
            Assert.IsTrue(planstatus);
            try
            {
                AutoRetiredPlanService.AutomaticExecute(this.injector, this.msRepository, preNow, now, null);
                planstatus = this.msRepository.Slave1<Plan>().FirstOrDefault(i => i.Id == planEffectiveEntity.Entity.Id).Status == PlanStatus.Retired;
                Assert.IsTrue(planstatus);
                this.msRepository.Master<Plan>().DeleteNow(planEffectiveEntity.Entity.Id);
                this.msRepository.Master<PlanGroup>().DeleteNow(planGroupEntity.Entity.Id);
            }
            catch (Exception ee)
            {
                this.msRepository.Master<Plan>().Delete(planEffectiveEntity.Entity.Id);
                this.msRepository.Master<PlanGroup>().DeleteNow(planGroupEntity.Entity.Id);
                Assert.Fail();
            }
        }

    }
}
