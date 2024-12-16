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

using ESys.DataAnnotations;
using ESys.Contract.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ESys.Security.Entity
{
    /// <summary>
    /// 权限类型
    /// </summary>
    [ODataConfig]
    public enum PermissionType
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 1,
        /// <summary>
        /// 操作
        /// </summary>
        Action
    }
    /// <summary>
    /// 权限实体
    /// </summary>
    [AuditDisable]
    public partial class Permission : BizEntity<Permission, int>
    {
        /// <summary>
        /// 备注，由于多语言需求，页面根据code显示
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 根据部门生成多个菜单
        /// </summary>
        public string DepartFormatter { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public PermissionType Type { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父权限
        /// </summary>
        public Permission Parent { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        /// <summary>
        /// 下级权限
        /// </summary>
        public virtual ICollection<Permission> SubPermissions { get; set; } = new List<Permission>();

        /// <summary>
        /// 角色映射
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<Permission> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(e => e.Code);
            entityBuilder
                .HasOne(e => e.Parent)
                .WithMany(e => e.SubPermissions)
                .HasForeignKey(e => e.ParentId);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        protected override IEnumerable<Permission> HasDataCore(DbContext dbContext, Type dbContextLocator)
        {

            //dashboard: "仪表板",
            //system: "系统管理",
            //region: "区域",
            //production: "产品",
            //testMethod: "测试方法",
            //device: "设备",
            //medium: "培养基",
            //microorganism: "微生物",
            //security: "安全",
            //department: "部门管理",
            //user: "用户管理",
            //role: "角色管理",
            //booking: "预订管理",
            //auditRecord: "审计追踪",
            //log: "日志",
            //visualization: "可视化",
            //map: "地图管理",
            //visualizations: "可视化呈现",
            //inspectionPlan: "检验计划",
            //inspectionExecution: "检验执行",
            //missions: "任务管理",
            //inspectionRecord: "结果录入",
            //audioPrompt: "审核批准",
            //analyse: "分析报表"
            //plan: "计划"
            var id = 0;
            var levelIds = new int[4];
            Array.Fill(levelIds, 0);
            yield return new Permission()
            {
                Id = ++id,
                Description = "系统管理",
                Type = PermissionType.Menu,
                Order = id,
                Code = "system"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "区域",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "region"
            };

            levelIds[1] = id;

            yield return new Permission()
            {
                Id = ++id,
                Description = "添加采样点类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "SiteType:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑采样点类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "SiteType:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用采样点类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "SiteType:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加区域类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "LocationType:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑区域类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "LocationType:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用区域类型",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "LocationType:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加区域",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Location:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑区域",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Location:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用区域",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Location:Disable"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "产品",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "production"
            };

            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加产品",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Product:Add"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑产品",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Product:Edit"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用产品",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Product:Disable"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "设备",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "device"
            };

            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加设备",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Equipment:Add"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑设备",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Equipment:Edit"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用设备",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Equipment:Disable"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "上传文件",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Equipment:UpdateConfig"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "培养基",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "medium"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "安全",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "security"
            };
            levelIds[1] = id;

            yield return new Permission()
            {
                Id = ++id,
                Description = "部门管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "department"
            };
            levelIds[2] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加部门",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Department:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑部门",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Department:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用部门",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Department:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "用户管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "user"
            };
            levelIds[2] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加用户",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "User:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑用户",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "User:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用用户",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "User:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "修改密码",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "User:Password"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "角色管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "role"
            };
            levelIds[2] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加角色",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Role:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑角色",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Role:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用角色",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Role:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "预订管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "booking"
            };
            levelIds[2] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑警告订阅",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Subscription:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用警告订阅",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[2],
                Code = "Subscription:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "系统设置",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "settings"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "配置密码",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "Security:Password"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "配置邮箱",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "Security:Email"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "日志",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "log"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "可视化",
                Type = PermissionType.Menu,
                Order = id,
                Code = "visualization"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "地图管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "map"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加地图",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Map:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用地图",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Map:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑地图",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Map:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加地图分类",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "MapCategory:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用地图分类",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "MapCategory:Disable"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑地图分类",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "MapCategory:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "可视化呈现",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "visualizations"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "设备维护",
                Type = PermissionType.Menu,
                Order = id,
                Code = "equipment"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "设备维护",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "equipmentTPM"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加维护单",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "equipmentTPM:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "设备维护单列表",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "equipmentTPMList"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "导出维护单",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "equipmentTPM:Export"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑维护单",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "equipmentTPM:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用维护单",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "equipmentTPM:Disable"
            };
        }
    }
}
