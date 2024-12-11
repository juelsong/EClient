import { login, logout, getInfo } from "@/api/user";
import * as Cookies from "@/utils/cookies";
import * as odata from "@/utils/odata";
import { autoCheckToken, stopCheckToken } from "@/utils/tokenWatcher";
import {
  getToken,
  removeToken,
  getRefreshToken,
  removeRefreshToken,
  setToken,
  setRefreshToken,
} from "@/utils/auth";
// import router from '@/router'
const ResultDefaultConfigKey = "EMIS-ResultDefaultConfig";
const UserSettingsKey = "EMSI-UserSettings";
const UserManagementModeKey = "EMIS-UserManagementMode";

function getConfigFromCookies(key) {
  const str = Cookies.get(key);
  if (str == undefined) {
    return undefined;
  } else {
    return JSON.parse(str);
  }
}
function setConfigToCookies(key, config) {
  if (config === undefined || config === null) {
    return Cookies.remove(key);
  }
  const str = JSON.stringify(config);
  return Cookies.set(key, str);
}

const state = {
  name: "",
  avatar: "",
  id: undefined,
  introduction: "",
  roles: [],
  locations: [],
  locationId: undefined,
  userName: "",
  permissions: undefined,
  profile: {},
  resultDefaultConfig: getConfigFromCookies(ResultDefaultConfigKey),
  userSettings: getConfigFromCookies(UserSettingsKey),
  shouldChangePass: false,
  userManagementMode: getConfigFromCookies(UserManagementModeKey),
};

const mutations = {
  SET_TOKEN: (state, token) => {
    setToken(token);
  },
  SET_REFRESH_TOKEN: (state, token) => {
    setRefreshToken(token);
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction;
  },
  SET_ID: (state, id) => {
    state.id = id;
  },
  SET_NAME: (state, name) => {
    state.name = name;
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar;
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles;
  },
  SET_PERMISSIONS: (state, permissions) => {
    state.permissions = permissions;
  },
  SET_RESULT_DEFAULT_CONFIG: (state, config) => {
    state.resultDefaultConfig = config;
  },
  SET_USER_SETTINGS: (state, settings) => {
    state.userSettings = settings;
    setConfigToCookies(UserSettingsKey, settings);
  },
  SET_LOCATIONS: (state, locations) => {
    state.locations = locations;
  },
  SET_LOCATION_ID: (state, locationId) => {
    state.locationId = locationId;
  },
  SET_USERNAME: (state, userName) => {
    state.userName = userName;
  },
  SET_PROFILE: (state, profile) => {
    state.profile = profile;
  },
  SET_SHOULD_CHANGE_PASS: (state, shouldChangePass) => {
    state.shouldChangePass = shouldChangePass;
  },
  SET_USER_MANAGEMENT_MODE: (state, userManagementMode) => {
    state.userManagementMode = userManagementMode;
    setConfigToCookies(UserManagementModeKey, userManagementMode);
  },
};

const actions = {
  // user login
  login(ctx, userInfo) {
    const { username, password, currdatetime, captcha } = userInfo;
    return new Promise((resolve, reject) => {
      login({
        account: username.trim(),
        password: password,
        captcha,
        checkKey: currdatetime,
      })
        .then((rsp) => {
          const shouldChangePass =
            rsp && rsp.data && rsp.data.ShouldChangePass === true;
          ctx.dispatch("setShouldChangePass", shouldChangePass);
          ctx.dispatch("setResultDefaultConfig", undefined);
          ctx.dispatch("setUserSettings", undefined);
          resolve();
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  // get user info
  getInfo({ commit, dispatch, state }) {
    let that = this;
    return new Promise((resolve, reject) => {
      getInfo()
        .then(async (response) => {
          const { data } = response;

          if (!data) {
            reject("Verification failed, please Login again.");
          }
          autoCheckToken();
          const syncService = window.syncService;
          if (syncService) {
            await syncService.setCurrentUserInfo(
              data.Id,
              data.Account,
              getToken(),
              getRefreshToken()
            );
          }
          commit("SET_ID", data.Id);
          commit("SET_ROLES", data.Roles);
          commit("SET_NAME", data.Account);
          commit("SET_PERMISSIONS", data.Permissions);
          commit("SET_USERNAME", data.Name);
          commit("SET_PROFILE", data.Profile);
          commit("SET_USER_MANAGEMENT_MODE", data.UserManagementMode);
          if (data.Profile.Locale) {
            await dispatch("app/setLocale", data.Profile.Locale, {
              root: true,
            });
          }
          commit("SET_USER_SETTINGS", data.Profile.UserSettings);
          commit("SET_LOCATION_ID", data.LocationId);
          //TODO 加入locationV
          // const res = await odata.oDataQuery("LocationV", {
          //   $filter: `contains(Breadcrumb,',${data.LocationId},')`,
          //   $select: "LocationId,Name",
          // });
          // commit("SET_LOCATIONS", res.value);
          resolve(data);
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  // user logout
  logout({ commit, state, dispatch }) {
    dispatch("system/setIsClientOffline", false);
    stopCheckToken();
    return new Promise((resolve, reject) => {
      logout(state.token)
        .then(() => {
          commit("SET_ID", undefined);
          commit("SET_TOKEN", "");
          commit("SET_REFRESH_TOKEN", "");
          commit("SET_ROLES", []);
          commit("SET_PERMISSIONS", undefined);
          commit("SET_LOCATIONS", undefined);
          commit("SET_USERNAME", "");

          removeToken();
          removeRefreshToken();

          dispatch("setResultDefaultConfig", undefined);
          // resetRouter()

          // reset visited views and cached views
          // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
          dispatch("tagsView/delAllViews", null, { root: true });
          dispatch("permission/removeRoutes", null, { root: true });
          resolve();
        })
        .catch((error) => {
          reject(error);
        });
    });
  },

  // remove token
  resetToken({ commit }) {
    return new Promise((resolve) => {
      commit("SET_TOKEN", "");
      commit("SET_REFRESH_TOKEN", "");
      commit("SET_ROLES", []);
      removeToken();
      removeRefreshToken();
      resolve();
    });
  },
  setResultDefaultConfig({ commit }, config) {
    commit("SET_RESULT_DEFAULT_CONFIG", config);
    setConfigToCookies(ResultDefaultConfigKey, config);
  },
  setUserSettings({ commit }, settings) {
    commit("SET_USER_SETTINGS", settings);
  },
  setShouldChangePass({ commit }, shouldChangePass) {
    commit("SET_SHOULD_CHANGE_PASS", shouldChangePass);
  },
  // dynamically modify permissions
  // async changeRoles({ commit, dispatch }, role) {
  //     const token = role + '-token'

  //     commit('SET_TOKEN', token)
  //     setToken(token)

  //     const { roles } = await dispatch('getInfo')

  //     // resetRouter()

  //     // generate accessible routes map based on roles
  //     const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })
  //     // dynamically add accessible routes
  //     router.addRoutes(accessRoutes)

  //     // reset visited views and cached views
  //     dispatch('tagsView/delAllViews', null, { root: true })
  // }
};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
};
