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
    using System.Threading.Tasks;

    /// <summary>
    /// 工作流元数据
    /// </summary>
    /// <param name="Id">模板实体Id</param>
    /// <param name="WorkflowId">工作流Id</param>
    /// <param name="Version"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Category"></param>
    public record WorkflowMeta(long Id, string WorkflowId, int? Version, string Name, string Description, int? Category);

    /// <summary>
    /// 工作流参数
    /// </summary>
    /// <param name="WorkflowId">工作流Id</param>
    /// <param name="Version"></param>
    /// <param name="Name"></param>
    /// <param name="Description"></param>
    /// <param name="Category"></param>
    /// <param name="GraphJson"></param>
    public record WorkflowDefParameter(string WorkflowId, int? Version, string Name, string Description, int? Category, string GraphJson);

    /// <summary>
    /// 工作流执行器
    /// </summary>
    public interface IWorkflowExecutor
    {
        /// <summary>
        /// 执行工作流
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="version"></param>
        /// <param name="context"></param>
        /// <returns>成功返回工作流Id，null为是败</returns>
        Task<string> Execute(string workflowId, int? version, WorkflowContext context);
        /// <summary>
        /// 执行工作流
        /// </summary>
        /// <param name="workflowTemplateId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<string> Execute(long workflowTemplateId, WorkflowContext context);
        /// <summary>
        /// 获取工作流实例的执行参数
        /// </summary>
        /// <param name="workflowInstanceId"></param>
        /// <returns></returns>
        Task<WorkflowContext> GetContext(string workflowInstanceId);
        /// <summary>
        /// 获取工作流元数据
        /// </summary>
        /// <param name="workflowTemplateId"></param>
        /// <returns></returns>
        Task<WorkflowMeta> GetWorkflowMeta(long workflowTemplateId);
        /// <summary>
        /// 获取工作流元数据
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        Task<WorkflowMeta> GetWorkflowMeta(string workflowId, int version);
        /// <summary>
        /// 创建工作流
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>workflow Id，0为创建失败</returns>
        Task<(int Code, long Id)> CreateWorkflow(WorkflowDefParameter parameter);
    }
}
