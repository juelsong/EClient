namespace ESys.Security.Service
{
    using ESys.Contract.Db;
    using ESys.Contract.Service;
    using ESys.Security.Entity;
    using Furion.DatabaseAccessor;
    using Furion.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 日志服务
    /// </summary>
    public class LogService : ILogService, ITransient
    {
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> repo;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repo"></param>
        public LogService(IMSRepository<TenantMasterLocator, TenantSlaveLocator> repo)
        {
            this.repo = repo;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logInfo"></param>
        /// <exception cref="NotImplementedException"></exception>
        public bool LogData(LogInfo logInfo)
        {
            var user = this.repo.Slave1<User>().FirstOrDefault(u => u.Id == logInfo.UserId);
            if (null!=user)
            {
                var type = this.repo.Master<Log>().InsertNow(new Log()
                {
                    Name = logInfo.TypeName,
                    Description= logInfo.Description,
                    UserName = user.Account,
                    CreatedTime= DateTimeOffset.UtcNow,
                    CreateBy = user.Id,
                });
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
