<template>
  <el-dialog
    v-model="visibleInner"
    :title="`${$t('template.search')}-${$t('Visualization.query')}`"
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
      label-width="80px"
      label-position="right"
    >
      <!-- <el-form-item :label="$t('Visualization.filter.Barcode')">
        <el-input v-model="queryModelInner.BarCode"></el-input>
      </el-form-item> -->
      <!-- <el-form-item :label="$t('Visualization.filter.Complete')">
        <el-switch v-model="queryModelInner.Completed"></el-switch>
      </el-form-item>
      <el-form-item :label="$t('Visualization.filter.Approved')">
        <el-switch v-model="queryModelInner.Approved"></el-switch>
      </el-form-item> -->
      <!-- <el-form-item :label="$t('Visualization.filter.Product')">
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Visualization.filter.Product'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="queryModelInner.Product"
          filter="IsActive eq true"
          entity="Product"
          label="Name"
          value="Name"
        />
      </el-form-item> -->
      <el-form-item :label="$t('Visualization.filter.TestType')">
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Visualization.filter.TestType'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="queryModelInner.TestType"
          filter="IsActive eq true"
          entity="TestType"
          label="Description"
          value="Description"
        />
      </el-form-item>
      <!-- <el-form-item :label="$t('Visualization.filter.Organism')">
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Visualization.filter.Organism'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="queryModelInner.Organism"
          filter="IsActive eq true"
          entity="Organism"
          label="Species"
          value="Species"
        />
      </el-form-item> -->
      <el-form-item :label="$t('Visualization.filter.Location')">
        <region-tree ref="regionTreeRef" v-model="queryModelInner.Location" />
      </el-form-item>
      <el-form-item :label="$t('Visualization.filter.TestUser')">
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Visualization.filter.TestUser'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="queryModelInner.User"
          filter="(IsActive eq true) and (Status eq 'Normal')"
          entity="User"
          label="RealName"
          value="RealName"
        />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import ODataSelector from "@/components/ODataSelector.vue";
import cloneDeep from "lodash.clonedeep";
import RegionTree from "@/components/RegionTree.vue";

export default defineComponent({
  name: "VisualizatioinSearch",
  components: { ODataSelector, RegionTree },
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
        if (newVal) {
          this.$refs.regionTreeRef.loadData();
        }
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
