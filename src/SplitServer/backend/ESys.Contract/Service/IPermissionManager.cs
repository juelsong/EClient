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
namespace ESys.Contract.Service
{
    /// <summary>
    /// 权限管理器接口
    /// </summary>
    public interface IPermissionManager
    {
        /// <summary>
        /// 检查用户名密码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <param name="realName"></param>
        /// <returns></returns>
        int CheckUser(string account, string password, out int? userId, out string realName);
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool HasAllPermission(int userId, params string[] permissions);

        /// <summary>
        /// 检查至少有一项授权
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        bool HasAnyPermission(int userId, params string[] permissions);
        /// <summary>
        /// 获取用户位置面包屑
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        string GetUserBreadcrumb(int userId);
    }
}
