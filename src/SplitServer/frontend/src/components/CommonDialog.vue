<template>
  <el-dialog
    ref="dialogRef"
    :title="title"
    :width="width"
    v-model="dialogVisible"
    @opened="tryFocusFirstInput"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="onBtnClick(false)">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" @click="onBtnClick(true)">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="formRef"
      :rules="rules"
      :label-width="labelWidth"
      :label-position="labelPosition"
      :model="model"
    >
      <slot></slot>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import { ElDialog, ElForm } from "element-plus";
import * as vue from "vue";

export default vue.defineComponent({
  name: "CommonDialog",
  inheritAttrs: false,
  props: { ...ElForm.props, ...ElDialog.props },
  setup(props, ctx) {
    const dialogVisible = vue.ref(false);
    let resolveKeeper: (val: boolean) => void;
    const dialogRef = vue.ref<typeof ElDialog>();
    const formRef = vue.ref<typeof ElForm>();
    function tryFocusFirstInput() {
      if (dialogRef.value) {
        const div = dialogRef.value.dialogContentRef.$el as HTMLDivElement;
        if (div) {
          const input = div.querySelector("input") as HTMLInputElement;
          if (input) {
            input.focus();
          }
        }
      }
    }
    function showDialog() {
      return new Promise((resolve) => {
        resolveKeeper = resolve;
        dialogVisible.value = true;
      });
    }

    function validateSafe() {
      return new Promise((resolve) => {
        formRef.value
          ?.validate()
          .then(() => resolve(true))
          .catch(() => resolve(false));
      });
    }

    async function onBtnClick(result: boolean) {
      if (!result || (await validateSafe())) {
        dialogVisible.value = false;
        resolveKeeper(result);
      }
    }
    vue.watch(dialogVisible, (val) => {
      if (val) {
        formRef.value?.resetFields();
        formRef.value?.clearValidate();
      }
    });
    return {
      dialogVisible,
      formRef,
      showDialog,
      onBtnClick,
      tryFocusFirstInput,
    };
  },
});
</script>
