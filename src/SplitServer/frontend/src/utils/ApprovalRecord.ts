import StringUnion from "@/utils/string-union";
import {
  oDataQuery,
  oDataPatch,
  oDataBatchUpdate,
  BatchUpdate,
} from "@/utils/odata";
import {
  ApprovalEntity,
  ApprovalItem,
  ApprovalStep,
  User,
} from "@/defs/Entity";
import { WorkflowContext } from "@/defs/Model";
import asyncSleep from "@/utils/AsyncSleeper";
import {
  createWorkflow,
  createCommit,
  createApprove,
  createDisapprove,
  createWithdraw,
  createReview,
} from "@/api/approval_gen";
import moment from "moment";
// 审批流程状态
export const PlanCommitStatus = 1;
export const PlanAuditStatus = 2;
export const PlanApprovingStatus = 3;
export const ApprovalStatusGuard = StringUnion(
  "Commit", //提交
  "Audit", // 审核
  "Approving" //审批
);

export const ApprovalOperationGuard = StringUnion(
  "Commit", //提交
  "PassAudit", // 审核通过
  "FailAudit", // 审核不通过
  "PassApproving", // 审批通过
  "FailApproving" // 审批不同通过
);

export type ApprovalStatus = typeof ApprovalStatusGuard.type;
export type ApprovalOperation = typeof ApprovalOperationGuard.type;

export type ApprovalRecord = {
  Data: ApprovalItem;
  DisplayStatus?: ApprovalStatus;
  User?: string;
  OperationTime?: Date;
  Operation?: ApprovalOperation;
};

export type ApprovalEntityParameter = {
  EntityName: string;
  EntityId: number;
};
function getOperationTime(item: ApprovalItem): Date | undefined {
  if (item.OperationTime) {
    return moment(item.OperationTime).toDate();
  }
  return undefined;
}
function getOperation(item: ApprovalItem): ApprovalOperation | undefined {
  if (item.Status == PlanCommitStatus) {
    return "Commit";
  } else if (item.Status == PlanAuditStatus) {
    if (item.Approved == true) {
      return "PassAudit";
    }
    if (item.Approved == false) {
      return "FailAudit";
    }
  } else if (item.Status == PlanApprovingStatus) {
    if (item.Approved == true) {
      return "PassApproving";
    }
    if (item.Approved == false) {
      return "FailApproving";
    }
  }
}

export async function getApprovalRecord(parameter: ApprovalEntityParameter) {
  const approvalEntityResult = await oDataQuery("ApprovalEntity", {
    $select: "EntityId, Belongs, Status",
    $filter: `(EntityName eq '${parameter.EntityName}') and (EntityId eq ${parameter.EntityId})`,
    $expand: `ApprovalItems($select=Approved,Status,Comment,UserId,CreatedTime,UpdatedTime,OperationTime;$orderby=CreatedTime,UpdatedTime;$filter=Status in[${PlanCommitStatus},${PlanAuditStatus},${PlanApprovingStatus}])`,
    $orderby: "CreatedTime desc,UpdatedTime",
    $top: 1,
  });

  const approvalEntities = approvalEntityResult.value as ApprovalEntity[];
  // 按照OperationTime 排序，undefined的排在后面
  approvalEntities.forEach((entity) => {
    entity.ApprovalItems?.sort((a, b) => {
      if (a.OperationTime == undefined) {
        if (b.OperationTime == undefined) {
          return 0;
        } else {
          return 1;
        }
      } else {
        if (b.OperationTime == undefined) {
          return -1;
        } else {
          const momentA = moment(a.OperationTime);
          const momentB = moment(b.OperationTime);

          return momentA.isSame(momentB)
            ? 0
            : momentA.isBefore(momentB)
            ? -1
            : 1;
        }
      }
    });
  });

  const approvalItems = approvalEntities
    .map((e) => e.ApprovalItems!)
    .reduce((p, c) => {
      p.push(...c);
      return p;
    }, []);
  const userIds = Array.from(
    new Set(approvalItems.filter((e) => e.UserId).map((e) => e.UserId!))
  );
  const users = (
    await oDataQuery("User", {
      $select: "Id, RealName",
      $filter: `Id in [${userIds.join(",")}]`,
    })
  ).value as User[];

  return approvalItems.map((e) => {
    const ret: ApprovalRecord = {
      Data: e,
      DisplayStatus:
        e.Status == PlanCommitStatus
          ? "Commit"
          : e.Status == PlanAuditStatus
          ? "Audit"
          : e.Status == PlanApprovingStatus
          ? "Approving"
          : undefined,
      User: users.find((u) => u.Id == e.UserId)?.RealName,
      Operation: getOperation(e),
      OperationTime: getOperationTime(e),
    };
    return ret;
  });
}

