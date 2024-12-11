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

namespace ESys.Controllers
{
    using ESys.Contract.Db;
    using ESys.Contract.Entity;
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Extensions;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.OData.Edm;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Reflection;

    public class DatetimeoffsetConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return ((DateTimeOffset)value).DateTime.ToString();
            }
        }
    }
    public class MapData<T> : ClassMap<T>
    {
        public static string locale = "en";
        public static IEdmModel edmModel = null;
        public MapData() : base()
        {
            var t = typeof(T);
            var type = edmModel.SchemaElements.FirstOrDefault(i => i.Name == t.Name);
            if (type is IEdmStructuredType structuredType)
            {
                var dscProperties = structuredType.Properties();
                var properties = t.GetProperties();
                foreach (var item in properties)
                {
                    switch (locale)
                    {
                        case "zh-cn":
                            var ggg = edmModel.GetDescriptionAnnotation(
                                dscProperties.FirstOrDefault(i => i.Name == item.Name));
                            this.Map(t, item).Name(edmModel.GetDescriptionAnnotation(
                                dscProperties.FirstOrDefault(i => i.Name == item.Name)));
                            break;
                        default:
                            this.Map(t, item).Name(item.Name);
                            break;
                    }
                }

            }
            else
            {
                throw new Exception("no description");
            }
        }
    }
    /// <summary>
    /// 导出csv控制器
    /// </summary>
#if DEBUG
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
#endif
    [ApiController]
    public class ExportCsvController : Controller
    {
        private readonly ILogger<ExportCsvController> logger;
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IEdmModel edmModel;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="msRepository"></param>
        /// <param name="edmModel"></param>
        public ExportCsvController(
           ILogger<ExportCsvController> logger,
           IServiceProvider serviceProvider,
           IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository,
           IEdmModel edmModel)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.msRepository = msRepository;
            this.edmModel = edmModel;
        }
        [HttpGet]
        [Route("[controller]/{entity}")]
        public IActionResult Get(string entity)
        {
            var entityType = Furion.App.EffectiveTypes.First(t => t.Name == entity);
            var path = this.Request.ODataFeature().Path;
            var edmModel = this.HttpContext.RequestServices.GetService<IEdmModel>();
            var ctx = new ODataQueryContext(edmModel, entityType, path);
            var method = this.GetType().GetMethod(nameof(ExportCsvController.BatchSelectEntities), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(entityType);
            return method.Invoke(this, new object[] { ctx, this.Request }) as IActionResult;
        }

        private IActionResult BatchSelectEntities<T>(ODataQueryContext ctx, HttpRequest request) where T : class, IPrivateEntity, new()
        {
            if (!this.serviceProvider.GetService<IDataProvider>().TryGetCurrentUserId(out var currentUserId))
            {
                return this.Forbid();
            }
            var msRepository = this.serviceProvider.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();

            var locale = "";
            var userProfile = msRepository.Slave1<UserProfile>().FirstOrDefault(i => i.UserId == currentUserId);
            if (userProfile == null)
            {
                locale = "zh-cn";
            }
            else
            {
                locale = userProfile.Locale;
            }

            var list = this.msRepository.Slave1<T>();
            var options = new ODataQueryOptions<T>(ctx, request);
            // Disposed by FileStreamResult
            var ms = new MemoryStream();
            using var sw = new StreamWriter(ms, System.Text.Encoding.UTF8, -1, true);
            using var csv = new CsvWriter(sw, CultureInfo.InvariantCulture);
            csv.Context.TypeConverterCache.RemoveConverter(typeof(DateTimeOffset));
            csv.Context.TypeConverterCache.AddConverter(typeof(DateTimeOffset), new DatetimeoffsetConverter());
            var ts = options.ApplyTo(list.AsQueryable()).Cast<T>().ToList();
            var processor = this.serviceProvider.GetService<IEntityCsvProcessor<T>>();

            if (processor != null)
            {
                foreach (var item in ts)
                {
                    processor.Process(item);
                }
            }
            MapData<T>.locale = locale;
            MapData<T>.edmModel = this.edmModel;

            csv.Context.RegisterClassMap<MapData<T>>();
            csv.WriteRecords(ts);
            ms.Flush();
            sw.Flush();
            csv.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            this.Response.ContentType = new MediaTypeHeaderValue("application/octet-stream").ToString();// Content type
            var result = new FileStreamResult(ms, "application/octet-stream")
            {
                FileDownloadName = "Export_" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv"
            };

            return result;
        }
    }
}
