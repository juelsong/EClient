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
    using System.Text.Json.Nodes;

    /// <summary>
    /// 节点
    /// </summary>
    public class Node : Cell
    {
        /// <summary>
        /// 开始节点图形名
        /// </summary>
        public const string StartShape = "Start";
        /// <summary>
        /// 节点链接桩
        /// </summary>
        public NodePorts Ports { get; set; }
        /// <summary>
        /// 数据 一般为json
        /// </summary>
        public JsonNode Data { get; set; }
        /// <summary>
        /// 视图 目前没用
        /// </summary>
        public string View { get; set; }
    }
}