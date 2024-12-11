<template>
  <el-input v-model="filter"
            @keypress="onKeyPress">
    <template #append>
      <el-button @click="handleTypeFilter">
        <svg-icon :icon-class="showClear?'close-bold':'search'" />
      </el-button>
    </template>
  </el-input>
</template>

<script lang="ts">
import { defineComponent, ref, computed } from "vue";

export default defineComponent({
  setup() {
    const filter = ref("");
    const lastFilter = ref("");
    const showClear = computed(() => {
      return filter.value.length > 0 && filter.value == lastFilter.value;
    });
    return { filter, lastFilter, showClear };
  },
  name: "searcher",
  emits: ["search"],
  methods: {
    handleTypeFilter() {
      let needEmit = this.lastFilter != this.filter || this.showClear;
      if (this.showClear) {
        this.lastFilter = this.filter = "";
      } else {
        this.lastFilter = this.filter;
      }
      if (needEmit) {
        this.$emit("search", this.lastFilter);
      }
    },
    onKeyPress(evt: KeyboardEvent) {
      if (evt.key == "Enter") {
        this.handleTypeFilter();
      }
    },
  },
});
</script>
