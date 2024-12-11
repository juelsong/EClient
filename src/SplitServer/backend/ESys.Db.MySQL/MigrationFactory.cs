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

#if MIGRATION

using ESys.Contract.Db;
using ESys.Contract.Defs;
using ESys.Contract.Service;
using Furion.DatabaseAccessor;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tenant = ESys.Contract.Service.Tenant;

namespace ESys.Db.DbContext
{
    internal class FakeTenantService : ITenantService
    {
        private readonly IConfigurationSection configurationSection;
        public FakeTenantService(IConfiguration configuration)
        {
            this.configurationSection = configuration.GetSection("Db");
        }

        public Tenant GetCurrentTenant()
        {
            var tenantId = "Emis";
            if (this.configurationSection == null)
            {
                throw new NullReferenceException("configurationSection");
            }
            var typeStr = this.configurationSection.GetValue<string>("type");

            if (string.IsNullOrEmpty(typeStr))
            {
                throw new NullReferenceException($"typeStr ======{this.configurationSection}");
            }
            var type = Enum.Parse<DbType>(typeStr);
            return type switch
            {
                DbType.MySQL => this.GetTenant<MySqlConnection, MySqlCommand>(tenantId),
                DbType.SQLServer => this.GetTenant<SqlConnection, SqlCommand>(tenantId),
                DbType.SQLite => this.GetTenant<SqliteConnection, SqliteCommand>(tenantId),
                _ => throw new NotImplementedException(),
            };
        }

        public Task ExecuteInTenantScope(Func<IServiceScope, Task> action, bool validOnly)
        {
            throw new NotImplementedException();
        }

        public void SetTenantScope(string tenantId)
        {
            throw new NotImplementedException();
        }

        private Tenant GetTenant<CONN, CMD>(string tenantId) where CONN : DbConnection, new() where CMD : DbCommand, new()
        {
            var connectionStr = this.configurationSection.GetValue<string>("connectionStr");
            var cmdStr = $"Select Code,Name,MasterDbConnStr,SlaveDbConnStr,DbType from Tenant where Code = '{tenantId}';";
            Tenant ret = null;
            using var conn = new CONN();
            conn.ConnectionString = connectionStr;
            using var cmd = new CMD();
            cmd.Connection = conn;
            cmd.CommandText = cmdStr;
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                ret = new Tenant()
                {
                    Code = reader.GetString(0),
                    Name = reader.GetString(1),
                    MasterDbConnStr = reader.GetString(2),
                    SlaveDbConnStr = reader.GetString(3),
                    DbType = (DbType)reader.GetInt32(4),
                };
            }
            conn.Close();

            return ret;
        }

        public IServiceProvider GetTenantServiceProvider()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TenantDbContextFactory<CTX, TL> : IDesignTimeDbContextFactory<CTX>
        where CTX : TenantDbContext<CTX, TL>
        where TL : class, IDbContextLocator
    {
        private readonly IHost host;
        public TenantDbContextFactory()
        {
            this.host = Host.CreateDefaultBuilder()
            .Inject((_, cfg) => cfg.AutoRegisterBackgroundService = false)
            .ConfigureAppConfiguration((_, configBuilder) =>
            {
#if MIGRATION_SQLSERVER
                var dbType = "SqlServer";
#endif
#if MIGRATION_MYSQL
                var dbType = "MySQL";
#endif
#if MIGRATION_POSTGRESQL
                var dbType = "PostgreSQL";
#endif
#if MIGRATION_SQLITE
                var dbType = "SQLite";
#endif
                configBuilder.AddJsonFile($"{Path.Combine(AppContext.BaseDirectory, $"appsettings.Migration{dbType}.json")}");
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDatabaseAccessor(options =>
                {
                    options
                    .AddDb<TenantMasterDbContext>()
                    .AddDb<TenantSlaveDbContext>()
                    ;
                });
                services.AddSingleton<ITenantService, FakeTenantService>();
            })
            .Build();
        }
        public CTX CreateDbContext(string[] args)
        {
            //var ass = Furion.App.Assemblies.Where(a => a.GetName().Name.Contains("ESys")).ToArray();
            //System.Diagnostics.Debugger.Launch();
            var optionsBuilder = new DbContextOptionsBuilder<CTX>();
            var constructor = typeof(CTX).GetConstructor(new[] { typeof(IServiceProvider), typeof(DbContextOptions<CTX>) });
            return constructor.Invoke(new object[] { this.host.Services, optionsBuilder.Options }) as CTX;
        }
    }

    public class TenantSlaveDbContextFactory : TenantDbContextFactory<TenantSlaveDbContext, TenantSlaveLocator>
    {
    }

    public class TenantMasterDbContextFactory : TenantDbContextFactory<TenantMasterDbContext, TenantMasterLocator>
    {
    }

    public class MasterDbContextFactory : IDesignTimeDbContextFactory<MasterDbContext>
    {
        private readonly IHost host;
        public MasterDbContextFactory()
        {
            this.host = Host.CreateDefaultBuilder()
            .Inject((_, cfg) => cfg.AutoRegisterBackgroundService = false)
            .ConfigureAppConfiguration((_, configBuilder) =>
            {
#if MIGRATION_SQLSERVER
                var dbType = "SqlServer";
#endif
#if MIGRATION_MYSQL
                var dbType = "MySQL";
#endif
#if MIGRATION_POSTGRESQL
                var dbType = "PostgreSQL";
#endif
#if MIGRATION_SQLITE
                var dbType = "SQLite";
#endif
                configBuilder.AddJsonFile($"{Path.Combine(AppContext.BaseDirectory, $"appsettings.Migration{dbType}.json")}");
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDatabaseAccessor(options =>
                {
                    options
                    .AddDb<MasterDbContext>()
                    ;
                });
            })
            .Build();
        }
        public MasterDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MasterDbContext>();
            using var scope = this.host.Services.CreateScope();
            var config = scope.ServiceProvider.GetService<IConfiguration>();
            return new MasterDbContext(optionsBuilder.Options, config);
        }
    }

}
#endif
