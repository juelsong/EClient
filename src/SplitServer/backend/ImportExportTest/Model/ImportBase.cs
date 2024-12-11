namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// 插入数据的状态
    /// </summary>
    public enum InsertStatus
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        /// <summary>
        /// 必填项
        /// </summary>
        RequiredMissing,
        /// <summary>
        /// 数据错误
        /// </summary>
        DataError,
        /// <summary>
        /// 重名
        /// </summary>
        DuplicateName,
        /// <summary>
        /// 数据成功
        /// </summary>
        Success,
    }

    /// <summary>
    /// 导入数据
    /// </summary>
    public class ImportBase
    {
        public ImportBase()
        {
            // 默认构造函数，可以不进行任何操作
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public ImportBase(int row)
        {
            this.RowNumber = row;
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int RowNumber { get; set; }
        /// <summary>
        /// 插入数据状态
        /// </summary>
        public InsertStatus InsertStatus { get; set; }
        /// <summary>
        /// 错误信息描述
        /// </summary>
        public string ErrorDescription { get; set; }
    }
}
