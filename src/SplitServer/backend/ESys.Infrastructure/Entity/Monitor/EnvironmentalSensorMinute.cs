namespace ESys.Infrastructure.Entity
{
#pragma warning disable 1591

    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 天数据
    /// </summary>

    [AuditDisable]
    partial class EnvironmentalSensorMinute : BizEntity<EnvironmentalSensorMinute, long>
    {

        // 主键
        [Key]
        [Column("id")]
        public override long Id { get; set; }
        [Column("device_of_data")]
        public int EquipmentId { get; set; }

        [Column("update_time")]
        public DateTimeOffset Date { get; set; }


        [Column("pm2_5_average")]
        public decimal pm2_5_average { get; set; }
        [Column("pm2_5_max")]
        public decimal pm2_5_max { get; set; }
        [Column("pm2_5_min")]
        public decimal pm2_5_min { get; set; }

        [Column("pm10_average")]
        public decimal pm10_average { get; set; }
        [Column("pm10_max")]
        public decimal pm10_max { get; set; }
        [Column("pm10_min")]
        public decimal pm10_min { get; set; }

        /// <summary>
        /// 扬尘TSP（单位：mg/m³）
        /// </summary>
        [Column("tsp_min")]
        public decimal ParticulateMatterMin { get; set; }
        [Column("tsp_max")]
        public decimal ParticulateMatterMax { get; set; }
        [Column("tsp_average")]
        public decimal ParticulateMatterAvg { get; set; }

        /// <summary>
        /// 温度（单位：℃）
        /// </summary>
        [Column("temperature_min")]
        public decimal TemperatureMin { get; set; }
        [Column("temperature_max")]
        public decimal TemperatureMax { get; set; }
        [Column("temperature_average")]
        public decimal TemperatureAvg { get; set; }

        /// <summary>
        /// 湿度（单位：Rh%）
        /// </summary>
        [Column("humidity_min")]
        public decimal HumidityMin { get; set; }
        [Column("humidity_max")]
        public decimal HumidityMax { get; set; }
        [Column("humidity_average")]
        public decimal HumidityAvg { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        [Column("pre_min")]
        public decimal AirPressureMin { get; set; }
        [Column("pre_max")]
        public decimal AirPressureMax { get; set; }
        [Column("pre_avg")]
        public decimal AirPressureAvg { get; set; }

        /// <summary>
        /// 风速（单位：m/s）
        /// </summary>
        [Column("wind_speed_min")]
        public decimal WindSpeedMin { get; set; }
        [Column("wind_speed_max")]
        public decimal WindSpeedMax { get; set; }
        [Column("wind_speed_average")]
        public decimal WindSpeedAvg { get; set; }

        /// <summary>
        /// 风向（单位：°）
        /// </summary>
        [Column("wind_direction_min")]
        public decimal WindDirectionMin { get; set; }
        [Column("wind_direction_max")]
        public decimal WindDirectionMax { get; set; }
        [Column("wind_direction_average")]
        public decimal WindDirectionAvg { get; set; }

        /// <summary>
        /// 噪声（单位：dB）
        /// </summary>
        [Column("noise_min")]
        public decimal NoiseMin { get; set; }
        [Column("noise_max")]
        public decimal NoiseMax { get; set; }
        [Column("noise_average")]
        public decimal NoiseAvg { get; set; }

        /// <summary>
        /// CPM
        /// </summary>
        [Column("cpm")]
        public decimal CPMAvg { get; set; }
        [Column("is_power_on")]
        public bool is_power_on { get; set; }
        [Column("is_valid_data")]
        public bool is_valid_data { get; set; }
        [Column("equipment_data_valid_flag")]
        public decimal equipment_data_valid_flag { get; set; }
        [Column("source_of_data")]
        public decimal source_of_data { get; set; }
        [Column("dust_calibration_step")]
        public decimal dust_calibration_step { get; set; }


        public override void Configure(EntityTypeBuilder<EnvironmentalSensorMinute> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(p => p.EquipmentId);
        }
    }
}
