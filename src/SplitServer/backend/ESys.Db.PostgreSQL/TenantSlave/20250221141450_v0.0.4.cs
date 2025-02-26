using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESys.Db.PostgreSQL.TenantSlave
{
    public partial class v004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentNotification",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone_address = table.Column<string>(type: "text", nullable: true),
                    alert_mode = table.Column<bool>(type: "boolean", nullable: false),
                    device_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentNotification", x => x.id);
                    table.ForeignKey(
                        name: "FK_EquipmentNotification_Equipment_device_id",
                        column: x => x.device_id,
                        principalTable: "Equipment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentNotification_device_id",
                table: "EquipmentNotification",
                column: "device_id");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentNotification_device_id_phone_address",
                table: "EquipmentNotification",
                columns: new[] { "device_id", "phone_address" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentNotification");
        }
    }
}
