namespace EHost.Infrastructure.Entity.Environment
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 2024年11月25日17:17:42
    /// 新提供的数据类型
    /// </summary>
    public class FugitiveDust
    {
        // 主键
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        /// <summary>
        /// 请求编号QN 精确到毫秒的时间戳 用来唯一标识一个命令请求，用于请求命令或通知命令 
        /// </summary>
        public string QuestCode { get; set; }
        /// <summary>
        /// ST=系统编号  
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 访问密码PW 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// MN=设备编号  
        /// </summary>
        public string EquipmentCode { get; set; }

        /// <summary>
        /// 温度（单位：℃）
        /// </summary>
        public decimal TemperatureMin { get; set; }
        public decimal TemperatureMax { get; set; }
        public decimal TemperatureAvg { get; set; }
        public char TemperatureFlag { get; set; }

        /// <summary>
        /// 湿度（单位：Rh%）
        /// </summary>
        public decimal HumidityMin { get; set; }
        public decimal HumidityMax { get; set; }
        public decimal HumidityAvg { get; set; }
        public char HumidityFlag { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public decimal AirPressureMin { get; set; }
        public decimal AirPressureMax { get; set; }
        public decimal AirPressureAvg { get; set; }
        public char AirPressureFlag { get; set; }

        /// <summary>
        /// 风速（单位：m/s）
        /// </summary>
        public decimal WindSpeedMin { get; set; }
        public decimal WindSpeedMax { get; set; }
        public decimal WindSpeedAvg { get; set; }
        public char WindSpeedFlag { get; set; }

        /// <summary>
        /// 风向（单位：°）
        /// </summary>
        public decimal WindDirectionMin { get; set; }
        public decimal WindDirectionMax { get; set; }
        public decimal WindDirectionAvg { get; set; }
        public char WindDirectionFlag { get; set; }

        /// <summary>
        /// 扬尘TSP（单位：mg/m³）
        /// </summary>
        public decimal ParticulateMatterMin { get; set; }
        public decimal ParticulateMatterMax { get; set; }
        public decimal ParticulateMatterAvg { get; set; }
        public char ParticulateMatterFlag { get; set; }

        /// <summary>
        /// 噪声（单位：dB）
        /// </summary>
        public decimal NoiseMin { get; set; }
        public decimal NoiseMax { get; set; }
        public decimal NoiseAvg { get; set; }
        public char NoiseFlag { get; set; }
        /// <summary>
        /// CPM
        /// </summary>
        public decimal CPMMin { get; set; }
        public decimal CPMMax { get; set; }
        public decimal CPMAvg { get; set; }
        public char CPMFlag { get; set; }
        /// <summary>
        /// 原值
        /// </summary>
        [NotMapped]
        public string OriginData { get; set; }
    }
}
