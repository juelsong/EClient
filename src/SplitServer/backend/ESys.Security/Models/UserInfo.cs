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

namespace ESys.Security.Models
{
    using ESys.Security.Service;
    using ESys.Utilty.Defs;
    using global::System;
    using global::System.Collections.Generic;

    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
    /// <summary>
    /// 用户角色信息
    /// </summary>
    public class UserRoleInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 用户配置
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// 首页配置
        /// </summary>
        public UserDashboardLayout Dashboard { get; set; }
        /// <summary>
        /// 用户设置
        /// </summary>
        public UserSettings UserSettings { get; set; }
        /// <summary>
        /// 语言设置
        /// </summary>
        public string Locale { get; set; }
    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 管理区域
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserManagementMode UserManagementMode { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public IEnumerable<UserPermission> Permissions { get; set; } = Array.Empty<UserPermission>();
        /// <summary>
        /// 角色
        /// </summary>
        public IEnumerable<UserRoleInfo> Roles { get; set; } = Array.Empty<UserRoleInfo>();

        /// <summary>
        /// 用户配置
        /// </summary>
        public Profile Profile { get; set; } = new Profile();
    }
}
