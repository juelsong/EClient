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
    /// 用于移植MN 数据
    /// </summary>
    [AuditDisable]
    [Table("monitor_device")]
    public partial class MonitorDevice : BizEntity<MonitorDevice, int>
    {
        [Key]
        [Column("id")]
        public override int Id { get; set; }

        [Column("device_id")]
        public string DeviceId { get; set; }

        [Column("device_name")]
        [MaxLength(256)]
        public string DeviceName { get; set; } = null!;

        [Column("mn_code")]
        [MaxLength(256)]
        public string MnCode { get; set; } = null!;

        [Column("site_of_device")]
        public int SiteOfDevice { get; set; }

        [Column("device_card_tel_number_1")]
        [MaxLength(64)]
        public string DeviceCardTelNumber1 { get; set; } = null!;

        [Column("device_card_tel_number_2")]
        [MaxLength(64)]
        public string DeviceCardTelNumber2 { get; set; } = null!;

        [Column("config_dust_k")]
        public double ConfigDustK { get; set; }

        [Column("config_noise_zero_offset")]
        public int ConfigNoiseZeroOffset { get; set; }

        [Column("config_noise_k")]
        public double ConfigNoiseK { get; set; }

        [Column("config_dust_early_warning_enable")]
        public bool ConfigDustEarlyWarningEnable { get; set; }

        [Column("config_dust_early_warning_max")]
        public int ConfigDustEarlyWarningMax { get; set; }

        [Column("config_dust_early_warning_min")]
        public int ConfigDustEarlyWarningMin { get; set; }

        [Column("config_noise_early_warning_enable")]
        public bool ConfigNoiseEarlyWarningEnable { get; set; }

        [Column("config_noise_early_warning_max")]
        public int ConfigNoiseEarlyWarningMax { get; set; }

        [Column("config_noise_early_warning_min")]
        public int ConfigNoiseEarlyWarningMin { get; set; }

        [Column("config_dust_warning_max")]
        public int ConfigDustWarningMax { get; set; }

        [Column("config_dust_warning_min")]
        public int ConfigDustWarningMin { get; set; }

        [Column("config_noise_warning_max")]
        public int ConfigNoiseWarningMax { get; set; }

        [Column("config_noise_warning_min")]
        public int ConfigNoiseWarningMin { get; set; }

        [Column("is_in_use")]
        public bool IsInUse { get; set; }

        [Column("production_date")]
        public DateTime ProductionDate { get; set; }

        [Column("config_dust_early_warning_auto")]
        public bool ConfigDustEarlyWarningAuto { get; set; }

        [Column("config_dust_warning_auto")]
        public bool ConfigDustWarningAuto { get; set; }

        [Column("config_dust_warning_auto_mode")]
        public int ConfigDustWarningAutoMode { get; set; }

        [Column("video_device_serial")]
        [MaxLength(64)]
        public string VideoDeviceSerial { get; set; } = null!;

        [Column("video_device_verification_code")]
        [MaxLength(64)]
        public string VideoDeviceVerificationCode { get; set; } = null!;

        [Column("auto_spray_threshold")]
        public int AutoSprayThreshold { get; set; }

        [Column("jw_mn_code")]
        [MaxLength(256)]
        public string JwMnCode { get; set; } = null!;

        [Column("noise_night_alert")]
        public bool NoiseNightAlert { get; set; }

        [Column("dust_early_warning_command")]
        [MaxLength(16)]
        public string DustEarlyWarningCommand { get; set; } = null!;

        [Column("dust_k_frontend")]
        public double DustKFrontend { get; set; }

        [Column("spray_mode")]
        public int SprayMode { get; set; }

        [Column("protocol")]
        public int Protocol { get; set; }

        [Column("door_status")]
        public int DoorStatus { get; set; }

        [Column("lock_mode")]
        public int LockMode { get; set; }

        [Column("lock_id")]
        public string LockId { get; set; }

        [Column("range_data_analyse_en")]
        public int RangeDataAnalyseEn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<MonitorDevice> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(p => p.DeviceId);
            entityBuilder.HasIndex(p => p.DeviceName);
        }
    }
}