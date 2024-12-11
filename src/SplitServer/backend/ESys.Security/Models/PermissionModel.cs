/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Security.Models
{
    using ESys.Security.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 权限参数
    /// </summary>
    public class PermissionModel
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public PermissionType Type { get; set; } = PermissionType.Menu;
        /// <summary>
        /// 子权限
        /// </summary>
        public List<PermissionModel> SubPermissions { get; set; } = new();

        /// <summary>
        /// 从模型获取实体
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Permission GetPermission(ref int order)
        {
            var ret = new Permission()
            {
                Code = this.Code ?? string.Empty,
                Description = this.Desc,
                Type = this.Type,
                Order = order++,
            };
            if (this.SubPermissions != null)
            {
                foreach (var item in this.SubPermissions)
                {
                    ret.SubPermissions.Add(item.GetPermission(ref order));
                }
            }
            return ret;
        }
    }
}
