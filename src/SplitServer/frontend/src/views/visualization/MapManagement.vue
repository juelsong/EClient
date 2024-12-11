<template>
  <el-tabs v-model="activeName">
    <el-tab-pane :label="$t('Map.entity')" name="map">
      <map-list ref="mapList" />
    </el-tab-pane>
    <el-tab-pane :label="$t('MapCategory.entity')" name="mapCategory">
      <map-category />
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import MapList from "./MapList.vue";
import MapCategory from "./MapCategory.vue";

export default {
  name: "MapManagement",
  components: { MapList, MapCategory },
  data() {
    return {
      activeName: "map",
    };
  },
  watch: {
    activeName(tabName) {
      if ("map" == tabName) {
        this.$refs.mapList.loadData();
      }
    },
  },
};
</script>