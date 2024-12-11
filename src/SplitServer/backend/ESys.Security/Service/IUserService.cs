/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Security.Service
{
    using ESys.Security.Entity;
    using ESys.Security.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<(int ErrorCode, LoginResult Result, bool IsSuper)> Login(LoginParams input);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<(int ErrorCode, UserInfo Result)> GetUserInfo(int userId);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> AddUser(User user);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<(int ErrorCode, string Msg)> PatchPassword(User user);
        /// <summary>
        /// 修改自己密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(int ErrorCode, string Msg)> PatchSelfPassword(int userId, ChangePassSelf model);
    }
    /// <summary>
    /// 用户管理模式
    /// </summary>
    public enum UserManagementMode
    {
        /// <summary>
        /// ESys
        /// </summary>
        ESys = 0,
        /// <summary>
        /// LDAP
        /// </summary>
        LDAP = 1,
    }
    /// <summary>
    /// 用户服务工厂
    /// </summary>
    public interface IUserServiceFactory
    {
        /// <summary>
        /// 获取用户服务
        /// </summary>
        /// <returns></returns>
        IUserService GetUserService();
    }
}
