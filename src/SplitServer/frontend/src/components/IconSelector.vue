<template>
  <div class="ui-fas" @click="popoverShowFun(false)">
    <el-popover
      :placement="state.myPlacement"
      :disabled="disabled"
      popper-class="el-icon-popper"
      :width="state.popoverWidth"
      v-model:visible="state.visible"
      show-arrow
      trigger="manual"
    >
      <template #reference>
        <el-input
          v-model="selected"
          :placeholder="$t('placeholder.select', [$t('placeholder.icon')])"
          ref="input"
          type="primary"
          link
          :style="styles"
          :clearable="clearable"
          :disabled="disabled"
          :readonly="readonly"
          :size="size"
        >
          <template #prefix>
            <el-icon
              :size="24"
              style="margin: auto"
              v-if="selected && selected.length > 0"
            >
              <svg-icon :icon-class="selected" />
            </el-icon>
          </template>
        </el-input>
      </template>
      <el-scrollbar max-height="400px" ref="eScrollbar">
        <ul class="icon-list">
          <li
            v-for="(item, index) in svgIcons"
            :key="index"
            :ref="item.name"
            :class="item.selected ? 'icon-item selected' : 'icon-item'"
            @click="handleIconSelected(item)"
          >
            <span class="svg-icon">
              <el-icon :size="24">
                <svg-icon :icon-class="item.name" />
              </el-icon>
            </span>
          </li>
        </ul>
      </el-scrollbar>
    </el-popover>
  </div>
</template>

<script>
import {
  defineComponent,
  computed,
  reactive,
  watch,
  nextTick,
  onMounted,
  ref,
} from "vue";
import icons from "@/icons";
import SvgIcon from "@/components/SvgIcon";

const on = (function () {
  if (document.addEventListener) {
    return function (element, event, handler) {
      if (element && event && handler) {
        element.addEventListener(event, handler, false);
      }
    };
  } else {
    return function (element, event, handler) {
      if (element && event && handler) {
        element.attachEvent("on" + event, handler);
      }
    };
  }
})();
const off = (function () {
  if (document.removeEventListener) {
    return function (element, event, handler) {
      if (element && event) {
        element.removeEventListener(event, handler, false);
      }
    };
  } else {
    return function (element, event, handler) {
      if (element && event) {
        element.detachEvent("on" + event, handler);
      }
    };
  }
})();

export default defineComponent({
  name: "IconSelector",
  components: { SvgIcon },

  props: {
    // 是否禁用文本框
    disabled: {
      type: Boolean,
      // false
      default: false,
    },
    readonly: {
      type: Boolean,
      // false
      default: false,
    },
    clearable: {
      type: Boolean,
      // true
      default: true,
    },
    // e-icon-picker 样式
    styles: {
      type: Object,
      default() {
        return {};
      },
    },
    // 弹出框位置
    placement: {
      type: String,
      //  bottom
      default: "bottom",
    },
    modelValue: {
      type: String,
      default: undefined,
    },
    options: {
      type: Object,
    },
    width: {
      type: Number,
      default: -1,
    },
    size: {
      type: String,
      default: "default",
    },
    emptyText: {
      type: String,
      default() {
        return "暂无可选图标";
      },
    },
    highLightColor: {
      type: String,
      default() {
        return "";
      },
    },
  },
  emits: ["update:modelValue", "SelectionChanged"],
  setup(props, ctx) {
    const state = reactive({
      visible: false, // popover v-model
      myPlacement: "bottom",
      popoverWidth: 200,
      destroy: false,
    });

    let input = ref(null);
    let eScrollbar = ref(null);
    const selected = computed({
      get: () => props.modelValue,
      set: (newVal) => {
        ctx.emit("update:modelValue", newVal);
        ctx.emit("SelectionChanged");
      },
    });
    watch(
      () => state.visible,
      (newValue) => {
        if (newValue === false) {
          nextTick(() => {
            off(document, "mouseup", popoverHideFun);
          });
        } else {
          nextTick(() => {
            on(document, "mouseup", popoverHideFun);
          });
        }
      },
      { deep: true }
    );
    watch(
      () => props.modelValue,
      (newValue) => {
        if (newValue && newValue.length > 0) {
          svgIcons.forEach((item) => {
            item.selected = item.name == newValue;
          });
        }
      }
    );
    //绑定时检查宽度
    onMounted(() => {
      updateW();
    });
    // 更新宽度
    const updateW = () => {
      nextTick(() => {
        if (props.width === -1) {
          state.popoverWidth =
            input.value.$el.getBoundingClientRect().width - 36;
        } else {
          state.popoverWidth = props.width;
        }
        if (eScrollbar && eScrollbar.value && eScrollbar.value.setScrollTop) {
          eScrollbar.value.setScrollTop(0);
          eScrollbar.value.update();
        }
      });
    };
    // 显示弹出框的时候容错，查看是否和el宽度一致
    const popoverShowFun = () => {
      if (props.readonly !== true && props.disabled !== true) {
        state.visible = true;
        updateW();
      }
    };
    // 点击控件外，判断是否隐藏弹出框
    const popoverHideFun = (e) => {
      let path = e.path || (e.composedPath && e.composedPath());
      let isInter = path.some(
        (list) =>
          list.className && list.className.toString().indexOf("is-empty") !== -1
      );
      if (!isInter) {
        setTimeout(() => {
          state.visible = false;
        }, 10);
      }
    };
    const svgIcons = [];
    for (let prop in icons.elementIcons) {
      svgIcons.push({
        name: prop,
        icon: icons.elementIcons[prop],
      });
    }
    return {
      state,
      svgIcons,
      popoverShowFun,
      input,
      eScrollbar,
      selected,
    };
  },
  methods: {
    handleIconSelected(item) {
      this.state.visible = false;
      this.selected = item.name;
    },
  },
});
</script>

<style scoped>
.icon-list {
  list-style: none;
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  padding: 0 !important;
}
.icon-item {
  color: var(--el-text-color-regular);
  text-align: center;
  height: 48px;
}
.icon-item.selected {
  border: 2px solid var(--el-color-primary-light-6);
  border-radius: 4px;
  /* border-color: var(--el-color-primary-light-9); */
}
.icon-item:hover {
  background-color: #f2f6fc;
}
.svg-icon {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  cursor: pointer;
}
</style>
