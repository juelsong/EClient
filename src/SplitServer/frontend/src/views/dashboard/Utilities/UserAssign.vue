<template>
  <div id="usersAssigns" style="height: 100%"></div>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import * as echarts from "echarts";
// import ODataSelector from "@/components/ODataSelector.vue";
import moment from "moment";

export default defineComponent({
  name: "UserAssign",
  mixins: [ListMixin],
  data() {
    return {
      entityName: "CurrentWorkSpace",
      query: {
        $expand: "User($select=Id,RealName)",
        $select: "Id,ScheduledDate",
      },
      limitType: {
        Alert: "警戒线",
        Action: "行动限",
      },
      echartDatas: [],
      withoutPaging: true,
    };
  },
  mounted() {
    this.myEcharts();
  },
  watch: {},
  methods: {
    buildFilterStr() {
      let filterStr = [];
      filterStr.push(`UserId ne null`);

      let sDate = new Date();
      let startDate = new Date(
        sDate.getFullYear(),
        sDate.getMonth(),
        sDate.getDate(),
        0,
        0,
        0,
        0
      );
      let endDate = new Date(
        sDate.getFullYear(),
        sDate.getMonth(),
        sDate.getDate(),
        23,
        59,
        59,
        0
      );
      filterStr.push(`ScheduledDate ge ${startDate.toISOString()}`);
      filterStr.push(`ScheduledDate le ${endDate.toISOString()}`);
      
      filterStr.push(`Completed eq false`);

      // if (this.queryModel.Group && this.queryModel.Group.length > 0) {
      //   filterStr.push(`contains(Group,'${this.queryModel.Group}')`);
      // }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    setEchart() {
      //   this.echartOptions = {
      //     xAxis: {
      //       name: '用户名',
      //       type: 'category',
      //     },
      //     yAxis: {
      //       name: '数量',
      //       type: 'value'
      //     },
      //     tooltip: {
      //       trigger: "axis",
      //       position: function (pt) {
      //         return [(pt[0] - 150 > 100 ? pt[0] - 150 : 100), pt[1]];
      //       },
      //     },
      //     series: [
      //       {
      //         data: [
      //           ['juelsong', 40],
      //           [user1, 100],
      //           [user2, 20]
      //         ],
      //         type: 'bar',
      //       }
      //     ]
      //   }
      this.myChart.setOption({
        xAxis: {
          name: "用户名",
          type: "category",
        },
        yAxis: {
          name: "数量",
          type: "value",
          minInterval: 1,
        },
        tooltip: {
          trigger: "axis",
          position: function (pt) {
            return [pt[0] - 150 > 100 ? pt[0] - 150 : 100, pt[1]];
          },
        },
        series: [
          {
            data: this.echartDatas,
            type: "bar",
          },
        ],
      });
    },
    myEcharts() {
      let that = this;
      // 基于准备好的dom，初始化echarts实例
      that.myChart = echarts
        .init(document.getElementById("usersAssigns"))
        .dispose();
      that.myChart = echarts.init(document.getElementById("usersAssigns"));
      that.loadData().then(() => {
        for (let index = 0; index < that.tableData.data.length; index++) {
          const element = that.tableData.data[index];
          var hasKey = true;
          that.echartDatas.forEach((echartData) => {
            if (echartData[0] == element.User.RealName) {
              hasKey = false;
              echartData[1] += 1;
            }
          });
          if (hasKey) {
            that.echartDatas.push([element.User.RealName, 1]);
          }
        }
        that.setEchart();
      });

      // 绘制图表
    },
  },
});
</script>
