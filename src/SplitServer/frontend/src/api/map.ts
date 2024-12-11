import { oDataQuery } from "@/utils/odata";
import { OdataQuery as oDataOptions } from "odata";
export const MapTreeNodeTypeCategory = 1;
export const MapTreeNodeTypeMap = 2;

export declare interface MapTreeNode {
  /**
   * 树节点Id，避免不同类型实体主键冲突，规则为MapCategory->`CATEGORY_${entity.Id}`  Map->`MAP_${entity.Id}`
   */
  NodeKey?: string;
  /** 区分树节点类型 1为MapCategory,2为Map*/
  NodeType?: number;
  /**是否启用 */
  IsActive: boolean;
}

export declare interface MapCategory extends MapTreeNode {
  Id: number;
  Name: string;
  Description?: string;
  ParentId?: number;
  Maps: Array<Map>;
  Children: Array<Map | MapCategory>;
}

export declare interface Map extends MapTreeNode {
  Id: number;
  Name: string;
  Description?: string;
  Path: string;
  MapCategoryId: number;
}

export async function getMapTree(showDeactive: boolean) {
  const mapCategoryOptions: oDataOptions = {
    $filter : "IsActive eq true",
    $select: "Id,Name,Description,ParentId,IsActive",
    $expand: `Maps($select=Id,Name,Description,Path,IsActive,MapCategoryId${
      showDeactive ? "" : ";$filter=IsActive eq true"
    })`,
  };
  // if (true !== showDeactive) {
  //   mapCategoryOptions.$filter = "IsActive eq true";
  // }
  const categoryResult = await oDataQuery("MapCategory", mapCategoryOptions);
  const mapCategorys = categoryResult.value as Array<MapCategory>;

  mapCategorys.forEach((mc) => {
    mc.Maps.forEach((m) => {
      m.NodeKey = `MAP_${m.Id}`;
      m.NodeType = MapTreeNodeTypeMap;
    });
    mc.NodeKey = `CATEGORY_${mc.Id}`;
    mc.Children = new Array<Map | MapCategory>();
    mc.Children.push(...mc.Maps);
    mc.NodeType = MapTreeNodeTypeCategory;
  });
  mapCategorys.forEach((mc) => {
    if (mc.ParentId) {
      const parent = mapCategorys.find((p) => p.Id == mc.ParentId);
      parent?.Children.push(mc);
    }
  });
  return mapCategorys.filter((mc) => mc.ParentId == undefined);
}
