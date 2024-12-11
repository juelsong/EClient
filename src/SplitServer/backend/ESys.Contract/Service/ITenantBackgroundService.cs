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

namespace ESys.Contract.Service
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// 租户后台服务配置
    /// </summary>
    public interface ITenantBackgroundService
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="services"></param>
        /// <param name="startBackground"></param>
        /// <returns></returns>
        IServiceCollection Configure(Tenant tenant, IServiceCollection services, bool startBackground);
        /// <summary>
        /// host启动回调
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="serviceProvider"></param>
        void OnHostStart(Tenant tenant,IServiceProvider serviceProvider);
        /// <summary>
        /// host停止回调
        /// </summary>
        /// <param name="tenant"></param>
        /// <param name="serviceProvider"></param>
        void OnHostStop(Tenant tenant,IServiceProvider serviceProvider);
    }
}
