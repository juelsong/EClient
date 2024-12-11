import * as Cookies from "@/utils/cookies";
import moment from 'moment';
import { i18n } from "@/i18n";

const state = {
    sidebar: {
        opened: Cookies.get('sidebarStatus') ? !!+Cookies.get('sidebarStatus') : true,
        withoutAnimation: false
    },
    device: 'desktop',
    size: Cookies.get('size') || 'default',
    locale: Cookies.get('locale') || 'zh-cn'
}

const mutations = {
    TOGGLE_SIDEBAR: state => {
        state.sidebar.opened = !state.sidebar.opened
        state.sidebar.withoutAnimation = false
        if (state.sidebar.opened) {
            Cookies.set('sidebarStatus', 1)
        } else {
            Cookies.set('sidebarStatus', 0)
        }
    },
    CLOSE_SIDEBAR: (state, withoutAnimation) => {
        Cookies.set('sidebarStatus', 0)
        state.sidebar.opened = false
        state.sidebar.withoutAnimation = withoutAnimation
    },
    TOGGLE_DEVICE: (state, device) => {
        state.device = device
    },
    SET_SIZE: (state, size) => {
        Cookies.set('size', size);
        state.size = size
    },
    SET_LOCALE: (state, locale) => {
        i18n.locale = locale;
        moment.locale(locale);
        Cookies.set('locale', locale);
        state.locale = locale;
    }
}

const actions = {
    toggleSideBar({ commit }) {
        commit('TOGGLE_SIDEBAR')
    },
    closeSideBar({ commit }, { withoutAnimation }) {
        commit('CLOSE_SIDEBAR', withoutAnimation)
    },
    toggleDevice({ commit }, device) {
        commit('TOGGLE_DEVICE', device)
    },
    setSize({ commit }, size) {
        commit('SET_SIZE', size)
    },
    setLocale({ commit }, locale) {
        commit("SET_LOCALE", locale)
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}
