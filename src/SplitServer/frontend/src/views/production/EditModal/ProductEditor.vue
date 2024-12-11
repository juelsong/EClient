<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Product.entity'
    )}`"
    width="400px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="editForm"
      :model="modelInner"
      :rules="rules"
      label-width="110px"
      label-position="right"
    >
      <el-form-item :label="$t('Product.editor.ProductType')" prop="Name">
        <o-data-selector
          ref="ProductType"
          :placeholder="`${$t('template.select', [
            $t('Product.editor.ProductType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.ProductTypeId"
          entity="ProductType"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('Product.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Product.editor.Name')"
        ></el-input>
      </el-form-item>
      <!-- <el-form-item
        :label="$t('Product.editor.ChainOfCustody')"
        prop="ChainOfCustody"
      >
        <el-switch
          ref="modelInner.ChainOfCustody"
          v-model="modelInner.ChainOfCustody"
          :placeholder="$t('Product.editor.ChainOfCustody')"
        ></el-switch>
      </el-form-item> -->
      <el-form-item :label="$t('Product.editor.Location')" prop="Location">
        <region-tree ref="regionTreeRef" v-model="modelInner.LocationId" />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed, ref } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";

import RegionTree from "@/components/RegionTree.vue";
export default defineComponent({
  name: "ProductEditor",
  components: { ODataSelector, RegionTree },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Object,
    },
    createNew: {
      type: Boolean,
      default: false,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const modelInner = ref({});
    return { visibleInner, modelInner };
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        this.$refs["modelInner.Name"].focus();
        if (newVal) {
          this.$refs.regionTreeRef.loadData();
        }
        this.$refs.editForm.clearValidate();
        this.$refs["ProductType"].loadData();
      });
    },
  },
  data() {
    return {
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Product.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist("Product", "Name", value, this.modelInner.Id).then(
                  (exist) => {
                    if (exist) {
                      callback(
                        new Error(
                          this.$t("validator.template.exist", [
                            this.$t("Product.editor.Name"),
                          ])
                        )
                      );
                    } else {
                      callback();
                    }
                  }
                );
              }
            },
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
  },
});
</script>
