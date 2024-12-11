<script lang="ts">
import * as vue from "vue";
import { i18n } from "@/i18n";
import { ElPopover, ElScrollbar } from "element-plus";
import { onClickOutside } from "@vueuse/core";

export type PopPlacement =
  | "top"
  | "top-start"
  | "top-end"
  | "bottom"
  | "bottom-start"
  | "bottom-end"
  | "left"
  | "left-start"
  | "left-end"
  | "right"
  | "right-start"
  | "right-end";

export default vue.defineComponent({
  name: "ItemsView",
  props: {
    data: {
      type: Array,
      default: [],
    },
    maxLine: {
      type: Number,
      default: 5,
    },
    morePlacement: {
      type: String as vue.PropType<PopPlacement>,
      default: "top-start",
    },
    detailPlacement: {
      type: String as vue.PropType<PopPlacement>,
      default: "top-start",
    },
  },
  components: { ElPopover },
  setup(props, { slots }) {
    const itemsCount = vue.computed(() => {
      if (props.data) {
        return Math.min(props.maxLine, props.data.length);
      } else {
        return 0;
      }
    });
    const moreRef = vue.ref<HTMLElement>();
    const moreCount = vue.computed(() => {
      if (props.data) {
        return props.data.length - props.maxLine;
      }
      return 0;
    });
    const popMoreVisible = vue.ref(false);
    onClickOutside(moreRef, () => (popMoreVisible.value = false));
    function handleClickShowMore() {
      popMoreVisible.value = true;
    }
    function buildDetail(data: any) {
      const vnode = slots.default?.(data);
      const vDetail = slots.detail?.(data);
      return vue.h(
        ElPopover,
        {
          trigger: "hover",
          width: 260,
          placement: props.detailPlacement,
        },
        {
          reference: () => vnode,
          default: () => vDetail,
        }
      );
    }
    return {
      itemsCount,
      moreCount,
      handleClickShowMore,
      popMoreVisible,
      moreRef,
      buildDetail,
    };
  },
  render() {
    const vNodes = new Array<vue.VNode[] | vue.VNode | undefined>();

    if (this.$props.data) {
      const maxCount = Math.min(this.$props.data.length, this.itemsCount);
      for (let i = 0; i < maxCount; i++) {
        const dataItem = this.$props.data[i];
        vNodes.push(this.buildDetail(dataItem));
      }
      if (this.moreCount > 0) {
        vNodes.push(
          vue.h(
            ElPopover,
            {
              visible: this.popMoreVisible,
              placement: this.morePlacement,
              width: 200,
            },
            {
              reference: () =>
                vue.h(
                  "span",
                  {
                    style: {
                      color: "#409EFF",
                      fontSize: "10px",
                      marginTop: "2px",
                      cursor: "pointer",
                      userSelect: "none",
                    },
                    ref: "moreRef",
                    onClick: this.handleClickShowMore,
                  },
                  {
                    default: () =>
                      i18n.global.t("Calender.Label.MoreFormatter", [
                        this.moreCount,
                      ]),
                  }
                ),
              default: () =>
                vue.h(
                  ElScrollbar,
                  {
                    maxHeight: "400px",
                  },
                  {
                    default: () =>
                      this.$props.data.map((d) => this.buildDetail(d)),
                  }
                ),
            }
          )
        );
      }
    }
    return vue.h("div", vNodes);
  },
});
</script>
