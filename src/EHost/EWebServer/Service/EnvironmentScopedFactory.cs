using EHost.Contract.Service;
using EWebServer.Db;
using Microsoft.EntityFrameworkCore;

namespace EWebServer.Service
{
    public class EnvironmentScopedFactory : IDbContextFactory<EnvironmentDbContext>
    {
        //private const int DefaultTenantId = -1;

        private readonly IDbContextFactory<EnvironmentDbContext> _pooledFactory;

        public EnvironmentScopedFactory(
            IDbContextFactory<EnvironmentDbContext> pooledFactory,
            ITenant tenant)
        {
            _pooledFactory = pooledFactory;
            //_tenantId = tenant?.Id ?? DefaultTenantId;
        }

        public EnvironmentDbContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();

            return context;
        }
    }
}
