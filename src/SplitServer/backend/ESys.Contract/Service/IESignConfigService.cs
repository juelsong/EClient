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
    using System.Threading.Tasks;

    /// <summary>
    /// 电子签名配置
    /// </summary>
    /// <param name="SignCount">签名次数</param>
    /// <param name="Permissions">签名权限</param>
    public record ESignConfig(int SignCount, string[] Permissions);

    /// <summary>
    /// 获取、设置电子签名需要的权限
    /// </summary>
    public interface IESignConfigService
    {
        /// <summary>
        /// 获取给特定业务签名所需的权限
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<ESignConfig> GetPermissions(string tenant, string category);

        /// <summary>
        /// 设置给特定业务签名所需的权限
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="category"></param>
        /// <param name="singCount"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        Task SetPermissions(string tenant, string category, int singCount, params string[] permissions);
    }
}
