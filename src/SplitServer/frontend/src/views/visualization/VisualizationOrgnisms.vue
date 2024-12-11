<template>
  <div class="view-root">
    <VisualizationsOrgnaismResult
      v-show="resultVisible"
      v-model:visible="resultVisible"
      :querys="querys"
    />
    <VisualizationsBase
      ref="visualizationsBase"
      :showOrganism="true"
      :getSiteLevel="getSiteLevel"
      :getLocationLevel="getLocationLevel"
      @updateDiagramPos="updatePop"
      @showResult="showResult"
    />

    <el-card
      v-show="popUpShow"
      class="box-card hover_con"
      v-bind:style="{
        right: popupRight + 'px',
        top: popupTop + 'px',
        width: '360px',
      }"
      @mouseenter="enter"
      @mouseleave="leave"
    >
      <el-scrollbar ref="scrollbar" max-height="330px">
        <div>{{ $t("Visualization.label.Result") }}</div>
        <el-divider />
        <!-- <room-result-pop-up />
          <site-result-pop-up testtype="top"/> -->
        <div v-show="!isLocation" style="margin-left: -20">
          <OrganismSitePopUp
            v-for="item of site"
            :key="item.value"
            :testtype="item.testtype"
            :name="item.Name"
            :count="item.Count"
            :datas="item.Datas"
            :popId="item.PopId"
            @showMaxData="cilckShowMaxData"
          />
          <!-- 
            :datas="item.datas"
            :colors="item.colors"
            :dates="item.dates"
            :datas="item.datas"
            :dates="item.dates" -->
        </div>
        <div v-show="isLocation">
          <div>{{ $t("PopData.label.TestLocate") }}:{{ location.name }}</div>
          <div>
            <el-icon color="#00D576" :size="24">
              <svg-icon icon-class="operation" style="margin-top: 7px" />
            </el-icon>
            {{ $t("PopData.label.OrganismCount") }}:{{ location.organismCount }}
            <!-- 
            <div v-for="o in location.organisms" :key="o.Name">
              {{ o.Name }}:{{ o.Count }}
            </div> -->
          </div>
        </div>
      </el-scrollbar>
    </el-card>
    <el-card
      v-show="showMaxDataVisible"
      v-bind:style="{
        height: '400px',
        width: '700px',
        position: 'fixed',
        top: '0px',
        left: '0px',
        right: '0px',
        bottom: ' 0px',
        margin: 'auto',
      }"
    >
      <max-show
        :chartOptions="showMaxDatas"
        chartId="maxOrganism"
        @close="onMaxClose"
      />
    </el-card>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, toRaw } from "vue";
import cloneDeep from "lodash.clonedeep";

import VisualizationsOrgnaismResult from "./VisualizationsOrgnaismResult.vue";
import OrganismSitePopUp from "./PopUp/OrganismSitePopUp.vue";
import MaxShow from "./PopUp/MaxShow.vue";
import VisualizationsBase from "./VisualizationsBase.vue";
import { DiagramPosition, OrganismData, ChartData } from "./Diagram";

import map from "lodash.map";

import { ElForm, ElTree } from "element-plus";

declare interface IQueryModel {
  StartDate: Array<Date>;
  BarCode?: string;
  Completed?: boolean;
  TestType?: number | string;
  Organism?: number | string;
  Location?: number | string;
  User?: number | string;
}

