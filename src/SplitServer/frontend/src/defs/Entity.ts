import { MomentInput } from "moment";
export const Version = 'v0.0.5'
/**
 * 数据实体基类
 */
export declare class BizEntity {
    Id: number
}

/**
 * 视图实体基类
 */
export declare class BizView {
    PlaceHolder?: string
}

/**
 *产品实体
 */
export declare class Equipment extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *序列号 用于编辑显示的Id
     */
    SerialNumber?: string;
    /**
     *校准日期
     */
    CalibrationDate?: MomentInput;
    /**
     *校准数值
     */
    CalibrationValue?: number;
    /**
     *下次校准日期
     */
    NextCalibrationDate?: MomentInput;
    /**
     *设备条码
     */
    Barcode?: string;
    /**
     *控制编号 用于 粉尘仪编号
     */
    ControlNumber?: string;
    /**
     *经度
     */
    Longitude?: string;
    /**
     *纬度
     */
    Latitude?: string;
    /**
     *是否运维打卡
     */
    IsOperation?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *设备类型Id
     */
    EquipmentTypeId?: number;
    /**
     *设备配置Id
     */
    EquipmentConfigId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *设备类型 粉尘仪型号
     */
    EquipmentType?: EquipmentType;
    /**
     *设备配置
     */
    EquipmentConfig?: EquipmentConfig;
    /**
     *区域
     */
    Location?: Location;
    /**
     *设备维护列表
     */
    EquipmentTPMs?: Array<EquipmentTPM>;
}

/**
 *设备类别实体            TODO fhn 初始化dll路径到相关设备类型
 */
export declare class EquipmentType extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *类型
     */
    Type?: string;
    /**
     *型号
     */
    Model?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *路径
     */
    Path?: string;
    /**
     *加载方式
     */
    LoadingMode?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *设备配置
 */
export declare class EquipmentConfig extends BizEntity {
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *附加配置
     */
    AdditionalConfig?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *区域实体
 */
export declare class Location extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *父对象Id
     */
    ParentId?: number;
    /**
     *区域类型Id
     */
    LocationTypeId?: number;
    /**
     *可视化图Id
     */
    VisioDiagramId?: number;
    /**
     *可视化图
     */
    VisioDiagram?: VisioDiagram;
    /**
     *区域扩展信息
     */
    LocationExtra?: LocationExtra;
    /**
     *采样点集合
     */
    Sites?: Array<Site>;
    /**
     *父对象
     */
    Parent?: Location;
    /**
     *区域类型
     */
    LocationType?: LocationType;
}

/**
 *产品实体             从前端按钮暂存 update            每次加载 刷新时间
 */
export declare class EquipmentTPM extends BizEntity {
    /**
     *当前步骤 1 2 3
     */
    Stage?: number;
    /**
     *剩余时间 初始值为480 8分钟
     */
    RemainSecond?: number;
    /**
     *运行维护单位名称
     */
    Name?: string;
    /**
     *当前设备的设备 SerialNumber
     */
    EquipmentSerialNumber?: string;
    /**
     *控制编号 用于 当前设备的粉尘仪编号
     */
    EquipmentControlNumber?: string;
    /**
     *站点名称 来源于设备
     */
    EquipmentName?: string;
    /**
     *操作人
     */
    UserName?: string;
    /**
     *工程报建号
     */
    ProjectNumber?: string;
    /**
     *备注
     */
    Description?: string;
    /**
     *表单一 开始
     */
    StartDate?: MomentInput;
    /**
     *全部 结束
     */
    EndDate?: MomentInput;
    /**
     *供电是否正常 默认正常
     */
    PowerSupplyIsError?: boolean;
    /**
     *处理方法
     */
    PowerSupplyMsg?: string;
    /**
     *采样头、链接线路 默认正常
     */
    SampleIsNormal?: boolean;
    /**
     *处理方法
     */
    SampleMsg?: string;
    /**
     *加热除湿温度测温结果 用于填写温度 使用℃
     */
    TemperatureData?: string;
    /**
     *加热除湿温度测温结果 信息
     */
    TemperatureMsg?: string;
    /**
     *扬尘系统
            检查仪器线路、采样泵、校准系统是否正常
     */
    DeviceIsError?: boolean;
    /**
     *处理方法
     */
    DeviceMsg?: string;
    /**
     *扬尘系统
            检查仪器滤罐是否需更换
     */
    ResetEquipment?: boolean;
    /**
     *处理方法
     */
    ResetEquipmentMsg?: string;
    /**
     *气象参数
            检查温度、湿度、大气压、风速、风向是否正常
     */
    EnvironmentIsError?: boolean;
    /**
     *处理方法
     */
    EnvironmentMsg?: string;
    /**
     *数据采集传输
            数据传输是否正常
     */
    DataTransmissionIsError?: boolean;
    /**
     *处理方法
     */
    DataTransmissionMsg?: string;
    /**
     *视频监控是否正常
     */
    VedioIsError?: boolean;
    /**
     *处理方法
     */
    VedioMsg?: string;
    /**
     *设备现场位置
     */
    DeviceLocation?: string;
    /**
     *表单二 开始
     */
    C1StartDate?: MomentInput;
    /**
     *表单一 结束
     */
    C1EndDate?: MomentInput;
    /**
     *设定流量
     */
    SetFlow?: string;
    /**
     *实测流量1
     */
    BeforeFlow1?: string;
    /**
     *实测流量2
     */
    BeforeFlow2?: string;
    /**
     *实测流量3
     */
    BeforeFlow3?: string;
    /**
     *相对误差
     */
    BeforeRelativeError?: string;
    /**
     *是否需要调整流量 
            根据 相对误差的结果 
            满足超过10%  则 取值为 “是” 如果 为是 则需要输入校准后的内容
            否的话可以直接提交
     */
    BeforeSetFlow?: boolean;
    /**
     *设定流量2
     */
    SetFlow2?: string;
    /**
     *实测流量1
     */
    BehindFlow1?: string;
    /**
     *实测流量2
     */
    BehindFlow2?: string;
    /**
     *实测流量3
     */
    BehindFlow3?: string;
    /**
     *相对误差
     */
    BehindRelativeError?: string;
    /**
     *是否需要调整流量
     */
    BehindSetFlow?: boolean;
    /**
     *是否更换备机
     */
    ResetC1Equipment?: string;
    /**
     *备注C1
     */
    DescriptionC1?: string;
    /**
     *表单一 开始
     */
    C2StartDate?: MomentInput;
    /**
     *表单一 结束
     */
    C2EndDate?: MomentInput;
    /**
     *仪器零值
     */
    EquipmentZero?: string;
    /**
     *实测值1
     */
    EquipmentReal1?: string;
    /**
     *实测值2
     */
    EquipmentReal2?: string;
    /**
     *C2相对误差
     */
    EquipmentRelativeError?: string;
    /**
     *校零
     */
    CalibrationZero?: string;
    /**
     *是否合格
     */
    BeforeEquipmentQualified?: boolean;
    /**
     *仪器跨度值
     */
    EquipmentSpan?: string;
    /**
     *实测值 3
     */
    EquipmentReal3?: string;
    /**
     *相对误差
     */
    BehindEquipmentRelativeError?: string;
    /**
     *校标
     */
    BehindCalibration?: string;
    /**
     *是否合格
     */
    BehindEquipmentQualified?: boolean;
    /**
     *是否更换备机
     */
    ResetC2Equipment?: string;
    /**
     *备注C2
     */
    DescriptionC2?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *设备配置Id
     */
    EquipmentId?: number;
    /**
     *设备
     */
    Equipment?: Equipment;
}

