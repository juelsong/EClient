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
      <el-form-item label="设备ID" prop="SerialNumber">
        <el-input
          ref="modelInner.EquipmentId"
          v-model="modelInner.EquipmentId"
          :maxlength="15"
          placeholder="设备ID"
        ></el-input>
      </el-form-item>
      <el-form-item label="电话" prop="Phone">
        <el-input
          ref="modelInner.Phone"
          v-model="modelInner.Phone"
          placeholder="电话"
        ></el-input>
      </el-form-item>
      <el-form-item label="是否超限告警" prop="IsAlert">
        <el-switch
          v-model="modelInner.IsAlert"
          class="ml-2"
          width="55px"
          inline-prompt
          active-text="是"
          inactive-text="否"
        />
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
  name: "EquipmentNoticicationEditor",
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
        this.modelInner.IsAlert = true;
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
                this.$query("EquipmentNotification", {
                  $filter: `EquipmentId eq ${value}`,
                }).then((rsp) => {
                  if (rsp.data.value.length > 0) {
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
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.modelInner.EquipmentId = parseInt(
            this.modelInner.EquipmentId.trim()
          );
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
