namespace EHost.App.Db
{
    using EHost.Contract.Entity;
    using EHost.Infrastructure.Entity;
    using EHost.Infrastructure.Entity.Environment;
    using EHost.Security.Entity;
    using Microsoft.EntityFrameworkCore;
    public class EServerDbContext : DbContext
    {
        private readonly string connectStr;

        public EServerDbContext(string connectStr)
        {
            this.connectStr = connectStr;
        }
        public DbSet<User> User { get; set; }

        public DbSet<MonitorDataMinute> EnvironmentalSensorMinute { get; set; }
        public DbSet<MonitorDataDaily> EnvironmentalSensorDaily { get; set; }
        public DbSet<MonitorDataQuarter> EnvironmentalSensorQuarter { get; set; }
        public DbSet<MonitorDataHour> EnvironmentalSensorHour { get; set; }


        //public DbSet<FugitiveDust> FugitiveDust { get; set; }
        //public DbSet<MonitorData> MonitorData { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectStr);
                //optionsBuilder.UseLazyLoadingProxies(true);
                //optionsBuilder.UseNpgsql("Host=118.31.237.242;Port=5432;Database=aiqi_dust_s7;Username=aqhj_admin;Password=aqhj#dust_s7");
            }
        }

    }


}
