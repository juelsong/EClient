export default {
  entity: "检测方法",
  filter: {
    IsActive: "显示禁用",
    Name: "检测方法名称",
    TestClass: "检测等级",
    TestCategory: "检测类别",
    Location: "所属区域",
  },
  column: {
    Description: "检测方法名称",
    TestClass: "检测等级",
    TestCategory: "检测类别",
    Location: "所属区域",
    CreatedTime: "添加时间",
  },
  confirm: {
    editWorkflow:
      "当前检测方法有{0}个未完成的任务，修改可能会导致任务执行异常。建议您等待任务完成后再修改或执行新生成的任务。您确定要继续修改吗？",
    editWorkflowLackMeasurement: "缺少名称为{0}的数据项",
  },
  editor: {
    TestClass: "检测等级",
    TestCategory: "检测类别",
    TestTypeCode: "代码",
    TestTypeLabel: "标签代码",
    Description: "描述",
    Price: "价格",
    Location: "区域",
    RequireProductSelection: "需要选择相关产品",
    ProdSelectionTimeFrame: "选择产品流程",
  },
  label: {
    Copy: "复制",
    Workflow: "流程设置",
  },
};
