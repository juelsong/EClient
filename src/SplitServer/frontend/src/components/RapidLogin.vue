<script lang="ts">
import * as vue from "vue";
import { ElForm, ElInput, ElMessage, ElButton } from "element-plus";
import SelfPassword from "@/layout/components/SelfPassword.vue";
import { i18n } from "@/i18n";
import store from "@/store";
import { randomImg, login } from "@/api/user";

declare type resolveCallback = (val: any) => void;

export default vue.defineComponent({
  name: "OfflineSelector",
  components: { SelfPassword },
  setup(props, ctx) {
    const requestCodeSuccess = vue.ref(false);
    const usernameRef = vue.ref<typeof ElInput>();
    const passwordRef = vue.ref<typeof ElInput>();
    const loginFormRef = vue.ref<typeof ElForm>();
    const btn = vue.ref<typeof ElButton>();
    const resolveKeeper = vue.ref<resolveCallback>();
    const dialogVisible = vue.ref(false);
    const loading = vue.ref(false);
    const currdatetime = vue.ref<number>(0);
    const changePassDialogRef = vue.ref<typeof SelfPassword>();
    const changePassDialogTitle = vue.ref("");
    const capsTooltip = vue.ref(false);
    const passwordType = vue.ref<string>("password");
    const randCodeImage = vue.ref("");
    const loginParameter = vue.ref({
      username: "",
      password: "",
      captcha: "",
    });
    vue.watch(dialogVisible, (newVal) => {
      vue.nextTick(() => {
        if (newVal) {
          setTimeout(() => {
            usernameRef.value?.focus();
            handleChangeCheckCode();
          }, 600);
        } else {
          loginFormRef.value?.resetFields();
          loginFormRef.value?.clearValidate();
        }
      });
    });

    function showPwd() {
      if (passwordType.value === "password") {
        passwordType.value = "";
      } else {
        passwordType.value = "password";
      }
      vue.nextTick(() => {
        if (passwordRef.value) {
          passwordRef.value.focus();
        }
      });
    }
    function checkCapslock(e) {
      const { key } = e;
      capsTooltip.value = key && key.length === 1 && key >= "A" && key <= "Z";
    }
    const onCancelClick = () => {
      dialogVisible.value = false;
      if (resolveKeeper.value) {
        resolveKeeper.value(false);
      }
    };
    const handleLogin = async () => {
      const valid = await loginFormRef.value?.validate();

      if (valid) {
        loading.value = true;

        try {
          let canSetToken = true;
          const { headers, data } = await login(
            {
              Account: loginParameter.value.username.trim(),
              Password: loginParameter.value.password,
              Captcha: loginParameter.value.captcha,
              CheckKey: `${currdatetime.value}`,
            },
            props.baseUrl
          );

          const token = headers["access-token"];
          const refreshToken = headers["x-access-token"];

          const shouldChangePass =
            data &&
            data.data &&
            (data.data.PasswordExpiried === true ||
              data.data.ChangePassUponFirstLogin === true);
          if (shouldChangePass) {
            // TODO 多语言
            changePassDialogTitle.value = data.data.ChangePassUponFirstLogin
              ? i18n.global.t("UserPassword.changePassUponFirstLoginTitle")
              : i18n.global.t("UserPassword.passwordExpiriedTitle");
            if (changePassDialogRef.value) {
              canSetToken = await changePassDialogRef.value.showDialog(token);
            }
          }

          if (canSetToken) {
            if (token) {
              store.commit("user/SET_TOKEN", token);
            }
            if (refreshToken) {
              store.commit("user/SET_REFRESH_TOKEN", refreshToken);
            }
          }
          if (resolveKeeper.value && canSetToken) {
            dialogVisible.value = false;
            resolveKeeper.value(true);
          }
        } catch {
        } finally {
          loading.value = false;
        }
      }
    };

    async function handleChangeCheckCode() {
      currdatetime.value = new Date().getTime();

      try {
        const res = await randomImg(currdatetime.value, props.baseUrl);
        if (res.success) {
          randCodeImage.value = res.data;
          requestCodeSuccess.value = true;
        } else {
          ElMessage.error(res.message);
          requestCodeSuccess.value = false;
        }
      } catch {
        requestCodeSuccess.value = false;
      }
    }

    function showDialog() {
      return new Promise((resolve) => {
        resolveKeeper.value = resolve;
        dialogVisible.value = true;
      });
    }
    ctx.expose({ showDialog });
    return {
      dialogVisible,
      randCodeImage,
      handleChangeCheckCode,
      showPwd,
      handleLogin,
      loginFormRef,
      checkCapslock,
      capsTooltip,
      requestCodeSuccess,
      btn,
      usernameRef,
      passwordRef,
      resolveKeeper,
      showDialog,
      loading,
      changePassDialogTitle,
      loginParameter,
      passwordType,
      onCancelClick,
    };
  },
  props: {
    baseUrl: String,
  },
  data() {
    return {
      loginRules: {
        username: [
          {
            required: true,
            trigger: "blur",
            message: i18n.global.t("validator.login.username"),
          },
        ],
        password: [
          {
            required: true,
            trigger: "blur",
            message: i18n.global.t("validator.login.password"),
          },
        ],
        captcha: [
          {
            required: true,
            trigger: "blur",
            message: i18n.global.t("validator.login.captcha"),
          },
        ],
      },
    };
  },
});
</script>

