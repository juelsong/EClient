import * as Cookies from "@/utils/cookies";
const TokenKey = 'EMIS-Token';
const RefreshTokenKey = 'EMIS-RefreshToken';

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  return Cookies.set(TokenKey, token)
}
export function getRefreshToken() {
  return Cookies.get(RefreshTokenKey)
}

export function setRefreshToken(token) {
  return Cookies.set(RefreshTokenKey, token)
}

export function removeRefreshToken() {
  return Cookies.set(TokenKey)
}

export function removeToken() {
  return Cookies.set(RefreshTokenKey)
}
