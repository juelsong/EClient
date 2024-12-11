<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'User.entity'
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
      ref="editFormRef"
      :model="modelInner"
      :rules="rules"
      label-width="82px"
      label-position="right"
    >
      <el-form-item :label="$t('User.editor.Account')" prop="Account">
        <el-input
          ref="accountRef"
          :disabled="$store.getters.isESysSecurity != true"
          v-model="modelInner.Account"
          :placeholder="$t('User.editor.Account')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.RealName')" prop="RealName">
        <el-input
          ref="modelInner.RealName"
          :disabled="$store.getters.isESysSecurity != true"
          v-model="modelInner.RealName"
          :placeholder="$t('User.editor.RealName')"
        ></el-input>
      </el-form-item>
      <el-form-item
        v-if="createNew"
        :label="$t('User.editor.Password')"
        prop="Password"
      >
        <el-input
          show-password
          ref="modelInner.Password"
          v-model="modelInner.Password"
          :placeholder="$t('User.editor.Password')"
        ></el-input>
      </el-form-item>
      <el-form-item
        v-if="createNew"
        :label="$t('User.editor.Password2')"
        prop="Password2"
      >
        <el-input
          show-password
          ref="modelInner.Password2"
          v-model="modelInner.Password2"
          :placeholder="$t('User.editor.Password2')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.EmployeeId')" prop="EmployeeId">
        <el-input
          ref="modelInner.EmployeeId"
          v-model="modelInner.EmployeeId"
          :placeholder="$t('User.editor.EmployeeId')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Department')" prop="DepartmentId">
        <o-data-selector
          ref="departmentRef"
          :placeholder="`${$t('template.select', [
            $t('User.editor.Department'),
          ])}`"
          :multiple="false"
          filter="IsActive eq true"
          v-model="modelInner.DepartmentId"
          :autoLoad="false"
          entity="Department"
          label="Name"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('User.editor.EMail')" prop="EMail">
        <el-input
          ref="modelInner.EMail"
          v-model="modelInner.EMail"
          :placeholder="$t('User.editor.EMail')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Phone')" prop="Phone">
        <el-input
          ref="modelInner.Phone"
          v-model="modelInner.Phone"
          :placeholder="$t('User.editor.Phone')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Title')" prop="Title">
        <el-input
          ref="modelInner.Title"
          v-model="modelInner.Title"
          :placeholder="$t('User.editor.Title')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Location')" prop="Location">
        <region-tree ref="regionRef" v-model="modelInner.LocationId" />
      </el-form-item>
      <el-form-item :label="$t('User.editor.Roles')" prop="Roles">
        <o-data-selector
          ref="rolesRef"
          :placeholder="`${$t('template.include', [$t('User.editor.Roles')])}`"
          :multiple="true"
          :autoLoad="false"
          v-model="modelInner.Roles"
          filter="IsActive eq true"
          entity="Role"
          label="Name"
          value="Id"
        />
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import * as vue from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
import RegionTree from "@/components/RegionTree.vue";
import { queryPasswordRule } from "@/api/security_gen";
import store from "@/store";
import { FormItemRule } from "@/defs/Types";
import { ElForm, ElInput } from "element-plus";
import { i18n } from "@/i18n";
import { oDataExist } from "@/utils/odata";

export default vue.defineComponent({
  name: "UserEditor",
  components: { ODataSelector, RegionTree },
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
    const visibleInner = vue.computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const modelInner = vue.ref<Record<string, any>>({});
    const rolesRef = vue.ref<typeof ODataSelector>();
    const departmentRef = vue.ref<typeof ODataSelector>();
    const regionRef = vue.ref<typeof ODataSelector>();
    const editFormRef = vue.ref<typeof ElForm>();
    const accountRef = vue.ref<typeof ElInput>();
    vue.watch(visibleInner, (newVal) => {
      if (newVal) {
        let copyQuery = cloneDeep(vue.toRaw(props.model));
        modelInner.value = copyQuery;
        queryPasswordRule(store.getters.locale).then((result) => {
          rules.value.Password.splice(2);
          rules.value.Password2.splice(2);
          if (result.success && result.data) {
            const additionalRules = result.data.map((rule) => {
              const formRule: FormItemRule = {
                pattern: rule.RegexString,
                message: rule.Prompt,
                trigger: "blur",
              };
              return formRule;
            });
            rules.value.Password.push(...additionalRules);
            rules.value.Password2.push(...additionalRules);
          }
        });
      }
      vue.nextTick(() => {
        if (newVal) {
          rolesRef.value?.loadData();
          departmentRef.value?.loadData();
          regionRef.value?.loadData();
        }
        editFormRef.value?.clearValidate();
        accountRef.value?.focus();
      });
    });
    const ruleData: Record<string, FormItemRule[]> = {
      Account: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Account"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (value) {
              oDataExist("User", "Account", value, modelInner.value.Id).then(
                (exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        i18n.global.t("validator.template.exist", [
                          i18n.global.t("User.editor.Account"),
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
      RealName: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.RealName"),
            ]),
          trigger: "blur",
        },
      ],
      // EmployeeId: [
      //   {
      //     required: true,
      //     message: () =>
      //       i18n.global.t("validator.template.required", [
      //         i18n.global.t("User.editor.EmployeeId"),
      //       ]),
      //     trigger: "blur",
      //   },
      // ],
      DepartmentId: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Department"),
            ]),
          trigger: "blur",
        },
      ],
      Password: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Password"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (!value || value === "") {
              callback(
                new Error(
                  i18n.global.t("validator.template.required", [
                    i18n.global.t("User.editor.Password"),
                  ])
                )
              );
            } else {
              if (modelInner.value.Password2 !== "") {
                editFormRef.value?.validateField("Password2");
              }
              callback();
            }
          },
          trigger: "blur",
        },
      ],
      Password2: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.Password"),
            ]),
          trigger: "blur",
        },
        {
          validator: (rule, value, callback) => {
            if (!value || value === "") {
              callback(
                new Error(
                  i18n.global.t("validator.template.required", [
                    i18n.global.t("User.editor.Password"),
                  ])
                )
              );
            } else if (value !== modelInner.value.Password) {
              callback(
                new Error(i18n.global.t("User.validator.PasswordNotSame"))
              );
            } else {
              callback();
            }
          },
          trigger: "blur",
        },
      ],
      EMail: [
        {
          type: "email",
          message: i18n.global.t("validator.template.email"),
        },
      ],
    };
    const rules = vue.ref(ruleData);
    async function onAcceptClick() {
      const valid = await editFormRef.value?.validate();
      if (valid) {
        ctx.emit("update:model", modelInner.value);
        visibleInner.value = false;
        ctx.emit("accept");
      }
    }

    return {
      regionRef,
      rolesRef,
      departmentRef,
      accountRef,
      editFormRef,
      visibleInner,
      modelInner,
      rules,
      onAcceptClick,
    };
  },
});
</script>
