namespace ESys.ImportExport.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 菌种
    /// </summary>
    public class Organism:ImportBase
    {

        /// <summary>
        /// 构造
        /// </summary>
        public Organism()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public Organism(int row) : base(row)
        {
        }
        /// <summary>
        /// 类别，不作为插入判断数据
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 微生物菌属
        /// </summary>
        public string OrganismGenus { get; set; }
        /// <summary>
        /// 菌种名称 种属
        /// </summary>
        [StringLength(100)]
        public string Species { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 菌株品系
        /// </summary>
        public string Strain { get; set; }

        /// <summary>
        /// 微生物致病性
        /// </summary>
        public string OrganismSeverity { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string SeverityLocation { get; set; }

    }
}
