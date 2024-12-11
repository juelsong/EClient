export default {
  NoError: "成功",
  User: {
    WrongPassword: "密码错误",
    NotExist: "用户不存在",
    Forbidden: "用户被禁用",
    TokenExpired: "令牌过期，请重新登录",
    NotAuthorized: "没有权限，请联系管理员",
    CodeError: "验证码无效",
    NotValidPassword: "密码不符合规则",
    AlreadyExist: "用户已存在"
  },
  Service: {
    InnerError: "内部错误",
    NeedESign: "需要电子签名",
    DataExpired: "数据过期",
    DataNotExist: "数据不存在",
    DeactiveInvalid: "无法禁用数据",
    InvalidModel: "数据无效",
    DuplicateESign: "重复电子签名"
  },
  Report: {
    TypeError: "报告类型错误",
    NotExist: "报告不存在"
  },
  Workflow: {
    VersionError: "版本错误",
    LackStartStep: "缺乏开始步骤",
    LackStartStepPort: "缺乏开始步骤端口",
    TooManyStartStep: "开始步骤重复",
    LoadError: "加载失败"
  },
  Approval: {
    Denied: "拒绝"
  },
  Device: {
    ConfigError: "配置错误",
    ConnectError: "连接错误",
    GetDataError: "获取数据错误",
    SNError: "序列号错误",
    DataError: "数据错误",
    NotSelectFile: "未选择文件"
  },
  Import: {
    TypeError: "类型错误",
    NotExist: "不存在",
    DataError: "数据错误"
  }
}