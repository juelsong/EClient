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
using ESys.Security.Entity;
using ESys.Utilty.Defs;
using ESys.Utilty.Security;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ESys.Security.Service
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public class PermissionManager : IPermissionManager, ITransient
    {
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;
        //private readonly IDistributedCache distributedCache;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msRepository"></param>
        public PermissionManager(IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository)
        {
            this.msRepository = msRepository;
        }
        /// <summary>
        /// 检查用户名密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <param name="realName"></param>
        /// <returns></returns>
        public int CheckUser(string account, string password, out int? userId, out string realName)
        {
            userId = null;
            realName = null;
            var user = this.msRepository.Slave1<User>()
                .AsQueryable()
                .Select(u => new { u.Id, u.Account, u.Password, u.Salt, u.RealName, u.IsActive })
                .FirstOrDefault(u => u.Account == account);
            if (user == null)
            {
                return ErrorCode.User.NotExist;
            }
            var pass = PasswordHasher.HashPassword(password, user.Salt);
            if (user.Password != pass)
            {
                return ErrorCode.User.WrongPassword;
            }
            if (!user.IsActive)
            {
                return ErrorCode.User.Forbidden;
            }
            userId = user.Id;
            realName = user.RealName;
            return ErrorCode.NoError;
        }

        // TODO 目前没有缓存机制，是否需要memorycache或者redis缓存
        /// <summary>
        /// 获取是否具有全部权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限</param>
        /// <returns></returns>
        public bool HasAllPermission(int userId, params string[] permissions)
        {
            var user = this.msRepository.Slave1<User>()
                .Include(u => u.Roles, false)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    IsSuper = u.IsHidden,
                    Permissions = u.Roles.SelectMany(r => r.Permissions).Select(p => p.Code).ToArray()
                })
                .FirstOrDefault();
            return user.IsSuper || user.Permissions.Distinct().Count(c => permissions.Contains(c)) == permissions.Length;
        }
        /// <summary>
        /// 是否具有任一权限
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限</param>
        /// <returns></returns>
        public bool HasAnyPermission(int userId, params string[] permissions)
        {
            var user = this.msRepository.Slave1<User>()
                .Include(u => u.Roles, false)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    IsSuper = u.IsHidden,
                    Permissions = u.Roles.SelectMany(r => r.Permissions).Select(p => p.Code).ToArray()
                })
                .FirstOrDefault();
            return user.IsSuper || user.Permissions.Any(c => permissions.Contains(c));
        }
        /// <summary>
        /// 获取用户位置面包屑
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public string GetUserBreadcrumb(int userId)
        {
            return this.msRepository.Slave1<User>()
                                    .AsQueryable(false)
                                    .Where(u => u.Id == userId)
                                    .Select(u => u.Location.LocationExtra.Breadcrumb)
                                    .FirstOrDefault();
        }
    }
}
