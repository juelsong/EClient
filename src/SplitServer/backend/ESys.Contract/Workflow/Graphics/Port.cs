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

namespace CN.Metaura.EMIS.Contract.Workflow
{
    /// <summary>
    /// 链接桩
    /// </summary>
    public class Port
    {
        /// <summary>
        /// 下一项组名
        /// </summary>
        public const string NextPortGroup = "next";
        /// <summary>
        /// 上一项组名
        /// </summary>
        public const string PreviousPortGroup = "previous";
        /// <summary>
        /// false项组名
        /// </summary>
        public const string FalsePortGroup = "false";
        /// <summary>
        /// 标识
        /// </summary>
        public string Id{get;set;}
        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; set; }
    }
}