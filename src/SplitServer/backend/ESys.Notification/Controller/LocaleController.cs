namespace ESys.Schedule.Controllers
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Infrastructure.Entity;
    using ESys.Notification.Entity;
    using ESys.Security.Entity;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 修改user接口
    /// </summary>
    public class UpdateLocalParam
    {
        /// <summary>
        /// 默认显示中英文
        /// </summary>
        public string Locale { get; set; }
    }

    /// <summary>
    /// 测试方法控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ODataIgnored]
    public class LocaleController : Controller
    {
        private readonly ILogger<LocaleController> logger;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="msRepository"></param>
        /// <param name="serviceProvider"></param>
        public LocaleController(
            ILogger<LocaleController> logger,
            IServiceProvider serviceProvider,
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository)
        {
            this.logger = logger;
            this.msRepository = msRepository;
            this.serviceProvider = serviceProvider;

        }
        /// <summary>
        /// 获取当前用户默认设置语言
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserLocale")]
        public Result GetCurrentId()
        {
            if (!this.serviceProvider.GetService<IDataProvider>().TryGetCurrentUserId(out var currentUserId))
            {
                return ResultBuilder.Error(ErrorCode.User.TokenExpired);
            }
            var userProfile = this.msRepository.Slave1<UserProfile>().FirstOrDefault(i => i.Id == currentUserId);
            return ResultBuilder.Ok(new { UserLocal = userProfile.Locale });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpDateLocale")]
        public Result UpDateLocale(UpdateLocalParam locale)
        {
            if (!this.serviceProvider.GetService<IDataProvider>().TryGetCurrentUserId(out var currentUserId))
            {
                return ResultBuilder.Error(ErrorCode.User.TokenExpired);
            }
            var userProfile = this.msRepository.Slave1<UserProfile>().FirstOrDefault(i => i.UserId == currentUserId);
            if (userProfile is null)
            {
                this.msRepository.Master<UserProfile>().InsertNow(new UserProfile()
                {
                    UserId = currentUserId,
                    DashboardConfig = "",
                    Locale = locale.Locale
                });

            }
            else
            {
                userProfile.Locale = locale.Locale;
                this.msRepository.Master<UserProfile>().UpdateNow(userProfile);
            }

            return ResultBuilder.Ok(true);
        }
    }
}
