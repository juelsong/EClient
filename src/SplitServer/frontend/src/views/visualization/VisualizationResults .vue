<template>
  <div class="view-root">
    <visualizations-result
      v-show="resultVisible"
      v-model:visible="resultVisible"
    />
    <VisualizationsBase
      ref="visualizationsBase"
      :getSiteLevel="getSiteLevel"
      :getLocationLevel="getLocationLevel"
      @updateDiagramPos="updatePop"
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
          <site-result-pop-up
            v-for="item of site"
            :key="item.value"
            :testtype="item.testtype"
            :site="item.site"
            :count="item.count"
            :alertLine="item.alertValue"
            :actionLine="item.actionValue"
            :measurements="item.measurements"
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
          <room-result-pop-up
            :passCount="location.passCount"
            :alertCount="location.alertCount"
            :actionCount="location.actionCount"
            :locate="location.name"
          />
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
        chartId="maxdata"
        @close="onMaxClose"
      />
    </el-card>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, toRaw } from "vue";
import cloneDeep from "lodash.clonedeep";

import VisualizationsResult from "./VisualizationsResult.vue";
import SiteResultPopUp from "./PopUp/SiteResultPopUp.vue";
import RoomResultPopUp from "./PopUp/RoomResultPopUp.vue";
import MaxShow from "./PopUp/MaxShow.vue";
import VisualizationsBase from "./VisualizationsBase.vue";
import { DiagramPosition } from "./Diagram";

import map from "lodash.map";

import { ElForm, ElTree } from "element-plus";

