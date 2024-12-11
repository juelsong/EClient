using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0050 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Deviation_LimitDef_LimitDefId",
            //    table: "Deviation");

            //migrationBuilder.RenameColumn(
            //    name: "LimitDefId",
            //    table: "DeviationAudit",
            //    newName: "LimitId");

            //migrationBuilder.RenameColumn(
            //    name: "LimitDefId",
            //    table: "Deviation",
            //    newName: "LimitId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Deviation_LimitDefId",
            //    table: "Deviation",
            //    newName: "IX_Deviation_LimitId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Deviation_Limit_LimitId",
            //    table: "Deviation",
            //    column: "LimitId",
            //    principalTable: "Limit",
            //    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deviation_Limit_LimitId",
                table: "Deviation");

            migrationBuilder.RenameColumn(
                name: "LimitId",
                table: "DeviationAudit",
                newName: "LimitDefId");

            migrationBuilder.RenameColumn(
                name: "LimitId",
                table: "Deviation",
                newName: "LimitDefId");

            migrationBuilder.RenameIndex(
                name: "IX_Deviation_LimitId",
                table: "Deviation",
                newName: "IX_Deviation_LimitDefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deviation_LimitDef_LimitDefId",
                table: "Deviation",
                column: "LimitDefId",
                principalTable: "LimitDef",
                principalColumn: "Id");
        }
    }
}
