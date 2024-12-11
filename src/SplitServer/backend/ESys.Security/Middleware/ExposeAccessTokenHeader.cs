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

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ESys.Security.Middleware
{
    /// <summary>
    /// Access-Control-Expose-Headers 中间件
    /// </summary>
    public class ExposeAccessTokenHeader
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public ExposeAccessTokenHeader(RequestDelegate next)
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
            ctx.Response.Headers["Access-Control-Expose-Headers"] = "access-token, x-access-token,content-disposition";
            await this.next(ctx);
        }
    }
}
