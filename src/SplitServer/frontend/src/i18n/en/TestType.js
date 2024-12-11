export default {
  entity: "TestType",
  filter: {
    IsActive: "IsActive",
    Name: "Name",
    TestClass: "TestClass",
    TestCategory: "TestCategory",
    Location: "Location",
  },
  column: {
    Description: "Description",
    TestClass: "TestClass",
    TestCategory: "TestCategory",
    Location: "Location",
    CreatedTime: "CreatedTime",
  },
  confirm: {
    editWorkflow:
      "当前检测方法有{0}个未完成的任务，修改可能会导致任务执行异常。建议您等待任务完成后再修改或执行新生成的任务。您确定要继续修改吗？",
    editWorkflowLackMeasurement: "缺少名称为{0}的数据项",
  },
  editor: {
    TestClass: "TestClass",
    TestCategory: "TestCategory",
    TestTypeCode: "Code",
    TestTypeLabel: "LabelCode",
    Description: "Description",
    Price: "Price",
    Location: "Name",
    RequireProductSelection: "RequireProductSelection",
    ProdSelectionTimeFrame: "选择产品流程",
  },
  label: {
    Copy: "Copy",
    Workflow: "Workflow",
  },
};
