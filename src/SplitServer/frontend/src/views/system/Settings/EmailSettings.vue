<script lang="ts">
import * as vue from "vue";
import { EMailConfig } from "@/defs/Model";
import { queryEmailConfig, editEmailConfig } from "@/api/notification_gen";
import { ElForm, ElMessage } from "element-plus";
import { i18n } from "@/i18n";
import { FormItemRule } from "@/defs/Types";

export default vue.defineComponent({
  name: "EmailSettings",
  setup() {
    const emailSettings = vue.ref<EMailConfig>({});
    const emailFormRef = vue.ref<typeof ElForm>();
    async function onAcceptClick() {
      if (emailFormRef.value) {
        const valid = await emailFormRef.value.validate();
        if (valid) {
          const rsp = await editEmailConfig(emailSettings.value);
          if (rsp.success) {
            ElMessage.success(i18n.global.t("prompt.success"));
          } else {
            ElMessage.error(i18n.global.t("prompt.failed"));
          }
        }
      }
    }
    async function loadSettings() {
      const configResult = await queryEmailConfig();
      if (configResult.success) {
        emailSettings.value = configResult.data;
      }
    }

    const ipRegex =
      /^(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)$/;
    const hostRegex = /^(\w+\.){2,}\w+$/;
    const emailFormRules: Record<string, FormItemRule[]> = {
      Server: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Settings.email.Server"),
            ]),
        },
        {
          validator: (rule, value: string, callback) => {
            if (value && value.length) {
              if (ipRegex.test(value) || hostRegex.test(value)) {
                callback();
                return;
              }
            }
            callback(i18n.global.t("Settings.email.validator.Server"));
          },
        },
      ],
      Port: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Settings.email.Port"),
            ]),
        },
        {
          type: "integer",
          min: 1,
          max: 65535,
          message: i18n.global.t("Settings.email.validator.PortRange"),
        },
      ],
      Address: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Settings.email.Address"),
            ]),
        },
        {
          type: "email",
          message: i18n.global.t("Settings.email.validator.AddressWrongFormat"),
        },
      ],
      Password: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Settings.email.Password"),
            ]),
        },
      ],
      EnableSSL: [],
    };
    loadSettings();
    return {
      emailFormRef,
      emailFormRules,
      emailSettings,
      onAcceptClick,
      loadSettings,
    };
  },
});
</script>

<template>
  <el-scrollbar>
    <el-form
      class="config_form"
      ref="emailFormRef"
      :model="emailSettings"
      :rules="emailFormRules"
      :label-width="'auto'"
    >
      <el-form-item prop="Server" :label="$t('Settings.email.Server')">
        <el-input v-model="emailSettings.Server" />
      </el-form-item>

      <el-form-item prop="Port" :label="$t('Settings.email.Port')">
        <el-input v-model.number="emailSettings.Port" />
      </el-form-item>
      <el-form-item prop="Address" :label="$t('Settings.email.Address')">
        <el-input v-model="emailSettings.Address" />
      </el-form-item>
      <el-form-item prop="Password" :label="$t('Settings.email.Password')">
        <el-input v-model="emailSettings.Password" />
      </el-form-item>

      <el-form-item prop="EnableSSL" :label="$t('Settings.email.EnableSSL')">
        <el-checkbox v-model="emailSettings.EnableSSL" />
      </el-form-item>
      <el-button type="primary" @click="onAcceptClick" v-has="'Security:Email'">
        {{ $t("template.accept") }}
      </el-button>
    </el-form>
  </el-scrollbar>
</template>

<style lang="scss" scoped>
.config_form {
  :deep(.el-form-item) {
    max-width: 400px;
  }
}
</style>
