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
namespace ESys.DataGenerator
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [Generator]
    class ODataControllerGenerator : ISourceGenerator
    {
        private class NavigationPropertyMeta
        {
            public NavigationPropertyMeta(
                string navigationPropertyName,
                string navigationPropertyType,
                string navigatioinDisplayProperty,
                bool hasActive,
                bool checkActive,
                bool isCollection)
            {
                this.NavigationPropertyName = navigationPropertyName;
                this.NavigationPropertyType = navigationPropertyType;
                this.CheckActive = checkActive;
                this.HasActive = hasActive;
                this.NavigatioinDisplayProperty = navigatioinDisplayProperty;
                this.IsCollection = isCollection;
            }

            public string NavigationPropertyName { get; }
            public string NavigationPropertyType { get; }
            public string NavigatioinDisplayProperty { get; }
            public bool IsCollection { get; }
            public bool CheckActive { get; set; }
            public bool HasActive { get; set; }
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not ClassSyntaxReceiver receiver)
            {
                return;
            }
            var compilation = context.Compilation;

            foreach (var classSymbol in receiver.Candidates)
            {
                foreach (var item in CreateControllerCode(compilation, classSymbol))
                {
                    context.AddSource($"{item.clazzName}.gen.cs", SourceText.From(item.code, Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassSyntaxReceiver());
        }

        private static IEnumerable<(string clazzName, string code)> CreateControllerCode(Compilation compilation, ClassDeclarationSyntax clazz)
        {
            var ret = new List<(string clazzName, string code)>();
            try
            {
                var exposedSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataExposedAttribute");
                var hiddenSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.ODataHiddenAttribute");
                var deactivateSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.DeactivateCheckAttribute");
                var enumerableSymbol = compilation.GetTypeByMetadataName(typeof(IEnumerable).FullName);
                var classSymbol = compilation.GetSemanticModel(clazz.SyntaxTree).GetDeclaredSymbol(clazz);
                var attributeSymbols = classSymbol!.GetAttributesWithInherited().ToArray();
                var auditSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditEntityAttribute");
                var disableSymbol = compilation.GetTypeByMetadataName("ESys.DataAnnotations.AuditDisableAttribute");
                if (classSymbol!.BaseType != null
                    && !classSymbol.IsAbstract
                    && attributeSymbols.Any(a => exposedSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                    && attributeSymbols.All(a => !hiddenSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability)))
                {
                    var keyType = classSymbol.BaseType.Name.StartsWith("BizEntity") ? classSymbol.BaseType.TypeArguments[1].ToDisplayString() : string.Empty;

                    var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

                    var checkPropertyNames = attributeSymbols
                        .Where(attr => deactivateSymbol!.Equals(attr.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                        .ToDictionary(a => a.ConstructorArguments.First().Value as string,
                                      a => a.ConstructorArguments.Skip(1).First().Value as string);

                    var relationMeta = clazz.Members.OfType<PropertyDeclarationSyntax>()
                        .Select(p => compilation.GetSemanticModel(p.SyntaxTree).GetDeclaredSymbol(p))
                        .Where(p => p!.IsVirtual)
                        .Select(p =>
                        {
                            var isCollection = p!.Type.AllInterfaces.Any(t => t!.Equals(enumerableSymbol, SymbolEqualityComparer.IncludeNullability));
                            var activeCandidateType = isCollection ? ((INamedTypeSymbol)p!.Type).TypeArguments[0] : p!.Type;
                            var hasActive = activeCandidateType.AllInterfaces.Any(t => t!.Name == "IActiveEntity");
                            var checkActive = checkPropertyNames.TryGetValue(p!.Name, out var displayName);
                            return new NavigationPropertyMeta(p.Name, activeCandidateType.ToDisplayString(), displayName ?? string.Empty, hasActive, checkActive, isCollection);
                        })
                        .ToArray();

                    var exposedAttribute = attributeSymbols.Where(a => exposedSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability)).ToArray();
                    var allowCreate = !string.IsNullOrEmpty(keyType) && exposedAttribute.Select(a => a.ConstructorArguments.First()).All(par => true.Equals(par.Value));
                    var allowDelete = !string.IsNullOrEmpty(keyType) && exposedAttribute.Select(a => a.ConstructorArguments.Skip(1).First()).All(par => true.Equals(par.Value));
                    var allowEdit = !string.IsNullOrEmpty(keyType) && exposedAttribute.Select(a => a.ConstructorArguments.Skip(2).First()).All(par => true.Equals(par.Value));
                    var deactiveOnly = allowDelete && classSymbol.AllInterfaces.Any(i => i.Name == "IActiveEntity");
                    var filterHidden = classSymbol.AllInterfaces.Any(i => i.Name == "IHiddenEntity");
                    var code = GetControllerCode(namespaceName, classSymbol.Name, keyType, filterHidden, allowCreate, allowDelete, allowEdit, deactiveOnly, relationMeta);
                    ret.Add(new($"{classSymbol.Name}Controller", code));


                    var needAudit = attributeSymbols.Any(a => auditSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability))
                      && attributeSymbols.All(a => !disableSymbol!.Equals(a.AttributeClass, SymbolEqualityComparer.IncludeNullability));
                    if (needAudit)
                    {
                        var auditCode = GetControllerCode(namespaceName, $"{classSymbol.Name}Audit", "long", filterHidden, false, false, false, false, Enumerable.Empty<NavigationPropertyMeta>());
                        ret.Add(new($"{classSymbol.Name}AuditController", auditCode));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return ret;
        }

        private static string GetControllerCode(
            string namespaceName,
            string entityName,
            string entityKeyType,
            bool filterHidden,
            bool allowCreate,
            bool allowDelete,
            bool allowEdit,
            bool deactiveOnly,
            IEnumerable<NavigationPropertyMeta> propertyMetas)
        {
            var allowSingle = !string.IsNullOrEmpty(entityKeyType);
            var needWrite = allowCreate || allowDelete || allowEdit;
            var controllerNS = namespaceName.Split('.').ToArray();
            controllerNS[controllerNS.Length - 1] = "ODataControllers";
            var controllerNsStr = string.Join(".", controllerNS);
            var entityFull = $"{namespaceName}.{entityName}";
            var lowerEntityName = entityName.Substring(0, 1).ToLower() + entityName.Substring(1);
            var sb = new StringBuilder();
            var hasNavigationProperty = true.Equals(propertyMetas?.Any());
            var hasActiveNavigationProperty = true.Equals(propertyMetas?.Any(p => p.HasActive));
            var listPredicate = filterHidden ? $@".Where(entity=>!entity.IsHidden)" : string.Empty;
            if (allowSingle)
            {
                sb.AppendLine(@$"

        /// <summary>
        /// 根据Id查询{entityName}
        /// </summary>
        /// <param name=""key""></param>
        /// <returns></returns>
        [EnableQuery(MaxExpansionDepth = 6, MaxAnyAllExpressionDepth = 6)]
// #if DEBUG
//         [Microsoft.AspNetCore.Authorization.AllowAnonymous]
// #endif
        public IActionResult Get({entityKeyType} key)
        {{
            var filtered = this.readableRepository.Where({(filterHidden ? "entity => entity.Id == key && !entity.IsHidden" : "entity => entity.Id == key")});
            return this.Ok(SingleResult.Create(filtered));
        }}");
            }

            var delete = @$"this.writableRepository.DeleteNow(key);
            return new NoContentResult();";

            if (allowEdit || allowDelete)
            {
                var checkMethodSb = new StringBuilder($@"
        /// <summary>
        /// 检测{entityName}是否能禁用
        /// </summary>
        /// <param name=""key"">{entityName}主键</param>
        /// <param name=""names"">待检查的属性名数组</param>{(deactiveOnly? "\r\n        /// <param name=\"modelIsActive\">模型是否启用</param>":string.Empty)}
        /// <param name=""{lowerEntityName}"">实体对象</param>
        /// <param name=""against"">无法禁用原因</param>
        /// <returns>实体对象是否存在</returns>
        private bool CheckDeactiveInvalided({entityKeyType} key, string[] names,{(deactiveOnly?" bool modelIsActive,":string.Empty)} out {entityFull} {lowerEntityName}, out DeactiveInvalidation[] against)
        {{
");
                checkMethodSb.AppendLine($@"            var list = new List<DeactiveInvalidation>();");
                checkMethodSb.AppendLine($@"            var ret = false;");
                checkMethodSb.Append($@"            IQueryable<{entityFull}> query = this.readableRepository.AsQueryable(false)");
                foreach (var item in propertyMetas!.Where(p => p.CheckActive))
                {
                    checkMethodSb.Append($"\r\n                .Include(entity => entity.{item.NavigationPropertyName})");
                };
                checkMethodSb.Append(";\r\n");
                if (hasNavigationProperty)
                {
                    checkMethodSb.AppendLine($@"            foreach (var property in navigationProperties.Where(property => names.Contains(property)))");
                    checkMethodSb.AppendLine($@"            {{");
                    checkMethodSb.AppendLine($@"                query = query.Include(property);");
                    checkMethodSb.AppendLine($@"            }}");
                }
                checkMethodSb.AppendLine("#pragma warning disable CS8601");
                checkMethodSb.AppendLine($@"            {lowerEntityName} = query.FirstOrDefault(entity => entity.Id == key{(filterHidden ? " && !entity.IsHidden" : string.Empty)});");
                checkMethodSb.AppendLine("#pragma warning restore CS8601");
                checkMethodSb.AppendLine($@"            if ({lowerEntityName} != null)");
                checkMethodSb.AppendLine($@"            {{");
                checkMethodSb.AppendLine($@"                ret = true;");
                if (deactiveOnly)
                {
                    checkMethodSb.AppendLine($@"                if ( !names.Contains(nameof(IActiveEntity.IsActive)) || modelIsActive || !{lowerEntityName}.IsActive)");
                    checkMethodSb.AppendLine($@"                {{");
                    checkMethodSb.AppendLine($@"                    against = Array.Empty<DeactiveInvalidation>();");
                    checkMethodSb.AppendLine($@"                    return ret;");
                    checkMethodSb.AppendLine($@"                }}");
                }
                foreach (var item in propertyMetas.Where(p => p.CheckActive && p.IsCollection))
                {
                    checkMethodSb.AppendLine($@"                if ({lowerEntityName}.{item.NavigationPropertyName}.Any(n => n.IsActive))");
                    checkMethodSb.AppendLine($@"                {{");
                    checkMethodSb.AppendLine($@"                    list.Add(DeactiveInvalidation.Generate(");
                    checkMethodSb.AppendLine($@"                        n => n.{item.NavigatioinDisplayProperty},");
                    checkMethodSb.AppendLine($@"                        {lowerEntityName}.{item.NavigationPropertyName}.Where(n => n.IsActive)");
                    checkMethodSb.AppendLine($@"                    ));");
                    checkMethodSb.AppendLine($@"                }}");
                }
                foreach (var item in propertyMetas.Where(p => p.CheckActive && !p.IsCollection))
                {
                    checkMethodSb.AppendLine($@"                if (true.Equals({lowerEntityName}.{item.NavigationPropertyName}?.IsActive))");
                    checkMethodSb.AppendLine($@"                {{");
                    checkMethodSb.AppendLine($@"                    list.Add(DeactiveInvalidation.Generate(");
                    checkMethodSb.AppendLine($@"                        n => n.{item.NavigatioinDisplayProperty},");
                    checkMethodSb.AppendLine($@"                        new [] {{{lowerEntityName}.{item.NavigationPropertyName}}} ");
                    checkMethodSb.AppendLine($@"                    ));");
                    checkMethodSb.AppendLine($@"                }}");
                }
                checkMethodSb.AppendLine($@"            }}");
                checkMethodSb.AppendLine($@"            against = list.ToArray();");
                checkMethodSb.AppendLine($@"            return ret;");
                checkMethodSb.AppendLine($@"        }}");
                sb.Append(checkMethodSb.ToString());
            }

            if (deactiveOnly && hasNavigationProperty && (allowEdit || allowDelete))
            {
                if (hasNavigationProperty)
                {
                    delete = $@"var check = this.CheckDeactiveInvalided(key, {(deactiveOnly? "activeProperty" : "Array.Empty<string>()")}, {(deactiveOnly ? "true, " : string.Empty)}out var {lowerEntityName}, out var against);
            if (check)
            {{
                if (against.Length == 0)
                {{
                    {lowerEntityName}.IsActive = false;
                    this.writableRepository.UpdateIncludeNow({lowerEntityName}, activeProperty);
                    return new NoContentResult();
                }}
                else
                {{
                    return ResultBuilder.CreateODataError(ErrorCode.Service.DeactiveInvalid, string.Empty, against);
                }}
            }}
            else
            {{
                return ResultBuilder.CreateODataError(ErrorCode.Service.DataNotExist);
            }}";
                }
                else
                {
                    delete = $@"var entity = new {entityFull}()
            {{
                Id = key,
                IsActive = false
            }};
            this.writableRepository.UpdateIncludeNow(entity, activeProperty);
            return new NoContentResult();";
                }
            }
            if (allowDelete)
            {
                var d = $@"var entity = this.readableRepository.AsQueryable(false)";
                sb.AppendLine(@$"
        /// <summary>
        /// 删除{entityName}
        /// </summary>
        /// <param name=""key"">{entityName}主键</param>
        /// <returns></returns>
        public IActionResult Delete([FromODataUri] {entityKeyType} key)
        {{
            {delete}
        }}");
            }
            if (allowCreate)
            {
                sb.AppendLine(@$"
        /// <summary>
        /// 创建{entityName}
        /// </summary>
        /// <param name=""model"">{entityName}实体，必须验证通过</param>
        /// <returns></returns>
        public IActionResult Post([Entity] {entityFull} model)
        {{
            if (!this.ModelState.IsValid)
            {{
                var msg = string.Join("","", this.ModelState.SelectMany(s => s.Value?.Errors ?? Enumerable.Empty<ModelError>())
                                                            .Select(err => string.IsNullOrEmpty(err.ErrorMessage) 
                                                                           ? err.Exception?.Message 
                                                                           : err.ErrorMessage));
                return ResultBuilder.CreateODataError(ErrorCode.Service.InvalidModel, msg);
            }}

            this.BeforePost(model);
            var entry = this.writableRepository.Insert(model);
            this.writableRepository.SaveNow();
            return this.Created(entry.Entity);
        }}
        partial void BeforePost({entityFull} model);");
            }
            if (allowEdit)
            {
                var updateMethod = "            var entry = this.writableRepository.UpdateIncludeNow(model.Entity, names);";
                if (hasNavigationProperty)
                {
                    updateMethod = @$"            var withoutNavigationNames = names.Except(navigationProperties).ToArray();
            var ctx = this.writableRepository.Context;
            var entry = this.writableRepository.UpdateInclude(model.Entity, withoutNavigationNames);
            {string.Join("\r\n            ", propertyMetas.Select(p => $@"if (names.Contains(nameof({entityFull}.{p.NavigationPropertyName})))
            {{
                RelationOperator.{(p.HasActive ? "SynchronizeActivedRelations" : "SynchronizeRelations")}(
                    {(p.IsCollection ? $"exist.{p.NavigationPropertyName}" : $"exist.{p.NavigationPropertyName} == null ? Array.Empty<{p.NavigationPropertyType.TrimEnd('?')}>() : new [] {{ exist.{p.NavigationPropertyName} }}")},
                    {(p.IsCollection ? $"model.Entity.{p.NavigationPropertyName}" : $"model.Entity.{p.NavigationPropertyName} == null ? Array.Empty<{p.NavigationPropertyType.TrimEnd('?')}>() : new [] {{ model.Entity.{p.NavigationPropertyName} }}")},
                    ctx,
                    n => n.Id);
            }}"))}
            this.writableRepository.SaveNow();
            entry.Reload();
            foreach (var navigationProperty in navigationProperties)
            {{
                entry.Navigation(navigationProperty).Load();
            }}
";
                }
                sb.AppendLine(@$"
        /// <summary>
        /// 更新{entityName}
        /// </summary>
        /// <param name=""key"">{entityName}实体主键</param>
        /// <param name=""model"">包含{entityName}实体及属性名称的模型</param>
        /// <returns></returns>
        public IActionResult Patch([FromODataUri] {entityKeyType} key, [EntityWithNames] ({entityFull} Entity, string[] Names) model)
        {{
            var names = model.Names.Except(keyProperties).ToArray();
            if(model.Entity.Id == 0)
            {{
                model.Entity.Id = key;
            }}");
                sb.AppendLine($@"
            if(this.CheckDeactiveInvalided(model.Entity.Id, names, {(deactiveOnly ? "model.Entity.IsActive, " : string.Empty)}out var exist, out var against)) // 实体存在
            {{
                if (against.Length > 0) // 有冲突
                {{
                    return ResultBuilder.CreateODataError(ErrorCode.Service.DeactiveInvalid, string.Empty, against);
                }}
            }}
            else // 实体不存在
            {{
                return ResultBuilder.CreateODataError(ErrorCode.Service.DataNotExist);
            }}
            this.BeforePatch(model.Entity, names);
{updateMethod}
            return this.Updated(entry.Entity);");
                sb.AppendLine($@"        }}");
                sb.AppendLine(@$"        partial void BeforePatch({entityFull} model, string[] names);");
            }

            var ret = $@"/*
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
namespace {controllerNsStr}
{{
    using ESys.Contract.Db;
    using ESys.Contract.Entity;
    using ESys.Utilty.Defs;
    using ESys.Utilty.Entity;
    using Furion.DatabaseAccessor;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.OData.Formatter;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.AspNetCore.OData.Results;
    using Microsoft.AspNetCore.OData.Routing.Controllers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using global::System.Collections.Generic;
    using global::System;
    using global::System.Linq;

    /// <summary>
    /// {entityName} OData控制器 自动生成，请勿修改
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public partial class {entityName}Controller : ODataController
    {{
        private readonly IReadableRepository<{entityFull}, TenantSlaveLocator> readableRepository;{(needWrite ? $"\r\n        private readonly IRepository<{entityFull}, TenantMasterLocator> writableRepository;" : string.Empty)}
        private readonly ILogger<{entityName}Controller> logger;
        {(allowEdit ? $@"private static readonly string[] keyProperties = new[] {{ nameof({entityFull}.Id) }};" : string.Empty)}{(allowDelete && deactiveOnly ? "\r\n        private static readonly string[] activeProperty = new[] { nameof(IActiveEntity.IsActive) };" : string.Empty)}
        {(hasNavigationProperty ? $@"private static readonly string[] navigationProperties = new[] {{
{string.Join(",\r\n", propertyMetas.Select(p => $"            nameof({entityFull}.{p.NavigationPropertyName})"))}
        }};" : "")}
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name=""logger"">日志</param>
        /// <param name=""repository"">存储</param>
        public {entityName}Controller(
            ILogger<{entityName}Controller> logger,
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> repository
            )
        {{
            this.logger = logger;
            this.readableRepository = repository.Slave1<{entityFull}>();{(needWrite ? $"\r\n            this.writableRepository = repository.Master<{entityFull}>();" : string.Empty)}
        }}

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [EnableQuery(MaxExpansionDepth = 6, MaxAnyAllExpressionDepth = 6)]
// #if DEBUG
//         [Microsoft.AspNetCore.Authorization.AllowAnonymous]
// #endif
        public IQueryable<{entityFull}> Get()
        {{
            return this.readableRepository.AsQueryable(false){listPredicate};
        }}{sb}
    }}
}}";

            return ret;
        }

    }
}
