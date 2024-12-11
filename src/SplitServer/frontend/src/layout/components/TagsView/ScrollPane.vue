<template>
  <el-scrollbar
    ref="scrollRef"
    :vertical="false"
    class="scroll-container"
    @wheel.prevent="handleScroll"
  >
    <slot></slot>
  </el-scrollbar>
</template>

<script lang="ts">
import {
  computed,
  ComputedRef,
  ref,
  onMounted,
  defineEmits,
  onBeforeUnmount,
  inject,
  watch,
  toRefs,
} from "vue";
import { ElScrollbar } from "element-plus";
import { ITag } from "@/defs/Types";

export default {
  name: "ScrollPane",
};
</script>

<script setup lang="ts">
const scrollRef = ref<typeof ElScrollbar>();
const tagAndTagSpacing = 4; // tagAndTagSpacing
const visitedViews = inject<ComputedRef<ITag[]>>("visitedViews");
const scrollWrapper = computed<HTMLElement>(() =>
  scrollRef.value == undefined ? undefined : scrollRef.value.$refs.wrap$
);
const resizeWrapper = computed<HTMLElement>(() =>
  scrollRef.value == undefined ? undefined : scrollRef.value.$refs.resize$
);
const props = defineProps({
  selectedIndex: {
    type: Number,
    default: 0,
  },
});
const { selectedIndex } = toRefs(props);
watch(selectedIndex, (idx) => {
  if (!scrollWrapper.value) {
    return;
  }
  const containerWidth = scrollWrapper.value.offsetWidth;
  if (scrollWrapper.value && resizeWrapper) {
    if (idx == 0) {
      scrollWrapper.value.scrollLeft = 0;
    } else if (
      visitedViews != undefined &&
      idx == visitedViews.value.length - 1
    ) {
      scrollWrapper.value.scrollLeft =
        scrollWrapper.value.scrollWidth - containerWidth;
    } else {
      const prevTag = resizeWrapper.value.children[idx - 1] as HTMLElement;
      const nextTag = resizeWrapper.value.children[idx + 1] as HTMLElement;

      const afterNextTagOffsetLeft =
        nextTag.offsetLeft + nextTag.offsetWidth + tagAndTagSpacing;

      // the tag's offsetLeft before of prevTag
      const beforePrevTagOffsetLeft = prevTag.offsetLeft - tagAndTagSpacing;

      if (
        afterNextTagOffsetLeft >
        scrollWrapper.value.scrollLeft + containerWidth
      ) {
        scrollWrapper.value.scrollLeft =
          afterNextTagOffsetLeft - containerWidth;
      } else if (beforePrevTagOffsetLeft < scrollWrapper.value.scrollLeft) {
        scrollWrapper.value.scrollLeft = beforePrevTagOffsetLeft;
      }
    }
  }
});
const emits = defineEmits(["scroll"]);
onMounted(() => {
  if (scrollWrapper.value) {
    scrollWrapper.value.addEventListener("scroll", emitScroll, true);
  }
});
onBeforeUnmount(() => {
  if (scrollWrapper.value) {
    scrollWrapper.value.removeEventListener("scroll", emitScroll);
  }
});
const handleScroll = (e: WheelEvent) => {
  const eventDelta = -e.deltaY * 40;
  if (scrollWrapper.value) {
    scrollWrapper.value.scrollLeft =
      scrollWrapper.value.scrollLeft + eventDelta / 4;
  }
};
const emitScroll = () => {
  emits("scroll");
};
</script>

<style lang="scss" scoped>
.scroll-container {
  white-space: nowrap;
  position: relative;
  overflow: hidden;
  width: 100%;

  :deep {
    .el-scrollbar__bar {
      bottom: 0px;
    }

    .el-scrollbar__wrap {
      height: 49px;
    }
  }
}
</style>
