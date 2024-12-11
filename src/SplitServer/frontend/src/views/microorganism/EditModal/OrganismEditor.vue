<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Organism.entity'
    )}`"
    width="450px"
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
      label-width="90px"
      label-position="right"
    >
      <el-form-item
        :label="$t('Organism.editor.OrganismType')"
        prop="OrganismType"
      >
        <o-data-selector
          ref="OrganismType"
          :placeholder="`${$t('template.select', [
            $t('Organism.editor.OrganismType'),
          ])}`"
          :multiple="false"
          v-model="selectOrganismType"
          entity="OrganismType"
          label="Description"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.OrganismGenus')"
        prop="Organism.OrganismGenusId"
      >
        <o-data-selector
          ref="Organism.OrganismGenus"
          :placeholder="`${$t('template.select', [
            $t('Organism.editor.OrganismGenus'),
          ])}`"
          :multiple="false"
          :autoLoad="false"
          v-bind:filter="organismTypeFilter"
          v-model="modelInner.Organism.OrganismGenusId"
          entity="OrganismGenus"
          label="Description"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.Species')"
        prop="Organism.Species"
      >
        <el-input
          ref="Organism.Species"
          v-model="modelInner.Organism.Species"
          :placeholder="$t('Organism.editor.Species')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.Description')"
        prop="Organism.Description"
      >
        <el-input
          ref="Organism.Description"
          v-model="modelInner.Organism.Description"
          :placeholder="$t('Organism.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.Strain')"
        prop="Organism.Strain"
      >
        <el-input
          ref="Organism.Strain"
          v-model="modelInner.Organism.Strain"
          :placeholder="$t('Organism.editor.Strain')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.OrganismSeverity')"
        prop="Organism.OrganismSeverity"
      >
        <o-data-selector
          :placeholder="`${$t('template.include', [
            $t('Organism.editor.OrganismSeverity'),
          ])}`"
          :multiple="false"
          v-model="modelInner.Organism.OrganismSeverityId"
          entity="OrganismSeverity"
          :label="getLableDescription()"
          value="Id"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Organism.editor.PicturePath')"
        prop="Organism.PicturePath"
      >
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
      <el-form-item>
        <el-button @click="addDomain">
          {{ $t("Organism.operation.Add") }}
        </el-button>
      </el-form-item>
      <template v-for="(domain, index) in sevLocFilter" :key="domain.key">
        <el-form-item
          :label="$t('Organism.editor.OrganismSeverity') + (index + 1)"
          :prop="'SeverityLocations.' + index + '.OrganismSeverity'"
        >
          <o-data-selector
            :placeholder="`${$t('template.include', [
              $t('Organism.editor.OrganismSeverity'),
            ])}`"
            :multiple="false"
            v-model="domain.OrganismSeverityId"
            entity="OrganismSeverity"
            :label="getLableDescription()"
            value="Id"
          />
          <el-button @click.prevent="removeDomain(domain)">
            {{ $t("template.delete") }}
          </el-button>
        </el-form-item>
        <el-form-item
          :label="$t('Organism.editor.Location') + (index + 1)"
          :prop="'SeverityLocations.' + index + '.Location'"
        >
          <region-tree
            :autoLoad="true"
            :placeholder="`${$t('template.include', [
              $t('Organism.editor.Location'),
            ])}`"
            v-model="domain.LocationId"
          />
        </el-form-item>
      </template>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, unref, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
import RegionTree from "@/components/RegionTree.vue";
import { requestRaw } from "@/utils/request";
import { getBaseUrl, getTenant, getHeader } from "@/utils/global";
import getLableDescription from "@/i18n/localeHelper";

let imgSubmitResolve = undefined,
  imgSubmitReject = undefined;
export default defineComponent({
  name: "OrganismEditor",
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
    OrganismTypeId: {
      type: Number,
      default: null,
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
  computed: {
    organismTypeFilter() {
      if (this.selectOrganismType) {
        return `OrganismTypeId eq ${this.selectOrganismType}`;
      } else {
        return undefined;
      }
    },
    sevLocFilter() {
      return this.modelInner.SeverityLocations.filter((sevLoc) => {
        return sevLoc.IsActive;
      });
    },
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(unref(this.model));
        this.modelInner = copyQuery;
        this.loadImage();
        if (this.modelInner.Organism.OrganismGenusId == undefined) {
          if (this.OrganismTypeId) {
            this.selectOrganismType = this.OrganismTypeId;
            this.modelInner.OrganismType = this.OrganismTypeId;
          }
        } else {
          // todo: 需要在这里增加没有微生物类型选择而编辑时获取微生物类型的办法
          if (this.OrganismTypeId) {
            this.selectOrganismType = this.OrganismTypeId;
            this.modelInner.OrganismType = this.OrganismTypeId;
          }
        }
        this.$nextTick(() => {
          this.$refs.editForm.clearValidate();
          this.$refs["OrganismType"].loadData();
          this.$refs["Organism.OrganismGenus"].loadData();
          this.$refs["Organism.Species"].focus();
        });
      } else {
        this.imageChange = false;
      }
    },
    organismTypeFilter() {
      this.$nextTick(() => {
        this.modelInner.Organism.OrganismGenusId = undefined;
        this.$refs["Organism.OrganismGenus"].loadData();
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
      selectOrganismType: 0,
      rules: {
        OrganismType: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Organism.editor.OrganismType"),
              ]),
            trigger: "blur",
          },
        ],
        Organism: {
          OrganismGenusId: [
            {
              required: true,
              message: () =>
                this.$t("validator.template.required", [
                  this.$t("Organism.editor.OrganismGenus"),
                ]),
              trigger: "blur",
            },
          ],
          Species: [
            {
              required: true,
              message: () =>
                this.$t("validator.template.required", [
                  this.$t("Organism.editor.Species"),
                ]),
              trigger: "blur",
            },
            {
              validator: (rule, value, callback) => {
                if (value) {
                  this.$exist(
                    "Organism",
                    "Species",
                    value,
                    this.modelInner.Organism.Id
                  ).then((exist) => {
                    if (exist) {
                      callback(
                        new Error(
                          this.$t("validator.template.exist", [
                            this.$t("Organism.editor.Species"),
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
          Description: [
            {
              required: true,
              message: () =>
                this.$t("validator.template.required", [
                  this.$t("Organism.editor.Description"),
                ]),
              trigger: "blur",
            },
          ],
        },
      },
    };
  },
  methods: {
    getLableDescription,
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          if (this.imgPreview && this.imageChange) {
            this.submitImgAsync().then((rsp) => {
              if (rsp) {
                this.modelInner.Organism.PicturePath = rsp.data;
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
      if (this.modelInner.Organism.PicturePath) {
        requestRaw({
          url: `/File/Image/${this.modelInner.Organism.PicturePath}`,
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
    addDomain() {
      this.modelInner.SeverityLocations.push({
        key: Date.now(),
        OrganismSeverityId: 18,
        LocationId: undefined,
        IsActive: true,
      });
    },
    removeDomain(item) {
      item.IsActive = false;
      // const index = this.modelInner.SeverityLocations.indexOf(item)
      // if (index !== -1) {
      //   this.modelInner.SeverityLocations.splice(index, 1)
      // }
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
