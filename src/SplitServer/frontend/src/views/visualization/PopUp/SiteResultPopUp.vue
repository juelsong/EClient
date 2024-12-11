<template>
  <div>{{ $t("PopData.label.TestType") }}:{{ testtype }}</div>
  <div>{{ $t("PopData.label.TestLocate") }}:{{ site }}</div>
  <div>{{ $t("PopData.label.TestCount") }}:{{ count }}</div>
  <!-- <el-row type="flex"
          justify="end">
    <el-button @click="max"
               size="mini"
               style="margin-right:20px">
    </el-button>
  </el-row> -->
  <div v-for="item of measurements"
       :key="item.value">
    <site-measure-pop-up :popId="item.dataId"
                         :datas="item.datas"
                         :colors="item.colors"
                         :dates="item.dates"
                         :name="item.name"
                         @showMaxData="cilckShowMaxData"
                         style="width: 300px; height: 200px" />
  </div>
</template>

<script>
import SiteMeasurePopUp from "./SiteMeasurePopUp.vue";

export default {
  name: "PopSites",
  props: {
    testtype: {
      type: String,
      default: "",
    },
    chartId: {
      type: Number,
      default: 0,
    },
    site: {
      type: String,
      default: "",
    },
    count: {
      type: Number,
      default: 0,
    },
    alertLine: {
      type: Number,
      default: 0,
    },
    actionLine: {
      type: Number,
      default: 0,
    },
    // datas: {
    //   type: Array,
    // },
    // dates: {
    //   type: [Array],
    // },
    // colors: {
    //   type: Array,
    // },
    measurements: {
      type: Array,
    },
  },
  components: {
    SiteMeasurePopUp
  },
  emits: ["showMaxData"],
  methods: {
    cilckShowMaxData(data) {
      this.$emit("showMaxData", data);
    }
  }

};
</script>

