using ESys.Contract.Db;
using ESys.Contract.Entity;
using ESys.Contract.Feature;
using ESys.Contract.Service;
using ESys.Db.DbContext;
using ESys.Security.Entity;
using ESys.Utilty.Defs;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Conventions;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace ESys.Extensions
{
    internal class ODataMetaJsonMiddleware
    {
        private readonly RequestDelegate next;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public ODataMetaJsonMiddleware(RequestDelegate next)
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
            if (ctx.Request.Path.Equals("/OData/$metadata", StringComparison.OrdinalIgnoreCase)
                && "json".Equals(ctx.Request.Query["lang"], StringComparison.OrdinalIgnoreCase))
            {

                ctx.Response.ContentType = "application/json";
            }
            await this.next(ctx);
        }
    }

    public static class MvcBuilderExtensions
    {
        internal class ComplexTypeSegment : ODataPathSegment
        {
            public override IEdmType EdmType => this.ComplexType;

            public IEdmComplexType ComplexType { get; }
            public ComplexTypeSegment(IEdmComplexType complexType)
            {
                this.ComplexType = complexType;
            }
            public override void HandleWith(PathSegmentHandler handler)
            {
            }

            public override T TranslateWith<T>(PathSegmentTranslator<T> translator)
            {
                return default(T);
            }
        }
        private class ComplexTypeTemplate : ODataSegmentTemplate
        {
            public IEdmComplexType ComplexType { get; }
            public ComplexTypeTemplate(IEdmComplexType complexType)
            {
                this.ComplexType = complexType;
            }
            public override IEnumerable<string> GetTemplates(ODataRouteOptions options)
            {
                yield return $"/{this.ComplexType.Name}";
            }

            public override bool TryTranslate(ODataTemplateTranslateContext context)
            {
                context.Segments.Add(new ComplexTypeSegment(this.ComplexType));
                return true;
            }
        }

        private class ComplexTypeRoutingConvention : IODataControllerActionConvention
        {
            public virtual int Order => 900;

            public bool AppliesToAction(ODataControllerActionContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var action = context.Action;
                var model = context.Model.SchemaElements.FirstOrDefault(n => n.Name == context.Controller.ControllerName);
                (string httpMethod, string castTypeName) = Split(action.ActionName);
                if (httpMethod == null)
                {
                    return false;
                }

                if (model is IEdmComplexType complexType)
                {
                    IList<ODataSegmentTemplate> segments = new List<ODataSegmentTemplate>
                    {
                        new ComplexTypeTemplate(complexType)
                    };

                    action.AddSelector(httpMethod, context.Prefix, context.Model, new ODataPathTemplate(segments), context.Options?.RouteOptions);
                    return true;
                }
                return false;
            }

            internal static (string, string) Split(string actionName)
            {
                string typeName;
                string methodName = null;
                if (actionName.StartsWith("Get", StringComparison.Ordinal))
                {
                    methodName = "Get";
                }
                else if (actionName.StartsWith("Put", StringComparison.Ordinal))
                {
                    methodName = "Put";
                }
                else if (actionName.StartsWith("Patch", StringComparison.Ordinal))
                {
                    methodName = "Patch";
                }
                else if (actionName.StartsWith("Delete", StringComparison.Ordinal))
                {
                    methodName = "Delete";
                }

                if (methodName != null)
                {
                    typeName = actionName.Substring(methodName.Length);
                }
                else
                {
                    return (null, null);
                }

                if (string.IsNullOrEmpty(typeName))
                {
                    return (methodName, null);
                }

                return (methodName, typeName);
            }

            public bool AppliesToController(ODataControllerActionContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }
                var model = context.Model.SchemaElements.FirstOrDefault(n => n.Name == context.Controller.ControllerName);
                if (model != null)
                {
                    return true;
                }
                return false;
            }
        }
        private class ODataCorsBatchHandler : DefaultODataBatchHandler
        {
            private class ESignFeature : IESignFeature
            {
                public ESignFeature(IEnumerable<ESignData> data)
                {
                    this.ESignDatas = data.ToArray();
                }

                public ESignData[] ESignDatas { get; }
            }
            public override Task ProcessBatchAsync(HttpContext context, RequestDelegate nextHandler)
            {
                if (context.Request.Method == HttpMethods.Options)
                {
                    context.Response.StatusCode = 200;
                    return nextHandler(context);
                }
                else
                {
                    // 客户端离线批量提交
                    if (context.Request.Headers.TryGetValue(ConstDefs.RequestHeader.Offline, out var offlineData))
                    {
                        using var scope = context.RequestServices.CreateScope();
                        var str = Encoding.UTF8.GetString(Convert.FromBase64String(offlineData));
                        var esArray = JsonSerializer.Deserialize<ElectronicSignature[]>(str);
                        var ddd = esArray.Select(es => new ESignData()
                        {
                            Account = es.Account,
                            RealName = es.RealName,
                            ESignedBy = es.UserId,
                            Timestamp = es.SignDate,
                            IpAddress = es.IpAddress,
                            Category = es.Category,
                            Comment = es.Comment,
                            UserId = es.CreateBy,
                            IsSystemOperation = es.IsSystemOperation,
                            Order = es.Order,
                        }).ToArray();
                        // 批量处理时会每个子请求创建Scope，IDataInjector失效
                        context.Features.Set<IESignFeature>(new ESignFeature(ddd));
                    }
                    context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                    return base.ProcessBatchAsync(context, nextHandler);
                }
            }
        }

        public static IMvcBuilder AddODataEntity(this IMvcBuilder builder, out IEdmModel model)
        {
            var innerModel = model = GetEdmModel();

            return builder.AddOData(o =>
            {
                o.AddRouteComponents("OData", innerModel,  services =>
                {
                    var batchHandler = new ODataCorsBatchHandler();
                    services.AddSingleton<ODataBatchHandler>(batchHandler);

                    var odataServiceConfigureTypes = Furion.App.EffectiveTypes
                                                           .Where(t => typeof(IODataServiceConfigure).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                                                           .Select(t => Activator.CreateInstance(t))
                                                           .Cast<IODataServiceConfigure>()
                                                           .ToList();
                    foreach (var item in odataServiceConfigureTypes)
                    {
                        item.Configure(services);
                    }
                    services.AddLogging(lb =>
                    {
                        lb
                        .AddConsole()
                        .AddDebug()
#if CLIENT
                        .AddLog4Net("log4net_app_settings.xml")
#else
                        .AddLog4Net()
#endif
                        ;
                    });
                    services.AddDatabaseAccessor(options =>
                    {
                        Action<IServiceProvider, DbContextOptionsBuilder> dbAction = (sp, opt) =>
                        {
                            var factory = sp.GetRequiredService<ILoggerFactory>();
                            opt.UseLoggerFactory(factory)
                               .EnableSensitiveDataLogging()
                               .EnableDetailedErrors()
                            ;
                        };
                        options
                        .CustomizeMultiTenants()
                        .AddDb<MasterDbContext>()
                        .AddDb<TenantMasterDbContext, TenantMasterLocator>(dbAction)
                        .AddDb<TenantSlaveDbContext, TenantSlaveLocator>(dbAction)

                        ;
                    });
                    // 无需注入 IDataProvider IDataInjector ITenantService 等 过滤组件全部使用HttpContext中的ServiceProvider
                })
                  .EnableQueryFeatures();
                o.Conventions.Add(new ComplexTypeRoutingConvention());
            });
        }

        internal static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder()
            {
                ContainerName = "EmisContainer",
                Namespace = "ESys.OData",
            };
            var configs = Furion.App.EffectiveTypes
                .Where(t => typeof(IODataEntityBuilder).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .Select(t => Activator.CreateInstance(t))
                .Cast<IODataEntityBuilder>()
                .ToList();
            foreach (var config in configs)
            {
                config.BuildODataModel(builder);
            }
            foreach (var type in builder.StructuralTypes)
            {
                type.Namespace = $"{type.ClrType.Namespace}.OData";
            }
            foreach (var type in builder.EnumTypes)
            {
                type.Namespace = $"{type.ClrType.Namespace}.OData";
            }
            builder.Namespace = "ESys.OData";

            var comments = Furion.App.Assemblies
                    .Select(ass => Path.Join(Path.GetDirectoryName(ass.Location), $"{ass.GetName().Name}.xml"))
                    .Where(path => File.Exists(path))
                    .SelectMany(path =>
                    {
                        var doc = new XmlDocument();
                        doc.Load(path);
                        return doc.SelectNodes("//doc/members/member").Cast<XmlNode>();
                    })
                    .ToArray()
                    .ToDictionary(
                        n => n.Attributes.GetNamedItem("name").Value[2..],
                        n => n.ChildNodes[0].InnerText.Trim());

            var model = builder.GetEdmModel() as EdmModel;
            foreach (var schemaElement in model.SchemaElements)
            {
                var typeName = schemaElement.FullName().Replace(".OData.", ".");
                if (comments.TryGetValue(typeName, out var typeComment))
                {
                    model.SetDescriptionAnnotation(schemaElement, typeComment);
                }
                if (schemaElement is IEdmStructuredType structuredType)
                {
                    var clrType = Furion.App.EffectiveTypes.FirstOrDefault(t => $"{t.Namespace}.OData.{t.Name}" == structuredType.FullTypeName());
                    if (clrType == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"lack for assembly include type:{structuredType.FullTypeName()}");
                        continue;
                    }
                    var baseType = true.Equals(clrType.BaseType?.IsGenericType) ? clrType.BaseType.GetGenericTypeDefinition() : clrType.BaseType;
                    foreach (var property in structuredType.Properties())
                    {
                        if (comments.TryGetValue($"{typeName}.{property.Name}", out var propertyComment))
                        {
                            model.SetDescriptionAnnotation(property, propertyComment);
                        }
                        else if (baseType != null)
                        {
                            var baseProperty = baseType.GetProperty(property.Name);
                            if (baseProperty != null
                                && comments.TryGetValue($"{baseType.FullName}.{property.Name}", out propertyComment))
                            {
                                model.SetDescriptionAnnotation(property, propertyComment);
                            }
                        }
                    }
                }
            }
            // using var writer = global::System.Xml.XmlWriter.Create("d:/tmp/edm/test.xml", new global::System.Xml.XmlWriterSettings()
            // {
            //     Indent = true
            // });
            // Microsoft.OData.Edm.Csdl.CsdlWriter.TryWriteCsdl(model, writer, Microsoft.OData.Edm.Csdl.CsdlTarget.EntityFramework, out var errors);
            return model;
        }
    }
}