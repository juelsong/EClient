<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Equipment.entity'
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
      label-width="110px"
      label-position="right"
    >
      <el-form-item label="设备Id" prop="DeviceId">
        <el-input
          ref="modelInner.DeviceId"
          v-model="modelInner.DeviceId"
          :maxlength="15"
          placeholder="设备Id"
        ></el-input>
      </el-form-item>
      <el-form-item label="设备名称" prop="DeviceName">
        <el-input
          ref="modelInner.DeviceName"
          v-model="modelInner.DeviceName"
          placeholder="设备名称"
        ></el-input>
      </el-form-item>
      
      <!-- <el-form-item label="粉尘仪型号" prop="EquipmentType">
        <o-data-selector
          ref="EquipmentType"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.EquipmentType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.EquipmentTypeId"
          entity="EquipmentType"
          label="Description"
          value="Id"
        />
      </el-form-item> -->
      <el-form-item label="MN码" prop="MnCode">
        <el-input
          ref="modelInner.MnCode"
          v-model="modelInner.MnCode"
          :maxlength="15"
          placeholder="MN码"
        ></el-input>
      </el-form-item>
      <el-form-item label="建委MN码" prop="JwMnCode">
        <el-input
          ref="modelInner.JwMnCode"
          v-model="modelInner.JwMnCode"
          :maxlength="15"
          placeholder="建委MN码"
        ></el-input>
      </el-form-item>
      <el-form-item label="锁Id" prop="LockId">
        <el-input
          ref="modelInner.LockId"
          v-model="modelInner.LockId"
          :maxlength="15"
          placeholder="锁Id"
        ></el-input>
      </el-form-item>
      
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed, ref } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
//import RegionTree from "@/components/RegionTree.vue";
import { saveAs } from "file-saver";

export default defineComponent({
  name: "EquipmentEditor",
  // components: { ODataSelector },
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
    equipmentTypeId: {
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
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
        if (this.modelInner.EquipmentTypeId == undefined) {
          if (this.equipmentTypeId) {
            this.modelInner.EquipmentTypeId = this.equipmentTypeId;
          }
        }
        this.modelInner.IsOperation = true;
      }
      this.$nextTick(() => {
        if (newVal) {
          // this.$refs.regionTreeRef.loadData();
        }
        this.$refs.editForm.clearValidate();
        this.$refs["EquipmentType"].loadData();
        this.$refs["modelInner.Name"].focus();
      });
    },
  },
  computed: {},
  data() {
    return {
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Equipment.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "Equipment",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("Equipment.editor.Name"),
                        ])
                      )
                    );
                  } else {
                    callback();
                  }
                });
              }
            },
            trigger: "blur",
          },
        ],
        CalibrationValue: [
          {
            pattern: /^[-,+]?\d+.?\d*$/,
            message: () =>
              this.$t("validator.template.isNumber", [
                this.$t("Equipment.editor.CalibrationValue"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    handleFrequencyChange(val) {
      if (val) {
        if (val > 999999999.999999) {
          this.modelInner.CalibrationValue = 99999999999999.999999;
        }
      }
    },
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
