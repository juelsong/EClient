export const Version = 'v0.0.47'
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

export declare class AllSystemPromptV extends BizView {
    LocationId?: number;
    LocationName?: string;
    LocationTypeId?: number;
    ParentId?: number;
    SystemId?: number;
    SystemTypeId?: number;
    SystemTypeDesc?: string;
    SystemClassificationId?: number;
    SystemClassificationDesc?: string;
    SystemName?: string;
    SystemDescription?: string;
    SystemBarcode?: string;
    SiteId?: number;
    SiteTypeId?: number;
    SiteTypeDesc?: string;
    SiteClassificationId?: number;
    SiteClassificationDesc?: string;
    SiteBarcode?: string;
    SiteName?: string;
    SiteDescription?: string;
}

/**
 *数据类型
 */
export declare class DataType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *可使用的符号
     */
    Signs?: Array<Sign>;
}

/**
 *符号实体
 */
export declare class Sign extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *可用此符号的数据类型
     */
    DataTypes?: Array<DataType>;
    /**
     *可用此符号的限度记号
     */
    LimitTokens?: Array<LimitToken>;
}

/**
 *DataType审计 自动生成，请勿修改
 */
export declare class DataTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *设备配置
 */
export declare class DeviceConfig extends BizEntity {
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *官方配置
     */
    OfficialConfig?: string;
    /**
     *附加配置
     */
    AdditionalConfig?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *DeviceConfig审计 自动生成，请勿修改
 */
export declare class DeviceConfigAudit extends BizEntity {
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *官方配置
     */
    OfficialConfig?: string;
    /**
     *附加配置
     */
    AdditionalConfig?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
     *序列号
     */
    SerialNumber?: string;
    /**
     *最后一次同步数据时间戳
     */
    LastSyncDataTimestamp?: Date;
    /**
     *校准日期
     */
    CalibrationDate?: Date;
    /**
     *校准数值
     */
    CalibrationValue?: number;
    /**
     *下次校准日期
     */
    NextCalibrationDate?: Date;
    /**
     *设备条码
     */
    Barcode?: string;
    /**
     *控制编号
     */
    ControlNumber?: string;
    /**
     *设备校准人
     */
    PerformedBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *设备类型Id
     */
    EquipmentTypeId?: number;
    /**
     *设备单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *设备配置Id
     */
    DeviceConfigId?: number;
    /**
     *区域
     */
    Location?: Location;
    /**
     *设备类型
     */
    EquipmentType?: EquipmentType;
    /**
     *设备单位
     */
    UnitOfMeasure?: UnitOfMeasure;
    /**
     *设备单位
     */
    DeviceConfig?: DeviceConfig;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    /**
     *级别
     */
    Classification?: Classification;
    /**
     *第二级别
     */
    SecondClassification?: Classification;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *测量单位
 */
export declare class UnitOfMeasure extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Equipment审计 自动生成，请勿修改
 */
export declare class EquipmentAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *序列号
     */
    SerialNumber?: string;
    /**
     *最后一次同步数据时间戳
     */
    LastSyncDataTimestamp?: Date;
    /**
     *校准日期
     */
    CalibrationDate?: Date;
    /**
     *校准数值
     */
    CalibrationValue?: number;
    /**
     *下次校准日期
     */
    NextCalibrationDate?: Date;
    /**
     *设备条码
     */
    Barcode?: string;
    /**
     *控制编号
     */
    ControlNumber?: string;
    /**
     *设备校准人
     */
    PerformedBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *设备类型Id
     */
    EquipmentTypeId?: number;
    /**
     *设备单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *设备配置Id
     */
    DeviceConfigId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *EquipmentType审计 自动生成，请勿修改
 */
export declare class EquipmentTypeAudit extends BizEntity {
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
    CalibrationDate?: Date;
    /**
     *校准数值
     */
    CalibrationValue?: number;
    /**
     *下次校准日期
     */
    NextCalibrationDate?: Date;
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
    CreatedTime?: Date;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *限度记号
 */
export declare class LimitToken extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *相关实体
     */
    RelatedEntity?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *可使用的符号
     */
    Signs?: Array<Sign>;
}

/**
 *LimitToken审计 自动生成，请勿修改
 */
export declare class LimitTokenAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *相关实体
     */
    RelatedEntity?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *限度类型
 */
export declare class LimitType extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *颜色
     */
    Color?: string;
    /**
     *权重
     */
    Weight?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *LimitType审计 自动生成，请勿修改
 */
export declare class LimitTypeAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *颜色
     */
    Color?: string;
    /**
     *权重
     */
    Weight?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *级别实体
 */
export declare class Classification extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Classification审计 自动生成，请勿修改
 */
export declare class ClassificationAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *级别Id
     */
    ClassificationId?: number;
    /**
     *第二级别Id
     */
    SecondClassificationId?: number;
    /**
     *采样点类型Id
     */
    SiteTypeId?: number;
    /**
     *区域
     */
    Location?: Location;
    /**
     *级别
     */
    Classification?: Classification;
    /**
     *第二级别
     */
    SecondClassification?: Classification;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *级别Id
     */
    ClassificationId?: number;
    /**
     *第二级别Id
     */
    SecondClassificationId?: number;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *促生长实验状态
 */
export declare class GrowthPromotionStatus extends BizEntity {
    /**
     *状态代码
     */
    StatusCode?: string;
    /**
     *状态名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *GrowthPromotionStatus审计 自动生成，请勿修改
 */
export declare class GrowthPromotionStatusAudit extends BizEntity {
    /**
     *状态代码
     */
    StatusCode?: string;
    /**
     *状态名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *培养基实体
 */
export declare class Media extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *供应商
     */
    Vendor?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *批号
     */
    LotNumber?: string;
    /**
     *有效期
     */
    LotNumberExpDate?: Date;
    /**
     *供应商批号
     */
    ManufacturerLotNumber?: string;
    /**
     *促生长实验结果
     */
    GrowthPromotionResult?: string;
    /**
     *库存
     */
    Inventory?: number;
    /**
     *库存调整
     */
    InventoryAdj?: number;
    /**
     *是否启用通知
     */
    Notified?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *试剂耗材类型Id
     */
    MediaTypeId?: number;
    /**
     *促生长实验状态Id
     */
    GrowthPromotionStatusId?: number;
    /**
     *区域
     */
    Location?: Location;
    /**
     *试剂耗材类型
     */
    MediaType?: MediaType;
    /**
     *促生长实验状态
     */
    GrowthPromotionStatus?: GrowthPromotionStatus;
}

/**
 *培养基类别
 */
export declare class MediaType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *产品编号
     */
    ProductNumber?: string;
    /**
     *库存管理
     */
    InventoryControl?: boolean;
    /**
     *库存警告百分比
     */
    NotificationPercentage?: number;
    /**
     *产品编号正则表达式
     */
    ProductNumRegex?: string;
    /**
     *产品编号正则表达式组，逗号分隔
     */
    ProductNumGroup?: string;
    /**
     *培养基编号正则表达式
     */
    MediaNumRegex?: string;
    /**
     *培养基编号正则表达式组，逗号分隔
     */
    MediaNumGroup?: string;
    /**
     *批号正则表达式
     */
    LotNumRegex?: string;
    /**
     *批号正则表达式组，逗号分隔
     */
    LotNumGroup?: string;
    /**
     *过期日期正则表达式
     */
    ExpDateRegex?: string;
    /**
     *过期日期正则表达式组，逗号分隔
     */
    ExpDateGroup?: string;
    /**
     *过期日期格式
     */
    ExpDateFormat?: string;
    /**
     *条码正则表达式
     */
    BarcodeRegex?: string;
    /**
     *条码正则表达式组，逗号分隔
     */
    BarcodeGroup?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Media审计 自动生成，请勿修改
 */
export declare class MediaAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *供应商
     */
    Vendor?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *批号
     */
    LotNumber?: string;
    /**
     *有效期
     */
    LotNumberExpDate?: Date;
    /**
     *供应商批号
     */
    ManufacturerLotNumber?: string;
    /**
     *促生长实验结果
     */
    GrowthPromotionResult?: string;
    /**
     *库存
     */
    Inventory?: number;
    /**
     *库存调整
     */
    InventoryAdj?: number;
    /**
     *是否启用通知
     */
    Notified?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *试剂耗材类型Id
     */
    MediaTypeId?: number;
    /**
     *促生长实验状态Id
     */
    GrowthPromotionStatusId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MediaType审计 自动生成，请勿修改
 */
export declare class MediaTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *产品编号
     */
    ProductNumber?: string;
    /**
     *库存管理
     */
    InventoryControl?: boolean;
    /**
     *库存警告百分比
     */
    NotificationPercentage?: number;
    /**
     *产品编号正则表达式
     */
    ProductNumRegex?: string;
    /**
     *产品编号正则表达式组，逗号分隔
     */
    ProductNumGroup?: string;
    /**
     *培养基编号正则表达式
     */
    MediaNumRegex?: string;
    /**
     *培养基编号正则表达式组，逗号分隔
     */
    MediaNumGroup?: string;
    /**
     *批号正则表达式
     */
    LotNumRegex?: string;
    /**
     *批号正则表达式组，逗号分隔
     */
    LotNumGroup?: string;
    /**
     *过期日期正则表达式
     */
    ExpDateRegex?: string;
    /**
     *过期日期正则表达式组，逗号分隔
     */
    ExpDateGroup?: string;
    /**
     *过期日期格式
     */
    ExpDateFormat?: string;
    /**
     *条码正则表达式
     */
    BarcodeRegex?: string;
    /**
     *条码正则表达式组，逗号分隔
     */
    BarcodeGroup?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *培养基视图
 */
export declare class MediaV extends BizView {
    /**
     *培养基Id
     */
    MediaId?: number;
    /**
     *名称
     */
    Name?: string;
    /**
     *试剂耗材类型Id
     */
    MediaTypeId?: number;
    /**
     *试剂耗材类型名称
     */
    MediaTypeName?: string;
    /**
     *试剂耗材类型描述
     */
    MediaTypeDescription?: string;
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
     *供应商
     */
    Vendor?: string;
    /**
     *条码
     */
    Barcode?: string;
    /**
     *批号
     */
    LotNumber?: string;
    /**
     *有效期
     */
    LotNumberExpDate?: Date;
    /**
     *供应商批号
     */
    ManufacturerLotNumber?: string;
    /**
     *促生长实验状态Id
     */
    GrowthPromotionStatusId?: number;
    /**
     *促生长实验状态名称
     */
    GrowthPromotionStatusName?: string;
    /**
     *促生长实验结果
     */
    GrowthPromotionResult?: string;
    /**
     *库存
     */
    Inventory?: number;
    /**
     *库存调整
     */
    InventoryAdj?: number;
    /**
     *是否启用通知
     */
    Notified?: boolean;
    /**
     *库存管理
     */
    InventoryControl?: boolean;
    /**
     *库存警告百分比
     */
    NotificationPercentage?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
}

/**
 *微生物菌属实体
 */
export declare class Organism extends BizEntity {
    /**
     *菌种名称
     */
    Species?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *菌株品系
     */
    Strain?: string;
    /**
     *革兰氏阳性
     */
    IsGramStrain?: boolean;
    /**
     *图片路径
     */
    PicturePath?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物菌属Id
     */
    OrganismGenusId?: number;
    /**
     *微生物致病性Id
     */
    OrganismSeverityId?: number;
    /**
     *微生物菌属
     */
    OrganismGenus?: OrganismGenus;
    /**
     *微生物致病性
     */
    OrganismSeverity?: OrganismSeverity;
    /**
     *致病性区域
     */
    SeverityLocations?: Array<SeverityLocation>;
}

/**
 *微生物菌属实体
 */
export declare class OrganismGenus extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *是否由API创建
     */
    IsApiCreated?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物类型Id
     */
    OrganismTypeId?: number;
    /**
     *微生物类型
     */
    OrganismType?: OrganismType;
}

/**
 *微生物致病性实体
 */
export declare class OrganismSeverity extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *致病性区域实体
 */
export declare class SeverityLocation extends BizEntity {
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物致病性Id
     */
    OrganismSeverityId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *微生物Id
     */
    OrganismId?: number;
    /**
     *微生物致病性
     */
    OrganismSeverity?: OrganismSeverity;
    /**
     *区域
     */
    Location?: Location;
    /**
     *微生物
     */
    Organism?: Organism;
}

/**
 *Organism审计 自动生成，请勿修改
 */
export declare class OrganismAudit extends BizEntity {
    /**
     *菌种名称
     */
    Species?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *菌株品系
     */
    Strain?: string;
    /**
     *革兰氏阳性
     */
    IsGramStrain?: boolean;
    /**
     *图片路径
     */
    PicturePath?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物菌属Id
     */
    OrganismGenusId?: number;
    /**
     *微生物致病性Id
     */
    OrganismSeverityId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *微生物结构表征实体
 */
export declare class OrganismChar extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否由API创建
     */
    IsApiCreated?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物类型Id
     */
    OrganismTypeId?: number;
    /**
     *微生物类型
     */
    OrganismType?: OrganismType;
}

/**
 *微生物类别实体
 */
export declare class OrganismType extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *OrganismChar审计 自动生成，请勿修改
 */
export declare class OrganismCharAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否由API创建
     */
    IsApiCreated?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物类型Id
     */
    OrganismTypeId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *OrganismGenus审计 自动生成，请勿修改
 */
export declare class OrganismGenusAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *是否由API创建
     */
    IsApiCreated?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物类型Id
     */
    OrganismTypeId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *OrganismSeverity审计 自动生成，请勿修改
 */
export declare class OrganismSeverityAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *OrganismType审计 自动生成，请勿修改
 */
export declare class OrganismTypeAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *微生物发现方法实体
 */
export declare class OrgFoundMethod extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *OrgFoundMethod审计 自动生成，请勿修改
 */
export declare class OrgFoundMethodAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *SeverityLocation审计 自动生成，请勿修改
 */
export declare class SeverityLocationAudit extends BizEntity {
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *微生物致病性Id
     */
    OrganismSeverityId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *微生物Id
     */
    OrganismId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *Note定义
 */
export declare class PreDefinedNote extends BizEntity {
    /**
     *Note添加信息
     */
    Message?: string;
    /**
     *Note 显示标题
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *PreDefinedNote审计 自动生成，请勿修改
 */
export declare class PreDefinedNoteAudit extends BizEntity {
    /**
     *Note添加信息
     */
    Message?: string;
    /**
     *Note 显示标题
     */
    Name?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
     *是否在监管之下
     */
    ChainOfCustody?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *区域
     */
    Location?: Location;
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
     *是否在监管之下
     */
    ChainOfCustody?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *Sign审计 自动生成，请勿修改
 */
export declare class SignAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测量法周期
 */
export declare class Cycle extends BizEntity {
    /**
     *读取记录周期数
     */
    CycleNumber?: number;
    /**
     *测试步骤Id
     */
    TimeFrameId?: number;
    /**
     *包括周期名称
     */
    IncludeCycleName?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *检查限度
     */
    CheckLimits?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试步骤
     */
    TimeFrame?: TimeFrame;
}

/**
 *测试阶段实体             定义测试方法的工作流阶段以及工作流阶段的规则。
 */
export declare class TimeFrame extends BizEntity {
    /**
     *从此阶段克隆
     */
    ClonedTimeFrameId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *测试阶段Id
     */
    TestStageId?: number;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *提示信息
     */
    PromptMsg?: string;
    /**
     *最短时间
     */
    MinTime?: string;
    /**
     *最长时间
     */
    MaxTime?: string;
    /**
     *workflow框架 使用
     */
    StepId?: string;
    /**
     *执行时间
     */
    ExecuteTime?: ExecuteTime;
    /**
     *下一阶段分配方式
     */
    NextAllocation?: NextAllocation;
    /**
     *人工录入结果
     */
    InputByManual?: boolean;
    /**
     *对接设备数据录入结果
     */
    InputByEquipment?: boolean;
    /**
     *最少周期
     */
    MinCycles?: number;
    /**
     *最大周期
     */
    MaxCycles?: number;
    /**
     *电子签名
     */
    ESign?: boolean;
    /**
     *电子签名验证
     */
    ESignVerification?: boolean;
    /**
     *代理操作
     */
    ProxyOperation?: boolean;
    /**
     *添加菌种
     */
    AddOrganism?: boolean;
    /**
     *上传文件
     */
    UpLoadDocuments?: boolean;
    /**
     *批量操作
     */
    BulkOperation?: boolean;
    /**
     *打印标签
     */
    PrintLabels?: boolean;
    /**
     *对接设备数据
     */
    DockingDeviceData?: boolean;
    /**
     *使用之前的结果
     */
    FrontResult?: boolean;
    /**
     *验证之前步骤
     */
    VerifyPrevious?: boolean;
    /**
     *样本开始时间 是否选择 不可看 null 可编辑（true） 或仅查看（false）
     */
    SampleStartDateCanEdit?: boolean;
    /**
     *样本结束时间
     */
    SampleEndDateCanEdit?: boolean;
    /**
     *执行者
     */
    ExecuteUserCanEdit?: boolean;
    /**
     *环境
     */
    EnvironmentCanEdit?: boolean;
    /**
     *产品
     */
    Product?: boolean;
    /**
     *培养开始时间
     */
    IncubationStartDateCanEdit?: boolean;
    /**
     *培养结束时间
     */
    IncubationEndDateCanEdit?: boolean;
    /**
     *校准最短时间
     */
    MinCalAlignment?: AlignTime;
    /**
     *校准最长时间
     */
    MaxCalAlignment?: AlignTime;
    /**
     *自动分配下个步骤
     */
    AutoAssignNext?: boolean;
    /**
     *用在个人采样
     */
    ShowPersonnelPanel?: boolean;
    /**
     *需要开始日期
     */
    RequireStartDate?: boolean;
    /**
     *需要结束日期
     */
    RequireEndDate?: boolean;
    /**
     *自动开始日期
     */
    AutoStartDate?: boolean;
    /**
     *自动结束日期
     */
    AutoEndDate?: boolean;
    /**
     *锁定日期
     */
    LockStartDate?: boolean;
    /**
     *需要人员
     */
    RequirePerformedUser?: boolean;
    /**
     *显示记录数据
     */
    ShowReadings?: boolean;
    /**
     *显示微生物鉴定结果
     */
    ShowOrgid?: boolean;
    /**
     *使用之前结果
     */
    UsePreviousResult?: boolean;
    /**
     *显示配件控制
     */
    ShowDeviceControl?: boolean;
    /**
     *显示样本培养基
     */
    ShowSampleMedia?: boolean;
    /**
     *显示样本时间
     */
    ShowSampleTimes?: boolean;
    /**
     *显示培养时间
     */
    ShowIncubationTimes?: boolean;
    /**
     *显示添加周期
     */
    ShowAddCycle?: boolean;
    /**
     *显示环境
     */
    ShowEnvironment?: boolean;
    /**
     *显示匹配结果的记录数据
     */
    MatchingResultsOnly?: boolean;
    /**
     *执行平台
     */
    PlatformType?: PlatformType;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *从此阶段克隆
     */
    ClonedTimeFrame?: TimeFrame;
    /**
     *测试方法
     */
    TestType?: TestType;
    /**
     *测试阶段
     */
    TestStage?: TestStage;
    /**
     *测量法周期
     */
    Cycles?: Array<Cycle>;
    /**
     *测量法
     */
    Measurements?: Array<Measurement>;
    /**
     *所需设备
     */
    TestTypeEquipments?: Array<TestTypeEquipment>;
    /**
     *所需培养基
     */
    TestTypeMedias?: Array<TestTypeMedia>;
    /**
     *稀释
     */
    TimeFrameDilutions?: Array<TimeFrameDilution>;
    /**
     *事件
     */
    TimeFrameEvents?: Array<TimeFrameEvent>;
}

/**
 *Cycle审计 自动生成，请勿修改
 */
export declare class CycleAudit extends BizEntity {
    /**
     *读取记录周期数
     */
    CycleNumber?: number;
    /**
     *测试步骤Id
     */
    TimeFrameId?: number;
    /**
     *包括周期名称
     */
    IncludeCycleName?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *检查限度
     */
    CheckLimits?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *环境定义
 */
export declare class EnvironmentDef extends BizEntity {
    /**
     *环境名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *环境代码
     */
    Code?: string;
    /**
     *环境变化发生的顺序
     */
    Sequence?: number;
    /**
     *是否为默认
     */
    IsDefault?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *EnvironmentDef审计 自动生成，请勿修改
 */
export declare class EnvironmentDefAudit extends BizEntity {
    /**
     *环境名称
     */
    Name?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *环境代码
     */
    Code?: string;
    /**
     *环境变化发生的顺序
     */
    Sequence?: number;
    /**
     *是否为默认
     */
    IsDefault?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试方法限度
 */
export declare class LimitDef extends BizEntity {
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *普遍程度
     */
    Prevalence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否为频率限度
     */
    FreqLimit?: boolean;
    /**
     *源限度类型Id
     */
    SourceLimitTypeId?: number;
    /**
     *源限度Id
     */
    SourceLimitDefId?: number;
    /**
     *发生次数
     */
    OccurrenceCount?: number;
    /**
     *时段
     */
    Period?: LimitPeriod;
    /**
     *定期总数
     */
    PeriodCount?: number;
    /**
     *偏差
     */
    Deviation?: boolean;
    /**
     *邮件通知
     */
    EmailNotify?: boolean;
    /**
     *屏幕通知
     */
    ScreenNotify?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *重新计划数量
     */
    RescheduleCount?: number;
    /**
     *重新计划偏差
     */
    RescheduleOffset?: string;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *是否每周期执行
     */
    ExecutePerCycle?: boolean;
    /**
     *报告数字算子
     */
    ReportableOperator?: string;
    /**
     *报告数值
     */
    ReportableValue?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *限度类型
     */
    LimitType?: LimitType;
    /**
     *测试方法
     */
    TestType?: TestType;
    /**
     *源限度类型
     */
    SourceLimitType?: LimitType;
    /**
     *源限度
     */
    SourceLimitDef?: LimitDef;
    /**
     *限制规则组 web 横向是与 纵向是或
     */
    LimitRuleGroups?: Array<LimitRuleGroup>;
}

/**
 *测试方法实体
 */
export declare class TestType extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *测试种类Id
     */
    TestCategoryId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *代码Id
     */
    TestTypeCodeId?: number;
    /**
     *设置对照组
     */
    NegativeControl?: boolean;
    /**
     *系统是否以来此记录
     */
    SystemRecord?: boolean;
    /**
     *价格
     */
    Price?: number;
    /**
     *需要选择产品
     */
    RequireProductSelection?: boolean;
    /**
     *选择产品的测试阶段Id
     */
    ProdSelectionTimeFrameId?: number;
    /**
     *标签代码Id
     */
    TestTypeLabelId?: number;
    /**
     *需要批准
     */
    NeedApprove?: boolean;
    /**
     *批准(人数)次数
     */
    ApproveCount?: number;
    /**
     *需要复核
     */
    NeedReview?: boolean;
    /**
     *复核(人数)次数
     */
    ReviewCount?: number;
    /**
     *引擎版本
     */
    EngineVerion?: number;
    /**
     *流程图关联
     */
    WorkflowTemplateId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试种类
     */
    TestCategory?: TestCategory;
    /**
     *区域
     */
    Location?: Location;
    /**
     *代码
     */
    TestTypeCode?: TestTypeCode;
    /**
     *选择产品的测试阶段
     */
    ProdSelectionTimeFrame?: TimeFrame;
    /**
     *标签代码
     */
    TestTypeLabel?: TestTypeLabel;
    /**
     *测试步骤
     */
    TimeFrames?: Array<TimeFrame>;
    /**
     *限度
     */
    LimitDefs?: Array<LimitDef>;
    /**
     *人员监测位置
     */
    PersonnelSites?: Array<PersonnelSite>;
    /**
     *检测数据
     */
    Measurements?: Array<Measurement>;
    /**
     *稀释实体
     */
    TimeFrameDilutions?: Array<TimeFrameDilution>;
}

/**
 *限度规则组
 */
export declare class LimitRuleGroup extends BizEntity {
    /**
     *限度Id
     */
    LimitDefId?: number;
    /**
     *限制Id
     */
    LimitId?: number;
    /**
     *名称
     */
    Description?: string;
    /**
     *父级Id
     */
    ParentId?: number;
    /**
     *逻辑符号
     */
    LogicSymbol?: LogicSymbol;
    /**
     *顺序，与规则混排
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *限度
     */
    LimitDef?: LimitDef;
    /**
     *限度规则
     */
    LimitRules?: Array<LimitRule>;
    /**
     *组
     */
    LimitRuleGroups?: Array<LimitRuleGroup>;
    /**
     *父级
     */
    Parent?: LimitRuleGroup;
}

/**
 *LimitDef审计 自动生成，请勿修改
 */
export declare class LimitDefAudit extends BizEntity {
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *普遍程度
     */
    Prevalence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否为频率限度
     */
    FreqLimit?: boolean;
    /**
     *源限度类型Id
     */
    SourceLimitTypeId?: number;
    /**
     *源限度Id
     */
    SourceLimitDefId?: number;
    /**
     *发生次数
     */
    OccurrenceCount?: number;
    /**
     *时段
     */
    Period?: LimitPeriod;
    /**
     *定期总数
     */
    PeriodCount?: number;
    /**
     *偏差
     */
    Deviation?: boolean;
    /**
     *邮件通知
     */
    EmailNotify?: boolean;
    /**
     *屏幕通知
     */
    ScreenNotify?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *重新计划数量
     */
    RescheduleCount?: number;
    /**
     *重新计划偏差
     */
    RescheduleOffset?: string;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *是否每周期执行
     */
    ExecutePerCycle?: boolean;
    /**
     *报告数字算子
     */
    ReportableOperator?: string;
    /**
     *报告数值
     */
    ReportableValue?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *限度规则
 */
export declare class LimitRule extends BizEntity {
    /**
     *名称
     */
    Description?: string;
    /**
     *限度规则组Id
     */
    LimitRuleGroupId?: number;
    /**
     *限度记号Id
     */
    LimitTokenId?: number;
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *符号Id
     */
    SignAndId?: number;
    /**
     *限制值
     */
    LimitValue?: string;
    /**
     *顺序，与规则组混排
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *限度规则组
     */
    LimitRuleGroup?: LimitRuleGroup;
    /**
     *限度记号
     */
    LimitToken?: LimitToken;
    /**
     *测量法
     */
    Measurement?: Measurement;
    /**
     *符号
     */
    Sign?: Sign;
    /**
     *符号
     */
    SignAnd?: Sign;
}

/**
 *测量法实体
 */
export declare class Measurement extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *数据类型Id
     */
    DataTypeId?: number;
    /**
     *颗粒大小
     */
    ParticleSize?: number;
    /**
     *默认数值
     */
    DefaultValue?: string;
    /**
     *最小数值
     */
    MinValue?: number;
    /**
     *最大数值
     */
    MaxValue?: number;
    /**
     *公式
     */
    Formula?: string;
    /**
     *在结果中使用
     */
    Result?: boolean;
    /**
     *默认打开
     */
    AlwaysDefault?: boolean;
    /**
     *从公式默认
     */
    DefaultFromFormula?: boolean;
    /**
     *子样本测量Id
     */
    ChildMeasurementId?: number;
    /**
     *格式
     */
    Format?: string;
    /**
     *稀释配置Id
     */
    TimeFrameDilutionId?: number;
    /**
     *小数位数
     */
    DecimalLength?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试方法外键
     */
    TestTypeId?: number;
    /**
     *测试阶段
     */
    TimeFrame?: TimeFrame;
    /**
     *数据类型
     */
    DataType?: DataType;
    /**
     *子样本测量
     */
    ChildMeasurement?: Measurement;
    /**
     *稀释配置
     */
    TimeFrameDilution?: TimeFrameDilution;
    /**
     *清单定义
     */
    MeasurementListDefs?: Array<MeasurementListDef>;
    /**
     *测量符号
     */
    MeasurementSigns?: Array<MeasurementSign>;
    /**
     *单位
     */
    MeasurementUOMs?: Array<MeasurementUOM>;
    /**
     *周期
     */
    MeasurementCycles?: Array<MeasurementCycle>;
    /**
     *测试方法
     */
    TestType?: TestType;
}

/**
 *LimitRule审计 自动生成，请勿修改
 */
export declare class LimitRuleAudit extends BizEntity {
    /**
     *名称
     */
    Description?: string;
    /**
     *限度规则组Id
     */
    LimitRuleGroupId?: number;
    /**
     *限度记号Id
     */
    LimitTokenId?: number;
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *符号Id
     */
    SignAndId?: number;
    /**
     *限制值
     */
    LimitValue?: string;
    /**
     *顺序，与规则组混排
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *LimitRuleGroup审计 自动生成，请勿修改
 */
export declare class LimitRuleGroupAudit extends BizEntity {
    /**
     *限度Id
     */
    LimitDefId?: number;
    /**
     *限制Id
     */
    LimitId?: number;
    /**
     *名称
     */
    Description?: string;
    /**
     *父级Id
     */
    ParentId?: number;
    /**
     *逻辑符号
     */
    LogicSymbol?: LogicSymbol;
    /**
     *顺序，与规则混排
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *稀释实体
 */
export declare class TimeFrameDilution extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *显示文本
     */
    DisplayText?: string;
    /**
     *稀释数值
     */
    ListValue?: number;
    /**
     *测量单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *选择
     */
    Selected?: boolean;
    /**
     *自动填充
     */
    AutoFill?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试阶段
     */
    TimeFrame?: TimeFrame;
    /**
     *测试方法
     */
    TestType?: TestType;
    /**
     *测量单位
     */
    UnitOfMeasure?: UnitOfMeasure;
}

/**
 *测量法清单定义
 */
export declare class MeasurementListDef extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *显示字母
     */
    DisplayText?: string;
    /**
     *操作符号
     */
    Operation?: string;
    /**
     *数值比较1
     */
    Value1?: string;
    /**
     *数值比较2
     */
    Value2?: string;
    /**
     *选择
     */
    Selected?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测量法
     */
    Measurement?: Measurement;
}

/**
 *测量法符号
 */
export declare class MeasurementSign extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测量法
     */
    Measurement?: Measurement;
    /**
     *符号
     */
    Sign?: Sign;
}

/**
 *测量法的测量单位
 */
export declare class MeasurementUOM extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *测量单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测量法
     */
    Measurement?: Measurement;
    /**
     *测量单位
     */
    UnitOfMeasure?: UnitOfMeasure;
}

/**
 *测量法与测量法周期关联
 */
export declare class MeasurementCycle extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *测量法周期Id
     */
    CycleId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测量法
     */
    Measurement?: Measurement;
    /**
     *测量法周期
     */
    Cycle?: Cycle;
}

/**
 *Measurement审计 自动生成，请勿修改
 */
export declare class MeasurementAudit extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *数据类型Id
     */
    DataTypeId?: number;
    /**
     *颗粒大小
     */
    ParticleSize?: number;
    /**
     *默认数值
     */
    DefaultValue?: string;
    /**
     *最小数值
     */
    MinValue?: number;
    /**
     *最大数值
     */
    MaxValue?: number;
    /**
     *公式
     */
    Formula?: string;
    /**
     *在结果中使用
     */
    Result?: boolean;
    /**
     *默认打开
     */
    AlwaysDefault?: boolean;
    /**
     *从公式默认
     */
    DefaultFromFormula?: boolean;
    /**
     *子样本测量Id
     */
    ChildMeasurementId?: number;
    /**
     *格式
     */
    Format?: string;
    /**
     *稀释配置Id
     */
    TimeFrameDilutionId?: number;
    /**
     *小数位数
     */
    DecimalLength?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试方法外键
     */
    TestTypeId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MeasurementCycle审计 自动生成，请勿修改
 */
export declare class MeasurementCycleAudit extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *测量法周期Id
     */
    CycleId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MeasurementListDef审计 自动生成，请勿修改
 */
export declare class MeasurementListDefAudit extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *显示字母
     */
    DisplayText?: string;
    /**
     *操作符号
     */
    Operation?: string;
    /**
     *数值比较1
     */
    Value1?: string;
    /**
     *数值比较2
     */
    Value2?: string;
    /**
     *选择
     */
    Selected?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MeasurementSign审计 自动生成，请勿修改
 */
export declare class MeasurementSignAudit extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *MeasurementUOM审计 自动生成，请勿修改
 */
export declare class MeasurementUOMAudit extends BizEntity {
    /**
     *测量法Id
     */
    MeasurementId?: number;
    /**
     *测量单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *人员监测位置
 */
export declare class PersonnelSite extends BizEntity {
    /**
     *代码
     */
    Code?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试方法
     */
    TestType?: TestType;
}

/**
 *PersonnelSite审计 自动生成，请勿修改
 */
export declare class PersonnelSiteAudit extends BizEntity {
    /**
     *代码
     */
    Code?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试种类实体
 */
export declare class TestCategory extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *测试分类Id
     */
    TestClassId?: number;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试分类
     */
    TestClass?: TestClass;
}

/**
 *测试分类实体
 */
export declare class TestClass extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *TestCategory审计 自动生成，请勿修改
 */
export declare class TestCategoryAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *测试分类Id
     */
    TestClassId?: number;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestClass审计 自动生成，请勿修改
 */
export declare class TestClassAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试阶段实体             定义可配置用于定义给定测试方法生命周期的工作流阶段。
 */
export declare class TestStage extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *电子签名
     */
    ESign?: boolean;
    /**
     *电子签名验证
     */
    ESignVerification?: boolean;
    /**
     *自动分配下个步骤
     */
    AutoAssignNext?: boolean;
    /**
     *用在个人采样
     */
    ShowPersonnelPanel?: boolean;
    /**
     *需要开始日期
     */
    RequireStartDate?: boolean;
    /**
     *需要结束日期
     */
    RequireEndDate?: boolean;
    /**
     *自动开始日期
     */
    AutoStartDate?: boolean;
    /**
     *自动结束日期
     */
    AutoEndDate?: boolean;
    /**
     *锁定日期
     */
    LockStartDate?: boolean;
    /**
     *需要人员
     */
    RequirePerformedUser?: boolean;
    /**
     *打印标签
     */
    PrintLabels?: boolean;
    /**
     *显示记录数据
     */
    ShowReadings?: boolean;
    /**
     *显示微生物鉴定结果
     */
    ShowOrgid?: boolean;
    /**
     *使用之前结果
     */
    UsePreviousResult?: boolean;
    /**
     *验证之前步骤
     */
    VerifyPrevious?: boolean;
    /**
     *显示配件控制
     */
    ShowDeviceControl?: boolean;
    /**
     *显示样本培养基
     */
    ShowSampleMedia?: boolean;
    /**
     *显示样本时间
     */
    ShowSampleTimes?: boolean;
    /**
     *显示培养时间
     */
    ShowIncubationTimes?: boolean;
    /**
     *显示添加周期
     */
    ShowAddCycle?: boolean;
    /**
     *显示环境
     */
    ShowEnvironment?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *TestStage审计 自动生成，请勿修改
 */
export declare class TestStageAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *电子签名
     */
    ESign?: boolean;
    /**
     *电子签名验证
     */
    ESignVerification?: boolean;
    /**
     *自动分配下个步骤
     */
    AutoAssignNext?: boolean;
    /**
     *用在个人采样
     */
    ShowPersonnelPanel?: boolean;
    /**
     *需要开始日期
     */
    RequireStartDate?: boolean;
    /**
     *需要结束日期
     */
    RequireEndDate?: boolean;
    /**
     *自动开始日期
     */
    AutoStartDate?: boolean;
    /**
     *自动结束日期
     */
    AutoEndDate?: boolean;
    /**
     *锁定日期
     */
    LockStartDate?: boolean;
    /**
     *需要人员
     */
    RequirePerformedUser?: boolean;
    /**
     *打印标签
     */
    PrintLabels?: boolean;
    /**
     *显示记录数据
     */
    ShowReadings?: boolean;
    /**
     *显示微生物鉴定结果
     */
    ShowOrgid?: boolean;
    /**
     *使用之前结果
     */
    UsePreviousResult?: boolean;
    /**
     *验证之前步骤
     */
    VerifyPrevious?: boolean;
    /**
     *显示配件控制
     */
    ShowDeviceControl?: boolean;
    /**
     *显示样本培养基
     */
    ShowSampleMedia?: boolean;
    /**
     *显示样本时间
     */
    ShowSampleTimes?: boolean;
    /**
     *显示培养时间
     */
    ShowIncubationTimes?: boolean;
    /**
     *显示添加周期
     */
    ShowAddCycle?: boolean;
    /**
     *显示环境
     */
    ShowEnvironment?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试方法代码
 */
export declare class TestTypeCode extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *测试方法标签
 */
export declare class TestTypeLabel extends BizEntity {
    /**
     *标签代码
     */
    Code?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *TestType审计 自动生成，请勿修改
 */
export declare class TestTypeAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *测试种类Id
     */
    TestCategoryId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *代码Id
     */
    TestTypeCodeId?: number;
    /**
     *设置对照组
     */
    NegativeControl?: boolean;
    /**
     *系统是否以来此记录
     */
    SystemRecord?: boolean;
    /**
     *价格
     */
    Price?: number;
    /**
     *需要选择产品
     */
    RequireProductSelection?: boolean;
    /**
     *选择产品的测试阶段Id
     */
    ProdSelectionTimeFrameId?: number;
    /**
     *标签代码Id
     */
    TestTypeLabelId?: number;
    /**
     *需要批准
     */
    NeedApprove?: boolean;
    /**
     *批准(人数)次数
     */
    ApproveCount?: number;
    /**
     *需要复核
     */
    NeedReview?: boolean;
    /**
     *复核(人数)次数
     */
    ReviewCount?: number;
    /**
     *引擎版本
     */
    EngineVerion?: number;
    /**
     *流程图关联
     */
    WorkflowTemplateId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestTypeCode审计 自动生成，请勿修改
 */
export declare class TestTypeCodeAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *名称
     */
    Name?: string;
    /**
     *代码
     */
    Code?: string;
    /**
     *图标
     */
    Icon?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试阶段所需设备
 */
export declare class TestTypeEquipment extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *设备类别Id
     */
    EquipmentTypeId?: number;
    /**
     *需要的数量
     */
    RequiredNum?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试阶段
     */
    TimeFrame?: TimeFrame;
    /**
     *设备类别
     */
    EquipmentType?: EquipmentType;
}

/**
 *TestTypeEquipment审计 自动生成，请勿修改
 */
export declare class TestTypeEquipmentAudit extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *设备类别Id
     */
    EquipmentTypeId?: number;
    /**
     *需要的数量
     */
    RequiredNum?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestTypeLabel审计 自动生成，请勿修改
 */
export declare class TestTypeLabelAudit extends BizEntity {
    /**
     *标签代码
     */
    Code?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试所需培养基
 */
export declare class TestTypeMedia extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *培养基类别Id
     */
    MediaTypeId?: number;
    /**
     *需要的数量
     */
    RequiredNum?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试阶段
     */
    TimeFrame?: TimeFrame;
    /**
     *培养基类别
     */
    MediaType?: MediaType;
}

/**
 *TestTypeMedia审计 自动生成，请勿修改
 */
export declare class TestTypeMediaAudit extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *培养基类别Id
     */
    MediaTypeId?: number;
    /**
     *需要的数量
     */
    RequiredNum?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

export declare class TestTypeV extends BizView {
    TestTypeId?: number;
    Description?: string;
    TestCategoryId?: number;
    TestTypeCodeId?: number;
    TestCategoryDesc?: string;
    TestClassId?: number;
    TestClassDesc?: string;
    Code?: string;
    LabelCode?: string;
    NegativeControl?: boolean;
    RequireProductSelection?: boolean;
    ProdSelectionTimeFrameId?: number;
    TimeFrameDescription?: string;
    LocationName?: string;
    LocationFullName?: string;
    LocationId?: number;
    Breadcrumb?: string;
    HasSubTestTypes?: boolean;
    IsSubTestType?: boolean;
    HasResultMeasurement?: boolean;
    HasShowOrgId?: boolean;
    Price?: number;
    SystemRecord?: boolean;
    CreateBy?: number;
    CreatedTime?: Date;
    UpdateBy?: number;
    UpdatedTime?: Date;
    IsActive?: boolean;
}

/**
 *测试阶段事件
 */
export declare class TimeFrameEvent extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *测试方法限度Id
     */
    LimitDefId?: number;
    /**
     *事件
     */
    EventType?: string;
    /**
     *行动
     */
    ActionType?: string;
    /**
     *跳转测试阶段Id
     */
    NextTimeFrameId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试阶段
     */
    TimeFrame?: TimeFrame;
    /**
     *跳转测试阶段
     */
    NextTimeFrame?: TimeFrame;
    /**
     *测试方法限度
     */
    LimitDef?: LimitDef;
}

/**
 *TimeFrame审计 自动生成，请勿修改
 */
export declare class TimeFrameAudit extends BizEntity {
    /**
     *从此阶段克隆
     */
    ClonedTimeFrameId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *测试阶段Id
     */
    TestStageId?: number;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *提示信息
     */
    PromptMsg?: string;
    /**
     *最短时间
     */
    MinTime?: string;
    /**
     *最长时间
     */
    MaxTime?: string;
    /**
     *workflow框架 使用
     */
    StepId?: string;
    /**
     *执行时间
     */
    ExecuteTime?: ExecuteTime;
    /**
     *下一阶段分配方式
     */
    NextAllocation?: NextAllocation;
    /**
     *人工录入结果
     */
    InputByManual?: boolean;
    /**
     *对接设备数据录入结果
     */
    InputByEquipment?: boolean;
    /**
     *最少周期
     */
    MinCycles?: number;
    /**
     *最大周期
     */
    MaxCycles?: number;
    /**
     *电子签名
     */
    ESign?: boolean;
    /**
     *电子签名验证
     */
    ESignVerification?: boolean;
    /**
     *代理操作
     */
    ProxyOperation?: boolean;
    /**
     *添加菌种
     */
    AddOrganism?: boolean;
    /**
     *上传文件
     */
    UpLoadDocuments?: boolean;
    /**
     *批量操作
     */
    BulkOperation?: boolean;
    /**
     *打印标签
     */
    PrintLabels?: boolean;
    /**
     *对接设备数据
     */
    DockingDeviceData?: boolean;
    /**
     *使用之前的结果
     */
    FrontResult?: boolean;
    /**
     *验证之前步骤
     */
    VerifyPrevious?: boolean;
    /**
     *样本开始时间 是否选择 不可看 null 可编辑（true） 或仅查看（false）
     */
    SampleStartDateCanEdit?: boolean;
    /**
     *样本结束时间
     */
    SampleEndDateCanEdit?: boolean;
    /**
     *执行者
     */
    ExecuteUserCanEdit?: boolean;
    /**
     *环境
     */
    EnvironmentCanEdit?: boolean;
    /**
     *产品
     */
    Product?: boolean;
    /**
     *培养开始时间
     */
    IncubationStartDateCanEdit?: boolean;
    /**
     *培养结束时间
     */
    IncubationEndDateCanEdit?: boolean;
    /**
     *校准最短时间
     */
    MinCalAlignment?: AlignTime;
    /**
     *校准最长时间
     */
    MaxCalAlignment?: AlignTime;
    /**
     *自动分配下个步骤
     */
    AutoAssignNext?: boolean;
    /**
     *用在个人采样
     */
    ShowPersonnelPanel?: boolean;
    /**
     *需要开始日期
     */
    RequireStartDate?: boolean;
    /**
     *需要结束日期
     */
    RequireEndDate?: boolean;
    /**
     *自动开始日期
     */
    AutoStartDate?: boolean;
    /**
     *自动结束日期
     */
    AutoEndDate?: boolean;
    /**
     *锁定日期
     */
    LockStartDate?: boolean;
    /**
     *需要人员
     */
    RequirePerformedUser?: boolean;
    /**
     *显示记录数据
     */
    ShowReadings?: boolean;
    /**
     *显示微生物鉴定结果
     */
    ShowOrgid?: boolean;
    /**
     *使用之前结果
     */
    UsePreviousResult?: boolean;
    /**
     *显示配件控制
     */
    ShowDeviceControl?: boolean;
    /**
     *显示样本培养基
     */
    ShowSampleMedia?: boolean;
    /**
     *显示样本时间
     */
    ShowSampleTimes?: boolean;
    /**
     *显示培养时间
     */
    ShowIncubationTimes?: boolean;
    /**
     *显示添加周期
     */
    ShowAddCycle?: boolean;
    /**
     *显示环境
     */
    ShowEnvironment?: boolean;
    /**
     *显示匹配结果的记录数据
     */
    MatchingResultsOnly?: boolean;
    /**
     *执行平台
     */
    PlatformType?: PlatformType;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TimeFrameDilution审计 自动生成，请勿修改
 */
export declare class TimeFrameDilutionAudit extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *测试方法Id
     */
    TestTypeId?: number;
    /**
     *序号
     */
    Sequence?: number;
    /**
     *显示文本
     */
    DisplayText?: string;
    /**
     *稀释数值
     */
    ListValue?: number;
    /**
     *测量单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *选择
     */
    Selected?: boolean;
    /**
     *自动填充
     */
    AutoFill?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TimeFrameEvent审计 自动生成，请勿修改
 */
export declare class TimeFrameEventAudit extends BizEntity {
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *测试方法限度Id
     */
    LimitDefId?: number;
    /**
     *事件
     */
    EventType?: string;
    /**
     *行动
     */
    ActionType?: string;
    /**
     *跳转测试阶段Id
     */
    NextTimeFrameId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *UnitOfMeasure审计 自动生成，请勿修改
 */
export declare class UnitOfMeasureAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

export declare class RptFrequencySummaryV extends BizView {
    TestId?: number;
    TestName?: string;
    TestDescription?: string;
    SampleBarcode?: string;
    SampleCreatedDate?: Date;
    EnvironmentId?: number;
    Environment?: string;
    Volume?: number;
    VolumeUom?: string;
    StartDate?: Date;
    EndDate?: Date;
    PerformedUsername?: string;
    SampleId?: number;
    Approved?: boolean;
    PersonnelUserId?: number;
    PersonnelSiteId?: number;
    NoTest?: boolean;
    SampleYear?: string;
    LocationId?: number;
    LocationName?: string;
    SiteId?: number;
    SiteClassificationId?: number;
    SiteClassificationDesc?: string;
    SystemId?: number;
    SystemName?: string;
    TestTypeId?: number;
    TestTypeDescription?: string;
    TestResult?: string;
    TestResultNumber?: number;
    Uom?: string;
    ParticleSize?: number;
    TestResultCreatedDate?: Date;
    DeviationId?: number;
    LimitType?: string;
    Exception?: string;
    DeviationNumber?: string;
}

export declare class RptLocPromptComplianceV extends BizView {
    LocationId?: number;
    LocationName?: string;
}

export declare class RptSamplePeriodV extends BizView {
    PeriodType?: string;
    SampleYear?: number;
    SampleYearText?: string;
    SamplePeriod?: number;
}

export declare class SampleV extends BizView {
    SampleId?: number;
    ParentSampleId?: number;
    ParentDilutionId?: number;
    TimeFrameDilutionId?: number;
    Name?: string;
    TestId?: number;
    TestTypeId?: number;
    SiteId?: number;
    SiteName?: string;
    LocationId?: number;
    LocationName?: string;
    Breadcrumb?: string;
    MediaId?: number;
    EquipmentId?: number;
    PersonnelSiteId?: number;
    PersonnelSiteDescription?: string;
    PersonnelUserId?: number;
    PersonnelUsername?: string;
    PersonnelEmpId?: string;
    PersonnelRealName?: string;
    LastMonitoredDate?: Date;
    InitialQualificationDate?: Date;
    NextQualificationDate?: Date;
    StartDate?: Date;
    EndDate?: Date;
    BarCode?: string;
    MeasurementUom?: string;
    Vloume?: number;
    PerformedUsername?: string;
    PerformedUserId?: number;
    NoTest?: boolean;
    IsNegative?: boolean;
    Approved?: boolean;
    ApprovedUserId?: number;
    ApprovedDate?: Date;
    DeviationNote?: string;
    IsEmEdit?: boolean;
    IsFdcEdit?: boolean;
    IsCompleted?: boolean;
    CompleteDate?: Date;
    Environment?: string;
    ParentOrgFoundId?: number;
    ScheduledStartDate?: Date;
    SamplingCompleted?: boolean;
    MaxChildOrgCount?: number;
    CreateBy?: number;
    CreatedTime?: Date;
    UpdateBy?: number;
    UpdatedTime?: Date;
    IsActive?: boolean;
}

/**
 *限度时段
 */
export declare type LimitPeriod = "Sample" | "Day" | "Hour";

/**
 *逻辑符号
 */
export declare type LogicSymbol = "And" | "Or";

/**
 *对齐时间
 */
export declare type AlignTime = "None" | "BeginningOfDay" | "EndOfDay";

export declare type PlatformType = "Web" | "Phone" | "Pad";

/**
 *下一阶段分配方式
 */
export declare type NextAllocation = "None" | "Same" | "Manual";

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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    LastMonitoredDate?: Date;
    /**
     *初始认证日期
     */
    InitialQualificationDate?: Date;
    /**
     *下次认证日期
     */
    NextQualificationDate?: Date;
    /**
     *密码有效期
     */
    PasswordExpiryPeriod?: Date;
    /**
     *最后一次密码更改时间戳
     */
    LastPasswordModified?: Date;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    SignDate?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    LastMonitoredDate?: Date;
    /**
     *初始认证日期
     */
    InitialQualificationDate?: Date;
    /**
     *下次认证日期
     */
    NextQualificationDate?: Date;
    /**
     *密码有效期
     */
    PasswordExpiryPeriod?: Date;
    /**
     *最后一次密码更改时间戳
     */
    LastPasswordModified?: Date;
    /**
     *是否隐藏
     */
    IsHidden?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    Logined?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
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
    CreatedTime?: Date;
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
    UpdatedTime?: Date;
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
    Timestamp?: Date;
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
    Timestamp?: Date;
}

/**
 *通知类型枚举
 */
export declare type NotificationTypes = "Deviation" | "LoginFailure" | "InvalidESig" | "AccountLocked" | "SampleNotCompleted" | "WeeklyTestNotCompleted" | "MonthlyTestNotCompleted" | "QuarterlyTestNotCompleted" | "MaxTimeAboutToExceed" | "WorkNotYetCompletedForToday" | "OrganismFound" | "OrganismAdded" | "WorkflowError" | "EquipmentAboutToExpire" | "MediaInventoryLow" | "UserQualificationLapsed" | "UserQualificationDue";

/**
 *审批实体
 */
export declare class ApprovalEntity extends BizEntity {
    /**
     *实体名
     */
    EntityName?: string;
    /**
     *实体Id
     */
    EntityId?: number;
    /**
     *状态
     */
    Status?: number;
    /**
     *流程实例Id
     */
    WorkflowInstanceId?: string;
    /**
     *结果
     */
    ApprovalResult?: ApprovalResult;
    /**
     *数据归属用户Id
     */
    Belongs?: number;
    /**
     *评论
     */
    Comment?: string;
    /**
     *是否完成
     */
    IsCompleted?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *审批项目集合
     */
    ApprovalItems?: Array<ApprovalItem>;
    /**
     *审批步骤集合
     */
    ApprovalSteps?: Array<ApprovalStep>;
}

/**
 *审批项目
 */
export declare class ApprovalItem extends BizEntity {
    /**
     *是否通过 审批步骤使用
     */
    Approved?: boolean;
    /**
     *设置状态状态 设置状态步骤使用
     */
    Status?: number;
    /**
     *步骤Id,同流程步骤Id
     */
    StepId?: string;
    /**
     *是否放弃流程
     */
    Abandoned?: boolean;
    /**
     *评论
     */
    Comment?: string;
    /**
     *操作时间
     */
    OperationTime?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *从此用户转移
     */
    FromUserId?: number;
    /**
     *审批用户Id
     */
    UserId?: number;
    /**
     *审批实体Id
     */
    ApprovalEntityId?: number;
    /**
     *审批实体
     */
    ApprovalEntity?: ApprovalEntity;
}

/**
 *审批步骤，目前只做前端查询那些用户为审批步骤的审批人
 */
export declare class ApprovalStep extends BizEntity {
    /**
     *步骤Id，同流程步骤Id
     */
    StepId?: string;
    /**
     *设置的审批用户Id字符串，每个用户id前后都有 , 方便查询
     */
    UserIdStr?: string;
    /**
     *状态
     */
    Status?: number;
    /**
     *期望通过数量0则需全部通过
     */
    ExpectCount?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *审批实体Id
     */
    ApprovalEntityId?: number;
    /**
     *审批实体
     */
    ApprovalEntity?: ApprovalEntity;
}

/**
 *ApprovalStep审计 自动生成，请勿修改
 */
export declare class ApprovalStepAudit extends BizEntity {
    /**
     *步骤Id，同流程步骤Id
     */
    StepId?: string;
    /**
     *设置的审批用户Id字符串，每个用户id前后都有 , 方便查询
     */
    UserIdStr?: string;
    /**
     *状态
     */
    Status?: number;
    /**
     *期望通过数量0则需全部通过
     */
    ExpectCount?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *审批实体Id
     */
    ApprovalEntityId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

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
    SendDate?: Date;
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
    NotificationDate?: Date;
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
     *培养基Id
     */
    MediaId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试样本
     */
    Sample?: Sample;
    /**
     *偏差
     */
    Deviation?: Deviation;
    /**
     *设备
     */
    Equipment?: Equipment;
    /**
     *培养基
     */
    Media?: Media;
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
    CreatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Notification审计 自动生成，请勿修改
 */
export declare class NotificationAudit extends BizEntity {
    /**
     *通知内容，可自行设置，也可通过 Messages 设置为json数组
     */
    Content?: string;
    /**
     *执行通知的时间戳
     */
    NotificationDate?: Date;
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
     *培养基Id
     */
    MediaId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *NotificationType审计 自动生成，请勿修改
 */
export declare class NotificationTypeAudit extends BizEntity {
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
    NotificationDate?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Subscription审计 自动生成，请勿修改
 */
export declare class SubscriptionAudit extends BizEntity {
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
    CreatedTime?: Date;
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
    UpdatedTime?: Date;
}

/**
 *测试样本
 */
export declare class Sample extends BizEntity {
    /**
     *父样本Id
     */
    ParentSampleId?: number;
    /**
     *没有文档
     */
    ParentDilutionId?: number;
    /**
     *没有文档
     */
    ParentOrgFoundId?: number;
    /**
     *没有文档
     */
    ScheduledStartDate?: Date;
    /**
     *同Moda
     */
    SamplingCompleted?: boolean;
    /**
     *描述
     */
    Name?: string;
    /**
     *测试方法Id
     */
    TestId?: number;
    /**
     *培养基id
     */
    MediaId?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *起始时间
     */
    StartDate?: Date;
    /**
     *结束时间
     */
    EndDate?: Date;
    /**
     *被抽样者身上的部位，例如被抽样者手套或长袍上的部位。
     */
    PersonnelSiteId?: number;
    /**
     *取样人员Id
     */
    PersonnelUserId?: number;
    /**
     *分配任务的用户
     */
    PerformedUserId?: number;
    /**
     *条码
     */
    BarCode?: string;
    /**
     *取样体积的测量单位
     */
    MeasurementUOM?: string;
    /**
     *测量体积(如空气采样体积)
     */
    Vloume?: number;
    /**
     *不再测试
     */
    NoTest?: boolean;
    /**
     *是否为阴性对照
     */
    IsNegative?: boolean;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *批准人Id
     */
    ApprovedUserId?: number;
    /**
     *批准日期
     */
    ApprovedDate?: Date;
    /**
     *是否审核
     */
    Reviewed?: boolean;
    /**
     *审核人Id
     */
    ReviewedUserId?: number;
    /**
     *审核日期
     */
    ReviewedDate?: Date;
    /**
     *用户Note信息
     */
    DeviationNote?: string;
    /**
     *任务是否完成
     */
    IsCompleted?: boolean;
    /**
     *完成日期
     */
    CompleteDate?: Date;
    /**
     *是否通过EM测试结果编辑功能编辑
     */
    IsEmEdit?: boolean;
    /**
     *是否通过FDC测试结果编辑功能编辑
     */
    IsFdcEdit?: boolean;
    /**
     *最严重异常
            LimitType Alert Action ......
     */
    MostLevelLimit?: string;
    /**
     *产品附加信息
     */
    ProductNote?: string;
    /**
     *工作流实例Id
     */
    WorkflowInstanceId?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *采样时的环境
     */
    EnvironmentId?: number;
    /**
     *父样本
     */
    ParentSample?: Sample;
    /**
     *测试方法
     */
    Test?: Test;
    /**
     *培养基
     */
    Media?: Media;
    /**
     *设备
     */
    Equipment?: Equipment;
    /**
     *人体采样点
     */
    PersonnelSite?: PersonnelSite;
    /**
     *取样人员
     */
    PersonnelUser?: User;
    /**
     *任务分配人员
     */
    PerformedUser?: User;
    /**
     *任务批准人员
     */
    ApprovedUser?: User;
    /**
     *采样环境
     */
    Environment?: EnvironmentDef;
    /**
     *任务项目
     */
    CurrentWorkSpaces?: Array<CurrentWorkSpace>;
}

/**
 *存储与样本相关的唯一偏差事件，例如超出操作或警报限制的结果。
 */
export declare class Deviation extends BizEntity {
    /**
     *测试结果
     */
    ReadingValue?: number;
    /**
     *字母数字字段，用于为与外部系统的偏差分配跟踪编号
     */
    DeviationNumber?: number;
    /**
     *测试结果描述
     */
    ValueDescription?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *限定Id LimitDefId
     */
    LimitDefId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *实际测试（经过plan）的测试方法
     */
    TestId?: number;
    /**
     *测试用例实体id
     */
    SampleId?: number;
    /**
     *样本任务Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *限定
     */
    LimitDef?: LimitDef;
    /**
     *限度类型
     */
    LimitType?: LimitType;
    /**
     *测试
     */
    Test?: Test;
    /**
     *测试用例实体
     */
    Sample?: Sample;
    /**
     *样本任务
     */
    CurrentWorkSpace?: CurrentWorkSpace;
}

/**
 *批次
 */
export declare class Batch extends BizEntity {
    /**
     *产品Id
     */
    ProductId?: number;
    /**
     *批号
     */
    BatchLotNumber?: string;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *产品
     */
    Product?: Product;
}

/**
 *Batch审计 自动生成，请勿修改
 */
export declare class BatchAudit extends BizEntity {
    /**
     *产品Id
     */
    ProductId?: number;
    /**
     *批号
     */
    BatchLotNumber?: string;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试池
 */
export declare class CurrentWorkSpace extends BizEntity {
    /**
     *是否未测试 
            TODO 具体分步骤(测试方法)时是都跳过
     */
    NoTest?: boolean;
    /**
     *样本条码
     */
    SampleBarcode?: string;
    /**
     *最早执行日期
     */
    EarlyExecutionDate?: Date;
    /**
     *安排日期,目前作为完成的最后日期
     */
    ScheduledDate?: Date;
    /**
     *失效日期
     */
    IneffectiveDate?: Date;
    /**
     *复制数字
     */
    ReplicateNumber?: number;
    /**
     *状态
            case 0: Scheduled
            case 1: Assigned
            case 2: Pending
            case 3: Completed
            case 4: Failed
     */
    Status?: CurrentWorkStatus;
    /**
     *是否完成
     */
    Completed?: boolean;
    /**
     *完成日期
     */
    CompletedDate?: Date;
    /**
     *复制原因
     */
    ReplicateReason?: string;
    /**
     *工作流是否完成
     */
    WorkflowCompleted?: boolean;
    /**
     *工作流完成日期
     */
    WorkflowCompletedDate?: Date;
    /**
     *分配任务的用户Id
     */
    PerformedUserId?: number;
    /**
     *是否安排
     */
    Scheduled?: boolean;
    /**
     *最后ESIG用户名
     */
    LastEsigUserName?: string;
    /**
     *剩余时间
     */
    RemainingMinutes?: number;
    /**
     *执行通知的时间戳
     */
    NotificationDate?: Date;
    /**
     *接收或分配时间
     */
    RecieveDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *来自上一阶段执行人
     */
    LastUserId?: number;
    /**
     *来自上一阶段的交接说明
     */
    LastStepRemark?: string;
    /**
     *执行操作的用户Id
     */
    UserId?: number;
    /**
     *页面设置的实际执行人Id
     */
    ExecuteUserId?: number;
    /**
     *备注信息
     */
    Remark?: string;
    /**
     *测试计划Id
     */
    TestId?: number;
    /**
     *批次Id
     */
    BatchId?: number;
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *测试阶段Id
     */
    TestStageId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *测试计划Id
     */
    PlanId?: number;
    /**
     *组Id
     */
    GroupId?: number;
    /**
     *时间框架Id
     */
    TimeFrameId?: number;
    /**
     *父当前工作空间对象Id
     */
    ParentCurrentWorkSpaceId?: number;
    /**
     *分配任务的用户
     */
    PerformedUser?: User;
    /**
     *来自上一阶段执行操作用户
     */
    LastUser?: User;
    /**
     *执行操作用户
     */
    User?: User;
    /**
     *页面设置的实际执行人
     */
    ExecuteUser?: User;
    /**
     *测试计划
     */
    Test?: Test;
    /**
     *批次
     */
    Batch?: Batch;
    /**
     *样本
     */
    Sample?: Sample;
    /**
     *测试阶段
     */
    TestStage?: TestStage;
    /**
     *区域
     */
    Location?: Location;
    /**
     *测试计划
     */
    Plan?: Plan;
    /**
     *组
     */
    Group?: Group;
    /**
     *时间框架
     */
    TimeFrame?: TimeFrame;
    /**
     *父当前工作空间对象
     */
    ParentCurrentWorkSpace?: CurrentWorkSpace;
    /**
     *偏差事件
     */
    Deviations?: Array<Deviation>;
}

/**
 *测试
 */
export declare class Test extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *间距（每隔几小时、天、周、月）
     */
    Interval?: number;
    /**
     *刻钟数 组合 1->15 2->30 4->45
     */
    MinutesOfHour?: number;
    /**
     *一天中的小时（二进制组合）
     */
    HoursOfDay?: number;
    /**
     *一周中的天（二进制组合）
     */
    DaysOfWeek?: number;
    /**
     *一月中的天（二进制组合）
     */
    DaysOfMonth?: number;
    /**
     *一年中的月（二进制组合）
     */
    MonthsOfYear?: number;
    /**
     *是否随机
     */
    Randomize?: boolean;
    /**
     *是否未测试
     */
    NotTest?: boolean;
    /**
     *是否设置对照组
     */
    NegativeControl?: boolean;
    /**
     *是否推荐顺序
     */
    SequenceAdvice?: boolean;
    /**
     *计划开始日期
     */
    ScheduleStartDate?: Date;
    /**
     *计划首次偏移
     */
    ScheduleOffset?: number;
    /**
     *价格
     */
    Price?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *通知频率Id
     */
    NotificationFrequencyId?: number;
    /**
     *测试点Id
     */
    SiteId?: number;
    /**
     *测试类别Id
     */
    TestTypeId?: number;
    /**
     *测试计划Id
     */
    PlanId?: number;
    /**
     *父测试对象Id
     */
    ParentTestId?: number;
    /**
     *时间框架稀释Id
     */
    TimeFrameDilutionId?: number;
    /**
     *组Id
     */
    GroupId?: number;
    /**
     *测试频率Id
     */
    TestFrequencyId?: number;
    /**
     *测试频率类型Id
     */
    TestFrequencyTypeId?: number;
    /**
     *测试频率出现Id
     */
    TestFrequencyOccurrenceId?: number;
    /**
     *通知频率
     */
    NotificationFrequency?: NotificationFrequency;
    /**
     *测试点
     */
    Site?: Site;
    /**
     *测试类别
     */
    TestType?: TestType;
    /**
     *测试计划
     */
    Plan?: Plan;
    /**
     *父测试对象
     */
    ParentTest?: Test;
    /**
     *时间框架稀释
     */
    TimeFrameDilution?: TimeFrameDilution;
    /**
     *组
     */
    Group?: Group;
    /**
     *测试频率
     */
    TestFrequency?: TestFrequency;
    /**
     *测试频率类型
     */
    TestFrequencyType?: TestFrequencyType;
    /**
     *测试频率出现
     */
    TestFrequencyOccurrence?: TestFrequencyOccurrence;
    /**
     *限度
     */
    Limits?: Array<Limit>;
    /**
     *环境
     */
    Environment?: EnvironmentDef;
}

/**
 *测试计划            频率：            1.设定间隔时长，代表当前的勾选时长+间隔时长为一个判断周期，如果存在勾选的时长重叠问题，不会影响具体的生成，因为有标记            2.如果断电:对于已经生成的任务，依旧正常使用，但断电期间未能生成的任务，则不会恢复生成，而是只会生成未来的任务            3.计划的升版本，会存在多个版本的任务在任务池中，需要特定权限的用户维护正常可执行的任务，对于旧版计划的任务，会选择ToTest掉
 */
export declare class Plan extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *是否为当前可用版本
     */
    Enabled?: boolean;
    /**
     *有效日期
     */
    EffectiveDate?: Date;
    /**
     *失效日期
     */
    IneffectiveDate?: Date;
    /**
     *批准日期
     */
    ApprovedDate?: Date;
    /**
     *完成日期
     */
    CompletedDate?: Date;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *状态
     */
    Status?: PlanStatus;
    /**
     *本周第一天
     */
    WorkWeek?: number;
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *几号（范围1-31）
     */
    FillDayOfMonth?: number;
    /**
     *星期几（周日：0；周一：1）
     */
    FillDayOfWeek?: number;
    /**
     *时间（小时*60+分钟；范围0-1439）
     */
    FillTime?: number;
    /**
     *小时组合（0-23的组合）
     */
    FillHoursOfDay?: number;
    /**
     *分钟（0-59；只用在Hourly，不是组合）
     */
    FillMinutesOfHour?: number;
    /**
     *持续时间（几小时、天、周、月）
     */
    FillLength?: number;
    /**
     *计划有效时间
     */
    FillEffectiveDate?: Date;
    /**
     *计划自动 退役时间
     */
    AutoRetiredDate?: Date;
    /**
     *最后填写时间（作为计划完成标记）
     */
    LastFillDate?: Date;
    /**
     *每周第1天是星期几（周日：0；周一：1）
     */
    FirstDayOfWeek?: number;
    /**
     *是否重复自动填充
     */
    IsRepeatFill?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *组Id
     */
    GroupId?: number;
    /**
     *计划组Id
     */
    PlanGroupId?: number;
    /**
     *自动填充选项的频率类别
     */
    AutoFillFrequencyTypeId?: number;
    /**
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *区域
     */
    Location?: Location;
    /**
     *组
     */
    Group?: Group;
    /**
     *计划组
     */
    PlanGroup?: PlanGroup;
    /**
     *测试频率类型
     */
    AutoFillFrequencyType?: TestFrequencyType;
    /**
     *测试
     */
    Tests?: Array<Test>;
    /**
     *环境
     */
    Environment?: EnvironmentDef;
}

/**
 *组
 */
export declare class Group extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *CurrentWorkSpace审计 自动生成，请勿修改
 */
export declare class CurrentWorkSpaceAudit extends BizEntity {
    /**
     *是否未测试 
            TODO 具体分步骤(测试方法)时是都跳过
     */
    NoTest?: boolean;
    /**
     *样本条码
     */
    SampleBarcode?: string;
    /**
     *最早执行日期
     */
    EarlyExecutionDate?: Date;
    /**
     *安排日期,目前作为完成的最后日期
     */
    ScheduledDate?: Date;
    /**
     *失效日期
     */
    IneffectiveDate?: Date;
    /**
     *复制数字
     */
    ReplicateNumber?: number;
    /**
     *状态
            case 0: Scheduled
            case 1: Assigned
            case 2: Pending
            case 3: Completed
            case 4: Failed
     */
    Status?: CurrentWorkStatus;
    /**
     *是否完成
     */
    Completed?: boolean;
    /**
     *完成日期
     */
    CompletedDate?: Date;
    /**
     *复制原因
     */
    ReplicateReason?: string;
    /**
     *工作流是否完成
     */
    WorkflowCompleted?: boolean;
    /**
     *工作流完成日期
     */
    WorkflowCompletedDate?: Date;
    /**
     *分配任务的用户Id
     */
    PerformedUserId?: number;
    /**
     *是否安排
     */
    Scheduled?: boolean;
    /**
     *最后ESIG用户名
     */
    LastEsigUserName?: string;
    /**
     *剩余时间
     */
    RemainingMinutes?: number;
    /**
     *执行通知的时间戳
     */
    NotificationDate?: Date;
    /**
     *接收或分配时间
     */
    RecieveDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *来自上一阶段执行人
     */
    LastUserId?: number;
    /**
     *来自上一阶段的交接说明
     */
    LastStepRemark?: string;
    /**
     *执行操作的用户Id
     */
    UserId?: number;
    /**
     *页面设置的实际执行人Id
     */
    ExecuteUserId?: number;
    /**
     *备注信息
     */
    Remark?: string;
    /**
     *测试计划Id
     */
    TestId?: number;
    /**
     *批次Id
     */
    BatchId?: number;
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *测试阶段Id
     */
    TestStageId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *测试计划Id
     */
    PlanId?: number;
    /**
     *组Id
     */
    GroupId?: number;
    /**
     *时间框架Id
     */
    TimeFrameId?: number;
    /**
     *父当前工作空间对象Id
     */
    ParentCurrentWorkSpaceId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *文件关联
 */
export declare class FileAttachment extends BizEntity {
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
     *sampleId
     */
    SampleId?: number;
    /**
     *关联CurrentWorkSpace Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *关联sample
     */
    Sample?: Sample;
    /**
     *关联CurrentWorkSpace
     */
    CurrentWorkSpace?: CurrentWorkSpace;
}

/**
 *FileAttachment审计 自动生成，请勿修改
 */
export declare class FileAttachmentAudit extends BizEntity {
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
     *sampleId
     */
    SampleId?: number;
    /**
     *关联CurrentWorkSpace Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *视图
 */
export declare class GeneralPoolV extends BizView {
    /**
     *当前工作空间Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *区域Id
     */
    LocationId?: number;
    /**
     *区域名称
     */
    LocationName?: string;
    /**
     *区域条码
     */
    LocationBarcode?: string;
    /**
     *区域Id面包导航
     */
    LocationBreadcrumb?: string;
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
    SiteDescription?: string;
    /**
     *采样点条码
     */
    SiteBarcode?: string;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *测试样本条码
     */
    SampleBarcode?: string;
    /**
     *测试样本是否批准
     */
    SampleApproved?: boolean;
    /**
     *测试样本起始时间
     */
    SampleStartDate?: Date;
    /**
     *测试样本结束时间
     */
    SampleEndDate?: Date;
    /**
     *取样体积的测量单位
     */
    SampleUOM?: string;
    /**
     *被抽样者身上的部位，例如被抽样者手套或长袍上的部位。
     */
    PersonnelSiteId?: number;
    /**
     *取样人员Id
     */
    PersonnelUserId?: number;
    /**
     *取样人员用户名
     */
    PersonnelUsername?: string;
    /**
     *是否为阴性对照
     */
    NegativeControl?: boolean;
    /**
     *测量体积(如空气采样体积)
     */
    SampleVolume?: number;
    /**
     *用户Note信息
     */
    SampleDeviationNote?: string;
    /**
     *任务分配给Who
     */
    SamplePerformedUsername?: string;
    /**
     *工作项分配的用户Id
     */
    UserId?: number;
    /**
     *工作项分配的用户名
     */
    Username?: string;
    /**
     *组Id
     */
    GroupId?: number;
    /**
     *组名称
     */
    GroupName?: string;
    /**
     *测试计划Id
     */
    PlanId?: number;
    /**
     *测试计划组Id
     */
    PlanGroupId?: number;
    /**
     *测试计划名称
     */
    PlanName?: string;
    /**
     *测试计划条码
     */
    PlanBarcode?: string;
    /**
     *测试计划描述
     */
    PlanDescription?: string;
    /**
     *测试计划版本
     */
    PlanVersion?: number;
    /**
     *测试计划是否可用
     */
    PlanEnabled?: boolean;
    /**
     *测试计划有效日期
     */
    PlanEffectiveDate?: Date;
    /**
     *测试计划失效日期
     */
    PlanIneffectiveDate?: Date;
    /**
     *测试计划完成日期
     */
    PlanCompletedDate?: Date;
    /**
     *测试计划状态
     */
    PlanStatus?: number;
    /**
     *测试计划批准日期
     */
    PlanApprovedDate?: Date;
    /**
     *测试计划是否批准
     */
    PlanApproved?: boolean;
    /**
     *安排日期
     */
    ScheduledDate?: Date;
    /**
     *失效日期
     */
    IneffectiveDate?: Date;
    /**
     *复制数字
     */
    ReplicateNumber?: number;
    /**
     *状态
     */
    Status?: number;
    /**
     *是否安排
     */
    Scheduled?: boolean;
    /**
     *是否完成
     */
    Completed?: boolean;
    /**
     *复制原因
     */
    ReplicateReason?: string;
    /**
     *工作流是否完成
     */
    WorkflowCompleted?: boolean;
    /**
     *工作流完成日期
     */
    WorkflowCompletedDate?: Date;
    /**
     *执行用户名
     */
    PerformedUsername?: string;
    /**
     *最后ESIG用户名
     */
    LastEsigUserName?: string;
    /**
     *是否未测试
     */
    NoTest?: boolean;
    /**
     *测试计划Id
     */
    TestId?: number;
    /**
     *测试计划名称
     */
    TestName?: string;
    /**
     *测试计划描述
     */
    TestDescription?: string;
    /**
     *测试计划环境
     */
    TestEnvironment?: string;
    /**
     *测试类别Id
     */
    TestTypeId?: number;
    /**
     *测试频率Id
     */
    TestFrequencyId?: number;
    /**
     *测试频率类型Id
     */
    TestFrequencyTypeId?: number;
    /**
     *测试类别描述
     */
    TestTypeDescription?: string;
    /**
     *测试种类Id
     */
    TestCategoryId?: number;
    /**
     *测试种类描述
     */
    TestCategoryDescription?: string;
    /**
     *测试分类描述
     */
    TestClassDescription?: string;
    /**
     *测试阶段Id
     */
    TestStageId?: number;
    /**
     *测试阶段描述
     */
    TestStageDescription?: string;
    /**
     *测试阶段Id
     */
    TimeFrameId?: number;
    /**
     *测试阶段序号
     */
    TimeFrameSequence?: number;
    /**
     *测试阶段描述
     */
    TimeFrameDescription?: string;
    /**
     *验证之前步骤
     */
    VerifyPrevious?: boolean;
    /**
     *校准最长时间
     */
    MaxCalAlignment?: AlignTime;
    /**
     *父当前工作空间对象Id
     */
    ParentCurrentWorkSpaceId?: number;
    /**
     *批次Id
     */
    BatchId?: number;
    /**
     *产品名称
     */
    ProductName?: string;
    /**
     *批号
     */
    BatchLotNumber?: string;
    /**
     *采样阶段
     */
    IsSamplingStage?: boolean;
    /**
     *采样过晚
     */
    IsSampleLate?: boolean;
    /**
     *超出限定
     */
    HasExceededLimits?: boolean;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
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
    UpdatedTime?: Date;
}

/**
 *Group审计 自动生成，请勿修改
 */
export declare class GroupAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *用于工作项目的限度
 */
export declare class Limit extends BizEntity {
    /**
     *测试方法限度Id
     */
    LimitDefId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *测试计划Id
     */
    TestId?: number;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *流行
            数越大越优先
     */
    Prevalence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否为频率限定
     */
    FreqLimit?: boolean;
    /**
     *源限度类型Id
     */
    SourceLimitTypeId?: number;
    /**
     *源限度Id
     */
    SourceLimitDefId?: number;
    /**
     *发生次数
     */
    OccurrenceCount?: number;
    /**
     *时段
     */
    Period?: LimitPeriod;
    /**
     *定期总数
     */
    PeriodCount?: number;
    /**
     *超出限度Id
     */
    LimitExceededId?: number;
    /**
     *偏差
     */
    Deviation?: boolean;
    /**
     *邮件通知
     */
    EmailNotify?: boolean;
    /**
     *屏幕通知
     */
    ScreenNotify?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *重新计划数量
     */
    RescheduleCount?: number;
    /**
     *重新计划偏差
     */
    RescheduleOffset?: string;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *是否每周期执行
     */
    ExecutePerCycle?: boolean;
    /**
     *报告数字算子
     */
    ReportableOperator?: string;
    /**
     *报告数值
     */
    ReportableValue?: number;
    /**
     *freq 关联的限度Id
     */
    ParentLimitId?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *测试方法限度
     */
    LimitDef?: LimitDef;
    /**
     *限度类型
     */
    LimitType?: LimitType;
    /**
     *测试计划
     */
    Test?: Test;
    /**
     *采样点
     */
    Site?: Site;
    /**
     *源限度类型
     */
    SourceLimitType?: LimitType;
    /**
     *源限度
     */
    SourceLimitDef?: LimitDef;
    /**
     *超出限度
     */
    LimitExceeded?: LimitExceeded;
    /**
     *限制规则组
     */
    LimitRuleGroups?: Array<LimitRuleGroup>;
    /**
     *freq 关联的限度
     */
    ParentLimit?: Limit;
}

/**
 *超出限度,跟踪可能触发基于频率的限制的任何限制和偏差
 */
export declare class LimitExceeded extends BizEntity {
    /**
     *限度Id
     */
    LimitId?: number;
    /**
     *频率限度Id
     */
    FrequencyLimitId?: number;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *限度
     */
    Limit?: Limit;
    /**
     *频率限度
     */
    FrequencyLimit?: Limit;
    /**
     *测试样本
     */
    Sample?: Sample;
    /**
     *限度类型
     */
    LimitType?: LimitType;
}

/**
 *Limit审计 自动生成，请勿修改
 */
export declare class LimitAudit extends BizEntity {
    /**
     *测试方法限度Id
     */
    LimitDefId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *测试计划Id
     */
    TestId?: number;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *流行
            数越大越优先
     */
    Prevalence?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *是否为频率限定
     */
    FreqLimit?: boolean;
    /**
     *源限度类型Id
     */
    SourceLimitTypeId?: number;
    /**
     *源限度Id
     */
    SourceLimitDefId?: number;
    /**
     *发生次数
     */
    OccurrenceCount?: number;
    /**
     *时段
     */
    Period?: LimitPeriod;
    /**
     *定期总数
     */
    PeriodCount?: number;
    /**
     *超出限度Id
     */
    LimitExceededId?: number;
    /**
     *偏差
     */
    Deviation?: boolean;
    /**
     *邮件通知
     */
    EmailNotify?: boolean;
    /**
     *屏幕通知
     */
    ScreenNotify?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *重新计划数量
     */
    RescheduleCount?: number;
    /**
     *重新计划偏差
     */
    RescheduleOffset?: string;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *是否每周期执行
     */
    ExecutePerCycle?: boolean;
    /**
     *报告数字算子
     */
    ReportableOperator?: string;
    /**
     *报告数值
     */
    ReportableValue?: number;
    /**
     *freq 关联的限度Id
     */
    ParentLimitId?: number;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *LimitExceeded审计 自动生成，请勿修改
 */
export declare class LimitExceededAudit extends BizEntity {
    /**
     *限度Id
     */
    LimitId?: number;
    /**
     *频率限度Id
     */
    FrequencyLimitId?: number;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *算进频率限度
     */
    CountTowardFrequency?: boolean;
    /**
     *重新启动频率限度
     */
    ResetFreqLimit?: boolean;
    /**
     *重新计划
     */
    Reschedule?: boolean;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *通知频率
 */
export declare class NotificationFrequency extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *NotificationFrequency审计 自动生成，请勿修改
 */
export declare class NotificationFrequencyAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试计划组
 */
export declare class PlanGroup extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *变化历史
     */
    ChangeHistory?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试计划
     */
    Plans?: Array<Plan>;
}

/**
 *测试频率类型
 */
export declare class TestFrequencyType extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Plan审计 自动生成，请勿修改
 */
export declare class PlanAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *是否为当前可用版本
     */
    Enabled?: boolean;
    /**
     *有效日期
     */
    EffectiveDate?: Date;
    /**
     *失效日期
     */
    IneffectiveDate?: Date;
    /**
     *批准日期
     */
    ApprovedDate?: Date;
    /**
     *完成日期
     */
    CompletedDate?: Date;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *状态
     */
    Status?: PlanStatus;
    /**
     *本周第一天
     */
    WorkWeek?: number;
    /**
     *条形码
     */
    Barcode?: string;
    /**
     *几号（范围1-31）
     */
    FillDayOfMonth?: number;
    /**
     *星期几（周日：0；周一：1）
     */
    FillDayOfWeek?: number;
    /**
     *时间（小时*60+分钟；范围0-1439）
     */
    FillTime?: number;
    /**
     *小时组合（0-23的组合）
     */
    FillHoursOfDay?: number;
    /**
     *分钟（0-59；只用在Hourly，不是组合）
     */
    FillMinutesOfHour?: number;
    /**
     *持续时间（几小时、天、周、月）
     */
    FillLength?: number;
    /**
     *计划有效时间
     */
    FillEffectiveDate?: Date;
    /**
     *计划自动 退役时间
     */
    AutoRetiredDate?: Date;
    /**
     *最后填写时间（作为计划完成标记）
     */
    LastFillDate?: Date;
    /**
     *每周第1天是星期几（周日：0；周一：1）
     */
    FirstDayOfWeek?: number;
    /**
     *是否重复自动填充
     */
    IsRepeatFill?: boolean;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *组Id
     */
    GroupId?: number;
    /**
     *计划组Id
     */
    PlanGroupId?: number;
    /**
     *自动填充选项的频率类别
     */
    AutoFillFrequencyTypeId?: number;
    /**
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *PlanGroup审计 自动生成，请勿修改
 */
export declare class PlanGroupAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *变化历史
     */
    ChangeHistory?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *粒子计数器数据
 */
export declare class ParticleCounterData extends BizEntity {
    /**
     *读取Id
     */
    DataId?: number;
    /**
     *设备数据状态
     */
    Status?: EquipmentDataStatus;
    /**
     *采样时间
     */
    SampleDate?: Date;
    /**
     *房间名称
     */
    RoomName?: string;
    /**
     *位点
     */
    SiteName?: string;
    /**
     *采样时长
     */
    SampleDuration?: string;
    /**
     *采样体积L
     */
    Volume?: number;
    /**
     *0.5 μm
     */
    UM05?: number;
    /**
     *1.0 μm
     */
    UM10?: number;
    /**
     *2.0 μm
     */
    UM20?: number;
    /**
     *3.0 μm
     */
    UM30?: number;
    /**
     *4.0 μm
     */
    UM40?: number;
    /**
     *5.0 μm
     */
    UM50?: number;
    /**
     *10 μm
     */
    UM100?: number;
    /**
     *匹配时间
     */
    MatchedDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *设备关联
     */
    Equipment?: Equipment;
    /**
     *采样点
     */
    Site?: Site;
    /**
     *测试样本
     */
    Sample?: Sample;
    /**
     *
     */
    CompareDataRemarks?: Array<CompareDataRemark>;
}

/**
 *ParticleCounterData审计 自动生成，请勿修改
 */
export declare class ParticleCounterDataAudit extends BizEntity {
    /**
     *读取Id
     */
    DataId?: number;
    /**
     *设备数据状态
     */
    Status?: EquipmentDataStatus;
    /**
     *采样时间
     */
    SampleDate?: Date;
    /**
     *房间名称
     */
    RoomName?: string;
    /**
     *位点
     */
    SiteName?: string;
    /**
     *采样时长
     */
    SampleDuration?: string;
    /**
     *采样体积L
     */
    Volume?: number;
    /**
     *0.5 μm
     */
    UM05?: number;
    /**
     *1.0 μm
     */
    UM10?: number;
    /**
     *2.0 μm
     */
    UM20?: number;
    /**
     *3.0 μm
     */
    UM30?: number;
    /**
     *4.0 μm
     */
    UM40?: number;
    /**
     *5.0 μm
     */
    UM50?: number;
    /**
     *10 μm
     */
    UM100?: number;
    /**
     *匹配时间
     */
    MatchedDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *采样点Id
     */
    SiteId?: number;
    /**
     *测试样本Id
     */
    SampleId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试频率
 */
export declare class TestFrequency extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *间距
     */
    Interval?: number;
    /**
     *一小时中的分钟（二进制组合）
     */
    MinutesOfHour?: number;
    /**
     *一天中的小时（二进制组合）
     */
    HoursOfDay?: number;
    /**
     *一周中的天（二进制组合）
     */
    DaysOfWeek?: number;
    /**
     *一月中的天（二进制组合）
     */
    DaysOfMonth?: number;
    /**
     *一年中的月（二进制组合）
     */
    MonthsOfYear?: number;
    /**
     *是否随机
     */
    Randomize?: boolean;
    /**
     *计划开始日期
     */
    ScheduleStartDate?: Date;
    /**
     *计划首次偏移
     */
    ScheduleOffset?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试频率类型Id
     */
    TestFrequencyTypeId?: number;
    /**
     *测试频率出现Id
     */
    TestFrequencyOccurrenceId?: number;
    /**
     *测试频率类型
     */
    TestFrequencyType?: TestFrequencyType;
    /**
     *测试频率出现
     */
    TestFrequencyOccurrence?: TestFrequencyOccurrence;
}

/**
 *测试频率出现
 */
export declare class TestFrequencyOccurrence extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *Test审计 自动生成，请勿修改
 */
export declare class TestAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *间距（每隔几小时、天、周、月）
     */
    Interval?: number;
    /**
     *刻钟数 组合 1->15 2->30 4->45
     */
    MinutesOfHour?: number;
    /**
     *一天中的小时（二进制组合）
     */
    HoursOfDay?: number;
    /**
     *一周中的天（二进制组合）
     */
    DaysOfWeek?: number;
    /**
     *一月中的天（二进制组合）
     */
    DaysOfMonth?: number;
    /**
     *一年中的月（二进制组合）
     */
    MonthsOfYear?: number;
    /**
     *是否随机
     */
    Randomize?: boolean;
    /**
     *是否未测试
     */
    NotTest?: boolean;
    /**
     *是否设置对照组
     */
    NegativeControl?: boolean;
    /**
     *是否推荐顺序
     */
    SequenceAdvice?: boolean;
    /**
     *计划开始日期
     */
    ScheduleStartDate?: Date;
    /**
     *计划首次偏移
     */
    ScheduleOffset?: number;
    /**
     *价格
     */
    Price?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *通知频率Id
     */
    NotificationFrequencyId?: number;
    /**
     *测试点Id
     */
    SiteId?: number;
    /**
     *测试类别Id
     */
    TestTypeId?: number;
    /**
     *测试计划Id
     */
    PlanId?: number;
    /**
     *父测试对象Id
     */
    ParentTestId?: number;
    /**
     *时间框架稀释Id
     */
    TimeFrameDilutionId?: number;
    /**
     *组Id
     */
    GroupId?: number;
    /**
     *测试频率Id
     */
    TestFrequencyId?: number;
    /**
     *测试频率类型Id
     */
    TestFrequencyTypeId?: number;
    /**
     *测试频率出现Id
     */
    TestFrequencyOccurrenceId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestFrequency审计 自动生成，请勿修改
 */
export declare class TestFrequencyAudit extends BizEntity {
    /**
     *描述
     */
    Description?: string;
    /**
     *间距
     */
    Interval?: number;
    /**
     *一小时中的分钟（二进制组合）
     */
    MinutesOfHour?: number;
    /**
     *一天中的小时（二进制组合）
     */
    HoursOfDay?: number;
    /**
     *一周中的天（二进制组合）
     */
    DaysOfWeek?: number;
    /**
     *一月中的天（二进制组合）
     */
    DaysOfMonth?: number;
    /**
     *一年中的月（二进制组合）
     */
    MonthsOfYear?: number;
    /**
     *是否随机
     */
    Randomize?: boolean;
    /**
     *计划开始日期
     */
    ScheduleStartDate?: Date;
    /**
     *计划首次偏移
     */
    ScheduleOffset?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试频率类型Id
     */
    TestFrequencyTypeId?: number;
    /**
     *测试频率出现Id
     */
    TestFrequencyOccurrenceId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestFrequencyOccurrence审计 自动生成，请勿修改
 */
export declare class TestFrequencyOccurrenceAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *TestFrequencyType审计 自动生成，请勿修改
 */
export declare class TestFrequencyTypeAudit extends BizEntity {
    /**
     *名称
     */
    Name?: string;
    /**
     *描述
     */
    Description?: string;
    /**
     *英文
     */
    En?: string;
    /**
     *中文
     */
    Zh?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *Deviation审计 自动生成，请勿修改
 */
export declare class DeviationAudit extends BizEntity {
    /**
     *测试结果
     */
    ReadingValue?: number;
    /**
     *字母数字字段，用于为与外部系统的偏差分配跟踪编号
     */
    DeviationNumber?: number;
    /**
     *测试结果描述
     */
    ValueDescription?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *是否启用
     */
    IsActive?: boolean;
    /**
     *限定Id LimitDefId
     */
    LimitDefId?: number;
    /**
     *限度类型Id
     */
    LimitTypeId?: number;
    /**
     *实际测试（经过plan）的测试方法
     */
    TestId?: number;
    /**
     *测试用例实体id
     */
    SampleId?: number;
    /**
     *样本任务Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试结果
 */
export declare class Reading extends BizEntity {
    /**
     *测试用例Id
     */
    SampleId?: number;
    /**
     *测试父用例Id
     */
    ParentSampleId?: number;
    /**
     *工作区Id
     */
    WorkspaceId?: number;
    /**
     *稀释Id
     */
    DilutionId?: number;
    /**
     *实体测试Id,Plan关联的测试方法
     */
    TestId?: number;
    /**
     *测试步骤Id
     */
    MeasurementId?: number;
    /**
     *循环次数
     */
    CycleNumber?: number;
    /**
     *显示顺序 用于Analyze, Review  Approve reports，来自测试方法Measurement
     */
    Sequence?: number;
    /**
     *执行人
     */
    PerformUserName?: number;
    /**
     *微生物Id
     */
    OrganismId?: number;
    /**
     *录入结果
     */
    Value?: string;
    /**
     *结果取值类型
     */
    DataTypeId?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *计算公式
     */
    Formula?: string;
    /**
     *测试阶段实体
     */
    TimeFrameId?: number;
    /**
     *单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *粒子尺寸
     */
    ParticleSize?: number;
    /**
     *结果
     */
    Result?: boolean;
    /**
     *取消测试
     */
    NoTest?: boolean;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *结果发生时间
     */
    ReadingDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *测试用例
     */
    Sample?: Sample;
    /**
     *测试父用例Id
     */
    ParentSample?: Sample;
    /**
     *测试方法
     */
    Test?: Test;
    /**
     *测试步骤
     */
    Measurement?: Measurement;
    /**
     *微生物
     */
    Organism?: Organism;
    /**
     *单位
     */
    UnitOfMeasure?: UnitOfMeasure;
    /**
     *符号
     */
    Sign?: Sign;
}

/**
 *Reading审计 自动生成，请勿修改
 */
export declare class ReadingAudit extends BizEntity {
    /**
     *测试用例Id
     */
    SampleId?: number;
    /**
     *测试父用例Id
     */
    ParentSampleId?: number;
    /**
     *工作区Id
     */
    WorkspaceId?: number;
    /**
     *稀释Id
     */
    DilutionId?: number;
    /**
     *实体测试Id,Plan关联的测试方法
     */
    TestId?: number;
    /**
     *测试步骤Id
     */
    MeasurementId?: number;
    /**
     *循环次数
     */
    CycleNumber?: number;
    /**
     *显示顺序 用于Analyze, Review  Approve reports，来自测试方法Measurement
     */
    Sequence?: number;
    /**
     *执行人
     */
    PerformUserName?: number;
    /**
     *微生物Id
     */
    OrganismId?: number;
    /**
     *录入结果
     */
    Value?: string;
    /**
     *结果取值类型
     */
    DataTypeId?: number;
    /**
     *描述
     */
    Description?: string;
    /**
     *计算公式
     */
    Formula?: string;
    /**
     *测试阶段实体
     */
    TimeFrameId?: number;
    /**
     *单位Id
     */
    UnitOfMeasureId?: number;
    /**
     *符号Id
     */
    SignId?: number;
    /**
     *粒子尺寸
     */
    ParticleSize?: number;
    /**
     *结果
     */
    Result?: boolean;
    /**
     *取消测试
     */
    NoTest?: boolean;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *结果发生时间
     */
    ReadingDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

export declare class RptSampleMart extends BizEntity {
    /**
     *测试样本id
     */
    SampleId?: number;
    /**
     *计划id
     */
    PlanId?: number;
    /**
     *测试Id
     */
    TestId?: number;
    /**
     *测试点id
     */
    SiteId?: number;
    /**
     *测试点名称
     */
    SiteName?: string;
    /**
     *测试方法id
     */
    TestTypeId?: number;
    /**
     *测试方法名称
     */
    TestTypeName?: string;
    /**
     *开始时间
     */
    StartDate?: Date;
    /**
     *结束时间
     */
    EndDate?: Date;
    /**
     *条码
     */
    BarCode?: string;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *是否审核
     */
    Reviewed?: boolean;
    /**
     *是否完成
     */
    Completed?: boolean;
    /**
     *测试方法 作为结果的measurement name
     */
    Name?: string;
    /**
     *洁净级别
     */
    Classification?: string;
    /**
     *产品1
     */
    Product1?: string;
    /**
     *批次1
     */
    Batch1?: string;
    /**
     *产品2
     */
    Product2?: string;
    /**
     *批次2
     */
    Batch2?: string;
    /**
     *微生物1
     */
    Organism1?: string;
    /**
     *微生物2
     */
    Organism2?: string;
    /**
     *与第一次检查测量相关的读数周期数。
     */
    Reading1Cycle?: number;
    /**
     *与第一次检查测量相关的读数顺序
     */
    Reading1Seq?: number;
    /**
     *与第一次评审测量相关的读数说明。
     */
    Reading1Desc?: number;
    /**
     *与第一次评审测量相关的读数值。
     */
    Reading1Value?: number;
    /**
     *与第一次评审测量相关的读数文本值。
     */
    Reading1Text?: string;
    /**
     *与第一次评审测量相关的读数符号。
     */
    Reading1Sign?: string;
    /**
     *与第一次评审测量相关的读数测量单位。
     */
    Reading1UOM?: string;
    /**
     *与第2次检查测量相关的读数周期数。
     */
    Reading2Cycle?: number;
    /**
     *与第2次检查测量相关的读数顺序
     */
    Reading2Seq?: number;
    /**
     *与第2次评审测量相关的读数说明。
     */
    Reading2Desc?: number;
    /**
     *与第2次评审测量相关的读数值。
     */
    Reading2Value?: number;
    /**
     *与第2次评审测量相关的读数文本值。
     */
    Reading2Text?: string;
    /**
     *与第2次评审测量相关的读数符号。
     */
    Reading2Sign?: string;
    /**
     *与第2次评审测量相关的读数测量单位。
     */
    Reading2UOM?: string;
    /**
     *与第3次检查测量相关的读数周期数。
     */
    Reading3Cycle?: number;
    /**
     *与第3次检查测量相关的读数顺序
     */
    Reading3Seq?: number;
    /**
     *与第3次评审测量相关的读数说明。
     */
    Reading3Desc?: number;
    /**
     *与第3次评审测量相关的读数值。
     */
    Reading3Value?: number;
    /**
     *与第3次评审测量相关的读数文本值。
     */
    Reading3Text?: string;
    /**
     *与第3次评审测量相关的读数符号。
     */
    Reading3Sign?: string;
    /**
     *与第3次评审测量相关的读数测量单位。
     */
    Reading3UOM?: string;
    /**
     *结果id
     */
    ReadingId?: number;
    /**
     *稀释Id
     */
    DilutionId?: number;
    /**
     *结果值
     */
    TestResultValue?: string;
    /**
     *数值结果
     */
    TestResultNumberValue?: number;
    /**
     *读数的粒度值
     */
    ParticleSize?: number;
    /**
     *单位
     */
    UOM?: string;
    /**
     *如果样品存在偏差，则参考最严重的偏差Id
     */
    MostServerDeviationId?: number;
    /**
     *触发限度的记录内容
     */
    MostServerDeviation?: string;
    /**
     *如果样品存在偏差，则参考导致最严重偏差的限值id
     */
    MostServerLimitId?: number;
    /**
     *最严重偏差的限值等级 Info Alert Action
     */
    MostServerLimit?: string;
    /**
     *行动线Id
     */
    ActionLimitId?: number;
    /**
     *行动线数值
     */
    ActionLimitValue?: number;
    /**
     *警戒线id
     */
    AlertLimitId?: number;
    /**
     *警戒线数值
     */
    AlertLimitValue?: number;
    /**
     *行动线操作符号 = >
     */
    ActionLimitOperator?: string;
    /**
     *警戒线符号
     */
    AlertLimitOperator?: string;
    /**
     *与具有测量结果的样本读数相关的低作用极限的标识符id
     */
    LowActionLimitId?: number;
    /**
     *低作用极限的标识符数值
     */
    LowActionLimitValue?: number;
    /**
     *低作用极限的标识
     */
    LowAlertLimitId?: number;
    /**
     *低作用极限的标识符数值
     */
    LowAlertLimitValue?: number;
    /**
     *低作用极限的标识符
     */
    LowActionLimitOperator?: string;
    /**
     *低作用极限的标识符
     */
    LowAlertLimitOperator?: string;
    /**
     *结果值符号
     */
    CSign?: string;
    /**
     *人员样本的代码
     */
    PersonnelSiteCode?: string;
    /**
     *录入结果用户id
     */
    ResultsEnteredUserId?: number;
    /**
     *录入结果用户 employee
     */
    ResultsEnteredEmpId?: string;
    /**
     *录入结果用户名字
     */
    ResultsEnteredUserName?: string;
    /**
     *录入结果用户 姓
     */
    ResultsEnteredFirstName?: string;
    /**
     *录入结果用户 名
     */
    ResultsEnteredLastName?: string;
    /**
     *地点id
     */
    LocationId?: number;
    /**
     *地点全名
     */
    LocationFullName?: string;
    /**
     *与样本关联的位置LOCATION_ID_BREADCRUMB
     */
    LocationIdBreadCrumb?: string;
    /**
     *更新时间
     */
    UpdateDate?: Date;
    /**
     *完成时间
     */
    CompleteDate?: Date;
    /**
     *批准时间
     */
    ApproveDate?: Date;
    /**
     *审核时间
     */
    ReviewedDate?: Date;
    /**
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *采样点
     */
    Site?: Site;
    /**
     *环境
     */
    Environment?: EnvironmentDef;
}

/**
 *RptSampleMart审计 自动生成，请勿修改
 */
export declare class RptSampleMartAudit extends BizEntity {
    /**
     *测试样本id
     */
    SampleId?: number;
    /**
     *计划id
     */
    PlanId?: number;
    /**
     *测试Id
     */
    TestId?: number;
    /**
     *测试点id
     */
    SiteId?: number;
    /**
     *测试点名称
     */
    SiteName?: string;
    /**
     *测试方法id
     */
    TestTypeId?: number;
    /**
     *测试方法名称
     */
    TestTypeName?: string;
    /**
     *开始时间
     */
    StartDate?: Date;
    /**
     *结束时间
     */
    EndDate?: Date;
    /**
     *条码
     */
    BarCode?: string;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *是否审核
     */
    Reviewed?: boolean;
    /**
     *是否完成
     */
    Completed?: boolean;
    /**
     *测试方法 作为结果的measurement name
     */
    Name?: string;
    /**
     *洁净级别
     */
    Classification?: string;
    /**
     *产品1
     */
    Product1?: string;
    /**
     *批次1
     */
    Batch1?: string;
    /**
     *产品2
     */
    Product2?: string;
    /**
     *批次2
     */
    Batch2?: string;
    /**
     *微生物1
     */
    Organism1?: string;
    /**
     *微生物2
     */
    Organism2?: string;
    /**
     *与第一次检查测量相关的读数周期数。
     */
    Reading1Cycle?: number;
    /**
     *与第一次检查测量相关的读数顺序
     */
    Reading1Seq?: number;
    /**
     *与第一次评审测量相关的读数说明。
     */
    Reading1Desc?: number;
    /**
     *与第一次评审测量相关的读数值。
     */
    Reading1Value?: number;
    /**
     *与第一次评审测量相关的读数文本值。
     */
    Reading1Text?: string;
    /**
     *与第一次评审测量相关的读数符号。
     */
    Reading1Sign?: string;
    /**
     *与第一次评审测量相关的读数测量单位。
     */
    Reading1UOM?: string;
    /**
     *与第2次检查测量相关的读数周期数。
     */
    Reading2Cycle?: number;
    /**
     *与第2次检查测量相关的读数顺序
     */
    Reading2Seq?: number;
    /**
     *与第2次评审测量相关的读数说明。
     */
    Reading2Desc?: number;
    /**
     *与第2次评审测量相关的读数值。
     */
    Reading2Value?: number;
    /**
     *与第2次评审测量相关的读数文本值。
     */
    Reading2Text?: string;
    /**
     *与第2次评审测量相关的读数符号。
     */
    Reading2Sign?: string;
    /**
     *与第2次评审测量相关的读数测量单位。
     */
    Reading2UOM?: string;
    /**
     *与第3次检查测量相关的读数周期数。
     */
    Reading3Cycle?: number;
    /**
     *与第3次检查测量相关的读数顺序
     */
    Reading3Seq?: number;
    /**
     *与第3次评审测量相关的读数说明。
     */
    Reading3Desc?: number;
    /**
     *与第3次评审测量相关的读数值。
     */
    Reading3Value?: number;
    /**
     *与第3次评审测量相关的读数文本值。
     */
    Reading3Text?: string;
    /**
     *与第3次评审测量相关的读数符号。
     */
    Reading3Sign?: string;
    /**
     *与第3次评审测量相关的读数测量单位。
     */
    Reading3UOM?: string;
    /**
     *结果id
     */
    ReadingId?: number;
    /**
     *稀释Id
     */
    DilutionId?: number;
    /**
     *结果值
     */
    TestResultValue?: string;
    /**
     *数值结果
     */
    TestResultNumberValue?: number;
    /**
     *读数的粒度值
     */
    ParticleSize?: number;
    /**
     *单位
     */
    UOM?: string;
    /**
     *如果样品存在偏差，则参考最严重的偏差Id
     */
    MostServerDeviationId?: number;
    /**
     *触发限度的记录内容
     */
    MostServerDeviation?: string;
    /**
     *如果样品存在偏差，则参考导致最严重偏差的限值id
     */
    MostServerLimitId?: number;
    /**
     *最严重偏差的限值等级 Info Alert Action
     */
    MostServerLimit?: string;
    /**
     *行动线Id
     */
    ActionLimitId?: number;
    /**
     *行动线数值
     */
    ActionLimitValue?: number;
    /**
     *警戒线id
     */
    AlertLimitId?: number;
    /**
     *警戒线数值
     */
    AlertLimitValue?: number;
    /**
     *行动线操作符号 = >
     */
    ActionLimitOperator?: string;
    /**
     *警戒线符号
     */
    AlertLimitOperator?: string;
    /**
     *与具有测量结果的样本读数相关的低作用极限的标识符id
     */
    LowActionLimitId?: number;
    /**
     *低作用极限的标识符数值
     */
    LowActionLimitValue?: number;
    /**
     *低作用极限的标识
     */
    LowAlertLimitId?: number;
    /**
     *低作用极限的标识符数值
     */
    LowAlertLimitValue?: number;
    /**
     *低作用极限的标识符
     */
    LowActionLimitOperator?: string;
    /**
     *低作用极限的标识符
     */
    LowAlertLimitOperator?: string;
    /**
     *结果值符号
     */
    CSign?: string;
    /**
     *人员样本的代码
     */
    PersonnelSiteCode?: string;
    /**
     *录入结果用户id
     */
    ResultsEnteredUserId?: number;
    /**
     *录入结果用户 employee
     */
    ResultsEnteredEmpId?: string;
    /**
     *录入结果用户名字
     */
    ResultsEnteredUserName?: string;
    /**
     *录入结果用户 姓
     */
    ResultsEnteredFirstName?: string;
    /**
     *录入结果用户 名
     */
    ResultsEnteredLastName?: string;
    /**
     *地点id
     */
    LocationId?: number;
    /**
     *地点全名
     */
    LocationFullName?: string;
    /**
     *与样本关联的位置LOCATION_ID_BREADCRUMB
     */
    LocationIdBreadCrumb?: string;
    /**
     *更新时间
     */
    UpdateDate?: Date;
    /**
     *完成时间
     */
    CompleteDate?: Date;
    /**
     *批准时间
     */
    ApproveDate?: Date;
    /**
     *审核时间
     */
    ReviewedDate?: Date;
    /**
     *环境Id
     */
    EnvironmentId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *Sample审计 自动生成，请勿修改
 */
export declare class SampleAudit extends BizEntity {
    /**
     *父样本Id
     */
    ParentSampleId?: number;
    /**
     *没有文档
     */
    ParentDilutionId?: number;
    /**
     *没有文档
     */
    ParentOrgFoundId?: number;
    /**
     *没有文档
     */
    ScheduledStartDate?: Date;
    /**
     *同Moda
     */
    SamplingCompleted?: boolean;
    /**
     *描述
     */
    Name?: string;
    /**
     *测试方法Id
     */
    TestId?: number;
    /**
     *培养基id
     */
    MediaId?: number;
    /**
     *设备Id
     */
    EquipmentId?: number;
    /**
     *起始时间
     */
    StartDate?: Date;
    /**
     *结束时间
     */
    EndDate?: Date;
    /**
     *被抽样者身上的部位，例如被抽样者手套或长袍上的部位。
     */
    PersonnelSiteId?: number;
    /**
     *取样人员Id
     */
    PersonnelUserId?: number;
    /**
     *分配任务的用户
     */
    PerformedUserId?: number;
    /**
     *条码
     */
    BarCode?: string;
    /**
     *取样体积的测量单位
     */
    MeasurementUOM?: string;
    /**
     *测量体积(如空气采样体积)
     */
    Vloume?: number;
    /**
     *不再测试
     */
    NoTest?: boolean;
    /**
     *是否为阴性对照
     */
    IsNegative?: boolean;
    /**
     *是否批准
     */
    Approved?: boolean;
    /**
     *批准人Id
     */
    ApprovedUserId?: number;
    /**
     *批准日期
     */
    ApprovedDate?: Date;
    /**
     *是否审核
     */
    Reviewed?: boolean;
    /**
     *审核人Id
     */
    ReviewedUserId?: number;
    /**
     *审核日期
     */
    ReviewedDate?: Date;
    /**
     *用户Note信息
     */
    DeviationNote?: string;
    /**
     *任务是否完成
     */
    IsCompleted?: boolean;
    /**
     *完成日期
     */
    CompleteDate?: Date;
    /**
     *是否通过EM测试结果编辑功能编辑
     */
    IsEmEdit?: boolean;
    /**
     *是否通过FDC测试结果编辑功能编辑
     */
    IsFdcEdit?: boolean;
    /**
     *最严重异常
            LimitType Alert Action ......
     */
    MostLevelLimit?: string;
    /**
     *产品附加信息
     */
    ProductNote?: string;
    /**
     *工作流实例Id
     */
    WorkflowInstanceId?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *采样时的环境
     */
    EnvironmentId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *跟踪每个样本的审核活动，将进行审核的用户与审核发生的日期/时间关联起来
 */
export declare class SampleReview extends BizEntity {
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *复核描述
     */
    Note?: string;
    /**
     *审核样本的用户Id
     */
    UserId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *样本
     */
    Sample?: Sample;
    /**
     *审核样本的用户
     */
    User?: User;
}

/**
 *SampleReview审计 自动生成，请勿修改
 */
export declare class SampleReviewAudit extends BizEntity {
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *复核描述
     */
    Note?: string;
    /**
     *审核样本的用户Id
     */
    UserId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试结果 Note
 */
export declare class TableNote extends BizEntity {
    /**
     *输入用户Id
     */
    UserId?: number;
    /**
     *输入日期
     */
    EnteryDate?: Date;
    /**
     *Note内容
     */
    Note?: string;
    /**
     *关联表类型
     */
    TableName?: string;
    /**
     *关联表Id
     */
    TableId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *关联用户Id
     */
    User?: User;
}

/**
 *TableNote审计 自动生成，请勿修改
 */
export declare class TableNoteAudit extends BizEntity {
    /**
     *输入用户Id
     */
    UserId?: number;
    /**
     *输入日期
     */
    EnteryDate?: Date;
    /**
     *Note内容
     */
    Note?: string;
    /**
     *关联表类型
     */
    TableName?: string;
    /**
     *关联表Id
     */
    TableId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *关联的currentworkspace medias
 */
export declare class WorkSpaceEquipment extends BizEntity {
    /**
     *当前workspace id
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的设备
     */
    EquipmentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *关联的CurrentworkSpace
     */
    CurrentWorkSpace?: CurrentWorkSpace;
    /**
     *关联的设备
     */
    Equipment?: Equipment;
}

/**
 *WorkSpaceEquipment审计 自动生成，请勿修改
 */
export declare class WorkSpaceEquipmentAudit extends BizEntity {
    /**
     *当前workspace id
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的设备
     */
    EquipmentId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *关联的currentworkspace medias
 */
export declare class WorkSpaceMedia extends BizEntity {
    /**
     *当前workspace id
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的耗材ID
     */
    MediaId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *关联的CurrentworkSpace
     */
    CurrentWorkSpace?: CurrentWorkSpace;
    /**
     *关联的耗材
     */
    Media?: Media;
}

/**
 *WorkSpaceMedia审计 自动生成，请勿修改
 */
export declare class WorkSpaceMediaAudit extends BizEntity {
    /**
     *当前workspace id
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的耗材ID
     */
    MediaId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *关联sample 的菌种
 */
export declare class WorkSpaceOrganism extends BizEntity {
    /**
     *当前SampleId
     */
    SampleId?: number;
    /**
     *当前SampleId
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的Organism Id
     */
    OrganismId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *关联的Sample
     */
    Sample?: Sample;
    /**
     *关联的Sample
     */
    CurrentWorkSpace?: CurrentWorkSpace;
    /**
     *关联的Organism
     */
    Organism?: Organism;
}

/**
 *WorkSpaceOrganism审计 自动生成，请勿修改
 */
export declare class WorkSpaceOrganismAudit extends BizEntity {
    /**
     *当前SampleId
     */
    SampleId?: number;
    /**
     *当前SampleId
     */
    CurrentWorkSpaceId?: number;
    /**
     *关联的Organism Id
     */
    OrganismId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *测试产品
 */
export declare class WorkSpaceProduct extends BizEntity {
    /**
     *产品Id
     */
    ProductId?: number;
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *样本任务Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *历史记录
     */
    BatchRecordId?: number;
    /**
     *批次Id
     */
    BatchId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *产品
     */
    Product?: Product;
    /**
     *样本
     */
    Sample?: Sample;
    /**
     *样本任务
     */
    CurrentWorkSpace?: CurrentWorkSpace;
    /**
     *批次
     */
    Batch?: Batch;
}

/**
 *WorkSpaceProduct审计 自动生成，请勿修改
 */
export declare class WorkSpaceProductAudit extends BizEntity {
    /**
     *产品Id
     */
    ProductId?: number;
    /**
     *样本Id
     */
    SampleId?: number;
    /**
     *样本任务Id
     */
    CurrentWorkSpaceId?: number;
    /**
     *历史记录
     */
    BatchRecordId?: number;
    /**
     *批次Id
     */
    BatchId?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *当前任务状态
 */
export declare type CurrentWorkStatus = "Scheduled" | "Assigned" | "Pending" | "Completed" | "Failed";

/**
 *计划状态
 */
export declare type PlanStatus = "Draft" | "Approved" | "Effective" | "Superceded" | "Retired";

/**
 *设备数据状态
 */
export declare type EquipmentDataStatus = "None" | "Matched" | "Obsoleted";

/**
 *粒子计数器数据
 */
export declare class CompareDataRemark extends BizEntity {
    /**
     *输入用户Id
     */
    UserId?: number;
    /**
     *备注信息
     */
    Remark?: string;
    /**
     *备注时间
     */
    RemarkDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *测试样本Id
     */
    ParticleCounterDataId?: number;
    /**
     *关联用户Id
     */
    User?: User;
    /**
     *测试样本
     */
    ParticleCounterData?: ParticleCounterData;
}

/**
 *CompareDataRemark审计 自动生成，请勿修改
 */
export declare class CompareDataRemarkAudit extends BizEntity {
    /**
     *输入用户Id
     */
    UserId?: number;
    /**
     *备注信息
     */
    Remark?: string;
    /**
     *备注时间
     */
    RemarkDate?: Date;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
    /**
     *创建人
     */
    CreateBy?: number;
    /**
     *更新人
     */
    UpdateBy?: number;
    /**
     *测试样本Id
     */
    ParticleCounterDataId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *子流程映射对象
 */
export declare class SubWorkflowTemplateMapping extends BizEntity {
    /**
     *步骤Id
     */
    NodeId?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *父流程Id
     */
    ParentId?: number;
    /**
     *子流程Id
     */
    ChindId?: number;
    /**
     *父流程
     */
    Parent?: WorkflowTemplate;
    /**
     *子流程
     */
    Chind?: WorkflowTemplate;
}

/**
 *SubWorkflowTemplateMapping审计 自动生成，请勿修改
 */
export declare class SubWorkflowTemplateMappingAudit extends BizEntity {
    /**
     *步骤Id
     */
    NodeId?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *父流程Id
     */
    ParentId?: number;
    /**
     *子流程Id
     */
    ChindId?: number;
    /**
     *操作
     */
    Action?: AuditAction;
    /**
     *操作时间
     */
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

/**
 *工作流参数
 */
export declare class WorkflowParameter extends BizEntity {
    /**
     *工作流实例Id
     */
    WorkflowInstanceId?: string;
    /**
     *工作流上下文Json
     */
    ContextJson?: string;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
 *流程模板
 */
export declare class WorkflowTemplate extends BizEntity {
    /**
     *流程图Json信息
     */
    GraphJson?: string;
    /**
     *执行Json信息
     */
    ExecutorJson?: string;
    /**
     *工作流定义Id
     */
    WorkflowId?: string;
    /**
     *工作流类别
     */
    Category?: number;
    /**
     *工作流名称
     */
    Name?: string;
    /**
     *备注
     */
    Description?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
     *父流程映射
     */
    ParentsMapping?: Array<SubWorkflowTemplateMapping>;
    /**
     *父流程
     */
    Parents?: Array<WorkflowTemplate>;
    /**
     *子流程映射
     */
    ChildrenMapping?: Array<SubWorkflowTemplateMapping>;
    /**
     *子流程
     */
    Children?: Array<WorkflowTemplate>;
}

/**
 *WorkflowTemplate审计 自动生成，请勿修改
 */
export declare class WorkflowTemplateAudit extends BizEntity {
    /**
     *流程图Json信息
     */
    GraphJson?: string;
    /**
     *执行Json信息
     */
    ExecutorJson?: string;
    /**
     *工作流定义Id
     */
    WorkflowId?: string;
    /**
     *工作流类别
     */
    Category?: number;
    /**
     *工作流名称
     */
    Name?: string;
    /**
     *备注
     */
    Description?: string;
    /**
     *版本
     */
    Version?: number;
    /**
     *创建时间
     */
    CreatedTime?: Date;
    /**
     *更新时间
     */
    UpdatedTime?: Date;
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
    AuditTime?: Date;
    /**
     *实体Id
     */
    EntityId?: number;
}

export declare type ExecuteTime = "AutoExecuteCompleted" | "AutoOpenPage" | "Manual" | "ManualAutoFillTime";

export declare type ApprovalResult = "None" | "Approved" | "Disapprove" | "Withdraw";



/**
 *审计操作
 */
export declare type AuditAction = "Insert" | "Update" | "Delete";

/**
 *限度登记
 */
export declare type LimitLevel = "None" | "Alert" | "Action" | "Others";

export const ActivedEntities = [
    'DataType',
    'Sign',
    'DeviceConfig',
    'Equipment',
    'Location',
    'EquipmentType',
    'UnitOfMeasure',
    'EquipmentV',
    'LimitToken',
    'LimitType',
    'Classification',
    'VisioDiagram',
    'Site',
    'LocationType',
    'LocationV',
    'SiteType',
    'SiteV',
    'GrowthPromotionStatus',
    'Media',
    'MediaType',
    'MediaV',
    'Organism',
    'OrganismGenus',
    'OrganismSeverity',
    'SeverityLocation',
    'OrganismChar',
    'OrganismType',
    'OrgFoundMethod',
    'PreDefinedNote',
    'Product',
    'Cycle',
    'TimeFrame',
    'EnvironmentDef',
    'LimitDef',
    'TestType',
    'LimitRuleGroup',
    'LimitRule',
    'Measurement',
    'TimeFrameDilution',
    'MeasurementListDef',
    'MeasurementSign',
    'MeasurementUOM',
    'MeasurementCycle',
    'PersonnelSite',
    'TestCategory',
    'TestClass',
    'TestStage',
    'TestTypeCode',
    'TestTypeLabel',
    'TestTypeEquipment',
    'TestTypeMedia',
    'TestTypeV',
    'TimeFrameEvent',
    'Map',
    'MapCategory',
    'VisioLocation',
    'VisioSite',
    'SampleV',
    'Department',
    'User',
    'ESignConfig',
    'Role',
    'UserV',
    'ApprovalItem',
    'Notification',
    'NotificationType',
    'Subscription',
    'SubscriptionV',
    'Sample',
    'Deviation',
    'Batch',
    'CurrentWorkSpace',
    'Test',
    'Plan',
    'Group',
    'FileAttachment',
    'GeneralPoolV',
    'Limit',
    'LimitExceeded',
    'NotificationFrequency',
    'PlanGroup',
    'TestFrequencyType',
    'TestFrequency',
    'TestFrequencyOccurrence',
    'Reading',
    'SampleReview',
    'TableNote',
    'WorkSpaceEquipment',
    'WorkSpaceMedia',
    'WorkSpaceOrganism',
    'WorkSpaceProduct',
    'SubWorkflowTemplateMapping',
    'WorkflowTemplate'];
