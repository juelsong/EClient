<template>
  <el-config-provider :locale="current_locale" :size="current_size">
    <router-view />
    <e-sign v-model:visible="showESign" />
  </el-config-provider>
</template>

<script lang="ts">
import { ElConfigProvider } from "element-plus";
import * as vue from "vue";
import * as vuex from "vuex";
import { locale } from "@/i18n";
import ESign from "@/components/ESign.vue";
import { oDataBatchQuery } from "@/utils/odata";

// import { TestStage, EnvironmentDef } from "@/defs/Entity";

export default vue.defineComponent({
  components: { ElConfigProvider, ESign },
  name: "App",
  setup(props, ctx) {
    // const testStages = vue.ref(new Array<TestStage>());
    // const environmentDefs = vue.ref(new Array<EnvironmentDef>());
    async function initializeConstData() {
      const batchRsp = await oDataBatchQuery({
        TestStage: {
          $filter: "IsActive eq true",
          $orderby: "Sequence",
        },
        EnvironmentDef: {
          $filter: "IsActive eq true",
          $orderby: "Sequence",
        },
      });

      // const testStageData = batchRsp[0].body.value as TestStage[];
      // const environmentDefData = batchRsp[1].body.value as EnvironmentDef[];
      // testStages.value.splice(0, testStages.value.length, ...testStageData);
      // environmentDefs.value.splice(
      //   0,
      //   environmentDefs.value.length,
      //   ...environmentDefData
      // );
    }
    const store = vuex.useStore();
    vue.watch(
      () => store.getters.userId,
      (val?: number) => {
        if (val) {
          initializeConstData();
        }
      }
    );
    // vue.provide("TestStages", testStages);
    // vue.provide("EnvironmentDefs", environmentDefs);
  },
  provide() {
    return {
      isClient: process.env.NODE_ENV === "client",
      isSyncTool: process.env.NODE_ENV === "sync",
    };
  },
  data() {
    return { showESign: false };
  },
  watch: {
    "$store.state.esign.serialNumber": {
      handler(newVal) {
        this.showESign = false;
        if (newVal) {
          this.$nextTick(() => {
            this.showESign = true;
          });
        }
      },
    },
  },
  computed: {
    current_locale() {
      return {
        name: this.$store.getters.locale,
        el: locale[this.$store.getters.locale],
      };
    },
    current_size() {
      return this.$store.getters.size;
    },
    // showESign: {
    //   get(): boolean {
    //     return this.needESign;
    //   },
    //   set(val: boolean) {
    //     this.$store.commit("esign/SET_NEED_ESIGN", val);
    //   },
    // },
    // ...mapGetters(["needESign"]),
  },
  mounted() {
    this.$i18n.locale = this.$store.getters.locale;
  },
});
</script>
