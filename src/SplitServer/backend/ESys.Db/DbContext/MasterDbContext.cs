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

using ESys.Contract.Defs;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace ESys.Db.DbContext
{
    public class MasterDbContext : AppDbContext<MasterDbContext>
    {
        private readonly IConfigurationSection configurationSection;
        public MasterDbContext(DbContextOptions<MasterDbContext> config, IConfiguration configuration) : base(config)
        {
            this.configurationSection = configuration.GetSection("Db");
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var typeStr = this.configurationSection.GetValue<string>("type");
            var connectionStr = this.configurationSection.GetValue<string>("connectionStr");
            var type = Enum.Parse<DbType>(typeStr);
            switch (type)
            {
                case DbType.MySQL:
                    optionsBuilder.UseMySql(
                        connectionStr,
                        ServerVersion.AutoDetect(connectionStr),
                        op => op.MigrationsAssembly("ESys.Db.MySQL"));
                    break;
                case DbType.SQLServer:
                    optionsBuilder.UseSqlServer(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.SqlServer"));
                    break;
                case DbType.PostgreSQL:
                    optionsBuilder.UseNpgsql(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.PostgreSQL"));
                    break;
                case DbType.SQLite:
                    optionsBuilder.UseSqlite(
                        connectionStr,
                        op => op.MigrationsAssembly("ESys.Db.SQLite"));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
