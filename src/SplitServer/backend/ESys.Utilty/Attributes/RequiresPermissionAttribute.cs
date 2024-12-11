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

using System;

namespace ESys.Utilty.Attributes
{
    /// <summary>
    /// 需要权限特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RequiresPermissionAttribute : Attribute
    {
        /// <summary>
        /// 权限数组
        /// </summary>
        public string[] Permissions { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissions"></param>
        public RequiresPermissionAttribute(params string[] permissions)
        {
            this.Permissions = permissions;
        }
    }
}
