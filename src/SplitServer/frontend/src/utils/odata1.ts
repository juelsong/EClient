import {
  getToken,
  getRefreshToken,
  setToken,
  setRefreshToken,
} from "@/utils/auth";
import { ElMessage } from "element-plus";
import { o, OdataConfig, OdataQuery, OHandler, ORequest } from "odata";
import { getBaseUrl, getTenant } from "@/utils/global";
import store from "@/store";
import ErrorCode from "@/defs/ErrorCode";
import {
  DeactivateAgainst,
  buildDeactivateAgainstPrompt,
  showPrompt,
} from "./esign-inject";
import { checkEsignReply } from "./esign-inject";

function getODataBaseUrl() {
  const url = `${getBaseUrl()}/OData/`;
  return url;
}

const getHeader = () => {
  const headers = new Headers({ "X-TENANT": getTenant() });
  headers.append("Content-Type", "application/json");
  // headers.append("Access-Control-Allow-Origin", "*");
  if (store.getters.token) {
    headers.append("Authorization", `Bearer ${store.getters.token}`);
  }
  if (store.getters.refreshToken) {
    headers.append("X-Authorization", `Bearer ${store.getters.refreshToken}`);
  }
  return headers;
};

declare interface IODataErrorResponse {
  error: {
    code: string;
    message: string;
    target?: string;
  };
}

const onFinish = (_: OHandler, response?: Response) => {
  if (response) {
    const token = response.headers.get("access-token");
    const refreshToken = response.headers.get("x-access-token");
    if (token) {
      setToken(token);
      store.commit("user/SET_TOKEN", token);
    }
    if (refreshToken) {
      setRefreshToken(refreshToken);
      store.commit("user/SET_REFRESH_TOKEN", refreshToken);
    }
  }
  return null;
};

const onError = (
  _: OHandler,
  response: Response,
  reject?: (reason?: any) => void
) => {
  if (response.status == 401) {
    ElMessage({
      message: "超时，请重新登录",
      type: "error",
      duration: 3 * 1000,
      onClose: () => {
        // to re-login
        store.dispatch("user/resetToken").then(() => {
          location.reload();
        });
      },
    });
  } else if (response.status === 400) {
    response.json().then((rsp: IODataErrorResponse) => {
      const code = Number.parseInt(rsp.error.code);
      let msg = "";
      if (code === ErrorCode.Service.DeactiveInvalid && rsp.error.target) {
        const invalidateInfo = JSON.parse(
          rsp.error.target
        ) as DeactivateAgainst[];
        msg = invalidateInfo.map(buildDeactivateAgainstPrompt).join("\r");
      }
      showPrompt(code, undefined, msg);
    });
  }

  if (reject) {
    reject();
  }
  return null;
};

export interface BatchQuery {
  [key: string]: OdataQuery;
}

export interface BatchUpdate {
  [key: string]: object[];
}

export interface BatchDelete {
  [key: string]: number[];
}

function doNothing() {}
declare type ODataMethod = "GET" | "POST" | "PATCH" | "DELETE";

function oDataBatch(
  batch: BatchQuery | BatchUpdate | BatchDelete,
  method: ODataMethod
) {
  const batchHeader = getHeader();
  const config: OdataConfig = {
    batch: {
      boundaryPrefix: "batch_",
      endpoint: "$batch",
      headers: batchHeader,
      useChangset: false,
      useRelativeURLs: false,
    },
    headers: new Headers({
      "Content-Type": "application/json",
    }),
    onFinish: onFinish,
    mode: "cors",
    fragment: "",
    onStart: () => null,
    onError,
  };
  const handler = o(`${getODataBaseUrl()}$batch`, config);
  const innerConfig = { ...config, method };
  for (const key in batch) {
    const request = new ORequest(`${getODataBaseUrl()}${key}`, innerConfig);
    if (method == "GET") {
      request.applyQuery(batch[key]);
      handler.request(request);
    } else if (method == "PATCH") {
      const objs = batch[key] as object[];
      objs.forEach((obj) => {
        handler.patch(key+`/${(obj as any).Id}`, obj);
      });
    } else if (method == "POST") {
      const objs = batch[key] as object[];
      objs.forEach((obj) => {
        handler.post(key, obj);
      });
    } else if (method == "DELETE") {
      const ids = batch[key] as number[];
      ids.forEach((id) => {
        handler.delete(`${key}(${id})`);
      });
    }
  }
  return handler.batch();
}

export const oDataBatchQuery = (batch: BatchQuery) => oDataBatch(batch, "GET");

export const oDataBatchUpdate = (batch: BatchUpdate) =>
  oDataBatch(batch, "PATCH");

export const oDataBatchDelete = (batch: BatchDelete) =>
  oDataBatch(batch, "DELETE");

