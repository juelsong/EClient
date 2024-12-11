import axios, { AxiosResponse } from "axios";
import { ElMessage } from "element-plus";
import { setToken, setRefreshToken } from "@/utils/auth";
import { getBaseUrl, getTenant, getHeader } from "@/utils/global";
import { app } from "@/main";
import { checkEsignReply } from "./esign-inject";
import { i18n } from "@/i18n";
import * as vue from "vue";
import store from "@/store";
// create an axios instance
axios.defaults.headers.post["Content-Type"] = "application/json";

const service = axios.create({
  headers: {
    "X-TENANT": getTenant(),
  },
  baseURL: getBaseUrl(), // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 500000, // request timeout
});

const serviceRaw = axios.create({
  headers: {
    "X-TENANT": getTenant(),
  },
  responseType: "blob",
  baseURL: getBaseUrl(), // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 500000, // request timeout
});
const serviceWithoutToken = axios.create({
  headers: {
    "X-TENANT": getTenant(),
  },
  baseURL: getBaseUrl(), // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 500000, // request timeout
});

vue.watch(
  () => store.state.system.IsClientOffline,
  () => {
    service.defaults.baseURL =
      serviceRaw.defaults.baseURL =
      serviceWithoutToken.defaults.baseURL =
        getBaseUrl();
  }
);

let headerCount: string | undefined = undefined;
const authReq = (config) => {
  const header = getHeader();
  header.forEach((v, k) => {
    const lower = k.toLocaleLowerCase();
    const upper = k.toUpperCase();
    const configHeaderKeys = Object.keys(config.headers);
    if (configHeaderKeys.includes(lower)) {
      config.headers[lower] = v;
    } else if (configHeaderKeys.includes(upper)) {
      config.headers[upper] = v;
    } else {
      config.headers[k] = v;
    }
  });
  headerCount = config.headers["X-ESIGN-COUNT"];
  return config;
};

const errorReq = (error) => {
  // do something with request error
  console.log(error); // for debug
  return Promise.reject(error);
};

function errorRsp(showPromit) {
  return (error) => {
    if (error && error.response) {
      if (error.response.status == 401) {
        ElMessage({
          message: i18n.global.t("prompt.invalidToken"),
          type: "error",
          duration: 3 * 1000,
          onClose: () => {
            // 有可能连续request needRelogin 防抖
            // to re-login
            store.dispatch("user/resetToken").then(() => {
              location.reload();
            });
          },
        });
        return;
      }
      if (error.response.status == 403) {
        ElMessage({
          message: i18n.global.t("prompt.invalidPermission"),
          type: "error",
          duration: 3 * 1000,
        });
        return;
      }
    }
    // TODO 多语言  呵呵
    console.log("err" + error); // for debug
    if (showPromit) {
      ElMessage({
        message: error.message,
        type: "error",
        duration: 5 * 1000,
      });
    }
    return Promise.reject(error);
  };
}

function processToken(response: AxiosResponse<any, any>) {
  const store = app.config.globalProperties.$store;
  const token = response.headers["access-token"];
  const refreshToken = response.headers["x-access-token"];
  if (token) {
    setToken(token);
    store.commit("user/SET_TOKEN", token);
  }
  if (refreshToken) {
    setRefreshToken(refreshToken);
    store.commit("user/SET_REFRESH_TOKEN", refreshToken);
  }
}

function processResponseData(response: AxiosResponse<any, any>) {
  const res = response.data;
  if (res.code == undefined) {
    return Promise.resolve(res);
  }
  return checkEsignReply(
    res,
    response.request.responseURL,
    response.config.method as string,
    headerCount
  );
}

// request interceptor
service.interceptors.request.use(authReq, errorReq);

// response interceptor
service.interceptors.response.use((response) => {
  processToken(response);
  return processResponseData(response);
}, errorRsp(true));

serviceRaw.interceptors.request.use(authReq, errorReq);
serviceRaw.interceptors.response.use(undefined, errorRsp(false));

serviceWithoutToken.interceptors.request.use(authReq, errorReq);
serviceWithoutToken.interceptors.response.use((response) => {
  return new Promise((resolve, reject) => {
    const res = response.data;
    checkEsignReply(
      res,
      response.request.responseURL,
      response.config.method as string,
      undefined
    )
      .then(() => resolve(response))
      .catch((err) => {
        reject(err);
      });
  });
}, errorRsp(true));
export const request = service;
export const requestRaw = serviceRaw;
export const requestWithoutToken = serviceWithoutToken;
