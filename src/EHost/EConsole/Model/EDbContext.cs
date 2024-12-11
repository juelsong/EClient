using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EConsole.Model
{
    public class EDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public EDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<Monitor> monitor_min202005 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));

                //optionsBuilder.UseNpgsql("Host=118.31.237.242;Port=5432;Database=aiqi_dust_s7;Username=aqhj_admin;Password=aqhj#dust_s7");
            }

        }
        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder
        //    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFDataSeeding;Trusted_Connection=True;ConnectRetryCount=0")
        //    .UseSeeding((context, _) =>
        //    {
        //        var testBlog = context.Set<Blog>().FirstOrDefault(b => b.Url == "http://test.com");
        //        if (testBlog == null)
        //        {
        //            context.Set<Blog>().Add(new Blog { Url = "http://test.com" });
        //            context.SaveChanges();
        //        }
        //    })
        //    .UseAsyncSeeding(async (context, _, cancellationToken) =>
        //    {
        //        var testBlog = await context.Set<Blog>().FirstOrDefaultAsync(b => b.Url == "http://test.com", cancellationToken);
        //        if (testBlog == null)
        //        {
        //            context.Set<Blog>().Add(new Blog { Url = "http://test.com" });
        //            await context.SaveChangesAsync(cancellationToken);
        //        }
        //    });
    }

    public class Monitor
    {
        public int id { get; set; }
        public int device_of_data { get; set; }
        public int source_of_data { get; set; }
        public int equipment_data_valid_flag { get; set; }
        public double pm2_5_average { get; set; }
        public double pm2_5_max { get; set; }
        public double pm2_5_min { get; set; }
        public double pm10_average { get; set; }
        public double pm10_max { get; set; }
        public double pm10_min { get; set; }
        public double tsp_average { get; set; }
        public double tsp_max { get; set; }
        public double tsp_min { get; set; }
        public double dust_calibration_step { get; set; }
        public double noise_average { get; set; }
        public double noise_max { get; set; }
        public double noise_min { get; set; }
        public double wind_speed_average { get; set; }
        public double wind_speed_max { get; set; }
        public double wind_speed_min { get; set; }
        public double wind_direction_average { get; set; }
        public double wind_direction_max { get; set; }
        public double wind_direction_min { get; set; }
        public double temperature_average { get; set; }
        public double temperature_max { get; set; }
        public double temperature_min { get; set; }
        public double humidity_average { get; set; }
        public double humidity_max { get; set; }
        public double humidity_min { get; set; }
        public DateTimeOffset update_time { get; set; }
        public bool is_valid_data { get; set; }
        public bool is_power_on { get; set; }
        public int pre_max { get; set; }
        public int pre_min { get; set; }
        public int pre_avg { get; set; }
        public int cpm { get; set; }

    }
}
