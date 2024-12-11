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
            prop="Sample.BarCode"
            :label="$t('Visualization.column.Barcode')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="CurrentWorkSpace.ExecuteUserId.Account"
            :label="$t('Visualization.column.ResultsEnteredUserName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="Sample.Test.TestType.Description"
            :label="$t('Visualization.column.TestTypeName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="Sample.Test.Site.Name"
            :label="$t('Visualization.column.SiteName')"
            width="150"
            sortable="custom"
          />
          <el-table-column
            prop="CurrentWorkSpace.Location.Name"
            :label="$t('Visualization.column.LocationFullName')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            prop="CreatedTime"
            :formatter="datetimeFormatUI"
            sortable="custom"
            :label="$t('Visualization.column.StartDate')"
            width="200"
          />
          <el-table-column
            prop="Count"
            :label="$t('Visualization.organism.Count')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            prop="Organism.Description"
            :label="$t('Visualization.organism.Name')"
            sortable="custom"
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
import { oDataQuery } from "@/utils/odata";
import { OdataQuery } from "odata";
import streamSaver from "streamsaver";

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
      entityName: "WorkSpaceOrganism",
      query: {
        $expand:
          "Sample($select=BarCode;$expand=Test($select=Name;$expand=Site($select=Name),TestType($select=Description))),Organism,CurrentWorkSpace($select=CompletedDate;$expand=Location($select=Name),ExecuteUser($select=Account))",
      },
      disableMountedLoad: true,
      downLoadData: {
        writer: undefined,
        fileStream: undefined,
        currentLength: 0,
        pageSize: 1000,
        total: 0,
        skip: 0,
      },
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
      if (this.querys.length > 1) {
        return this.querys.map((f) => `(${f})`).join(" and ");
      } else {
        return "";
      }
    },
    exportExcel() {
      this.downLoadData.currentLength = 0;
      this.downLoadData.fileStream =
        streamSaver.createWriteStream("organismData.csv");
      this.downLoadData.writer = this.downLoadData?.fileStream.getWriter();
      this.getSinglePageData();
    },
    getSinglePageData() {
      let that = this;
      let encode = TextEncoder.prototype.encode.bind(new TextEncoder());

      oDataQuery(this.entityName, {
        $count: true,
        $expand: this.query.$expand,
        $skip: this.downLoadData.skip,
        $filter:
          this.querys.length > 1
            ? this.querys.map((f) => `(${f})`).join(" and ")
            : "Id ne 0",
      }).then((data) => {
        that.downLoadData?.writer.write(encode("\uFEFF"));
        if (that.downLoadData.currentLength == 0) {
          let header =
            `${that.$t("Visualization.column.Barcode")},` +
            `${that.$t("Visualization.column.ResultsEnteredUserName")},` +
            `${that.$t("Visualization.column.TestTypeName")},` +
            `${that.$t("Visualization.column.SiteName")},` +
            `${that.$t("Visualization.column.LocationFullName")},` +
            `${that.$t("Visualization.column.StartDate")},` +
            `${that.$t("Visualization.organism.Count")},` +
            `${that.$t("Visualization.organism.Name")},` +
            ``;
          that.downLoadData?.writer.write(encode(header));
          that.downLoadData?.writer.write(encode("\n"));
        }
        that.downLoadData.currentLength += data.value.length;
        that.downLoadData.total = data["@odata.count"];
        data.value.forEach((element) => {
          console.log(element);
          let line = "";
          line +=
            `="${element.Sample.BarCode}",` +
            `${element.CurrentWorkSpace.User.Account},` +
            `${element.Sample.Test.TestType.Description},` +
            `${element.Sample.Test.Site.Name},` +
            `${element.CurrentWorkSpace.Location.Name},` +
            `${moment(element.CreatedTime).format(
              that.$store.state.user.userSettings.DateTimeFormat
            )},` +
            `${element.Sample.Test.TestType.Description},` +
            `${element.Count},` +
            `${element.Organism.Description},`;
          that.downLoadData?.writer.write(encode(line));
          that.downLoadData?.writer.write(encode("\n"));
        });
        if (that.downLoadData.total > that.downLoadData.currentLength) {
          that.downLoadData.skip = that.downLoadData.currentLength;
          that.getSinglePageData();
        } else {
          that.downLoadData?.writer.close();
          that.exportButtonLoading = false;
          return;
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
../../utils/request.js
