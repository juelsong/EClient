<template>
  <el-dialog
    v-model="dialogVisible"
    :close-on-click-modal="false"
    :title="title"
    width="400px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button ref="btn" @click="onCancelClick">
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
      <el-form-item :label="$t('User.editor.OriPassword')" prop="OriPassword">
        <el-input
          show-password
          ref="oriPassInput"
          v-model="modelInner.OriPassword"
          :placeholder="$t('User.editor.OriPassword')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('User.editor.Password')" prop="Password">
        <el-input
          show-password
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
import store from "@/store";
import { defineComponent, ref, reactive, watch, nextTick } from "vue";
import { ElForm, ElInput, ElMessage, ElButton } from "element-plus";
import { request } from "@/utils/request";
import { BooleanReply } from "@/defs/Reply";
import { i18n } from "@/i18n";
import { queryPasswordRule } from "@/api/security_gen";
import { FormItemRule } from "@/defs/Types";

declare type resolveCallback = (val: any) => void;

export default defineComponent({
  name: "SelfPassword",
  setup(props, ctx) {
    const modelInner = reactive({
      OriPassword: "",
      Password: "",
      Password2: "",
    });
    const editForm = ref<typeof ElForm>();
    const oriPassInput = ref<typeof ElInput>();
    const btn = ref<typeof ElButton>();
    const resolveKeeper = ref<resolveCallback>();
    const dialogVisible = ref(false);
    const tokenStr = ref<string>();
    watch(dialogVisible, (newVal) => {
      if (newVal) {
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
      nextTick(() => {
        if (newVal) {
          setTimeout(() => {
            oriPassInput.value?.focus();
          }, 600);
        } else {
          editForm.value?.resetFields();
          editForm.value?.clearValidate();
        }
      });
    });
    const onCancelClick = () => {
      dialogVisible.value = false;
      if (resolveKeeper.value) {
        resolveKeeper.value(false);
      }
    };
    const onAcceptClick = () => {
      editForm.value?.validate((valid) => {
        if (valid) {
          const data = {
            oriPassword: modelInner.OriPassword,
            password: modelInner.Password,
          };
          const headers: Record<string, string> = {
            "Content-Type": "application/json;",
          };
          if (tokenStr.value) {
            headers["Authorization"] = `Bearer ${tokenStr.value}`;
          }
          request({
            url: `/api/user/self-password`,
            method: "patch",
            data,
            headers,
          })
            .then((reply) => {
              const ret = reply as any as BooleanReply;
              if (ret.success) {
                ElMessage.success(i18n.global.t("prompt.success"));
                if (resolveKeeper.value) {
                  resolveKeeper.value(true);
                }
                dialogVisible.value = false;
              } else {
                // 应该不能执行到这里
                console.error(ret);
              }
            })
            .catch((err) => {
              console.error(err);
              // 应该不用执行到这里
            });
        } else {
          return false;
        }
      });
    };

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
              if (modelInner.Password2 !== "") {
                editForm.value?.validateField("Password2");
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
            } else if (value !== modelInner.Password) {
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
    const rules = ref(ruleData);

    function showDialog(token: string | undefined) {
      tokenStr.value = token;
      return new Promise((resolve) => {
        resolveKeeper.value = resolve;
        dialogVisible.value = true;
      });
    }
    ctx.expose({ showDialog });
    return {
      dialogVisible,
      modelInner,
      rules,
      onAcceptClick,
      onCancelClick,
      editForm,
      btn,
      oriPassInput,
      resolveKeeper,
      showDialog,
    };
  },
  props: {
    title: {
      type: String,
      default: i18n.global.t("UserPassword.title"),
    },
  },
});
</script>
