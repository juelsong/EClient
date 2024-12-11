import { Location, Site } from "./location";
import { oDataQuery } from "@/utils/odata";
import { OdataQuery as oDataOptions } from "odata";

export declare interface VisioLocation {
  Id?: number;
  LocationId: number;
  VisioDiagramId?: number;
  X: number;
  Y: number;
  Width: number;
  Height: number;
  Location?: Location;
}
export declare interface VisioSite {
  Id?: number;
  SiteId: number;
  VisioDiagramId?: number;
  X: number;
  Y: number;
  Site?: Site;
}
export declare interface VisioDiagram {
  Id?: number;
  MapId: number;
  VisioLocations: Array<VisioLocation>;
  VisioSites: Array<VisioSite>;
}

export async function getVisioDiagramByMapId(
  mapId: number,
  locationIds: number[]
) {
  const visioLocationFilter = ["IsActive eq true"];
  const visioSiteFilter = ["IsActive eq true"];
  if (locationIds.length > 0) {
    visioLocationFilter.push(`Location/Id in [${locationIds.join()}]`); //(Site/LocationId eq 24
    visioSiteFilter.push(`Site/LocationId in [${locationIds.join()}]`);
  }
  const visionOptions: oDataOptions = {
    $expand: `VisioLocations($select=Id,LocationId,X,Y,Width,Height;$filter=${visioLocationFilter
      .map((f) => `(${f})`)
      .join(
        " and "
      )};$expand=Location($select=Id,Name,ParentId;$expand=LocationType($select=Id,Icon))),VisioSites($select=Id,SiteId,X,Y;$filter=${visioSiteFilter
      .map((f) => `(${f})`)
      .join(
        " and "
      )};$expand=Site($select=Id,Name,LocationId;$expand=SiteType($select=Id,Icon)))`,
    $select: "Id,MapId",
    $top: 1,
    $filter: `(IsActive eq true) and (MapId eq ${mapId})`,
  };
  const visioResults = await oDataQuery("VisioDiagram", visionOptions);
  const visios = visioResults.value as Array<VisioDiagram>;
  return visios.length == 0 ? undefined : visios[0];
}
