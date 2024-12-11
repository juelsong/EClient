<template>
  <div>
    <!-- <el-button type="primary"
               @click="onClose">
      {{ $t("template.accept") }}
    </el-button> -->
    <div :id="chartId"
         style='width: 650px; height: 300px'>
    </div>

    <el-row type="flex"
            justify="end">
      <el-button type="primary"
                 @click="onClose"
                 style="margin-bottom:10px">
        {{ $t("template.accept") }}
      </el-button>
    </el-row>
  </div>

</template>

<script>
import * as echarts from "echarts";
//import moment from "moment";
export default {
  props: {
    chartId: {
      type: String,
      default: "-",
    },
    chartOptions: {
      type: Object,
    },
  },
  data() {
    return {
      mychart: {},
    };
  },
  emits: ["close"],
  watch: {
    chartOptions() {
      this.setEchart();
    },
  },
  methods: {
    onClose() {
      this.$emit("close");
    },
    setEchart() {
      if (this.chartOptions) {
        this.myChart.setOption(this.chartOptions);
      }
    },
    myEcharts() {
      // 基于准备好的dom，初始化echarts实例
      this.myChart = echarts.init(document.getElementById(this.chartId));
      this.setEchart();
      // 绘制图表
    },
  },
  mounted() {
    this.myEcharts();
  },
};
</script>

