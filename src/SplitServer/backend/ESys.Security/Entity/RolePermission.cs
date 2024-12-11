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

using ESys.Contract.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ESys.Security.Entity
{
    /// <summary>
    /// 角色权限关联
    /// </summary>
    public partial class RolePermission : BizEntity<RolePermission, int>, ITraceableEntity, ITimedEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public Permission Permission { get; set; }
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
        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<RolePermission> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            for (var i = 0; i < 4;)
            {
                i++;
                yield return new RolePermission()
                {
                    Id = i,
                    RoleId = 2,
                    PermissionId = i
                };
            }
        }
    }
}