const dragIconWidth = 24;
const dragIconHeight = 24;
export default defineComponent({
  name: "OrganismVisualizations",
  components: {
    OrganismSitePopUp,
    VisualizationsOrgnaismResult,
    MaxShow,
    VisualizationsBase,
  },
  setup() {
    // const m: IQueryModel = {
    //   StartDate: new Array<Date>(),
    //   ApproveDate: new Array<Date>(),
    // };
    const queryModel = reactive<IQueryModel>({
      StartDate: new Array<Date>(),
    });
    return { queryModel };
  },
  data() {
    return {
      popUpShow: false,
      resultVisible: false,
      queryModalVisible: false,
      popupTop: 0,
      popupRight: 0,
      currentSymbol: undefined, // 当前选择的符号
      currentMapNode: undefined, // 地图树当前节点
      isLocation: false,
      location: {
        name: "",
        organismCount: 0,
        organisms: Array<OrganismData>(),
      },
      site: [] as any[],
      colorAlert: "#FF9B00",
      colorAction: "#FF0000",
      colorOther: "#5B8FF9",
      showMaxDataVisible: false,
      showMaxDatas: undefined,
      limitType: {
        Alert: undefined,
        Action: undefined,
      },
      locationId: undefined,
      mapId: 0,
      querys: [],
    };
  },
  watch: {
    mapId(val?) {
      if (val) {
        this.popQuery(val);
      }
    },
  },
  // computed: {
  //   queryDatas: () => {
  //     return this.getQueryStringArray();
  //   },
  // },
  mounted() {
    this.getQueryLimitType();
  },
  methods: {
    enter() {
      this.popUpShow = true;
    },
    leave() {
      this.popUpShow = false;
      this.locationId = undefined;
      this.mapId = 0;
    },
    async getQueryLimitType() {
      const data = await this.$query("LimitType", {
        $select: "Id,Name",
      });
      this.limitType.Alert = data.value[0].Name;
      this.limitType.Action = data.value[1].Name;
    },
    //获取基础筛选
    getQueryStringArray() {
      let filterStr = new Array<string>();
      var endDate = new Date();
      this.queryModel = (
        this.$refs.visualizationsBase as any
      ).getQueryStringArray() as IQueryModel;
      if (this.queryModel.StartDate && this.queryModel.StartDate.length > 0) {
        filterStr.push(
          `Sample/StartDate ge ${this.queryModel.StartDate[0].toISOString()}`
        );
        var setDate = Date.parse(this.queryModel.StartDate[1] as any);
        endDate.setTime(setDate + 3600 * 1000 * 24 - 1000);
        filterStr.push(`Sample/StartDate le ${endDate.toISOString()}`);
      }
      if (this.queryModel.TestType) {
        if (Number.isInteger(this.queryModel.TestType)) {
          filterStr.push(
            `Sample/Test/TestTypeId eq ${this.queryModel.TestType}`
          );
        } else if ((this.queryModel.TestType as string).length > 0) {
          filterStr.push(`TestTypeName eq '${this.queryModel.TestType}'`);
        }
      }
      if (this.queryModel.Location) {
        if (Number.isInteger(this.queryModel.Location)) {
          filterStr.push(`LocationId eq ${this.queryModel.Location}`);
        } else if ((this.queryModel.Location as string).length > 0) {
          filterStr.push(`LocationFullName eq '${this.queryModel.Location}'`);
        }
      }
      if (this.queryModel.User) {
        if (Number.isInteger(this.queryModel.User)) {
          filterStr.push(`CurrentWorkSpace/UserId eq ${this.queryModel.User}`);
        } else if ((this.queryModel.User as string).length > 0) {
          filterStr.push(
            `CurrentWorkSpace/UserId eq '${this.queryModel.User}'`
          );
        }
      }
      if (this.queryModel.Organism) {
        if (Number.isInteger(this.queryModel.Organism)) {
          filterStr.push(`OrganismId eq ${this.queryModel.Organism}`);
        } else if ((this.queryModel.Location as string).length > 0) {
          filterStr.push(`Organism eq '${this.queryModel.Organism}'`);
        }
      }
      return filterStr;
    },
    getLocationdata(data) {
      this.location.organismCount = 0;
      if (data) {
        // this.location.name = data[0]?.LocationFullName;
        let organismId = 0;
        for (let index = 0; index < data.length; index++) {
          let element = data[index];
          this.location.organismCount += element.Count;
          if (organismId != element.OrganismId) {
            organismId = element.OrganismId;
            this.location.organisms.push({
              Name: element.Organism.Description,
              Count: element.Count,
            });
          } else {
            this.location.organisms[this.location.organisms.length - 1].Count +=
              element.Count;
          }
        }
      }
    },
    buildQueryData(query) {
      if (query.length > 1) {
        return query.map((f) => `(${f})`).join(" and ");
      } else {
        return query.join("");
      }
    },
    popQuery(query) {
      let data = {
        Id: undefined,
      };
      let ids = query.split("_");
      data.Id = ids[1];
      var filters = this.getQueryStringArray();
      filters.push(` Count ge 0`);
      if (ids[0] == "1") {
        //Location
        this.location = {
          name: "",
          organismCount: 0,
          organisms: [],
        };
        this.isLocation = true;
        this.$query("LocationV", {
          $filter: `contains(Breadcrumb,',${data.Id},')`,
          $select: "LocationId,Name",
        })
          .then((res) => {
            for (let index = 0; index < res.value.length; index++) {
              const element = res.value[index];
              if (element.LocationId == data.Id) {
                this.location.name = element.Name;
              }
            }
            filters.push(
              `CurrentWorkSpace/LocationId in [${res.value
                .map((i) => `${i.LocationId}`)
                .join(`,`)}]`
            );
            if (filters && filters.length > 0) {
              var qs = this.buildQueryData(filters);
              // console.log(filter);
              this.$query("WorkSpaceOrganism", {
                // $select:
                //   "Id,LocationId,MostServerLimit,LocationFullName,TestId,Name,SiteName,BarCode",
                $expand: "Organism",
                $filter: qs,
                $orderby: "OrganismId",
              }).then((data) => {
                this.getLocationdata(data.value);
              });
            } else {
              this.getLocationdata(null);
            }
          })
          .catch(() => {});
      } else {
        this.isLocation = false;

        let filters = this.getQueryStringArray();
        //TestTypeId eq ${res.value[index].Id} and
        filters.push(`Sample/Test/SiteId eq ${ids[1]}`);

        this.$query("WorkSpaceOrganism", {
          $filter: this.buildQueryData(filters),
          $expand:
            "Organism($select=Description),CurrentWorkSpace($select=CompletedDate)",
          // $select:
          //   "Id,ActionLimitValue,Name,BarCode,AlertLimitValue,SiteName,LocationId,TestTypeName,MostServerLimit,LocationFullName,TestResultValue,StartDate",
          $orderby: "OrganismId,CurrentWorkSpace/CompletedDate",
        }).then((data) => {
          const organismResults = new Array<ChartData>();
          var organismId = 0;
          for (let index = 0; index < data.value.length; index++) {
            const element = data.value[index];
            if (organismId != element.OrganismId) {
              organismId = element.OrganismId;
              organismResults.push({
                Name: element.Organism.Description,
                Count: element.Count,
                Datas: [],
                PopId: "chartId_" + element.OrganismId,
              } as ChartData);
            } else {
              organismResults[organismResults.length - 1].Count +=
                element.Count;
            }
            organismResults[organismResults.length - 1].Datas.push([
              new Date(element.CreatedTime),
              element.Count,
            ]);
          }

          // singleTest.measurements.push(measurement)
          // singleTest.count = measurement.datas.length
          // AllTestResults[0] = singleTest
          this.site = organismResults;
        });
      }
    },

    async getLocationLevel(id) {
      // let query = {
      //   Id: id,
      // };
      var level = 0; //Normal
      // const res = await GetLocationInfos(query);
      const res = await this.$query("LocationV", {
        $filter: `contains(Breadcrumb,',${id},')`,
        $select: "LocationId,Name",
      });
      let filters = this.getQueryStringArray();
      filters.push(` Count ge 0`);
      var q = "";
      //console.log(res);
      q += `CurrentWorkSpace/LocationId in [${res.value.map(
        (i) => i.LocationId
      )}]`;
      filters.push(q);
      const data = await this.$query("WorkSpaceOrganism", {
        $filter: this.buildQueryData(filters),
      });
      if (data.value.length) {
        level = 1; //Action
        //level = 2; //Alert
      }
      return level;
    },
    async getSiteLevel(id) {
      var level = 0; //Normal
      let filters = this.getQueryStringArray();
      filters.push(`CurrentWorkSpace/Test/SiteId eq ${id}`);
      filters.push(` Count ge 0`);
      if (this.buildQueryData(filters).length == 0) {
        const data = await this.$query("WorkSpaceOrganism", {});
        if (data.value.length) {
          level = 1; //Action
          //level = 2; //Alert
        }
      } else {
        const data = await this.$query("WorkSpaceOrganism", {
          $filter: this.buildQueryData(filters),
        });
        if (data.value.length) {
          level = 1; //Action
          //level = 2; //Alert
        }
      }
      return level;
    },
    cilckShowMaxData(options) {
      if (options) {
        this.showMaxDatas = options;
        this.showMaxDataVisible = true;
      } else {
        this.showMaxDataVisible = false;
      }
    },
    onMaxClose() {
      this.showMaxDataVisible = false;
    },
    updatePop(data: DiagramPosition) {
      if (data.MapId) {
        this.popUpShow = true;
        this.mapId = data.MapId;
        this.popupRight = data.PopupRight;
        this.popupTop = data.PopupTop;
      } else {
        this.popUpShow = false;
      }
    },
    showResult() {
      this.querys = this.getQueryStringArray() as any;
      this.resultVisible = true;
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.header {
  background: white;
  border-radius: 4px 4px 0px 0px;
  height: 100%;
  padding: 0px 12px;
  align-content: center;
  display: flex;
  align-items: center;

  :deep(.el-form-item) {
    margin: 0px 16px 0px 0px;
  }
}

.el-card {
  height: 100%;
}

.el-aside {
  margin: 0;
  padding: 0px 6px 0px 12px;
  background: unset;
}

.el-main {
  margin: 0;
  padding: 0px 12px 0px 6px;
}

.hover_con {
  position: fixed;
  width: 220px;
  max-height: 380px;
  margin: 0px;
  padding: 0px;
}

.advanced_data {
  width: 550px;
  height: 450px;
  padding: 10px;

  border: 1px solid rgb(8, 8, 8);
  background: rgb(255, 255, 255);
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  margin: auto;
  z-index: 999;
}

.view-root {
  height: 100%;

  :deep(.el-container) {
    height: 100%;
  }

  :deep(.el-container:not(.is-vertical)) {
    margin: 12px 0 0 0;
  }

  :deep(.el-header) {
    --el-header-padding: 0;

    .el-button {
      margin-left: 8px;
    }
  }

  :deep(.el-aside) {
    padding: 0;
    margin: 0 12px 0 0;
  }

  :deep(.el-main) {
    padding: 0;
    background: white;
    border-radius: 4px;
  }
}
</style>
