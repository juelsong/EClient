<script lang="ts">
import * as vue from "vue";
import { SecurityModel } from "@/defs/Model";
import { querySecurityConfig, editSecurityConfig } from "@/api/security_gen";
import { ElForm, ElMessage } from "element-plus";
import { i18n } from "@/i18n";

export default vue.defineComponent({
  name: "SecuritySettings",
  setup() {
    const passwordSettings = vue.ref<SecurityModel>({
      Rules: {
        IncludeSpecial: false,
        IncludeLower: false,
        IncludeUpper: false,
        MinLength: 0,
        IncludeNumber: false,
      },
      ChangePassUponFirstLogin: false,
      CanNotRepeatedTimes: 0,
      InvalidLoginAttempts: 0,
    });
    const passwordFormRef = vue.ref<typeof ElForm>();
    const expiryPeriodDays = vue.ref(0);
    const passwordFormRules = {};
    async function onAcceptClick() {
      if (passwordFormRef.value) {
        const valid = await passwordFormRef.value.validate();
        if (valid) {
          if (expiryPeriodDays.value == 0) {
            passwordSettings.value.ExpiryPeriod = undefined;
          } else {
            passwordSettings.value.ExpiryPeriod = `${expiryPeriodDays.value}.00:00:00`;
          }
          const rsp = await editSecurityConfig(passwordSettings.value);

          if (rsp.success) {
            ElMessage.success(i18n.global.t("prompt.success"));
          } else {
            ElMessage.error(i18n.global.t("prompt.failed"));
          }
        }
      }
    }
    async function loadSettings() {
      const configResult = await querySecurityConfig();
      if (configResult.success) {
        passwordSettings.value = configResult.data;
        if (passwordSettings.value.ExpiryPeriod) {
          expiryPeriodDays.value = Number.parseInt(
            passwordSettings.value.ExpiryPeriod.split(":")[0]
          );
        } else {
          expiryPeriodDays.value = 0;
        }
        if (!passwordSettings.value.Rules) {
          passwordSettings.value.Rules = {};
        }
        if (!passwordSettings.value.Rules!.MinLength) {
          passwordSettings.value.Rules!.MinLength = 0;
        }
      }
    }

    loadSettings();

    return {
      passwordFormRef,
      passwordSettings,
      passwordFormRules,
      onAcceptClick,
      loadSettings,
      expiryPeriodDays,
    };
  },
});
</script>

<template>
  <el-scrollbar>
    <el-form
      class="password_form"
      ref="passwordFormRef"
      :model="passwordSettings"
      :rules="passwordFormRules"
    >
      <span class="group_title">{{ $t("Settings.password.Validity") }}</span>
      <el-form-item prop="IncludeNumber">
        <el-checkbox
          v-model="passwordSettings.Rules!.IncludeNumber"
          :label="$t('Settings.password.IncludeNumber')"
        />
      </el-form-item>

      <el-form-item prop="IncludeLower">
        <el-checkbox
          v-model="passwordSettings.Rules!.IncludeLower"
          :label="$t('Settings.password.IncludeLower')"
        />
      </el-form-item>
      <el-form-item prop="IncludeUpper">
        <el-checkbox
          v-model="passwordSettings.Rules!.IncludeUpper"
          :label="$t('Settings.password.IncludeUpper')"
        />
      </el-form-item>
      <el-form-item prop="IncludeSpecial">
        <el-checkbox
          v-model="passwordSettings.Rules!.IncludeSpecial"
          :label="$t('Settings.password.IncludeSpecial')"
        />
      </el-form-item>

      <el-form-item prop="MinLength">
        <span>
          {{ $t("Settings.password.MinLength") }}
        </span>
        <span class="tips">{{ $t("Settings.password.ZeroForNotSet") }}</span>
        <el-input-number v-model="passwordSettings.Rules!.MinLength" :min="0" />
      </el-form-item>

      <span class="group_title">
        {{ $t("Settings.password.Security") }}
      </span>
      <el-form-item prop="ChangePassUponFirstLogin">
        <el-checkbox
          v-model="passwordSettings.ChangePassUponFirstLogin"
          :label="$t('Settings.password.ChangePassUponFirstLogin')"
        />
      </el-form-item>

      <el-form-item prop="CanNotRepeatedTimes">
        <span>
          {{ $t("Settings.password.CanNotRepeatedTimes") }}
        </span>
        <span class="tips">{{ $t("Settings.password.ZeroForNotSet") }}</span>
        <el-input-number
          v-model="passwordSettings.CanNotRepeatedTimes"
          :min="0"
        />
      </el-form-item>
      <el-form-item prop="ExpiryPeriod">
        <span>
          {{ $t("Settings.password.ExpiryPeriod") }}
        </span>
        <span class="tips">{{ $t("Settings.password.ZeroForNotSet") }}</span>
        <el-input-number v-model="expiryPeriodDays" :min="0" />
      </el-form-item>
      <el-form-item prop="InvalidLoginAttempts">
        <span>
          {{ $t("Settings.password.InvalidLoginAttempts") }}
        </span>
        <span class="tips">{{ $t("Settings.password.ZeroForNotSet") }}</span>
        <el-input-number
          v-model="passwordSettings.InvalidLoginAttempts"
          :min="0"
        />
      </el-form-item>
      <el-button
        type="primary"
        @click="onAcceptClick"
        v-has="'Security:Password'"
      >
        {{ $t("template.accept") }}
      </el-button>
    </el-form>
  </el-scrollbar>
</template>

<style lang="scss" scoped>
.group_title {
  font-weight: bold;
  font-size: 16px;
  margin: 16px 0;
  display: inline-block;
}
.tips {
  color: #888888;
}
.password_form {
  :deep(.el-form-item) {
    margin: 4px 0 4px 16px;
  }
}
</style>
