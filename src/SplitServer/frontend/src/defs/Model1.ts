import { CurrentWorkSpace, PlanGroup, Plan } from "@/defs/Entity";

export declare interface Result<T = any> {
    code: number;
    data: T;
    message: string;
    success: boolean;
    timestamp: Date;
}

/**
 *流程上下文
 */
export declare class WorkflowContext {
    /**
     *动态数据
     */
    Integer?: DynamicData<number>;
    /**
     *动态数据
     */
    NullableInteger?: DynamicData<number | null>;
    /**
     *动态数据
     */
    Long?: DynamicData<number>;
    /**
     *动态数据
     */
    Boolean?: DynamicData<boolean>;
    /**
     *动态数据
     */
    NullableBoolean?: DynamicData<boolean | null>;
    /**
     *动态数据
     */
    String?: DynamicData<string>;
    /**
     *动态数据
     */
    Decimal?: DynamicData<number>;
    /**
     *动态数据
     */
    Double?: DynamicData<number>;
    /**
     *动态数据
     */
    DateTimeOffset?: DynamicData<undefined>;
    /**
     *动态数据
     */
    IntegerArray?: DynamicData<number[]>;
    /**
     *动态数据
     */
    LongArray?: DynamicData<number[]>;
    /**
     *动态数据
     */
    BooleanArray?: DynamicData<boolean[]>;
    /**
     *动态数据
     */
    StringArray?: DynamicData<string[]>;
    /**
     *动态数据
     */
    DecimalArray?: DynamicData<number[]>;
    /**
     *动态数据
     */
    DoubleArray?: DynamicData<number[]>;
}

/**
 *动态数据
 */
export declare class DynamicData<T> {
    /**
     *存储
     */
    Storage?: Record<string, T>;
}

/**
 *设置状态模型
 */
export declare class StatusModel {
    /**
     *实体名称
     */
    Entity?: string;
    /**
     *实体Id
     */
    Id?: number;
    /**
     *评论
     */
    Comment?: string;
    /**
     *状态
     */
    Status?: number;
}

/**
 *审批模型
 */
export declare class ApprovalModel {
    /**
     *实体名称
     */
    Entity?: string;
    /**
     *实体Id
     */
    Id?: number;
    /**
     *评论
     */
    Comment?: string;
}

/**
 *复核修改数据
 */
export declare class AuditData {
    /**
     *结果数值
     */
    AuditReadings?: AuditReading[];
    /**
     *修改设备列表
     */
    AuditEquipments?: AuditEquipment[];
    /**
     *修改耗材列表
     */
    AuditMedias?: AuditMedia[];
    /**
     *修改菌种鉴定列表
     */
    AuditOrgnisms?: AuditOrgnism[];
    /**
     *文件修改
     */
    AuditFiles?: AuditFile[];
    /**
     *新增修改信息
     */
    Note?: string;
    /**
     *关联产品信息
     */
    SampleProduct?: string;
    /**
     *当前样品Id
     */
    SampleId?: number;
}

/**
 *修改结果数值
 */
export declare class AuditReading {
    /**
     *SampleOrganismId
     */
    Id?: number;
    /**
     *关联的结果Id
     */
    Value?: number;
}

/**
 *修改设备
 */
export declare class AuditEquipment {
    /**
     *workspaceEquipmentId
     */
    Id?: number;
    /**
     *关联的设备Id
     */
    EquipmentId?: number;
}

/**
 *修改培养基
 */
export declare class AuditMedia {
    /**
     *workspaceMediaId
     */
    Id?: number;
    /**
     *关联的耗材Id
     */
    MediaId?: number;
}

/**
 *修改菌种鉴定
 */
export declare class AuditOrgnism {
    /**
     *SampleOrganismId
     */
    Id?: number;
    /**
     *关联的耗材Id
     */
    OrganismId?: number;
}

/**
 *文件修改
 */
export declare class AuditFile {
    /**
     *文件存储Id
     */
    Id?: number;
    /**
     *文件名称
     */
    Name?: string;
    /**
     *文件存储路径
     */
    Path?: string;
}

/**
 *测试池数据
 */
export declare class CurrentWorkSpaceParameter {
    /**
     *测试池Id数组
     */
    CurrentWorkSpaceIds?: number[];
    /**
     *安排日期
     */
    ScheduledTime?: Date;
}

/**
 *延时测试参数
 */
