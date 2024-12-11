using CN.Metaura.EMIS.Contract.Entity;
using EHost.Contract.Entity;
using System.Text.Json.Serialization;

namespace EHost.Security.Entity
{
    public  class EntityBase<TKey> : IKeyEntity<TKey>
    {

        #region interfaces
        /// <summary>
        /// ID
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        #endregion interfaces
    }
}
