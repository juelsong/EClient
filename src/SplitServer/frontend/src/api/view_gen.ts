import { request, requestRaw } from "@/utils/request";
import { Result } from "@/defs/Model";

export function queryVersion() {
  return request.request<Result<string>, Result<string>>({
    url: `/View/Version`,
    method: "get"
  })
}

export function queryAssemblies() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/Assemblies`,
    method: "get"
  })
}

export function queryAssembilesMeta() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/AssembilesMeta`,
    method: "get"
  })
}

export function queryEsignCategorys() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/EsignCategorys`,
    method: "get"
  })
}

export function queryErrorCodes() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/ErrorCodes`,
    method: "get"
  })
}

export function queryErrorCodeFunction() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/ErrorCodeFunction`,
    method: "get"
  })
}

export function queryErrorPrompts() {
  //TODO check request&response body
  return requestRaw({
    url: `/View/ErrorPrompts`,
    method: "get"
  })
}
