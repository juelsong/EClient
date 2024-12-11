<template>
  <div :class="classObj" class="app-wrapper">
    <div
      v-if="device === 'mobile' && sidebar.opened"
      class="drawer-bg"
      @click="handleClickOutside"
    />
    <sidebar
      class="sidebar-container"
      v-if="isClient != true && isSyncTool != true"
    />
    <div :class="{ hasTagsView: needTagsView }" class="main-container">
      <div :class="{ 'fixed-header': fixedHeader }">
        <navbar />
        <tags-view v-if="needTagsView && !isSyncTool" />
      </div>
      <section :class="['app-main', `${isSyncTool ? 'app-main-sync' : ''}`]">
        <div
          ref="appMainInner"
          :class="
            appMainEnabled
              ? 'app-main-inner'
              : 'app-main-inner app-main-disable'
          "
        >
          <app-main />
        </div>
      </section>
      <el-drawer
        v-model="showSettings"
        :size="440"
        :title="$t('Profile.title')"
        direction="rtl"
      >
        <profile />
      </el-drawer>
    </div>
  </div>
</template>

<script>
import { AppMain, Navbar, Sidebar, TagsView } from "./components";
import Profile from "./Profile.vue";
import ResizeMixin from "./mixin/ResizeHandler";
import { mapState } from "vuex";
import { provide, ref } from "vue";

export default {
  name: "Layout",
  components: {
    AppMain,
    Navbar,
    Sidebar,
    TagsView,
    Profile,
  },
  setup() {
    const appMainEnabled = ref(true);
    const setAppMainInnerEnabled = (enabled) => {
      // this.$refs.appMainInner.addC
      appMainEnabled.value = enabled;
    };
    provide("setAppMainInnerEnabled", setAppMainInnerEnabled);
    return { appMainEnabled };
  },
  mixins: [ResizeMixin],
  inject: ["isClient", "isSyncTool"],
  computed: {
    ...mapState({
      sidebar: (state) => state.app.sidebar,
      device: (state) => state.app.device,
      needTagsView: (state) => state.settings.tagsView,
      fixedHeader: (state) => state.settings.fixedHeader,
    }),
    classObj() {
      return {
        hideSidebar: !this.sidebar.opened,
        openSidebar: this.sidebar.opened,
        withoutAnimation: this.sidebar.withoutAnimation,
        mobile:
          this.device === "mobile" ||
          this.isClient == true ||
          this.isSyncTool == true,
      };
    },
    showSettings: {
      get() {
        return this.$store.state.settings.showSettings;
      },
      set(val) {
        this.$store.dispatch("settings/changeSetting", {
          key: "showSettings",
          value: val,
        });
      },
    },
  },
  methods: {
    handleClickOutside() {
      this.$store.dispatch("app/closeSideBar", { withoutAnimation: false });
    },
  },
};
</script>

<style lang="scss" scoped>
@import "~@/styles/mixin.scss";
@import "~@/styles/variables.scss";

:deep(.el-drawer__header) {
  background-color: var(--el-color-primary);
  height: 64px;
  padding: 0px 20px;
  color: #fff;
  margin-bottom: 0px;
  .el-drawer__close-btn {
    color: #fff;
    transform-origin: 50%;
    :hover {
      color: #fff;
      transform: scale(1.2, 1.2);
    }
  }
}

:deep(.el-drawer__body) {
  padding: 20px 0px 0px 0px;
}

.app-wrapper {
  @include clearfix;
  position: relative;
  height: 100%;
  width: 100%;

  &.mobile.openSidebar {
    position: fixed;
    top: 0;
  }
}

.drawer-bg {
  background: #000;
  opacity: 0.3;
  width: 100%;
  top: 0;
  height: 100%;
  position: absolute;
  z-index: 999;
}

.fixed-header {
  position: fixed;
  top: 0;
  right: 0;
  z-index: 9;
  width: calc(100% - #{$sideBarWidth});
  transition: width 0.28s;
}

.hideSidebar .fixed-header {
  width: calc(100% - 54px);
}

.mobile .fixed-header {
  width: 100%;
}
</style>

<style lang="scss" scoped>
@import "../styles/variables.scss";

.app-main {
  /* 50= navbar  50  */
  min-height: calc(100vh - 50px);
  width: 100%;
  position: relative;
  overflow: hidden;
  padding: $--app-main-padding;
  background-color: #f0f2f5;
  .app-main-inner {
    width: 100%;
    padding: $--app-main-inner-padding;
    border-radius: 6px;
    min-height: calc(100vh - 50px - 2 * $--app-main-padding);
    background-color: white;
  }
  .app-main-disable {
    padding: 0px !important;
    background-color: transparent !important;
  }
}

.fixed-header + .app-main {
  padding-top: 50px;
}

.hasTagsView {
  .app-main {
    /* 84 = navbar + tags-view = 50 + 34 */
    min-height: calc(100vh - 84px);

    .app-main-inner {
      min-height: calc(100vh - 84px - 2 * $--app-main-padding);
      height: calc(100vh - 84px - 2 * $--app-main-padding);
    }
  }
  .app-main.app-main-sync {
    min-height: calc(100vh - 50px) !important;
    .app-main-inner {
      min-height: calc(100vh - 50px - 2 * $--app-main-padding);
      height: calc(100vh - 50px - 2 * $--app-main-padding);
    }
  }

  .fixed-header + .app-main {
    padding-top: 84px;
  }
}
</style>

<style lang="scss">
// fix css style bug in open el-dialog
.el-popup-parent--hidden {
  .fixed-header {
    padding-right: 15px;
  }
}
</style>
