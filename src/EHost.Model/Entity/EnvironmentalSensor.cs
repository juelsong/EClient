namespace EHost.Contract.Entity
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 标准协议对应的数据库结构
    /// </summary>
    public class EnvironmentalSensor
    {
        [Key]
        public long Id { get; set; }
        public string DeviceIdNet { get; set; }
        public string DeviceIdNode { get; set; }

        public DateTime UpdateDate { get; set; }
        public string MN { get; set; }
        /// <summary>
        /// MN 剩余保留
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
    }

}
