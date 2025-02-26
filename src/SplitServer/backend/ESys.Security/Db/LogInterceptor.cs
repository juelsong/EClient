///*
// *        ┏┓   ┏┓+ +
// *       ┏┛┻━━━┛┻┓ + +
// *       ┃       ┃
// *       ┃   ━   ┃ ++ + + +
// *       ████━████ ┃+
// *       ┃       ┃ +
// *       ┃   ┻   ┃
// *       ┃       ┃ + +
// *       ┗━┓   ┏━┛
// *         ┃   ┃
// *         ┃   ┃ + + + +
// *         ┃   ┃    Code is far away from bug with the animal protecting
// *         ┃   ┃ +     神兽保佑,代码无bug
// *         ┃   ┃
// *         ┃   ┃  +
// *         ┃    ┗━━━┓ + +
// *         ┃        ┣┓
// *         ┃        ┏┛
// *         ┗┓┓┏━┳┓┏┛ + + + +
// *          ┃┫┫ ┃┫┫
// *          ┗┻┛ ┗┻┛+ + + +
// */
//namespace ESys.Security.Db
//{
//    using ESys.Contract.Db;
//    using ESys.Contract.Service;
//    using ESys.Security.Entity;
//    using ESys.Utilty.Defs;
//    using Furion.DatabaseAccessor;
//    using Microsoft.AspNetCore.Http;
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.ChangeTracking;
//    using Microsoft.EntityFrameworkCore.Diagnostics;
//    using Microsoft.Extensions.DependencyInjection;
//    using Microsoft.Extensions.Logging;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Numerics;
//    using System.Text;
//    using System.Threading;
//    using System.Threading.Tasks;

//    /// <summary>
//    /// 数据修改日志拦截器
//    /// </summary>
//    public class LogInterceptor : SaveChangesInterceptor
//    {
//        private readonly ILogger<LogInterceptor> _logger;
//        private readonly IServiceProvider _serviceProvider;
//        private EntityEntry[] _modifiedEntries = null;
//        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;

//        /// <summary>
//        /// 构造
//        /// </summary>
//        /// <param name="serviceProvider"></param>
//        /// <param name="logger"></param>
//        public LogInterceptor(
//            IServiceProvider serviceProvider,
//            ILogger<LogInterceptor> logger,
//            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository)

//        {
//            this._serviceProvider = serviceProvider;
//            this._logger = logger;
//            this.msRepository = msRepository;

//        }

//        /// <summary>
//        /// 保存
//        /// </summary>
//        /// <param name="eventData"></param>
//        /// <param name="result"></param>
//        /// <returns></returns>
//        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//        {
//            this._modifiedEntries = GetModifiedEntries(eventData.Context);
//            return base.SavingChanges(eventData, result);
//        }

//        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//        {
//            this._modifiedEntries = GetModifiedEntries(eventData.Context);
//            return base.SavingChangesAsync(eventData, result, cancellationToken);
//        }

//        private static EntityEntry[] GetModifiedEntries(DbContext dbContext)
//        {
//            return dbContext.ChangeTracker.Entries()
//                .Where(e => e.State == EntityState.Modified)
//                .ToArray();
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="eventData"></param>
//        /// <param name="result"></param>
//        /// <returns></returns>
//        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
//        {
//            this.SaveLogIfNeeded(eventData.Context);
//            return base.SavedChanges(eventData, result);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="eventData"></param>
//        /// <param name="result"></param>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
//        {
//            this.SaveLogIfNeeded(eventData.Context);
//            return base.SavedChangesAsync(eventData, result, cancellationToken);
//        }

//        private void SaveLogIfNeeded(DbContext dbContext)
//        {
//            try
//            {
//                if (this._modifiedEntries == null || !this._modifiedEntries.Any())
//                {
//                    return;
//                }

//                var logs = new List<Log>();
//                var userName = this.GetCurrentUserName();

//                foreach (var entry in this._modifiedEntries)
//                {
//                    var modifiedProperties = entry.Properties
//                        .Where(p => p.IsModified && !p.Metadata.IsPrimaryKey())
//                        .ToList();

//                    if (!modifiedProperties.Any())
//                    {
//                        continue;
//                    }

//                    var entityType = entry.Entity.GetType();
//                    var tableName = entityType.Name;

//                    var description = new StringBuilder();
//                    description.Append($"修改了{tableName}表的以下字段：");

//                    foreach (var property in modifiedProperties)
//                    {
//                        var propertyName = property.Metadata.Name;
//                        var originalValue = property.OriginalValue;
//                        var currentValue = property.CurrentValue;

//                        description.Append($"\n{propertyName}: {originalValue} -> {currentValue}");
//                    }

//                    var log = new Log
//                    {
//                        UserName = userName,
//                        Name = "修改",
//                        Description = description.ToString(),
//                        // 如果Log类有其他必要字段，在这里设置
//                    };

//                    logs.Add(log);
//                }

//                if (logs.Any())
//                {
//                    dbContext.Set<Log>().AddRange(logs);
//                    dbContext.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                this._logger.LogError(ex, "SaveLogIfNeeded error");
//            }
//            finally
//            {
//                this._modifiedEntries = null;
//            }
//        }

//        private string GetCurrentUserName()
//        {
//            try
//            {
//                // 从HttpContext或其他地方获取当前用户名
//                //var httpContext = this._serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
//                //return httpContext?.User?.Identity?.Name ?? "Unknown";

//                var userId = Furion.App.GetService<IDataProvider>().TryGetCurrentUserId(out var tryUserId)
//                    ? tryUserId: ConstDefs.SystemUserId;
//                var user = this.msRepository.Master<User>().FirstOrDefault(i => i.Id == userId);

//                return user.Account;
//            }
//            catch (Exception ex)
//            {
//                this._logger.LogError(ex, "GetCurrentUserName error");
//                return "Unknown";
//            }
//        }
//    }
//}
