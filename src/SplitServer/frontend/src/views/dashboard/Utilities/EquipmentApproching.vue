<template>
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
      prop="Name"
      :label="$t('Dashboard.component.component3.column.Name')"
      sortable="custom"
    />
    <el-table-column
      prop="EquipmentType.Description"
      :label="$t('Dashboard.component.component3.column.EquipmentType')"
      sortable="custom"
      width="120"
    />
    <el-table-column
      prop="CalibrationDate"
      :label="$t('Dashboard.component.component3.column.CalibrationDate')"
      sortable="custom"
      width="180"
      :formatter="dateFormatUI"
    />
    <el-table-column
      prop="NextCalibrationDate"
      :label="$t('Dashboard.component.component3.column.NextCalibrationDate')"
      sortable="custom"
      width="180"
      :formatter="dateFormatUI"
    />
  </el-table>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import { dateOnlyFormat, datetimeFormat } from "@/utils/formatter";
// import ODataSelector from "@/components/ODataSelector.vue";

export default defineComponent({
  name: "EquipmentApproching",
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Equipment",
      query: {
        $orderby: "NextCalibrationDate desc",
        $expand: "EquipmentType($select=Type,Description)",
        $select: "Id,Name,CalibrationDate,NextCalibrationDate",
      },
      withoutPaging: true,
    };
  },
  mounted() {
    this.loadData();
  },
  watch: {},
  methods: {
    datetimeFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    dateFormatUI: function (row, column) {
      return dateOnlyFormat(row, column);
    },
    buildFilterStr() {
      let filterStr = new Array<string>();
      let ss = this.getTimeInterval(-30);
      filterStr.push("IsActive eq true");
      filterStr.push(`NextCalibrationDate le ${ss.startTime}`);
      // if (this.queryModel.Group && this.queryModel.Group.length > 0) {
      //   filterStr.push(`contains(Group,'${this.queryModel.Group}')`);
      // }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    getTimeInterval(time) {
      //time为减去的指定天数
      const date1 = new Date();
      const date2 = new Date(date1);

      // -30为30天前，+30可以获得30天后的日期
      date2.setDate(date1.getDate() - time);

      // 30天前（日，月份判断是否小于10，小于10的前面+0）
      const agoDay = `${date2.getFullYear()}-${
        date2.getMonth() + 1 < 10
          ? `0${date2.getMonth() + 1}`
          : date2.getMonth() + 1
      }${
        date2.getDate() < 10 ? `-0${date2.getDate()}` : `-` + date2.getDate()
      }`;

      // 当前日期
      const nowDay = `${date1.getFullYear()}-${
        date1.getMonth() + 1 < 10
          ? `0${date1.getMonth() + 1}`
          : date1.getMonth() + 1
      }${
        date1.getDate() < 10 ? `-0${date1.getDate()}` : `-` + date1.getDate()
      }`;

      // console.log(`30天前：${agoDay}`)
      // console.log(`当前日期：${nowDay}`)
      const obj = {
        startTime: new Date(agoDay).toISOString(),
        endTime: new Date(nowDay).toISOString(),
      };
      return obj;
    },
  },
});
</script>
