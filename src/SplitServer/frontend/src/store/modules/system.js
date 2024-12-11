import { querySystemConfig } from "@/api/system_gen";

const state = {
  LocationTypeLevelWeightCount: 0,
  IsClientOffline: false,
  ClockSkew: 0,
};

const mutations = {
  SET_LOCATION_TYPE_WEIGHT_LEVEL_COUNT: (state, cnt) =>
    (state.LocationTypeLevelWeightCount = cnt),
  SET_IS_CLIENT_OFFLINE: (state, val) => (state.IsClientOffline = val),
  SET_ClockSkew: (state, val) => {
    if (val) {
      try {
        const clockSkew = Number.parseInt(val);
        state.ClockSkew = clockSkew;
      } catch (error) {
        console.error(error);
      }
    }
  },
};

const actions = {
  setLocationTypeLevelWeightCount({ commit }, cnt) {
    commit("SET_LOCATION_TYPE_WEIGHT_LEVEL_COUNT", cnt);
  },
  setIsClientOffline({ commit }, val) {
    window.IsClientOffline = val;
    commit("SET_IS_CLIENT_OFFLINE", val);
  },
  async getSystemConfig({ commit }) {
    const value = (await querySystemConfig()).data;
    const keys = Object.keys(mutations);
    for (const valueKey in value) {
      const propertyName = `SET_${valueKey}`;
      // TODO else
      if (keys.includes(propertyName)) {
        commit(propertyName, value[valueKey]);
      }
    }
  },
};

export default {
  namespaced: true,
  state,
  mutations,
  actions,
};