/**
 *EquipmentConfig审计 自动生成，请勿修改
 */
export declare class EquipmentConfigAudit extends BizEntity {
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *附加配置
     */
    AdditionalConfig?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *用于数据库 单项处理 邮件或者短信发送
 */
export declare class EquipmentNotification extends BizEntity {
    /**
     *电话号
     */
    Phone?: string;
    /**
     *电话号
     */
    IsAlert?: boolean;
    /**
     *设备Id 作用 当切换设备时 不希望其中的信息改变，所以不绑定主键
     */
    EquipmentId?: number;
    /**
     *设备名称
     */
    EquipmentName?: string;
}

/**
 *产品视图
 */
export declare class EquipmentV extends BizView {
    /**
     *产品Id
     */
    EquipmentId?: number;
    /**
     *产品名称
     */
    Name?: string;
    /**
     *产品描述
     */
    Description?: string;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *区域Id面包屑导航
     */
    LocationBreadcrumb?: string;
    /**
     *区域名称
     */
    LocationName?: string;
    /**
     *设备类型Id
     */
    EquipmentTypeId?: number;
    /**
     *设备类型描述
     */
    EquipmentTypeDescription?: string;
    /**
     *校准日期
     */
    CalibrationDate?: MomentInput;
    /**
     *校准数值
     */
    CalibrationValue?: number;
    /**
     *下次校准日期
     */
    NextCalibrationDate?: MomentInput;
    /**
     *单位
     */
    UOM?: string;
    /**
     *设备条码
     */
    Barcode?: string;
    /**
     *控制编号
     */
    ControlNumber?: string;
    /**
     *设备校准用户名
     */
    PerformedBy?: string;
    /**
     *设备校准人
     */
    PerformedByUserId?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *可视化图
 */
export declare class VisioDiagram extends BizEntity {
    /**
     *地图Id
     */
    MapId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *地图
     */
    Map?: Map;
    /**
     *可视化区域
     */
    VisioLocations?: Array<VisioLocation>;
    /**
     *可视化采样点
     */
    VisioSites?: Array<VisioSite>;
}

/**
 *区域附加属性
 */
export declare class LocationExtra extends BizEntity {
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *面包屑导航，逗号拼接
     */
    Breadcrumb?: string;
    /**
     *路径，->拼接
     */
    LocationPath?: string;
    /**
     *区域
     */
    Location?: Location;
}

/**
 *采样点实体
 */
export declare class Site extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *采样点类型Id
     */
    SiteTypeId?: number;
    /**
     *区域
     */
    Location?: Location;
    /**
     *采样点类型
     */
    SiteType?: SiteType;
}

/**
 *Location审计 自动生成，请勿修改
 */
export declare class LocationAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *父对象Id
     */
    ParentId?: number;
    /**
     *区域类型Id
     */
    LocationTypeId?: number;
    /**
     *可视化图Id
     */
    VisioDiagramId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *区域类型实体
 */
export declare class LocationType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *权重
     */
    Weight?: number;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *包括的Location
     */
    Locations?: Array<Location>;
}

