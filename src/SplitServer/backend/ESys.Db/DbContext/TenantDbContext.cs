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


using ESys.Contract.Db;
using ESys.Contract.Defs;
using ESys.Contract.Service;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Tenant = ESys.Contract.Service.Tenant;

namespace ESys.Db.DbContext
{
#if MIGRATION
    using ESys.Contract.Entity;
    using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;
    using Microsoft.EntityFrameworkCore.Migrations.Internal;
    using Microsoft.EntityFrameworkCore.Migrations.Operations;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.EntityFrameworkCore.Update;
    using Microsoft.EntityFrameworkCore.Update.Internal;
    using System.Collections.Generic;
    using System.Text;
#pragma warning disable EF1001 // Internal EF Core API usage.

    internal static class IModelExtensions
    {
        public static IEnumerable<IEntityType> GetEntityTypesWithoutView(this IModel model)
        {
            return model.GetEntityTypes().Where(t => (t is IConventionEntityType ct) && ct.FindAnnotation(RelationalAnnotationNames.ViewName) == null);
        }
    }


    internal abstract class TriggerMigrationsModelDiffer : MigrationsModelDiffer
    {
        public TriggerMigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            : base(
                  typeMappingSource,
                  migrationsAnnotations,
                  changeDetector,
                  updateAdapterFactory,
                  commandBatchPreparerDependencies)
        { }

        public override IReadOnlyList<MigrationOperation> GetDifferences(
            IRelationalModel source,
            IRelationalModel target)
        {
            bool hasAuditTable(IEntityType entityType, IRelationalModel model)
                => model.Model
                    .GetEntityTypesWithoutView()
                    .Any(e => e
                            .GetTableName()
                            .Equals($"{entityType.GetTableName()}Audit",
                                    StringComparison.InvariantCultureIgnoreCase));



            var baseDiff = base.GetDifferences(source, target);
            if (baseDiff.Count > 0)
            {
                var operations = new List<MigrationOperation>();
                var diffColumnTable = baseDiff.OfType<AddColumnOperation>()
                    .Select(c => c.Table)
                    .Union(baseDiff.OfType<DropColumnOperation>().Select(d => d.Table))
                    .Union(baseDiff.OfType<RenameColumnOperation>().Select(d => d.Table))
                    .ToArray();

                var dropTrigger = baseDiff.OfType<DropTableOperation>().Select(o => o.Name).Union(diffColumnTable).ToArray();
                var createTrigger = baseDiff.OfType<CreateTableOperation>().Select(o => o.Name).Union(diffColumnTable).ToArray();
                // 把加减列的表触发器删除再创建
                // 删除删除表的触发器
                // 添加新增表的触发器
                // 删除 禁用Audit表的触发器
                if (source?.Model != null)
                {
                    operations.AddRange(
                        source.Model.GetEntityTypesWithoutView()
                            .Where(entityType => dropTrigger.Contains(entityType.GetTableName()) &&
                                                                      (hasAuditTable(entityType, source) || entityType.Name.EndsWith("Audit")))
                            .Select(entityType =>
                            {
                                var name = entityType.Name.Split('.').Last();
                                if (name.EndsWith("Audit"))
                                {
                                    name = name[..^5];
                                }
                                return name;
                            }).Distinct()
                            .SelectMany(name =>
                            {
                                return new[]
                                {
                                    new SqlOperation() { Sql = this.GetDropTriggerSql(GetInsertTriggerName(name)) },
                                    new SqlOperation() { Sql = this.GetDropTriggerSql(GetUpdateTriggerName(name)) },
                                    new SqlOperation() { Sql = this.GetDropTriggerSql(GetDeleteTriggerName(name)) }
                                };
                            }));
                }

                operations.AddRange(baseDiff);

                if (target?.Model != null)
                {
                    operations.AddRange(
                        target.Model.GetEntityTypesWithoutView()
                            .Where(entityType => createTrigger.Contains(entityType.GetTableName()) && (hasAuditTable(entityType, target) || entityType.Name.EndsWith("Audit")))
                            .Select(entityType =>
                            {
                                var name = entityType.Name.Split('.').Last();
                                if (name.EndsWith("Audit"))
                                {
                                    name = name[..^5];
                                    entityType = target.Model.GetEntityTypesWithoutView().First(e => e.GetTableName().Equals(name, StringComparison.InvariantCultureIgnoreCase));
                                }
                                return entityType;
                            }).GroupBy(entityType => entityType.Name)
                            .SelectMany(g =>
                            {
                                var entityType = g.First();
                                return new[]
                                {
                                    new SqlOperation() { Sql = this.GenerateInsertTrigger(entityType) },
                                    new SqlOperation() { Sql = this.GenerateUpdateTrigger(entityType) },
                                    new SqlOperation() { Sql = this.GenerateDeleteTrigger(entityType) }
                                };
                            }));

                }
                return operations;
            }
            else
            {
                return baseDiff;
            }
        }

