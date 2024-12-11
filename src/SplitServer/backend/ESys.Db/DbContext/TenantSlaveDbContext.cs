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
using ESys.Contract.Service;
using Microsoft.EntityFrameworkCore;
using System;

namespace ESys.Db.DbContext
{
    public class TenantSlaveDbContext : TenantDbContext<TenantSlaveDbContext,TenantSlaveLocator>
    {
        public TenantSlaveDbContext(
            IServiceProvider sp,
            DbContextOptions<TenantSlaveDbContext> options) : base(sp, options)
        {
//#if DEBUG
            this.Database.Migrate();
//#endif
        }
        protected override string GetConnectionStr(Tenant tenant) => tenant.SlaveDbConnStr;
    }
}

