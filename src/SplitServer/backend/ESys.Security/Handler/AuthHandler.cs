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
namespace ESys.Security.Handler
{
    using ESys.Contract.Service;
    using ESys.Utilty.Attributes;
    using ESys.Utilty.Defs;
    using Furion.Authorization;
    using Furion.DataEncryption;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.OData.Routing.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// 权鉴
    /// </summary>
    public class AuthHandler : AppAuthorizeHandler
    {
        private static readonly Type metadataControllerType = typeof(MetadataController);
        private readonly ILogger<AuthHandler> logger;
        const int refreshTokenExpiredMins = 5;
        private readonly IOptions<JWTSettingsOptions> jwtOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jwtOptions"></param>
        public AuthHandler(ILogger<AuthHandler> logger, IOptions<JWTSettingsOptions> jwtOptions)
        {
            this.logger = logger;
            this.jwtOptions = jwtOptions;
        }
        /// <summary>
        /// 验证管道
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override Task<bool> PipelineAsync(
            AuthorizationHandlerContext context,
            DefaultHttpContext httpContext)
        {
            var flag = false;
            try
            {
                flag = this.CheckAuthorzie(httpContext);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, message: string.Empty);
            }
            if (!flag)
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            return Task.FromResult(flag);
        }
        /// <summary>
        /// 授权验证核心方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            var flag = false;
            try
            {
                flag = context.GetCurrentHttpContext()
                              .GetControllerActionDescriptor()
                              .ControllerTypeInfo
                              .AsType() == metadataControllerType
                    || JWTEncryption.AutoRefreshToken(
                                context,
                                context.GetCurrentHttpContext(),
                                refreshTokenExpiredTime: GetRefreshTokenExpiredMinutes(this.jwtOptions.Value));
            }
            catch (System.Exception ex)
            {
                this.logger.LogError(ex, message: string.Empty);
            }
            if (flag)
            {
                return this.AuthorizeHandleAsync(context);
            }
            else
            {
                context.GetCurrentHttpContext().Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.Run(context.Fail);
            }
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private bool CheckAuthorzie(DefaultHttpContext httpContext)
        {
            var sss = Furion.App.GetOptions<JWTSettingsOptions>();
            var userId = -1;
            if (httpContext.User != null
                && int.TryParse(httpContext.User.FindFirstValue(ConstDefs.Jwt.UserId), out userId))
            {
                httpContext.RequestServices.GetService<IDataInjector>().InjectCurrentUserId(userId);
            }
            // 超级用户有所有权限
            if ("true".Equals(httpContext?.User.FindFirstValue(ConstDefs.Jwt.IsSuper))
                || httpContext.GetControllerActionDescriptor()
                              .ControllerTypeInfo.AsType() == metadataControllerType)
            {
                return true;
            }
            // 登录用户信息租户与header里租户不一致
            if (string.Compare(
                httpContext.Request.Headers[ConstDefs.RequestHeader.Tenant],
                httpContext?.User.FindFirstValue(ConstDefs.Jwt.Tenant)) != 0)
            {
                this.logger.LogInformation(
                    "TenantDiff:{headerTenant}\t{jwtTenant}",
                    httpContext.Request.Headers[ConstDefs.RequestHeader.Tenant],
                    httpContext?.User.FindFirstValue(ConstDefs.Jwt.Tenant));
                return false;
            }
            // 获取权限特性
            var permissionAttrs = httpContext.GetAllMetadata<RequiresPermissionAttribute>();
            if (permissionAttrs == null || !permissionAttrs.Any())
            {
                // 没有权限限制
                return true;
            }
            // 解析服务
            var permissionManager = httpContext.RequestServices.GetService<IPermissionManager>();

            // 检查授权
            var hasPremission = permissionAttrs.Any(
                attr => permissionManager.HasAllPermission(userId, attr.Permissions));
            if (!hasPremission)
            {
                this.logger.LogInformation(
                    "try visit:{url}\t{tenant}\t{userId}",
                    httpContext.Request.Path.Value,
                    httpContext.Request.Headers[ConstDefs.RequestHeader.Tenant],
                    userId);
            }
            return hasPremission;
        }

        internal static int GetRefreshTokenExpiredMinutes(JWTSettingsOptions options)
            => (int)(options.ExpiredTime ?? 20) + refreshTokenExpiredMins;
    }
}
