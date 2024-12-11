import { BatchQuery, oDataBatchQuery } from "@/utils/odata";

export const LocationTreeNodeTypeLocation = 1;
export const LocationTreeNodeTypeSite = 2;

export declare interface Meta {
  TypeName: string; //各自Type的名称,
  Icon: string; // 各自Type的图标,
  Weight?: number; // 只对Location有效，值为LocationType的Weight，
}

export declare interface LocationType {
  Id: number;
  Name: string;
  Icon: string;
  Weight?: number;
}

export declare interface SiteType {
  Id: number;
  Name: string;
  Icon: string;
}

export declare type LocationTreeChildNode = Location | Site;

export declare interface LocationTreeNode {
  /**
   * 树节点Id，避免不同类型实体主键冲突，结合了自身的Id和ParentId和NodeType作为树节点的key `${NodeType}_${l.Id}_${l.ParentId}`
   */
  NodeKey?: string;
  /** 区分树节点类型 1为Location,2为Site*/
  NodeType?: number;
  IsActive: boolean;
  Meta: Meta;
  Children?: Array<LocationTreeChildNode>;
  Barcode?: string;
  Parent?: Location;
  CreatedTime: string;
  CreateBy: number;
  UpdatedTime?: string;
  UpdateBy?: number;
  ClassificationId?: number;
  SecondClassificationId?: number;
}

export declare interface Location extends LocationTreeNode {
  Id: number;
  Name: string;
  Description?: string;
  ParentId: number | null;
  LocationType: LocationType;
  Code?: string;
}
export declare interface Site extends LocationTreeNode {
  Id: number;
  Name: string;
  Description?: string;
  LocationId: number;
  SiteType: SiteType;
}
export function getSiteNodeKey(site: Site) {
  return `${LocationTreeNodeTypeSite}_${site.Id}_${site.LocationId}`;
}
export function getLocationNodeKey(location: Location) {
  return `${LocationTreeNodeTypeLocation}_${location.Id}_${location.ParentId}`;
}

export const locationQueryExpand =
  "LocationType($select=Name,Icon,Weight),VisioDiagram($select=Id,MapId)";
export const siteQueryExpand =
  "SiteType($select=Name,Icon)";

export async function getLocationTree(
  showDeactive: boolean,
  withSite: boolean,
  locationIds: number[]
) {
  const locationFilterArray = new Array<string>();
  const siteFilterArray = new Array<string>();
  if (true !== showDeactive) {
    locationFilterArray.push("IsActive eq true");
    siteFilterArray.push("IsActive eq true");
  }
  if (locationIds.length > 0) {
    const filterLocationIds = locationIds.join();
    locationFilterArray.push(`Id in [${filterLocationIds}]`);
    siteFilterArray.push(`LocationId in [${filterLocationIds}]`);
  }
  const batch: BatchQuery = {
    Location: {
      $expand: locationQueryExpand,
      $count: false,
    },
  };
  if (locationFilterArray.length > 0) {
    batch.Location.$filter = locationFilterArray
      .map((f) => `(${f})`)
      .join(" And ");
  }
  if (withSite) {
    batch.Site = {
      $expand: siteQueryExpand,
      $count: false,
      $orderby: "Name",
    };
    if (siteFilterArray.length > 0) {
      batch.Site.$filter = siteFilterArray.map((f) => `(${f})`).join(" And ");
    }
  }

  const ret = await oDataBatchQuery(batch);
  const locations = ret[0].body.value as Location[];

  locations.forEach((location) => {
    location.NodeKey = getLocationNodeKey(location);
    location.Children = new Array<Location | Site>();
    location.NodeType = LocationTreeNodeTypeLocation;
    location.Meta = {
      TypeName: location.LocationType.Name,
      Icon: location.LocationType.Icon,
      Weight: location.LocationType.Weight,
    };
  });
  locations.forEach((location) => {
    if (location.ParentId) {
      const parent = locations.find((p) => p.Id == location.ParentId);
      parent?.Children!.push(location);
      location.Parent = parent;
    }
  });
  if (withSite) {
    const sites = ret[1].body.value as Site[];
    sites.forEach((site) => {
      site.NodeType = LocationTreeNodeTypeSite;
      site.NodeKey = getSiteNodeKey(site);
      site.Meta = {
        TypeName: site.SiteType.Name,
        Icon: site.SiteType.Icon,
      };
      const location = locations.find((l) => l.Id == site.LocationId);
      site.Parent = location;
      location?.Children!.push(site);
    });
  }
  return locations.filter((l) => l.Parent === undefined);
}

export async function getLocationTreeByBreadcrumb(
  showDeactive: boolean,
  withSite: boolean,
  locationId?: number
) {
  const locationFilterArray = new Array<string>();
  const siteFilterArray = new Array<string>();
  if (true !== showDeactive) {
    locationFilterArray.push("IsActive eq true");
    siteFilterArray.push("IsActive eq true");
  }
  if (locationId) {
    locationFilterArray.push(
      `contains(LocationExtra/Breadcrumb, '${locationId}')`
    );
    siteFilterArray.push(
      `contains(Location/LocationExtra/Breadcrumb, '${locationId}')`
    );
  }
  const batch: BatchQuery = {
    Location: {
      $expand: locationQueryExpand,
      $count: false,
    },
  };
  if (locationFilterArray.length > 0) {
    batch.Location.$filter = locationFilterArray
      .map((f) => `(${f})`)
      .join(" And ");
  }
  if (withSite) {
    batch.Site = {
      $expand: siteQueryExpand,
      $count: false,
      $orderby: "Name",
    };
    if (siteFilterArray.length > 0) {
      batch.Site.$filter = siteFilterArray.map((f) => `(${f})`).join(" And ");
    }
  }

  const ret = await oDataBatchQuery(batch);
  const locations = ret[0].body.value as Location[];

  locations.forEach((location) => {
    location.NodeKey = getLocationNodeKey(location);
    location.Children = new Array<Location | Site>();
    location.NodeType = LocationTreeNodeTypeLocation;
    location.Meta = {
      TypeName: location.LocationType.Name,
      Icon: location.LocationType.Icon,
      Weight: location.LocationType.Weight,
    };
  });
  locations.forEach((location) => {
    if (location.ParentId) {
      const parent = locations.find((p) => p.Id == location.ParentId);
      parent?.Children!.push(location);
      location.Parent = parent;
    }
  });
  if (withSite) {
    const sites = ret[1].body.value as Site[];
    sites.forEach((site) => {
      site.NodeType = LocationTreeNodeTypeSite;
      site.NodeKey = getSiteNodeKey(site);
      site.Meta = {
        TypeName: site.SiteType.Name,
        Icon: site.SiteType.Icon,
      };
      const location = locations.find((l) => l.Id == site.LocationId);
      site.Parent = location;
      location?.Children!.push(site);
    });
  }
  if (locationId) {
    return locations.filter((l) => l.Id == locationId);
  } else {
    return locations.filter((l) => l.Parent === undefined);
  }
}