export type CommitParameter = {
  comment?: string;
  auditUserIds?: number[];
  approvingIds?: number[];
};
export async function startPlanApprovalWorkflow(
  entityParameter: ApprovalEntityParameter,
  commitParameter: CommitParameter
) {
  // 实体是否存在没完成流程的ApprovalEntity
  // 如果存在，只设置提交状态
  // 不存在就是第一次提交，创建工作流并自动设置提交状态
  const approvalEntities = (
    await oDataQuery("ApprovalEntity", {
      $select: "EntityId, Belongs, Status",
      $filter: `(EntityName eq '${entityParameter.EntityName}') and (IsCompleted eq false) and (EntityId eq ${entityParameter.EntityId})`,
      $expand: "ApprovalSteps($select=Id,StepId)",
    })
  ).value as ApprovalEntity[];
  const firstStart = approvalEntities.length == 0;
  // 第一次执行需要开始流程，并触发提交
  // 后续执行只触发提交
  if (firstStart) {
    const ctx: WorkflowContext = {
      Integer: {
        Storage: {
          SetStatusCommitStatus: 1,
          ApprovalAuditStatus: 2,
          ApprovalApprovedStatus: 3,
          SetStatusApprovedStatus: 4,
        },
      },
      Boolean: {
        Storage: {
          SetStatusApprovedAutoSetStatus: true,
          SetStatusApprovedIsEntityApproved: true,
        },
      },
      Long: {
        Storage: {
          EntityId: entityParameter.EntityId,
        },
      },
      String: {
        Storage: {
          EntityName: entityParameter.EntityName,
        },
      },
      IntegerArray: {
        Storage: {
          ApprovalAuditDefaultApprover: commitParameter.auditUserIds ?? [],
          ApprovalApprovedDefaultApprover: commitParameter.approvingIds ?? [],
        },
      },
    };
    const wf = await createWorkflow(ctx, "PlanApproval");
  } else {
    const approvalEntity = approvalEntities[0];
    const auditStep: ApprovalStep = {
      Id:
        approvalEntity.ApprovalSteps?.find((s) => s.StepId == "ApprovalAudit")
          ?.Id ?? 0,
      UserIdStr: (commitParameter.auditUserIds ?? [])
        .map((str) => `,${str},`)
        .join(""),
    };
    const approvedStep: ApprovalStep = {
      Id:
        approvalEntity.ApprovalSteps?.find(
          (s) => s.StepId == "ApprovalApproved"
        )?.Id ?? 0,
      UserIdStr: (commitParameter.approvingIds ?? [])
        .map((str) => `,${str},`)
        .join(""),
    };
    const batch: BatchUpdate = {
      ApprovalStep: [auditStep, approvedStep],
    };
    await oDataBatchUpdate(batch);
  }
  const ret = await createCommit({
    Entity: entityParameter.EntityName,
    Id: entityParameter.EntityId,
    Status: PlanCommitStatus,
    Comment: commitParameter.comment,
  });
  return ret.success;
}

export async function reviewEntity(
  parameter: ApprovalEntityParameter,
  reviewed: boolean,
  comment?: string
) {
  const api = reviewed ? createReview : createDisapprove;
  const result = await api({
    Entity: parameter.EntityName,
    Id: parameter.EntityId,
    Comment: comment,
  });
  return result.success;
}

export async function approveEntity(
  parameter: ApprovalEntityParameter,
  approved: boolean,
  comment?: string
) {
  const api = approved ? createApprove : createDisapprove;
  const result = await api({
    Entity: parameter.EntityName,
    Id: parameter.EntityId,
    Comment: comment,
  });
  return result.success;
}

export async function withdrawEntity(parameter: ApprovalEntityParameter) {
  const result = await createWithdraw(parameter.EntityName, parameter.EntityId);
  return result.success;
}
