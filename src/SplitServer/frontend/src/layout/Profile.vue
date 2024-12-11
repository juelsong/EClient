<script lang="ts">
import * as vue from "vue";
import store from "@/store";
import { UserSettings, ChangePassSelf } from "@/defs/Model";
import { IOptions } from "@/defs/Types";
import moment from "moment";
import { ElForm, ElMessage } from "element-plus";
import { FormItemRule } from "@/defs/Types";
import { i18n } from "@/i18n";
import { editSelfPassword, editUserSettings } from "@/api/user_gen";
import { Router } from "vue-router";
// import router from "@/router";

const enDateTimeFormats: IOptions[] = [];
const zhcnDateTimeFormats: IOptions[] = [];
const enDateFormats: IOptions[] = [];
const zhcnDateFormats: IOptions[] = [];

const enDateTimeFormatStrings = [
  "MMM DD YYYY HH:mm:ss",
  "MM/DD/YYYY HH:mm:ss",
  "MM-DD-YYYY HH:mm:ss",
  "MM/DD/YYYY HH.mm.ss",
  "MM-DD-YYYY HH.mm.ss",
];
const zhcnDateTimeFormatStrings = [
  "YYYY年MM月DD HH时mm分ss秒",
  "YYYY/MM/DD HH:mm:ss",
  "YYYY-MM-DD HH:mm:ss",
  "YYYY/MM/DD HH.mm.ss",
  "YYYY-MM-DD HH.mm.ss",
];
const enDateFormatStrings = ["MMM DD YYYY", "MM/DD/YYYY", "MM-DD-YYYY"];
const zhcnDateFormatStrings = ["YYYY年MM月DD", "YYYY/MM/DD", "YYYY-MM-DD"];

const dateTimeSample = moment("2000/01/01 08:00:00", "YYYY/MM/DD HH:mm:ss");

enDateTimeFormats.push(
  ...enDateTimeFormatStrings.map((str) => {
    return { label: dateTimeSample.format(str), value: str };
  })
);
zhcnDateTimeFormats.push(
  ...zhcnDateTimeFormatStrings.map((str) => {
    return { label: dateTimeSample.format(str), value: str };
  })
);
enDateFormats.push(
  ...enDateFormatStrings.map((str) => {
    return { label: dateTimeSample.format(str), value: str };
  })
);
zhcnDateFormats.push(
  ...zhcnDateFormatStrings.map((str) => {
    return { label: dateTimeSample.format(str), value: str };
  })
);

