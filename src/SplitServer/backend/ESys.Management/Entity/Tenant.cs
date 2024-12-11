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
using ESys.Contract.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ESys.Security.Entity
{
    /// <summary>
    /// 租户
    /// </summary>
    public class Tenant : MasterEntity<Tenant, int>
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 限期
        /// </summary>
        public DateTime Limits { get; set; }
        /// <summary>
        /// 主数据库连接
        /// </summary>
        public string MasterDbConnStr { get; set; }
        /// <summary>
        /// 从数据库连接
        /// </summary>
        public string SlaveDbConnStr { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DbType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<Tenant> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {
            yield return new Tenant()
            {
                Id = 1,
                Code = "Test",
                Name = "Test",
                Limits = DateTime.UtcNow.AddYears(100),
                MasterDbConnStr = "server=127.0.0.1;database=modus;user id=root;password=123456;",
                SlaveDbConnStr = "server=127.0.0.1;database=modus;user id=root;password=123456;",
                DbType = DbType.MySQL
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Tenant> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(lt => lt.Name);
            entityBuilder.Property(e => e.Limits)
                .HasColumnType("timestamp without time zone")
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
