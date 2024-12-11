<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Map.entity'
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
      <el-form-item :label="$t('Map.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Map.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Map.editor.Category')" prop="MapCategoryId">
        <el-select
          :disabled="createNew"
          ref="modelInner.MapCategoryId"
          v-model="modelInner.MapCategoryId"
          :placeholder="$t('Map.editor.Category')"
        >
          <el-option
            v-for="item in categorys"
            :key="item.Id"
            :label="item.Name"
            :value="item.Id"
          />
        </el-select>
      </el-form-item>
      <el-form-item :label="$t('Map.editor.Path')" prop="Path">
        <el-upload
          ref="imgUpload"
          class="avatar-upload"
          :action="`${baseUrl}/File/UploadImg`"
          :show-file-list="false"
          :headers="uploadHeader"
          :thumbnail-mode="true"
          :auto-upload="false"
          :before-upload="onBeforeImgUpload"
          :on-success="onImgUploadSuccess"
          :on-error="onImgUploadError"
          :on-change="onImgChange"
        >
          <img v-if="imgPreview" :src="imgPreview" class="avatar" />
          <svg-icon v-else iconClass="plus" />
        </el-upload>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import SvgIcon from "@/components/SvgIcon";
import { requestRaw } from "@/utils/request";
import { getBaseUrl, getTenant, getHeader } from "@/utils/global";
// import base64js from "base64-js";

let imgSubmitResolve = undefined,
  imgSubmitReject = undefined;
export default defineComponent({
  components: { SvgIcon },
  name: "MapEditor",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Object,
    },
    categorys: {
      type: Array,
      required: true,
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
        this.loadImage();
      } else {
        this.imageChange = false;
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        if (newVal) {
          this.$refs["modelInner.Name"].focus();
        }
      });
    },
  },
  data() {
    return {
      baseUrl: getBaseUrl(),
      uploadHeader: { "X-TENANT": getTenant() },
      imageChange: false,
      modelInner: {},
      imgPreview: undefined,
      rules: {
        MapCategoryId: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Map.editor.Category"),
              ]),
            trigger: "blur",
          },
        ],
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Map.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist("Map", "Name", value, this.modelInner.Id).then(
                  (exist) => {
                    if (exist) {
                      callback(
                        new Error(
                          this.$t("validator.template.exist", [
                            this.$t("Map.editor.Name"),
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
        Path: [
          {
            required: true,
            validator: (rule, value, callback) => {
              if (this.imgPreview) {
                // if(this.validateImg())
                if (
                  this.$refs.imgUpload.uploadFiles &&
                  this.$refs.imgUpload.uploadFiles[0]
                ) {
                  let file = this.$refs.imgUpload.uploadFiles[0];
                  let validate = this.validateImg(file.raw);
                  if (validate) {
                    callback(validate);
                  } else {
                    callback();
                  }
                } else {
                  callback();
                }
              } else {
                this.$t("validator.template.required", [
                  this.$t("Map.editor.Path"),
                ]);
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
          if (this.imgPreview && this.imageChange) {
            this.submitImgAsync().then((rsp) => {
              if (rsp) {
                this.modelInner.Path = rsp.data;
                this.submitRecord();
              }
            });
          } else {
            this.submitRecord();
          }
        } else {
          return false;
        }
      });
    },
    onBeforeImgUpload(file) {
      let validate = this.validateImg(file);
      if (validate) {
        this.$message.Error(validate);
        return false;
      }
      const header = getHeader();
      header.forEach((v, k) => {
        const lower = k.toLocaleLowerCase();
        const upper = k.toUpperCase();
        const configHeaderKeys = Object.keys(this.uploadHeader);
        if (configHeaderKeys.includes(lower)) {
          this.uploadHeader[lower] = v;
        } else if (configHeaderKeys.includes(upper)) {
          this.uploadHeader[upper] = v;
        } else {
          this.uploadHeader[k] = v;
        }
      });
      return true;
    },
    validateImg(file) {
      const isImg = file.type === "image/jpeg" || file.type === "image/png";
      const isLt5M = file.size / 1024 / 1024 < 5;

      if (!isImg) {
        return this.$t("Map.validator.ImageType");
      }
      if (!isLt5M) {
        return this.$t("Map.validator.ImageSize");
      }
    },
    onImgUploadSuccess(rsp) {
      if (rsp && imgSubmitResolve) {
        imgSubmitResolve(rsp);
      }
    },
    onImgUploadError(err) {
      if (imgSubmitReject) {
        imgSubmitReject(err);
      }
    },
    loadImage() {
      if (this.modelInner.Path) {
        requestRaw({
          url: `/File/Image/${this.modelInner.Path}`,
          method: "get",
        })
          .then((ret) => {
            let tmp = URL.createObjectURL(ret.data);
            this.imgPreview = tmp;
          })
          .catch(() => {
            this.imgPreview = undefined;
          });
      } else {
        this.imgPreview = undefined;
      }
    },
    onImgChange(file) {
      this.imageChange = true;
      this.imgPreview = URL.createObjectURL(file.raw);
    },
    submitRecord() {
      this.$emit("update:model", this.modelInner);
      this.visibleInner = false;
      this.$emit("accept");
    },
    submitImgAsync() {
      return new Promise((resolve, reject) => {
        imgSubmitResolve = resolve;
        imgSubmitReject = reject;
        this.$refs.imgUpload.submit();
      });
    },
  },
});
</script>
<style>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
