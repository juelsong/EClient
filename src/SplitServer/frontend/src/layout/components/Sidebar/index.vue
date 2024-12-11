<template>
  <div :class="{ 'has-logo': showLogo }">
    <logo v-if="showLogo" :collapse="isCollapse" />
    <el-scrollbar>
      <el-menu
        :default-active="activeMenu"
        :collapse="isCollapse"
        :background-color="'#304156'"
        :text-color="'#bfcbd9'"
        :active-text-color="'#409EFF'"
        :unique-opened="false"
        :collapse-transition="false"
        mode="vertical"
      >
        <sidebar-item
          v-for="route in permission_routes"
          :key="route.path"
          :item="route"
          :base-path="route.path"
        />
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script lang="ts">
import { computed } from "vue";
import router from "@/router";
import store from "@/store";
import SidebarItem from "./SidebarItem.vue";
// TODO 引入样式
// import cssVariables from "@/styles/variables.scss";
import Logo from "./Logo.vue";

export default {
  name: "Sidebar",
};
</script>

<script setup lang="ts">
const permission_routes = computed(() => store.state.permission.routes);
const sidebar = computed(() => store.state.app.sidebar);
const activeMenu = computed(() => {
  const meta = router.currentRoute.value.meta;
  return router.currentRoute.value.path;
});
const showLogo = computed(() => {
  return store.state.settings.sidebarLogo;
});
// const variables = computed(() => cssVariables);
const isCollapse = computed(() => {
  return !sidebar.value.opened;
});
</script>
