using ESys.Contract.Db;
using ESys.Contract.Service;
using ESys.Security.Entity;
using ESys.Security.Handler;
using ESys.Security.Models;
using ESys.Security.Service;
using ESys.Security.Utility;
using ESys.Utilty.Attributes;
using ESys.Utilty.Defs;
using Furion;
using Furion.Authorization;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ESys.Security.ApiControllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("api/user/[action]")]
    public class UserController : ControllerBase
    {
        private static readonly string BASE_CHECK_CODES = "qwertyuiplkjhgfdsazxcvbnmQWERTYUPLKJHGFDSAZXCVBNM1234567890";

        private readonly ILogger<UserController> logger;
        private readonly IUserService userService;
        private readonly ILogService logService;
        private readonly IMemoryCache memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private readonly IOptions<JWTSettingsOptions> jwtOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="log"></param>
        /// <param name="userServiceFactory"></param>
        /// <param name="memoryCache"></param>
        /// <param name="msRepository"></param>
        /// <param name="httpContextAccessor"></param>
        public UserController(
            ILogger<UserController> logger,
            IOptions<JWTSettingsOptions> jwtOptions,
            ILogService log,
            IUserServiceFactory userServiceFactory,
            IMemoryCache memoryCache,
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.jwtOptions = jwtOptions;
            this.logService = log;
            this.memoryCache = memoryCache;
            this.userService = userServiceFactory.GetUserService();
            this.msRepository = msRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("login")]
        public async Task<Result<LoginResult>> Login(LoginParams input)
        {
            var ret = await this.userService.Login(input);
            if (ret.ErrorCode == ErrorCode.NoError)
            {
                var refreshExpiredMins = AuthHandler.GetRefreshTokenExpiredMinutes(this.jwtOptions.Value);
                var user = ret.Result;
                var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
                    {
                        { ConstDefs.Jwt.UserId, user.Id },  // 存储Id
                        { ConstDefs.Jwt.Account, user.Account }, // 存储用户名
                        { ConstDefs.Jwt.IsSuper, ret.IsSuper },
                        { ConstDefs.Jwt.Tenant, this.httpContextAccessor.HttpContext.Request.Headers[ConstDefs.RequestHeader.Tenant] },
                    });
                var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, refreshExpiredMins);
                var headers = this.httpContextAccessor.HttpContext.Response.Headers;
                headers[ConstDefs.ResponseHeader.AccessToken] = accessToken;
                headers[ConstDefs.ResponseHeader.RefreshAccessToken] = refreshToken;
                return ResultBuilder.Ok(ret.Result);
            }
            else
            {
                return ResultBuilder.Error<LoginResult>(ret.ErrorCode);
            }
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("info")]
        public async Task<Result<UserInfo>> Info()
        {
            if (!int.TryParse(
                    this.httpContextAccessor.HttpContext?.User.FindFirstValue(ConstDefs.Jwt.UserId), 
                    out var userId))
            {
                return ResultBuilder.Error<UserInfo>(ErrorCode.User.TokenExpired);
            }
            var ret = await this.userService.GetUserInfo(userId);
            return ret.ErrorCode == ErrorCode.NoError
                ? ResultBuilder.Ok(ret.Result)
                : ResultBuilder.Error<UserInfo>(ret.ErrorCode);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [IfException(1000, ErrorMessage = "获取验证码出错")]
        [HttpGet]
        [ActionName("random-image")]
        [Route("{key}")]
        public Result<string> RandomImage(string key)
        {
            try
            {
                var code = RandomUtil.RandomString(BASE_CHECK_CODES, 4);
                var lowerCaseCode = code.ToLower();
                var realKey = MD5Util.MD5Encode(lowerCaseCode + key);
                this.memoryCache.Set(realKey, lowerCaseCode, TimeSpan.FromSeconds(60));
#if DEBUG
                global::System.Diagnostics.Debug.WriteLine($"code:{lowerCaseCode}\rkey:{realKey}");
#endif
                var base64 = RandImageUtil.GenerateCodeImageBase64(code);
                return ResultBuilder.Ok(base64);
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "获取验证码出错");
                throw Oops.Oh(1000);
            }
        }


        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("logout")]
        public Result<bool> Logout()
        {
            // TODO token 加入redis黑名单  中间件过滤
            if (int.TryParse(this.httpContextAccessor.HttpContext?.User.FindFirstValue(ConstDefs.Jwt.UserId), out var userId))
            {
                var user = this.msRepository.Master<User>().FirstOrDefault(i => i.Id == userId);
                this.logService.LogData(new LogInfo()
                {
                    Description = $"Logout",
                    TypeName = ConstDefs.LogTypeNames.Logout,
                    UserId = userId,
                });
            }
            return ResultBuilder.Ok(true);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // TODO 组织机构？
        [RequiresPermission("User:Add")]
        [HttpPost]
        [ActionName("")]
        public async Task<Result<bool>> Post(User user)
        {
            var ret = await this.userService.AddUser(user);
            return ret ? ResultBuilder.Ok(true) : ResultBuilder.Error<bool>(ErrorCode.User.AlreadyExist);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [RequiresPermission("User:Edit")]
        [HttpPatch]
        [ActionName("password")]
        public async Task<Result<bool>> PatchPassword(User user)
        {
            if (int.TryParse(App.User.FindFirstValue(ConstDefs.Jwt.UserId), out var userId))
            {
                var ret = await this.userService.PatchPassword(user);
                return ret.ErrorCode == ErrorCode.NoError
                                ? ResultBuilder.Ok(true)
                                : ResultBuilder.Error<bool>(ret.ErrorCode, ret.Msg);
            }
            else
            {
                return ResultBuilder.Error<bool>(ErrorCode.User.TokenExpired);
            }
        }

        /// <summary>
        /// 修改自己密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("self-password")]
        public async Task<Result<bool>> PatchSelfPassword(ChangePassSelf model)
        {
            if (this.ModelState.IsValid)
            {
                if (int.TryParse(App.User.FindFirstValue(ConstDefs.Jwt.UserId), out var userId))
                {
                    var ret = await this.userService.PatchSelfPassword(userId, model);
                    return ret.ErrorCode == ErrorCode.NoError
                                        ? ResultBuilder.Ok(true)
                                        : ResultBuilder.Error<bool>(ret.ErrorCode, ret.Msg);
                }
                else
                {
                    return ResultBuilder.Error<bool>(ErrorCode.User.TokenExpired);
                }
            }
            else
            {
                return ResultBuilder.Error(ErrorCode.Service.InvalidModel, false);
            }
        }

        /// <summary>
        /// 设置用户配置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPatch]
        [ActionName("user-settings")]
        public async Task<Result<bool>> PatchUserSettings(UserSettings settings)
        {
            if (this.ModelState.IsValid)
            {
                if (int.TryParse(App.User.FindFirstValue(ConstDefs.Jwt.UserId), out var userId))
                {
                    var settingsStr = JsonSerializer.Serialize(settings);
                    var profile = await this.msRepository.Slave1<UserProfile>().FirstOrDefaultAsync(u => u.UserId == userId);
                    var writeRepo = this.msRepository.Master<UserProfile>();
                    if (profile == null)
                    {
                        profile = new UserProfile()
                        {
                            UserSettings = settingsStr
                        };
                        await writeRepo.InsertAsync(profile);
                    }
                    else
                    {
                        profile.UserSettings = settingsStr;
                        await writeRepo.UpdateAsync(profile);
                    }
                    await writeRepo.SaveNowAsync();
                    return ResultBuilder.Ok(true);
                }
                else
                {
                    return ResultBuilder.Error<bool>(ErrorCode.User.TokenExpired, false);
                }
            }
            else
            {
                return ResultBuilder.Error<bool>(ErrorCode.Service.InvalidModel);
            }
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("token")]
        public Result<bool> RefreshToken()
        {
            return ResultBuilder.Ok(true);
        }
    }
}
