<script lang="ts">
import * as vue from "vue";
import { ElLoading, ElMessageBox, ElMessage } from "element-plus";
import store from "@/store";
import { i18n } from "@/i18n";
import { tryVerifyToken } from "@/api/user";
import RapidLogin from "@/components/RapidLogin.vue";

declare type ElLoadingType = ReturnType<typeof ElLoading.service>;
declare type CSharpCallback = (success: boolean) => void;
declare interface IServiceParameter {
  Url: string;
  Token: string;
  RefreshToken: string;
}
export default vue.defineComponent({
  name: "OfflineSelector",
  components: { RapidLogin },
  props: {
    color: {
      type: String,
      required: false,
      default: undefined,
    },
  },
  setup() {
    const isClientOffline = vue.computed({
      get() {
        return store.state.system.IsClientOffline === true;
      },
      set(val) {
        store.dispatch("system/setIsClientOffline", val);
      },
    });
    const rapidLoginRef = vue.ref<typeof RapidLogin>();
    const syncService = (window as any).syncService;
    const baseUrl = vue.ref<string | undefined>();
    let loadingInstance: ElLoadingType | undefined = undefined;

    function toggleOfflineCallback(toggleSuccess: boolean) {
      if (toggleSuccess) {
        isClientOffline.value = !isClientOffline.value;
      }
      closeLoading();
      if (!toggleSuccess) {
        // TODO 切换失败
      }
    }
    function doNothingCallback(toggleSuccess: boolean) {
      closeLoading();
      if (!toggleSuccess) {
        // TODO 切换失败
      }
    }
    async function download(callback: CSharpCallback) {
      const userId = store.state.user.id;
      const account = store.state.user.name;
      const token = store.getters.token;
      const refreshToken = store.getters.refreshToken;
      await syncService.setCurrentUserInfo(
        userId,
        account,
        token,
        refreshToken
      );
      const hasOfflineData = await syncService.getHasOfflineDataToUpload();
      // 没有需要上传离线数据  或  确认丢弃
      if (
        hasOfflineData === false ||
        (await ElMessageBox.confirm(
          i18n.global.t("Navbar.connection.confirmDropData"),
          i18n.global.t("confirm.title"),
          {
            confirmButtonText: i18n.global.t("template.accept"),
            cancelButtonText: i18n.global.t("template.cancel"),
            type: "warning",
            confirmButtonClass: "el-button--danger",
          }
        ))
      ) {
        showLoading();
        await syncService.downloadOfflineDataAsync(callback);
      }
    }
    async function upload(callback: CSharpCallback) {
      const serviceParameters: IServiceParameter =
        await syncService.getServiceParameters();
      baseUrl.value = serviceParameters.Url;
      store.commit("user/SET_TOKEN", serviceParameters.Token);
      store.commit("user/SET_REFRESH_TOKEN", serviceParameters.RefreshToken);
      const tokenValid = await tryVerifyToken(serviceParameters.Url);
      let canUpload = false;
      if (tokenValid === undefined) {
        // 无法连接服务
        ElMessage.error(i18n.global.t("prompt.canNotConnectServer"));
      } else if (tokenValid === false) {
        // TODO 重新登录
        const rapidLoginResult = await rapidLoginRef.value?.showDialog();
        canUpload = rapidLoginResult === true;
      } else {
        canUpload = true;
      }
      if (canUpload) {
        showLoading();
        await syncService.uploadOfflineDataAsync(callback);
      }
    }

    async function handleCommand(cmd: string) {
      store.state.system.IsClientOffline === true;
      if (cmd == "sync") {
        await upload(doNothingCallback);
        await download(doNothingCallback);
      } else if (cmd == "toggle") {
        if (isClientOffline.value === false) {
          // 从在线转为离线
          await download(toggleOfflineCallback);
        } else {
          //从离线转为在线
          await upload(toggleOfflineCallback);
        }
      }
    }
    let loadingCnt = 0;
    function showLoading() {
      loadingCnt++;
      loadingInstance = ElLoading.service({
        fullscreen: true,
      });
    }

    function closeLoading() {
      loadingCnt--;
      if (loadingCnt == 0) {
        loadingInstance?.close();
      }
    }

    return { handleCommand, isClientOffline, rapidLoginRef, baseUrl };
  },
});
</script>

<template>
  <div>
    <el-dropdown
      trigger="click"
      @command="handleCommand"
      style="vertical-align: middle"
    >
      <div>
        <el-icon :size="24" :color="color">
          <svg-icon
            class-name="size-icon"
            :icon-class="isClientOffline ? 'offline' : 'online'"
          />
        </el-icon>
      </div>
      <template #dropdown>
        <el-dropdown-menu>
          <el-dropdown-item :command="'toggle'">
            {{
              $t(
                isClientOffline
                  ? "Navbar.connection.online"
                  : "Navbar.connection.offline"
              )
            }}
          </el-dropdown-item>
          <el-dropdown-item
            :disabled="isClientOffline === true"
            :command="'sync'"
          >
            {{ $t("Navbar.connection.sync") }}
          </el-dropdown-item>
        </el-dropdown-menu>
      </template>
    </el-dropdown>
    <RapidLogin ref="rapidLoginRef" :base-url="baseUrl" />
  </div>
</template>
