///*
// *        ┏┓   ┏┓+ +
// *       ┏┛┻━━━┛┻┓ + +
// *       ┃       ┃
// *       ┃   ━   ┃ +++ + +
// *       ████━████ ┃+
// *       ┃       ┃ +
// *       ┃   ┻   ┃
// *       ┃       ┃ + +
// *       ┗━┓   ┏━┛
// *         ┃   ┃
// *         ┃   ┃ + + + +
// *         ┃   ┃    Code is far away from bug with the animal protecting
// *         ┃   ┃ +     神兽保佑, 代码无bug
// *         ┃   ┃
// *         ┃   ┃  +
// *         ┃    ┗━━━┓ + +
// *         ┃        ┣┓
// *         ┃        ┏┛
// *         ┗┓┓┏━┳┓┏┛ + + + +
// *          ┃┫┫ ┃┫┫
// *          ┗┻┛ ┗┻┛+ + + +
// */

//namespace ESys.Contract.Service
//{
//    using ESys.Contract.Workflow;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// 审批结果
//    /// </summary>
//    public enum ApprovalResult
//    {
//        /// <summary>
//        /// 无
//        /// </summary>
//        None = 0,
//        /// <summary>
//        /// 批准
//        /// </summary>
//        Approved = 1,
//        /// <summary>
//        /// 驳回
//        /// </summary>
//        Disapprove = 2,
//        /// <summary>
//        /// 撤回
//        /// </summary>
//        Withdraw = 3
//    }

//    /// <summary>
//    /// 审批项目结果
//    /// </summary>
//    /// <param name="Result"></param>
//    /// <param name="EntityName"></param>
//    /// <param name="EntityId"></param>
//    public record ApprovalResultItem(ApprovalResult? Result, string EntityName, long EntityId);

//    /// <summary>
//    /// 审批服务
//    /// </summary>
//    public interface IApprovalService
//    {
//        /// <summary>
//        /// 开始审批流程
//        /// </summary>
//        /// <param name="workflowId"></param>
//        /// <param name="userId"></param>
//        /// <param name="workflowContext"></param>
//        /// <returns></returns>
//        Task<bool> StartApprovalWorkflow(string workflowId, int userId, WorkflowContext workflowContext);
//        /// <summary>
//        /// 撤回审批流程
//        /// </summary>
//        /// <param name="entityName"></param>
//        /// <param name="entityId">实体Id</param>
//        /// <param name="userId">操作用户Id</param>
//        /// <param name="eSignDatas"></param>
//        /// <returns></returns>
//        Task<bool> Withdraw(string entityName, long entityId, int userId, params ESignData[] eSignDatas);
//        /// <summary>
//        /// 设置状态
//        /// </summary>
//        /// <param name="entityName"></param>
//        /// <param name="entityId">实体Id</param>
//        /// <param name="userId">用户Id</param>
//        /// <param name="status"></param>
//        /// <param name="comment"></param>
//        /// <param name="eSignDatas"></param>
//        /// <returns></returns>
//        Task<bool> SetStatus(string entityName, long entityId, int userId, int status, string comment, params ESignData[] eSignDatas);

//        /// <summary>
//        /// 设置审核结果
//        /// </summary>
//        /// <param name="entityName"></param>
//        /// <param name="entityId">实体Id</param>
//        /// <param name="comment"></param>
//        /// <param name="userId">用户Id</param>
//        /// <param name="approved">是否通过</param>
//        /// <param name="eSignDatas"></param>
//        /// <returns></returns>
//        Task<bool> SetApproved(string entityName, long entityId, string comment, int userId, bool approved, params ESignData[] eSignDatas);

//        /// <summary>
//        /// 获取审批结果
//        /// </summary>
//        /// <param name="entityName"></param>
//        /// <param name="entityId">实体Id</param>
//        /// <returns></returns>
//        Task<ApprovalResult?> GetResult(string entityName, long entityId);

//        /// <summary>
//        /// 获取审批项目
//        /// </summary>
//        /// <param name="workflowId">审批流实例Id</param>
//        /// <returns></returns>
//        Task<ApprovalResultItem> GetResult(string workflowId);
//    }
//}
