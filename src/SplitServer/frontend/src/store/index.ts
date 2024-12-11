import { createStore } from "vuex";
import getters from "./getters";
const modulesFiles = require.context("./modules", true, /\.(js|ts)$/);

const modules = modulesFiles.keys().reduce((modules, modulePath) => {
  const moduleName = modulePath.replace(/^\.\/(.*)\.\w+$/, "$1");
  const value = modulesFiles(modulePath);
  modules[moduleName] = value.default;
  return modules;
}, {});

const store = createStore<any>({
  modules,
  devtools: true,
  strict: process.env.NODE_ENV === "development",
  getters,
});

export default store;
