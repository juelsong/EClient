namespace EWebServer.Middleware
{
    public class SwaggerTenantIdMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerTenantIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (context.Request.Path.StartsWithSegments("/swagger/v1/swagger.json"))
            {
                // 检查查询字符串中是否包含TenantId
                if (!context.Request.Query.ContainsKey("TenantId"))
                {
                    // 从Swagger UI中获取租户ID
                    string tenantId = context.Request.Headers["X-TenantId"].ToString() ?? "dev";

                    if (!string.IsNullOrEmpty(tenantId))
                    {
                        // 创建一个新的查询字符串，包含租户ID
                        string newQueryString = context.Request.QueryString.HasValue ? context.Request.QueryString.Value + "&TenantId=" + tenantId : "?TenantId=" + tenantId;

                        // 重写请求以包含新的查询字符串
                        context.Request.QueryString = new QueryString(newQueryString);
                        //// 将租户ID添加到查询字符串中
                        //context.Request.QueryString = new QueryString(context.Request.QueryString.ToString() + "&TenantId=" + tenantId);
                    }
                }
            }

            // 继续执行请求
            await _next(context);
        }
    }

}
