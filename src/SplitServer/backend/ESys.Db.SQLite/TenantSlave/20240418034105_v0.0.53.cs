using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0053 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "FlowRate",
            //    table: "ParticleCounterDataAudit",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "UM03",
            //    table: "ParticleCounterDataAudit",
            //    type: "INTEGER",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "FlowRate",
            //    table: "ParticleCounterData",
            //    type: "TEXT",
            //    nullable: true);

            //migrationBuilder.AddColumn<long>(
            //    name: "UM03",
            //    table: "ParticleCounterData",
            //    type: "INTEGER",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlowRate",
                table: "ParticleCounterDataAudit");

            migrationBuilder.DropColumn(
                name: "UM03",
                table: "ParticleCounterDataAudit");

            migrationBuilder.DropColumn(
                name: "FlowRate",
                table: "ParticleCounterData");

            migrationBuilder.DropColumn(
                name: "UM03",
                table: "ParticleCounterData");
        }
    }
}
