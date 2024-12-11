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

using ESys.DataAnnotations;
using Furion.DatabaseAccessor;
using System;

namespace ESys.Contract.Entity
{
    /// <summary>
    /// 审计操作
    /// </summary>
    [ODataConfig]
    public enum AuditAction : byte
    {
        /// <summary>
        /// 增加
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update,
        /// <summary>
        /// 删除
        /// </summary>
        Delete
    }

    /// <summary>
    /// 审计实体基类
    /// </summary>
    /// <typeparam name="TAudit"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    [AuditDisable]
    [SuppressChangedListener]
    public abstract class AuditEntity<TAudit, TEntity, TKey>
          : BizEntity<TAudit, long>
          where TEntity : BizEntity<TEntity, TKey>, new()
          where TAudit : class, IPrivateEntity, new()
          where TKey : struct, IConvertible
    {
        /// <summary>
        /// 实体Id
        /// </summary>
        public TKey EntityId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTimeOffset AuditTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 操作
        /// </summary>
        public AuditAction Action { get; set; }

        /// <summary>
        /// 转储基本属性，结构、字符串
        /// </summary>
        /// <param name="to"></param>
        /// <param name="includeId"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void DumpBasicProperty(TAudit to, bool includeId)
        {
            throw new NotImplementedException();
        }
    }
}