export const oDataQuery = (entityName: string, options: OdataQuery) => {
  const config: OdataConfig = {
    headers: getHeader(),
    onFinish: onFinish,
    fragment: "",
    onStart: () => null,
    onError,
  };

  if (options && options.$orderby) {
    options.$orderby = options.$orderby.replaceAll(".", "/");
  }
  const handler = o(getODataBaseUrl(), config);
  return options
    ? handler.get(entityName).query(options)
    : handler.get(entityName).query();
};
export const oDataExport = (entityName: string, options: OdataQuery) => {
  const config: OdataConfig = {
    headers: getHeader(),
    onFinish: onFinish,
    fragment: "",
    onStart: () => null,
    onError,
  };

  const handler = o(`${getBaseUrl()}/Export/`, config);
  return options
    ? handler.get(entityName).query(options)
    : handler.get(entityName).query();
};

export function oDataDelete(entityName: string, ids: Array<number>) {
  return new Promise((resolve, reject) => {
    const isSingle = ids.length == 1;
    if (isSingle) {
      const config: OdataConfig = {
        headers: getHeader(),
        onFinish: onFinish,
        method: "DELETE",
        fragment: "",
        onStart: () => null,
        onError,
      };
      const handler = o(getODataBaseUrl(), config);
      handler
        .delete(`${entityName}(${ids[0]})`)
        .query()
        .then(resolve)
        .catch(doNothing);
    } else {
      // TODO
      // const deleteData: BatchDelete = {};
      // deleteData[entityName] = ids;
      // const ret = await oDataBatchDelete(deleteData);
      // return ret;
    }
  });
}

export function oDataPatch(entityName: string, entity: any) {
  return new Promise((resolve, reject) => {
    const config: OdataConfig = {
      headers: getHeader(),
      onFinish: onFinish,
      fragment: "",
      onStart: () => null,
      onError,
    };
    const handler = o(getODataBaseUrl(), config);
    handler
      .patch(`${entityName}/${entity.Id}`, entity)
      .query()
      .then((rsp) => {
        debugger;
        resolve(rsp);
      })
      // .then(resolve=>{
      //   console.log(resolve);
      //   // checkEsignReply(resolve,`${getODataBaseUrl()${entityName}}`,"patch")
      // })
      .catch(doNothing);
  });
}

export function oDataPost(entityName: string, entity: any) {
  return new Promise((resolve, reject) => {
    const config: OdataConfig = {
      headers: getHeader(),
      fragment: "",
      onStart: () => null,
      onFinish: onFinish,
      onError,
    };
    const handler = o(getODataBaseUrl(), config);
    handler
      .post(`${entityName}`, entity)
      .query()
      .then((rsp) => {
        debugger;
        resolve(rsp);
      })
      .catch(doNothing);
  });
}

export const oDataExist = async (
  entityName: string,
  property: string,
  value: any,
  id: number
) => {
  const config: OdataConfig = {
    headers: getHeader(),
    fragment: "",
    onStart: () => null,
    onFinish: onFinish,
    onError,
  };
  const options = {
    $filter: `${property} eq '${value}'`,
    $select: `${property}`,
    $count: true,
  };

  if (id) {
    options.$filter = `(${options.$filter}) and (Id ne ${id})`;
  }

  const handler = o(getODataBaseUrl(), config);
  const data = await handler.get(entityName).query(options);
  return data["@odata.count"] != 0;
};

const oDataTraceUser = async (creatorId: number, updateUserId: number) => {
  const userIds: Array<number> = [];
  if (creatorId) {
    userIds.push(creatorId);
  }
  if (updateUserId && !userIds.includes(updateUserId)) {
    userIds.push(updateUserId);
  }
  const options: OdataQuery = {
    $select: "Id,Account,RealName",
  };
  const ret = [undefined, undefined];
  if (userIds.length > 0) {
    if (userIds.length > 1) {
      options.$filter = `Id in [${userIds.join(",")}]`;
    } else {
      options.$filter = `Id eq ${userIds[0]}`;
    }
    const { value } = await oDataQuery("User", options);
    if (value) {
      ret[0] = value.find((item) => item.Id == creatorId);
      ret[1] = value.find((item) => item.Id == updateUserId);
    }
  }
  return ret;
};

export default {
  install: (app) => {
    app.config.globalProperties.$query = oDataQuery;
    app.config.globalProperties.$export = oDataExport;

    app.config.globalProperties.$delete = oDataDelete;
    app.config.globalProperties.$update = oDataPatch;
    app.config.globalProperties.$insert = oDataPost;
    app.config.globalProperties.$exist = oDataExist;
    app.config.globalProperties.$traceUser = oDataTraceUser;
  },
};
