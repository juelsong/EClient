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

using System.Threading.Tasks;

namespace ESys.Contract.Service
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tenant"></param>
        /// <param name="configKey"></param>
        /// <returns></returns>
        Task<T> GetConfig<T>(string tenant, string configKey);
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tenant"></param>
        /// <param name="configKey"></param>
        /// <param name="value"></param>
        Task SetConfig<T>(string tenant, string configKey, T value);
    }
}
