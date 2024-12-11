<template>
  <div class="login-container">
    <!-- <local-selector class="i18nselector" /> -->
    <el-form
      ref="loginFormRef"
      :model="loginParameter"
      :rules="loginRules"
      class="login-form"
      autocomplete="on"
      label-position="left"
    >
      <div class="title-container">
        <h3 class="title">Login Form</h3>
      </div>

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
            src="../../assets/checkcode.png"
            @click="handleChangeCheckCode"
          />
        </el-col>
      </el-row>
      <el-button
        :loading="loading"
        type="primary"
        style="width: 100%; margin-bottom: 30px"
        native-type="submit"
        @click.prevent="handleLogin"
      >
        {{ $t("label.login") }}
      </el-button>
    </el-form>

    <self-password ref="changePassDialogRef" :title="changePassDialogTitle" />
  </div>
</template>

<script lang="ts">
import { randomImg, login } from "@/api/user";
import router from "@/router";
import store from "@/store";
import { reactive, ref, nextTick, onMounted, watch, inject } from "vue";
import { ElInput, ElForm, ElMessage } from "element-plus";
import SelfPassword from "@/layout/components/SelfPassword.vue";
import { i18n } from "@/i18n";
import { LocationQuery } from "vue-router";
export default {
  name: "Login",
  components: { SelfPassword },
  setup(props, ctx) {
    const modelInner = reactive({
      OriPassword: "",
      Password: "",
      Password2: "",
    });
    const passwordType = ref<string>("password");
    const changePassDialogRef = ref<typeof SelfPassword>();
    const isSyncTool = inject<boolean>("isSyncTool");
    function showPwd() {
      if (passwordType.value === "password") {
        passwordType.value = "";
      } else {
        passwordType.value = "password";
      }
      nextTick(() => {
        if (passwordRef.value) {
          passwordRef.value.focus();
        }
      });
    }
    function checkCapslock(e) {
      const { key } = e;
      capsTooltip.value = key && key.length === 1 && key >= "A" && key <= "Z";
    }
    async function handleChangeCheckCode() {
      currdatetime.value = new Date().getTime();

      try {
        const res = await randomImg(currdatetime.value);
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
    const usernameRef = ref<typeof ElInput>();
    const passwordRef = ref<typeof ElInput>();
    const loginFormRef = ref<typeof ElForm>();

    const capsTooltip = ref(false);
    const loading = ref(false);
    const redirect = ref<string>();
    const otherQuery = ref<LocationQuery>({});
    const requestCodeSuccess = ref(false);
    const randCodeImage = ref("");
    const currdatetime = ref<number>(0);
    const changePassDialogTitle = ref("");
    const loginParameter = ref({
      username: "",
      password: "",
      captcha: "",
    });

    async function handleLogin() {
      if (!loginFormRef.value) {
        return;
      }
      const valid = await loginFormRef.value.validate();
      if (valid) {
        loading.value = true;

        try {
          let canSetToken = true;
          const { headers, data } = await login({
            Account: loginParameter.value.username.trim(),
            Password: loginParameter.value.password,
            Captcha: loginParameter.value.captcha,
            CheckKey: `${currdatetime.value}`,
          });

          const token = headers["access-token"];
          const refreshToken = headers["x-access-token"];

          loading.value = false;
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
            router.push({
              path: redirect.value || "/",
              query: otherQuery.value,
            });
          }
        } catch {
          loading.value = false;
        }
      }
    }

    function getOtherQuery(query) {
      return Object.keys(query).reduce((acc, cur) => {
        if (cur !== "redirect") {
          acc[cur] = query[cur];
        }
        return acc;
      }, {});
    }
    onMounted(() => {
      if (loginParameter.value.username === "" && usernameRef.value) {
        usernameRef.value.focus();
      } else if (loginParameter.value.password === "" && passwordRef.value) {
        passwordRef.value.focus();
      }
    });
    handleChangeCheckCode();
    watch(
      () => router.currentRoute,
      (val) => {
        redirect.value = isSyncTool
          ? "/syncData/syncData"
          : val.value.query.redirect?.toString();
        otherQuery.value = getOtherQuery(val.value.query);
      },
      { deep: true, immediate: true }
    );
    return {
      modelInner,
      usernameRef,
      passwordRef,
      passwordType,
      loginFormRef,
      currdatetime,
      showPwd,
      loginParameter,
      handleLogin,
      redirect,
      otherQuery,
      changePassDialogRef,
      requestCodeSuccess,
      randCodeImage,
      loading,
      capsTooltip,
      handleChangeCheckCode,
      changePassDialogTitle,
      checkCapslock,
    };
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
};
</script>

<style lang="scss">
$bg: #283443;
$light_gray: #fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .login-container .el-input input {
    color: $cursor;
  }
}

/* reset element-ui css */
.login-container {
  .el-input {
    display: inline-block;
    height: 47px;
    width: calc(100% - 30px);

    .el-input__wrapper {
      width: 100%;
      height: 100%;
      background-color: transparent;
      box-shadow: none;

      input {
        background: transparent;
        border: 0px;
        -webkit-appearance: none;
        border-radius: 0px;
        padding: 12px 5px 12px 15px;
        color: $light_gray;
        height: 47px;
        caret-color: $cursor;

        &:-webkit-autofill {
          box-shadow: 0 0 0px 1000px $bg inset !important;
          -webkit-text-fill-color: $cursor !important;
        }
      }
    }
  }

  .el-form-item {
    border: 1px solid rgba(255, 255, 255, 0.1);
    background: rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    color: #454545;
  }
}
</style>

<style lang="scss" scoped>
$bg: #2d3a4b;
$dark_gray: #889aa4;
$light_gray: #eee;

.login-container {
  min-height: 100%;
  width: 100%;
  background-color: $bg;
  overflow: hidden;

  .login-form {
    position: relative;
    width: 520px;
    max-width: 100%;
    padding: 160px 35px 0;
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
    color: $dark_gray;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
  }

  .title-container {
    position: relative;

    .title {
      font-size: 26px;
      color: $light_gray;
      margin: 0px auto 40px auto;
      text-align: center;
      font-weight: bold;
    }
  }

  .show-pwd {
    position: absolute;
    right: 10px;
    top: 7px;
    font-size: 16px;
    color: $dark_gray;
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
}
</style>