/**
 *LocationType审计 自动生成，请勿修改
 */
export declare class LocationTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *权重
     */
    Weight?: number;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *区域视图
 */
export declare class LocationV extends BizView {
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *父对象Id
     */
    ParentId?: number;
    /**
     *区域类型Id
     */
    LocationTypeId?: number;
    /**
     *级别Id
     */
    ClassificationId?: number;
    /**
     *第二级别Id
     */
    SecondClassificationId?: number;
    /**
     *可视化图Id
     */
    VisioDiagramId?: number;
    /**
     *面包屑导航，逗号拼接
     */
    Breadcrumb?: string;
    /**
     *路径，->拼接
     */
    LocationPath?: string;
}

/**
 *Site审计 自动生成，请勿修改
 */
export declare class SiteAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *采样点类型Id
     */
    SiteTypeId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *采样点类型实体
 */
export declare class SiteType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *采样点
     */
    Sites?: Array<Site>;
}

/**
 *SiteType审计 自动生成，请勿修改
 */
export declare class SiteTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *采样点视图
 */
export declare class SiteV extends BizView {
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *采样点名称
     */
    SiteName?: string;
    /**
     *采样点描述
     */
    SiteDesc?: string;
    /**
     *采样点类型Id
     */
    SiteTypeId?: number;
    /**
     *采样点类型描述
     */
    SiteTypeDesc?: string;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *区域名称
     */
    LocationName?: string;
    /**
     *区域描述
     */
    LocationDesc?: string;
    /**
     *区域Id面包屑导航
     */
    LocationBreadcrumb?: string;
    /**
     *区域路径
     */
    LocationFullName?: string;
    /**
     *级别Id
     */
    ClassificationId?: number;
    /**
     *级别描述（名称）
     */
    ClassificationDesc?: string;
    /**
     *第二级别Id
     */
    SecondClassificationId?: number;
    /**
     *第二级别描述（名称）
     */
    SecondClassificationDesc?: string;
    /**
     *条码
     */
    BarCode?: string;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *天数据
 */
export declare class EnvironmentalSensorDaily extends BizEntity {
    EquipmentId?: number;
    Date?: MomentInput;
    pm2_5_average?: number;
    pm2_5_max?: number;
    pm2_5_min?: number;
    pm10_average?: number;
    pm10_max?: number;
    pm10_min?: number;
    /**
     *扬尘TSP（单位：mg/m³）
     */
    ParticulateMatterMin?: number;
    ParticulateMatterMax?: number;
    ParticulateMatterAvg?: number;
    /**
     *温度（单位：℃）
     */
    TemperatureMin?: number;
    TemperatureMax?: number;
    TemperatureAvg?: number;
    /**
     *湿度（单位：Rh%）
     */
    HumidityMin?: number;
    HumidityMax?: number;
    HumidityAvg?: number;
    /**
     *气压
     */
    AirPressureMin?: number;
    AirPressureMax?: number;
    AirPressureAvg?: number;
    /**
     *风速（单位：m/s）
     */
    WindSpeedMin?: number;
    WindSpeedMax?: number;
    WindSpeedAvg?: number;
    /**
     *风向（单位：°）
     */
    WindDirectionMin?: number;
    WindDirectionMax?: number;
    WindDirectionAvg?: number;
    /**
     *噪声（单位：dB）
     */
    NoiseMin?: number;
    NoiseMax?: number;
    NoiseAvg?: number;
    /**
     *CPM
     */
    CPMAvg?: number;
    is_power_on?: boolean;
    is_valid_data?: boolean;
    equipment_data_valid_flag?: number;
    source_of_data?: number;
    dust_calibration_step?: number;
}

/**
 *小时
 */
export declare class EnvironmentalSensorHour extends BizEntity {
    EquipmentId?: number;
    Date?: MomentInput;
    pm2_5_average?: number;
    pm2_5_max?: number;
    pm2_5_min?: number;
    pm10_average?: number;
    pm10_max?: number;
    pm10_min?: number;
    /**
     *扬尘TSP（单位：mg/m³）
     */
    ParticulateMatterMin?: number;
    ParticulateMatterMax?: number;
    ParticulateMatterAvg?: number;
    /**
     *温度（单位：℃）
     */
    TemperatureMin?: number;
    TemperatureMax?: number;
    TemperatureAvg?: number;
    /**
     *湿度（单位：Rh%）
     */
    HumidityMin?: number;
    HumidityMax?: number;
    HumidityAvg?: number;
    /**
     *气压
     */
    AirPressureMin?: number;
    AirPressureMax?: number;
    AirPressureAvg?: number;
    /**
     *风速（单位：m/s）
     */
    WindSpeedMin?: number;
    WindSpeedMax?: number;
    WindSpeedAvg?: number;
    /**
     *风向（单位：°）
     */
    WindDirectionMin?: number;
    WindDirectionMax?: number;
    WindDirectionAvg?: number;
    /**
     *噪声（单位：dB）
     */
    NoiseMin?: number;
    NoiseMax?: number;
    NoiseAvg?: number;
    /**
     *CPM
     */
    CPMAvg?: number;
    is_power_on?: boolean;
    is_valid_data?: boolean;
    equipment_data_valid_flag?: number;
    source_of_data?: number;
    dust_calibration_step?: number;
}

/**
 *天数据
 */
export declare class EnvironmentalSensorMinute extends BizEntity {
    EquipmentId?: number;
    Date?: MomentInput;
    pm2_5_average?: number;
    pm2_5_max?: number;
    pm2_5_min?: number;
    pm10_average?: number;
    pm10_max?: number;
    pm10_min?: number;
    /**
     *扬尘TSP（单位：mg/m³）
     */
    ParticulateMatterMin?: number;
    ParticulateMatterMax?: number;
    ParticulateMatterAvg?: number;
    /**
     *温度（单位：℃）
     */
    TemperatureMin?: number;
    TemperatureMax?: number;
    TemperatureAvg?: number;
    /**
     *湿度（单位：Rh%）
     */
    HumidityMin?: number;
    HumidityMax?: number;
    HumidityAvg?: number;
    /**
     *气压
     */
    AirPressureMin?: number;
    AirPressureMax?: number;
    AirPressureAvg?: number;
    /**
     *风速（单位：m/s）
     */
    WindSpeedMin?: number;
    WindSpeedMax?: number;
    WindSpeedAvg?: number;
    /**
     *风向（单位：°）
     */
    WindDirectionMin?: number;
    WindDirectionMax?: number;
    WindDirectionAvg?: number;
    /**
     *噪声（单位：dB）
     */
    NoiseMin?: number;
    NoiseMax?: number;
    NoiseAvg?: number;
    /**
     *CPM
     */
    CPMAvg?: number;
    is_power_on?: boolean;
    is_valid_data?: boolean;
    equipment_data_valid_flag?: number;
    source_of_data?: number;
    dust_calibration_step?: number;
}

/**
 *15分钟
 */
export declare class EnvironmentalSensorQuarter extends BizEntity {
    EquipmentId?: number;
    Date?: MomentInput;
    pm2_5_average?: number;
    pm2_5_max?: number;
    pm2_5_min?: number;
    pm10_average?: number;
    pm10_max?: number;
    pm10_min?: number;
    /**
     *扬尘TSP（单位：mg/m³）
     */
    ParticulateMatterMin?: number;
    ParticulateMatterMax?: number;
    ParticulateMatterAvg?: number;
    /**
     *温度（单位：℃）
     */
    TemperatureMin?: number;
    TemperatureMax?: number;
    TemperatureAvg?: number;
    /**
     *湿度（单位：Rh%）
     */
    HumidityMin?: number;
    HumidityMax?: number;
    HumidityAvg?: number;
    /**
     *气压
     */
    AirPressureMin?: number;
    AirPressureMax?: number;
    AirPressureAvg?: number;
    /**
     *风速（单位：m/s）
     */
    WindSpeedMin?: number;
    WindSpeedMax?: number;
    WindSpeedAvg?: number;
    /**
     *风向（单位：°）
     */
    WindDirectionMin?: number;
    WindDirectionMax?: number;
    WindDirectionAvg?: number;
    /**
     *噪声（单位：dB）
     */
    NoiseMin?: number;
    NoiseMax?: number;
    NoiseAvg?: number;
    /**
     *CPM
     */
    CPMAvg?: number;
    is_power_on?: boolean;
    is_valid_data?: boolean;
    equipment_data_valid_flag?: number;
    source_of_data?: number;
    dust_calibration_step?: number;
}

/**
 *用于移植MN 数据
 */
export declare class MonitorDevice extends BizEntity {
    DeviceId?: string;
    DeviceName?: string;
    MnCode?: string;
    SiteOfDevice?: number;
    DeviceCardTelNumber1?: string;
    DeviceCardTelNumber2?: string;
    ConfigDustK?: number;
    ConfigNoiseZeroOffset?: number;
    ConfigNoiseK?: number;
    ConfigDustEarlyWarningEnable?: boolean;
    ConfigDustEarlyWarningMax?: number;
    ConfigDustEarlyWarningMin?: number;
    ConfigNoiseEarlyWarningEnable?: boolean;
    ConfigNoiseEarlyWarningMax?: number;
    ConfigNoiseEarlyWarningMin?: number;
    ConfigDustWarningMax?: number;
    ConfigDustWarningMin?: number;
    ConfigNoiseWarningMax?: number;
    ConfigNoiseWarningMin?: number;
    IsInUse?: boolean;
    ProductionDate?: MomentInput;
    ConfigDustEarlyWarningAuto?: boolean;
    ConfigDustWarningAuto?: boolean;
    ConfigDustWarningAutoMode?: number;
    VideoDeviceSerial?: string;
    VideoDeviceVerificationCode?: string;
    AutoSprayThreshold?: number;
    JwMnCode?: string;
    NoiseNightAlert?: boolean;
    DustEarlyWarningCommand?: string;
    DustKFrontend?: number;
    SprayMode?: number;
    Protocol?: number;
    DoorStatus?: number;
    LockMode?: number;
    LockId?: string;
    RangeDataAnalyseEn?: number;
}

/**
 *产品实体
 */
export declare class Product extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *产品类别id
     */
    ProductTypeId?: number;
    /**
     *是否在监管之下
     */
    ChainOfCustody?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *产品类别
     */
    ProductType?: ProductType;
}

