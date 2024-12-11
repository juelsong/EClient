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

namespace ESys.Notification.Service.EMailBuilders
{
    using ESys.Utilty;
    using Furion.ViewEngine;
    using System.Globalization;
    using System.Resources;

    /// <summary>
    /// email构建器基类
    /// </summary>
    public abstract class EMailBuilderBase
    {
        private readonly ResourceManager resourceManager;
        /// <summary>
        /// 渲染引擎
        /// </summary>
        internal protected readonly IViewEngine viewEngine;
        /// <summary>
        /// 
        /// </summary>
        public EMailBuilderBase()
        {
            this.resourceManager = ResourceManagerFactory.GetResourceManager("Resources.Resource");
            this.viewEngine = Furion.App.GetService<IViewEngine>();
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected string GetString(string key, CultureInfo culture)
        {
            return this.resourceManager.GetString(key, culture);
        }

        /// <summary>
        /// 渲染引擎加载程序集
        /// </summary>
        /// <param name="builder"></param>
        protected static void LoadAllAssembly(IViewEngineOptionsBuilder builder)
        {
            foreach (var item in Furion.App.Assemblies)
            {
                builder.AddAssemblyReference(item);
            }
        }
    }

}
