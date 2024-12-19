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
          ref="modelInner.SerialNumber"
          v-model="modelInner.SerialNumber"
          :maxlength="15"
          placeholder="设备ID"
        ></el-input>
      </el-form-item>
      <el-form-item label="设备名称" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          placeholder="设备名称"
        ></el-input>
      </el-form-item>
      <el-form-item label="所属区县" prop="Location">
        <o-data-selector
          :placeholder="`所属区县`"
          :multiple="false"
          v-model="modelInner.LocationId"
          entity="Location"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item label="粉尘仪型号" prop="EquipmentType">
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
      </el-form-item>
      <el-form-item label="粉尘仪编号" prop="ControlNumber">
        <el-input
          ref="modelInner.ControlNumber"
          v-model="modelInner.ControlNumber"
          :maxlength="15"
          placeholder="粉尘仪编号"
        ></el-input>
      </el-form-item>
      <el-form-item label="是否运维打卡" prop="IsOperation">
        <el-switch
          v-model="modelInner.IsOperation"
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
  name: "EquipmentEditor",
  components: { ODataSelector },
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
    downLoadXml() {
      var data = this.modelInner.EquipmentConfig.OfficialConfig;
      let str = new Blob([data], { type: "xml/plain;charset=utf-8" });
      saveAs(str, `config.xml`);
    },
    openFile(file) {
      const fileName = file.name;
      const fileType = fileName.substring(fileName.lastIndexOf("."));

      if (fileType != ".xml") {
        this.$refs.upload.clearFiles();
        this.$message.error(this.$t("Only upload xml Document!"));
        return;
      }
      var reader = new FileReader();

      reader.onload = async () => {
        let that = this;
        if (reader.result) {
          //打印文件内容
          console.log(reader.result);
          // if (that.modelInner.EquipmentConfig) {
          //   that.modelInner.EquipmentConfig.OfficialConfig = reader.result;
          //   that.modelInner.EquipmentConfig.UpLoad = true;
          // } else {
          //   that.modelInner.EquipmentConfig = {
          //     OfficialConfig: reader.result,
          //   };
          // }
          let config = {};
          let configIsNew =
            that.createNew || that.modelInner.EquipmentConfig == null;
          let configApi = that.$insert;

          if (configIsNew) {
            config.Version = 1;
          } else {
            config.Version = 1 + that.modelInner.EquipmentConfig.Version;
          }
          config.OfficialConfig = reader.result;
          configApi("EquipmentConfig", config)
            .then((t) => {
              console.log(t);
              that.modelInner.EquipmentConfigId = t.Id;
              that.modelInner.EquipmentConfig = {
                UpLoad: true,
                OfficialConfig: reader.result,
              };
            })
            .catch((error) => {
              console.log(error);
            });
        }
      };
      reader.readAsText(file.raw);
    },
    changeCalibrationDate(date) {
      if (this.modelInner.NextCalibrationDate) {
        if (date.getTime() >= this.modelInner.NextCalibrationDate) {
          this.modelInner.NextCalibrationDate = undefined;
        }
      }
    },
    disabledNextCalibrationDate(date) {
      if (this.modelInner.CalibrationDate) {
        if (date.getTime() <= this.modelInner.CalibrationDate) {
          return true;
        } else {
          return false;
        }
      }
      return false;
    },
    // changeInput() {
    //   if (this.modelInner.CalibrationValue.indexOf(".") >= 0) {
    //     this.modelInner.CalibrationValue =this.modelInner.CalibrationValue.substring(
    //       0,
    //       this.modelInner.CalibrationValue.indexOf(".") + 6
    //     );
    //   }
    // },
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
