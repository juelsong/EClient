
<template>
  <el-dialog v-model="visibleInner"
             :close-on-click-modal="false"
             :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Department.entity'
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
             label-width="54px"
             label-position="right">
      <el-form-item :label="$t('Department.editor.Name')"
                    prop="Name">
        <el-input ref="modelInner.Name"
                  v-model="modelInner.Name"
                  :placeholder="$t('Department.editor.Name')"></el-input>
      </el-form-item>
      <el-form-item :label="$t('Department.editor.Description')"
                    prop="Description">
        <el-input ref="modelInner.Description"
                  v-model="modelInner.Description"
                  :placeholder="$t('Department.editor.Description')"></el-input>
      </el-form-item>
      <el-form-item :label="$t('Department.editor.Code')"
                    prop="Code">
        <el-input ref="modelInner.Code"
                  v-model="modelInner.Code"
                  :placeholder="$t('Department.editor.Code')"></el-input>
      </el-form-item>
      <el-form-item :label="$t('Department.editor.Manager')"
                    prop="Manager">
        <o-data-selector :placeholder="`${$t('template.select', [
            $t('Department.editor.Manager'),
          ])}`"
                         :multiple="false"
                         :clearable="true"
                         v-model="modelInner.ManagerId"
                         entity="User"
                         label="RealName"
                         value="Id" />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "DepartmentEditor",
  components: { ODataSelector },
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
        Name: [{
          required: true,
          message: () =>
            this.$t("validator.template.required", [
              this.$t("Department.editor.Name"),
            ]),
          trigger: "blur"
        }, {
          validator: (rule, value, callback) => {
            if (value) {
              this.$exist(
                "Department",
                "Name",
                value,
                this.modelInner.Id
              ).then((exist) => {
                if (exist) {
                  callback(
                    new Error(
                      this.$t("validator.template.exist", [
                        this.$t("Department.editor.Name"),
                      ])
                    )
                  );
                } else {
                  callback();
                }
              });
            }
          },
          trigger: "blur"
        }],
        Description: [{
          required: true,
          message: () =>
            this.$t("validator.template.required", [
              this.$t("Department.editor.Description"),
            ]),
          trigger: "blur"
        }]
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
