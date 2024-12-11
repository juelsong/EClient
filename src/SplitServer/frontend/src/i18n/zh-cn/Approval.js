export default {
  Title: "审批记录",
  Status: {
    Commit: "提交",
    Audit: "审核",
    Approving: "审批",
  },
  Column: {
    Status: "节点名称",
    User: "操作人",
    Operation: "操作",
    Comment: "意见",
    Timestamp: "时间",
  },
  Operation: {
    Commit: "提交", //提交
    PassAudit: "审核通过", // 审核通过
    FailAudit: "审核不通过", // 审核不通过
    PassApproving: "审批通过", // 审批通过
    FailApproving: "审批不通过", // 审批不通过
  },
};
