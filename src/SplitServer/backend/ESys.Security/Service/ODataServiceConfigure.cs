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
namespace ESys.Security.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Query;
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using ESys.Utilty.Defs;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.OData.Edm;
    using Microsoft.AspNetCore.OData.Extensions;
    using Microsoft.AspNetCore.OData.Query.Expressions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.OData.Edm;
    using Microsoft.OData.UriParser;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Security.Claims;
    using System.Threading.Tasks;

    internal class LocationSelectExpandBinder : SelectExpandBinder
    {
        private static readonly FieldInfo FilterClauseExpressionField = typeof(FilterClause).GetField(
                                                                        "expression",
                                                                        BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly PropertyInfo FilterClausePropertyInfo = typeof(ExpandedReferenceSelectItem).GetProperty(
                                                                        nameof(ExpandedReferenceSelectItem.FilterOption));
        private readonly IEdmModel edmModel;
        private readonly List<IEntityQueryVisitor> visitors;

        public LocationSelectExpandBinder(
            IEdmModel edmModel,
            IEnumerable<IEntityQueryVisitor> visitors)
        {
            this.edmModel = edmModel;
            this.visitors = new(visitors);
        }
        private static IODataTypeMapper odataTypeMapper = new DefaultODataTypeMapper();
        public static Type GetClrType(IEdmModel model, IEdmType type)
        {
            IEdmTypeReference typeReference = null;
            switch (type.TypeKind)
            {
                case EdmTypeKind.Collection:
                    typeReference = ((IEdmCollectionType)type).ElementType;
                    break;
                case EdmTypeKind.Complex:
                    typeReference = new EdmComplexTypeReference((IEdmComplexType)type, false);
                    break;
                default:
                    throw new Exception("");
            }
            return odataTypeMapper.GetClrType(model, typeReference);
        }

        protected override LambdaExpression GetProjectionLambda(SelectExpandBinderContext context)
        {
            var httpContext = context.SelectExpand.Context.RequestContainer.GetService<IHttpContextAccessor>().HttpContext;
            if (httpContext.TryGetUserId(out var userId))
            {
                var repository = httpContext.RequestServices.GetService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
                var breadcrumb = repository.Slave1<User>()
                                           .Where(u => u.Id == userId)
                                           .Select(u => u.Location.LocationExtra.Breadcrumb)
                                           .FirstOrDefault();
                if (!string.IsNullOrEmpty(breadcrumb))
                {
                    var types = new Stack<Type>();
                    types.Push(context.SelectExpand.Context.ElementClrType);
                    this.VisitSelectExpandClause(userId, breadcrumb, types, context.SelectExpand.SelectExpandClause, this.edmModel);
                }
            }
            var ret = base.GetProjectionLambda(context);
            return ret;
        }

        private void VisitSelectExpandClause(int userId, string breadcrumb, Stack<Type> types, SelectExpandClause selectExpandClause, IEdmModel model)
        {
            foreach (var item in selectExpandClause.SelectedItems.OfType<ExpandedNavigationSelectItem>())
            {
                var itemClrType = GetClrType(model, item.NavigationSource.Type);
                types.Push(itemClrType);
                var typesArray = types.Reverse().ToArray();
                var expList = new List<SingleValueNode>();
                foreach (var visitor in this.visitors)
                {
                    var exp = visitor.VisitQuery(userId, breadcrumb, model, typesArray);
                    if (exp != null)
                    {
                        expList.Add(exp);
                    }
                }
                if (expList.Count > 0)
                {
                    if (item.FilterOption == null)
                    {
                        var itVariable = new ResourceRangeVariable("$it", (item.NavigationSource.Type as EdmCollectionType).ElementType.AsStructured(), item.NavigationSource);
                        var exp = expList.First();
                        foreach (var tmp in expList.Skip(1))
                        {
                            exp = new BinaryOperatorNode(BinaryOperatorKind.And, exp, tmp);
                        }
                        var filterOption = new FilterClause(exp, itVariable);
                        FilterClausePropertyInfo.SetValue(item, filterOption);
                    }
                    else
                    {
                        var expression = item.FilterOption.Expression;
                        foreach (var e in expList)
                        {
                            expression = new BinaryOperatorNode(BinaryOperatorKind.And, expression, e);
                        }
                        FilterClauseExpressionField.SetValue(item.FilterOption, expression);
                    }
                }
                if (item.SelectAndExpand != null)
                {
                    this.VisitSelectExpandClause(userId, breadcrumb, types, item.SelectAndExpand, model);
                }
                types.Pop();
            }
        }
    }
    /// <summary>
    /// 安全服务Odata配置
    /// </summary>
    public class ODataServiceConfigure : IODataServiceConfigure, IServiceConfigure
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceCollection Configure(IServiceCollection services)
        {
            services.AddSingleton<ISelectExpandBinder, LocationSelectExpandBinder>();
            services.Scan(scan => scan.FromAssemblies(Furion.App.Assemblies)
                                      .AddClasses(c => c.AssignableTo<IEntityQueryVisitor>()).As<IEntityQueryVisitor>().WithSingletonLifetime());
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="startBackground"></param>
        /// <returns></returns>
        public IServiceCollection Configure(IServiceCollection services, bool startBackground)
        {
            return this.Configure(services);
        }
    }

    internal class ODataAutoFilterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<ODataAutoFilterMiddleware> logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="logger"></param>
        public ODataAutoFilterMiddleware(
            RequestDelegate next,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ODataAutoFilterMiddleware> logger)
        {
            this.next = next;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }
        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext ctx)
        {
            if (ctx.Request.Path.StartsWithSegments("/OData")
                && !ctx.Request.Path.StartsWithSegments("/OData/$metadata"))
            {
                var odataFeature = ctx.Request.ODataFeature();
                var firstPath = odataFeature.Path?.FirstOrDefault();
                if (firstPath != null)
                {
                    this.httpContextAccessor.HttpContext = ctx;
                    var clrType = LocationSelectExpandBinder.GetClrType(odataFeature.Model, firstPath.EdmType);
                    var tenant = ctx.GetTenantId();
                    if (int.TryParse(ctx.User.FindFirstValue(ConstDefs.Jwt.UserId), out var userId))
                    {
                        var repository = ctx.RequestServices.GetRequiredService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>();
                        var breadcrumb = repository.Slave1<User>()
                                                   .Where(u => u.Id == userId)
                                                   .Select(u => u.Location.LocationExtra.Breadcrumb)
                                                   .FirstOrDefault();
                        var visitors = ctx.RequestServices.GetServices<IEntityQueryVisitor>().ToArray();
                        var tenantService = ctx.RequestServices.GetRequiredService<ITenantService>();
                        tenantService.SetTenantScope(tenant);
                        var list = new List<string>();
                        foreach (var visitor in visitors)
                        {
                            var filter = visitor.VisitQuery(userId, breadcrumb, clrType);
                            if (!string.IsNullOrEmpty(filter))
                            {
                                list.Add(filter);
                            }
                        }
                        if (list.Count > 0)
                        {
                            var oriFilterStr = ctx.Request.Query["$filter"];
                            var filterStr = string.Join(" and ", list.Union(new string[] { oriFilterStr })
                                                                     .Where(str => !string.IsNullOrEmpty(str)).Select(x => $"({x})"));
                            var queryString = new QueryString($"?$filter={filterStr}");
                            foreach (var key in ctx.Request.Query.Keys.Where(k => k != "$filter"))
                            {

                                queryString = queryString.Add(key, ctx.Request.Query[key]);
                            }
                            ctx.Request.QueryString = queryString;
                        }
                        // 无需关心 IDataInjector InjectCurrentUserId，LocationSelectExpandBinder使用httpcontext的serviceprovider
                    }
                    else
                    {
                        this.logger.LogError("can not get UserId\t{path}", ctx.Request.Path);
                    }
                }
            }
            await this.next(ctx);
        }
    }
    /// <summary>
    /// 安全模块App配置
    /// </summary>
    public class ApplicationBuilderConfigure : IApplicationBuilderConfigure
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ODataAutoFilterMiddleware>();
        }
    }
}
