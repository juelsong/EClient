export default {
  entity: "检测数据",
  filter: {},
  column: {
    Sequence: "阶段序列",
    Name: "名称",
    DataType: "数据类型",
  },
  editor: {
    Name: "名称",
    DataType: "数据类型",
    DecimalLength: "小数位数",
    MeasurementSigns: "操作符",
    Description: "描述",
    Format: "格式",
    MinValue: "最小数值",
    MaxValue: "最大数值",
    ParticleSize: "颗粒大小",
    Result: "在结果中使用",
    MeasurementUOMs: "测量单位(UOM)",
    MeasurementCycles: "周期",
    DefaultValue: "默认数值",
    AlwaysDefault: "默认打开",
    Calculate: {
      Formula: "计算公式",
      Button: {
        Edit: "编辑",
      },
      MeasurementName: "检测值",
      Function: "函数",
      EditError: "编辑错误",
    },
    DigitalDefinition: {
      Lable: "数字显示定义",
      Value: "数值",
      ShowDef: "显示操作符",
      EditColumn: {
        ShowName: "显示名称",
        Operation: "操作符",
        Value1: "数值1",
        Value2: "数值2",
      },
    },
    NoSymbols: "禁止输入特殊字符",
  },
  confirm: {
    UsedByOther: "被数据项:{0}使用,禁止删除",
  },
};
