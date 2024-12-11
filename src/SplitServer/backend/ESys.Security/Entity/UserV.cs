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

namespace ESys.Security.Entity
{
    using ESys.Contract.Entity;
    using System;

    /// <summary>
    /// 用户视图
    /// </summary>
    public class UserV : BizView<UserV>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 所属部门Id
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// 区域名
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        /// 区域路径
        /// </summary>
        public string LocationFullName { get; set; }
        /// <summary>
        /// 区域Id面包屑导航
        /// </summary>
        public string LocationBreadcrumb { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 电邮
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public UserStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasAccess { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
    }
}
