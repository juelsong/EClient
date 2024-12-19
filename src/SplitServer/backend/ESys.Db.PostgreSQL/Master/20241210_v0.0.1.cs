using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ESys.Db.PostgreSQL.Master
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Limits = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MasterDbConnStr = table.Column<string>(type: "text", nullable: true),
                    SlaveDbConnStr = table.Column<string>(type: "text", nullable: true),
                    DbType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "Code", "DbType", "Limits", "MasterDbConnStr", "Name", "SlaveDbConnStr" },
                values: new object[] 
                {
                    1, 
                    "Test", 
                    0, 
                    DateTime.SpecifyKind(new DateTime(2121, 12, 21, 0, 50, 48, 20), DateTimeKind.Unspecified),
                    "Host=127.0.0.1;Port=5432;Database=etestData;User Id=postgres;Password=123456;",
                    "Test",
                    "Host=127.0.0.1;Port=5432;Database=etestData;User Id=postgres;Password=123456;"
                }); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
