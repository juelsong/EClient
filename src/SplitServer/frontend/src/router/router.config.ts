import { RouteRecordRaw } from "vue-router";
/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap: RouteRecordRaw[] = [
  {
    path: "/redirect",
    component: () => import("@/layout/index.vue"),
    meta: { hidden: true },
    children: [
      {
        path: "/redirect/:path(.*)",
        component: () => import("@/views/redirect/index.vue"),
      },
    ],
  },
  {
    path: "/",
    component: () => import("@/layout/index.vue"),
    redirect:
      process.env.NODE_ENV === "client"
        ? "/inspectionExecution/inspectionRecord"
        : "/dashboard",
    children:
      process.env.NODE_ENV === "client"
        ? []
        : [
            {
              path: "dashboard",
              component: () => import("@/views/dashboard/index.vue"),
              name: "Dashboard",
              meta: { code: "menu.dashboard", icon: "dashboard", affix: true },
            },
          ],
  },
  {
    path: "/login",
    component: () => import("@/views/login/index.vue"),
    meta: { hidden: true },
  },
  {
    path: "/profile",
    component: () => import("@/layout/index.vue"),
    redirect: "/profile/index",
    meta: { hidden: true },
    children: [
      {
        path: "index",
        component: () => import("@/views/profile/index.vue"),
        name: "Profile",
        meta: { code: "label.navbar.profile", icon: "user", noCache: true },
      },
    ],
  },
  {
    //http://localhost:3300/#/syncData/syncData
    path: "/syncData",
    redirect: "/syncData/syncData",
    component: () => import("@/layout/index.vue"),
    name: "syncData",
    meta: {
      hidden: true,
      code: "menu.SyncData",
      icon: "SyncData",
      permissions: ["production"],
    },
    children: [
      {
        path: "syncData",
        component: () => import("@/views/device/SyncData.vue"),
        name: "syncData",
        meta: {
          code: "menu.region",
          icon: "region",
          permissions: ["region"],
        },
      },
    ],
  },
  {
    path: "/404",
    component: () => import("@/views/error-page/404.vue"),
    meta: { hidden: true },
  },
  {
    path: "/401",
    component: () => import("@/views/error-page/401.vue"),
    meta: { hidden: true },
  },
  {
    path: "/error",
    component: () => import("@/layout/index.vue"),
    redirect: "noRedirect",
    name: "ErrorPages",
    meta: {
      title: "Error Pages",
      icon: "404",
      hidden: true,
    },
    // children: new Array(21).fill(1).map((_, idx) => {
    //     let name = (Array(3).join("0") + (idx + 1)).slice(-3);
    //     return {
    //         path: `${name}`,
    //         component: {
    //             template: `<div>${name}</div>`,
    //         },
    //         name: `Page${name}`,
    //         meta: { title: `${name}`, noCache: true }
    //     }
    // }),
    children: [
      {
        path: "401",
        component: () => import("@/views/error-page/401.vue"),
        name: "Page401",
        meta: { title: "401", noCache: true },
      },
      {
        path: "404",
        component: () => import("@/views/error-page/404.vue"),
        name: "Page404",
        meta: { title: "404", noCache: true },
      },
    ],
  },
  // 404 page must be placed at the end !!!
  // { path: '*', redirect: '/404', hidden: true }
];

