import { requestRaw } from "@/utils/request";

export function createExcels(type: string) {
  //TODO check request&response body
  return requestRaw({
    url: `/Import/excels/${type}`,
    method: "post"
  })
}

export function createExcel(type: string, hasHeader?: boolean) {
  const query = new Array<string>();
  if (hasHeader !== undefined) {
    query.push(`hasHeader=${hasHeader}`);
  }
  const queryStr = query.length > 0 ? `?${query.join("&")}` : "";
  //TODO check request&response body
  return requestRaw({
    url: `/Import/excel/${type}${(queryStr.length > 0 ? queryStr : "")}`,
    method: "post"
  })
}
