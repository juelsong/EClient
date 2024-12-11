
<template>
  <el-dialog v-model="visibleInner"
             :close-on-click-modal="false"
             :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t('OrgFoundMethod.entity')}`"
             width="400px">
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">{{$t('template.cancel')}}</el-button>
        <el-button type="primary"
                   @click="onAcceptClick">{{$t('template.accept')}}</el-button>
      </span>
    </template>
    <el-form ref="editForm"
             :model="modelInner"
             :rules="rules"
             label-width="56px"
             label-position="right">
      <el-form-item :label="$t('OrgFoundMethod.editor.Name')"
                    prop="Name">
        <el-input ref="modelInner.Name"
                  v-model="modelInner.Name"
                  :placeholder="$t('OrgFoundMethod.editor.Name')" />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, unref, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
// import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "OrgFoundMethodEditor",
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
        let copyQuery = cloneDeep(unref(this.model));
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
                this.$t("OrgFoundMethod.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "OrgFoundMethod",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("OrgFoundMethod.editor.Name"),
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
