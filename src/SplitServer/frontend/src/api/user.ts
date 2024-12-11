import { request, requestRaw, requestWithoutToken } from "@/utils/request";
import { LoginParams, Result, UserInfo } from "@/defs/Model";
import { AxiosRequestConfig } from "axios";

export function login(data: LoginParams, baseUrl?: string) {
  const param: AxiosRequestConfig = {
    url: "/api/user/login",
    method: "post",
    data,
  };
  if (baseUrl) {
    param.baseURL = baseUrl;
  }
  return requestWithoutToken(param);
}

export function tryVerifyToken(baseUrl: string) {
  return new Promise<boolean | undefined>((resolve, reject) => {
    requestRaw
      .request<Result<UserInfo>, Result<UserInfo>>({
        baseURL: baseUrl,
        url: "/api/user/info",
        method: "get",
      })
      .then((rsp) => {
        resolve(true);
      })
      .catch((err) => {
        if (err && err.response) {
          if (err.response.status == 401) {
            resolve(false);
            return;
          }
        }
        resolve(undefined);
      });
  });
}

export function getInfo() {
  return request.request<Result<UserInfo>, Result<UserInfo>>({
    url: "/api/user/info",
    method: "get",
  });
}

export function logout() {
  return request.request<Result<boolean>, Result<boolean>>({
    url: "/api/user/logout",
    method: "post",
  });
}

export function randomImg(timestamp: string | number, baseUrl?: string) {
  const param: AxiosRequestConfig = {
    url: `/api/user/random-image/${timestamp}`,
    method: "get",
  };
  if (baseUrl) {
    param.baseURL = baseUrl;
  }
  return request.request<Result<string>, Result<string>>(param);
}
