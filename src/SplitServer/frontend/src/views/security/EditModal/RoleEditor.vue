<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Role.entity'
    )}`"
    width="480px"
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
      label-width="82px"
      label-position="right"
    >
      <el-form-item :label="$t('Role.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Role.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Role.editor.Description')" prop="Description">
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('Role.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Role.editor.Permissions')" prop="Permissions">
        <el-tree
          ref="permissions"
          :data="permissionOptions"
          :props="permissionProps"
          node-key="Id"
          show-checkbox
          check-strictly
          @check="onCheckChange"
        ></el-tree>
        <!-- <o-data-selector
          :placeholder="`${$t('template.include', [
            $t('Role.editor.Permissions'),
          ])}`"
          :multiple="true"
          v-model="modelInner.Permissions"
          entity="Permission"
          label="Description"
          value="Id"
        /> -->
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import { getPermissionTree } from "@/api/permission";
// import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "RoleEditor",
  // components: { ODataSelector },
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
        if (this.modelInner.Permissions) {
          this.$refs.permissions.setCheckedKeys(
            this.modelInner.Permissions,
            false
          );
        } else {
          this.$refs.permissions.setCheckedKeys([], false);
        }
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
                this.$t("Role.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist("Role", "Name", value, this.modelInner.Id).then(
                  (exist) => {
                    if (exist) {
                      callback(
                        new Error(
                          this.$t("validator.template.exist", [
                            this.$t("Role.editor.Name"),
                          ])
                        )
                      );
                    } else {
                      callback();
                    }
                  }
                );
              }
            },
            trigger: "blur",
          },
        ],
      },
      permissionOptions: [],
      permissionProps: {
        children: "Children",
        label: "Description",
      },
    };
  },
  mounted() {
    this.loadData();
  },
  methods: {
    i18t(permissions) {
      permissions.forEach((permission) => {
        if (permission.Children) this.i18t(permission.Children);
        permission.Description = this.$t("permission." + permission.Code);
      });
    },
    loadData() {
      getPermissionTree().then((permissions) => {
        this.i18t(permissions);
        this.permissionOptions = permissions;
      });
    },
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.modelInner.Permissions = this.$refs.permissions.getCheckedKeys();
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
    setNodeChildrenChecked(node, checked) {
      if (node.Children) {
        node.Children.forEach((element) => {
          this.setNodeChildrenChecked(element, checked);
        });
      }
      this.$refs.permissions.setChecked(node.Id, checked);
    },
    setNodeParentChecked(node, checked) {
      if (checked && node.Parent) {
        this.setNodeParentChecked(node.Parent, checked);
        this.$refs.permissions.setChecked(node.Parent.Id, checked);
      }
    },
    onCheckChange(node, info) {
      let checked = info.checkedKeys.includes(node.Id) ? true : false;
      this.setNodeChildrenChecked(node, checked);
      this.setNodeParentChecked(node, checked);
    },
  },
});
</script>