export default vue.defineComponent({
  name: "Profile",
  setup(props, ctx) {
    const settings = vue.ref<UserSettings>({
      ...store.state.user.userSettings,
    });
    const dateTimeFormat = vue.computed(() => {
      if (store.getters.locale == "en") {
        return enDateTimeFormats;
      } else {
        return zhcnDateTimeFormats;
      }
    });
    const dateFormat = vue.computed(() => {
      if (store.getters.locale == "en") {
        return enDateFormats;
      } else {
        return zhcnDateFormats;
      }
    });
    const passwordFormRef = vue.ref<typeof ElForm>();
    const settingsFormRef = vue.ref<typeof ElForm>();
    const isSyncTool = vue.inject<boolean>("isSyncTool");
    const router = vue.ref<Router>();
    const dateTimeNow = vue.computed(() => {
      return moment().format(settings.value.DateTimeFormat);
    });
    const dateNow = vue.computed(() => {
      return moment().format(settings.value.DateFormat);
    });
    const changePasswordModel = vue.reactive({
      OriPassword: "",
      Password: "",
      Password2: "",
    });
    const account = vue.ref<string>(store.state.user.name);
    const changePasswordRuleData: Record<string, FormItemRule[]> = {
      OriPassword: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.OriPassword"),
            ]),
          trigger: "blur",
        },
      ],
      Password: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Password"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (!value || value === "") {
              callback(
                new Error(
                  i18n.global.t("validator.template.required", [
                    i18n.global.t("User.editor.Password"),
                  ])
                )
              );
            } else {
              if (changePasswordModel.Password2 !== "") {
                passwordFormRef.value?.validateField("Password2");
              }
              callback();
            }
          },
          trigger: "blur",
        },
      ],
      Password2: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Password"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (!value || value === "") {
              callback(
                new Error(
                  i18n.global.t("validator.template.required", [
                    i18n.global.t("User.editor.Password"),
                  ])
                )
              );
            } else if (value !== changePasswordModel.Password) {
              callback(
                new Error(i18n.global.t("User.validator.PasswordNotSame"))
              );
            } else {
              callback();
            }
          },
          trigger: "blur",
        },
      ],
    };
    const changePasswordRules = vue.ref(changePasswordRuleData);

    vue.onMounted(() => {
      const instance = vue.getCurrentInstance();
      router.value = instance?.appContext.config.globalProperties.$router;
    });

    function onPasswordCancelClick() {
      passwordFormRef.value?.resetFields();
    }
    async function onPasswordAcceptClick() {
      const valid = await passwordFormRef.value?.validate();
      if (valid) {
        const data: ChangePassSelf = {
          OriPassword: changePasswordModel.OriPassword,
          Password: changePasswordModel.Password,
        };
        const ret = await editSelfPassword(data);
        if (ret.success) {
          passwordFormRef.value?.resetFields();
          ElMessage.success(i18n.global.t("prompt.success"));
        } else {
          // 应该不能执行到这里
          console.error(ret);
        }
      }
    }
    function onSettingsCancelClick() {
      if (settingsFormRef.value) {
        settingsFormRef.value.resetFields();
      }
    }
    async function onSettingsAcceptClick() {
      const valid = await settingsFormRef.value?.validate();
      if (valid) {
        const ret = await editUserSettings(settings.value);
        if (ret.success) {
          await store.dispatch("user/setUserSettings", settings.value);
          ElMessage.success(i18n.global.t("prompt.success"));
        } else {
          // 应该不能执行到这里
          console.error(ret);
        }
      }
    }

    async function handleLogoutClick() {
      await store.dispatch("user/logout");
      await store.dispatch("settings/changeSetting", {
        key: "showSettings",
        value: false,
      });
      // 不用redirect如果权限更改处理麻烦
      router.value?.push(`/login`);
    }
    async function handleDashboardClick() {
      await store.dispatch("settings/changeSetting", {
        key: "showSettings",
        value: false,
      });
    }
    return {
      isSyncTool,
      settings,
      account,
      dateTimeFormat,
      dateFormat,
      dateTimeNow,
      dateNow,
      passwordFormRef,
      settingsFormRef,
      changePasswordModel,
      changePasswordRules,
      onPasswordCancelClick,
      onPasswordAcceptClick,
      onSettingsCancelClick,
      onSettingsAcceptClick,
      handleLogoutClick,
      handleDashboardClick,
    };
  },
});
</script>

