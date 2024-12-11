using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.Contract.Service
{
    public interface ITenantDbContextOptionsBuilder
    {
        DbContextOptions<TContext> BuildOptions<TContext>() where TContext : DbContext;
    }
}
