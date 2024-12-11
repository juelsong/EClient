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

namespace ESys.Utilty.Defs
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
#pragma warning disable 1591
    public static class ConstDefs
    {
        public static class RequestHeader
        {
            public const string Tenant = "X-TENANT";
            public const string Offline = "X-OFFLINE";
            public static class ESign
            {
                public const string Account = "X-ESIGN-USER";
                public const string Password = "X-ESIGN-PASS";
                public const string SerialNumber = "X-ESIGN-SERIAL";
                public const string Comment = "X-ESIGN-COMMENT";
                public const string ESignCount = "X-ESIGN-COUNT";
            }
        }

        public static class ResponseHeader
        {
            public const string AccessToken = "access-token";
            public const string RefreshAccessToken = "x-access-token";
        }

        public static class Jwt
        {
            public const string UserId = "UserId";
            public const string Account = "Account";
            public const string IsSuper = "IsSuper";
            //public const string Permission = "Permission";
            public const string Tenant = "Tenant";
        }
        public const int SuperUserId = 1;
        public const int SystemUserId = 2;
        public const int AdminRoleId = 2;
        public static class SystemConfigKey
        {
            /// <summary>
            /// 默认的组件
            /// </summary>
            public const string DashboardDefault = "DASHBOARD_DEFAULT";
            /// <summary>
            /// 只有布局
            /// </summary>
            public const string DashboardLayout = "DASHBOARD_LAYOUT";
            /// <summary>
            /// 备选组件
            /// </summary>
            public const string DashboardComponents = "DASHBOARD_COMPONENTS";
            /// <summary>
            /// 安全配置
            /// </summary>
            public const string SecurityConfig = "SECURITY_CONFIG";
            /// <summary>
            /// EMAIL配置
            /// </summary>
            public const string EmailConfig = "EMAIL_CONFIG";
            /// <summary>
            /// 用户管理模式配置 ESys、LDAP
            /// </summary>
            public const string UserManagementModelConfig = "USER_MANAGEMENT_MODEL_CONFIG";
            /// <summary>
            /// Ldap配置
            /// </summary>
            public const string LdapConfig = "LDAP_CONFIG";
        }

        public static class LogTypeNames
        {
            public const string Login = "Login";
            public const string Logout = "Logout";
            public const string EsignData = "EsignData";
            public const string ViewReport = "ViewReport";
            public const string ExportReport = "ExportReport";
            public const string ImportData = "ImportData";

        }

        public static class ApprovalStages
        {
            /// <summary>
            /// 无
            /// </summary>
            public const int None = 0;
            /// <summary>
            /// 起草
            /// </summary>
            public const int Draft = 1;
            /// <summary>
            /// 提交
            /// </summary>
            public const int Commit = 2;
            /// <summary>
            /// 审核
            /// </summary>
            public const int Audit = 3;
            /// <summary>
            /// 审批
            /// </summary>
            public const int Approve = 4;
            /// <summary>
            /// 通过
            /// </summary>
            public const int Approved = 5;
        }

        public static class WorkflowCategory
        {
            /// <summary>
            /// 审批流
            /// </summary>
            public const int Approval = 1;
            /// <summary>
            /// 测试方法
            /// </summary>
            public const int TestType = 2;
        }
    }
#pragma warning restore 1591
}