<template>
  <div class="root">
    <el-dialog
      v-model="dialogVisible"
      :close-on-click-modal="false"
      :title="$t('label.login')"
      width="400px"
      :show-close="false"
    >
      <template #footer>
        <span class="dialog-footer">
          <el-button ref="btn" @click="onCancelClick">
            {{ $t("template.cancel") }}
          </el-button>
          <el-button :loading="loading" type="primary" @click="handleLogin">
            {{ $t("template.accept") }}
          </el-button>
        </span>
      </template>

      <el-form
        ref="loginFormRef"
        :model="loginParameter"
        :rules="loginRules"
        class="login-form"
        autocomplete="on"
        label-position="left"
      >
        <el-form-item prop="username">
          <span class="svg-container">
            <svg-icon icon-class="user" />
          </span>
          <el-input
            ref="usernameRef"
            v-model="loginParameter.username"
            :placeholder="$t('placeholder.username')"
            name="username"
            type="primary"
            link
            tabindex="1"
            autocomplete="on"
          />
        </el-form-item>
        <el-tooltip
          v-model="capsTooltip"
          content="Caps lock is On"
          placement="right"
          manual
        >
          <el-form-item prop="password">
            <span class="svg-container">
              <svg-icon icon-class="password" />
            </span>
            <el-input
              :key="passwordType"
              ref="passwordRef"
              v-model="loginParameter.password"
              :type="passwordType"
              :placeholder="$t('placeholder.password')"
              name="password"
              tabindex="2"
              autocomplete="on"
              @keyup="checkCapslock"
              @blur="capsTooltip = false"
              @keyup.enter="handleLogin"
            />
            <span class="show-pwd" @click="showPwd">
              <svg-icon
                :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'"
              />
            </span>
          </el-form-item>
        </el-tooltip>

        <el-row :gutter="0">
          <el-col :span="16">
            <el-form-item prop="captcha">
              <span class="svg-container">
                <svg-icon icon-class="key" />
              </span>
              <el-input
                :placeholder="$t('placeholder.captcha')"
                type="primary"
                link
                tabindex="3"
                v-model="loginParameter.captcha"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="8" style="text-align: right">
            <img
              v-if="requestCodeSuccess"
              style="margin-top: 2px"
              :src="randCodeImage"
              @click="handleChangeCheckCode"
            />
            <img
              v-else
              style="margin-top: 2px"
              src="../assets/checkcode.png"
              @click="handleChangeCheckCode"
            />
          </el-col>
        </el-row>
      </el-form>
    </el-dialog>
    <self-password ref="changePassDialogRef" :title="changePassDialogTitle" />
  </div>
</template>

<style lang="scss" scoped>
.root {
  cursor: auto;
}

/* reset element-ui css */
.login-form {
  .el-input {
    display: inline-block;
    width: 85%;

    input {
      background: transparent;
      border: 0px;
      -webkit-appearance: none;
      border-radius: 0px;
      padding: 12px 5px 12px 15px;
      height: 47px;
    }
  }

  .el-form-item {
    border-radius: 5px;
  }
}

.login-form {
  // position: relative;
  // width: 520px;
  // max-width: 100%;
  // padding: 160px 35px 0;
  margin: 0 auto;
  overflow: hidden;
}

.tips {
  font-size: 14px;
  color: #fff;
  margin-bottom: 10px;

  span {
    &:first-of-type {
      margin-right: 16px;
    }
  }
}

.svg-container {
  padding: 6px 5px 6px 15px;
  vertical-align: middle;
  width: 30px;
  display: inline-block;
}

.title-container {
  position: relative;

  .title {
    font-size: 26px;
    margin: 0px auto 40px auto;
    text-align: center;
    font-weight: bold;
  }
}

.show-pwd {
  position: absolute;
  right: 6px;
  top: 7px;
  font-size: 16px;
  cursor: pointer;
  user-select: none;
}

.thirdparty-button {
  position: absolute;
  right: 0;
  bottom: 6px;
}

@media only screen and (max-width: 470px) {
  .thirdparty-button {
    display: none;
  }
}
</style>
