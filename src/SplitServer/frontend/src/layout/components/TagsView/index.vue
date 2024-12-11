<template>
  <div id="tags-view-container" class="tags-view-container">
    <scroll-pane
      class="tags-view-wrapper"
      @scroll="closeMenu"
      v-bind:selectedIndex="selectedIndex"
    >
      <router-link
        v-for="tag in visitedViews"
        :key="tag.path"
        :class="isActive(tag) ? 'active' : ''"
        :to="{ path: tag.path }"
        tag="span"
        class="tags-view-item"
        :style="tagStyle(tag)"
        @click.middle="!isAffix(tag) ? closeSelectedTag(tag) : ''"
        @contextmenu.prevent="openMenu(tag, $event)"
      >
        {{ $t(`${tag.meta.code}`) }}
        <el-icon
          v-if="!isAffix(tag)"
          class="el-icon-close"
          @click.prevent.stop="closeSelectedTag(tag)"
        >
          <svg-icon icon-class="close" />
        </el-icon>
      </router-link>
    </scroll-pane>
    <ul
      v-show="visible"
      :style="{ left: left + 'px', top: top + 'px' }"
      class="contextmenu"
    >
      <li @click="refreshSelectedTag(selectedTag)">
        {{ $t("contextMenu.refresh") }}
      </li>
      <li v-if="!isAffix(selectedTag)" @click="closeSelectedTag(selectedTag)">
        {{ $t("contextMenu.close") }}
      </li>
      <li @click="closeOthersTags">{{ $t("contextMenu.closeOthers") }}</li>
      <li @click="closeAllTags(selectedTag)">
        {{ $t("contextMenu.closeAll") }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import {
  computed,
  reactive,
  ref,
  onMounted,
  watch,
  nextTick,
  provide,
} from "vue";

export default {
  name: "TagsView",
};
</script>

<script setup lang="ts">
import { _RouteLocationBase, RouteRecordRaw } from "vue-router";
import { ITag } from "@/defs/Types";
import ScrollPane from "./ScrollPane.vue";
import store from "@/store";
import router from "@/router";
import path from "path-browserify";

const visible = ref(false);
const top = ref(0);
const left = ref(0);
const selectedIndex = ref(0);
let selectedTag = ref<ITag>({
  fullPath: "",
  path: "",
  name: "",
  meta: {},
});
const affixTags = reactive(new Array<_RouteLocationBase>());

const visitedViews = computed<ITag[]>(() => store.state.tagsView.visitedViews);
const routes = computed<RouteRecordRaw[]>(() => store.state.permission.routes);
const theme = computed(() => store.state.settings.theme);

provide("visitedViews", visitedViews);
const ss = document.getElementById("");
ss?.style;
const isActive = (route: ITag) => route.path === router.currentRoute.value.path;
const isAffix = (tag: ITag) => tag.meta && tag.meta.affix;
const tagStyle = (tag: ITag) => {
  let newStyle: Partial<CSSStyleDeclaration> = {};
  if (!isAffix(tag)) {
    newStyle.padding = "0 0 0 8px";
  }
  if (isActive(tag)) {
    newStyle.backgroundColor = theme.value;
    newStyle.borderColor = theme.value;
  }
  return newStyle;
};
const filterAffixTags = (routes: RouteRecordRaw[], basePath: string = "/") => {
  let tags = new Array<_RouteLocationBase>();
  routes.forEach((route) => {
    if (route.meta && route.meta.affix) {
      const tagPath = path.resolve(basePath, route.path);
      tags.push({
        path: tagPath,
        fullPath: tagPath,
        query: {},
        hash: "",
        name: route.name,
        params: {},
        redirectedFrom: undefined,
        meta: { ...route.meta },
      });
    }
    if (route.children) {
      const tempTags = filterAffixTags(route.children, route.path);
      if (tempTags.length >= 1) {
        tags = [...tags, ...tempTags];
      }
    }
  });
  return tags;
};
const initTags = () => {
  const localTags = filterAffixTags(routes.value);
  affixTags.splice(0, affixTags.length, ...localTags);
  for (const tag of localTags) {
    // Must have tag name
    if (tag.name) {
      store.dispatch("tagsView/addVisitedView", tag);
    }
  }
};
const addTags = () => {
  if (router.currentRoute.value.name) {
    store.dispatch("tagsView/addView", router.currentRoute.value);
  }
  return false;
};
const moveToCurrentTag = () => {
  let idx = visitedViews.value.findIndex(
    (v) => v.path == router.currentRoute.value.path
  );
  if (idx > -1) {
    selectedIndex.value = idx;
  }
};
const refreshSelectedTag = (view: ITag) => {
  store.dispatch("tagsView/delCachedView", view).then(() => {
    const { fullPath } = view;
    nextTick(() => {
      router.replace({
        path: "/redirect" + fullPath,
      });
    });
  });
};
const closeSelectedTag = (view: ITag) => {
  store.dispatch("tagsView/delView", view).then(({ visitedViews }) => {
    if (isActive(view)) {
      toLastView(visitedViews, view);
    }
  });
};
const closeOthersTags = () => {
  router.push(selectedTag.value);
  store.dispatch("tagsView/delOthersViews", selectedTag.value).then(() => {
    moveToCurrentTag();
  });
};
const closeAllTags = (view: ITag) => {
  store.dispatch("tagsView/delAllViews").then(({ visitedViews }) => {
    if (affixTags.some((tag) => tag.path === view.path)) {
      return;
    }
    toLastView(visitedViews, view);
  });
};
const toLastView = (visitedViews: ITag[], view: ITag) => {
  const latestView = visitedViews.slice(-1)[0];
  if (latestView) {
    router.push(latestView.fullPath);
  } else {
    // now the default is to redirect to the home page if there is no tags-view,
    // you can adjust it according to your needs.
    if (view.name === "Dashboard") {
      // to reload home page
      router.replace({ path: "/redirect" + view.fullPath });
    } else {
      router.push("/");
    }
  }
};
const openMenu = (tag: ITag, e: MouseEvent) => {
  const menuMinWidth = 105;
  const divContainer = document.getElementById("tags-view-container");
  if (!divContainer) {
    return;
  }
  const offsetLeft = divContainer.getBoundingClientRect().left; // container margin left
  const offsetWidth = divContainer.offsetWidth; // container width
  const maxLeft = offsetWidth - menuMinWidth; // left boundary
  const newleft = e.clientX - offsetLeft + 15; // 15: margin right

  if (newleft > maxLeft) {
    left.value = maxLeft;
  } else {
    left.value = newleft;
  }
  top.value = e.clientY;
  visible.value = true;
  selectedTag.value = tag;
};
const closeMenu = () => {
  visible.value = false;
};

onMounted(() => {
  initTags();
  addTags();
});

watch(
  router.currentRoute,
  (newVal, oldVal) => {
    addTags();
    moveToCurrentTag();
  },
  { deep: false }
);
watch(visible, (value) => {
  if (value) {
    document.body.addEventListener("click", closeMenu);
  } else {
    document.body.removeEventListener("click", closeMenu);
  }
});
</script>

<style lang="scss" scoped>
.tags-view-container {
  height: 32px;
  width: 100%;
  background: #fff;
  border-bottom: 1px solid #d8dce5;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.12), 0 0 3px 0 rgba(0, 0, 0, 0.04);

  .tags-view-wrapper {
    .tags-view-item {
      display: inline-block;
      position: relative;
      cursor: pointer;
      line-height: 26px;
      border: 1px solid #d8dce5;
      color: #495060;
      background: #fff;
      padding: 0 8px;
      font-size: 12px;
      margin-left: 5px;
      margin-top: 4px;

      &:first-of-type {
        margin-left: 15px;
      }

      &:last-of-type {
        margin-right: 15px;
      }

      &.active {
        color: #fff;

        &::before {
          content: "";
          background: #fff;
          display: inline-block;
          width: 8px;
          height: 8px;
          border-radius: 50%;
          position: relative;
          margin-right: 2px;
        }
      }
    }
  }
}
</style>

<style lang="scss">
//reset element css of el-icon-close
.tags-view-wrapper {
  .tags-view-item {
    .el-icon-close {
      width: 16px;
      height: 16px;
      vertical-align: middle;
      border-radius: 50%;
      text-align: center;
      transition: all 0.3s cubic-bezier(0.645, 0.045, 0.355, 1);
      transform-origin: 100% 50%;

      &:before {
        transform: scale(0.6);
        display: inline-block;
        vertical-align: -3px;
      }

      &:hover {
        background-color: #b4bccc;
        color: #fff;
      }
    }
  }
}
</style>
