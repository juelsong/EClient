<template>
  <div style="height: 100%">
    <div class="subCard left">
      <el-card
        class="single-box-card is-always-shadow"
        :body-style="{ height: '100%' }"
      >
        <el-tabs class="single-tabs">
          <el-tab-pane :label="$t('OrganismType.entity')">
            <el-container>
              <el-main>
                <organism-type @selectOrganismType="onOrganismChange" />
              </el-main>
            </el-container>
          </el-tab-pane>
          <el-tab-pane :label="$t('OrgFoundMethod.entity')">
            <el-container>
              <el-main>
                <org-found-method />
              </el-main>
            </el-container>
          </el-tab-pane>
        </el-tabs>
      </el-card>
    </div>

    <div class="subCard right">
      <el-card
        class="single-box-card is-always-shadow"
        :body-style="{ height: '100%' }"
      >
        <el-tabs class="single-tabs">
          <el-tab-pane :label="$t('Organism.entity')">
            <el-container>
              <el-main>
                <organism ref="compOrganism" />
              </el-main>
            </el-container>
          </el-tab-pane>
          <el-tab-pane :label="$t('OrganismChar.entity')">
            <el-container>
              <el-main>
                <organism-char ref="compOrganismChar" />
              </el-main>
            </el-container>
          </el-tab-pane>
          <el-tab-pane :label="$t('OrganismGenus.entity')">
            <el-container>
              <el-main>
                <organism-genus ref="compOrganismGenus" />
              </el-main>
            </el-container>
          </el-tab-pane>
        </el-tabs>
      </el-card>
    </div>
  </div>
</template>

<script>
import OrganismType from "./OrganismType.vue";
import OrganismChar from "./OrganismChar.vue";
import OrganismGenus from "./OrganismGenus.vue";
import OrgFoundMethod from "./OrgFoundMethod.vue";
import Organism from "./Organism.vue";
import { AppMainMixin } from "@/mixins/AppMainMixin";
export default {
  name: "MicroOrganism",
  components: {
    OrganismType,
    Organism,
    OrganismChar,
    OrganismGenus,
    OrgFoundMethod,
  },
  mixins: [AppMainMixin],
  methods: {
    onOrganismChange(id) {
      this.$refs.compOrganism.searchOrganismType(id);
      this.$refs.compOrganismChar.searchOrganismType(id);
      this.$refs.compOrganismGenus.searchOrganismType(id);
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
