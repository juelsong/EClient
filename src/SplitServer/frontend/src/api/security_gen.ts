import { request } from "@/utils/request";
import { SecurityModel, Result, PasswordRule } from "@/defs/Model";

export function queryPasswordRule(locale?: string) {
  const query = new Array<string>();
  if (locale !== undefined) {
    query.push(`locale=${locale}`);
  }
  const queryStr = query.length > 0 ? `?${query.join("&")}` : "";
  return request.request<Result<PasswordRule[]>, Result<PasswordRule[]>>({
    url: `/api/user/password-rule${(queryStr.length > 0 ? queryStr : "")}`,
    method: "get"
  })
}

export function editSecurityConfig(data: SecurityModel) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/security-config`,
    method: "patch",
    data
  })
}

export function querySecurityConfig() {
  return request.request<Result<SecurityModel>, Result<SecurityModel>>({
    url: `/api/user/security-config`,
    method: "get"
  })
}
