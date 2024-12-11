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

namespace ESys.Utilty.Mvc
{
    using ESys.Utilty.Attributes;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Linq;

    /// <summary>
    /// controller名称转换
    /// </summary>
    public class ControllerNameConvention : IApplicationModelProvider
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public int Order => 99;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            // Nothing here.
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (var controller in context.Result.Controllers)
            {
                var controllerNameAttribute = controller.Attributes.OfType<ControllerNameAttribute>().SingleOrDefault();
                if (controllerNameAttribute != null)
                {
                    controller.ControllerName = controllerNameAttribute.Name;
                }
            }
        }
    }
}
