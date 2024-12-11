using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.Master
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Limits = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MasterDbConnStr = table.Column<string>(type: "TEXT", nullable: true),
                    SlaveDbConnStr = table.Column<string>(type: "TEXT", nullable: true),
                    DbType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "Code", "DbType", "Limits", "MasterDbConnStr", "Name", "SlaveDbConnStr" },
                values: new object[] { 1, "Seagull", 0, new DateTime(2123, 3, 7, 9, 43, 56, 210, DateTimeKind.Utc).AddTicks(8072), "Data Source=192.168.31.51;Database=TenantMasterSeagull;User ID=sr;Password=Seagull@2020;pooling=true;port=3306;sslmode=none;CharSet=utf8;", "施格机器人", "Data Source=192.168.31.51;Database=TenantSlaveSeagull;User ID=sr;Password=Seagull@2020;pooling=true;port=3306;sslmode=none;CharSet=utf8;" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
