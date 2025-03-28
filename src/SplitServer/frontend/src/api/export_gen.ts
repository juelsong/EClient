import { requestRaw } from "@/utils/request";

export function createExcel(type: string) {
  //TODO check request&response body
  return requestRaw({
    url: `/Export/excel/${type}`,
    method: "post"
  })
}

export function queryExcel(type: string) {
  //TODO check request&response body
  return requestRaw({
    url: `/Export/excel/${type}`,
    method: "get"
  })
}
