<template>
  <div class="dashboard-container">
    <el-scrollbar v-if="showComponent">
      <el-row>
        <el-col
          v-for="(c, idx) in profile.Dashboard?.ContentCode"
          :key="idx"
          :xs="profile.Dashboard?.XS"
          :sm="profile.Dashboard?.SM"
          :md="profile.Dashboard?.MD"
          :lg="profile.Dashboard?.LG"
          :xl="profile.Dashboard?.XL"
          :style="{ padding: `${profile.Dashboard?.Margin ?? 0}px` }"
        >
          <content :code="c" :height="profile.Dashboard?.Height!" />
        </el-col>
      </el-row>
    </el-scrollbar>
    <div class="dashboard-container-img" v-else>
      <img src="logo.svg" width="618" height="176" />
      <div class="info">欢迎使用EMIS系统</div>
    </div>
  </div>
</template>

<script lang="ts">
import { Profile } from "@/defs/Model";
import * as vue from "vue";
import Content from "./content.vue";
export default vue.defineComponent({
  name: "Dashboard",
  components: { Content },
  computed: {
    profile() {
      return this.$store.state.user.profile as Profile;
    },
    showComponent() {
      let profile = this.$store.state.user.profile as Profile;
      return (
        profile.Dashboard &&
        profile.Dashboard.ContentCode &&
        profile.Dashboard.ContentCode.length > 0
      );
    },
  },
});
</script>

<style scoped>
.info {
  font-size: 60px;
  font-weight: 700;
  color: rgb(96, 96, 96);
}
.dashboard-container {
  height: 100%;
  overflow-x: hidden;
}
.dashboard-container-img {
  text-align: center;
  margin-top: 20%;
}
</style>
