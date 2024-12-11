<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Location.entity'
    )}`"
    width="400px"
    @open="onDialogOpen"
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
      <el-form-item :label="$t('Location.editor.Name')" 
      prop="Name">
        <el-input
          ref="modelInnerName"
          v-model.trim="modelInner.Name"
          :placeholder="$t('Location.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Location.editor.Description')"
        prop="Description"
      >
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('Location.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Location.editor.LocationType')"
        prop="LocationTypeId"
      >
        <o-data-selector
          ref="locationType"
          :placeholder="`${$t('template.select', [
            $t('Location.editor.LocationType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.LocationTypeId"
          :filter="`(Weight lt ${minLevelWeight}) and (Weight gt ${maxLevelWeight})`"
          entity="LocationType"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <!-- <el-form-item
        :label="$t('Location.editor.Classification')"
        prop="ClassificationId"
      >
        <o-data-selector
          ref="classification"
          :placeholder="`${$t('template.select', [
            $t('Location.editor.Classification'),
          ])}`"
          :multiple="false"
          v-model="modelInner.ClassificationId"
          entity="Classification"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Location.editor.SecondClassification')"
        prop="SecondClassificationId"
      >
        <o-data-selector
          ref="secondClassification"
          :placeholder="`${$t('template.select', [
            $t('Location.editor.SecondClassification'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="modelInner.SecondClassificationId"
          entity="Classification"
          label="Name"
          value="Id"
        />
      </el-form-item> -->
      <el-form-item :label="$t('Location.editor.Barcode')" prop="Barcode">
        <el-input
          ref="modelInner.Barcode"
          v-model="modelInner.Barcode"
          :placeholder="$t('Location.editor.Barcode')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Location.editor.Code')" prop="Code">
        <el-input
          ref="modelInner.Code"
          v-model="modelInner.Code"
          :placeholder="$t('Location.editor.Code')"
        ></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import { defineComponent, toRaw, computed, ref, inject } from "vue";
import cloneDeep from "lodash.clonedeep";
import { Meta } from "@/api/location";
import ODataSelector from "@/components/ODataSelector.vue";
import { ElInput, ElForm } from "element-plus";
import { i18n } from "@/i18n";

declare type ExistSameNameFunc = (
  name: string,
  id: number | undefined,
  type: number
) => boolean;

export default defineComponent({
  name: "LocationEditor",
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
    maxLevelWeight: {
      type: Number,
      required: true,
      default: 0,
    },
    minLevelWeight: {
      type: Number,
      required: true,
      default: Number.MAX_VALUE,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const editForm = ref<typeof ElForm>();
    const modelInnerName = ref<typeof ElInput>();
    const locationType = ref<typeof ODataSelector>();
    // const classification = ref<typeof ODataSelector>();
    // const secondClassification = ref<typeof ODataSelector>();
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const meta: Meta = {
      TypeName: "",
      Icon: "",
    };
    const modelInner = ref({
      Id: undefined,
      Name: undefined,
      Description: undefined,
      LocationTypeId: undefined,
      // ClassificationId: undefined,
      // SecondClassificationId: undefined,
      Barcode: undefined,
      Code: undefined,
      Meta: meta,
      NodeType: 0,
    });
    const existSameName = inject<ExistSameNameFunc>("existSameName");
    const rules = {
      Name: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Location.editor.Name"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule: any, value: string, callback: any) => {
            if (value) {
              if (existSameName!(value, modelInner.value.Id, 1)) {
                // type 1 区域
                callback(
                  new Error(
                    i18n.global.t("validator.template.exist", [
                      i18n.global.t("Location.editor.Name"),
                    ])
                  )
                );
              } else {
                callback();
              }
            }
          },
          trigger: "blur",
        },
      ],
      Description: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Location.editor.Description"),
            ]),
          trigger: "blur",
        },
      ],
      LocationTypeId: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Location.editor.LocationType"),
            ]),
          trigger: "blur",
        },
      ],
      // ClassificationId: [
      //   {
      //     required: true,
      //     message: () =>
      //       i18n.global.t("validator.template.required", [
      //         i18n.global.t("Location.editor.Classification"),
      //       ]),
      //     trigger: "blur",
      //   },
      // ],
    };
    return {
      visibleInner,
      editForm,
      locationType,
      // classification,
      // secondClassification,
      modelInnerName,
      modelInner,
      rules,
    };
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
      }
      this.$nextTick(() => {
        this.editForm?.clearValidate();
        this.modelInnerName?.focus();
      });
    },
  },
  methods: {
    onAcceptClick() {
      this.editForm?.validate((valid) => {
        if (valid) {
          let selectedType = this.locationType!.selectedItem;
          if (selectedType) {
            if (!this.modelInner.Meta) {
              this.modelInner.Meta = {
                TypeName: "",
                Icon: "",
              };
            }
            this.modelInner.Meta.TypeName = selectedType.Name;
            this.modelInner.Meta.Icon = selectedType.Icon;
            this.modelInner.NodeType = 1;
            this.modelInner.Meta.Weight = selectedType.Weight;
          }
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
    onDialogOpen() {
      this.$nextTick(() => {
        this.locationType?.loadData();
        // this.classification?.loadData();
        // this.secondClassification?.loadData();
      });
    },
  },
});
</script>
