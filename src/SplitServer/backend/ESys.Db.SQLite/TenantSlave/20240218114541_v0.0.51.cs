using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "ReApprove",
            //    table: "SampleAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "ReApproveDate",
            //    table: "SampleAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ReApproveUserId",
            //    table: "SampleAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "ReApprove",
            //    table: "Sample",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "ReApproveDate",
            //    table: "Sample",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ReApproveUserId",
            //    table: "Sample",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "NoTestReason",
            //    table: "CurrentWorkSpaceAudit",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "NoTestReason",
            //    table: "CurrentWorkSpace",
            //    type: "TEXT",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReApprove",
                table: "SampleAudit");

            migrationBuilder.DropColumn(
                name: "ReApproveDate",
                table: "SampleAudit");

            migrationBuilder.DropColumn(
                name: "ReApproveUserId",
                table: "SampleAudit");

            migrationBuilder.DropColumn(
                name: "ReApprove",
                table: "Sample");

            migrationBuilder.DropColumn(
                name: "ReApproveDate",
                table: "Sample");

            migrationBuilder.DropColumn(
                name: "ReApproveUserId",
                table: "Sample");

            migrationBuilder.DropColumn(
                name: "NoTestReason",
                table: "CurrentWorkSpaceAudit");

            migrationBuilder.DropColumn(
                name: "NoTestReason",
                table: "CurrentWorkSpace");
        }
    }
}
