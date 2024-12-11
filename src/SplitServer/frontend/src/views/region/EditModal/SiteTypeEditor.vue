<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'SiteType.entity'
    )}`"
    width="400px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="editForm"
      :model="modelInner"
      :rules="rules"
      label-width="54px"
      label-position="right"
    >
      <el-form-item :label="$t('SiteType.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model.trim="modelInner.Name"
          :placeholder="$t('SiteType.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('SiteType.editor.Icon')" prop="Icon">
        <icon-selector v-model="modelInner.Icon" />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import IconSelector from "@/components/IconSelector.vue";
import cloneDeep from "lodash.clonedeep";
export default defineComponent({
  name: "SiteTypeEditor",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Object,
    },
    createNew: {
      type: Boolean,
      default: false,
    },
  },
  components: { IconSelector },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    return { visibleInner };
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        this.$refs["modelInner.Name"].focus();
      });
    },
  },
  data() {
    return {
      modelInner: {},
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("SiteType.editor.Name"),
              ]),
            trigger: "blur",
          },
        ],
        Icon: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("SiteType.editor.Icon"),
              ]),
            trigger: "blur",
          },
        ],
      },
      optons: {
        FontAwesome: false,
        ElementUI: false,
        eIcon: true, //自带的图标，来自阿里妈妈
        eIconSymbol: true, //是否开启彩色图标
        // addIconList: icon_names,
        removeIconList: [],
        zIndex: 0, //选择器弹层的最低层,全局配置
      },
    };
  },
  methods: {
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
  },
});
</script>
