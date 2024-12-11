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
      prop="ScheduledDate"
      :label="$t('Dashboard.component.component7.column.ScheduleDate')"
      sortable="custom"
      :formatter="datetimeFormatUI"
      width="180"
    />
    <el-table-column
      prop="SampleBarcode"
      :label="$t('Dashboard.component.component7.column.BarCode')"
      sortable="custom"
      width="110"
    />
    <el-table-column
      prop="Test.Name"
      :label="$t('Dashboard.component.component7.column.TestName')"
      sortable="custom"
      width="140"
    />
    <el-table-column
      prop="Location.Name"
      :label="$t('Dashboard.component.component7.column.Location')"
      sortable="custom"
      width="110"
    />
    <el-table-column
      prop="Test.TestType.Description"
      :label="$t('Dashboard.component.component7.column.TestMethod')"
      sortable="custom"
      width="110"
    />
  </el-table>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import { datetimeFormat } from "@/utils/formatter";
import { CurrentWorkSpace } from "@/defs/Entity";

export default defineComponent({
  name: "MyWork",
  mixins: [ListMixin],
  data() {
    return {
      entityName: "CurrentWorkSpace",
      currentUserId: 0,
      disableMountedLoad: true,
      query: {
        // $orderby: "Sample/StartDate desc",
        $expand:
          "Location($select=Name),Sample($select=StartDate,PersonnelUserId),Test($select=Name;$expand=TestType($select=Description))",
        $select: "Id,Status,ScheduledDate,SampleBarcode",
      },
    };
  },
  mounted() {
    // this.handleOnlyMyTaskChange();
    // request({
    //   url: `/Plan/GetCurrent`,
    //   method: "get",
    // }).then((ret) => {
    //   if (ret.success) {
    //     this.currentUserId = ret.data.CurrentId;
    //     this.loadData();
    //   }
    // });
    this.currentUserId = this.$store.state.user.id;
    this.loadData();
  },
  watch: {
    "tableData.data": {
      handler(newVal) {
        this.tableData.data.forEach((element: CurrentWorkSpace) => {
          if (
            element.ScheduledDate &&
            element.ScheduledDate.toString() == "9999-12-31T23:59:59.999999Z"
          ) {
            element.ScheduledDate = undefined;
          }
        });
      },
    },
  },
  methods: {
    tableRowClassName: function (row, index) {
      if (new Date(row.row.ScheduledDate) < new Date()) {
        return "Action-row";
      }
      if (row.rowIndex % 2 == 1) {
        return "double-row";
      }
      return "";
    },
    datetimeFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    buildFilterStr() {
      let filterStr = new Array<string>();
      filterStr.push(`NoTest eq false`);
      filterStr.push(`Completed eq false`);

      if (this.currentUserId) {
        filterStr.push(`UserId eq ${this.currentUserId}`);
      }
      //   let sDate = new Date();
      //   let startDate = new Date(
      //     sDate.getFullYear(),
      //     sDate.getMonth(),
      //     sDate.getDate(),
      //     0,
      //     0,
      //     0,
      //     0
      //   );
      //   let endDate = new Date(
      //     sDate.getFullYear(),
      //     sDate.getMonth(),
      //     sDate.getDate(),
      //     23,
      //     59,
      //     59,
      //     0
      //   );
      //   filterStr.push(`ScheduledDate ge ${startDate.toISOString()}`);
      //   filterStr.push(`ScheduledDate le ${endDate.toISOString()}`);
      // if (this.queryModel.Group && this.queryModel.Group.length > 0) {
      //   filterStr.push(`contains(Group,'${this.queryModel.Group}')`);
      // }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
  },
});
</script> -->
