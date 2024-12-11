<template>
  <slot></slot>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "LifeCircleHook",
  props: {
    beforeUpdateCallback: {
      type: Function,
      default: undefined,
    },
    updateCallback: {
      type: Function,
      default: undefined,
    },
  },

  beforeUpdate() {
    if (this.beforeUpdateCallback) {
      this.beforeUpdateCallback();
    }
  },
  updated() {
    if (this.updateCallback) {
      this.updateCallback();
    }
  },
});
</script>
