import { getToken, getRefreshToken } from "@/utils/auth";
import moment from "moment";

export function getBaseUrl(): string {
  if (process.env.NODE_ENV === "development") {
    return process.env.VUE_APP_API_BASE_URL!;
  } else {
    return (window as any).getBaseUrl();
  }
}

export function getTenant(): string {
  if (process.env.NODE_ENV === "development") {
    return process.env.VUE_APP_TENANT!;
  } else {
    return (window as any).getTenant();
  }
}

export function getTitle(): string {
  if (process.env.NODE_ENV === "development") {
    return process.env.VUE_APP_TITLE!;
  } else {
    return (window as any).getTitle();
  }
}

declare type TokenObj = {
  Account: string;
  aud: string;
  exp: number;
  iat: number;
  iss: string;
};
export function getExpDateFromToken(tokenStr?: string) {
  if (tokenStr) {
    const token: string = tokenStr.replace(/_/g, "/").replace(/-/g, "+");
    const jsonStr = decodeURIComponent(
      escape(window.atob(token.split(".")[1]))
    );
    const tokenObj = JSON.parse(jsonStr) as TokenObj;
    if (tokenObj.exp) {
      return moment(new Date(tokenObj.exp * 1000));
    }
  }
  return undefined;
}

export function getHeader() {
  const headers = new Headers({ "X-TENANT": getTenant() });
  const token = getToken();
  const refreshToken = getRefreshToken();
  if (token) {
    headers.append("Authorization", `Bearer ${token}`);
  }
  const tokenExp = getExpDateFromToken(token);
  if (tokenExp) {
    const now = moment();
    if (tokenExp.isBefore(now) && refreshToken) {
      headers.append("X-Authorization", `Bearer ${refreshToken}`);
    }
  }
  return headers;
}
