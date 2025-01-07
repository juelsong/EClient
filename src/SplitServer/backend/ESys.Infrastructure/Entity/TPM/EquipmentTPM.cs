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

namespace ESys.Infrastructure.Entity
{
    using ESys.Contract.Entity;
    using ESys.DataAnnotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 产品实体 
    /// 从前端按钮暂存 update
    /// 每次加载 刷新时间
    /// </summary>
    [AuditDisable]

    public partial class EquipmentTPM : BizEntity<EquipmentTPM, int>, ITraceableEntity, ITimedEntity, IActiveEntity
    {
        /// <summary>
        /// 当前步骤 1 2 3
        /// </summary>
        public int Stage { get; set; }
        /// <summary>
        /// 剩余时间 初始值为480 8分钟 
        /// </summary>
        public int RemainSecond { get; set; }
        /// <summary>
        /// 运行维护单位名称
        /// </summary>                          
        [StringLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 当前设备的设备 SerialNumber
        /// </summary>
        public string EquipmentSerialNumber { get; set; }
        /// <summary>
        /// 控制编号 用于 当前设备的粉尘仪编号
        /// </summary>
        public string EquipmentControlNumber { get; set; }
        /// <summary>
        /// 站点名称 来源于设备
        /// </summary>
        public string EquipmentName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 工程报建号
        /// </summary>
        public string ProjectNumber { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 表单一 开始
        /// </summary>
        public DateTimeOffset StartDate { get; set; }
        /// <summary>
        /// 全部 结束
        /// </summary>
        public DateTimeOffset EndDate { get; set; }
        #region 表单一 扬尘在线监测系统运行维护记录表

        /// <summary>
        /// 供电是否正常 默认正常
        /// </summary>
        public bool PowerSupplyIsError { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string PowerSupplyMsg { get; set; }
        /// <summary>
        /// 采样头、链接线路 默认正常
        /// </summary>

        public bool SampleIsNormal { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string SampleMsg { get; set; }
        /// <summary>
        /// 加热除湿温度测温结果 用于填写温度 使用℃
        /// </summary>
        public string TemperatureData { get; set; }
        /// <summary>
        /// 加热除湿温度测温结果 信息
        /// </summary>
        public string TemperatureMsg{ get; set; }
        /// <summary>
        /// 扬尘系统
        /// 检查仪器线路、采样泵、校准系统是否正常
        /// </summary>
        public bool DeviceIsError { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string DeviceMsg { get; set; }
        /// <summary>
        /// 扬尘系统
        /// 检查仪器滤罐是否需更换
        /// </summary>
        public bool ResetEquipment { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string ResetEquipmentMsg { get; set; }
        /// <summary>
        /// 气象参数
        /// 检查温度、湿度、大气压、风速、风向是否正常
        /// </summary>
        public bool EnvironmentIsError { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string EnvironmentMsg { get; set; }

        /// <summary>
        /// 数据采集传输
        /// 数据传输是否正常
        /// </summary>
        public bool DataTransmissionIsError { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string DataTransmissionMsg { get; set; }
        /// <summary>
        /// 视频监控是否正常
        /// </summary>
        public bool VedioIsError { get; set; }
        /// <summary>
        /// 处理方法
        /// </summary>
        public string VedioMsg { get; set; }
        /// <summary>
        /// 设备现场位置
        /// </summary>
        public string DeviceLocation { get; set; }
        #endregion

        #region 表单二
        /// <summary>
        /// 表单二 开始
        /// </summary>
        public DateTimeOffset C1StartDate { get; set; }
        /// <summary>
        /// 表单一 结束
        /// </summary>
        public DateTimeOffset C1EndDate { get; set; }
        /// <summary>
        /// 设定流量
        /// </summary>
        public string SetFlow { get; set; }
        /// <summary>
        /// 实测流量1
        /// </summary>
        public string BeforeFlow1 { get; set; }
        /// <summary>
        /// 实测流量2
        /// </summary>
        public string BeforeFlow2 { get; set; }
        /// <summary>
        /// 实测流量3
        /// </summary>
        public string BeforeFlow3 { get; set; }
        /// <summary>
        /// 相对误差
        /// </summary>
        public string BeforeRelativeError { get; set; }
        /// <summary>
        /// 是否需要调整流量 
        /// 根据 相对误差的结果 
        /// 满足超过10%  则 取值为 “是” 如果 为是 则需要输入校准后的内容
        /// 否的话可以直接提交
        /// </summary>
        public bool BeforeSetFlow { get; set; }
        /// <summary>
        /// 设定流量2
        /// </summary>
        public string SetFlow2 { get; set; }
        /// <summary>
        /// 实测流量1
        /// </summary>
        public string BehindFlow1 { get; set; }
        /// <summary>
        /// 实测流量2
        /// </summary>
        public string BehindFlow2 { get; set; }
        /// <summary>
        /// 实测流量3
        /// </summary>
        public string BehindFlow3 { get; set; }
        /// <summary>
        /// 相对误差
        /// </summary>
        public string BehindRelativeError { get; set; }
        /// <summary>
        /// 是否需要调整流量
        /// </summary>
        public bool BehindSetFlow { get; set; }
        /// <summary>
        /// 是否更换备机
        /// </summary>
        public string ResetC1Equipment { get; set; }
        /// <summary>
        /// 备注C1
        /// </summary>
        public string DescriptionC1 { get; set; }
        #endregion

        #region 表单三
        /// <summary>
        /// 表单一 开始
        /// </summary>
        public DateTimeOffset C2StartDate { get; set; }
        /// <summary>
        /// 表单一 结束
        /// </summary>
        public DateTimeOffset C2EndDate { get; set; }
        /// <summary>
        /// 仪器零值
        /// </summary>
        public string EquipmentZero { get; set; }
        /// <summary>
        /// 实测值1
        /// </summary>
        public string EquipmentReal1 { get; set; }
        /// <summary>
        /// 实测值2
        /// </summary>
        public string EquipmentReal2 { get; set; }
        /// <summary>
        /// C2相对误差
        /// </summary>
        public string EquipmentRelativeError { get; set; }
        /// <summary>
        /// 校零
        /// </summary>
        public string CalibrationZero { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public bool BeforeEquipmentQualified { get; set; }
        /// <summary>
        /// 仪器跨度值
        /// </summary>
        public string EquipmentSpan { get; set; }
        /// <summary>
        /// 实测值 3
        /// </summary>
        public string EquipmentReal3 { get; set; }
        /// <summary>
        /// 相对误差
        /// </summary>
        public string BehindEquipmentRelativeError { get; set; }
        /// <summary>
        /// 校标
        /// </summary>
        public string BehindCalibration { get; set; }
        /// <summary>
        /// 是否合格
        /// </summary>
        public bool BehindEquipmentQualified { get; set; }
        /// <summary>
        /// 是否更换备机
        /// </summary>
        public string ResetC2Equipment { get; set; }
        /// <summary>
        /// 备注C2
        /// </summary>
        public string DescriptionC2 { get; set; }
        #endregion
        #region interfaces
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int? UpdateBy { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        #endregion interfaces

        /// <summary>
        /// 设备配置Id
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// 设备
        /// </summary>
        public virtual Equipment Equipment { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public override void Configure(EntityTypeBuilder<EquipmentTPM> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(p => p.Equipment)
                .WithMany(e=>e.EquipmentTPMs)
                .HasForeignKey(i => i.EquipmentId)
                .OnDelete(DeleteBehavior.NoAction);

            entityBuilder.HasIndex(p => p.Name);
            entityBuilder.HasIndex(p => p.EquipmentId);
            entityBuilder.HasIndex(p => p.EquipmentControlNumber);


        }
    }
}
