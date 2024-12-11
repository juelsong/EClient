using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0049 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "Category",
            //    table: "TableNoteAudit",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Category",
            //    table: "TableNote",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "LastSyncUserId",
            //    table: "EquipmentAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "LastSyncUserId",
            //    table: "Equipment",
            //    type: "INTEGER",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "TableNoteAudit");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "TableNote");

            migrationBuilder.DropColumn(
                name: "LastSyncUserId",
                table: "EquipmentAudit");

            migrationBuilder.DropColumn(
                name: "LastSyncUserId",
                table: "Equipment");
        }
    }
}