/**
 *产品类别实体
 */
export declare class ProductType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *Product审计 自动生成，请勿修改
 */
export declare class ProductAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *产品类别id
     */
    ProductTypeId?: number;
    /**
     *是否在监管之下
     */
    ChainOfCustody?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *ProductType审计 自动生成，请勿修改
 */
export declare class ProductTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *地图
 */
export declare class Map extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *路径
     */
    Path?: string;
    /**
     *地图分类Id
     */
    MapCategoryId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *地图分类
     */
    MapCategory?: MapCategory;
    /**
     *地图相关的可视化图
     */
    VisioDiagrams?: Array<VisioDiagram>;
}

/**
 *地图分类实体
 */
export declare class MapCategory extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *父对象Id
     */
    ParentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *分类下的地图
     */
    Maps?: Array<Map>;
    /**
     *父对象
     */
    Parent?: Map;
}

/**
 *Map审计 自动生成，请勿修改
 */
export declare class MapAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *路径
     */
    Path?: string;
    /**
     *地图分类Id
     */
    MapCategoryId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MapCategory审计 自动生成，请勿修改
 */
export declare class MapCategoryAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *父对象Id
     */
    ParentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *可视化区域
 */
export declare class VisioLocation extends BizEntity {
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *地图Id
     */
    VisioDiagramId?: number;
    /**
     *地图位置X
     */
    X?: number;
    /**
     *地图位置Y
     */
    Y?: number;
    /**
     *宽度
     */
    Width?: number;
    /**
     *高度
     */
    Height?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *区域
     */
    Location?: Location;
    /**
     *地图
     */
    VisioDiagram?: VisioDiagram;
}

