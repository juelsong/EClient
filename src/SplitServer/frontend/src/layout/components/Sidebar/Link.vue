<template>
  <component :is="type" v-bind="linkProps(to)">
    <slot></slot>
  </component>
</template>
<script lang="ts">
import { isExternal as validateIsExternal } from "@/utils/validate";
import { computed, toRefs } from "vue";

export default {
  name: "SidebarLink",
};
</script>
<script setup lang="ts">
const props = defineProps({
  to: {
    type: String,
    required: true,
  },
});
const { to } = toRefs(props);
const isExternal = computed(() => {
  return validateIsExternal(to.value);
});
const type = computed(() => {
  if (isExternal.value) {
    return "a";
  }
  return "router-link";
});
const linkProps = (to: any) => {
  if (isExternal.value) {
    return {
      href: to,
      target: "_blank",
      rel: "noopener",
    };
  }
  return {
    to: to,
  };
};
</script>
