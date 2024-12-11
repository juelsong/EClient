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

namespace ESys.Security.Entity
{
    using ESys.Contract.Entity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 电子签名配置
    /// </summary>
    public partial class ESignConfig : BizEntity<ESignConfig, int>, ITimedEntity, ITraceableEntity, IActiveEntity
    {
        /// <summary>
        /// 电子签名类型
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 权限，逗号分隔
        /// </summary>
        public string Permissions { get; set; }

        /// <summary>
        /// 签名次数
        /// </summary>
        public int SignCount { get; set; }

        #region interfaces
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        #endregion interfaces

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<ESignConfig> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(c => c.Category);
            entityBuilder.HasIndex(c => new { c.Category, c.IsActive });
            entityBuilder.Property(c => c.SignCount).HasDefaultValue(1);
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<ESignConfig> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            var id = 0;
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "plan",
                Permissions = "Plan:Add,Plan:Edit",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划添加
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "addPlan",
                Permissions = "Plan:Add",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划编辑
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "editPlan",
                Permissions = "Plan:Edit",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划复核
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "planReview",
                Permissions = "Plan:Review",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划批准
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "planApprove",
                Permissions = "Plan:Approve",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划生效
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "planEffective",
                Permissions = "Plan:Effective",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划退役
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "planRetire",
                Permissions = "Plan:Retire",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //计划添加到任务列表
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "planAddToPool",
                Permissions = "Plan:AddToPool",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //任务领取
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "receiveTask",
                Permissions = "Missions:Receive",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //任务分配
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "assignTask",
                Permissions = "MissionManagement:Assign",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //任务执行
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "executeTask",
                Permissions = "Missions:Execute",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //退回任务到未执行
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "returnTask",
                Permissions = "MissionManagement:Return,MyMissions:Return",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //任务电子签名
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "eSignTasks",
                Permissions = "MyMissions:Execute,SampleCollection:Execute,ExperimentalOperations:Execute,Incubation:Execute,InspectionRecord:Execute",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //任务NoTest
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "noTest",
                Permissions = "MissionManagement:NoTest,MyMissions:NoTest,SampleCollection:NoTest,ExperimentalOperations:NoTest,Incubation:NoTest,InspectionRecord:NoTest",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //再测试
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "reTest",
                Permissions = "MissionManagement:Resume",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //审核
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "auditReview",
                Permissions = "SampleAudit:Review",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //批准
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "auditApprove",
                Permissions = "SamplePrompt:Approval",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //重新审核批准
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "auditReApprove",
                Permissions = "SampleCompleted:ReApprove",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
            //复核修改数据
            yield return new ESignConfig()
            {
                Id = ++id,
                Category = "auditEdit",
                Permissions = "MissionManagement:Edit,SampleAudit:Edit",
                CreateBy = 1,
                IsActive = true,
                CreatedTime = new DateTimeOffset(2022, 1, 1, 0, 0, 0, 0, new TimeSpan(0, 8, 0, 0, 0)),
            };
        }

    }
}
