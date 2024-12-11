
<template>
  <el-dialog v-model="visibleInner"
             :close-on-click-modal="false"
             :title="`${$t('template.search')}-${$t('User.entity')}`"
             width="400px">
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary"
                   @click="onAcceptClick">
          {{ $t("template.search") }}
        </el-button>
      </span>
    </template>
    <el-form ref="queryForm"
             :model="queryModelInner"
             label-width="28px"
             label-position="right">
      <el-form-item :label="$t('User.filter.Department')"
                    prop="Department">
        <o-data-selector :placeholder="`${$t('template.select', [
            $t('User.filter.Department'),
          ])}`"
                         :multiple="false"
                         :clearable="true"
                         v-model="queryModelInner.Department"
                         entity="Department"
                         label="Name"
                         value="Id" />
      </el-form-item>
      <el-form-item :label="$t('User.filter.RealName')"
                    prop="RealName">
        <el-input ref="queryModelInner.RealName"
                  v-model="queryModelInner.RealName"
                  :placeholder="$t('User.filter.RealName')"></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";

import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "UserSearch",
  components: { ODataSelector },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    queryModel: {
      type: Object,
    },
  },
  emits: ["update:visible", "update:queryModel", "search"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    return { visibleInner };
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.queryModel));
        this.queryModelInner = copyQuery;
      }
      this.$nextTick(() => {
        this.$refs.queryForm.clearValidate();
        this.$refs["queryModelInner.RealName"].focus();
      });
    },
  },
  data() {
    return {
      queryModelInner: {},
    };
  },
  methods: {
    onAcceptClick() {
      this.$emit("update:queryModel", this.queryModelInner);
      this.visibleInner = false;
      this.$emit("search");
    },
  },
});
</script>
