export type EquipmentQueryModel = {
  equipmentId?: number;
  locationId?: number;
  startDate?: Date;
  endDate?: Date;
  //设备名称
  EquipmentName?: string;
  isOperation?: boolean;
  equipmentIdAndName?:string;
};

export type LocationQueryModel = {
  equipmentId?: number;
  locationId?: number;
  startDate?: Date;
  endDate?: Date;
  IsActive?: boolean;
};
