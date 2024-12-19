export type EquipmentQueryModel = {
  equipmentId?: number;
  locationId?: number;
  startDate?: Date;
  endDate?: Date;
  //设备名称
  EquipmentName?: string;

  
};

export type LocationQueryModel = {
  equipmentId?: number;
  locationId?: number;
  startDate?: Date;
  endDate?: Date;
  IsActive?: boolean;
};
