
<template>
  <el-dialog v-model="visibleInner"
             :close-on-click-modal="false"
             :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'MapCategory.entity'
    )}`"
             width="400px">
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary"
                   @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form ref="editForm"
             :model="modelInner"
             :rules="rules"
             label-width="82px"
             label-position="right">
      <el-form-item :label="$t('MapCategory.editor.Name')"
                    prop="Name">
        <el-input ref="modelInner.Name"
                  v-model="modelInner.Name"
                  :placeholder="$t('MapCategory.editor.Name')"></el-input>
      </el-form-item>
      <el-form-item :label="$t('MapCategory.editor.Description')"
                    prop="Description">
        <el-input ref="modelInner.Description"
                  v-model="modelInner.Description"
                  :placeholder="$t('MapCategory.editor.Description')"></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
export default defineComponent({
  name: "MapCategoryEditor",
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
                this.$t("MapCategory.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "MapCategory",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("MapCategory.editor.Name"),
                        ])
                      )
                    );
                  } else {
                    callback();
                  }
                });
              }
            },
            trigger: "blur",
          },
        ],
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
