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

namespace ESys.Utilty.Defs
{
    /// <summary>
    /// 首页布局
    /// </summary>
    public class DashboardLayout
    {
        /// <summary>
        /// xs 响应尺寸
        /// </summary>
        public int? XS { get; set; }
        /// <summary>
        /// sm 响应尺寸
        /// </summary>
        public int? SM { get; set; }
        /// <summary>
        /// md 响应尺寸
        /// </summary>
        public int? MD { get; set; }
        /// <summary>
        /// lg 响应尺寸
        /// </summary>
        public int? LG { get; set; }
        /// <summary>
        /// xl 响应尺寸
        /// </summary>
        public int? XL { get; set; }
        /// <summary>
        /// 间距
        /// </summary>
        public int Margin { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }
    }

    /// <summary>
    /// 用户首页布局
    /// </summary>
    public class UserDashboardLayout : DashboardLayout
    {
        /// <summary>
        /// 内容代码
        /// </summary>
        public string[] ContentCode { get; set; }
    }
}
