namespace ESys.Infrastructure.Entity
{
    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// 15分钟
    /// </summary>
    [AuditDisable]
    partial class EnvironmentalSensorQuarter : BizEntity<EnvironmentalSensorQuarter, long>
    {
        /// <summary>
        /// 设备ID-Net(高字节在前)
        /// </summary>
        public string DeviceIdNet { get; set; }

        /// <summary>
        /// 设备ID-Node(高字节在前)
        /// </summary>
        public string DeviceIdNode { get; set; }
        /// <summary>
        /// 年月日时分秒毫秒(高字节在前)
        /// </summary>

        public DateTimeOffset UpdateDate { get; set; }
        /// <summary>
        /// 14字节MN码
        /// </summary>
        public string MN { get; set; }
        /// <summary>
        /// MN 剩余保留 +2预留字节(高字节在前)
        /// </summary>
        public ushort MNReverse { get; set; }


        // PM2.5 相关属性
        /// <summary>
        /// PM2.5颗粒物浓度的最大值
        /// </summary>
        public ushort PM2_5Max { get; set; }
        /// <summary>
        /// PM2.5颗粒物浓度的平均值
        /// </summary>
        public ushort PM2_5Average { get; set; }
        /// <summary>
        /// PM2.5颗粒物浓度的最小值
        /// </summary>
        public ushort PM2_5Min { get; set; }
        /// <summary>
        /// PM2.5颗粒物浓度的最近值
        /// </summary>
        public ushort PM2_5Current { get; set; }

        // PM10 相关属性
        /// <summary>
        /// PM10颗粒物浓度的最大值
        /// </summary>
        public ushort PM10Max { get; set; }
        /// <summary>
        /// PM10颗粒物浓度的平均值
        /// </summary>
        public ushort PM10Average { get; set; }
        /// <summary>
        /// PM10颗粒物浓度的最小值
        /// </summary>
        public ushort PM10Min { get; set; }
        /// <summary>
        /// PM10颗粒物浓度的最近值
        /// </summary>
        public ushort PM10Current { get; set; }

        // CPM 相关属性
        /// <summary>
        /// CPM（计数每分钟）的最大值
        /// </summary>
        public ushort CPMMax { get; set; }
        /// <summary>
        /// CPM（计数每分钟）的平均值
        /// </summary>
        public ushort CPMAverage { get; set; }
        /// <summary>
        /// CPM（计数每分钟）的最小值
        /// </summary>
        public ushort CPMMin { get; set; }
        /// <summary>
        /// CPM（计数每分钟）的最近值
        /// </summary>
        public ushort CPMCurrent { get; set; }

        // 噪音相关属性
        /// <summary>
        /// 噪音水平的最大值
        /// </summary>
        public ushort NoiseMax { get; set; }
        /// <summary>
        /// 噪音水平的平均值
        /// </summary>
        public ushort NoiseAverage { get; set; }
        /// <summary>
        /// 噪音水平的最小值
        /// </summary>
        public ushort NoiseMin { get; set; }
        /// <summary>
        /// 噪音水平的最近值
        /// </summary>
        public ushort NoiseCurrent { get; set; }

        // 风向相关属性
        /// <summary>
        /// 风向的最近值
        /// </summary>
        public ushort WindDirectionMax { get; set; }
        public ushort WindDirectionAverage { get; set; }
        public ushort WindDirectionMin { get; set; }
        public ushort WindDirectionCurrent { get; set; }
        // 风速相关属性
        /// <summary>
        /// 风速的最大值
        /// </summary>
        public ushort WindSpeedMax { get; set; }
        /// <summary>
        /// 风速的平均值
        /// </summary>
        public ushort WindSpeedAverage { get; set; }
        /// <summary>
        /// 风速的最小值
        /// </summary>
        public ushort WindSpeedMin { get; set; }
        /// <summary>
        /// 风速的最近值
        /// </summary>
        public ushort WindSpeedCurrent { get; set; }

        // 温度相关属性
        /// <summary>
        /// 温度的最大值
        /// </summary>
        public ushort TemperatureMax { get; set; }
        /// <summary>
        /// 温度的平均值
        /// </summary>
        public ushort TemperatureAverage { get; set; }
        /// <summary>
        /// 温度的最小值
        /// </summary>
        public ushort TemperatureMin { get; set; }
        /// <summary>
        /// 温度的最近值
        /// </summary>
        public ushort TemperatureCurrent { get; set; }

        // 湿度相关属性
        /// <summary>
        /// 湿度的最大值
        /// </summary>
        public ushort HumidityMax { get; set; }
        /// <summary>
        /// 湿度的平均值
        /// </summary>
        public ushort HumidityAverage { get; set; }
        /// <summary>
        /// 湿度的最小值
        /// </summary>
        public ushort HumidityMin { get; set; }
        /// <summary>
        /// 湿度的最近值
        /// </summary>
        public ushort HumidityCurrent { get; set; }

        // VOC相关属性
        /// <summary>
        /// VOC（挥发性有机化合物）的最大值
        /// </summary>
        public ushort VOCMax { get; set; }
        /// <summary>
        /// VOC（挥发性有机化合物）的平均值
        /// </summary>
        public ushort VOCAverage { get; set; }
        /// <summary>
        /// VOC（挥发性有机化合物）的最小值
        /// </summary>
        public ushort VOCMin { get; set; }
        /// <summary>
        /// VOC（挥发性有机化合物）的最近值
        /// </summary>
        public ushort VOCCurrent { get; set; }
        // 大气压相关属性
        /// <summary>
        /// 大气压的最大值
        /// </summary>
        public ushort AtmosPressureMax { get; set; }
        /// <summary>
        /// 大气压的平均值
        /// </summary>
        public ushort AtmosPressureAverage { get; set; }
        /// <summary>
        /// 大气压的最小值
        /// </summary>
        public ushort AtmosPressureMin { get; set; }
        /// <summary>
        /// 大气压的最近值
        /// </summary>
        public ushort AtmosPressureCurrent { get; set; }


        // 电源输入相关属性
        /// <summary>
        /// 设备是否正在校准的最新状态
        /// </summary>
        public bool IsCalibratingCurrent { get; set; }
        /// <summary>
        /// 电源是否开启的最新状态
        /// </summary>
        public bool IsPowerOnCurrent { get; set; }
        /// <summary>
        /// RTC（实时时钟）是否需要校时的最新状态
        /// </summary>
        public bool RTCNeedsTimeSyncCurrent { get; set; }
        /// <summary>
        /// 保留字段，用于未来的功能扩展 (高字节在前)
        /// </summary>
        public ushort Reserved { get; set; }

        ///// <summary>
        ///// 设备Id
        ///// </summary>
        //public int? EquipmentId { get; set; }

        ///// <summary>
        /////设备
        ///// </summary>
        //public virtual Equipment Equipment { get; set; }


        public override void Configure(EntityTypeBuilder<EnvironmentalSensorQuarter> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(p => p.DeviceIdNet);
        }
    }
}
