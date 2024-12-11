<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="$t('UserPassword.title')"
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
          v-model="modelInner.Account"
          disabled
          :placeholder="$t('User.editor.Account')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Password')" prop="Password">
        <el-input
          show-password
          ref="passwordRef"
          v-model="modelInner.Password"
          :placeholder="$t('User.editor.Password')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Password2')" prop="Password2">
        <el-input
          show-password
          v-model="modelInner.Password2"
          :placeholder="$t('User.editor.Password2')"
        ></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import { editPassword } from "@/api/user_gen";
import * as vue from "vue";
import { FormItemRule } from "@/defs/Types";
import { i18n } from "@/i18n";
import { ElForm, ElInput, ElMessage } from "element-plus";
import { queryPasswordRule } from "@/api/security_gen";
import store from "@/store";
import { User } from "@/defs/Entity";

export default vue.defineComponent({
  name: "UserPassword",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    id: {
      type: Number,
      required: true,
    },
    account: {
      type: String,
      required: true,
    },
  },
  emits: ["update:visible"],
  setup(props, ctx) {
    const visibleInner = vue.computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });

    const ruleData: Record<string, FormItemRule[]> = {
      OriPassword: [
        {
          required: true,
          message: () =>
            i18n.global.t("validator.template.required", [
              i18n.global.t("User.editor.OriPassword"),
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
    };
    const rules = vue.ref(ruleData);
    const modelInner = vue.ref({
      Id: 0,
      Account: "",
      Password: "",
      Password2: "",
    });
    const editFormRef = vue.ref<typeof ElForm>();
    const passwordRef = vue.ref<typeof ElInput>();
    vue.watch(visibleInner, (newVal) => {
      if (newVal) {
        modelInner.value.Id = props.id;
        modelInner.value.Account = props.account;
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
      } else {
        editFormRef.value?.resetFields();
      }
      vue.nextTick(() => {
        if (newVal) {
          passwordRef.value?.focus();
        }
      });
    });

    async function onAcceptClick() {
      if (editFormRef.value) {
        const valid = await editFormRef.value.validate();
        if (valid) {
          const data = { Id: props.id, Password: modelInner.value.Password };
          editPassword(data as User)
            .then(() => {
              ElMessage.success(i18n.global.t("prompt.success"));
              visibleInner.value = false;
            })
            .catch(() => {});
        }
      }
    }
    return {
      visibleInner,
      rules,
      modelInner,
      onAcceptClick,
      editFormRef,
      passwordRef,
    };
  },
});
</script>
