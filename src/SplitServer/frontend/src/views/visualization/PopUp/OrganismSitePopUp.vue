<template>
  <div>{{ $t("PopData.label.OrganismName") }}:{{ name }}</div>
  <div>{{ $t("PopData.label.OrganismCount") }}:{{ count }}</div>
  <!-- <el-row type="flex"
          justify="end">
    <el-button @click="max"
               size="mini"
               style="margin-right:20px">
    </el-button>
  </el-row> -->
  <div :id="popId" style="width: 300px; height: 200px"></div>
</template>

<script>
import * as echarts from "echarts";
//import moment from "moment";

export default {
  props: {
    popId: {
      type: String,
      default: "",
    },
    name: {
      type: String,
      default: "",
    },
    count: {
      type: String,
      default: "",
    },
    datas: {
      type: Array,
    },
    dates: {
      type: [Array],
    },
    colors: {
      type: Array,
    },
  },
  emits: ["showMaxData"],
  data() {
    return {
      mychart: {},
      echartOptions: undefined,
    };
  },

  watch: {
    datas() {
      this.setEchart();
    },
  },
  methods: {
    max() {
      debugger;
    },
    setEchart() {
      let that = this;
      this.echartOptions = {
        // color: ["#FF9B00", "#FF0000"],
        legend: {
          itemHeight: 10,
        },
        tooltip: {
          trigger: "axis",
          position: function (pt) {
            return [pt[0] - 150 > 100 ? pt[0] - 150 : 100, pt[1]];
          },
        },
        dataZoom: [
          {
            type: "inside", //1平移 缩放
            throttle: "50", //设置触发视图刷新的频率。单位为毫秒（ms）。
            minValueSpan: 6, //用于限制窗口大小的最小值,在类目轴上可以设置为 5 表示 5 个类目
            start: 0, //数据窗口范围的起始百分比 范围是：0 ~ 100。表示 0% ~ 100%。
            end: 100, //数据窗口范围的结束百分比。范围是：0 ~ 100。
          },
        ],
        xAxis: {
          type: "time",
          name: this.$t("PopData.label.Time"),
          nameTextStyle: {
            lineHeight: -30,
            verticalAlign: "bottom",
            padding: [0, 0, 0, -10],
          },
        },
        yAxis: {
          type: "value",
          name: this.$t("PopData.label.Value"),
          nameGap: 30,
          nameTextStyle: {
            verticalAlign: "bottom",
          },
          axisLabel: {
            inside: false, // 标签刻度朝内
            margin: -10, // 清除默认间距
            padding: [0, 10, 0, 0], // 右边内边距10
            textStyle: {
              align: "right", // 文字右对齐
            },
          },
        },
        series: [
          {
            name: this.$t("PopData.label.TestResult"),
            color:"#4285F4",
            data: this.datas,
            type: "line",
            colorBy: "index",
            // color: this.colors,
            symbol: "emptycircle",
            symbolSize: 10,
          },
        ],
      };
      this.myChart.setOption({
        // color: ["#FF9B00", "#FF0000"],
        legend: {
          itemHeight: 10,
        },
        tooltip: {
          trigger: "axis",
          position: function (pt) {
            return [pt[0] - 150 > 100 ? pt[0] - 150 : 100, pt[1]];
          },
        },
        toolbox: {
          show: true,
          feature: {
            myTool1: {
              show: true,
              title: this.$t("PopData.label.Restore"),
              icon: "path://M10 4c-.8-1.1-2-2.5-4.1-2.5-2.5 0-4.4 2-4.4 4.5s2 4.5 4.4 4.5c1.3 0 2.5-.6 3.3-1.5m1.3-7.5V4c0 .3-.2.5-.5.5H7.5",
              onclick: function () {
                that.setEchart();
              },
            },
            myTool2: {
              show: true,
              title: this.$t("PopData.label.MaxShow"),
              icon: "path://M432.45,595.444c0,2.177-4.661,6.82-11.305,6.82c-6.475,0-11.306-4.567-11.306-6.82s4.852-6.812,11.306-6.812C427.841,588.632,432.452,593.191,432.45,595.444L432.45,595.444z M421.155,589.876c-3.009,0-5.448,2.495-5.448,5.572s2.439,5.572,5.448,5.572c3.01,0,5.449-2.495,5.449-5.572C426.604,592.371,424.165,589.876,421.155,589.876L421.155,589.876z M421.146,591.891c-1.916,0-3.47,1.589-3.47,3.549c0,1.959,1.554,3.548,3.47,3.548s3.469-1.589,3.469-3.548C424.614,593.479,423.062,591.891,421.146,591.891L421.146,591.891zM421.146,591.891",
              onclick: function () {
                //TODO
                that.$emit("showMaxData", that.echartOptions);
              },
            },
          },
        },
        dataZoom: [
          {
            type: "inside", //1平移 缩放
            throttle: "50", //设置触发视图刷新的频率。单位为毫秒（ms）。
            minValueSpan: 6, //用于限制窗口大小的最小值,在类目轴上可以设置为 5 表示 5 个类目
            start: 0, //数据窗口范围的起始百分比 范围是：0 ~ 100。表示 0% ~ 100%。
            end: 100, //数据窗口范围的结束百分比。范围是：0 ~ 100。
          },
        ],
        xAxis: {
          type: "time",
          name: this.$t("PopData.label.Time"),
          nameTextStyle: {
            lineHeight: -30,
            verticalAlign: "bottom",
            padding: [0, 0, 0, -10],
          },
        },
        yAxis: {
          type: "value",
          name: this.$t("PopData.label.Value"),
          nameGap: 30,
          nameTextStyle: {
            verticalAlign: "bottom",
          },
          axisLabel: {
            inside: false, // 标签刻度朝内
            margin: -10, // 清除默认间距
            padding: [0, 10, 0, 0], // 右边内边距10
            textStyle: {
              align: "right", // 文字右对齐
            },
          },
        },
        series: [
          {
            name: this.$t("PopData.label.TestResult"),
            data: this.datas,
            type: "line",
            colorBy: "index",
            // color: this.colors,
            symbol: "emptycircle",
            symbolSize: 10,
          },
        ],
      });
    },
    myEcharts() {
      // 基于准备好的dom，初始化echarts实例
      let id = this.popId == "" ? "testp" : this.popId;
      this.myChart = echarts.init(document.getElementById(id)).dispose();
      this.myChart = echarts.init(document.getElementById(id));
      this.setEchart();
      // 绘制图表
    },
  },
  mounted() {
    this.myEcharts();
  },
};
</script>
