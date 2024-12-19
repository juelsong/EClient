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

using ESys.Contract.Service;
using ESys.Utilty.Defs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace ESys.Security.Middleware
{
    /// <summary>
    /// 租户检测中间件
    /// </summary>
    public class TenantChecker
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public TenantChecker(RequestDelegate next)
        {
            this.next = next;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext ctx)
        {
            //var ass = Furion.App.Assemblies.Where(a => a.GetName().Name.Contains("ESys")).ToArray();
            //System.Diagnostics.Debugger.Launch();
            var tenant = ctx.GetTenantId();
            var tenantService = ctx.RequestServices.GetService<ITenantService>();
            // odata batch
            tenantService.SetTenantScope(tenant);
            //System.Diagnostics.Debug.WriteLine(ctx.Request.GetRequestUrlAddress());
            var skip = ctx.GetMetadata<SkipTenantAttribute>();
            if (ctx.Request.Path.StartsWithSegments("/api/reporting")
                || ctx.Request.Path.StartsWithSegments("/schedule")
                || ctx.Request.Path.StartsWithSegments("/OData/$metadata", System.StringComparison.OrdinalIgnoreCase)
                || ctx.Request.Path.StartsWithSegments("/view", System.StringComparison.OrdinalIgnoreCase)
                || skip != null
                || tenantService.GetCurrentTenant() != null)
            {
                await this.next(ctx);
            }
            else
            {
                await ctx.Response.WriteAsync("you need send a correct tenant id");
            }
        }
    }
}
