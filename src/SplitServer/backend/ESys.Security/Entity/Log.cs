
namespace ESys.Security.Entity
{
    using ESys.DataAnnotations;
    using ESys.Contract.Entity;
    using System;

    /// <summary>
    /// 日志功能实体
    /// </summary>
    [AuditDisable]
    public partial class Log : BizEntity<Log, long>, ITimedEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public string Description { get; set; }

        #region interfaces
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
        #endregion interfaces
    }
}
