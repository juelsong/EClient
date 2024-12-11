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
using ESys.Security.Entity;
using ESys.Utilty.Defs;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESys.Security.Models;
using Microsoft.AspNetCore.Authorization;

namespace ESys.Security.ApiControllers
{
    /// <summary>
    /// 权限控制器
    /// </summary>
    [ApiController]
    [ODataIgnored]
    [Route("[controller]")]
    public class PermissionController : Controller
    {
        private readonly IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msRepository"></param>
        public PermissionController(
            IMSRepository<TenantMasterLocator, TenantSlaveLocator> msRepository
            )
        {
            this.msRepository = msRepository;
        }
#if DEBUG
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="locale"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet]
        [Route("Generate")]
        public async Task Generate(string locale)
        {
            var pstrs = this.msRepository.Slave1<Permission>().AsQueryable(false)
                  .OrderBy(p => p.Order)
                  .Select(p => new { p.Description, Code = p.Code.Contains(":") ? $"'{p.Code}'" : p.Code })
                  .ToArray()
                  .Select(p => $@"{p.Code}: ""{(locale == "zh-cn" ? p.Description : p.Code)}"",")
                  .ToArray();
            var str = string.Join("\n", pstrs);
            this.Response.ContentType = "text/plain"; //application/json; charset=utf-8 
            await this.Response.BodyWriter.WriteAsync(Encoding.UTF8.GetBytes(str));
        }
#endif
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public PermissionModel[] Get()
        {
            var pRepo = this.msRepository.Slave1<Permission>();
            var models = pRepo.AsQueryable().Select(e => new
            {
                e.Id,
                e.ParentId,
                Model = new PermissionModel()
                {
                    Code = e.Code,
                    Desc = e.Description,
                    Type = e.Type,
                }
            }).ToArray();
            foreach (var model in models)
            {
                if (model.ParentId.HasValue)
                {
                    var parent = models.FirstOrDefault(m => m.Id == model.ParentId.Value);
                    parent?.Model.SubPermissions.Add(model.Model);
                }
            }
            var ret = models.Where(m => !m.ParentId.HasValue).Select(m => m.Model).ToArray();
            return ret;
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
#if DEBUG
        [AllowAnonymous]
#endif
        [HttpPatch]
        public Result<bool> SetPermissions([FromBody] List<PermissionModel> permissions)
        {
            try
            {
                var rolePermissionDic = this.msRepository.Slave1<Role>()
                                            .Include(r => r.Permissions)
                                            .ToDictionary(r => r.Id, r => r.Permissions.Select(p => p.Code).ToArray());
                var rpRepo = this.msRepository.Master<RolePermission>();
                var rpIds = rpRepo.AsQueryable().Select(rp => rp.Id).ToArray();
                foreach (var item in rpIds)
                {
                    rpRepo.Delete(item);
                }
                rpRepo.SaveNow();
                var pRepo = this.msRepository.Master<Permission>();
                var pIds = pRepo.AsQueryable().OrderByDescending(p => p.ParentId).Select(p => p.Id).ToArray();
                foreach (var item in pIds)
                {
                    pRepo.DeleteNow(item);
                }
                pRepo.SaveNow();
                if (permissions != null && permissions.Count > 0)
                {
                    var order = 1;
                    foreach (var permission in permissions)
                    {
                        pRepo.Insert(permission.GetPermission(ref order));
                    }
                    pRepo.SaveNow();
                }
                foreach (var kvp in rolePermissionDic)
                {
                    rpRepo.Insert(pRepo.Where(p => kvp.Value.Contains(p.Code)).Select(p => new RolePermission()
                    {
                        RoleId = kvp.Key,
                        PermissionId = p.Id
                    }));
                }
                pRepo.SaveNow();
                return ResultBuilder.Ok(true);
            }
            catch (Exception ex)
            {
                return ResultBuilder.Error<bool>(ErrorCode.Service.InnerError, ex.Message);
            }
        }
    }
}
