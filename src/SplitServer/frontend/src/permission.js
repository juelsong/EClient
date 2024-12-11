import nProgress from "nprogress";
import 'nprogress/nprogress.css'
import { getToken } from '@/utils/auth' // get token from cookie
import getPageTitle from '@/utils/get-page-title'
import { ElMessage } from 'element-plus'

// import { ACCESS_TOKEN, INDEX_MAIN_PAGE_PATH } from '@/store/mutations'
// import { generateIndexRouter } from "@/utils/util"

const whiteList = ['/login'] // no redirect whitelist

export default {
    install: app => {
        let router = app.config.globalProperties.$router;
        router.beforeEach(async (to, from, next) => {
            nProgress.start()
            document.title = getPageTitle(to.meta.title)
            const hasToken = getToken()
            if (hasToken) {
                let store = app.config.globalProperties.$store;
                /* has token */
                if (to.path === '/login') {
                    next({ path: '/' })
                    nProgress.done()
                } else {
                    const hasPermissions = store.getters.permissions
                    if (hasPermissions) {
                        next();
                    } else {
                        try {
                            // get user info
                            const { Permissions } = await store.dispatch('user/getInfo')
                            await store.dispatch('system/getSystemConfig');
                            // generate accessible routes map based on permissions
                            const accessRoutes = await store.dispatch('permission/generateRoutes', Permissions)

                            // dynamically add accessible routes
                            //router.addRoutes(accessRoutes)
                            const removeRoutes = accessRoutes.map(r => router.addRoute(r));
                            await store.commit("permission/SET_REMOVE_ROUTES", removeRoutes);
                            // hack method to ensure that addRoutes is complete
                            // set the replace: true, so the navigation will not leave a history record
                            if (router.hasRoute(to.name) || router.hasRoute(router.resolve(to.path).name)) {
                                next({ ...to, replace: true })
                            } else {
                                next("/401");
                            }
                        } catch (error) {
                            // remove token and go to login page to re-login
                            await store.dispatch('user/resetToken')
                            ElMessage.error(error || 'Has Error')
                            next(`/login?redirect=${to.path}`)
                            nProgress.done()
                        }
                    }
                }
            } else {
                /* has no token*/
                if (whiteList.indexOf(to.path) !== -1) {
                    // 在免登录白名单，直接进入
                    next()
                } else {
                    // other pages that do not have permission to access are redirected to the login page.
                    next(`/login?redirect=${to.path}`)
                    nProgress.done()
                }
            }


        })

        router.afterEach(() => {
            nProgress.done()
        })
    }
}