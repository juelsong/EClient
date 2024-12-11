<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${$t('Settings.importExport.button.Import')}-${$t(
      `Settings.importExport.${type}`
    )}`"
    width="500px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button
          type="primary"
          :disabled="ensureId == ``"
          @click="onEnsureExcelClick"
        >
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>

    <div style="margin-top: -20px">
      <el-row>
        <span class="tip-style">
          {{ `${$t("Settings.importExport.lable.Tips")}:` }}
        </span>
      </el-row>
      <el-row>
        <span class="description-style">
          {{ `${$t("Settings.importExport.lable.Description")}` }}
        </span>
      </el-row>
    </div>
    <el-form
      ref="queryForm"
      style="margin-top: 20px"
      label-width="80px"
      label-position="right"
    >
      <el-form-item :label="$t('Settings.importExport.button.Import')">
        <el-row style="width: 100%">
          <!-- <el-col :span="8">
            <span>
              {{ currentName }}
            </span>
          </el-col> -->
          <!-- <el-col :span="5" /> -->
          <el-col :span="20">
            <el-upload
              ref="upload"
              :limit="2"
              :auto-upload="false"
              action=""
              accept=".xlsx,"
              :on-change="onImportClick"
              :multiple="false"
              :show-file-list="false"
              v-has="'Security:ImportExport'"
            >
              <el-input
                v-model="currentName"
                placeholder="请选择文件"
                disabled
                style="width: 150px; margin: 0 10px"
              ></el-input>
              <el-button type="primary">
                {{ $t("Settings.importExport.button.Import") }}
              </el-button>
            </el-upload>
          </el-col>
          <el-col :span="4">
            <el-button type="text" @click="onExportClick">
              {{ $t("Settings.importExport.button.Download") }}
            </el-button>
          </el-col>
        </el-row>
      </el-form-item>
    </el-form>

    <div style="margin-top: 20px">
      <el-row>
        <span class="tipsLable-style">
          {{ `${$t("Settings.importExport.lable.TipsLable")}:` }}
        </span>
      </el-row>
      <el-row>
        <span class="tipdata-style">
          {{ `${$t("Settings.importExport.lable.Tip1")}` }}
        </span>
      </el-row>
      <el-row>
        <span class="tipdata-style">
          {{ `${$t("Settings.importExport.lable.Tips2")}` }}
        </span>
      </el-row>
    </div>
    <div v-if="ensureVisible">
      <el-result :icon="uploadResult">
        <template #extra>
          <!--  -->
          <el-button
            v-if="uploadResult == `error`"
            type="text"
            size="medium"
            @click="onExportErrorInfoClick"
          >
            {{ `${$t("Settings.importExport.button.DownloadError")}` }}
          </el-button>
        </template>
      </el-result>
    </div>
  </el-dialog>
</template>

<script lang="ts">
import * as vue from "vue";
import { defineComponent, toRaw, computed } from "vue";
import { ElLoading, ElMessage } from "element-plus";
import { requestRaw } from "@/utils/request";
import { request } from "@/utils/request";
import { i18n } from "@/i18n";
import { showPrompt } from "@/utils/esign-inject";
import {
  queryExportExcel,
  createImportExcel,
  createEnsureExcel,
} from "@/api/importAndExport_gen";
import DownloadFile from "@/utils/download-file";

import cloneDeep from "lodash.clonedeep";
declare type ElLoadingType = ReturnType<typeof ElLoading.service>;

export default vue.defineComponent({
  name: "ImportData",
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    type: {
      type: String,
      default: "",
    },
  },
  emits: ["update:visible"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const currentName = vue.ref<string>("");
    const ensureId = vue.ref<string>("");
    const upload = vue.ref<HTMLInputElement | null>(null);
    const ensureVisible = vue.ref<boolean>(false);
    const uploadResult = vue.ref<string>("");
    const errorInfo = vue.ref<string>("");

    currentName.value = i18n.global.t("Settings.importExport.lable.Empty");
    ensureId.value = "";

    let loadingInstance: ElLoadingType | undefined = undefined;
    let loadingCnt = 0;
    function showLoading() {
      loadingCnt++;
      loadingInstance = ElLoading.service({
        fullscreen: true,
      });
    }
    function closeLoading() {
      loadingCnt--;
      if (loadingCnt == 0) {
        loadingInstance?.close();
      }
    }
    function callback(code: number): void {
      closeLoading();
      if (code == 0) {
        ElMessage.success(i18n.global.t("prompt.success"));
      } else {
        showPrompt(code);
      }
    }
    // async function onExportClick() {
    //   await queryExportExcel(props.type);
    // }

    async function onImportClick(file) {
      const fileName = file.name;
      const fileType = fileName.substring(fileName.lastIndexOf("."));
      let formData = new FormData(); //  用FormData存放上传文件

      if (fileType != ".xlsx") {
        ElMessage.error("Only upload xlsx Document!");
        return;
      } else {
        currentName.value = file.name;
        formData.append("file", file.raw);
        file.UpLoaded = true;
      }
      (upload.value as any).clearFiles();

      //showLoading();
      try {
        const res = await request({
          url: `/ImportAndExport/importExcel/${props.type}`,
          method: "post",
          data: formData,
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });
        ensureVisible.value = true;
        if ((res as any).code == 0) {
          uploadResult.value = "success";
          ensureId.value = res.data;
        } else {
          uploadResult.value = "error";
          errorInfo.value = res.data;
        }
      } catch (error) {
        console.log(error);
      }
    }

    async function onEnsureExcelClick() {
      showLoading();
      if (ensureId.value) {
        closeLoading();
        const res = await createEnsureExcel(ensureId.value);
        if ((res as any).code != 0) {
          ElMessage.error(i18n.global.t("prompt.failed"));
        } else {
          ElMessage.success(i18n.global.t("prompt.success"));
          visibleInner.value = false;
        }
      }
    }

    async function onExportClick() {
      if (props.type) {
        // queryExportExcel(props.type)
        //   .then((res) => {
        //     const link = document.createElement("a");
        //     try {
        //       let blob = new Blob([res.data], {
        //         type: "application/vnd.ms-excel",
        //       });
        //       link.style.display = "none";
        //       const contentDisposition = res.headers["content-disposition"];
        //       let filename = "defaultfilename.ext";
        //       const filenameStarMatch = contentDisposition.match(
        //         /filename\*=UTF-8''(.+)/
        //       );
        //       if (filenameStarMatch && filenameStarMatch.length > 1) {
        //         // 对 URL 编码的字符串进行解码
        //         filename = decodeURIComponent(filenameStarMatch[1]);
        //       }
        //       // 兼容不同浏览器的URL对象
        //       const url = window.URL || window.webkitURL;

        //       link.href = url.createObjectURL(blob);
        //       link.setAttribute(
        //         "download",
        //         filename.substring(filename.lastIndexOf("_") + 1)
        //       );
        //       document.body.appendChild(link);
        //       link.click();
        //       document.body.removeChild(link);
        //       url.revokeObjectURL(link.href); //销毁url对象
        //     } catch (e) {
        //       console.log("下载的文件出错", e);
        //     }
        //   })
        //   .catch(() => {});

        await DownloadFile(
          `/ImportAndExport/exportExcel/${props.type}`,
          ""!,
          "GET"
        );
      }
    }
    async function onExportErrorInfoClick() {
      if (errorInfo.value) {
        const content = errorInfo.value;
        const blob = new Blob([content], { type: "text/plain" });
        const link = document.createElement("a");
        link.href = window.URL.createObjectURL(blob);
        link.download = "errorInfo.txt";
        link.click();
        window.URL.revokeObjectURL(link.href);
      }
    }
    // vue.onMounted(() => {
    //   if (upload.value) {
    //     upload.value!.clearFiles();
    //   }
    // });
    vue.watch(
      () => props.visible,
      () => {
        // 在组件挂载之前重置变量
        currentName.value = "";
        ensureId.value = "";
        uploadResult.value = "";
        ensureVisible.value = false;
        errorInfo.value = "";
      }
    );
    return {
      currentName,
      ensureId,
      visibleInner,
      upload,
      ensureVisible,
      uploadResult,
      showLoading,
      onExportClick,
      onImportClick,
      onEnsureExcelClick,
      onExportErrorInfoClick,
    };
  },
});
</script>

<style lang="scss" scoped>
.tip-style {
  font-size: 16px; /* 设置字体大小 */
  color: red; /* 设置字体颜色 */
}

.description-style {
  font-size: 16px; /* 设置字体大小 */
}
.tipsLable-style {
  font-size: 16px; /* 设置字体大小 */
}
.tipdata-style {
  font-size: 14px; /* 设置字体大小 */
}
</style>
