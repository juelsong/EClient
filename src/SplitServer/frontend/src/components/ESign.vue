<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${$t('ESign.title')}${
      needESignCount > 1 ? `${currentESign + 1}/${needESignCount}` : ``
    }`"
    :show-close="false"
    width="400px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="onCancelClick">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button
          type="primary"
          :loading="loadingFlag"
          @click="onAcceptClick"
          v-no-more-click
        >
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
      <span style="color: red; fontsize: large">
        {{ $t("ESign.editor.Tips") }}
      </span>
      <el-form-item :label="$t('ESign.editor.CurrentUser')" v-if="firstSign">
        <!-- <el-input ref="modelInner.CurrentUser"
                  readonly="true"
                  v-model="modelInner.CurrentUser"
                  border:0
                  :placeholder="$t('ESign.editor.CurrentUser')"></el-input> -->
        <span>{{ modelInner.CurrentUser }}</span>
      </el-form-item>
      <!-- <div>
        <span>{{$t('ESign.editor.CurrentUser')}}</span>

        <span>{{modelInner.CurrentUser}}</span>
      </div> -->
      <el-form-item :label="$t('ESign.editor.Account')" prop="Account">
        <el-input
          ref="modelInner.Account"
          v-model="modelInner.Account"
          :placeholder="$t('ESign.editor.Account')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('ESign.editor.Password')" prop="Password">
        <el-input
          show-password
          v-if="visibleInner"
          ref="modelInner.Password"
          v-model="modelInner.Password"
          :placeholder="$t('ESign.editor.Password')"
        ></el-input>
      </el-form-item>
      <!-- <el-form-item :label="$t('ESign.editor.Comment')"
                    prop="Comment">
        <el-input ref="modelInner.Comment"
                  v-model="modelInner.Comment"
                  :placeholder="$t('ESign.editor.Comment')"></el-input>
      </el-form-item> -->
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import { defineComponent, computed, ref } from "vue";
import { ElForm, ElInput } from "element-plus";
import { request } from "@/utils/request";
import { AxiosRequestHeaders } from "axios";
import { GetPromptKeyFromCode } from "@/defs/ErrorCode";
import { mapGetters } from "vuex";
export default defineComponent({
  name: "ESign",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    category: {
      type: String,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const loadingFlag = ref(false);
    return { visibleInner, loadingFlag };
  },
  watch: {
    visibleInner(newVal) {
      if (!newVal) {
        // 二次签名需要
        this.loadingFlag = false;
      }
      this.$nextTick(() => {
        (this.$refs.editForm as typeof ElForm).clearValidate();
        this.modelInner.CurrentUser = this.$store.state.user.userName;
        this.modelInner.Account =
          this.modelInner.Password =
          this.modelInner.Comment =
            "";
        if (newVal) {
          (this.$refs["modelInner.Account"] as typeof ElInput).focus();
        }
      });
    },
  },
  data() {
    return {
      modelInner: {
        Account: "",
        Password: "",
        Comment: "",
        CurrentUser: "",
      },
      rules: {
        Account: [
          // {
          //   required: true,
          //   message: () =>
          //     this.$t("validator.template.required", [
          //       this.$t("ESign.editor.Account"),
          //     ]),
          //   trigger: "blur",
          // },
          {
            validator: (rule, value, callback) => {
              let that = this;
              if (value == "") {
                callback(
                  new Error(
                    this.$t("validator.template.required", [
                      this.$t("ESign.editor.Account"),
                    ])
                  )
                );
              } else if (
                this.firstSign &&
                value != this.$store.state.user.name
              ) {
                callback(new Error(this.$t("ESign.confirm.notSameUser")));
              } else {
                callback();
              }
            },
            required: true,
            trigger: "blur",
          },
        ],
        Password: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("ESign.editor.Password"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  computed: {
    ...mapGetters(["needESignCount", "currentESign"]),
    firstSign() {
      return this.currentESign == 0;
    },
  },
  methods: {
    onAcceptClick() {
      (this.$refs.editForm as typeof ElForm).validate((valid) => {
        if (valid) {
          const headers: AxiosRequestHeaders = {
            "X-ESIGN-USER": this.modelInner.Account,
            "X-ESIGN-PASS": this.modelInner.Password,
            "X-ESIGN-COMMENT": this.modelInner.Comment,
            "X-ESIGN-SERIAL": this.$store.state.esign.serialNumber,
          };
          if (this.$store.state.esign.headerCount) {
            headers["X-ESIGN-COUNT"] = this.$store.state.esign.headerCount;
          }
          this.loadingFlag = true;
          request({
            url: this.$store.state.esign.url,
            method: this.$store.state.esign.method,
            headers,
            data: {},
          })
            .then((ret) => {
              this.$store.state.esign.resolve(ret);
              this.$store.commit("esign/SET_SERIAL_NUMBER", undefined);
              this.loadingFlag = false;
            })
            //全局报错，此处用disable设置防抖
            // .finally(() => {
            //   setTimeout(() => {
            //     this.disabled = false;
            //   }, 2000);
            // })
            .catch((err) => {
              // if (err) {
              //   const promptKey = GetPromptKeyFromCode(err);
              //   this.$message.error(this.$t(promptKey));
              // }
              // 已经在 esign-inject 中集体处理了
              console.error(err);
              this.loadingFlag = false;
            });
        } else {
          return false;
        }
      });
    },
    onCancelClick() {
      this.$confirm(
        this.$t("ESign.confirm.cancelESign"),
        this.$t("template.tips"),
        {
          confirmButtonText: this.$t("template.accept"),
          cancelButtonText: this.$t("template.cancel"),
        }
      ).then(() => {
        this.$store.state.esign.reject();
        this.visibleInner = false;
      });
    },
  },
});
</script>
