<!-- <script lang="ts">
import * as vue from "vue";
import ImportData from "./Utilities/ImportData.vue";

import { ElForm, ElMessage } from "element-plus";
import { i18n } from "@/i18n";
import { FormItemRule } from "@/defs/Types";
import { queryDownloadExcel } from "@/api/importAndExport_gen";
export default vue.defineComponent({
  name: "ImportExport",
  components: { ImportData },
  setup() {
    const emailFormRef = vue.ref<typeof ElForm>();
    const currentType = vue.ref<String>();
    let importVisible = vue.ref(false);

    var importExportDatas = [
      "Location",
      "Site",
      "Equipment",
      "Media",
      "Organism",
    ] as string[];

    async function onAcceptClick() {
      if (emailFormRef.value) {
        const valid = await emailFormRef.value.validate();
        if (valid) {
          if (true) {
            ElMessage.success(i18n.global.t("prompt.success"));
          } else {
            ElMessage.error(i18n.global.t("prompt.failed"));
          }
        }
      }
    }

    function onImportClick(type) {
      currentType.value = type;
      importVisible.value = true;
    }
    async function onExportClick(type: String) {
      if (type) {
        currentType.value = type;
        queryDownloadExcel(type as string)
          .then((res) => {
            const link = document.createElement("a");
            try {
              let blob = new Blob([res.data], {
                type: "application/vnd.ms-excel",
              });
              link.style.display = "none";
              const contentDisposition = res.headers["content-disposition"];
              let filename = "defaultfilename.ext";
              const filenameStarMatch = contentDisposition.match(
                /filename\*=UTF-8''(.+)/
              );
              if (filenameStarMatch && filenameStarMatch.length > 1) {
                // 对 URL 编码的字符串进行解码
                filename = decodeURIComponent(filenameStarMatch[1]);
              }
              // 兼容不同浏览器的URL对象
              const url = window.URL || window.webkitURL;

              link.href = url.createObjectURL(blob);
              link.setAttribute(
                "download",
                filename.substring(filename.lastIndexOf("_") + 1)
              );
              document.body.appendChild(link);
              link.click();
              document.body.removeChild(link);
              url.revokeObjectURL(link.href); //销毁url对象
            } catch (e) {
              console.log("下载的文件出错", e);
            }
          })
          .catch(() => {});
      }
    }

    return {
      importExportDatas,
      currentType,
      importVisible,
      onImportClick,
      onExportClick,
    };
  },
});
</script>

<template>
  <el-scrollbar>
    <el-row v-for="(item, index) in importExportDatas" :key="index">
      <el-col :span="6">
        <div class="info-container">
          <div class="info-label">
            <span>{{ $t(`Settings.importExport.${item}`) }}:</span>
          </div>
          <div class="button-container">
            <el-button
              type="primary"
              class="info-container-button"
              v-has="`Import:${item}`"
              @click="onImportClick(item)"
            >
              {{ $t("Settings.importExport.button.Import") }}
            </el-button>
            <el-button
              class="info-container-button"
              v-has="`Export:${item}`"
              type="primary"
              @click="onExportClick(item)"       
            >
              {{ $t("Settings.importExport.button.Export") }}
            </el-button>
          </div>
        </div>
      </el-col>
    </el-row>
    <ImportData
      :type="currentType"
      :visible="importVisible"
      @update:visible="importVisible = false"
    />
  </el-scrollbar>
</template>

<style lang="scss" scoped>
.info-container {
  display: flex;
  align-items: center; /* 垂直居中对齐子元素 */
  justify-content: space-between; /* 在容器内分配空间，让span和按钮组两端对齐 */
  margin-top: 10px;
}

.info-label {
  margin-left: 10px;
  /* span的样式，确保它的长度不受影响 */
}

.button-container {
  display: flex; /* 使用flex布局 */
  justify-content: flex-end; /* 将按钮组向右对齐 */
}

.info-container-button {
  margin-left: 10px; /* 按钮之间的间距 */
}
</style> -->
