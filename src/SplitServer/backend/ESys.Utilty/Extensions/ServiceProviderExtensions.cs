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

namespace ESys.Utilty.Extensions
{
    using ESys.Contract.Db;
    using Furion.DatabaseAccessor;
    using Microsoft.Extensions.DependencyInjection;
    using IMasterEntity = Furion.DatabaseAccessor.IEntity<Furion.DatabaseAccessor.MasterDbContextLocator>;

#pragma warning disable 1591
    public static class ServiceProviderExtensions
    {
        public static IPrivateReadableRepository<T> GetReadableRepository<T>(this System.IServiceProvider serviceProvider) where T : class, IPrivateEntity, new()
        {
            if (typeof(IMasterEntity).IsAssignableFrom(typeof(T)))
            {
                return serviceProvider
                     .GetRequiredService<IRepository<T>>();
            }
            else
            {
                return serviceProvider
                     .GetRequiredService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>()
                     .Slave1<T>();
            }
        }

        public static IPrivateRepository<T> GetWritableRepository<T>(this System.IServiceProvider serviceProvider) where T : class, IPrivateEntity, new()
        {
            if (typeof(IMasterEntity).IsAssignableFrom(typeof(T)))
            {
                return serviceProvider.GetRequiredService<IRepository<T>>();
            }
            else
            {
                return serviceProvider
                    .GetRequiredService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>()
                    .Master<T>();
            }
        }

        public static IDeletableRepository<T> GetDeletableRepository<T>(this System.IServiceProvider serviceProvider) where T : class, IPrivateEntity, new()
        {
            if (typeof(IMasterEntity).IsAssignableFrom(typeof(T)))
            {
                return serviceProvider
                     .GetRequiredService<IRepository<T>>();
            }
            else
            {
                return serviceProvider
                     .GetRequiredService<IMSRepository<TenantMasterLocator, TenantSlaveLocator>>()
                     .Master<T>();
            }
        }
    }
#pragma warning restore 1591
}