        protected static string GetTriggerName(string entityName, string action) => $"audit_after_{entityName.ToLower()}_{action}";

        protected static string GetInsertTriggerName(string entityName) => GetTriggerName(entityName, "insert");
        protected static string GetDeleteTriggerName(string entityName) => GetTriggerName(entityName, "delete");
        protected static string GetUpdateTriggerName(string entityName) => GetTriggerName(entityName, "update");

        protected abstract string GetDropTriggerSql(string triggerName);

        protected abstract string GenerateInsertTrigger(IEntityType entityType);
        protected abstract string GenerateDeleteTrigger(IEntityType entityType);
        protected abstract string GenerateUpdateTrigger(IEntityType entityType);
    }
    internal class SqlServerTriggerMigrationsModelDiffer : TriggerMigrationsModelDiffer
    {
        public SqlServerTriggerMigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            : base(
                  typeMappingSource,
                  migrationsAnnotations,
                  changeDetector,
                  updateAdapterFactory,
                  commandBatchPreparerDependencies)
        { }

        protected override string GetDropTriggerSql(string triggerName) =>
            $@"IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID('[dbo].[{triggerName}]'))
        DROP trigger [dbo].[{triggerName}]
        GO";



        protected override string GenerateInsertTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var sb = new StringBuilder($@"CREATE TRIGGER {GetInsertTriggerName(name)} ON [dbo].[{name}]
            FOR INSERT AS INSERT INTO {name}Audit
            (
                EntityId,
                AuditTime,
                Action");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    )");
            sb.AppendLine("    SELECT");
            sb.AppendLine("        Id,");
            sb.AppendLine("        GETDATE(),");
            sb.Append($"        {AuditAction.Insert:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    FROM Inserted");
            return sb.ToString();
        }

        protected override string GenerateDeleteTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var sb = new StringBuilder($@"CREATE TRIGGER {GetDeleteTriggerName(name)} ON [dbo].[{name}]
            FOR DELETE AS INSERT INTO {name}Audit
            (
                EntityId,
                AuditTime,
                Action");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    )");
            sb.AppendLine("    SELECT");
            sb.AppendLine("        Id,");
            sb.AppendLine("        GETDATE(),");
            sb.Append($"        {AuditAction.Delete:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    FROM Deleted");
            return sb.ToString();
        }

