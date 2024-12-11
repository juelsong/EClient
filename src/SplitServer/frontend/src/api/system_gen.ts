import { request } from "@/utils/request";
import { LDAPConfig, DashboardLayout, Result } from "@/defs/Model";

export function createUserManagementLdap(data: LDAPConfig) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/System/UserManagementLdap`,
    method: "post",
    data,
  });
}

export function createUserManagementEmis() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/System/UserManagementEmis`,
    method: "post",
  });
}

export function editDefaultDashboard() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/System/DefaultDashboard`,
    method: "patch",
  });
}

export function queryDefaultDashboard() {
  return request.request<Result<string[]>, Result<string[]>>({
    url: `/System/DefaultDashboard`,
    method: "get",
  });
}

export function editDefaultLayout(data: DashboardLayout) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/System/DefaultLayout`,
    method: "patch",
    data,
  });
}

export function queryDefaultLayout() {
  return request.request<Result<DashboardLayout>, Result<DashboardLayout>>({
    url: `/System/DefaultLayout`,
    method: "get",
  });
}

export function editComponents() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/System/Components`,
    method: "patch",
  });
}

export function queryComponents() {
  return request.request<Result<string[]>, Result<string[]>>({
    url: `/System/Components`,
    method: "get",
  });
}

export function querySystemConfig() {
  return request.request<
    Result<Record<string, string>>,
    Result<Record<string, string>>
  >({
    url: `/System/SystemConfig`,
    method: "get",
  });
}