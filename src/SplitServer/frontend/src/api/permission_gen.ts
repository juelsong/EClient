import { requestRaw, request } from "@/utils/request";
import { Result } from "@/defs/Model";

export function queryGenerate(locale?: string) {
  const query = new Array<string>();
  if (locale !== undefined) {
    query.push(`locale=${locale}`);
  }
  const queryStr = query.length > 0 ? `?${query.join("&")}` : "";
  //TODO check request&response body
  return requestRaw({
    url: `/Permission/Generate${(queryStr.length > 0 ? queryStr : "")}`,
    method: "get"
  })
}

export function queryPermission() {
  //TODO check request&response body
  return requestRaw({
    url: `/Permission`,
    method: "get"
  })
}

export function editPermission() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/Permission`,
    method: "patch"
  })
}
