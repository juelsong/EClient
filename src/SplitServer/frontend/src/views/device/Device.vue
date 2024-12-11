<template>
  <div style="height: 100%">
    <div class="subCard left">
      <EquipmentType @selectEquipmentType="onEquipmentChange" />
    </div>
    <div class="subCard right">
      <Equipment ref="compEquipment" />
    </div>
  </div>
</template>

<script>
import EquipmentType from "./EquipmentType.vue";
import Equipment from "./Equipment.vue";
import { AppMainMixin } from "@/mixins/AppMainMixin";
export default {
  name: "Device",
  components: { EquipmentType, Equipment },
  mixins: [AppMainMixin],
  methods: {
    onEquipmentChange(id) {
      let compEquipment = this.$refs.compEquipment;
      compEquipment.searchEquipmentType(id);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
$subCardLeftWidth: 670px;
$subCardLeftMargin: 18px;
.subCard {
  height: 100%;
}
.subCard.left {
  float: left;
  width: $subCardLeftWidth;
}
.subCard.right {
  float: right;
  width: calc(100% - $subCardLeftWidth - $subCardLeftMargin);
}
</style>
