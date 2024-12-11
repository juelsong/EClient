<template>
  <app-link
    v-if="
      true !== item.meta?.hidden &&
      hasOneShowingChild(item.children, item) &&
      (!onlyOneChild.value.children || onlyOneChild.value.noShowingChildren) &&
      !item.alwaysShow &&
      onlyOneChild.value.meta
    "
    :to="resolvePath(onlyOneChild.value.path)"
  >
    <el-menu-item
      :index="resolvePath(onlyOneChild.value.path)"
      :class="{ 'submenu-title-noDropdown': !isNest }"
    >
      <el-icon
        v-if="onlyOneChild.value.meta.icon || (item.meta && item.meta.icon)"
        :size="24"
      >
        <svg-icon :icon-class="onlyOneChild.value.meta.icon" />
      </el-icon>
      <template #title>
        <span v-if="onlyOneChild.value.meta && onlyOneChild.value.meta.title">
          {{ onlyOneChild.value.meta.title }}
        </span>
        <span
          v-else-if="onlyOneChild.value.meta && onlyOneChild.value.meta.stage"
        >
          {{ testStageText }}
        </span>
        <span
          v-else-if="onlyOneChild.value.meta && onlyOneChild.value.meta.code"
        >
          {{ $t(`${onlyOneChild.value.meta.code}`) }}
        </span>
      </template>
    </el-menu-item>
  </app-link>
  <el-sub-menu
    v-else-if="!item.meta || !item.meta.hidden"
    :index="resolvePath(item.path)"
    popper-append-to-body
  >
    <template #title>
      <el-icon v-if="item.meta && item.meta.icon" :size="24">
        <svg-icon :icon-class="item.meta.icon" />
      </el-icon>
      <span v-if="item.meta && item.meta.title">
        {{ item.meta.title }}
      </span>
      <span v-else-if="item.meta && item.meta.code">
        {{ $t(`${item.meta.code}`) }}
      </span>
    </template>
    <sidebar-item
      v-for="child in item.children"
      :key="child.path"
      :is-nest="true"
      :item="child"
      :base-path="resolvePath(child.path)"
      class="nest-menu"
    />
  </el-sub-menu>
</template>
<script lang="ts">
import { reactive, toRefs } from "vue";
import AppLink from "./Link.vue";

export default {
  name: "SidebarItem",
  components: { AppLink },
};
</script>
<script setup lang="ts">
import * as vue from "vue";
import path from "path-browserify";
import { isExternal } from "@/utils/validate";
import { RouteRecordRaw } from "vue-router";
import getLableDescription from "@/i18n/localeHelper";
const props = defineProps({
  // route object
  item: {
    type: Object,
    required: true,
  },
  isNest: {
    type: Boolean,
    default: false,
  },
  basePath: {
    type: String,
    default: "",
  },
});

const { basePath } = vue.toRefs(props);

const onlyOneChild = vue.reactive<any>({});

const hasOneShowingChild = (
  children = new Array<RouteRecordRaw>(),
  parent: any
) => {
  const showingChildren = children.filter((item) => {
    if (item.meta?.hidden) {
      return false;
    } else {
      // Temp set(will be used if only has one showing child)
      onlyOneChild.value = item;
      return true;
    }
  });

  // When there is only one child router, the child router is displayed by default
  if (showingChildren.length === 1) {
    return true;
  }

  // Show parent if there are no child router to display
  if (showingChildren.length === 0) {
    onlyOneChild.value = { ...parent, path: "", noShowingChildren: true };
    return true;
  }

  return false;
};

const testStageText = vue.computed(() => {
  // onlyOneChild
  if (
    onlyOneChild.value &&
    onlyOneChild.value.meta &&
    onlyOneChild.value.meta.stage
  ) {

  }
  return "";
});
const resolvePath = (routePath: string) => {
  if (isExternal(routePath)) {
    return routePath;
  }
  if (isExternal(basePath.value)) {
    return basePath.value;
  }
  return path.resolve(basePath.value, routePath);
};

// data() {
//   // To fix https://github.com/PanJiaChen/vue-admin-template/issues/237
//   // TODO: refactor with render function
//   this.onlyOneChild = null;
//   return {};
// },
</script>

<style scoped>
.sub-el-icon {
  color: currentColor;
  width: 1em;
  height: 1em;
}
</style>
