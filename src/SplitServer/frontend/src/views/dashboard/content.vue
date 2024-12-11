<template>
  <el-card shadow="always" :body-style="{ height: `${height}px` }">
    <template #header>
      <div calss="dashboard-header">
        <span>
          {{
            $t(`Dashboard.component.${code}.Name`) +
            `:` +
            $t(`Dashboard.component.${code}.Description`)
          }}
        </span>
      </div>
    </template>
    <Excursions v-if="code == 'component1'" />
    <EquipmentApproching v-else-if="code == 'component3'" />
    <UserAssign v-else-if="code == 'component5'" />
    <MyWork v-else-if="code == 'component7'" />
    <span v-else>Error</span>
  </el-card>
</template>

<script lang="ts">
import * as vue from "vue";
import Excursions from "./Utilities/Excrusions.vue";
import EquipmentApproching from "./Utilities/EquipmentApproching.vue";
import UserAssign from "./Utilities/UserAssign.vue";
import MyWork from "./Utilities/MyWork.vue";

export default vue.defineComponent({
  name: "dashboardContent",
  components: { Excursions, EquipmentApproching, UserAssign, MyWork },
  props: {
    code: {
      type: String,
      required: true,
    },
    height: {
      type: Number,
      required: true,
    },
  },
});
</script>

<style>
.dashboard-content {
  background-color: yellow;
  width: 100%;
}

.dashboard-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>
