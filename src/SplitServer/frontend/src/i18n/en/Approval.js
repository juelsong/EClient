export default {
  Title: "审批记录",
  Status: {
    Commit: "Commit",
    Audit: "Audit",
    Approving: "Approving",
  },
  Column: {
    Status: "Status",
    User: "User",
    Operation: "Operation",
    Comment: "Comment",
    Timestamp: "Timestamp",
  },
  Operation: {
    Commit: "Commit", //提交
    PassAudit: "PassAudit", // 审核通过
    FailAudit: "FailAudit", // 审核不通过
    PassApproving: "PassApproving", // 审批通过
    FailApproving: "FailApproving", // 审批不通过
  },
};
