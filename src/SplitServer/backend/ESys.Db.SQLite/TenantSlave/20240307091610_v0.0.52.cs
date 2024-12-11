using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0052 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "Count",
            //    table: "WorkSpaceOrganismAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "Count",
            //    table: "WorkSpaceOrganism",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "NoTestUserId",
            //    table: "CurrentWorkSpaceAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "NoTestUserId",
            //    table: "CurrentWorkSpace",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_NoTestUserId",
            //    table: "CurrentWorkSpace",
            //    column: "NoTestUserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_User_NoTestUserId",
            //    table: "CurrentWorkSpace",
            //    column: "NoTestUserId",
            //    principalTable: "User",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_User_NoTestUserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropIndex(
                name: "IX_CurrentWorkSpace_NoTestUserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "WorkSpaceOrganismAudit");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "WorkSpaceOrganism");

            migrationBuilder.DropColumn(
                name: "NoTestUserId",
                table: "CurrentWorkSpaceAudit");

            migrationBuilder.DropColumn(
                name: "NoTestUserId",
                table: "CurrentWorkSpace");
        }
    }
}
