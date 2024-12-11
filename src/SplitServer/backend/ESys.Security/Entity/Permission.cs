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
                Description = "添加洁净级别",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Classification:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑洁净级别",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Classification:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用洁净级别",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Classification:Disable"
            };
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
                Description = "测试方法",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "testMethod"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加测试方法",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "TestMethod:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑测试方法",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "TestMethod:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用测试方法",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "TestMethod:Disable"
            };
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "添加检测数据",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "Measurement:Add"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "编辑检测数据",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "Measurement:Edit"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "删除检测数据",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "Measurement:Delete"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "添加测试方法设备",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeEquipment:Add"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "编辑测试方法设备",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeEquipment:Edit"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "删除测试方法设备",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeEquipment:Delete"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "添加测试方法培养基",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeMedia:Add"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "编辑测试方法培养基",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeMedia:Edit"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "删除测试方法培养基",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "TestTypeMedia:Delete"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "添加测试方法限度",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "LimitDef:Add"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "编辑测试方法限度",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "LimitDef:Edit"
            //};
            //yield return new Permission()
            //{
            //    Id = ++id,
            //    Description = "删除测试方法限度",
            //    Type = PermissionType.Action,
            //    Order = id,
            //    ParentId = levelIds[1],
            //    Code = "LimitDef:Delete"
            //};

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

            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加培养基",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Media:Add"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑培养基",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Media:Edit"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用培养基",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Media:Disable"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "微生物",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "microorganism"
            };


            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "添加微生物",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Organism:Add"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑微生物",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Organism:Edit"
            };

            yield return new Permission()
            {
                Id = ++id,
                Description = "禁用微生物",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Organism:Disable"
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
                Description = "审计追踪",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "auditRecord"
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
                Description = "检验执行",
                Type = PermissionType.Menu,
                Order = id,
                Code = "inspectionExecution"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "missions"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理领取",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Receive"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理分配",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Assign"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理复制",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Copy"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理退回",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Return"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理执行",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Execute"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务管理无需测试",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:NoTest"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务日历",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Calender"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "任务条码打印",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[1],
                Code = "Missions:Printer"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "结果录入",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "inspectionRecord"
            };
            levelIds[1] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "采样",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:Sampling"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "孵化",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:Incubation"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "测试",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:Testing"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "录入",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:ResultEntry"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "编辑设备导入数据",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:EditDeviceImport"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "结果录入无需测试",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[1],
                Code = "InspectionRecord:Notest"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "审核批准",
                Type = PermissionType.Menu,
                Order = id,
                Code = "auditPrompt"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "审核批准再测试",
                Type = PermissionType.Action,
                ParentId = levelIds[0],
                Order = id,
                Code = "AuditPrompt:ReTest"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "审核批准无需测试",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "AuditPrompt:Notest"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "批准",
                Type = PermissionType.Action,
                ParentId = levelIds[0],
                Order = id,
                Code = "AuditPrompt:Approve"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "审核",
                Type = PermissionType.Action,
                ParentId = levelIds[0],
                Order = id,
                Code = "AuditPrompt:Review"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "复核修改数据",
                Type = PermissionType.Action,
                ParentId = levelIds[0],
                Order = id,
                Code = "AuditPrompt:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "检验计划",
                Type = PermissionType.Menu,
                Order = id,
                Code = "plan"
            };
            levelIds[0] = id;
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划添加",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Add"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划编辑",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Edit"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划批准",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Approve"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划激活",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Effective"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划废弃",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Retire"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划添加到任务列表",
                Type = PermissionType.Action,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:AddToPool"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "计划日历",
                Type = PermissionType.Menu,
                Order = id,
                ParentId = levelIds[0],
                Code = "Plan:Calender"
            };
            yield return new Permission()
            {
                Id = ++id,
                Description = "分析报表",
                Type = PermissionType.Menu,
                Order = id,
                Code = "analyse"
            };
        }
    }
}
