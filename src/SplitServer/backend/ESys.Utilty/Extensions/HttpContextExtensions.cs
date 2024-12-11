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

using ESys.Utilty.Defs;
using System.Collections.Generic;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// HttpContex扩展
    /// </summary>
    public static class EmisHttpContextExtensions
    {
        /// <summary>
        /// 获取 Action 特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static IEnumerable<TAttribute> GetAllMetadata<TAttribute>(this HttpContext httpContext)
            where TAttribute : class
        {
            return httpContext.GetEndpoint()?.Metadata?.GetOrderedMetadata<TAttribute>();
        }

        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetTenantId(this HttpContext httpContext)
        {
            return httpContext.Request.Headers[ConstDefs.RequestHeader.Tenant];
        }

        /// <summary>
        /// 尝试获取单个头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetSingleHeader(this HttpRequest request, string key, out string value)
        {
            if (request.Headers.TryGetValue(key, out var values) && values.Count == 1)
            {
                value = values[0];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
        /// <summary>
        /// 尝试获取当前用户Id
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool TryGetUserId(this HttpContext httpContext, out int userId)
        {
            var userIdStr = httpContext.User.FindFirstValue(ConstDefs.Jwt.UserId);
            return int.TryParse(userIdStr, out userId);
        }
    }
}
