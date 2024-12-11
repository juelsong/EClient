
<template>
  <el-dialog
    v-model="visibleInner"
    :title="`${$t('template.search')}-${$t('EquipmentType.entity')}`"
    width="400px"
    @open="onDialogOpen"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" @click="onAcceptClick">
          {{ $t("template.search") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="queryForm"
      :model="queryModelInner"
      label-width="56px"
      label-position="right"
    >
      <el-form-item
        :label="$t('EquipmentType.filter.Description')"
        prop="Description"
      >
        <el-input
          ref="queryModelInner.Description"
          v-model="queryModelInner.Description"
          :placeholder="$t('EquipmentType.filter.Description')"
        ></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";

export default defineComponent({
  name: "EquipmentTypeSearch",
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
        this.$refs["queryModelInner.Description"].focus();
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
    onDialogOpen() {},
  },
});
</script>
