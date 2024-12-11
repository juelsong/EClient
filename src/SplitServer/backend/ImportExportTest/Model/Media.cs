namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 试剂耗材
    /// </summary>
    public class Media : ImportBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Media()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Media(int row) : base(row)
        {
        }

        /// <summary>
        ///  耗材类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  批号
        /// </summary>
        public string LotNumber { get; set; }
        /// <summary>
        ///  有效期
        /// </summary>
        public DateTimeOffset LotNumberExpDate { get; set; }

        /// <summary>
        ///  条码
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        ///  区域
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        ///  供应商
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        ///  供应商批号
        /// </summary>
        public string ManufacturerLotNumber { get; set; }
        /// <summary>
        ///  促生长状态
        /// </summary>
        public string GrowthPromotionStatus { get; set; }
        /// <summary>
        ///  促生长结果
        /// </summary>
        public string GrowthPromotionResult { get; set; }

    }
}
