/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑,代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Contract.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 无法禁用原因
    /// </summary>
    public class DeactivateAgainst
    {
        /// <summary>
        /// 实体类型
        /// </summary>
        [JsonIgnore]
        public Type EntityType { get; set; }

        /// <summary>
        /// 导航属性
        /// </summary>
        public string NavigationProperty { get; set; }

        /// <summary>
        /// 实体值
        /// </summary>
        public IEnumerable<string> Values { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// 实体类型名称
        /// </summary>
        public string EntityName
        {
            get
            {
                return this.EntityType?.FullName;
            }
        }
    }

    ///// <summary>
    ///// 禁用检查器
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public interface IDeactiveChecker<T> where T : IActiveEntity
    //{
    //    /// <summary>
    //    /// 检查是否能禁用
    //    /// </summary>
    //    /// <param name="source">数据源</param>
    //    /// <param name="key">主键</param>
    //    /// <returns></returns>
    //    IReadOnlyList<DeactivateAgainst> CheckCanDeactive(IQueryable<T> source, object key);
    //}

}
