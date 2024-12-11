/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Security.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using ESys.Security.Models;
    using ESys.Security.Utility;
    using ESys.Utilty.Defs;
    using ESys.Utilty.Security;
    using Furion.ConfigurableOptions;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Novell.Directory.Ldap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// LDAP配置
    /// </summary>
    public class LDAPConfig
    {
        /// <summary>
        /// LDAP 服务 sso:serverName
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// LDAP 端口 sso:serverPort
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// LDAP 用户 BaseDN   user_base_dn
        /// </summary>
        public string UserBaseDN { get; set; }
        /// <summary>
        /// LDAP 用户组 BaseDN   group_base_dn
        /// </summary>
        public string GroupBaseDN { get; set; }
        /// <summary> 
        /// LDAP 组织机构 BaseDN  organization_base_dn
        /// </summary>
        public string OrgBaseDN { get; set; }
        /// <summary>
        /// LDAP 用户账号属性名称  TODO 需要这个吗？ userid_attribute_name  ssid？
        /// </summary>
        public string UserAccountAttributeName { get; set; }
        /// <summary>
        /// LDAP 用户显示名称属性名称
        /// </summary>
        public string UserNameAttributeName { get; set; }
        /// <summary>
        /// LDAP 用户电邮属性名称
        /// </summary>
        public string UserEMailAttributeName { get; set; }
        /// <summary>
        /// LDAP 用户组成员显示名称 group_membership_attribute_name
        /// </summary>
        public string GroupMembershipAttributeName { get; set; }
        /// <summary>
        /// LDAP 用户组显示名称
        /// </summary>
        public string GroupDisplayAttributeName { get; set; }
        /// <summary>
        /// LDAP 用户组描述
        /// </summary>
        public string GroupDescriptionAttributeName { get; set; }
        /// <summary>
        /// LDAP User attribute中 ObjectClass名称  user_object_classes
        /// </summary>
        public string UserObjectClasses { get; set; }
        /// <summary>
        /// LDAP 用户组 attribute中 ObjectClass名称  group_object_classes
        /// </summary>
        public string GroupObjectClasses { get; set; }
        /// <summary>
        /// LDAP 组织 attribute中 ObjectClass名称
        /// </summary>
        public string OrgObjectClasses { get; set; }
        /// <summary>
        /// LDAP 登录用户
        /// </summary>
        public string ConnectAsUserId { get; set; }
        /// <summary>
        /// LDAP 登录用户密码
        /// </summary>
        public string ConnectAsUserIdPassword { get; set; }
    }
    /// <summary>
    /// 安全配置项
    /// </summary>
    public class SecuritySettingOptions : IConfigurableOptions
    {
        /// <summary>
        /// 有效时长
        /// </summary>
        public TimeSpan CheckDuration { get; set; }
        /// <summary>
        /// 是否登录校验验证码
        /// </summary>
        public bool CheckCode { get; set; }
    }

    internal class LdapUserService : IUserService
    {
        private readonly IConfigService configService;
        private readonly string tenant;
        private readonly SecuritySettingOptions securitySetting;
        private readonly INotificationService notificationService;
        private readonly IDistributedCache distributedCache;
        private readonly IMemoryCache memoryCache;
        private readonly ILogService log;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private const string loginError = "LoginError";
        private const string loginSuccess = "LoginSuccess";
        private const string memberOfAttributeName = "memberOf";
        //private static readonly object rolesLock = new object();
        //private static readonly object orgLock = new object();
        public LdapUserService(
            IServiceProvider serviceProvider,
            IOptions<SecuritySettingOptions> options)
        {
            var tenantService = serviceProvider.GetRequiredService<ITenantService>();
            this.configService = serviceProvider.GetRequiredService<IConfigService>();
            this.securitySetting = options.Value;
            this.distributedCache = serviceProvider.GetRequiredService<IDistributedCache>();
            this.notificationService = serviceProvider.GetRequiredService<INotificationService>();
            this.log = serviceProvider.GetRequiredService<ILogService>();
            this.memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
            this.tenant = tenantService.GetCurrentTenant().Code;
            this.msRepository = serviceProvider.GetRequiredService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
        }
        public Task<bool> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<(int, UserInfo)> GetUserInfo(int userId)
        {
            var user = this.msRepository.Slave1<User>()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Include(u => u.Profile)
                .FirstOrDefault(u => u.Id == userId);
            if (user == null || !user.IsActive)
            {
                return (ErrorCode.User.Forbidden, null);
            }
            var roles = user.Roles
                .Where(r => !r.IsHidden && r.IsActive)
                .Distinct()
                .Select(r => new Models.UserRoleInfo() { Name = r.Name })
                .ToArray();
            var isSuper = user.Roles.Any(r => r.IsHidden && r.IsActive);
            var permissions = (isSuper
                    ? this.msRepository.Slave1<Permission>().AsEnumerable(false)
                    : user.Roles.SelectMany(r => r.Permissions).Distinct())
                .Select(p => new UserPermission() { Code = p.Code, Type = (int)p.Type })
                .ToArray();
            var locationId = null == user.LocationId ? 0 : user.LocationId.Value;
            var userInfo = new UserInfo()
            {
                Account = user.Account,
                Id = user.Id,
                Permissions = permissions,
                Roles = roles,
                Name = user.RealName,
                LocationId = locationId
            };

            userInfo.Profile ??= new Profile();
            userInfo.Profile.Dashboard = await UserServiceFactory.GetUserOrDefaultDashboardLayout(
                user.Profile,
                this.configService,
                this.tenant);
            userInfo.Profile.UserSettings = string.IsNullOrEmpty(user.Profile?.UserSettings)
                ? UserSettings.GetDefaultSettings()
                : JsonSerializer.Deserialize<UserSettings>(user.Profile.UserSettings);
            userInfo.Profile.Locale = string.IsNullOrEmpty(user.Profile?.Locale)
                ? "zh-cn"
                : user.Profile.Locale;
            userInfo.UserManagementMode = UserManagementMode.LDAP;
            return (ErrorCode.NoError, userInfo);
        }

        public async Task<(int ErrorCode, LoginResult Result, bool IsSuper)> Login(LoginParams input)
        {
            var config = await this.configService.GetSecurityConfig(this.tenant);
            var ldapConfig = await this.configService.GetConfig<LDAPConfig>(this.tenant, ConstDefs.SystemConfigKey.LdapConfig);
            if (ldapConfig == null || string.IsNullOrEmpty(ldapConfig.GroupMembershipAttributeName))
            {
                throw new ArgumentNullException(nameof(ldapConfig));
            }
            if (this.securitySetting.CheckCode)
            {
                if (string.IsNullOrWhiteSpace(input.Captcha) || string.IsNullOrWhiteSpace(input.CheckKey))
                {
                    return (ErrorCode.User.CodeError, null, false);
                }
                var lowerCaseCaptcha = input.Captcha.ToLower();
                var realKey = MD5Util.MD5Encode(lowerCaseCaptcha + input.CheckKey);
                var checkCode = this.memoryCache.Get<string>(realKey);
                if (string.IsNullOrWhiteSpace(checkCode))
                {
                    return (ErrorCode.User.CodeError, null, false);
                }
            }
            var user = this.msRepository.Slave1<User>()
                .Include(u => u.Roles)
                .FirstOrDefault(u => u.Account == input.Account);

            var userAttributes = new List<string>
                {
                    ldapConfig.UserAccountAttributeName,
                    ldapConfig.UserNameAttributeName,
                    ldapConfig.UserEMailAttributeName,
                    memberOfAttributeName
                }.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            using var conn = this.GetConnection();
            var query = conn.Search(
                ldapConfig.UserBaseDN,
                LdapConnection.ScopeSub,
                $"(&(objectClass=person)(CN={input.Account}))",
                userAttributes,
                false);
            var result = query.ToList();
            if (result.Count != 1)
            {
                await this.notificationService.AddNotification(NotificationTypes.LoginFailure,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       input.Account,
                       $"{ErrorCode.User.NotExist}");
                return (ErrorCode.User.NotExist, null, false);
            }
            var entry = result[0];
            try
            {
                conn.Bind(entry.Dn, input.Password);
            }
            catch //(Exception ex)
            {
                if (user == null)
                {
                    await this.notificationService.AddNotification(NotificationTypes.LoginFailure,
                           null,
                           null,
                           null,
                           null,
                           null,
                           null,
                           input.Account,
                           $"{ErrorCode.User.WrongPassword}");
                    return (ErrorCode.User.WrongPassword, null, false);
                }
                else
                {
                    var loginWrongPassKey = UserServiceFactory.FormatUser(this.tenant, user.Id);
                    var value = this.distributedCache.GetString(loginWrongPassKey);
                    _ = int.TryParse(value, out var cnt);
                    cnt++;
                    this.distributedCache.SetString(
                        loginWrongPassKey,
                        $"{cnt}",
                        new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = this.securitySetting.CheckDuration
                        });
                    await this.notificationService.AddNotification(NotificationTypes.LoginFailure,
                        user.Id,
                        null,
                        null,
                        null,
                        null,
                        null,
                        input.Account,
                        $"{ErrorCode.User.WrongPassword}");

                    // 无效登录尝试次数，超过则锁定账户，0不限制
                    if (config != null
                        && config.InvalidLoginAttempts > 0
                        && cnt >= config.InvalidLoginAttempts && user.IsActive)
                    {
                        this.msRepository.Master<User>().UpdateIncludeNow(
                            new User() { Id = user.Id, IsActive = false },
                            new[] { nameof(user.IsActive) });
                        await this.notificationService.AddNotification(NotificationTypes.AccountLocked,
                            user.Id,
                            null,
                            null,
                            null,
                            null,
                            null,
                            input.Account,
                            $"{cnt}");
                    }

                    this.log.LogData(new LogInfo()
                    {
                        Description = $"{loginError} : Password Error or Active Is False",
                        TypeName = ConstDefs.LogTypeNames.Login,
                        UserId = user.Id,
                    });
                }
                return (ErrorCode.User.WrongPassword, null, false);
            }
            if (user == null)
            {
                user = new User()
                {
                    Status = UserStatus.Normal,
                    Account = input.Account,
                    Password = PasswordHasher.HashPassword(input.Password, out var salt),
                    CreateBy = ConstDefs.SystemUserId,
                    IsActive = true,
                    Roles = new List<Role>(),
                };
                user.Salt = salt;
                await this.msRepository.Master<User>().InsertNowAsync(user);
            }
            if (!user.IsActive)
            {
                await this.notificationService.AddNotification(NotificationTypes.LoginFailure,
                    user.Id,
                    null,
                    null,
                    null,
                    null,
                    null,
                    input.Account,
                    $"{ErrorCode.User.Forbidden}");
                this.log.LogData(new LogInfo()
                {
                    Description = $"{loginError} :User Not Active",
                    TypeName = ConstDefs.LogTypeNames.Login,
                    UserId = user.Id,
                });
                return (ErrorCode.User.Forbidden, null, false);
            }

            // Ldap验证通过，同步User
            var pass = PasswordHasher.HashPassword(input.Password, user.Salt);
            if (user.Password != pass)
            {
                user.Password = pass;
            }
            var realNameAttribute = GetLdapAttribute(entry, ldapConfig.UserNameAttributeName);
            var realName = realNameAttribute?.StringValue ?? user.Account;
            if (realName != user.RealName)
            {
                user.RealName = realName;
            }
            var emailAttribute = GetLdapAttribute(entry, ldapConfig.UserEMailAttributeName);
            var email = emailAttribute?.StringValue;
            if (email != user.EMail)
            {
                user.EMail = email;
            }
            var memberOfAttribute = GetLdapAttribute(entry, memberOfAttributeName);
            if (memberOfAttribute != null)
            {
                if (memberOfAttribute.StringValueArray.Any(s => s.StartsWith("CN=Administrators")))
                {
                    if (user.Roles.All(r => r.Id != ConstDefs.AdminRoleId))
                    {
                        var role = this.msRepository.Slave1<Role>().FirstOrDefault(r => r.Id == ConstDefs.AdminRoleId);
                        if (role != null)
                        {
                            user.Roles.Add(role);
                        }
                    }
                }

                if (memberOfAttribute.StringValueArray.All(s => !s.StartsWith("CN=Administrators")))
                {
                    var role = user.Roles.FirstOrDefault(r => r.Id == ConstDefs.AdminRoleId);
                    if (role != null)
                    {
                        user.Roles.Remove(role);
                    }
                }
            }
            // 暂时不同步用户组
            //await this.SyncUserRoles(input.Account);

            var output = new LoginResult()
            {
                Account = user.Account,
                Id = user.Id,
                RealName = user.RealName,
            };

            await this.msRepository.Master<UserHistory>().InsertNowAsync(new UserHistory()
            {
                UserId = user.Id,
                Logined = DateTimeOffset.Now
            });
            this.log.LogData(new LogInfo()
            {
                Description = loginSuccess,
                TypeName = ConstDefs.LogTypeNames.Login,
                UserId = user.Id,
            });


            return (ErrorCode.NoError, output, user.Roles.Any(r => r.IsActive && r.IsHidden));
        }

        public Task<(int, string)> PatchPassword(User user)
        {
            throw new NotImplementedException();
        }

        public Task<(int, string)> PatchSelfPassword(int userId, ChangePassSelf model)
        {
            throw new NotImplementedException();
        }

        //private static readonly string[] orgAttributes = new string[] { "distinguishedName" };


        //private static bool Exist(IEnumerable<string> ouArray, IEnumerable<Department> departments, ref int? parentId)
        //{
        //    if (!ouArray.Any())
        //    {
        //        return false;
        //    }
        //    var ou = ouArray.First();
        //    var localParentId = parentId;
        //    var department = departments.FirstOrDefault(d => d.Name == ou && d.ParentId == localParentId);
        //    if (department == null)
        //    {
        //        return false;
        //    }
        //    if (Exist(ouArray.Skip(1), departments, ref localParentId))
        //    {
        //        parentId = localParentId;
        //        return true;
        //    }
        //    parentId = localParentId;
        //    return false;
        //}

        //private async Task SyncOrgs()
        //{
        //    var ldapConfig = await this.configService.GetConfig<LDAPConfig>(this.tenant, ConstDefs.SystemConfigKey.LdapConfig);
        //    if (ldapConfig == null || string.IsNullOrEmpty(ldapConfig.GroupMembershipAttributeName))
        //    {
        //        throw new ArgumentNullException(nameof(ldapConfig));
        //    }
        //    lock (orgLock)
        //    {
        //        using var conn = this.GetConnection(null, null);
        //        var query = conn.Search(
        //            ldapConfig.OrgBaseDN,
        //            LdapConnection.ScopeSub,
        //            $"objectClass={ldapConfig.OrgObjectClasses}",
        //            orgAttributes,
        //            false);
        //        var orgDnStrs = query.Select(r => r.Dn.Split(",", StringSplitOptions.RemoveEmptyEntries)
        //                                              .Where(s => s.StartsWith("OU="))
        //                                              .SconfigService.GetConfigelect(s => s.Split("=")[0]).ToArray())
        //                             .ToArray();
        //        var departRepo = this.msRepository.Master<Department>();
        //        var departments = departRepo.Context.Set<Department>().ToArray();
        //        var exist = new List<int>();



        //        foreach (var item in orgDnStrs.OrderBy(a => a.Length))
        //        {
        //           if(exi)
        //        }
        //    }
        //}

        ///// <summary>
        ///// 同步角色
        ///// </summary>
        ///// <param name="account"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException"></exception>
        //private async Task SyncUserRoles(string account)
        //{
        //    var ldapConfig = await this.configService.GetConfig<LDAPConfig>(this.tenant, ConstDefs.SystemConfigKey.LdapConfig);
        //    if (ldapConfig == null || string.IsNullOrEmpty(ldapConfig.GroupMembershipAttributeName))
        //    {
        //        throw new ArgumentNullException(nameof(ldapConfig));
        //    }
        //    if (string.IsNullOrEmpty(account))
        //    {
        //        throw new ArgumentNullException(nameof(account));
        //    }
        //    lock (rolesLock)
        //    {
        //        using var conn = this.GetConnection();
        //        var groupAttributes = new List<string>
        //        {
        //            ldapConfig.GroupMembershipAttributeName,
        //            ldapConfig.GroupDisplayAttributeName,
        //            ldapConfig.GroupDescriptionAttributeName
        //        }.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        //        var userAttributes = new List<string>
        //        {
        //            ldapConfig.UserAccountAttributeName,
        //            ldapConfig.UserNameAttributeName,
        //            ldapConfig.UserEMailAttributeName
        //        }.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        //        var queue = conn.Search(
        //            ldapConfig.GroupBaseDN,
        //            LdapConnection.ScopeSub,
        //            $"objectClass={ldapConfig.GroupObjectClasses}",
        //            groupAttributes, false);
        //        var entries = queue.ToDictionary(e => GetGroupDisplaName(e, ldapConfig), e => e);
        //        var userRoleRepo = this.msRepository.Master<UserRole>();
        //        var roles = this.msRepository.Slave1<Role>().AsQueryable().ToArray();
        //        var user = this.msRepository.Slave1<User>().First(u => u.Account == account);
        //        var userRoles = this.msRepository.Slave1<UserRole>().Where(ur => ur.UserId == user.Id).ToList();
        //        var userGroups = new List<Role>();
        //        var uid = $"uid={account},";


        //        foreach (var kvp in entries)
        //        {
        //            var role = roles.FirstOrDefault(r => r.Name == kvp.Key);
        //            if (role == null)
        //            {
        //                role = new Role()
        //                {
        //                    Name = kvp.Key,
        //                    UserRoles = new List<UserRole>()
        //                };
        //                this.msRepository.Master<Role>().Insert(role);
        //            }
        //            var descAttribute = GetLdapAttribute(kvp.Value, ldapConfig.GroupDescriptionAttributeName);
        //            if (descAttribute != null && descAttribute.StringValue != role.Description)
        //            {
        //                role.Description = descAttribute.StringValue;
        //            }
        //            if (!role.IsActive)
        //            {
        //                role.IsActive = true;
        //            }
        //            var memberAttribute = GetLdapAttribute(kvp.Value, ldapConfig.GroupMembershipAttributeName);
        //            if (memberAttribute != null && memberAttribute.StringValueArray.Any(s => s.StartsWith(uid)))
        //            {
        //                userGroups.Add(role);
        //            }
        //        }
        //        foreach (var role in roles.Where(r => !entries.ContainsKey(r.Name)))
        //        {
        //            role.IsActive = false;
        //        }

        //        foreach (var ur in userRoles)
        //        {
        //            if (userGroups.All(g => g.Id != ur.RoleId))
        //            {
        //                userRoleRepo.Delete(ur);
        //            }
        //        }
        //        foreach (var g in userGroups)
        //        {
        //            if (userRoles.All(ur => ur.RoleId != g.Id))
        //            {
        //                if (g.Id == 0)
        //                {
        //                    g.UserRoles.Add(new UserRole()
        //                    {
        //                        UserId = user.Id
        //                    });
        //                }
        //                else
        //                {
        //                    userRoleRepo.Insert(new UserRole()
        //                    {
        //                        UserId = user.Id,
        //                        RoleId = g.Id
        //                    });
        //                }
        //            }
        //        }
        //        if (userAttributes.Length > 0)
        //        {
        //            var userEntry = conn.Read($"uid={account},{ldapConfig.UserBaseDN}", userAttributes);
        //            var realName = GetLdapAttribute(userEntry, ldapConfig.UserNameAttributeName);
        //            if (realName != null && realName.StringValue != user.RealName)
        //            {
        //                user.RealName = realName.StringValue;
        //            }
        //            var emailAttribute = GetLdapAttribute(userEntry, ldapConfig.UserEMailAttributeName);
        //            if (emailAttribute != null && emailAttribute.StringValue != user.EMail)
        //            {
        //                user.EMail = emailAttribute.StringValue;
        //            }
        //        }

        //        userRoleRepo.SaveNow();
        //    }
        //}
        private static readonly LdapConstraints DefaultConstraints = new(0, true, null, 10000);

        private ILdapConnection GetConnection()
        {
            var ldapConfig = this.configService.GetConfig<LDAPConfig>(this.tenant, ConstDefs.SystemConfigKey.LdapConfig).Result;
            if (ldapConfig == null)
            {
                throw new ArgumentNullException(nameof(ldapConfig));
            }
            var conn = new LdapConnection()
            {
                Constraints = DefaultConstraints
            };

            conn.Connect(ldapConfig.ServerName, ldapConfig.Port);
            conn.Bind(ldapConfig.ConnectAsUserId, ldapConfig.ConnectAsUserIdPassword);
            return conn;
        }

        //private static string GetGroupDisplaName(LdapEntry entry, LDAPConfig ldapConfig)
        //{
        //    var entryRoleName = entry.Dn.Split(",", StringSplitOptions.RemoveEmptyEntries)[0].Split('=', StringSplitOptions.RemoveEmptyEntries)[1];
        //    var displayAttribute = GetLdapAttribute(entry, ldapConfig.GroupDisplayAttributeName);
        //    if (displayAttribute != null)
        //    {
        //        entryRoleName = displayAttribute.StringValue;
        //    }
        //    return entryRoleName;
        //}

        private static LdapAttribute GetLdapAttribute(LdapEntry entry, string attributeName)
        {
            if (string.IsNullOrEmpty(attributeName))
            {
                return null;
            }
            try
            {
                return entry.GetAttribute(attributeName);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
