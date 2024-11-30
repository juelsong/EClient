namespace EHost.App.Db
{
    using EHost.Contract.Entity;
    using Microsoft.EntityFrameworkCore;
    public class EServerDbContext : DbContext
    {
        private readonly string connectStr;

        public EServerDbContext(string connectStr)
        {
            this.connectStr = connectStr;
        }
        public DbSet<EnvironmentalSensor> EnvironmentalSensor { get; set; }

        public DbSet<FugitiveDust> FugitiveDust { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectStr);

                //optionsBuilder.UseNpgsql("Host=118.31.237.242;Port=5432;Database=aiqi_dust_s7;Username=aqhj_admin;Password=aqhj#dust_s7");
            }
        }

    }


}
