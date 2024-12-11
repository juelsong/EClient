<template>
  <el-tabs class="single-tabs">
    <el-tab-pane :label="$t('Department.entity')" v-if="departmentShow">
      <department />
    </el-tab-pane>
    <el-tab-pane :label="$t('User.entity')" v-if="userShow">
      <user />
    </el-tab-pane>
    <el-tab-pane :label="$t('Role.entity')" v-if="roleShow">
      <role />
    </el-tab-pane>
    <el-tab-pane :label="$t('menu.booking')" v-if="bookingShow">
      <booking />
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import Department from "./Department.vue";
import User from "./User.vue";
import Role from "./Role.vue";
import Booking from "./Booking.vue";
import { UserPermission } from "@/defs/Types";

export default {
  name: "security",
  components: { Department, User, Role, Booking },
  data() {
    return {
      departmentShow: true,
      userShow: true,
      roleShow: true,
      bookingShow: true,
    };
  },
  mounted() {
    // const permissions = ;
    const permissions = this.$store.state.user.permissions.map((p) => p.Code);
    this.departmentShow = permissions.includes("department", 0);
    this.userShow = permissions.includes("user", 0);
    this.roleShow = permissions.includes("role", 0);
    this.bookingShow = permissions.includes("booking", 0);
  },
};
</script>

<style scoped>
.el-tabs {
  height: 100%;
}
.el-tabs >>> .el-tabs__content {
  height: calc(100% - 55px);
}
.el-tabs >>> .el-tabs__content > .el-tab-pane {
  height: 100%;
}
/* .full-tab-pane {
  height: 100%;
} */
</style>
