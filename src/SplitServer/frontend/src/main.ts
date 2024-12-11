import { createApp } from "vue";
import * as Cookies from "@/utils/cookies";
import App from "./App.vue";
import router from "./router";
import StoragePlugin from "vue-web-storage";
import permission from "./permission.js";
import store from "@/store";
import "normalize.css/normalize.css"; // a modern alternative to CSS resets
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import odata from "@/utils/odata";
import icon from "./icons";
import has from "./utils/extensions/has";
import "@/styles/index.scss"; // global css
import { i18n } from "@/i18n";
import { IReply } from "@/defs/Reply";
import { Store } from "vuex";
import moment from "moment";
import { ElMessageBoxShortcutMethod } from "element-plus";
import { OdataQuery } from "odata";
import "element-plus/theme-chalk/display.css";
import "@grapecity/ar-viewer/dist/jsViewer.min.js";
import "@grapecity/ar-viewer/dist/jsViewer.min.css";
import { request } from "@/utils/request";
import { Version } from "@/defs/Entity";
import tableUtility from "@/utils/table-utility";
import "moment/min/locales";

declare type ODataResponse = IReply<any>;
declare interface ITableData {
  data: Array<any>;
  pageSize: number;
  total: number;
  current: number;
}
declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $query: (entityName: string, options: OdataQuery) => Promise<any>;
    $insert: (entityName: string, entity: object) => Promise<ODataResponse>;
    $update: (entityName: string, entity: object) => Promise<ODataResponse>;
    $alert: ElMessageBoxShortcutMethod;
    $confirm: ElMessageBoxShortcutMethod;
    $prompt: ElMessageBoxShortcutMethod;
    $message: {
      success: (msg: string) => void;
      error: (msg: string) => void;
    };
    $t: (key: string, pars?: Array<string>) => string;
    $store: Store<any>;
    $i18n: any;
    loadData: () => Promise<void>;
    tableData: ITableData;
  }
}

import { use } from "echarts";
import { install as SVGRenderer } from "echarts/lib/renderer/installSVGRenderer.js";
import { install as CanvasRenderer } from "echarts/lib/renderer/installCanvasRenderer.js";

use(SVGRenderer);
use(CanvasRenderer);

const app_instance = createApp(App);
//按钮点击延迟方法
app_instance.directive("noMoreClick", {
  mounted(el, binding) {
    el.addEventListener("click", (e) => {
      el.classList.add("is-disabled");
      el.disabled = true;
      setTimeout(() => {
        el.disabled = false;
        el.classList.remove("is-disabled");
      }, 2000); //我这里设置的是2000毫秒也就是2秒
    });
  },
});
app_instance.config.globalProperties.$moment = moment;
app_instance
  .use(router)
  .use(permission)
  .use(odata)
  // .use(notify)
  .use(ElementPlus, {
    size: Cookies.get("size") || "default", // set element-ui default size
  })
  .use(icon)
  .use(StoragePlugin, {
    prefix: `${process.env.VUE_APP_NAME}_`,
    drivers: ["session", "local"],
  })
  .use(tableUtility)
  .use(i18n)
  .use(store)
  .use(has)
  .mount("#app");

if (process.env.NODE_ENV == "development") {
  request({
    url: `/View/Version`,
    method: "get",
  }).then((ret) => {
    if (ret.data !== Version) {
      console.error("***********数据版本不正确***********");
      alert("***********数据版本不正确***********");
    }
  });
}

export const app = app_instance;
