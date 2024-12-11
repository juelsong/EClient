namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 设备
    /// </summary>
    public class Equipment:ImportBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Equipment()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Equipment(int row) : base(row)
        {
        }

        /// <summary>
        ///  设备类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  条码
        /// </summary>
        public string Barcode { get; set; }
        /// <summary>
        ///  单位
        /// </summary>
        public string Uom { get; set; }
        /// <summary>
        ///  区域
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        ///  校准日期
        /// </summary>
        public DateTimeOffset CalibrationDate { get; set; }
        /// <summary>
        ///  下次校准日期
        /// </summary>
        public DateTimeOffset NextCalibrationDate { get; set; }
        /// <summary>
        ///  标准值
        /// </summary>
        public string CalibrationValue { get; set; }
        /// <summary>
        ///  控制编号
        /// </summary>
        public string ControlNumber { get; set; }
        /// <summary>
        ///  序列号
        /// </summary>
        public string SerialNumber { get; set; }

    }
}
