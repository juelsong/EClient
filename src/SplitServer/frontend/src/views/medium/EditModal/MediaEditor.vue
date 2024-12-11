<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Media.entity'
    )}`"
    width="450px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" v-no-more-click @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="editForm"
      :model="modelInner"
      :rules="rules"
      label-width="90px"
      label-position="right"
    >
      <el-form-item :label="$t('Media.editor.MediaType')" prop="MediaType">
        <o-data-selector
          ref="MediaType"
          :placeholder="`${$t('template.select', [
            $t('Media.editor.MediaType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.MediaTypeId"
          entity="MediaType"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('Media.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Media.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Media.editor.Barcode')" prop="Barcode">
        <el-input
          ref="modelInner.Barcode"
          v-model="modelInner.Barcode"
          :placeholder="$t('Media.editor.Barcode')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Media.editor.LotNumber')" prop="LotNumber">
        <el-input
          ref="modelInner.LotNumber"
          v-model="modelInner.LotNumber"
          :placeholder="$t('Media.editor.LotNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Media.editor.LotNumberExpDate')"
        prop="LotNumberExpDate"
      >
        <el-date-picker
          ref="modelInner.LotNumberExpDate"
          v-model="modelInner.LotNumberExpDate"
          type="date"
          :placeholder="`${$t('template.select', [
            $t('Media.editor.LotNumberExpDate'),
          ])}`"
        />
      </el-form-item>
      <el-form-item :label="$t('Media.editor.Location')" prop="Location">
        <region-tree ref="regionTreeRef" v-model="modelInner.LocationId" />
      </el-form-item>
      <el-form-item :label="$t('Media.editor.Vendor')" prop="Vendor">
        <el-input
          ref="modelInner.Vendor"
          v-model="modelInner.Vendor"
          :placeholder="$t('Media.editor.Vendor')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Media.editor.ManufacturerLotNumber')"
        prop="ManufacturerLotNumber"
      >
        <el-input
          ref="modelInner.ManufacturerLotNumber"
          v-model="modelInner.ManufacturerLotNumber"
          :placeholder="$t('Media.editor.ManufacturerLotNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Media.editor.GrowthPromotionStatus')"
        prop="GrowthPromotionStatus"
      >
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Media.editor.GrowthPromotionStatus'),
          ])}`"
          :multiple="false"
          v-model="modelInner.GrowthPromotionStatusId"
          entity="GrowthPromotionStatus"
          :label="getLableDescription()"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Media.editor.GrowthPromotionResult')"
        prop="GrowthPromotionResult"
      >
        <el-input
          ref="modelInner.GrowthPromotionResult"
          v-model="modelInner.GrowthPromotionResult"
          :rows="2"
          type="textarea"
          :placeholder="$t('Media.editor.GrowthPromotionResult')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Media.editor.Inventory')" prop="Inventory">
        <el-input-number
          ref="modelInner.Inventory"
          v-model="modelInner.Inventory"
          :min="0"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Media.editor.InventoryAdj')"
        prop="InventoryAdj"
      >
        <el-input-number
          ref="modelInner.InventoryAdj"
          v-model="modelInner.InventoryAdj"
          :min="0"
        />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, unref, computed, ref } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
import RegionTree from "@/components/RegionTree.vue";
import getLableDescription from "@/i18n/localeHelper";

export default defineComponent({
  name: "MediaEditor",
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
    MediaTypeId: {
      type: Number,
      default: null,
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
        let copyQuery = cloneDeep(unref(this.model));
        this.modelInner = copyQuery;
        if (this.modelInner.MediaTypeId == undefined) {
          if (this.MediaTypeId) {
            this.modelInner.MediaTypeId = this.MediaTypeId;
          }
        }
        if (this.modelInner.Inventory == undefined)
          this.modelInner.Inventory = 0;
        if (this.modelInner.InventoryAdj == undefined)
          this.modelInner.InventoryAdj = 0;
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        this.$refs["MediaType"].loadData();
        this.$refs["modelInner.Name"].focus();
        if (newVal) {
          this.$refs.regionTreeRef.loadData();
        }
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
                this.$t("Media.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist("Media", "Name", value, this.modelInner.Id).then(
                  (exist) => {
                    if (exist) {
                      callback(
                        new Error(
                          this.$t("validator.template.exist", [
                            this.$t("Media.editor.Name"),
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
        LotNumber: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Media.editor.LotNumber"),
              ]),
            trigger: "blur",
          },
        ],
        LotNumberExpDate: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Media.editor.LotNumberExpDate"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    getLableDescription,
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          const endDate = new Date();
          var setDate = Date.parse(this.modelInner.LotNumberExpDate);
          endDate.setTime(setDate + 3600 * 1000 * 24 - 1000);
          // var date = Date.parse(that.modelInner.LotNumberExpDate);
          this.modelInner.LotNumberExpDate = endDate.toISOString();
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
