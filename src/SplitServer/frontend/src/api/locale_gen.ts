import { request } from "@/utils/request";
import { UpdateLocalParam, Result } from "@/defs/Model";

export function queryGetUserLocale() {
  return request.request<Result, Result>({
    url: `/Locale/GetUserLocale`,
    method: "get"
  })
}

export function createUpDateLocale(data: UpdateLocalParam) {
  return request.request<Result, Result>({
    url: `/Locale/UpDateLocale`,
    method: "post",
    data
  })
}
