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
namespace ESys.Db.SQLite.TenantSlave
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Migrations.Operations;
    using System.Collections.Generic;

    partial class v0048
    {
        private readonly List<MigrationOperation> migrationUpOperations;
        private readonly List<MigrationOperation> migrationDownOperations;

        public v0048()
        {
            this.migrationUpOperations = new(base.UpOperations);
            this.migrationDownOperations = new(base.DownOperations);

            var upBuilder = new MigrationBuilder(this.ActiveProvider);
            var downBuilder = new MigrationBuilder(this.ActiveProvider);

            this.AddLocationView(upBuilder, downBuilder);
            this.AddUserView(upBuilder, downBuilder);
            this.AddSubscriptionView(upBuilder, downBuilder);
            this.AddSiteView(upBuilder, downBuilder);
            this.AddEquipmentView(upBuilder, downBuilder);
            this.AddMediaView(upBuilder, downBuilder);
            this.AddGeneralPoolV(upBuilder, downBuilder);
            this.AddAllSystemPromptV(upBuilder, downBuilder);
            this.AddSampleView(upBuilder, downBuilder);
            this.AddTestTypeView(upBuilder, downBuilder);
            this.AddRptLocPromptComplianceView(upBuilder, downBuilder);
            this.AddRptSamplePeriodView(upBuilder, downBuilder);
            this.AddRptFrequencySummaryView(upBuilder, downBuilder);
            this.AddNotificationView(upBuilder, downBuilder);

            this.migrationUpOperations.AddRange(upBuilder.Operations);
            this.migrationDownOperations.AddRange(downBuilder.Operations);
        }

        public override IReadOnlyList<MigrationOperation> UpOperations => this.migrationUpOperations;
        public override IReadOnlyList<MigrationOperation> DownOperations => this.migrationDownOperations;

    }
}
