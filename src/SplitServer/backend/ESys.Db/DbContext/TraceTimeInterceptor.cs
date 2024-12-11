namespace ESys.Db.DbContext
{
    using ESys.Contract.Entity;
    using ESys.Contract.Service;
    using ESys.Utilty.Defs;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TraceTimeInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            SetExtraInfo(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            SetExtraInfo(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void SetExtraInfo(DbContext dbContext)
        {
            var userId = Furion.App.GetService<IDataProvider>().TryGetCurrentUserId(out var tryUserId)
                ? tryUserId
                : ConstDefs.SystemUserId;
            var dbType = Furion.App.GetService<ITenantService>().GetCurrentTenant().DbType;

            foreach (var entry in dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added))
            {
                if (entry.Entity is ITimedEntity timedEntity)
                {
                    timedEntity.CreatedTime = timedEntity.CreatedTime == default
                        ? dbType == Contract.Defs.DbType.PostgreSQL ? DateTime.UtcNow : DateTimeOffset.Now
                        : (DateTimeOffset)timedEntity.CreatedTime.UtcDateTime;
                }
                if (entry.Entity is ITraceableEntity traceableEntity && traceableEntity.CreateBy == default)
                {
                    traceableEntity.CreateBy = userId;
                }
            }
            foreach (var entry in dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified))
            {

                if (entry.Entity is ITimedEntity timedEntity)
                {
                    timedEntity.UpdatedTime = timedEntity.UpdatedTime == default
                        ? dbType == Contract.Defs.DbType.PostgreSQL ? DateTime.UtcNow : DateTimeOffset.Now
                        : (DateTimeOffset)timedEntity.UpdatedTime?.UtcDateTime;
                }
                if (entry.Entity is ITraceableEntity traceableEntity)
                {
                    traceableEntity.UpdateBy = userId;
                }
            }
        }
    }
}
