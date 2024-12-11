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

namespace ESys.App
{
    using Microsoft.AspNetCore.Http;
    using System.Reflection;
    using System.Threading.Tasks;

    class DashboardIcoMiddleware
    {
        private readonly RequestDelegate _next;
        public DashboardIcoMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/__schedule__/favicon.ico"))
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                using var stream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.Resources.favicon.ico");
                var buf = new byte[stream.Length];
                await stream.ReadAsync(buf);
                context.Response.ContentType = "image/x-icon";
                context.Response.StatusCode = 200;
                context.Response.ContentLength = stream.Length;
                await context.Response.BodyWriter.WriteAsync(buf);
            }
            else
            {
                await this._next(context);
            }
        }
    }
}
