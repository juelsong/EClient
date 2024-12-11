namespace ESys.App.Handler
{
    using Furion.DependencyInjection;
    using Furion.FriendlyException;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class LogExceptionHandler : IGlobalExceptionHandler, ISingleton
    {
        private readonly ILogger logger;
        public LogExceptionHandler(ILogger<LogExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            this.logger.LogError(context.Exception,$"path:{context.HttpContext.Request.Path}");
            return Task.CompletedTask;
        }
    }
}