        protected override string GenerateUpdateTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var sb = new StringBuilder($@"CREATE TRIGGER {GetUpdateTriggerName(name)} ON [dbo].[{name}]
            FOR UPDATE AS INSERT INTO {name}Audit
            (
                EntityId,
                AuditTime,
                Action");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    )");
            sb.AppendLine("    SELECT");
            sb.AppendLine("        Id,");
            sb.AppendLine("        GETDATE(),");
            sb.Append($"        {AuditAction.Update:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n        [{prop.Name}]");
            }
            sb.AppendLine("\r\n    FROM Inserted");
            return sb.ToString();
        }
    }
    internal class MySqlTriggerMigrationsModelDiffer : TriggerMigrationsModelDiffer
    {
        public MySqlTriggerMigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            : base(
                  typeMappingSource,
                  migrationsAnnotations,
                  changeDetector,
                  updateAdapterFactory,
                  commandBatchPreparerDependencies)
        { }
        protected override string GetDropTriggerSql(string triggerName) => $"drop trigger if exists `{triggerName}`;";



        protected override string GenerateInsertTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var empty = new char[26 + name.Length];
            Array.Fill(empty, ' ');
            var emptyStr = new string(empty);
            var sb = new StringBuilder($@"CREATE TRIGGER {GetInsertTriggerName(name)} AFTER INSERT ON `{name}`
    FOR EACH ROW
    BEGIN
        INSERT INTO `{name}Audit` SET EntityId = NEW.Id,
{emptyStr}AuditTime = UTC_TIMESTAMP(),
{emptyStr}Action = {AuditAction.Insert:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n{emptyStr}`{prop.Name}` = NEW.{prop.Name}");
            }
            sb.AppendLine("        ;");
            sb.AppendLine("    END;");

            return sb.ToString();
        }

        protected override string GenerateDeleteTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var empty = new char[26 + name.Length];
            Array.Fill(empty, ' ');
            var emptyStr = new string(empty);
            var sb = new StringBuilder($@"CREATE TRIGGER {GetDeleteTriggerName(name)} AFTER DELETE ON `{name}`
    FOR EACH ROW
    BEGIN
        INSERT INTO `{name}Audit` SET EntityId = OLD.Id,
{emptyStr}AuditTime = UTC_TIMESTAMP(),
{emptyStr}Action = {AuditAction.Delete:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n{emptyStr}`{prop.Name}` = OLD.{prop.Name}");
            }
            sb.AppendLine("        ;");
            sb.AppendLine("    END;");

            return sb.ToString();
        }

        protected override string GenerateUpdateTrigger(IEntityType entityType)
        {
            var name = entityType.Name.Split('.').Last();
            var empty = new char[26 + name.Length];
            Array.Fill(empty, ' ');
            var emptyStr = new string(empty);
            var sb = new StringBuilder($@"CREATE TRIGGER {GetUpdateTriggerName(name)} AFTER UPDATE ON `{name}`
    FOR EACH ROW
    BEGIN
        INSERT INTO `{name}Audit` SET EntityId = NEW.Id,
{emptyStr}AuditTime = UTC_TIMESTAMP(),
{emptyStr}Action = {AuditAction.Update:d}");
            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
            {
                sb.Append($",\r\n{emptyStr}`{prop.Name}` = NEW.{prop.Name}");
            }
            sb.AppendLine("        ;");
            sb.AppendLine("    END;");

            return sb.ToString();
        }
    }
//        internal class PostgreSQLTriggerMigrationsModelDiffer : TriggerMigrationsModelDiffer
//    {
//        public PostgreSQLTriggerMigrationsModelDiffer(
//            IRelationalTypeMappingSource typeMappingSource,
//            IMigrationsAnnotationProvider migrationsAnnotations,
//            IChangeDetector changeDetector,
//            IUpdateAdapterFactory updateAdapterFactory,
//            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
//            : base(
//                  typeMappingSource,
//                  migrationsAnnotations,
//                  changeDetector,
//                  updateAdapterFactory,
//                  commandBatchPreparerDependencies)
//        { }

//        protected override string GetDropTriggerSql(string triggerName) =>
//            $@"DROP TRIGGER IF EXISTS {triggerName} ON ALL;

//CREATE OR REPLACE FUNCTION {triggerName}()
//RETURNS TRIGGER AS $$
//BEGIN
//    -- Your trigger logic here
//    RETURN NEW;
//END;
//$$ LANGUAGE plpgsql;

//CREATE TRIGGER {triggerName}
//BEFORE INSERT OR UPDATE OR DELETE ON {tableName}
//FOR EACH ROW EXECUTE FUNCTION {triggerName}();";

//        protected override string GenerateInsertTrigger(IEntityType entityType)
//        {
//            var name = entityType.Name.Split('.').Last();
//            var triggerName = GetInsertTriggerName(name);
//            var auditTableName = $"{name}Audit";
//            var sb = new StringBuilder($@"CREATE OR REPLACE FUNCTION {triggerName}()
//RETURNS TRIGGER AS $$
//BEGIN
//    INSERT INTO {auditTableName} (EntityId, AuditTime, Action");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", [{prop.Name}]");
//            }
//            sb.AppendLine(")")
//            .AppendLine("VALUES (NEW.Id, NOW(), 'I'");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", NEW.{prop.Name}");
//            }
//            sb.AppendLine(@");
//    RETURN NEW;
//END;
//$$ LANGUAGE plpgsql;

//CREATE TRIGGER {triggerName}
//BEFORE INSERT ON {name}
//FOR EACH ROW EXECUTE FUNCTION {triggerName}();");

//            return sb.ToString();
//        }

//        protected override string GenerateDeleteTrigger(IEntityType entityType)
//        {
//            var name = entityType.Name.Split('.').Last();
//            var triggerName = GetDeleteTriggerName(name);
//            var auditTableName = $"{name}Audit";
//            var sb = new StringBuilder($@"CREATE OR REPLACE FUNCTION {triggerName}()
//RETURNS TRIGGER AS $$
//BEGIN
//    INSERT INTO {auditTableName} (EntityId, AuditTime, Action");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", [{prop.Name}]");
//            }
//            sb.AppendLine(")")
//            .AppendLine("VALUES (OLD.Id, NOW(), 'D'");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", OLD.{prop.Name}");
//            }
//            sb.AppendLine(@");
//    RETURN OLD;
//END;
//$$ LANGUAGE plpgsql;

//CREATE TRIGGER {triggerName}
//BEFORE DELETE ON {name}
//FOR EACH ROW EXECUTE FUNCTION {triggerName}();");

//            return sb.ToString();
//        }

//        protected override string GenerateUpdateTrigger(IEntityType entityType)
//        {
//            var name = entityType.Name.Split('.').Last();
//            var triggerName = GetUpdateTriggerName(name);
//            var auditTableName = $"{name}Audit";
//            var sb = new StringBuilder($@"CREATE OR REPLACE FUNCTION {triggerName}()
//RETURNS TRIGGER AS $$
//BEGIN
//    INSERT INTO {auditTableName} (EntityId, AuditTime, Action");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", [{prop.Name}]");
//            }
//            sb.AppendLine(")")
//            .AppendLine("VALUES (NEW.Id, NOW(), 'U'");
//            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
//            {
//                sb.Append($", NEW.{prop.Name}");
//            }
//            sb.AppendLine(@");
//    RETURN NEW;
//END;
//$$ LANGUAGE plpgsql;

//CREATE TRIGGER {triggerName}
//BEFORE UPDATE ON {name}
//FOR EACH ROW EXECUTE FUNCTION {triggerName}();");

//            return sb.ToString();
//        }
//    }

    internal class SQLiteMigrationsModelDiffer : MigrationsModelDiffer
    {
        public SQLiteMigrationsModelDiffer(
            IRelationalTypeMappingSource typeMappingSource,
            IMigrationsAnnotationProvider migrationsAnnotations,
            IChangeDetector changeDetector,
            IUpdateAdapterFactory updateAdapterFactory,
            CommandBatchPreparerDependencies commandBatchPreparerDependencies)
            : base(
                  typeMappingSource,
                  migrationsAnnotations,
                  changeDetector,
                  updateAdapterFactory,
                  commandBatchPreparerDependencies)
        { }

        //        protected override string GetDropTriggerSql(string triggerName) => $"drop trigger if exists `{triggerName}`;";

        //        protected override string GenerateInsertTrigger(IEntityType entityType)
        //        {
        //            var name = entityType.Name.Split('.').Last();
        //            var empty = new char[26 + name.Length];
        //            Array.Fill(empty, ' ');
        //            var emptyStr = new string(empty);
        //            var sb = new StringBuilder($@"CREATE TRIGGER {GetInsertTriggerName(name)} AFTER INSERT ON `{name}`
        //    BEGIN
        //        INSERT INTO `{name}Audit` (EntityId,
        //{emptyStr}AuditTime,
        //{emptyStr}Action");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\n{emptyStr}`{prop.Name}`");
        //            }
        //            sb.AppendLine(") VALUES (");
        //            sb.AppendLine("NEW.Id,");
        //            sb.AppendLine("datetime('now'),");
        //            sb.AppendLine($"{AuditAction.Insert:d}");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\nNEW.`{prop.Name}`");
        //            }
        //            sb.AppendLine(");");
        //            sb.AppendLine("    END;");

        //            return sb.ToString();
        //        }

        //        protected override string GenerateDeleteTrigger(IEntityType entityType)
        //        {
        //            var name = entityType.Name.Split('.').Last();
        //            var empty = new char[26 + name.Length];
        //            Array.Fill(empty, ' ');
        //            var emptyStr = new string(empty);
        //            var sb = new StringBuilder($@"CREATE TRIGGER {GetDeleteTriggerName(name)} AFTER DELETE ON `{name}`
        //    BEGIN
        //        INSERT INTO `{name}Audit` (EntityId,
        //{emptyStr}AuditTime,
        //{emptyStr}Action");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\n{emptyStr}`{prop.Name}`");
        //            }
        //            sb.AppendLine(") VALUES (");
        //            sb.AppendLine("OLD.Id,");
        //            sb.AppendLine("datetime('now'),");
        //            sb.AppendLine($"{AuditAction.Delete:d}");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\nOLD.`{prop.Name}`");
        //            }
        //            sb.AppendLine(");");
        //            sb.AppendLine("    END;");

        //            return sb.ToString();
        //        }

        //        protected override string GenerateUpdateTrigger(IEntityType entityType)
        //        {
        //            var name = entityType.Name.Split('.').Last();
        //            var empty = new char[26 + name.Length];
        //            Array.Fill(empty, ' ');
        //            var emptyStr = new string(empty);
        //            var sb = new StringBuilder($@"CREATE TRIGGER {GetUpdateTriggerName(name)} AFTER UPDATE ON `{name}`
        //    BEGIN
        //        INSERT INTO `{name}Audit` (EntityId,
        //{emptyStr}AuditTime,
        //{emptyStr}Action");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\n{emptyStr}`{prop.Name}`");
        //            }
        //            sb.AppendLine(") VALUES (");
        //            sb.AppendLine("NEW.Id,");
        //            sb.AppendLine("datetime('now'),");
        //            sb.AppendLine($"{AuditAction.Update:d}");
        //            foreach (var prop in entityType.GetProperties().Where(p => !p.IsKey()))
        //            {
        //                sb.Append($",\r\nNEW.`{prop.Name}`");
        //            }
        //            sb.AppendLine(");");
        //            sb.AppendLine("    END;");

        //            return sb.ToString();
        //        }

        public override IReadOnlyList<MigrationOperation> GetDifferences(IRelationalModel source, IRelationalModel target)
        {
            var list = new List<MigrationOperation>(base.GetDifferences(source, target));
            list.RemoveAll(item => item is InsertDataOperation
                                || item is DeleteDataOperation
                                || item is UpdateDataOperation
                                );
            return list;
        }
    }
#pragma warning restore EF1001 // Internal EF Core API usage.
#endif
    public abstract class TenantDbContext<C, T> : AppDbContext<C, T>
        where T : class, IDbContextLocator
        where C : TenantDbContext<C, T>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IInterceptor[] interceptors;

        public TenantDbContext(
            IServiceProvider sp,
            DbContextOptions<C> options) : base(options)
        {
            this.serviceProvider = sp;
            var provider = sp.GetService<IInterceptorProvider>();
            this.interceptors = provider == null
                ? Array.Empty<IInterceptor>()
                : provider.GetInterceptors<C, T>().ToArray();
        }

        protected abstract string GetConnectionStr(Tenant tenant);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var tenantService = this.serviceProvider.GetService<ITenantService>();
            var tenant = tenantService.GetCurrentTenant();
            var connectionStr = this.GetConnectionStr(tenant);
#if MIGRATION
            Console.WriteLine($"{tenant?.DbType}\t{connectionStr}");
#endif
            //this.Type = tenant?.DbType ?? throw new SystemException("tenantId not exist");
            switch (tenant?.DbType)
            {
                case DbType.MySQL:
                    optionsBuilder.UseMySql(
                        connectionStr,
                        ServerVersion.AutoDetect(connectionStr),
                        op => op.MigrationsAssembly("ESys.Db.MySQL"))
#if MIGRATION
                        .ReplaceService<IMigrationsModelDiffer, MySqlTriggerMigrationsModelDiffer>()
#endif
                        ;
                    break;
                case DbType.SQLServer:
#if !UNSUPPORT_SQLSERVER2008
                    // Sql Server 2008 分页支持
                    optionsBuilder.ReplaceService<Microsoft.EntityFrameworkCore.Query.IQueryTranslationPostprocessorFactory,
                        Microsoft.EntityFrameworkCore.Query.SqlServer2008QueryTranslationPostprocessorFactory>();
#endif
                    optionsBuilder.UseSqlServer(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.SqlServer"))
#if MIGRATION
                        .ReplaceService<IMigrationsModelDiffer, SqlServerTriggerMigrationsModelDiffer>()
#endif
                        ;

                    break;


                case DbType.PostgreSQL:
                    optionsBuilder.UseNpgsql(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.PostgreSQL"))
#if MIGRATION
                        //.ReplaceService<IMigrationsModelDiffer, PostgreSQLTriggerMigrationsModelDiffer >()
#endif
                        ;

                    break;
                case DbType.SQLite:
                    optionsBuilder.UseSqlite(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.SQLite"))
#if MIGRATION
                        .ReplaceService<IMigrationsModelDiffer, SQLiteMigrationsModelDiffer>()
#endif
                      ;
                    break;
                default:
                    throw new NotImplementedException();
            }
            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();


            foreach (var interceptor in this.interceptors)
            {
                optionsBuilder.AddInterceptors(interceptor);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            var tenantService = this.serviceProvider.GetService<ITenantService>();
            var tenant = tenantService.GetCurrentTenant();
            var dbType = tenant.DbType;
            if (dbType == DbType.SQLite)
            {
                configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetToBinaryConverter>();
                configurationBuilder.Properties<DateTimeOffset?>().HaveConversion<DateTimeOffsetToBinaryConverter>();
                configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeToBinaryConverter>();
                configurationBuilder.Properties<DateTime?>().HaveConversion<DateTimeToBinaryConverter>();
            }
        }
    }

}
