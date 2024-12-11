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
      <el-form-item
        :label="$t('Equipment.editor.EquipmentType')"
        prop="EquipmentType"
      >
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
      <el-form-item :label="$t('Equipment.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Equipment.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.Description')"
        prop="Description"
      >
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('Equipment.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.Barcode')" prop="Barcode">
        <el-input
          ref="modelInner.Barcode"
          v-model="modelInner.Barcode"
          :placeholder="$t('Equipment.editor.Barcode')"
        ></el-input>
      </el-form-item>
      <!-- <el-form-item
        :label="$t('Equipment.editor.UnitOfMeasure')"
        prop="UnitOfMeasure"
      >
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.UnitOfMeasure'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="modelInner.UnitOfMeasureId"
          entity="UnitOfMeasure"
          label="Description"
          value="Id"
        />
      </el-form-item> -->
      <el-form-item :label="$t('Equipment.editor.Location')" prop="Location">
        <region-tree ref="regionTreeRef" v-model="modelInner.LocationId" />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.CalibrationDate')"
        prop="CalibrationDate"
      >
        <el-date-picker
          ref="calibrationDate"
          v-model="modelInner.CalibrationDate"
          type="date"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.CalibrationDate'),
          ])}`"
          @change="changeCalibrationDate"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.NextCalibrationDate')"
        prop="NextCalibrationDate"
      >
        <el-date-picker
          ref="nextCalibrationDate"
          v-model="modelInner.NextCalibrationDate"
          type="date"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.NextCalibrationDate'),
          ])}`"
          :disabled-date="disabledNextCalibrationDate"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.CalibrationValue')"
        prop="CalibrationValue"
      >
        <el-input-number
          ref="modelInner.CalibrationValue"
          style="width: 350px"
          v-model="modelInner.CalibrationValue"
          :max="999999999.999999"
          :step="0.000001"
          :placeholder="$t('Equipment.editor.CalibrationValue')"
        ></el-input-number>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.ControlNumber')"
        prop="ControlNumber"
      >
        <el-input
          ref="modelInner.ControlNumber"
          v-model="modelInner.ControlNumber"
          :maxlength="15"
          :placeholder="$t('Equipment.editor.ControlNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.SerialNumber')"
        prop="SerialNumber"
      >
        <el-input
          ref="modelInner.SerialNumber"
          v-model="modelInner.SerialNumber"
          :maxlength="15"
          :placeholder="$t('Equipment.editor.SerialNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.ConfigDocument')">
        <el-row style="width: 100%">
          <el-col :span="8">
            <span>
              {{
                `${
                  modelInner.DeviceConfig == undefined
                    ? $t("Equipment.editor.configEmpty")
                    : $t("Equipment.editor.configHasData")
                }`
              }}
            </span>
          </el-col>
          <el-col :span="7">
            <el-upload
              ref="upload"
              :auto-upload="false"
              action=""
              :on-change="openFile"
              :multiple="false"
              :show-file-list="false"
              v-has="'Equipment:UpdateConfig'"
            >
              <el-button type="primary">
                {{ $t("Equipment.editor.UpLoad") }}
              </el-button>
            </el-upload>
          </el-col>
          <el-col :span="2" />

          <el-col :span="7">
            <el-button
              type="primary"
              @click="downLoadXml"
              v-has="'Equipment:UpdateConfig'"
            >
              {{ $t("Equipment.editor.DownLoad") }}
            </el-button>
          </el-col>
        </el-row>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed, ref } from "vue";
import cloneDeep from "lodash.clonedeep";
// import ODataSelector from "@/components/ODataSelector.vue";
// import RegionTree from "@/components/RegionTree.vue";
import { saveAs } from "file-saver";

export default defineComponent({
  name: "EquipmentEditor",
  // components: { ODataSelector, RegionTree },
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
          this.$refs.regionTreeRef.loadData();
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
      var data = this.modelInner.DeviceConfig.OfficialConfig;
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
          // if (that.modelInner.DeviceConfig) {
          //   that.modelInner.DeviceConfig.OfficialConfig = reader.result;
          //   that.modelInner.DeviceConfig.UpLoad = true;
          // } else {
          //   that.modelInner.DeviceConfig = {
          //     OfficialConfig: reader.result,
          //   };
          // }
          let config = {};
          let configIsNew =
            that.createNew || that.modelInner.DeviceConfig == null;
          let configApi = that.$insert;

          if (configIsNew) {
            config.Version = 1;
          } else {
            config.Version = 1 + that.modelInner.DeviceConfig.Version;
          }
          config.OfficialConfig = reader.result;
          configApi("DeviceConfig", config)
            .then((t) => {
              console.log(t);
              that.modelInner.DeviceConfigId = t.Id;
              that.modelInner.DeviceConfig = {
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
