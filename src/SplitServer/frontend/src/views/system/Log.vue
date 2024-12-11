<template>
  <el-card class="single-box-card">
    <template #header>
      <div class="card-header">
        <span>{{ $t("Log.entity") }}</span>
      </div>
    </template>
    <el-container>
      <el-header>
        <el-form ref="queryForm" :inline="true" :model="queryModel">
          <el-form-item
            :label="$t('Missions.filter.TestDate')"
            prop="StartDate"
          >
            <el-date-picker
              v-model="queryModel.CreatedTime"
              type="daterange"
              :range-separator="$t('label.dateRange.separator')"
              :start-placeholder="$t('label.dateRange.start')"
              :end-placeholder="$t('label.dateRange.end')"
            ></el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSearchClick">
              {{ $t("template.search") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="edit" />
              </el-icon>
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="reset">
              {{ $t("template.reset") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="refresh" />
              </el-icon>
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button
              type="primary"
              @click="exportExcel"
              style="margin-left: 8px"
            >
              {{ $t("template.export") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="download" />
              </el-icon>
            </el-button>
          </el-form-item>
        </el-form>
      </el-header>
      <el-main>
        <el-table
          ref="dataTable"
          :always="true"
          :data="tableData.data"
          highlight-current-row
          :border="true"
          height="100%"
          max-height="100%"
          width="100%"
          @sort-change="onSortChange"
        >
          <el-table-column
            prop="CreatedTime"
            :label="$t('Log.column.CreatedTime')"
            sortable="custom"
            width="200"
            :formatter="datetimeFormatUI"
          />
          <el-table-column
            prop="UserName"
            :label="$t('Log.column.UserName')"
            sortable="custom"
            width="140"
          />
          <el-table-column
            prop="Name"
            :label="$t('Log.column.Name')"
            sortable="custom"
            width="140"
          />
          <el-table-column
            prop="Description"
            :label="$t('Log.column.Description')"
            sortable="custom"
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
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import moment from "moment";
import { requestRaw } from "../../utils/request.ts";
import { dateFormat, datetimeFormat } from "@/utils/formatter";

export default defineComponent({
  name: "Log",
  mixins: [ListMixin],
  data() {
    return {
      disableMountedLoad: false,
      entityName: "Log",
      queryModel: {
        CreatedTime: undefined,
        EndDate: undefined,
        UserName: undefined,
      },
      query: {
        // $expand: "User($select=Id,RealName)",
        $select: "Id,UserName,Name,Description,CreatedTime",
        $orderby: "CreatedTime desc",
      },
    };
  },
  methods: {
    datetimeFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    buildFilterStr() {
      let filterStr = [];
      if (
        this.queryModel.CreatedTime &&
        this.queryModel.CreatedTime.length > 0
      ) {
        filterStr.push(
          `CreatedTime ge ${this.queryModel.CreatedTime[0].toISOString()}`
        );
        filterStr.push(
          `CreatedTime le ${this.queryModel.CreatedTime[1].toISOString()}`
        );
      }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    reset() {
      for (let prop in this.queryModel) {
        this.queryModel[prop] = undefined;
      }
      this.loadData();
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
        url: `/ExportCsv/Log`,
        params: params,
        responseType: "DOMString",
      }).then((res) => {
        let data = res.data; //csv数据
        let filename = "log.csv"; //导出的文件名
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

.table-pagination {
  float: right;
  margin-top: 16px;
}

.el-card {
  height: calc(/*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding);
  margin: -16px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.sub-table {
  height: calc(84px + 30 * $--app-main-padding);
  margin-top: 20px;
}
</style>

<style>
.el-tabs {
  height: 100%;
}

.el-descriptions__body > table {
  table-layout: fixed;
}

.barcode-father {
  position: relative;
  height: 160px;
}

.barcode-show {
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0%);
}
.el-table .Action-row {
  /* display: none; */
  background: #ffe8e4;
}
.el-table .double-row {
  /* display: none; */
  background: #fafafa;
}
</style>
