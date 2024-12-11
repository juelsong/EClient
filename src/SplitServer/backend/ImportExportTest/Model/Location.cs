namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 区域
    /// </summary>
    public class Location : ImportBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        public Location()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Location(int row) : base(row)
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
    ///// <summary>
    ///// 区域基础数据
    ///// </summary>
    //public class LocationBaseData
    //{
    //    /// <summary>
    //    /// 洁净级别
    //    /// </summary>
    //    public string[] Classifications { get; set; }
    //    /// <summary>
    //    /// 类型
    //    /// </summary>
    //    public LocationType[] LocationTypes { get; set; }
    //}
    ///// <summary>
    ///// 区域类型
    ///// </summary>
    //public class LocationType
    //{
    //    /// <summary>
    //    /// 类型名称
    //    /// </summary>
    //    public string Name { get; set; }
    //    /// <summary>
    //    /// 区域等级(权重)
    //    /// </summary>
    //    public int Level { get; set; }
    //    /// <summary>
    //    /// 数据
    //    /// </summary>
    //    public string[] DataDetails{ get; set; }

    //}
}
