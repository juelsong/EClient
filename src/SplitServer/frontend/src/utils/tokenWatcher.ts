import moment from "moment";
import asyncSleep from "./AsyncSleeper";
import { getToken, getRefreshToken } from "@/utils/auth";
import { createToken } from "@/api/user_gen";
import { getExpDateFromToken } from "@/utils/global";
import store from "@/store";
import * as vue from "vue";
import { i18n } from "@/i18n";
import { ElDialog, ElButton } from "element-plus";
import { Router } from "vue-router";

const visiable = vue.ref(true);
let tokenFlag = false;
let container: HTMLDivElement | undefined = undefined;
const router = vue.ref<Router>();

function closeDialog() {
  visiable.value = false;
  setTimeout(() => {
    if (container) {
      vue.render(null, container!);
    }
  }, 500);
}

function showPromptDialog() {
  return new Promise<void>((resolve) => {
    container = document.createElement("div");
    visiable.value = true;
    const vnode = vue.createVNode(
      ElDialog,
      {
        title: i18n.global.t("template.tips"),
        width: "300px",
        modelValue: visiable.value,
        destroyOnClose: true,
        showClose: false,
        closeOnClickModal: false,
        closeOnPressEscape: false,
        onClosed: () => {
          vue.render(null, container!);
        },
      },
      {
        default: () => vue.h("div", null, i18n.global.t("prompt.refreshToken")),
        footer: () =>
          vue.h(
            "span",
            {
              class: "dialog-footer",
            },
            [
              vue.h(
                ElButton,
                {
                  onClick: async () => {
                    const refreshTokenExp = getExpDateFromToken(
                      getRefreshToken()
                    );
                    const now = moment();
                    if (refreshTokenExp && refreshTokenExp.isAfter(now)) {
                      await createToken();
                      const syncService = (window as any).syncService;
                      if (syncService) {
                        await syncService.setCurrentUserInfo(
                          store.getters.userId,
                          store.getters.name,
                          getToken(),
                          getRefreshToken()
                        );
                      }
                    } else {
                      await store.dispatch("user/logout");
                      router.value?.push(`/login`);
                    }
                    resolve();
                    closeDialog();
                  },
                  type: "primary",
                },
                {
                  default: () => i18n.global.t("template.accept"),
                }
              ),
            ]
          ),
      }
    );
    vue.render(vnode, container);
    document.body.appendChild(container.firstElementChild!);
  });
}

async function autoCheckTokenCore() {
  while (tokenFlag) {
    const tokenExp = getExpDateFromToken(getToken());
    const refreshTokenExp = getExpDateFromToken(getRefreshToken());
    if (tokenExp && refreshTokenExp) {
      const clockSkew: number = store.state.system.ClockSkew;
      tokenExp.add(clockSkew, "seconds");
      const now = moment();
      if (
        tokenExp.isBefore(now) &&
        refreshTokenExp.isAfter(now) &&
        !visiable.value
      ) {
        await showPromptDialog();
      }
    }
    await asyncSleep(100);
  }
}

async function aotoClosePromptDialog() {
  while (tokenFlag) {
    const tokenExp = getExpDateFromToken(getToken());
    const refreshTokenExp = getExpDateFromToken(getRefreshToken());
    if (tokenExp && refreshTokenExp) {
      const clockSkew: number = store.state.system.ClockSkew;
      tokenExp.add(clockSkew, "seconds");
      const now = moment();
      if (tokenExp.isAfter(now) && visiable.value) {
        closeDialog();
      }
    }

    await asyncSleep(100);
  }
}

export function autoCheckToken() {
  tokenFlag = true;
  autoCheckTokenCore();
  aotoClosePromptDialog();
}

export function stopCheckToken() {
  tokenFlag = false;
  closeDialog();
}
