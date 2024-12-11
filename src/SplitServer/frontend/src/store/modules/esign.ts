declare interface IESignState {
  needESign: boolean;
  serialNumber?: string;
  category?: string;
  url?: string;
  method?: string;
  total: number;
  current: number;
  headerCount?: string;
  resolve?: () => void;
  reject?: () => void;
}

const state: IESignState = {
  needESign: false,
  total: 0,
  current: 0,
};

const mutations = {
  SET_NEED_ESIGN: (state: IESignState, val: boolean) => {
    state.needESign = val;
  },
  SET_SERIAL_NUMBER: (state: IESignState, val?: string) => {
    state.serialNumber = val;
  },
  SET_CATEGORY: (state: IESignState, val?: string) => {
    state.category = val;
  },
  SET_URL: (state: IESignState, val?: string) => {
    state.url = val;
  },
  SET_METHOD: (state: IESignState, val?: string) => {
    state.method = val;
  },
  SET_RESOLVE: (state: IESignState, val?: () => void) => {
    state.resolve = val;
  },
  SET_REJECT: (state: IESignState, val?: () => void) => {
    state.reject = val;
  },
  SET_TOTAL: (state: IESignState, val: number) => {
    state.total = val;
  },
  SET_CURRENT: (state: IESignState, val: number) => {
    state.current = val;
  },
  SET_HEADER_COUNT: (state: IESignState, val?: string) => {
    state.headerCount = val;
  },
};

const actions = {
  popESign({ commit }, data: IESignState) {
    if (data.resolve) {
      commit("SET_RESOLVE", data.resolve);
    }
    if (data.reject) {
      commit("SET_REJECT", data.reject);
    }
    commit("SET_URL", data.url);
    commit("SET_METHOD", data.method);
    commit("SET_CATEGORY", data.category);
    commit("SET_SERIAL_NUMBER", data.serialNumber);
    commit("SET_NEED_ESIGN", true);
    commit("SET_TOTAL", data.total);
    commit("SET_CURRENT", data.current);
    commit("SET_HEADER_COUNT", data.headerCount);
  },
};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
};
