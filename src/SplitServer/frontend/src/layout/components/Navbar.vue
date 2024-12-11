<template>
  <div class="navbar">
    <hamburger
      v-if="!isSyncTool"
      id="hamburger-container"
      :is-active="sidebar.opened"
      class="hamburger-container"
      @toggleClick="toggleSideBar"
    />
    <span class="title" v-else>{{ $t("Equipment.title.Sync") }}</span>

    <breadcrumb
      id="breadcrumb-container"
      class="breadcrumb-container"
      v-if="!isSyncTool"
    />

    <div class="right-menu">
      <template v-if="device !== 'mobile'">
        <el-tooltip
          v-if="isClient"
          :content="$t('Navbar.connection.tooltip')"
          effect="dark"
          placement="bottom"
        >
          <offline-selector
            class="right-menu-item hover-effect"
            color="#A0A0A0"
          />
        </el-tooltip>
        <el-tooltip
          :content="$t('tooltip.language')"
          effect="dark"
          placement="bottom"
        >
          <local-selector
            class="right-menu-item hover-effect"
            color="#A0A0A0"
          />
        </el-tooltip>
        <error-log class="errLog-container right-menu-item hover-effect" />

        <el-tooltip
          :content="$t('tooltip.globalSize')"
          effect="dark"
          placement="bottom"
        >
          <size-select id="size-select" class="right-menu-item hover-effect" />
        </el-tooltip>
      </template>

      <div
        class="avatar-wrapper avatar-container right-menu-item hover-effect"
        @click="handleAvatarClick"
      >
        <el-icon :size="24">
          <svg-icon icon-class="user" className="svg-icon" />
        </el-icon>
        <i class="el-icon-caret-bottom" />
        <span style="marginleft: 5px">
          {{ currentUser }}
        </span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import * as vue from "vue";
import { mapGetters } from "vuex";
import store from "@/store";
import Breadcrumb from "@/components/Breadcrumb.vue";
import Hamburger from "@/components/Hamburger.vue";
import ErrorLog from "@/components/ErrorLog.vue";
import SizeSelect from "@/components/SizeSelect.vue";
import LocalSelector from "@/components/LocalSelector.vue";
import OfflineSelector from "@/components/OfflineSelector.vue";

export default vue.defineComponent({
  name: "Navbar",
  computed: mapGetters(["sidebar", "avatar", "device"]),
  components: {
    Breadcrumb,
    Hamburger,
    ErrorLog,
    SizeSelect,
    LocalSelector,
    OfflineSelector,
  },
  setup(props, ctx) {
    const isClient = vue.inject<boolean>("isClient");
    const isSyncTool = vue.inject<boolean>("isSyncTool");
    const currentUser = vue.ref("");
    vue.onMounted(() => {
      currentUser.value = store.state.user.userName;
    });

    function toggleSideBar() {
      store.dispatch("app/toggleSideBar");
    }
    async function handleAvatarClick() {
      await store.dispatch("settings/changeSetting", {
        key: "showSettings",
        value: true,
      });
    }

    return {
      isClient,
      isSyncTool,
      currentUser,
      toggleSideBar,
      handleAvatarClick,
    };
  },
});
</script>

<style lang="scss" scoped>
.navbar {
  height: 50px;
  overflow: hidden;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);

  .hamburger-container {
    line-height: 46px;
    height: 100%;
    float: left;
    cursor: pointer;
    transition: background 0.3s;
    -webkit-tap-highlight-color: transparent;

    &:hover {
      background: rgba(0, 0, 0, 0.025);
    }
  }

  .title {
    margin-left: 16px;
    line-height: 46px;
    font-size: 24px;
    font-weight: bold;
  }

  .breadcrumb-container {
    float: left;
  }

  .errLog-container {
    display: inline-block;
    vertical-align: top;
  }

  .right-menu {
    float: right;
    height: 100%;
    line-height: 50px;

    &:focus {
      outline: none;
    }

    .right-menu-item {
      display: inline-block;
      padding: 0 8px;
      height: 100%;
      font-size: 18px;
      color: #5a5e66;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background 0.3s;

        &:hover {
          background: rgba(0, 0, 0, 0.025);
        }
      }
    }

    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 15px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}
</style>
