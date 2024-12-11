<template>
  <el-select :multiple="false"
             :placeholder="$t('placeholder.select', [$t('LocationType.editor.Weight')])"
             v-model="selected">
    <el-option v-for="item in options"
               :key="item.Id"
               :label="$t('LocationType.WeightFormat', [item.Level])"
               :value="item.Weight"></el-option>
  </el-select>
</template>

<script>
import { defineComponent, computed } from "vue";
import { mapGetters } from "vuex";
export default defineComponent({
  name: "LocationWeightSelect",
  setup(props, ctx) {
    const selected = computed({
      get: () => props.modelValue ?? undefined,
      set: (newVal) => ctx.emit("update:modelValue", newVal),
    });
    return { selected };
  },
  computed: {
    ...mapGetters(["locationTypeLevelWeightCount"]),
  },
  data() {
    return {
      options: [],
    };
  },
  props: {
    modelValue: [Array, String, Number, Boolean, Object],
  },
  mounted() {
    this.loadData().then(() => {
      this.selected = this.modelValue == undefined ? this.options[0].Weight : this.modelValue;
    });
  },
  watch: {
    modelValue(newVal) {
      this.selected = newVal == undefined ? this.options[0].Weight : newVal;
    },
    locationTypeLevelWeightCount() {
      this.loadData();
    },
  },
  methods: {
    loadData() {
      let that = this;
      return new Promise((resolve) => {
        let cnt = parseInt(that.$store.getters.locationTypeLevelWeightCount);
        for (let idx = 1; idx <= cnt; idx++) {
          that.options.push({
            Level: idx,
            Weight: idx,
          });
        }
        resolve();
      });
    },
  },
});
</script>
