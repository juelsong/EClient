<template>
  <el-select
    :multiple="multiple"
    :placeholder="placeholder"
    :disabled="disabled"
    :filterable="filterable"
    v-model="selected"
    :clearable="clearable"
  >
    <el-option
      v-for="item in options"
      :key="item[value]"
      :value="item[value]"
      :label="item[label]"
    >
      <span style="float: left">{{ item[label] }}</span>
      <span
        v-show="labelDescription"
        style="
          float: right;
          color: var(--el-text-color-secondary);
          font-size: 13px;
        "
      >
        {{ labelLinkDescription }}{{ item[labelDescription] }}
      </span>
    </el-option>
  </el-select>
</template>

<script>
import { ActivedEntities } from "@/defs/Entity";
import { defineComponent, computed } from "vue";

export default defineComponent({
  name: "o-select",
  emits: ["change", "update:modelValue"],
  setup(props, ctx) {
    const selected = computed({
      get: () => props.modelValue ?? undefined,
      set: (newVal) =>
        ctx.emit("update:modelValue", newVal.length === 0 ? null : newVal),
    });
    return { selected };
  },
  computed: {
    selectedItem() {
      if (this.selected == undefined) {
        return undefined;
      } else {
        return this.options.find((o) => o[this.value] == this.selected);
      }
    },
  },
  data() {
    return {
      options: [],
    };
  },
  props: {
    label: {
      type: String,
      required: true,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    labelDescription: {
      type: String,
      required: false,
      default: "",
    },
    labelLinkDescription: {
      type: String,
      required: false,
      default: "",
    },
    value: {
      type: String,
      required: true,
    },
    defaultIndex: {
      type: Number,
      default: 0,
    },
    clearable: {
      type: Boolean,
      default: false,
    },
    entity: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
      required: false,
      default: "请选择",
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    filter: {
      type: String,
      required: false,
      default: undefined,
    },
    autoLoad: {
      type: Boolean,
      default: true,
    },
    autoSelect: {
      type: Boolean,
      default: true,
    },
    order: {
      type: String,
      required: false,
      default: undefined,
    },
    filterable: {
      type: Boolean,
      required: false,
      default: false,
    },
    modelValue: [Array, String, Number, Boolean, Object],
  },
  mounted() {
    if (this.autoLoad) {
      this.loadData();
    }
  },
  watch: {
    modelValue(newVal) {
      if (newVal) {
        this.selected = newVal;
      }
    },
    filter() {
      if (this.autoLoad) {
        this.loadData();
      }
    },
    selectedItem(newValue, oldValue) {
      if (
        (newValue != undefined &&
          oldValue != undefined &&
          newValue[this.value] != oldValue[this.value]) ||
        (oldValue == undefined && newValue != undefined) ||
        (oldValue != undefined && newValue == undefined)
      ) {
        this.$emit("change", newValue);
      }

      // if (this.selected != undefined && props.modelValue != undefined) {
      // }
    },
  },
  methods: {
    getLableDescription() {
      switch (this.$i18n.locale) {
        case "zh-cn":
          return "Zh";
        case "en":
          return "En";
        default:
          break;
      }
    },
    loadData() {
      let query = {};
      let filterArray = [];

      if (ActivedEntities.includes(this.entity)) {
        filterArray.push(`IsActive eq true`);
      }
      if (this.filter) {
        filterArray.push(this.filter);
      }
      let filterStr =
        filterArray.length > 1
          ? filterArray.map((f) => `(${f})`).join(" and ")
          : filterArray.join("");
      if (filterStr.length > 0) {
        query.$filter = filterStr;
      }
      if (this.order) {
        query.$orderby = this.order;
      }
      return this.$query(this.entity, query)
        .then((data) => {
          this.options = data.value;
          return Promise.resolve();
        })
        .then(() => {
          if (this.modelValue) {
            this.selected = this.modelValue;
          } else if (
            !this.clearable &&
            !this.multiple &&
            this.autoSelect &&
            this.options.length > 0
          ) {
            this.selected = this.options[this.defaultIndex][this.value];
          }
        });
    },
    selectFirst() {
      if (this.options.length && this.defaultIndex > -1) {
        this.selected = this.options[this.defaultIndex][this.value];
      }
    },
  },
});
</script>
