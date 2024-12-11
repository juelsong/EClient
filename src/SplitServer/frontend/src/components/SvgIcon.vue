<template>
  <div
    v-if="isExternal"
    :style="styleExternalIcon"
    class="svg-external-icon svg-icon"
    v-on="$attrs"
  ></div>
  <svg v-else :class="svgClass" aria-hidden="true" v-on="$attrs">
    <use :xlink:href="iconName" />
  </svg>
</template>
<script lang="ts">
import { isExternal as utilityIsExternal } from "@/utils/validate";
import { computed, toRef, toRefs } from "vue";
export default {
  name: "SvgIcon",
};
</script>
<script setup lang="ts">
const props = defineProps({
  iconClass: {
    type: String,
    required: true,
  },
  className: {
    type: String,
    default: "",
  },
});
const { iconClass, className } = toRefs(props);
const isExternal = computed(() => {
  return utilityIsExternal(iconClass.value);
});
const iconName = computed(() => {
  return `#icon-${iconClass.value}`;
});
const svgClass = computed(() => {
  if (className.value) {
    return "svg-icon " + className.value;
  } else {
    return "svg-icon";
  }
});
const styleExternalIcon = computed(() => {
  return {
    mask: `url(${iconClass.value}) no-repeat 50% 50%`,
    "-webkit-mask": `url(${iconClass.value}) no-repeat 50% 50%`,
  };
});
</script>

<style scoped>
.svg-icon {
  width: 1em;
  height: 1em;
  vertical-align: -0.15em;
  fill: currentColor;
  overflow: hidden;
}

.svg-external-icon {
  background-color: currentColor;
  mask-size: cover !important;
  display: inline-block;
}
</style>
