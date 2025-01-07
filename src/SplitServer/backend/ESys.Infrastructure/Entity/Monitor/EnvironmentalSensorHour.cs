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
    /// 小时
    /// </summary>
    [AuditDisable]
    partial class EnvironmentalSensorHour :  BizEntity<EnvironmentalSensorHour, long>
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
        public int pm2_5_average { get; set; }
        [Column("pm2_5_max")]
        public int pm2_5_max { get; set; }
        [Column("pm2_5_min")]
        public int pm2_5_min { get; set; }

        [Column("pm10_average")]
        public int pm10_average { get; set; }
        [Column("pm10_max")]
        public int pm10_max { get; set; }
        [Column("pm10_min")]
        public int pm10_min { get; set; }

        /// <summary>
        /// 扬尘TSP（单位：mg/m³）
        /// </summary>
        [Column("tsp_min")]
        public int ParticulateMatterMin { get; set; }
        [Column("tsp_max")]
        public int ParticulateMatterMax { get; set; }
        [Column("tsp_average")]
        public int ParticulateMatterAvg { get; set; }

        /// <summary>
        /// 温度（单位：℃）
        /// </summary>
        [Column("temperature_min")]
        public int TemperatureMin { get; set; }
        [Column("temperature_max")]
        public int TemperatureMax { get; set; }
        [Column("temperature_average")]
        public int TemperatureAvg { get; set; }

        /// <summary>
        /// 湿度（单位：Rh%）
        /// </summary>
        [Column("humidity_min")]
        public int HumidityMin { get; set; }
        [Column("humidity_max")]
        public int HumidityMax { get; set; }
        [Column("humidity_average")]
        public int HumidityAvg { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        [Column("pre_min")]
        public int AirPressureMin { get; set; }
        [Column("pre_max")]
        public int AirPressureMax { get; set; }
        [Column("pre_avg")]
        public int AirPressureAvg { get; set; }

        /// <summary>
        /// 风速（单位：m/s）
        /// </summary>
        [Column("wind_speed_min")]
        public int WindSpeedMin { get; set; }
        [Column("wind_speed_max")]
        public int WindSpeedMax { get; set; }
        [Column("wind_speed_average")]
        public int WindSpeedAvg { get; set; }

        /// <summary>
        /// 风向（单位：°）
        /// </summary>
        [Column("wind_direction_min")]
        public int WindDirectionMin { get; set; }
        [Column("wind_direction_max")]
        public int WindDirectionMax { get; set; }
        [Column("wind_direction_average")]
        public int WindDirectionAvg { get; set; }

        /// <summary>
        /// 噪声（单位：dB）
        /// </summary>
        [Column("noise_min")]
        public int NoiseMin { get; set; }
        [Column("noise_max")]
        public int NoiseMax { get; set; }
        [Column("noise_average")]
        public int NoiseAvg { get; set; }

        /// <summary>
        /// CPM
        /// </summary>
        [Column("cpm")]
        public int CPMAvg { get; set; }
        [Column("is_power_on")]
        public bool is_power_on { get; set; }
        [Column("is_valid_data")]
        public bool is_valid_data { get; set; }
        [Column("equipment_data_valid_flag")]
        public int equipment_data_valid_flag { get; set; }
        [Column("source_of_data")]
        public int source_of_data { get; set; }
        [Column("dust_calibration_step")]
        public int dust_calibration_step { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<EnvironmentalSensorHour> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            //entityBuilder.HasOne(p => p.Equipment)
            //    .WithMany(e => e.EquipmentTPMs)
            //    .HasForeignKey(i => i.EquipmentId)
            //    .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasIndex(p => p.EquipmentId);

        }
    }
}