declare interface IQueryModel {
  StartDate: Array<Date>;
  ApproveDate: Array<Date>;
  BarCode?: string;
  Completed?: boolean;
  Approved?: boolean;
  Product?: number | string;
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
    SiteResultPopUp,
    RoomResultPopUp,
    VisualizationsResult,
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
      ApproveDate: new Array<Date>(),
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
        alertCount: 0,
        passCount: 0,
        actionCount: 0,
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
    };
  },
  watch: {
    mapId(val?) {
      if (val) {
        this.popQuery(val);
      }
    },
  },
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

      this.queryModel = (this.$refs.visualizationsBase as any).getQueryStringArray() as IQueryModel;
      if (this.queryModel.StartDate && this.queryModel.StartDate.length > 0) {
        filterStr.push(
          `StartDate ge ${this.queryModel.StartDate[0].toISOString()}`
        );
        var setDate = Date.parse(this.queryModel.StartDate[1] as any);
        endDate.setTime(setDate + 3600 * 1000 * 24 - 1000);
        filterStr.push(`StartDate le ${endDate.toISOString()}`);
      }
      if (
        this.queryModel.ApproveDate &&
        this.queryModel.ApproveDate.length > 0
      ) {
        filterStr.push(
          `ApproveDate ge ${this.queryModel.ApproveDate[0].toISOString()}`
        );
        filterStr.push(
          `ApproveDate le ${this.queryModel.ApproveDate[1].toISOString()}`
        );
      }
      if (this.queryModel.BarCode && this.queryModel.BarCode.length > 0) {
        filterStr.push(`BarCode eq '${this.queryModel.BarCode}'`);
      }
      if (true === this.queryModel.Completed) {
        filterStr.push("Completed eq true");
      }
      if (true === this.queryModel.Approved) {
        filterStr.push("Approved eq true");
      }
      if (this.queryModel.Product) {
        if (Number.isInteger(this.queryModel.Product)) {
          filterStr.push(`Product1Id eq ${this.queryModel.Product}`);
        } else if ((this.queryModel.Product as string).length > 0) {
          filterStr.push(`Product1 eq '${this.queryModel.Product}'`);
        }
      }
      if (this.queryModel.TestType) {
        if (Number.isInteger(this.queryModel.TestType)) {
          filterStr.push(`TestTypeId eq ${this.queryModel.TestType}`);
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
          filterStr.push(`ResultsEnteredUserId eq ${this.queryModel.User}`);
        } else if ((this.queryModel.User as string).length > 0) {
          filterStr.push(`ResultsEnteredUserName eq '${this.queryModel.User}'`);
        }
      }

      return filterStr;
    },
    getLocationdata(data) {
      this.location.alertCount = 0;
      this.location.actionCount = 0;
      this.location.passCount = 0;
      if (data) {
        // this.location.name = data[0]?.LocationFullName;
        let BarCode = 0;
        for (let index = 0; index < data.length; index++) {
          if (BarCode != data[index].BarCode) {
            BarCode = data[index].BarCode;
          } else {
            continue;
          }
          switch (data[index].MostServerLimit) {
            case this.limitType.Alert:
              this.location.alertCount++;
              break;
            case this.limitType.Action:
              this.location.actionCount++;
              break;
            case "":
              this.location.passCount++;
              break;
            default:
              this.location.passCount++;
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
      var filter = this.getQueryStringArray();

      if (ids[0] == "1") {
        //Location
        this.location = {
          name: "",
          alertCount: 0,
          passCount: 0,
          actionCount: 0,
        };
        this.isLocation = true;
        this.$query("LocationV", {
          $filter: `contains(Breadcrumb,',${data.Id},')`,
          $select: "LocationId,Name",
        })
          .then((res) => {
            for (let index = 0; index < res.value.length; index++) {
              const element = res.value[index];
              if (element.LocationId == this.locationId) {
                this.location.name = element.Name;
              }
            }
            filter.push(
              `LocationId in [${res.value
                .map((i) => `${i.LocationId}`)
                .join(`,`)}]`
            );
            if (filter && filter.length > 0) {
              var qs = this.buildQueryData(filter);
              // console.log(filter);
              this.$query("RptSampleMart", {
                $select:
                  "Id,LocationId,MostServerLimit,LocationFullName,TestId,Name,SiteName,BarCode",
                $filter: qs,
                $orderby: "StartDate",
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
        filters.push(` SiteId eq ${ids[1]}`);
        this.$query("RptSampleMart", {
          $filter: this.buildQueryData(filters),
          $select:
            "Id,ActionLimitValue,Name,BarCode,AlertLimitValue,SiteName,LocationId,TestTypeName,MostServerLimit,LocationFullName,TestResultValue,StartDate",
          $orderby: "TestTypeId desc,Name,StartDate",
        }).then((data) => {
          let AllTestResults = new Array<object>();
          let singleTest = {
            testtype: "",
            count: 0,
            site: "",
            alertValue: 0,
            actionValue: 0,
            // datas: [[]],
            // colors: [] as any,
            measurements: [] as any,
          };
          var currentTestTypeName = "";
          var measurement = {
            name: "",
            datas: [[]],
            colors: [] as any,
            dataId: "chartId_" + 0,
          };
          for (let index = 0; index < data.value.length; index++) {
            if (currentTestTypeName != data.value[index].TestTypeName) {
              //？？
              if (measurement.name != "") {
                singleTest.count = measurement.datas.length;
                let imeasurementData = cloneDeep(toRaw(measurement));
                singleTest.measurements.push(imeasurementData);
                let isingleTestData = cloneDeep(toRaw(singleTest));
                AllTestResults.push(isingleTestData);
              }
              currentTestTypeName = data.value[index].TestTypeName;
              singleTest.count = 0;

              singleTest.testtype = currentTestTypeName;
              singleTest.site = data.value[index].SiteName;
              singleTest.measurements = [] as any;
              measurement = {
                name: "",
                datas: [[]],
                colors: [] as any,
                dataId: "chartId_" + data.value[index].Id,
              };
              measurement.name = data.value[index].Name;
              let single = [
                new Date(data.value[index].StartDate),
                data.value[index].TestResultValue,
              ];
              measurement.datas[0] = single as any;
            } else {
              let single = [
                new Date(data.value[index].StartDate),
                data.value[index].TestResultValue,
              ];
              if (measurement.name != data.value[index].Name) {
                singleTest.measurements.push(measurement);
                measurement = {
                  name: data.value[index].Name,
                  datas: [[]],
                  colors: [] as any,
                  dataId: "chartId_" + data.value[index].Id,
                };
                measurement.datas[0] = single as any;
              } else {
                measurement.datas.push(single as any);
              }
            }

            switch (data.value[index].MostServerLimit) {
              case this.limitType.Alert:
                measurement.colors.push(this.colorAlert);
                break;
              case this.limitType.Action:
                measurement.colors.push(this.colorAction);
                break;
              default:
                measurement.colors.push(this.colorOther);
                break;
            }

            // singleTest.count++;
          }
          measurement.datas.forEach((element) => {
            if (element.length) {
              singleTest.count++;
            }
          });
          let imeasurementData = cloneDeep(toRaw(measurement));
          singleTest.measurements.push(imeasurementData);
          let isingleTestData = cloneDeep(toRaw(singleTest));
          AllTestResults.push(isingleTestData);
          // singleTest.measurements.push(measurement)
          // singleTest.count = measurement.datas.length
          // AllTestResults[0] = singleTest
          this.site = AllTestResults;
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
      let filter = this.getQueryStringArray();
      var q = "";
      console.log(res);
      q += `LocationId in [${res.value.map((i) => i.LocationId)}]`;
      filter.push(q);
      const data = await this.$query("RptSampleMart", {
        $filter: this.buildQueryData(filter),
        $select: "MostServerLimit",
      });
      for (let index = 0; index < data.value.length; index++) {
        if (data.value[index].MostServerLimit == this.limitType.Action) {
          level = 1; //Action
          break;
        } else if (data.value[index].MostServerLimit == this.limitType.Alert) {
          level = 2; //Alert
        }
      }
      return level;
    },
    async getSiteLevel(id) {
      var level = 0; //Normal
      let filter = this.getQueryStringArray();
      filter.push(`SiteId eq ${id}`);
      if (this.buildQueryData(filter).length == 0) {
        const data = await this.$query("RptSampleMart", {
          $select: "MostServerLimit",
        });
        for (let index = 0; index < data.value.length; index++) {
          if (data.value[index].MostServerLimit == this.limitType.Action) {
            level = 1; //Action
            break;
          } else if (
            data.value[index].MostServerLimit == this.limitType.Alert
          ) {
            level = 2; //Alert
          }
        }
      } else {
        const data = await this.$query("RptSampleMart", {
          $filter: this.buildQueryData(filter),
          $select: "MostServerLimit",
        });
        for (let index = 0; index < data.value.length; index++) {
          if (data.value[index].MostServerLimit == this.limitType.Action) {
            level = 1; //Action
            break;
          } else if (
            data.value[index].MostServerLimit == this.limitType.Alert
          ) {
            level = 2; //Alert
          }
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
