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
    using System;
    using System.Linq;
    /// <summary>
    /// 图
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// 图形
        /// </summary>
        public Cell[] Cells { get; set; } = Array.Empty<Cell>();
        /// <summary>
        /// 获取下一个节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public Node GetNextNode(Node node, Port port)
        {
            var edge = this.Cells
                             .OfType<Edge>()
                             .FirstOrDefault(e => e.Source.Cell == node.Id
                                         && e.Source.Port == port.Id);
            return edge == null 
                ? null 
                : this.Cells.OfType<Node>().FirstOrDefault(n => n.Id == edge.Target.Cell);
        }
        /// <summary>
        /// 获取直接后续节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node[] FindNextNodes(Node node)
        {
            var outputPortIds = node.Ports.Items.Where(p => p.Group.Contains(Port.NextPortGroup)).Select(p => p.Id).ToArray();
            var edges = this.Cells
                             .OfType<Edge>()
                             .Where(e => e.Source.Cell == node.Id
                                         && outputPortIds.Contains(e.Source.Port));
            return edges.Select(edge => this.Cells.OfType<Node>().FirstOrDefault(n => n.Id == edge.Target.Cell)).ToArray();
        }
    }
}
