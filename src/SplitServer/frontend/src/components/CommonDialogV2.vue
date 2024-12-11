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
        <el-button
          type="primary"
          v-show="showAccept"
          @click="onBtnClick(true)"
          :disabled="acceptDisabled"
        >
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <slot></slot>
  </el-dialog>
</template>

<script lang="ts">
import { ElDialog } from "element-plus";
import * as vue from "vue";
export default vue.defineComponent({
  name: "CommonDialogV2",
  inheritAttrs: false,
  props: {
    canAccept: {
      type: Function as vue.PropType<() => boolean>,
    },
    acceptDisabled: {
      type: Boolean,
      default: false,
    },
    showAccept: {
      type: Boolean,
      default: true,
    },
    ...ElDialog.props,
  },
  setup(props, ctx) {
    const dialogVisible = vue.ref(false);
    let resolveKeeper: (val: boolean) => void;
    const dialogRef = vue.ref<typeof ElDialog>();
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

    async function onBtnClick(result: boolean) {
      if (!result || !props.canAccept || props.canAccept()) {
        dialogVisible.value = false;
        resolveKeeper(result);
      }
    }

    return {
      dialogVisible,
      showDialog,
      onBtnClick,
      tryFocusFirstInput,
    };
  },
});
</script>
