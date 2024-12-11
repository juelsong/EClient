/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑,代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

using System.ComponentModel;

namespace ESys.Utilty.Defs
{
#pragma warning disable 1591
    public static class ErrorCode
    {
        private const int UserBase = 1000;
        private const int ServiceBase = 2000;
        private const int ReportBase = 3000;
        private const int WorkflowBase = 4000;
        private const int ApprovalBase = 5000;
        private const int DeviceBase = 6000;
        private const int ImportBase = 7000;


        [Description("成功")]
        public const int NoError = 0;

        public static class User
        {
            [Description("密码错误")]
            public const int WrongPassword = UserBase + 1;
            [Description("用户不存在")]
            public const int NotExist = UserBase + 2;
            [Description("用户被禁用")]
            public const int Forbidden = UserBase + 3;
            [Description("令牌过期，请重新登录")]
            public const int TokenExpired = UserBase + 4;
            [Description("没有权限，请联系管理员")]
            public const int NotAuthorized = UserBase + 5;
            [Description("验证码无效")]
            public const int CodeError = UserBase + 6;
            [Description("密码不符合规则")]
            public const int NotValidPassword = UserBase + 7;
            [Description("用户已存在")]
            public const int AlreadyExist = UserBase + 8;
        }

        public static class Service
        {
            [Description("内部错误")]
            public const int InnerError = ServiceBase + 1;
            [Description("需要电子签名")]
            public const int NeedESign = ServiceBase + 2;
            [Description("数据过期")]
            public const int DataExpired = ServiceBase + 3;
            [Description("数据不存在")]
            public const int DataNotExist = ServiceBase + 4;
            [Description("无法禁用数据")]
            public const int DeactiveInvalid = ServiceBase + 5;
            [Description("数据无效")]
            public const int InvalidModel = ServiceBase + 6;
            [Description("重复电子签名")]
            public const int DuplicateESign = ServiceBase + 7;
        }

        public static class Report
        {
            [Description("报告类型错误")]
            public const int TypeError = ReportBase + 1;
            [Description("报告不存在")]
            public const int NotExist = ReportBase + 2;
        }

        public static class Workflow
        {
            [Description("版本错误")]
            public const int VersionError = WorkflowBase + 1;
            [Description("缺乏开始步骤")]
            public const int LackStartStep = WorkflowBase + 2;
            [Description("缺乏开始步骤端口")]
            public const int LackStartStepPort = WorkflowBase + 3;
            [Description("开始步骤重复")]
            public const int TooManyStartStep = WorkflowBase + 4;
            [Description("加载失败")]
            public const int LoadError = WorkflowBase + 5;
        }

        public static class Approval
        {
            [Description("拒绝")]
            public const int Denied = ApprovalBase + 1;
        }

        public static class Device
        {
            [Description("配置错误")]
            public const int ConfigError = DeviceBase + 1;
            [Description("连接错误")]
            public const int ConnectError = DeviceBase + 2;
            [Description("获取数据错误")]
            public const int GetDataError = DeviceBase + 3;
            [Description("序列号错误")]
            public const int SNError = DeviceBase + 4;
            [Description("数据错误")]
            public const int DataError = DeviceBase + 5;
            [Description("未选择文件")]
            public const int NotSelectFile = DeviceBase + 6;
        }
        public static class Import
        {
            [Description("类型错误")]
            public const int TypeError = ImportBase + 1;
            [Description("不存在")]
            public const int NotExist = ImportBase + 2;
            [Description("数据错误")]

            public const int DataError = ImportBase + 3;

        }
    }
#pragma warning restore 1591
}
