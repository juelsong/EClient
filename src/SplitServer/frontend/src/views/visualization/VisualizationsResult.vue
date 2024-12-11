<template>
  <el-card class="single-box-card">
    <template #header>
      <div>
        <span>{{ $t("Visualization.label.Result") }}</span>
        <el-button-group :style="{ float: 'right' }">
          <el-button type="primary" @click="exportExcel">
            {{ $t("label.export") }}
          </el-button>
          <el-button @click="visibleInner = false">
            {{ $t("label.return") }}
          </el-button>
        </el-button-group>
      </div>
    </template>
    <el-container>
      <el-main>
        <!-- table区域-begin -->
        <el-table
          id="out-table"
          ref="dataTable"
          :always="true"
          :data="tableData.data"
          height="100%"
          max-height="100%"
          width="100%"
          :border="true"
          stripe
          highlight-current-row
          :row-class-name="tableRowClassName"
          @sort-change="onSortChange"
          @selection-change="onSelectionChange"
        >
          <el-table-column
            prop="BarCode"
            :label="$t('Visualization.column.Barcode')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="TestTypeName"
            :label="$t('Visualization.column.TestTypeName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="SiteName"
            :label="$t('Visualization.column.SiteName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="LocationFullName"
            :label="$t('Visualization.column.LocationFullName')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            :prop="`Environment.${getLableDescription()}`"
            :label="$t('Visualization.column.Environment')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="Name"
            :label="$t('Visualization.column.TestResultName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="TestResultValue"
            :label="$t('Visualization.column.TestResultValue')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="UOM"
            :label="$t('Visualization.column.UOM')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="MostServerDeviation"
            :label="$t('Visualization.column.MostServerDeviation')"
            sortable="custom"
            width="500"
          />
          <!-- <el-table-column
          prop="MostServerDeviation"
          label="最大限度触发名称"
          sortable="custom"
          width="150"
        /> -->
          <el-table-column
            prop="ActionLimitValue"
            :label="$t('Visualization.column.ActionLimitValue')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            prop="AlertLimitValue"
            :label="$t('Visualization.column.AlertLimitValue')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            prop="ResultsEnteredUserName"
            :label="$t('Visualization.column.ResultsEnteredUserName')"
            sortable="custom"
            width="150"
          />

          <el-table-column
            prop="StartDate"
            :formatter="datetimeFormatUI"
            sortable="custom"
            :label="$t('Visualization.column.StartDate')"
            width="200"
          />
          <el-table-column
            prop="EndDate"
            :formatter="datetimeFormatUI"
            sortable="custom"
            :label="$t('Visualization.column.EndDate')"
            width="200"
          />
          <el-table-column
            prop="CompleteDate"
            :formatter="datetimeFormatUI"
            sortable="custom"
            :label="$t('Visualization.column.CompleteDate')"
            width="200"
          />
          <el-table-column
            prop="ApproveDate"
            :formatter="datetimeFormatUI"
            sortable="custom"
            :label="$t('Visualization.column.ApproveDate')"
            width="200"
          />
        </el-table>
      </el-main>
      <el-footer>
        <el-pagination
          class="table-pagination"
          v-model:currentPage="tableData.current"
          v-model:pageSize="tableData.pageSize"
          :page-sizes="[10, 20, 50]"
          layout="total, prev, pager, next, sizes, jumper"
          :total="tableData.total"
          :pager-count="5"
        ></el-pagination>
      </el-footer>
    </el-container>
  </el-card>
</template>

<script>
import { defineComponent, computed } from "vue";
import { ListMixin } from "@/mixins/ListMixin";
import moment from "moment";
import { requestRaw } from "../../utils/request.ts";
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import getLableDescription from "@/i18n/localeHelper";

export default defineComponent({
  name: "VisualizationsResult",
  mixins: [ListMixin],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    return { visibleInner, getLableDescription };
  },
  inject: ["visualizationQueryFilter"],
  emits: ["update:visible"],
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    result: {
      type: Array,
    },
    querys: {
      type: Array,
    },
  },
  data() {
    return {
      entityName: "RptSampleMart",
      query: {
        $expand: "Environment",
        $select:
          "BarCode,ActionLimitValue,AlertLimitValue,SiteName,TestTypeName,TestResultValue,UOM,MostServerDeviation,MostServerDeviation,ResultsEnteredUserName,LocationFullName,StartDate,EndDate,CompleteDate,ApproveDate,Name",
      },
      alertcolor: "#FF9B00",
      actioncolor: "#FF0000",
      disableMountedLoad: true,
    };
  },
  watch: {
    visible(val) {
      if (true === val) {
        this.loadData();
      }
    },
  },
  methods: {
    tableRowClassName({ row }) {
      if (row.MostServerDeviation === "Alert") {
        return "alert-row";
      }
      return "";
    },
    datetimeFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    buildFilterStr() {
      return this.visualizationQueryFilter();
    },
    exportExcel() {
      let filter = this.buildFilterStr();

      let params = {
        $filter: "",
      };
      if (filter && filter.length > 0) {
        params.$filter = filter;
      } else {
        delete params.$filter;
      }
      requestRaw({
        method: "get",
        url: `/ExportCsv/RptSampleMart`,
        params: params,
        responseType: "DOMString",
      }).then((res) => {
        let data = res.data; //csv数据
        let filename = "export.csv"; //导出的文件名
        let type = ""; //头部数据类型
        let file = new Blob(["\ufeff" + data], { type: type });
        if (window.navigator.msSaveOrOpenBlob)
          // IE10+
          window.navigator.msSaveOrOpenBlob(file, filename);
        else {
          // Others
          let a = document.createElement("a"),
            url = URL.createObjectURL(file);
          a.href = url;
          a.download = filename;
          document.body.appendChild(a);
          a.click();
          setTimeout(function () {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
          }, 0);
        }
      });
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.el-alert--info.is-light {
  background-color: var(--el-color-primary-light-9);
  border: 1px solid var(--el-color-primary-light-5);
  a {
    color: var(--el-color-primary);
  }
}
.table-pagination {
  float: right;
  margin-top: 16px;
}
.el-table .alert-row {
  background: #ff9b00;
}
.el-table .action-row {
  background: #ff0000;
}
.sub-table {
  height: calc(84px + 40 * $--app-main-padding);
}
</style>
