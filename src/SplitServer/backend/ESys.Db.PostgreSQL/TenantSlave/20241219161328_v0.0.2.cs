using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.PostgreSQL.TenantSlave
{
    public partial class v002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Equipment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Equipment",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Equipment");

        }
    }
}
