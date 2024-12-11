import { request } from "@/utils/request";
import { LoginParams, ChangePassSelf, UserSettings, Result, LoginResult, UserInfo } from "@/defs/Model";
import { User } from "@/defs/Entity";

export function createLogin(data: LoginParams) {
  return request.request<Result<LoginResult>, Result<LoginResult>>({
    url: `/api/user/login`,
    method: "post",
    data
  })
}

export function queryInfo() {
  return request.request<Result<UserInfo>, Result<UserInfo>>({
    url: `/api/user/info`,
    method: "get"
  })
}

export function queryRandomImage(key: string) {
  return request.request<Result<string>, Result<string>>({
    url: `/api/user/random-image/${key}`,
    method: "get"
  })
}

export function createLogout() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/logout`,
    method: "post"
  })
}

export function createUser(data: User) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user`,
    method: "post",
    data
  })
}

export function editPassword(data: User) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/password`,
    method: "patch",
    data
  })
}

export function editSelfPassword(data: ChangePassSelf) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/self-password`,
    method: "patch",
    data
  })
}

export function editUserSettings(data: UserSettings) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/user-settings`,
    method: "patch",
    data
  })
}

export function createToken() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/api/user/token`,
    method: "post"
  })
}