export declare class DelayedTestsParameter {
    /**
     *延时测试Id
     */
    Id?: number;
    /**
     *测试Id数组
     */
    TestIds?: number[];
    /**
     *延时时间
     */
    DelayedTime?: Date;
    /**
     *安排日期
     */
    ScheduledTime?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *计划数据
 */
export declare class PlanParameter {
    /**
     *计划Id
     */
    Id?: number;
    /**
     *当前时间
     */
    Now?: Date;
    /**
     *（以分钟为单位的）周期
     */
    CycleByMinute?: number;
}

/**
 *计划结果
 */
export declare class PlanResult {
    /**
     *错误信息
     */
    LogString?: string;
    /**
     *当前时间
     */
    CurrentWorkSpaces?: CurrentWorkSpace[];
}

/**
 *文件信息
 */
export declare class DocumentData {
    /**
     *显示名称
     */
    Name?: string;
    /**
     *存储名称 + 路径
     */
    Path?: string;
}

/**
 *修改user接口
 */
export declare class UpdateLocalParam {
    /**
     *默认显示中英文
     */
    Locale?: string;
}

/**
 *区域状态信息
 */
export declare class LocationStatusData {
    /**
     *节点id
     */
    LocationId?: number;
    /**
     *区域Id
     */
    SiteId?: number;
    /**
     *是否激活
     */
    IsActive?: boolean;
}

/**
 *编辑返回内容
 */
export declare class LocationEditResponse {
    /**
     *编辑结果
     */
    Result?: boolean;
    /**
     *失败内容
     */
    SiteWithPlans?: SiteWithPlan[];
}

/**
 *关联地点的计划名称
 */
export declare class SiteWithPlan {
    /**
     *地点Id
     */
    LocationId?: number;
    LocationName?: string;
    /**
     *节点id
     */
    PlanName?: string;
    /**
     *区域Id
     */
    Description?: string;
}

/**
 *订阅模型
 */
export declare class SubscriptionModel {
    /**
     *通知类型Id
     */
    NotificationTypeId?: number;
    /**
     *订阅用户Id
     */
    UserId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *订阅参数Id  当NotificationTypeId有值时为用户Id，当UserId有值时为NotificationTypeId
     */
    Subscribers?: number[];
}

/**
 *email配置
 */
export declare class EMailConfig {
    /**
     *服务器地址
     */
    Server?: string;
    /**
     *端口
     */
    Port?: number;
    /**
     *发送者地址
     */
    Address?: string;
    /**
     *密码
     */
    Password?: string;
    /**
     *检查周期（目前租户配置无效）
     */
    IntervalSeconds?: number;
    /**
     *是否启用SSL
     */
    EnableSSL?: boolean;
    Domain?: string;
}

/**
 *提交的计划数据
 */
export declare class PlanData {
    PlanGroup?: PlanGroup;
    Plan?: Plan;
}

/**
 *分配任务
 */
export declare class AssignData {
    /**
     *任务id
     */
    CurrentIds?: number[];
    /**
     *分配userId
     */
    UserId?: number;
    /**
     *记录时间
     */
    Date?: Date;
}

/**
 *限度数据
 */
export declare class LimitData {
    /**
     *测试池Id
     */
    CwsId?: number;
    /**
     *测试结果数组
     */
    ResultDatas?: ResultData[];
}

/**
 *测试结果
 */
export declare class ResultData {
    /**
     *关联MeasurementId Id
     */
    MeasurementId?: number;
    /**
     *测量单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *结果值
     */
    Value?: number;
}

/**
 *导入数据
 */
export declare class ImportData {
    /**
     *当前任务
     */
    CurrentTasks?: CurrentTask[];
}

/**
 *当前任务
 */
export declare class CurrentTask {
    /**
     *结果录入 CurrentworkspaceId 审核批准为SampleId
     */
    CurrentId?: number;
    /**
     *设备
     */
    Devices?: DeviceData[];
    /**
     *培养基、耗材
     */
    Medias?: MediaData[];
    /**
     *结果数值
     */
    ResultDataSet?: ResultData[];
    /**
     *是否NoTest
     */
    NoTest?: boolean;
    /**
     *NoTest原因
     */
    Reason?: string;
    /**
     *产品名称 不关联产品模块，人为手动录入
     */
    ProductNote?: string;
    /**
     *菌种信息
     */
    Organisms?: OrganismData[];
    /**
     *关联 产品
     */
    Products?: ProductData[];
    /**
     *附加信息
     */
    Note?: string;
    /**
     *文件信息
     */
    Documents?: DocumentData[];
    /**
     *无效数据
     */
    ObsoletedDatas?: ObsoletedData[];
    /**
     *匹配数据Id
     */
    ParticleCounterDataId?: number;
    /**
     *是否设备导入
     */
    IsDeviceImport?: boolean;
    /**
     *执行时间
     */
    ExecuteDate?: Date;
    /**
     *下阶段执行人id
     */
    NextStepUserId?: number;
    /**
     *真实id
     */
    RealExecuteUserId?: number;
    /**
     *下一阶段备注
     */
    NextStepNote?: string;
}

/**
 *关联设备信息
 */
export declare class DeviceData {
    /**
     *主键
     */
    Id?: number;
}

/**
 *关联培养基信息
 */
export declare class MediaData {
    /**
     *主键
     */
    Id?: number;
}

/**
 *关联菌种基信息
 */
export declare class OrganismData {
    /**
     *主键
     */
    Id?: number;
}

/**
 *关联产品
 */
export declare class ProductData {
    /**
     *主键
     */
    Id?: number;
}

/**
 *设备导入
 */
export declare class ObsoletedData {
    /**
     *匹配数据Id
     */
    ParticleCounterDataId?: number;
    /**
     *备注信息
     */
    Remark?: string;
}

/**
 *限度结果
 */
export declare class LimitResult {
    /**
     *限度描述
     */
    LimitDescription?: string;
    /**
     *限度Id
     */
    LimitId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *限度类型名称
     */
    LimitTypeName?: string;
    /**
     *限度消息
     */
    LimitMessage?: string;
    /**
     *频率限度描述
     */
    FreqLimitDescription?: string;
    /**
     *频率限度Id
     */
    FreqLimitId?: number;
    /**
     *频率限度类型Id
     */
    FreqLimitTypeId?: number;
    /**
     *频率限度类型名称
     */
    FreqLimitTypeName?: string;
    /**
     *频率限度消息
     */
    FreqLimitMessage?: string;
}

/**
 *安全配置模型
 */
export declare class SecurityModel {
    /**
     *密码规则
     */
    Rules?: PasswordRules;
    /**
     *首次登录时是否修改密码
     */
    ChangePassUponFirstLogin?: boolean;
    /**
     *近几次密码不能重复,0表示不限定
     */
    CanNotRepeatedTimes?: number;
    ExpiryPeriod?: string;
    /**
     *效登录尝试次数，超过则锁定账户，0不限制
     */
    InvalidLoginAttempts?: number;
}

/**
 *密码规则
 */
export declare class PasswordRules {
    /**
     *最小密码长度
     */
    MinLength?: number;
    /**
     *包含数字
     */
    IncludeNumber?: boolean;
    /**
     *包含小写字母
     */
    IncludeLower?: boolean;
    /**
     *包含大写字母
     */
    IncludeUpper?: boolean;
    /**
     *包含特殊字符
     */
    IncludeSpecial?: boolean;
}

/**
 *密码有效规则
 */
export declare class PasswordRule {
    /**
     *正则字符串
     */
    RegexString?: string;
    /**
     *提示消息
     */
    Prompt?: string;
}

/**
 *LDAP配置
 */
export declare class LDAPConfig {
    /**
     *LDAP 服务 sso:serverName
     */
    ServerName?: string;
    /**
     *LDAP 端口 sso:serverPort
     */
    Port?: number;
    /**
     *LDAP 用户 BaseDN   user_base_dn
     */
    UserBaseDN?: string;
    /**
     *LDAP 用户组 BaseDN   group_base_dn
     */
    GroupBaseDN?: string;
    /**
     *LDAP 组织机构 BaseDN  organization_base_dn
     */
    OrgBaseDN?: string;
    /**
     *LDAP 用户账号属性名称  TODO 需要这个吗？ userid_attribute_name  ssid？
     */
    UserAccountAttributeName?: string;
    /**
     *LDAP 用户显示名称属性名称
     */
    UserNameAttributeName?: string;
    /**
     *LDAP 用户电邮属性名称
     */
    UserEMailAttributeName?: string;
    /**
     *LDAP 用户组成员显示名称 group_membership_attribute_name
     */
    GroupMembershipAttributeName?: string;
    /**
     *LDAP 用户组显示名称
     */
    GroupDisplayAttributeName?: string;
    /**
     *LDAP 用户组描述
     */
    GroupDescriptionAttributeName?: string;
    /**
     *LDAP User attribute中 ObjectClass名称  user_object_classes
     */
    UserObjectClasses?: string;
    /**
     *LDAP 用户组 attribute中 ObjectClass名称  group_object_classes
     */
    GroupObjectClasses?: string;
    /**
     *LDAP 组织 attribute中 ObjectClass名称
     */
    OrgObjectClasses?: string;
    /**
     *LDAP 登录用户
     */
    ConnectAsUserId?: string;
    /**
     *LDAP 登录用户密码
     */
    ConnectAsUserIdPassword?: string;
}

/**
 *首页布局
 */
export declare class DashboardLayout {
    /**
     *xs 响应尺寸
     */
    XS?: number;
    /**
     *sm 响应尺寸
     */
    SM?: number;
    /**
     *md 响应尺寸
     */
    MD?: number;
    /**
     *lg 响应尺寸
     */
    LG?: number;
    /**
     *xl 响应尺寸
     */
    XL?: number;
    /**
     *间距
     */
    Margin?: number;
    /**
     *高度
     */
    Height?: number;
}

/**
 *测试方法复制模型
 */
export declare class CopyModel {
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *新名称
     */
    Description?: string;
}

/**
 *登录参数
 */
export declare class LoginParams {
    /**
     *用户名
     */
    Account: string;
    /**
     *密码
     */
    Password: string;
    /**
     *验证码
     */
    Captcha?: string;
    /**
     *验证码key
     */
    CheckKey?: string;
}

/**
 *修改自己密码
 */
export declare class ChangePassSelf {
    /**
     *原密码
     */
    OriPassword: string;
    /**
     *新密码
     */
    Password: string;
}

/**
 *用户个性化配置
 */
export declare class UserSettings {
    /**
     *时间日期格式化字符串
     */
    DateTimeFormat?: string;
    /**
     *日期格式化字符串
     */
    DateFormat?: string;
}

/**
 *登录结果
 */
export declare class LoginResult {
    /**
     *用户Id
     */
    Id?: number;
    /**
     *用户名
     */
    Account: string;
    /**
     *姓名
     */
    RealName?: string;
    /**
     *密码是否过期
     */
    PasswordExpiried?: boolean;
    /**
     *首次登录需要更改密码
     */
    ChangePassUponFirstLogin?: boolean;
}

/**
 *用户信息
 */
export declare class UserInfo {
    /**
     *用户Id
     */
    Id?: number;
    /**
     *用户名
     */
    Account?: string;
    /**
     *姓名
     */
    Name?: string;
    /**
     *管理区域
     */
    LocationId?: number;
    /**
     *用户管理模式
     */
    UserManagementMode?: UserManagementMode;
    /**
     *权限
     */
    Permissions?: UserPermission[];
    /**
     *角色
     */
    Roles?: UserRoleInfo[];
    /**
     *用户配置
     */
    Profile?: Profile;
}

/**
 *用户管理模式
 */
export declare type UserManagementMode = "EMIS" | "LDAP";
/**
 *用户权限
 */
export declare class UserPermission {
    /**
     *编码
     */
    Code?: string;
    /**
     *类型
     */
    Type?: number;
}

/**
 *用户角色信息
 */
export declare class UserRoleInfo {
    /**
     *名称
     */
    Name?: string;
}

/**
 *用户配置
 */
export declare class Profile {
    /**
     *用户首页布局
     */
    Dashboard?: UserDashboardLayout;
    /**
     *用户个性化配置
     */
    UserSettings?: UserSettings;
    /**
     *语言设置
     */
    Locale?: string;
}

/**
 *用户首页布局
 */
export declare class UserDashboardLayout {
    /**
     *xs 响应尺寸
     */
    XS?: number;
    /**
     *sm 响应尺寸
     */
    SM?: number;
    /**
     *md 响应尺寸
     */
    MD?: number;
    /**
     *lg 响应尺寸
     */
    LG?: number;
    /**
     *xl 响应尺寸
     */
    XL?: number;
    /**
     *间距
     */
    Margin?: number;
    /**
     *高度
     */
    Height?: number;
    /**
     *内容代码
     */
    ContentCode?: string[];
}

/**
 *工作流模型
 */
export declare class WorkflowDefModel {
    /**
     *Id
     */
    WorkflowId?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *分类
     */
    Category?: number;
    /**
     *图形定义
     */
    GraphJson?: string;
}

/**
 *工作流元数据
 */
export declare class WorkflowMeta {
    /**
     *模板实体Id
     */
    Id?: number;
    /**
     *工作流Id
     */
    WorkflowId?: string;
    Version?: number;
    Name?: string;
    Description?: string;
    Category?: number;
}

export declare type WorkflowStatus = "Runnable" | "Suspended" | "Complete" | "Terminated";
