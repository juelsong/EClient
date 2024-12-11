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

namespace ESys.Security.Controllers
{
    using ESys.Contract.Service;
    using ESys.Security.Models;
    using ESys.Utilty.Attributes;
    using ESys.Utilty.Defs;
    using ESys.Utilty.Entity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Formatter;
    using Microsoft.AspNetCore.OData.Routing.Attributes;
    /// <summary>
    /// 电子签名
    /// </summary>
    [ODataRouteComponent("OData")]
    [ControllerName("ODataOperationImport")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class OfflineController : ControllerBase
    {
        /// <summary>
        /// 签名数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result<bool> ESignData([FromODataBody] OfflineModel model)
        {
            return ResultBuilder.Ok(true);
        }
    }

    /// <summary>
    /// OfflineController OData配置
    /// </summary>
    public class OfflineControllerODataConfig : ControllerODataConfigBase<OfflineController>
    { }
}
