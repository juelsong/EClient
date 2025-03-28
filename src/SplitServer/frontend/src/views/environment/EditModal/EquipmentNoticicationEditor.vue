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
      <el-form-item label="设备ID" prop="EquipmentId">
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
        if (this.modelInner.EquipmentId == undefined) {
          this.modelInner.IsAlert = true;

          // if (this.equipmentTypeId) {
          //   this.modelInner.EquipmentTypeId = this.equipmentTypeId;
          // }
        }
      }
      // this.$nextTick(() => {
      //   if (newVal) {
      //     // this.$refs.regionTreeRef.loadData();
      //   }
      //   this.$refs.editForm.clearValidate();
      //   // this.$refs["EquipmentType"].loadData();
      //   // this.$refs["modelInner.Name"].focus();
      // });
    },
  },
  computed: {},
  data() {
    return {
      rules: {
        EquipmentId: [
          {
            required: true,
            message: () => this.$t("validator.template.required", "设备Id"),
            trigger: "blur",
          },
          // {
          //   validator: (rule, value, callback) => {
          //     if (value) {
          //       this.$query("EquipmentNotification", {
          //         $filter: `EquipmentId eq ${value}`,
          //       }).then((rsp) => {
          //         if (rsp.data.value.length > 0) {
          //           callback(
          //             new Error(
          //               this.$t("validator.template.exist", [
          //                 this.$t("Equipment.editor.Name"),
          //               ])
          //             )
          //           );
          //         } else {
          //           callback();
          //         }
          //       });
          //     }
          //   },
          //   trigger: "blur",
          // },
        ],
        Phone: [
          {
            required: true,
            message: () => this.$t("validator.template.required", "电话"),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (this.createNew && value && this.modelInner.EquipmentId) {
                this.$query("EquipmentNotification", {
                  $filter: `EquipmentId eq ${this.modelInner.EquipmentId} and Phone eq '${value}'`,
                }).then((rsp) => {
                  if (
                    rsp.value.length > 0 &&
                    (!this.model?.Id || this.model.Id !== rsp.value[0].Id)
                  ) {
                    callback(new Error("该设备Id和电话的组合已存在"));
                  } else {
                    callback();
                  }
                });
              } else {
                callback();
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
          // this.modelInner.EquipmentId = parseInt(
          //   this.modelInner.EquipmentId.trim()
          // );
          let equipmentId = this.modelInner.EquipmentId;
          if (typeof equipmentId === "string") {
            equipmentId = parseInt(equipmentId.trim());
            // 如果转换失败（NaN），可以设置一个默认值或进行错误处理
            if (isNaN(equipmentId)) {
              equipmentId = 0; // 或其他默认值
              // 或者抛出错误: throw new Error('无效的设备ID');
            }
          }
          this.modelInner.EquipmentId = equipmentId;
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
