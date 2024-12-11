<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Site.entity'
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
      label-width="96px"
      label-position="right"
    >
      <el-form-item :label="$t('Site.editor.Name')" prop="Name">
        <el-input
          ref="modelInnerName"
          v-model.trim="modelInner.Name"
          :placeholder="$t('Site.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Site.editor.Description')" prop="Description">
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('Site.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Site.editor.SiteType')" prop="SiteTypeId">
        <o-data-selector
          ref="siteType"
          :placeholder="`${$t('template.select', [
            $t('Site.editor.SiteType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.SiteTypeId"
          entity="SiteType"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Site.editor.Classification')"
        prop="ClassificationId"
      >
        <o-data-selector
          ref="classification"
          :placeholder="`${$t('template.select', [
            $t('Site.editor.Classification'),
          ])}`"
          :multiple="false"
          v-model="modelInner.ClassificationId"
          entity="Classification"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Site.editor.SecondClassification')"
        prop="SecondClassificationId"
      >
        <o-data-selector
          ref="secondClassification"
          :placeholder="`${$t('template.select', [
            $t('Site.editor.SecondClassification'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="modelInner.SecondClassificationId"
          entity="Classification"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('Site.editor.Barcode')" prop="Barcode">
        <el-input
          ref="modelInner.Barcode"
          v-model="modelInner.Barcode"
          :placeholder="$t('Site.editor.Barcode')"
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
  name: "SiteEditor",
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
  },
  emits: ["update:visible", "update:model", "accept"],
  inject: ["existSameName"],
  setup(props, ctx) {
    const editForm = ref<typeof ElForm>();
    const modelInnerName = ref<typeof ElInput>();
    const siteType = ref<typeof ODataSelector>();
    const classification = ref<typeof ODataSelector>();
    const secondClassification = ref<typeof ODataSelector>();
    const meta: Meta = {
      TypeName: "",
      Icon: "",
    };
    const modelInner = ref({
      Id: undefined,
      Name: undefined,
      Description: undefined,
      SiteTypeId: undefined,
      ClassificationId: undefined,
      SecondClassificationId: undefined,
      Barcode: undefined,
      LocationId: undefined,
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
              i18n.global.t("Site.editor.Name"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (value) {
              if (existSameName!(value, modelInner.value.Id, 2)) {
                // type 2 采样点
                callback(
                  new Error(
                    i18n.global.t("validator.template.exist", [
                      i18n.global.t("Site.editor.Name"),
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
      SiteTypeId: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Site.editor.SiteType"),
            ]),
          trigger: "blur",
        },
      ],
      ClassificationId: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Site.editor.Classification"),
            ]),
          trigger: "blur",
        },
      ],
    };
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    return {
      visibleInner,
      siteType,
      classification,
      secondClassification,
      modelInner,
      editForm,
      modelInnerName,
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
          let selectedType = this.siteType?.selectedItem;
          if (selectedType) {
            if (!this.modelInner.Meta) {
              this.modelInner.Meta = {
                TypeName: "",
                Icon: "",
              };
            }
            this.modelInner.Meta.TypeName = selectedType.Name;
            this.modelInner.Meta.Icon = selectedType.Icon;
            this.modelInner.NodeType = 2;
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
        this.siteType?.loadData();
        this.classification?.loadData();
        this.secondClassification?.loadData();
      });
    },
  },
});
</script>