<template>
  <div :style="{ height: '100%', position: 'relative' }">
    <div class="profile-content">
      <el-scrollbar>
        <div class="group-header">
          <el-icon :size="20">
            <svg-icon icon-class="user" className="svg-icon" />
          </el-icon>
          <span class="group-title">
            {{ $t("Profile.label.accountTitle") }}
          </span>
        </div>
        <div class="group-item">
          <span>{{ $t("Profile.label.account") }}</span>
          <span class="group-item-content">{{ account }}</span>
        </div>
        <el-divider />

        <div class="group-header">
          <el-icon :size="20">
            <svg-icon icon-class="calendar" className="svg-icon" />
          </el-icon>
          <span class="group-title">
            {{ $t("Profile.label.dateTimeFormatTitle") }}
          </span>
        </div>
        <div class="group-item">
          <!-- <span>{{ $t("Profile.label.dateTimeFormat") }}</span> -->

          <el-form ref="settingsFormRef" :model="settings">
            <el-form-item
              :label="$t('Profile.label.dateTimeFormat')"
              prop="DateTimeFormat"
            >
              <el-select
                v-model="settings.DateTimeFormat"
                class="group-item-content"
              >
                <el-option
                  v-for="item in dateTimeFormat"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>
            <div class="border-radius">
              {{ dateTimeNow }}
            </div>
            <el-form-item
              :label="$t('Profile.label.dateFormat')"
              prop="DateFormat"
            >
              <el-select
                v-model="settings.DateFormat"
                class="group-item-content"
              >
                <el-option
                  v-for="item in dateFormat"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>

            <div class="border-radius">
              {{ dateNow }}
            </div>

            <el-form-item>
              <el-button type="primary" @click="onSettingsAcceptClick">
                {{ $t("template.accept") }}
              </el-button>
              <el-button @click="onSettingsCancelClick">
                {{ $t("template.cancel") }}
              </el-button>
            </el-form-item>
          </el-form>
        </div>

        <el-collapse v-if="$store.getters.isESysSecurity">
          <el-collapse-item name="password">
            <template #title>
              <div class="group-header">
                <el-icon :size="20">
                  <svg-icon icon-class="key" className="svg-icon" />
                </el-icon>
                <span class="group-title">
                  {{ $t("Profile.label.changepass") }}
                </span>
              </div>
            </template>
            <el-form
              ref="passwordFormRef"
              :model="changePasswordModel"
              :rules="changePasswordRules"
              label-width="82px"
              label-position="right"
            >
              <el-form-item
                :label="$t('User.editor.OriPassword')"
                prop="OriPassword"
              >
                <el-input
                  show-password
                  ref="oriPassInput"
                  v-model="changePasswordModel.OriPassword"
                  :placeholder="$t('User.editor.OriPassword')"
                ></el-input>
              </el-form-item>
              <el-form-item :label="$t('User.editor.Password')" prop="Password">
                <el-input
                  show-password
                  v-model="changePasswordModel.Password"
                  :placeholder="$t('User.editor.Password')"
                ></el-input>
              </el-form-item>
              <el-form-item
                :label="$t('User.editor.Password2')"
                prop="Password2"
              >
                <el-input
                  show-password
                  v-model="changePasswordModel.Password2"
                  :placeholder="$t('User.editor.Password2')"
                ></el-input>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="onPasswordAcceptClick">
                  {{ $t("template.accept") }}
                </el-button>
                <el-button @click="onPasswordCancelClick">
                  {{ $t("template.cancel") }}
                </el-button>
              </el-form-item>
            </el-form>
          </el-collapse-item>
        </el-collapse>
      </el-scrollbar>
    </div>
    <div class="profile-footer">
      <!-- <router-link to="/">
        <el-button @click="handleDashboardClick">
          {{ $t("Profile.label.dashboard") }}
        </el-button>
      </router-link> -->
      <el-button @click="handleLogoutClick">
        {{ $t("Profile.label.logout") }}
      </el-button>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.group-header {
  margin: 10px 24px;
  display: flex;
  align-items: center;
  .group-title {
    margin: 0px 8px;
    font-size: 16px;
  }
}

.group-item {
  margin: 10px 52px;
  .group-item-content {
    margin-left: 10px;
  }
}

.border-radius {
  border: 1px solid #5a5e66;
  border-radius: 4px;
  padding: 10px;
  margin: 10px;
}
$profile-footer-height: 45px;
.profile-content {
  height: calc(100% - $profile-footer-height);
}
.profile-footer {
  position: absolute;
  bottom: 0px;
  left: 0px;
  right: 0px;
  height: $profile-footer-height;
  .el-button {
    height: 100%;
    width: 100%;
    display: block;
    margin: 0px;
    background-color: rgb(249, 249, 249);
    border-radius: 0px;
    border-bottom-width: 0px;
  }
}
</style>
