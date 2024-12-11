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
    using System.Collections.Generic;
    using WorkflowCore.Models.DefinitionStorage.v1;

    /// <summary>
    /// 步骤Builder
    /// </summary>
    public interface IStepBuilder
    {
        /// <summary>
        /// 是否能构建
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool CanBuild(Node node);
        /// <summary>
        /// Build
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        IEnumerable<StepSourceV1> Build( Graph graph, Node node);
    }
}
