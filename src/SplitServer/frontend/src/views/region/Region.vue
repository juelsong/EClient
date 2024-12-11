<template>
  <el-tabs v-model="activeName" class="single-tabs">
    <el-tab-pane :label="$t('Location.entity')" name="location">
      <location />
    </el-tab-pane>
    <el-tab-pane :label="$t('LocationType.entity')" name="locationType">
      <location-type />
    </el-tab-pane>
    <el-tab-pane :label="$t('SiteType.entity')" name="siteType">
      <site-type />
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import LocationType from "./LocationType.vue";
import Location from "./Location.vue";
import SiteType from "./SiteType.vue";

export default {
  name: "region",
  components: { Location, LocationType, SiteType },
  data() {
    return {
      activeName: "location",
    };
  },
};
</script>
