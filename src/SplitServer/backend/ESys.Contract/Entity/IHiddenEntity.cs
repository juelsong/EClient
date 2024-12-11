namespace ESys.Contract.Entity
{
    /// <summary>
    /// 可隐藏实体
    /// </summary>
    public interface IHiddenEntity
    {
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
