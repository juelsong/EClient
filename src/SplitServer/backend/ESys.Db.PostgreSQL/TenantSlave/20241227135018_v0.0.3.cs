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
            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorDaily",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceIdNet = table.Column<string>(type: "text", nullable: true),
                    DeviceIdNode = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MN = table.Column<string>(type: "text", nullable: true),
                    MNReverse = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Max = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Average = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Min = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Current = table.Column<int>(type: "integer", nullable: false),
                    PM10Max = table.Column<int>(type: "integer", nullable: false),
                    PM10Average = table.Column<int>(type: "integer", nullable: false),
                    PM10Min = table.Column<int>(type: "integer", nullable: false),
                    PM10Current = table.Column<int>(type: "integer", nullable: false),
                    CPMMax = table.Column<int>(type: "integer", nullable: false),
                    CPMAverage = table.Column<int>(type: "integer", nullable: false),
                    CPMMin = table.Column<int>(type: "integer", nullable: false),
                    CPMCurrent = table.Column<int>(type: "integer", nullable: false),
                    NoiseMax = table.Column<int>(type: "integer", nullable: false),
                    NoiseAverage = table.Column<int>(type: "integer", nullable: false),
                    NoiseMin = table.Column<int>(type: "integer", nullable: false),
                    NoiseCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMax = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionAverage = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMin = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMax = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedAverage = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMin = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedCurrent = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMax = table.Column<int>(type: "integer", nullable: false),
                    TemperatureAverage = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMin = table.Column<int>(type: "integer", nullable: false),
                    TemperatureCurrent = table.Column<int>(type: "integer", nullable: false),
                    HumidityMax = table.Column<int>(type: "integer", nullable: false),
                    HumidityAverage = table.Column<int>(type: "integer", nullable: false),
                    HumidityMin = table.Column<int>(type: "integer", nullable: false),
                    HumidityCurrent = table.Column<int>(type: "integer", nullable: false),
                    VOCMax = table.Column<int>(type: "integer", nullable: false),
                    VOCAverage = table.Column<int>(type: "integer", nullable: false),
                    VOCMin = table.Column<int>(type: "integer", nullable: false),
                    VOCCurrent = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMax = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureAverage = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMin = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureCurrent = table.Column<int>(type: "integer", nullable: false),
                    IsCalibratingCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    IsPowerOnCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    RTCNeedsTimeSyncCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Reserved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorDaily", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorHour",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceIdNet = table.Column<string>(type: "text", nullable: true),
                    DeviceIdNode = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MN = table.Column<string>(type: "text", nullable: true),
                    MNReverse = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Max = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Average = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Min = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Current = table.Column<int>(type: "integer", nullable: false),
                    PM10Max = table.Column<int>(type: "integer", nullable: false),
                    PM10Average = table.Column<int>(type: "integer", nullable: false),
                    PM10Min = table.Column<int>(type: "integer", nullable: false),
                    PM10Current = table.Column<int>(type: "integer", nullable: false),
                    CPMMax = table.Column<int>(type: "integer", nullable: false),
                    CPMAverage = table.Column<int>(type: "integer", nullable: false),
                    CPMMin = table.Column<int>(type: "integer", nullable: false),
                    CPMCurrent = table.Column<int>(type: "integer", nullable: false),
                    NoiseMax = table.Column<int>(type: "integer", nullable: false),
                    NoiseAverage = table.Column<int>(type: "integer", nullable: false),
                    NoiseMin = table.Column<int>(type: "integer", nullable: false),
                    NoiseCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMax = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionAverage = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMin = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMax = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedAverage = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMin = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedCurrent = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMax = table.Column<int>(type: "integer", nullable: false),
                    TemperatureAverage = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMin = table.Column<int>(type: "integer", nullable: false),
                    TemperatureCurrent = table.Column<int>(type: "integer", nullable: false),
                    HumidityMax = table.Column<int>(type: "integer", nullable: false),
                    HumidityAverage = table.Column<int>(type: "integer", nullable: false),
                    HumidityMin = table.Column<int>(type: "integer", nullable: false),
                    HumidityCurrent = table.Column<int>(type: "integer", nullable: false),
                    VOCMax = table.Column<int>(type: "integer", nullable: false),
                    VOCAverage = table.Column<int>(type: "integer", nullable: false),
                    VOCMin = table.Column<int>(type: "integer", nullable: false),
                    VOCCurrent = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMax = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureAverage = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMin = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureCurrent = table.Column<int>(type: "integer", nullable: false),
                    IsCalibratingCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    IsPowerOnCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    RTCNeedsTimeSyncCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Reserved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorHour", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorMinute",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceIdNet = table.Column<string>(type: "text", nullable: true),
                    DeviceIdNode = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MN = table.Column<string>(type: "text", nullable: true),
                    MNReverse = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Max = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Average = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Min = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Current = table.Column<int>(type: "integer", nullable: false),
                    PM10Max = table.Column<int>(type: "integer", nullable: false),
                    PM10Average = table.Column<int>(type: "integer", nullable: false),
                    PM10Min = table.Column<int>(type: "integer", nullable: false),
                    PM10Current = table.Column<int>(type: "integer", nullable: false),
                    CPMMax = table.Column<int>(type: "integer", nullable: false),
                    CPMAverage = table.Column<int>(type: "integer", nullable: false),
                    CPMMin = table.Column<int>(type: "integer", nullable: false),
                    CPMCurrent = table.Column<int>(type: "integer", nullable: false),
                    NoiseMax = table.Column<int>(type: "integer", nullable: false),
                    NoiseAverage = table.Column<int>(type: "integer", nullable: false),
                    NoiseMin = table.Column<int>(type: "integer", nullable: false),
                    NoiseCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMax = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionAverage = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMin = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMax = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedAverage = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMin = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedCurrent = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMax = table.Column<int>(type: "integer", nullable: false),
                    TemperatureAverage = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMin = table.Column<int>(type: "integer", nullable: false),
                    TemperatureCurrent = table.Column<int>(type: "integer", nullable: false),
                    HumidityMax = table.Column<int>(type: "integer", nullable: false),
                    HumidityAverage = table.Column<int>(type: "integer", nullable: false),
                    HumidityMin = table.Column<int>(type: "integer", nullable: false),
                    HumidityCurrent = table.Column<int>(type: "integer", nullable: false),
                    VOCMax = table.Column<int>(type: "integer", nullable: false),
                    VOCAverage = table.Column<int>(type: "integer", nullable: false),
                    VOCMin = table.Column<int>(type: "integer", nullable: false),
                    VOCCurrent = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMax = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureAverage = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMin = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureCurrent = table.Column<int>(type: "integer", nullable: false),
                    IsCalibratingCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    IsPowerOnCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    RTCNeedsTimeSyncCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Reserved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorMinute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentalSensorQuarter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceIdNet = table.Column<string>(type: "text", nullable: true),
                    DeviceIdNode = table.Column<string>(type: "text", nullable: true),
                    UpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MN = table.Column<string>(type: "text", nullable: true),
                    MNReverse = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Max = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Average = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Min = table.Column<int>(type: "integer", nullable: false),
                    PM2_5Current = table.Column<int>(type: "integer", nullable: false),
                    PM10Max = table.Column<int>(type: "integer", nullable: false),
                    PM10Average = table.Column<int>(type: "integer", nullable: false),
                    PM10Min = table.Column<int>(type: "integer", nullable: false),
                    PM10Current = table.Column<int>(type: "integer", nullable: false),
                    CPMMax = table.Column<int>(type: "integer", nullable: false),
                    CPMAverage = table.Column<int>(type: "integer", nullable: false),
                    CPMMin = table.Column<int>(type: "integer", nullable: false),
                    CPMCurrent = table.Column<int>(type: "integer", nullable: false),
                    NoiseMax = table.Column<int>(type: "integer", nullable: false),
                    NoiseAverage = table.Column<int>(type: "integer", nullable: false),
                    NoiseMin = table.Column<int>(type: "integer", nullable: false),
                    NoiseCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMax = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionAverage = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionMin = table.Column<int>(type: "integer", nullable: false),
                    WindDirectionCurrent = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMax = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedAverage = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedMin = table.Column<int>(type: "integer", nullable: false),
                    WindSpeedCurrent = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMax = table.Column<int>(type: "integer", nullable: false),
                    TemperatureAverage = table.Column<int>(type: "integer", nullable: false),
                    TemperatureMin = table.Column<int>(type: "integer", nullable: false),
                    TemperatureCurrent = table.Column<int>(type: "integer", nullable: false),
                    HumidityMax = table.Column<int>(type: "integer", nullable: false),
                    HumidityAverage = table.Column<int>(type: "integer", nullable: false),
                    HumidityMin = table.Column<int>(type: "integer", nullable: false),
                    HumidityCurrent = table.Column<int>(type: "integer", nullable: false),
                    VOCMax = table.Column<int>(type: "integer", nullable: false),
                    VOCAverage = table.Column<int>(type: "integer", nullable: false),
                    VOCMin = table.Column<int>(type: "integer", nullable: false),
                    VOCCurrent = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMax = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureAverage = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureMin = table.Column<int>(type: "integer", nullable: false),
                    AtmosPressureCurrent = table.Column<int>(type: "integer", nullable: false),
                    IsCalibratingCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    IsPowerOnCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    RTCNeedsTimeSyncCurrent = table.Column<bool>(type: "boolean", nullable: false),
                    Reserved = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentalSensorQuarter", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorDaily_DeviceIdNet",
                table: "EnvironmentalSensorDaily",
                column: "DeviceIdNet");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorHour_DeviceIdNet",
                table: "EnvironmentalSensorHour",
                column: "DeviceIdNet");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorMinute_DeviceIdNet",
                table: "EnvironmentalSensorMinute",
                column: "DeviceIdNet");

            migrationBuilder.CreateIndex(
                name: "IX_EnvironmentalSensorQuarter_DeviceIdNet",
                table: "EnvironmentalSensorQuarter",
                column: "DeviceIdNet");
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
        }
    }
}