const asyncRoutesArray = [
  {
    path: "/system",
    component: () => import("@/layout/index.vue"),
    name: "system",
    redirect: "noRedirect",
    meta: {
      code: "menu.system",
      icon: "system",
    },
    children: [
      {
        path: "region",
        component: () => import("@/views/region/Region.vue"),
        name: "Region",
        meta: {
          code: "menu.region",
          icon: "region",
          permissions: ["region"],
        },
      },
      {
        path: "production",
        component: () => import("@/views/production/Production.vue"),
        name: "Production",
        meta: {
          code: "menu.production",
          icon: "production",
          permissions: ["production"],
        },
      },
      {
        path: "device",
        component: () => import("@/views/device/Device.vue"),
        name: "Device",
        meta: {
          code: "menu.device",
          icon: "device",
          permissions: ["device"],
        },
      },
      {
        path: "medium",
        component: () => import("@/views/medium/Medium.vue"),
        name: "Medium",
        meta: {
          code: "menu.medium",
          icon: "medium",
          permissions: ["medium"],
        },
      },
      {
        path: "microorganism",
        component: () => import("@/views/microorganism/Microorganism.vue"),
        name: "Microorganism",
        meta: {
          code: "menu.microorganism",
          icon: "microorganism",
          permissions: ["microorganism"],
        },
      },
      {
        path: "security",
        component: () => import("@/views/security/Security.vue"),
        name: "Security",
        meta: {
          code: "menu.security",
          icon: "security",
          permissions: ["security"],
        },
      },
      {
        path: "auditRecord",
        component: () => import("@/views/auditrecord/Index.vue"),
        name: "AuditRecord",
        meta: {
          code: "menu.auditRecord",
          icon: "auditRecord",
          permissions: ["auditRecord"],
        },
      },
      {
        path: "log",
        component: () => import("@/views/system/Log.vue"),
        name: "Log",
        meta: {
          code: "menu.log",
          icon: "log",
          permissions: ["log"],
        },
      },
      {
        path: "settings",
        component: () => import("@/views/system/Settings/index.vue"),
        name: "Settings",
        meta: {
          code: "menu.settings",
          icon: "setting",
          permissions: ["settings"],
        },
      },
    ],
  },
  {
    path: "/visualization",
    component: () => import("@/layout/index.vue"),
    name: "Visualization",
    redirect: "noRedirect",
    meta: {
      code: "menu.visualization",
      icon: "visualization",
    },
    children: [
      {
        path: "map",
        component: () => import("@/views/visualization/MapManagement.vue"),
        name: "Map",
        meta: {
          code: "menu.map",
          icon: "map",
          permissions: ["map"],
        },
      },
      {
        path: "visualizations",
        component: () => import("@/views/visualization/Visualizations.vue"),
        name: "Visualizations",
        meta: {
          code: "menu.visualizations",
          icon: "visualizations",
          permissions: ["visualizations"],
        },
      },
      {
        path: "orgnismVisualizations",
        component: () =>
          import("@/views/visualization/VisualizationOrgnisms.vue"),
        name: "orgnismVisualizations",
        meta: {
          code: "menu.orgnismVisualizations",
          icon: "visualizations",
          permissions: ["orgnismVisualizations"],
        },
      },
    ],
  },
  {
    path: "/deviceTMP",
    component: () => import("@/layout/index.vue"),
    name: "DeviceTMP",
    meta: {
      icon: "auditPrompt",
    },
    children: [
      {
        path: "/deviceTMP",
        component: () => import("@/views/environment/DeviceTMP.vue"),
        name: "DeviceTMP",
        meta: {
          code: "menu.deviceTMP",
          icon: "auditPrompt",
          permissions: ["region"],
        },
      },
    ],
  },
];

function set_parent_permission(route: RouteRecordRaw) {
  if (
    route.meta &&
    !route.meta.permissions &&
    route.children &&
    route.children.length > 0
  ) {
    route.meta.permissions = [];
    route.children.forEach((sr) => {
      set_parent_permission(sr);
      if (sr.meta && sr.meta.permissions && route.meta) {
        const childPermissions = sr.meta.permissions as Array<string>;
        const routePermissions = route.meta.permissions as Array<string>;
        routePermissions.push(...childPermissions);
      }
    });
  }
}

asyncRoutesArray.forEach((r) => {
  set_parent_permission(r);
});

export const asyncRoutes = asyncRoutesArray;

export default {
  constantRouterMap,
};
