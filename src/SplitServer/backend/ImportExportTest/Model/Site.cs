namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 采样点
    /// </summary>
    public class Site : ImportBase
    {
        public Site()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Site(int row) : base(row)
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 洁净级别
        /// </summary>
        public string Classification { get; set; }
        /// <summary>
        /// 第二洁净级别
        /// </summary>
        public string SecondClassification { get; set; }


        /// <summary>
        /// 上一层区域
        /// </summary>
        public string ParentLocation { get; set; }
        /// <summary>
        /// 条码
        /// </summary>

        public string Barcode { get; set; }

    }
}
