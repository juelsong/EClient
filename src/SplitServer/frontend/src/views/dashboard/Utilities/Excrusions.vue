<!-- <template>
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
    :row-class-name="tableRowClassName"
  >
    <el-table-column
      prop="Sample.StartDate"
      :label="$t('Dashboard.component.component1.column.StartDate')"
      sortable="custom"
      width="180"
      :formatter="datetimeFormatUI"
    />
    <el-table-column
      prop="SampleBarcode"
      :label="$t('Dashboard.component.component1.column.BarCode')"
      sortable="custom"
      width="140"
    />
    <el-table-column
      prop="Test.Site.Name"
      :label="$t('Dashboard.component.component1.column.SiteName')"
      sortable="custom"
      width="140"
    />
    <el-table-column
      prop="Test.TestType.Description"
      :label="$t('Dashboard.component.component1.column.TestTypeName')"
      sortable="custom"
    />
  </el-table>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { CurrentWorkSpace } from "@/defs/Entity";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import { datetimeFormat } from "@/utils/formatter";

export default defineComponent({
  name: "Excursions",
  mixins: [ListMixin],
  data() {
    return {
      entityName: "CurrentWorkSpace",
      disableMountedLoad: true,
      query: {
        $orderby: "ScheduledDate desc",
        $top: 10,
        $expand:
          "Sample($select=StartDate),Test($select=Name;$expand=TestType($select=Description),Site($select=Id,Name)),Deviations($select=LimitTypeId)",
        $select: "Id,SampleBarcode,ScheduledDate,EarlyExecutionDate",
      },
    };
  },
  mounted() {
    this.loadData();
  },
  watch: {},
  methods: {
    tableRowClassName: function (row, index) {
      const currentWorkspace: CurrentWorkSpace = row.row;
      if (currentWorkspace.Deviations?.some((d) => d.LimitTypeId == 2)) {
        return "Action-row";
      }
      if (currentWorkspace.Deviations?.some((d) => d.LimitTypeId == 1)) {
        return "Alert-row";
      }
      // if (currentWorkspace.Deviation.LimitTypeId) {
      //   switch (row.row.Deviation.LimitTypeId) {
      //     case 1:
      //       return "Alert-row";
      //     case 2:
      //       return "Action-row";
      //     default:
      //       break;
      //   }
      // }
      if (row.rowIndex % 2 == 1) {
        return "double-row";
      }
      return "";
    },
    datetimeFormatUI: function (row, column) {
      if (null == row[column.property]) {
        return datetimeFormat(undefined, undefined, row.Sample.StartDate);
      }
      return datetimeFormat(row, column);
    },
    buildFilterStr() {
      let filterStr = new Array<string>();
      filterStr.push("Deviations/$count gt 0");
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
  },
});
</script>
<style>
.el-table .Action-row {
  /* display: none; */
  background: #ffe8e4;
}
.el-table .Alert-row {
  background: #fffae7;
  /* display: none; */
}
</style> -->
