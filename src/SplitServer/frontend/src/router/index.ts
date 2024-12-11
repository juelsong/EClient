import { createRouter, createWebHashHistory, Router, RouterScrollBehavior } from 'vue-router'
import { constantRouterMap } from './router.config'
const scrollBehavior: RouterScrollBehavior = () => {
    return Promise.resolve(false);
}
const router: Router = createRouter({
    history: createWebHashHistory(process.env.BASE_URL),
    // base: process.env.BASE_URL,
    scrollBehavior: scrollBehavior,
    routes: constantRouterMap
});

export default router;
