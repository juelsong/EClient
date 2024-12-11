
<template>
  <el-dialog v-model="visibleInner"
             :title="`${$t('template.search')}-${$t('Department.entity')}`"
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
             label-width="56px"
             label-position="right">
      <el-form-item :label="$t('Department.filter.Name')"
                    prop="Name">
        <el-input ref="queryModelInner.Name"
                  v-model="queryModelInner.Name"
                  :placeholder="$t('Department.filter.Name')"></el-input>
      </el-form-item>
      <el-form-item :label="$t('Department.filter.Code')"
                    prop="Code">
        <el-input ref="queryModelInner.Code"
                  v-model="queryModelInner.Code"
                  :placeholder="$t('Department.filter.Code')"></el-input>
      </el-form-item>

      <!-- 2023年6月9日11:11:15 根据禅道158 已删除
        <el-form-item :label="$t('template.showDisabled')"
                    prop="IsActive">
        <el-switch v-model="queryModelInner.IsActive"
                   :active-value="true"
                   :inactive-value="false" />
      </el-form-item> -->
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";

export default defineComponent({
  name: "DepartmentSearch",
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
        this.$refs["queryModelInner.Name"].focus();
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
