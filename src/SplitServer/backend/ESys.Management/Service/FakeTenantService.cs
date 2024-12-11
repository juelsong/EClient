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
namespace ESys.Management.Service
{
    using ESys.Contract.Service;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal class FakeTenantService : ITenantService
    {
        private readonly Tenant tenant;
        private readonly Func<string, IServiceProvider> providerFunc;
        public FakeTenantService(Tenant tenant, Func<string, IServiceProvider> providerFunc)
        {
            this.tenant = tenant;
            this.providerFunc = providerFunc;
        }
        public Task ExecuteInTenantScope(Func<IServiceScope, Task> action, bool validOnly = true)
        {
            throw new NotImplementedException();
        }

        public Tenant GetCurrentTenant()
        {
            return this.tenant;
        }

        public IServiceProvider GetTenantServiceProvider()
        {
            return this.providerFunc(this.tenant.Code);
        }

        public void SetTenantScope(string tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
