<template>
  <div>{{ $t("PopData.label.TestLocate") }}:{{ locate }}</div>
  <div>{{ $t("PopData.label.TestCount") }}:{{ passCount + alertCount + actionCount }}</div>
  <div>
    <el-icon color="#00D576" :size="24">
      <svg-icon icon-class="testresult" />
    </el-icon>
    {{ $t("PopData.label.PassCount") }}:{{ passCount }}
  </div>
  <div>
    <el-icon color="#FF9B00" :size="24">
      <svg-icon icon-class="testresult" />
    </el-icon>
    {{ $t("PopData.label.AlertCount") }}:{{ alertCount }}
  </div>
  <div>
    <el-icon color="#FF0000" :size="24">
      <svg-icon icon-class="testresult" />
    </el-icon>
    {{ $t("PopData.label.ActionCount") }}:{{ actionCount }}
  </div>
</template>

<script>
export default {
  name: "Echarts",
  props: {
    locate: {
      type: String,
      default: "",
    },
    passCount: {
      type: Number,
      default: 0,
    },
    alertCount: {
      type: Number,
      default: 0,
    },
    actionCount: {
      type: Number,
      default: 0,
    },
    iconName: {
      type: String,
      default: "",
    },
  },
  data() {
    return {};
  },
  methods: {},
  mounted() {},
};
</script>

