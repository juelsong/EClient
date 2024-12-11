<template>
  <div>
    <el-dropdown
      trigger="click"
      @command="handleSetLocale"
      style="vertical-align: middle"
    >
      <div>
        <el-icon :size="24" :color="color">
          <svg-icon class-name="size-icon" icon-class="language" />
        </el-icon>
      </div>
      <template #dropdown>
        <el-dropdown-menu>
          <el-dropdown-item
            v-for="item of localeOptions"
            :key="item.value"
            :disabled="locale === item.value"
            :command="item.value"
          >
            {{ item.label }}
          </el-dropdown-item>
        </el-dropdown-menu>
      </template>
    </el-dropdown>
  </div>
</template>

<script>
import { request } from "@/utils/request";

export default {
  name: "LocalSelector",
  props: {
    color: {
      type: String,
      required: false,
      default: undefined,
    },
  },
  data() {
    return {
      localeOptions: [
        { label: "简体中文", value: "zh-cn" },
        { label: "English", value: "en" },
      ],
    };
  },
  computed: {
    locale() {
      return this.$store.getters.locale;
    },
  },
  methods: {
    handleSetLocale(locale) {
      this.$i18n.locale = locale;
      this.$store.dispatch("app/setLocale", locale);
      request({
        url: `/Locale/UpDateLocale`,
        method: "post",
        data: { Locale: locale },
      });

      // .then((ret) => {
      //   if (ret.data) {
      //     that.$emit("clearChangedState");
      //     that.$message.success(that.$t("prompt.success"));
      //   } else {
      //     that.$message.error(that.$t("prompt.failed"));
      //   }
      //   that.clearSaveData();
      //   that.loadData();
      // });
    },
  },
};
</script>