/**
 *可视化采样点
 */
export declare class VisioSite extends BizEntity {
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *地图Id
     */
    VisioDiagramId?: number;
    /**
     *地图位置X
     */
    X?: number;
    /**
     *地图位置Y
     */
    Y?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *采样点
     */
    Site?: Site;
    /**
     *地图
     */
    VisioDiagram?: VisioDiagram;
}

/**
 *VisioDiagram审计 自动生成，请勿修改
 */
export declare class VisioDiagramAudit extends BizEntity {
    /**
     *地图Id
     */
    MapId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *VisioLocation审计 自动生成，请勿修改
 */
export declare class VisioLocationAudit extends BizEntity {
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *地图Id
     */
    VisioDiagramId?: number;
    /**
     *地图位置X
     */
    X?: number;
    /**
     *地图位置Y
     */
    Y?: number;
    /**
     *宽度
     */
    Width?: number;
    /**
     *高度
     */
    Height?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *VisioSite审计 自动生成，请勿修改
 */
export declare class VisioSiteAudit extends BizEntity {
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *地图Id
     */
    VisioDiagramId?: number;
    /**
     *地图位置X
     */
    X?: number;
    /**
     *地图位置Y
     */
    Y?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *部门实体
 */
export declare class Department extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *经理Id
     */
    ManagerId?: number;
    /**
     *父部门Id
     */
    ParentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *部门员工
     */
    Employees?: Array<User>;
    /**
     *子部门
     */
    Children?: Array<Department>;
    /**
     *经理
     */
    Manager?: User;
    /**
     *父部门
     */
    Parent?: Department;
}

/**
 *用户实体
 */
export declare class User extends BizEntity {
    /**
     *账号
     */
    Account?: string;
    /**
     *密码
     */
    Password?: string;
    /**
     *md5密码盐
     */
    Salt?: string;
    /**
     *真实姓名
     */
    RealName?: string;
    /**
     *员工编号
     */
    EmployeeId?: string;
    /**
     *职位
     */
    Title?: string;
    /**
     *性别
     */
    Gender?: Gender;
    /**
     *电子邮件
     */
    EMail?: string;
    /**
     *电话
     */
    Phone?: string;
    /**
     *状态
     */
    Status?: UserStatus;
    /**
     *上次认证日期
     */
    LastMonitoredDate?: MomentInput;
    /**
     *初始认证日期
     */
    InitialQualificationDate?: MomentInput;
    /**
     *下次认证日期
     */
    NextQualificationDate?: MomentInput;
    /**
     *密码有效期
     */
    PasswordExpiryPeriod?: MomentInput;
    /**
     *最后一次密码更改时间戳
     */
    LastPasswordModified?: MomentInput;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *所属部门Id
     */
    DepartmentId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *所属部门
     */
    Department?: Department;
    /**
     *所属区域
     */
    Location?: Location;
    /**
     *角色
     */
    Roles?: Array<Role>;
    /**
     *角色映射
     */
    UserRoles?: Array<UserRole>;
    /**
     *用户配置
     */
    Profile?: UserProfile;
    /**
     *用户密码记录
     */
    UserPasswordHistories?: Array<UserPasswordHistory>;
}

/**
 *Department审计 自动生成，请勿修改
 */
export declare class DepartmentAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *经理Id
     */
    ManagerId?: number;
    /**
     *父部门Id
     */
    ParentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *电子签名实体
 */
export declare class ElectronicSignature extends BizEntity {
    /**
     *签名账户
     */
    Account?: string;
    /**
     *签名用户姓名
     */
    RealName?: string;
    /**
     *签名用户Id
     */
    UserId?: number;
    /**
     *签名日期
     */
    SignDate?: MomentInput;
    /**
     *ip地址
     */
    IpAddress?: string;
    /**
     *签名分类
     */
    Category?: string;
    /**
     *备注
     */
    Comment?: string;
    /**
     *顺序
     */
    Order?: number;
    /**
     *是否系统自生成
     */
    IsSystemOperation?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *签名用户
     */
    User?: User;
    /**
     *具体数据项
     */
    ElectronicSignatureItems?: Array<ElectronicSignatureItem>;
}

/**
 *电子签名数据项
 */
export declare class ElectronicSignatureItem extends BizEntity {
    /**
     *表名
     */
    TableName?: string;
    /**
     *被签名数据主键
     */
    PrimaryKey?: number;
    /**
     *电子签名Id
     */
    ElectronicSignatureId?: number;
    /**
     *最后审计实体主键
     */
    LastAuditKey?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *电子签名
     */
    ElectronicSignature?: ElectronicSignature;
}

/**
 *电子签名配置
 */
export declare class ESignConfig extends BizEntity {
    /**
     *电子签名类型
     */
    Category?: string;
    /**
     *权限，逗号分隔
     */
    Permissions?: string;
    /**
     *签名次数
     */
    SignCount?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *ESignConfig审计 自动生成，请勿修改
 */
export declare class ESignConfigAudit extends BizEntity {
    /**
     *电子签名类型
     */
    Category?: string;
    /**
     *权限，逗号分隔
     */
    Permissions?: string;
    /**
     *签名次数
     */
    SignCount?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *日志功能实体
 */
export declare class Log extends BizEntity {
    /**
     *用户名
     */
    UserName?: string;
    /**
     *活动名称
     */
    Name?: string;
    /**
     *活动描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
}

/**
 *权限实体
 */
export declare class Permission extends BizEntity {
    /**
     *备注，由于多语言需求，页面根据code显示
     */
    Description?: string;
    /**
     *根据部门生成多个菜单
     */
    DepartFormatter?: string;
    /**
     *类型
     */
    Type?: PermissionType;
    /**
     *权限编码
     */
    Code?: string;
    /**
     *父id
     */
    ParentId?: number;
    /**
     *排序
     */
    Order?: number;
    /**
     *角色
     */
    Roles?: Array<Role>;
    /**
     *下级权限
     */
    SubPermissions?: Array<Permission>;
    /**
     *角色映射
     */
    RolePermissions?: Array<RolePermission>;
    /**
     *父权限
     */
    Parent?: Permission;
}

/**
 *角色实体
 */
export declare class Role extends BizEntity {
    /**
     *角色名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *权限
     */
    Permissions?: Array<Permission>;
    /**
     *权限映射
     */
    RolePermissions?: Array<RolePermission>;
    /**
     *用户
     */
    Users?: Array<User>;
    /**
     *用户映射
     */
    UserRoles?: Array<UserRole>;
}

/**
 *角色权限关联
 */
export declare class RolePermission extends BizEntity {
    /**
     *角色Id
     */
    RoleId?: number;
    /**
     *权限Id
     */
    PermissionId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *角色
     */
    Role?: Role;
    /**
     *权限
     */
    Permission?: Permission;
}

/**
 *用户角色关联
 */
export declare class UserRole extends BizEntity {
    /**
     *角色Id
     */
    RoleId?: number;
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *角色
     */
    Role?: Role;
    /**
     *用户
     */
    User?: User;
}

/**
 *Role审计 自动生成，请勿修改
 */
export declare class RoleAudit extends BizEntity {
    /**
     *角色名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *RolePermission审计 自动生成，请勿修改
 */
export declare class RolePermissionAudit extends BizEntity {
    /**
     *角色Id
     */
    RoleId?: number;
    /**
     *权限Id
     */
    PermissionId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *用户配置
 */
export declare class UserProfile extends BizEntity {
    /**
     *看板配置
     */
    DashboardConfig?: string;
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *用户多语言显示设置
     */
    Locale?: string;
    /**
     *用户配置JSON
     */
    UserSettings?: string;
    /**
     *用户
     */
    User?: User;
}

/**
 *用户密码记录
 */
export declare class UserPasswordHistory extends BizEntity {
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *用户
     */
    User?: User;
}

/**
 *User审计 自动生成，请勿修改
 */
export declare class UserAudit extends BizEntity {
    /**
     *账号
     */
    Account?: string;
    /**
     *密码
     */
    Password?: string;
    /**
     *md5密码盐
     */
    Salt?: string;
    /**
     *真实姓名
     */
    RealName?: string;
    /**
     *员工编号
     */
    EmployeeId?: string;
    /**
     *职位
     */
    Title?: string;
    /**
     *性别
     */
    Gender?: Gender;
    /**
     *电子邮件
     */
    EMail?: string;
    /**
     *电话
     */
    Phone?: string;
    /**
     *状态
     */
    Status?: UserStatus;
    /**
     *上次认证日期
     */
    LastMonitoredDate?: MomentInput;
    /**
     *初始认证日期
     */
    InitialQualificationDate?: MomentInput;
    /**
     *下次认证日期
     */
    NextQualificationDate?: MomentInput;
    /**
     *密码有效期
     */
    PasswordExpiryPeriod?: MomentInput;
    /**
     *最后一次密码更改时间戳
     */
    LastPasswordModified?: MomentInput;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *所属部门Id
     */
    DepartmentId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *用户历史记录实体
 */
export declare class UserHistory extends BizEntity {
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *登录日期
     */
    Logined?: MomentInput;
    /**
     *用户
     */
    User?: User;
}

/**
 *UserRole审计 自动生成，请勿修改
 */
export declare class UserRoleAudit extends BizEntity {
    /**
     *角色Id
     */
    RoleId?: number;
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *用户视图
 */
export declare class UserV extends BizView {
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *姓名
     */
    RealName?: string;
    /**
     *所属部门Id
     */
    DepartmentId?: number;
    /**
     *所属部门名称
     */
    DepartmentName?: string;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *区域名
     */
    LocationName?: string;
    /**
     *区域路径
     */
    LocationFullName?: string;
    /**
     *区域Id面包屑导航
     */
    LocationBreadcrumb?: string;
    /**
     *工号
     */
    EmployeeId?: string;
    /**
     *职位
     */
    Title?: string;
    /**
     *用户名
     */
    UserName?: string;
    /**
     *电邮
     */
    EMail?: string;
    /**
     *状态
     */
    Status?: UserStatus;
    /**
     *
     */
    HasAccess?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *权限类型
 */
export declare type PermissionType = "Menu" | "Action";

/**
 *用户状态
 */
export declare type UserStatus = "Normal" | "Frozen";

/**
 *性别
 */
export declare type Gender = "Male" | "Female";

export declare class Result_1OfBoolean extends BizView {
    Data?: boolean;
    Code?: number;
    Message?: string;
    Success?: boolean;
    Timestamp?: MomentInput;
}

/**
 *离线数据
 */
export declare class OfflineModel extends BizView {
    /**
     *签名数据
     */
}

/**
 *签名数据
 */
export declare class ESignData extends BizView {
    /**
     *签名账户
     */
    Account?: string;
    /**
     *签名用户姓名
     */
    RealName?: string;
    /**
     *签名用户Id
     */
    ESignedBy?: number;
    /**
     *修改、创建数据用户Id
     */
    UserId?: number;
    /**
     *ip地址
     */
    IpAddress?: string;
    /**
     *签名分类
     */
    Category?: string;
    /**
     *备注
     */
    Comment?: string;
    /**
     *签名的顺序
     */
    Order?: number;
    /**
     *
     */
    IsSystemOperation?: boolean;
    /**
     *时间戳
     */
    Timestamp?: MomentInput;
}

/**
 *通知类型枚举
 */
export declare type NotificationTypes = "Deviation" | "LoginFailure" | "InvalidESig" | "AccountLocked" | "SampleNotCompleted" | "WeeklyTestNotCompleted" | "MonthlyTestNotCompleted" | "QuarterlyTestNotCompleted" | "MaxTimeAboutToExceed" | "WorkNotYetCompletedForToday" | "OrganismFound" | "OrganismAdded" | "WorkflowError" | "EquipmentAboutToExpire" | "MediaInventoryLow" | "UserQualificationLapsed" | "UserQualificationDue";

/**
 *邮件
 */
export declare class EMail extends BizEntity {
    /**
     *主题
     */
    Subject?: string;
    /**
     *内容
     */
    Body?: string;
    /**
     *是否为html内容
     */
    IsHtmlBody?: boolean;
    /**
     *发送时间
     */
    SendDate?: MomentInput;
    /**
     *收件人Id
     */
    UserId?: number;
    /**
     *收件人
     */
    User?: User;
    /**
     *附件
     */
    Attachments?: Array<EMailAttachment>;
    /**
     *相关的通知
     */
    Notifications?: Array<Notification>;
    /**
     *通知关联
     */
    NotificationEMails?: Array<NotificationEMail>;
}

/**
 *邮件附件
 */
export declare class EMailAttachment extends BizEntity {
    /**
     *文件路径
     */
    FilePath?: string;
    /**
     *内容Id，如果UsedInBody为true，则必须与body中id一致
     */
    ContentId?: string;
    /**
     *是否用于body 如果true，则用于body中展示
     */
    UsedInBody?: boolean;
    /**
     *邮件Id
     */
    EMailId?: number;
    /**
     *邮件
     */
    EMail?: EMail;
}

/**
 *通知实体
 */
export declare class Notification extends BizEntity {
    /**
     *通知内容，可自行设置，也可通过 Messages 设置为json数组
     */
    Content?: string;
    /**
     *执行通知的时间戳
     */
    NotificationDate?: MomentInput;
    /**
     *通知类型Id
     */
    NotificationTypeId?: number;
    /**
     *创建此通知的用户Id
     */
    UserId?: number;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *通知类型
     */
    NotificationType?: NotificationType;
    /**
     *创建此通知的用户
     */
    User?: User;
    /**
     *采样点
     */
    Site?: Site;
    /**
     *设备
     */
    Equipment?: Equipment;
    /**
     *关联电邮
     */
    NotificationEMails?: Array<NotificationEMail>;
    /**
     *电邮
     */
    EMails?: Array<EMail>;
}

/**
 *通知电邮关联
 */
export declare class NotificationEMail extends BizEntity {
    /**
     *通知Id
     */
    NotificationId?: number;
    /**
     *电邮Id
     */
    EMailId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *通知
     */
    Notification?: Notification;
    /**
     *电邮
     */
    EMail?: EMail;
}

/**
 *通知类型
 */
export declare class NotificationType extends BizEntity {
    /**
     *通知类型标签
     */
    Type?: NotificationTypes;
    /**
     *通知类型名称
     */
    Name?: string;
    /**
     *通知类型描述
     */
    Description?: string;
    /**
     *英文
     */
    EnName?: string;
    /**
     *中文
     */
    ZhName?: string;
    /**
     *英文
     */
    EnDescription?: string;
    /**
     *中文
     */
    ZhDescription?: string;
    /**
     *通知类型处理器类型，此类通知都由一种处理器处理
     */
    ProcessorType?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *属于此类型的通知
     */
    Notifications?: Array<Notification>;
    /**
     *属于此类型的订阅
     */
    Subscriptions?: Array<Subscription>;
}

/**
 *通知视图
 */
export declare class NotificationV extends BizView {
    /**
     *通知Id
     */
    NotificationId?: number;
    /**
     *通知类型Id
     */
    NotificationTypeId?: number;
    /**
     *通知类型名称
     */
    NotificationTypeName?: string;
    /**
     *通知类型描述
     */
    NotificationTypeDesc?: string;
    /**
     *执行通知的时间戳
     */
    NotificationDate?: MomentInput;
    /**
     *创建此通知的用户Id
     */
    UserId?: number;
    /**
     *创建此通知的用户区域Id
     */
    UserLocationId?: number;
    /**
     *创建此通知的用户区域Id导航
     */
    UserLocationBreadcrumb?: string;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *采样点名称
     */
    SiteName?: string;
    /**
     *采样点描述
     */
    SiteDesc?: string;
    /**
     *采样点区域Id
     */
    SiteLocationId?: number;
    /**
     *采样点区域Id导航
     */
    SiteLocationBreadcrumb?: string;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *偏差Id
     */
    DeviationId?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *设备区域Id
     */
    EquipmentLocationId?: number;
    /**
     *设备区域Id导航
     */
    EquipmentLocationBreadcrumb?: string;
    /**
     *培养基Id
     */
    MediaId?: number;
    /**
     *培养基区域Id
     */
    MediaLocationId?: number;
    /**
     *培养基区域Id导航
     */
    MediaLocationBreadcrumb?: string;
    /**
     *通知内容，可自行设置，也可通过设置为json数组
     */
    Content?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
}

/**
 *订阅实体
 */
export declare class Subscription extends BizEntity {
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *通知类型Id
     */
    NotificationTypeId?: number;
    /**
     *属性Id
     */
    PlanGroupId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *用户
     */
    User?: User;
    /**
     *通知类型
     */
    NotificationType?: NotificationType;
    /**
     *区域
     */
    Location?: Location;
}

/**
 *订阅视图
 */
export declare class SubscriptionV extends BizView {
    /**
     *订阅Id
     */
    SubscriptionId?: number;
    /**
     *用户Id
     */
    UserId?: number;
    /**
     *用户姓名
     */
    RealName?: string;
    /**
     *用户所属部门Id
     */
    DepartmentId?: number;
    /**
     *用户区域Id
     */
    UserLocationId?: number;
    /**
     *用户区域Id面包导航
     */
    UserLocationBreadcrumb?: string;
    /**
     *用户工号
     */
    EmployeeId?: string;
    /**
     *用户职位
     */
    Title?: string;
    /**
     *用户名
     */
    UserName?: string;
    /**
     *用户电邮
     */
    EMail?: string;
    /**
     *用户状态
     */
    UserStatus?: UserStatus;
    /**
     *通知类型Id
     */
    NotificationTypeId?: number;
    /**
     *通知类型名称
     */
    NotificationTypeName?: string;
    /**
     *通知类型描述
     */
    NotificationTypeDescription?: string;
    /**
     *通知类型是否启用
     */
    IsNotificationTypeActive?: boolean;
    /**
     *属性Id
     */
    PlanGroupId?: number;
    /**
     *订阅区域Id
     */
    LocationId?: number;
    /**
     *订阅区域名称
     */
    LocationName?: string;
    /**
     *订阅区域是否启用
     */
    IsLocationActive?: boolean;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
}

/**
 *系统配置项
 */
export declare class ConfigItem extends BizEntity {
    /**
     *属性
     */
    Property?: string;
    /**
     *值
     */
    Value?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
}

/**
 *ConfigItem审计 自动生成，请勿修改
 */
export declare class ConfigItemAudit extends BizEntity {
    /**
     *属性
     */
    Property?: string;
    /**
     *值
     */
    Value?: string;
    /**
     *创建时间
     */
    CreatedTime?: MomentInput;
    /**
     *更新时间
     */
    UpdatedTime?: MomentInput;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: MomentInput;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *审计操作
 */
export declare type AuditAction = "Insert" | "Update" | "Delete";



export const ActivedEntities = [
    'Equipment',
    'EquipmentType',
    'EquipmentConfig',
    'Location',
    'EquipmentTPM',
    'EquipmentV',
    'VisioDiagram',
    'Site',
    'LocationType',
    'LocationV',
    'SiteType',
    'SiteV',
    'Product',
    'ProductType',
    'Map',
    'MapCategory',
    'VisioLocation',
    'VisioSite',
    'Department',
    'User',
    'ESignConfig',
    'Role',
    'UserV',
    'Notification',
    'NotificationType',
    'Subscription',
    'SubscriptionV'];
