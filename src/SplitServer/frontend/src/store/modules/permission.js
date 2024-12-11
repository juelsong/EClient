import { asyncRoutes, constantRouterMap } from '@/router/router.config'
import router from '@/router'
// import find from 'lodash.find'
/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(permissions, route) {
    if (route.meta && route.meta.permissions) {
        return permissions.some(permission => permission.Type == 1 && route.meta.permissions.includes(permission.Code))
    } else {
        return false
    }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, permissions) {
    const res = []

    routes.forEach(route => {
        const tmp = { ...route }
        if (hasPermission(permissions, tmp)) {
            if (tmp.children) {
                tmp.children = filterAsyncRoutes(tmp.children, permissions)
            }
            res.push(tmp)
        }
    })

    return res
}

const state = {
    routes: [],
    addRoutes: [],
    removeRoutes: []
}

const mutations = {
    SET_ROUTES: (state, routes) => {
        state.addRoutes = routes
        state.routes = constantRouterMap.concat(routes)
    },
    SET_REMOVE_ROUTES: (state, removeRoutes) => {
        state.removeRoutes = removeRoutes;
    }
}

const actions = {
    generateRoutes({ commit }, permissions) {
        return new Promise(resolve => {
            let accessedRoutes = filterAsyncRoutes(asyncRoutes, permissions)
            commit('SET_ROUTES', accessedRoutes)
            commit('SET_REMOVE_ROUTES', []);
            resolve(accessedRoutes)
        })
    },
    removeRoutes({ commit, state }) {
        if (state.removeRoutes !== undefined) {
            state.removeRoutes.forEach(r => {
                r();
            });
            commit('SET_REMOVE_ROUTES', []);
        }
    }
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}
