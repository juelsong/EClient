<script lang="ts">
import * as vue from "vue";
import SecuritySettings from "./SecuritySettings.vue";
import EmailSettings from "./EmailSettings.vue";
import ImportExport from "./ImportExport.vue";

import store from "@/store";
export default vue.defineComponent({
  name: "Settings",
  components: { SecuritySettings, EmailSettings, ImportExport },
  setup() {
    const activeName = vue.ref("password");
    const emailSettingsRef = vue.ref<typeof EmailSettings>();
    const securitySettingsRef = vue.ref<typeof SecuritySettings>();
    const importExportRef = vue.ref<typeof ImportExport>();

    vue.watch(activeName, (newName) => {
      if (newName == "password" && securitySettingsRef.value) {
        securitySettingsRef.value.loadSettings();
      }
      if (newName == "email" && emailSettingsRef.value) {
        emailSettingsRef.value.loadSettings();
      }
      if (newName == "importExport" && emailSettingsRef.value) {
        emailSettingsRef.value.loadSettings();
      }
    });
    return {
      activeName,
      securitySettingsRef,
      emailSettingsRef,
      importExportRef,
    };
  },
  data() {
    return {
      passwordShow: true,
      emailShow: true,
      importExportShow: true,
    };
  },
  mounted() {
    const permissions = this.$store.state.user.permissions.map((p) => p.Code);
    this.passwordShow =
      permissions.includes("Security:Password", 0) &&
      store.getters.isESysSecurity;
    this.emailShow = permissions.includes("Security:Email", 0);
    this.importExportShow = permissions.includes("Security:ImportExport", 0);
    if (this.passwordShow) {
      this.activeName = "password";
    } else {
      if (this.emailShow) {
        this.activeName = "email";
      } else {
        this.activeName = "importExport";
      }
    }
  },
});
</script>

<template>
  <el-tabs v-model="activeName" class="single-tabs">
    <el-tab-pane
      :label="$t('Settings.password.Title')"
      name="password"
      v-if="passwordShow"
    >
      <SecuritySettings ref="securitySettingsRef" />
    </el-tab-pane>

    <el-tab-pane
      :label="$t('Settings.email.Title')"
      name="email"
      v-if="emailShow"
    >
      <EmailSettings ref="emailSettingsRef" />
    </el-tab-pane>
    <el-tab-pane
      :label="$t('Settings.importExport.Title')"
      name="importExport"
      v-if="importExportShow"
    >
      <ImportExport />
    </el-tab-pane>
  </el-tabs>
</template>
