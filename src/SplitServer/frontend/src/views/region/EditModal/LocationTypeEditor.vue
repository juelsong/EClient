<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'LocationType.entity'
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
      label-width="54px"
      label-position="right"
    >
      <el-form-item :label="$t('LocationType.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model.trim="modelInner.Name"
          :placeholder="$t('LocationType.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('LocationType.editor.Description')"
        prop="Description"
      >
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('LocationType.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('LocationType.editor.Weight')" prop="Weight">
        <location-weight-selector v-model="modelInner.Weight" />
      </el-form-item>
      <el-form-item :label="$t('LocationType.editor.Icon')" prop="Icon">
        <icon-selector
          v-model="modelInner.Icon"
          @SelectionChanged="handleIconSelected"
        />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import IconSelector from "@/components/IconSelector.vue";
import LocationWeightSelector from "./LocationWeightSelector.vue";
export default defineComponent({
  components: { LocationWeightSelector, IconSelector },
  name: "LocationTypeEditor",
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
    return { visibleInner };
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
      });
    },
  },
  data() {
    return {
      modelInner: {},
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("LocationType.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "LocationType",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("LocationType.editor.Name"),
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
        Description: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("LocationType.editor.Description"),
              ]),
            trigger: "blur",
          },
        ],
        Weight: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("LocationType.editor.Weight"),
              ]),
            trigger: "blur",
          },
          {
            type: "number",
            trigger: "blur",
            message: () =>
              this.$t("validator.template.typeWrong", [
                this.$t("LocationType.editor.Weight"),
                this.$t("LocationType.validator.Weight.Type"),
              ]),
          },
        ],
        Icon: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("LocationType.editor.Icon"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    convertLabel(data) {
      return this.$t("LocationType.WeightFormat", [data.Level]);
    },
    handleIconSelected() {
      this.$refs.editForm.validateField("Icon");
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
