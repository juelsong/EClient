<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="title"
    width="600px"
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
      ref="editFormRef"
      :model="reasonModel"
      :rules="rules"
      label-width="110px"
      label-position="right"
    >
      <el-form-item :label="$t('Audit.editor.DefaultNote')" prop="DefaultNote">
        <o-data-selector
          ref="noteSelectorRef"
          :multiple="false"
          :clearable="true"
          :autoLoad="false"
          v-model="reasonModel.DefaultNote"
          filter="IsActive eq true"
          entity="PreDefinedNote"
          label="Name"
          value="Message"
        />
      </el-form-item>
      <el-form-item :label="propertyName" prop="Reason">
        <el-input
          v-model="reasonModel.Reason"
          :rows="3"
          type="textarea"
          :placeholder="propertyName"
        />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import * as vue from "vue";
import { ElForm, FormRules } from "element-plus";
import { i18n } from "@/i18n";
import ODataSelector from "@/components/ODataSelector.vue";
declare type ReasonModel = {
  Reason: string;
  DefaultNote: string;
};
export default vue.defineComponent({
  name: "Review",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    title: {
      type: String,
      default: "",
    },
    propertyName: {
      type: String,
      default: "",
    },
  },
  emits: ["update:visible", "accept"],
  components: { ODataSelector },
  setup(props, ctx) {
    const visibleInner = vue.computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const reasonModel = vue.ref<ReasonModel>({ Reason: "", DefaultNote: "" });
    const editFormRef = vue.ref<typeof ElForm>();
    const noteSelectorRef = vue.ref<typeof ODataSelector>();
    const rules: FormRules = {
      Reason: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("Audit.prompt.ReasonRequired", [
                props.propertyName,
              ]),
            ]),
          trigger: "blur",
        },
      ],
    };
    vue.watch(visibleInner, (val) => {
      if (val) {
        reasonModel.value = {
          Reason: "",
          DefaultNote: "",
        };
        vue.nextTick(() => {
          noteSelectorRef.value?.loadData();
        });
      } else {
        editFormRef.value?.clearValidate();
      }
    });

    vue.watch(
      () => reasonModel.value.DefaultNote,
      (val) => {
        if (val) {
          reasonModel.value.Reason = val;
        }
      }
    );

    async function onAcceptClick() {
      const valid = await editFormRef.value?.validate();
      if (valid) {
        visibleInner.value = false;
        ctx.emit("accept", reasonModel.value.Reason);
      }
    }
    return {
      visibleInner,
      editFormRef,
      noteSelectorRef,
      rules,
      reasonModel,
      onAcceptClick,
    };
  },
});
</script>
