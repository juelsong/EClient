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
    using ESys.Security.PassValidators;
    using ESys.Security.Utility;
    using ESys.Utilty.Defs;
    using ESys.Utilty.Security;
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    internal class EmisUserService : IUserService
    {
        private static readonly string[] UserPasswordModifiedPropertyNames = new[]
        {
            nameof(User.Password),
            nameof(User.Salt),
            nameof(User.LastPasswordModified)
        };
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
        public EmisUserService(
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
        public async Task<bool> AddUser(User user)
        {
            // TODO 已存在

            var existUser = this.msRepository.Slave1<User>().FirstOrDefault(u => u.Account == user.Account);
            if (existUser != null)
            {
                return false;
            }

            user.Password = PasswordHasher.HashPassword(user.Password, out var salt);
            user.Salt = salt;
            user.IsActive = true;
            user.CreateBy = int.Parse(Furion.App.User.FindFirstValue(ConstDefs.Jwt.UserId));
            user.CreatedTime = DateTimeOffset.Now;
            user.IsHidden = false;
            await this.msRepository.Master<User>().InsertNowAsync(user);
            await this.msRepository.Master<UserProfile>().InsertNowAsync(new UserProfile()
            {
                DashboardConfig = "",
                UserId = user.Id,
                Locale = "zh-cn",//和前台多语言显示一致,
            });
            return true;
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
            userInfo.UserManagementMode = UserManagementMode.ESys;
            return (ErrorCode.NoError, userInfo);
        }

        public async Task<(int ErrorCode, LoginResult Result, bool IsSuper)> Login(LoginParams input)
        {
            var config = await this.configService.GetSecurityConfig(this.tenant);
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
                .Include(u => u.Roles, false)
                .Select(u => new
                {
                    u.Id,
                    u.Account,
                    u.Roles,
                    u.Password,
                    u.Salt,
                    u.RealName,
                    u.IsActive,
                    u.PasswordExpiryPeriod,
                    u.LastPasswordModified,
                    IsSuper = u.Roles.Any(r => r.IsHidden)
                })
                .FirstOrDefault(u => u.Account == input.Account);
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
                       $"{ErrorCode.User.NotExist}");
                return (ErrorCode.User.NotExist, null, false);
            }
            var pass = PasswordHasher.HashPassword(input.Password, user.Salt);
            var loginWrongPassKey = UserServiceFactory.FormatUser(this.tenant, user.Id);
            if (user.Password != pass)
            {
                var value = this.distributedCache.GetString(loginWrongPassKey);
                int.TryParse(value, out var cnt);
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
                return (ErrorCode.User.WrongPassword, null, false);
            }
            this.distributedCache.Remove(loginWrongPassKey);
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

            var output = new LoginResult()
            {
                Account = user.Account,
                Id = user.Id,
                RealName = user.RealName,
            };
            // 首次登录修改密码
            if (!user.IsSuper && true.Equals(config?.ChangePassUponFirstLogin))
            {
                output.ChangePassUponFirstLogin = user.LastPasswordModified == null;
            }
            // 设定密码有效期，超期后修改密码
            if (!user.IsSuper && user.PasswordExpiryPeriod.HasValue && user.PasswordExpiryPeriod < DateTimeOffset.Now)
            {
                output.PasswordExpiried = true;
            }

            await this.msRepository.Master<UserHistory>().InsertNowAsync(new UserHistory()
            {
                UserId = user.Id,
                Logined = DateTimeOffset.UtcNow
            });
            this.log.LogData(new LogInfo()
            {
                Description = loginSuccess,
                TypeName = ConstDefs.LogTypeNames.Login,
                UserId = user.Id,
            });


            return (ErrorCode.NoError, output, user.Roles.Any(r => r.IsActive && r.IsHidden));
        }

        public async Task<(int, string)> PatchPassword(User user)
        {
            var oriUser = this.msRepository.Slave1<User>().Find(user.Id);
            var locale = this.GetUserLocale(user.Id);
            var validatePassMsg = await this.ValidatePasswordMatchRules(
                locale,
                user.Id,
                oriUser.Password,
                oriUser.Salt,
                user.Password,
                false);
            if (!string.IsNullOrEmpty(validatePassMsg))
            {
                return (ErrorCode.User.NotValidPassword, validatePassMsg);
            }
            await this.msRepository.Master<UserPasswordHistory>().InsertAsync(new UserPasswordHistory()
            {
                Salt = oriUser.Salt,
                Password = oriUser.Password,
                UserId = user.Id
            });
            user.Password = PasswordHasher.HashPassword(user.Password, out var salt);
            user.Salt = salt;
            // 设置null 用户登录时需要修改密码
            user.LastPasswordModified = null;
            //var config = await this.configService.GetSecurityConfig(tenantId);

            IEnumerable<string> changedProperties = UserPasswordModifiedPropertyNames;
            //if (config != null && config.ExpiryPeriod.HasValue)
            //{
            //    // 设置密码过期
            //    user.PasswordExpiryPeriod = DateTimeOffset.Now + config.ExpiryPeriod.Value;
            //    changedProperties = changedProperties.Union(new[] { nameof(Entity.User.PasswordExpiryPeriod) });
            //}
            var ctx = this.msRepository.Master<User>().Context;
            var entry = ctx.Attach(user);
            entry.State = EntityState.Modified;
            foreach (var property in entry.Metadata.GetProperties())
            {
                var propertyEntry = entry.Property(property.Name);
                propertyEntry.IsModified = changedProperties.Contains(property.Name);
            }
            await this.msRepository.Master<UserPasswordHistory>().SaveNowAsync();
            return (ErrorCode.NoError, string.Empty);
        }

        public async Task<(int, string)> PatchSelfPassword(int userId, ChangePassSelf model)
        {
            var user = this.msRepository.Slave1<User>().Find(userId);
            if (user == null)
            {
                return (ErrorCode.User.NotExist, null);
            }

            var hashPass = PasswordHasher.HashPassword(model.OriPassword, user.Salt);
            if (user.Password == hashPass)
            {
                var locale = this.GetUserLocale(userId);
                var validatePassMsg = await this.ValidatePasswordMatchRules(
                    locale,
                    userId,
                    user.Password,
                    user.Salt,
                    model.Password,
                    true
                    );
                if (!string.IsNullOrEmpty(validatePassMsg))
                {
                    return (ErrorCode.User.NotValidPassword, validatePassMsg);
                }
                await this.msRepository.Master<UserPasswordHistory>().InsertAsync(new UserPasswordHistory()
                {
                    Salt = user.Salt,
                    Password = user.Password,
                    UserId = user.Id
                });
                user.Password = PasswordHasher.HashPassword(model.Password, out var salt);
                user.Salt = salt;
                user.LastPasswordModified = DateTimeOffset.Now;


                IEnumerable<string> changedProperties = UserPasswordModifiedPropertyNames;
                var config = await this.configService.GetSecurityConfig(this.tenant);
                if (config != null && config.ExpiryPeriod.HasValue)
                {
                    // 设置密码过期
                    user.PasswordExpiryPeriod = DateTimeOffset.Now + config.ExpiryPeriod.Value;
                    changedProperties = changedProperties
                        .Union(new[] { nameof(User.PasswordExpiryPeriod) });
                }
                await this.msRepository.Master<User>().UpdateIncludeAsync(
                    user,
                    changedProperties);
                await this.msRepository.Master<UserPasswordHistory>().SaveNowAsync();
                return (ErrorCode.NoError, null);
            }
            else
            {
                return (ErrorCode.User.WrongPassword, null);
            }



        }
      

        private string GetUserLocale(int userId)
        {
            var userProfile = this.msRepository.Slave1<UserProfile>().FirstOrDefault(i => i.Id == userId);
            return userProfile?.Locale ?? "zh-cn";
        }

        private async Task<string> ValidatePasswordMatchRules(
            string locale,
            int userId,
            string currentPassword,
            string currentSalt,
            string password,
            bool validHistory)
        {
            var config = await this.configService.GetSecurityConfig(this.tenant);
            var errorMsg = string.Empty;
            foreach (var validatorItem in config.PassValidators)
            {
                if (validatorItem.Validator != null)
                {
                    if (!validatorItem.Validator.TryValidatePass(password, locale, ref errorMsg))
                    {
                        break;
                    }
                }
            }
            if (validHistory && string.IsNullOrEmpty(errorMsg) && config.CanNotRepeatedTimes > 0)
            {
                var current = new UserPasswordHistory()
                {
                    Password = currentPassword,
                    Salt = currentSalt,
                    UserId = userId,
                    CreatedTime = DateTimeOffset.Now
                };
                var historyValidatory = new UserPassHistoryValidator(
                    userId,
                    config.CanNotRepeatedTimes,
                    new[] { current }.Union(this.msRepository.Slave1<UserPasswordHistory>().AsQueryable()));
                historyValidatory.TryValidatePass(password, locale, ref errorMsg);
            }
            return errorMsg;
        }


    }
}
