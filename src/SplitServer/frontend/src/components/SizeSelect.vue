<template>
  <div style="padding: 0 15px">
    <el-dropdown
      trigger="click"
      @command="handleSetSize"
      style="vertical-align: middle"
    >
      <div>
        <svg-icon class-name="size-icon" icon-class="size" />
      </div>
      <template #dropdown>
        <el-dropdown-menu>
          <el-dropdown-item
            v-for="item of sizeOptions"
            :key="item.value"
            :disabled="size === item.value"
            :command="item.value"
          >
            {{ $t(`label.sizeSelection.${item.label}`) }}
          </el-dropdown-item>
        </el-dropdown-menu>
      </template>
    </el-dropdown>
  </div>
</template>

<script>
export default {
  name: "SizeSelect",
  data() {
    return {
      sizeOptions: [
        { label: "large", value: "large" },
        { label: "medium", value: "default" },
        { label: "small", value: "small" },
      ],
    };
  },
  computed: {
    size() {
      return this.$store.getters.size;
    },
  },
  methods: {
    handleSetSize(size) {
      this.$store.dispatch("app/setSize", size);
      this.refreshView();
    },
    refreshView() {
      this.$store.dispatch("tagsView/delAllCachedViews", this.$route);
      this.$router.go(0);
    },
  },
};
</script>
