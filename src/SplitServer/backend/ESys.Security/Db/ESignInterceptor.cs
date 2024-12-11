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
namespace ESys.Security.Db
{
    using ESys.Contract.Entity;
    using ESys.Contract.Feature;
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 电子签名拦截器
    /// </summary>
    public class ESignInterceptor : SaveChangesInterceptor
    {
        private volatile bool esignSaved = false;
        private readonly ILogger<ESignInterceptor> logger;
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>

        public ESignInterceptor(IServiceProvider serviceProvider,
            ILogger<ESignInterceptor> logger)
        {
            // IActionContextAccessor IHttpContextAccessor 貌似是客户端odata 使用
            //this.httpContext = httpContextAccessor.HttpContext ?? actionContextAccessor.ActionContext?.HttpContext;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        private long[] esignIds;
        private static readonly MethodInfo getLastAuditByEntityMethod = typeof(ESignInterceptor).GetMethod(nameof(GetLastAuditByEntity), BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo getLastAuditMethod = typeof(ESignInterceptor).GetMethod(nameof(GetLastAudit), BindingFlags.NonPublic | BindingFlags.Static);

        private EntityEntry[] savingEntries = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (!this.esignSaved)
            {
                this.savingEntries = GetSavingEntries(eventData.Context);
            }
            return base.SavingChanges(eventData, result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (!this.esignSaved)
            {
                this.savingEntries = GetSavingEntries(eventData.Context);
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private static EntityEntry[] GetSavingEntries(DbContext dbContext)
        {
            return dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached
                         && e.State != EntityState.Unchanged
                         && e.Entity.GetType() != typeof(ElectronicSignature)
                         && e.Entity.GetType() != typeof(ElectronicSignatureItem))
                .ToArray();
        }


        /// <summary>
        /// 已保存
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            this.SetESignIfNeed(eventData.Context);
            return base.SavedChanges(eventData, result);
        }
        /// <summary>
        /// 已保存 异步
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            this.SetESignIfNeed(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void SetESignIfNeed(DbContext dbContext)
        {
            try
            {
                // esignSaved 避免 DbContext SavedChanges嵌套
                IESignFeature esignFeature = null;
                var dataProvider = this.serviceProvider.GetService<IDataProvider>();
                if (!this.esignSaved &&
                    (dataProvider.TryGetESignData(out var eSignArray)
                            || this.TryGetESignFeature(out esignFeature)))
                {
                    var data = esignFeature?.ESignDatas ?? eSignArray;
                    if (data == null)
                    {
                        // TODO 是否需要日志吗？
                        return;
                    }
                    this.esignSaved = true;
                    // 此时审计实体已经插入了
                    var items = new List<ElectronicSignatureItem>();

                    foreach (var group in this.savingEntries
                        .GroupBy(e => e.Entity.GetType()))
                    {
                        var entityType = group.Key;
                        if (true.Equals(entityType.BaseType?.IsGenericType)
                            && entityType.BaseType.GetGenericTypeDefinition() == typeof(BizEntity<,>))
                        {
                            var keyType = entityType.BaseType.GetGenericArguments()[1];
                            var method = getLastAuditMethod.MakeGenericMethod(entityType, keyType);
                            var electronicSignatureItems = (ElectronicSignatureItem[])method.Invoke(null, new object[] { dbContext, group });
                            items.AddRange(electronicSignatureItems);
                        }
                        else
                        {
                            throw new Exception($"{entityType.Name} not inherited from BizEntity");
                        }
                    }
                    if (this.esignIds == null)
                    {
                        var esignEntity = data.Select(eSign => new ElectronicSignature()
                        {
                            Account = eSign.Account,
                            RealName = eSign.RealName,
                            UserId = eSign.ESignedBy,
                            SignDate = eSign.Timestamp,
                            IpAddress = eSign.IpAddress,
                            Category = eSign.Category,
                            Comment = eSign.Comment,
                            CreateBy = eSign.UserId,
                            Order = eSign.Order,
                            CreatedTime = DateTimeOffset.Now,
                            ElectronicSignatureItems = new List<ElectronicSignatureItem>(items.Select(item => new ElectronicSignatureItem()
                            {
                                TableName = item.TableName,
                                PrimaryKey = item.PrimaryKey,
                                LastAuditKey = item.LastAuditKey
                            }))
                        }).ToArray();
                        dbContext.Set<ElectronicSignature>().AddRange(esignEntity);
                        dbContext.SaveChanges();
                        this.esignIds = esignEntity.Select(e => e.Id).ToArray();
                    }
                    else
                    {
                        dbContext.Set<ElectronicSignatureItem>().AddRange(items.SelectMany(item => this.esignIds.Select(id => new ElectronicSignatureItem()
                        {
                            TableName = item.TableName,
                            PrimaryKey = item.PrimaryKey,
                            LastAuditKey = item.LastAuditKey,
                            ElectronicSignatureId = id
                        })));
                        dbContext.SaveChanges();
                    }
                    this.esignSaved = false;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "SetESignIfNeed error");
            }
        }

        private bool TryGetESignFeature(out IESignFeature eSignFeature)
        {
            eSignFeature = null;
            try
            {
                var httpContext = this.serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext
                                  ?? this.serviceProvider.GetService<IActionContextAccessor>()?.ActionContext?.HttpContext;
                httpContext?.Features?.Get<IESignFeature>();
                return true;
            }
            catch (ObjectDisposedException ex)
            {
                this.logger.LogInformation(ex, "TryGetESignFeature disposed");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "TryGetESignFeature error");
            }
            return false;
        }

        private static ElectronicSignatureItem[] GetLastAudit<TEntity, TKey>(DbContext context, IEnumerable<EntityEntry> entityEntries)
            where TEntity : BizEntity<TEntity, TKey>, new()
            where TKey : struct, IConvertible, IEquatable<TKey>
        {
            var auditType = Furion.App.EffectiveTypes.FirstOrDefault(t =>
                true.Equals(t.BaseType?.IsGenericType)
                && t.BaseType.GetGenericTypeDefinition() == typeof(AuditEntity<,,>)
                && t.BaseType.GetGenericArguments()[1] == typeof(TEntity));
            if (auditType == null)
            {
                return Array.Empty<ElectronicSignatureItem>();
            }
            else
            {
                var keyProperty = entityEntries.First().Metadata.FindPrimaryKey().Properties[0].PropertyInfo;
                var entityIds = entityEntries.Select(entry => (TKey)keyProperty.GetValue(entry.Entity)).ToArray();
                var method = getLastAuditByEntityMethod.MakeGenericMethod(auditType, typeof(TEntity), typeof(TKey));
                var entityAuditDic = (IDictionary<TKey, long>)method.Invoke(null, new object[] { context, entityIds });
                return entityIds.Select(entityId => new ElectronicSignatureItem()
                {
                    TableName = typeof(TEntity).Name,
                    PrimaryKey = entityId.ToInt64(null),
                    LastAuditKey = entityAuditDic.TryGetValue(entityId, out var val) ? val : null,
                }).ToArray(); ;
            }
        }

        private static IDictionary<TKey, long> GetLastAuditByEntity<TAudit, TEntity, TKey>(DbContext context, TKey[] entityIds)
            where TAudit : AuditEntity<TAudit, TEntity, TKey>, new()
            where TEntity : BizEntity<TEntity, TKey>, new()
            where TKey : struct, IConvertible, IEquatable<TKey>
        {
            var queryIds = entityIds.ToList();
            return context.Set<TAudit>().AsNoTracking()
                          .Where(t => queryIds.Contains(t.EntityId))
                          .GroupBy(t => t.EntityId)
                          .Select(g => new
                          {
                              g.Key,
                              AuditId = g.OrderByDescending(a => a.AuditTime).Select(a => a.Id).FirstOrDefault()
                          })
                          .ToDictionary(a => a.Key, a => a.AuditId);
        }
    }
}
