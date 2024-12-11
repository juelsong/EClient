using EHost.Contract.Service;
using EHost.Infrastructure.Entity.Environment;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace EWebServer.Db
{
    public class EnvironmentDbContext : DbContext
    {
        public DbSet<EnvironmentalSensor> EnvironmentalSensor { get; set; }
        public EnvironmentDbContext(DbContextOptions<EnvironmentDbContext> options)
            : base(options)
        {

        }
        // 如果您需要在OnConfiguring中设置连接字符串，可以使用以下代码
        // 但通常建议在服务注册时设置DbContextOptions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 为所有实体添加租户ID过滤条件
        }
    }
}
