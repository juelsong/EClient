using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESys.Db.PostgreSQL.TenantSlave
{
    public partial class v003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainSecond",
                table: "EquipmentTPM",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "EquipmentTPM",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorDaily",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_of_data = table.Column<int>(type: "integer", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    pm2_5_average = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_max = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_min = table.Column<int>(type: "integer", nullable: false),
                    pm10_average = table.Column<int>(type: "integer", nullable: false),
                    pm10_max = table.Column<int>(type: "integer", nullable: false),
                    pm10_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_max = table.Column<int>(type: "integer", nullable: false),
                    tsp_average = table.Column<int>(type: "integer", nullable: false),
                    temperature_min = table.Column<int>(type: "integer", nullable: false),
                    temperature_max = table.Column<int>(type: "integer", nullable: false),
                    temperature_average = table.Column<int>(type: "integer", nullable: false),
                    humidity_min = table.Column<int>(type: "integer", nullable: false),
                    humidity_max = table.Column<int>(type: "integer", nullable: false),
                    humidity_average = table.Column<int>(type: "integer", nullable: false),
                    pre_min = table.Column<int>(type: "integer", nullable: false),
                    pre_max = table.Column<int>(type: "integer", nullable: false),
                    pre_avg = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_min = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_max = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_average = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_min = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_max = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_average = table.Column<int>(type: "integer", nullable: false),
                    noise_min = table.Column<int>(type: "integer", nullable: false),
                    noise_max = table.Column<int>(type: "integer", nullable: false),
                    noise_average = table.Column<int>(type: "integer", nullable: false),
                    cpm = table.Column<int>(type: "integer", nullable: false),
                    is_power_on = table.Column<bool>(type: "boolean", nullable: false),
                    is_valid_data = table.Column<bool>(type: "boolean", nullable: false),
                    equipment_data_valid_flag = table.Column<int>(type: "integer", nullable: false),
                    source_of_data = table.Column<int>(type: "integer", nullable: false),
                    dust_calibration_step = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorDaily", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorHour",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_of_data = table.Column<int>(type: "integer", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    pm2_5_average = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_max = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_min = table.Column<int>(type: "integer", nullable: false),
                    pm10_average = table.Column<int>(type: "integer", nullable: false),
                    pm10_max = table.Column<int>(type: "integer", nullable: false),
                    pm10_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_max = table.Column<int>(type: "integer", nullable: false),
                    tsp_average = table.Column<int>(type: "integer", nullable: false),
                    temperature_min = table.Column<int>(type: "integer", nullable: false),
                    temperature_max = table.Column<int>(type: "integer", nullable: false),
                    temperature_average = table.Column<int>(type: "integer", nullable: false),
                    humidity_min = table.Column<int>(type: "integer", nullable: false),
                    humidity_max = table.Column<int>(type: "integer", nullable: false),
                    humidity_average = table.Column<int>(type: "integer", nullable: false),
                    pre_min = table.Column<int>(type: "integer", nullable: false),
                    pre_max = table.Column<int>(type: "integer", nullable: false),
                    pre_avg = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_min = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_max = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_average = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_min = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_max = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_average = table.Column<int>(type: "integer", nullable: false),
                    noise_min = table.Column<int>(type: "integer", nullable: false),
                    noise_max = table.Column<int>(type: "integer", nullable: false),
                    noise_average = table.Column<int>(type: "integer", nullable: false),
                    cpm = table.Column<int>(type: "integer", nullable: false),
                    is_power_on = table.Column<bool>(type: "boolean", nullable: false),
                    is_valid_data = table.Column<bool>(type: "boolean", nullable: false),
                    equipment_data_valid_flag = table.Column<int>(type: "integer", nullable: false),
                    source_of_data = table.Column<int>(type: "integer", nullable: false),
                    dust_calibration_step = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorHour", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorMinute",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_of_data = table.Column<int>(type: "integer", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    pm2_5_average = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_max = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_min = table.Column<int>(type: "integer", nullable: false),
                    pm10_average = table.Column<int>(type: "integer", nullable: false),
                    pm10_max = table.Column<int>(type: "integer", nullable: false),
                    pm10_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_max = table.Column<int>(type: "integer", nullable: false),
                    tsp_average = table.Column<int>(type: "integer", nullable: false),
                    temperature_min = table.Column<int>(type: "integer", nullable: false),
                    temperature_max = table.Column<int>(type: "integer", nullable: false),
                    temperature_average = table.Column<int>(type: "integer", nullable: false),
                    humidity_min = table.Column<int>(type: "integer", nullable: false),
                    humidity_max = table.Column<int>(type: "integer", nullable: false),
                    humidity_average = table.Column<int>(type: "integer", nullable: false),
                    pre_min = table.Column<int>(type: "integer", nullable: false),
                    pre_max = table.Column<int>(type: "integer", nullable: false),
                    pre_avg = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_min = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_max = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_average = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_min = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_max = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_average = table.Column<int>(type: "integer", nullable: false),
                    noise_min = table.Column<int>(type: "integer", nullable: false),
                    noise_max = table.Column<int>(type: "integer", nullable: false),
                    noise_average = table.Column<int>(type: "integer", nullable: false),
                    cpm = table.Column<int>(type: "integer", nullable: false),
                    is_power_on = table.Column<bool>(type: "boolean", nullable: false),
                    is_valid_data = table.Column<bool>(type: "boolean", nullable: false),
                    equipment_data_valid_flag = table.Column<int>(type: "integer", nullable: false),
                    source_of_data = table.Column<int>(type: "integer", nullable: false),
                    dust_calibration_step = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorMinute", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorQuarter",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_of_data = table.Column<int>(type: "integer", nullable: false),
                    update_time = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    pm2_5_average = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_max = table.Column<int>(type: "integer", nullable: false),
                    pm2_5_min = table.Column<int>(type: "integer", nullable: false),
                    pm10_average = table.Column<int>(type: "integer", nullable: false),
                    pm10_max = table.Column<int>(type: "integer", nullable: false),
                    pm10_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_min = table.Column<int>(type: "integer", nullable: false),
                    tsp_max = table.Column<int>(type: "integer", nullable: false),
                    tsp_average = table.Column<int>(type: "integer", nullable: false),
                    temperature_min = table.Column<int>(type: "integer", nullable: false),
                    temperature_max = table.Column<int>(type: "integer", nullable: false),
                    temperature_average = table.Column<int>(type: "integer", nullable: false),
                    humidity_min = table.Column<int>(type: "integer", nullable: false),
                    humidity_max = table.Column<int>(type: "integer", nullable: false),
                    humidity_average = table.Column<int>(type: "integer", nullable: false),
                    pre_min = table.Column<int>(type: "integer", nullable: false),
                    pre_max = table.Column<int>(type: "integer", nullable: false),
                    pre_avg = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_min = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_max = table.Column<int>(type: "integer", nullable: false),
                    wind_speed_average = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_min = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_max = table.Column<int>(type: "integer", nullable: false),
                    wind_direction_average = table.Column<int>(type: "integer", nullable: false),
                    noise_min = table.Column<int>(type: "integer", nullable: false),
                    noise_max = table.Column<int>(type: "integer", nullable: false),
                    noise_average = table.Column<int>(type: "integer", nullable: false),
                    cpm = table.Column<int>(type: "integer", nullable: false),
                    is_power_on = table.Column<bool>(type: "boolean", nullable: false),
                    is_valid_data = table.Column<bool>(type: "boolean", nullable: false),
                    equipment_data_valid_flag = table.Column<int>(type: "integer", nullable: false),
                    source_of_data = table.Column<int>(type: "integer", nullable: false),
                    dust_calibration_step = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorQuarter", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorDaily_device_of_data",
                table: "EnvironmentalSensorDaily",
                column: "device_of_data");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorHour_device_of_data",
                table: "EnvironmentalSensorHour",
                column: "device_of_data");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorMinute_device_of_data",
                table: "EnvironmentalSensorMinute",
                column: "device_of_data");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorQuarter_device_of_data",
                table: "EnvironmentalSensorQuarter",
                column: "device_of_data");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvironmentalSensorDaily");

            migrationBuilder.DropTable(
                name: "EnvironmentalSensorHour");

            migrationBuilder.DropTable(
                name: "EnvironmentalSensorMinute");

            migrationBuilder.DropTable(
                name: "EnvironmentalSensorQuarter");

            migrationBuilder.DropColumn(
                name: "RemainSecond",
                table: "EquipmentTPM");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "EquipmentTPM");
        }
    }
}
