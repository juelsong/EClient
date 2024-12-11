using EHost.Contract.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Db.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class TenantDbContextOptionsBuilder : ITenantDbContextOptionsBuilder
    {
        private readonly ITenant _tenant;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tenant"></param>
        public TenantDbContextOptionsBuilder(ITenant tenant)
        {
            _tenant = tenant;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public DbContextOptions<TContext> BuildOptions<TContext>() where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseNpgsql(_tenant.ConnectString);
            // 可以添加其他配置选项
            return optionsBuilder.Options;
        }
    }
}
