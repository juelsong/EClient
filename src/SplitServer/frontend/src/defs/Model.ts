import { MomentInput } from "moment";
export declare interface Result<T = any> {
    code: number;
    data: T;
    message: string;
    success: boolean;
    timestamp: MomentInput;
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
export declare type UserManagementMode = "ESys" | "LDAP";
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

