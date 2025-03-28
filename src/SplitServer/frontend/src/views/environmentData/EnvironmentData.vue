<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ stageName }}</span>
        </div>
      </template>
      <el-container>
        <el-header>
          <el-form ref="queryForm" :inline="true" :model="queryModel">
            <el-form-item label="时间" prop="startDate">
              <el-date-picker
                v-model="queryModel.startDate"
                unlink-panels
                type="daterange"
                align="right"
                :shortcuts="pickerOptions"
                :range-separator="$t('label.dateRange.separator')"
                :start-placeholder="$t('label.dateRange.start')"
                :end-placeholder="$t('label.dateRange.end')"
              ></el-date-picker>
            </el-form-item>
            <!-- <el-form-item label="设备Id" prop="equipmentId">
              <el-input v-model="queryModel.equipmentId" />
            </el-form-item> -->
            <el-form-item label="筛选类型" prop="filterIsAll">
              <el-select v-model="queryModel.filterIsAll" placeholder="请选择">
                <el-option
                  v-for="item in options"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="设备Id或名称" prop="equipmentIdAndName">
              <el-autocomplete
                v-model="queryModel.equipmentIdAndName"
                :fetch-suggestions="querySearchAsync"
                placeholder="请输入内容"
                @select="selectDeviceId"
              >
                <template #suffix>
                  <i class="el-icon-edit el-input__icon"></i>
                </template>
                <template #default="{ item }">
                  <div
                    style="
                      display: flex;
                      width: 300px;
                      justify-content: space-between;
                    "
                  >
                    <div
                      class="name"
                      style="
                        width: 45%;
                        overflow: hidden;
                        text-overflow: ellipsis;
                      "
                    >
                      {{ item.DeviceId }}
                    </div>
                    <span
                      class="addr"
                      style="
                        width: 45%;
                        overflow: hidden;
                        text-overflow: ellipsis;
                      "
                    >
                      {{ item.DeviceName }}
                    </span>
                  </div>
                </template>
              </el-autocomplete>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="loadData">
                {{ $t("template.search") }}
                <el-icon class="el-icon--right">
                  <svg-icon icon-class="edit" />
                </el-icon>
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onResetClick">
                {{ $t("template.reset") }}
                <el-icon class="el-icon--right">
                  <svg-icon icon-class="refresh" />
                </el-icon>
              </el-button>
            </el-form-item>
            <!-- <el-form-item>
              <el-button type="primary" @click="queryVisible = true">
                {{ $t("template.advanced") }}
                <el-icon class="el-icon--right">
                  <svg-icon icon-class="operation" />
                </el-icon>
              </el-button>
            </el-form-item> -->
          </el-form>
        </el-header>
        <el-main style="margin-top: 10px">
          <el-table
            :key="queryModel.filterIsAll"
            ref="dataRef"
            :always="true"
            highlight-current-row
            :data="tableData"
            :border="true"
            stripe
            height="100%"
            max-height="100%"
            width="100%"
            @sort-change="onSortChange"
          >
            <el-table-column
              prop="EquipmentId"
              label="设备Id"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Date"
              label="时间"
              sortable="custom"
              width="180"
              :formatter="datetimeFormat"
            />
            <el-table-column
              prop="ParticulateMatterAvg"
              label="颗粒物"
              sortable="custom"
              width="150"
            />
            <!-- <el-table-column
              prop="pm2_5_average"
              v-if="queryModel.filterIsAll"
              label="PM2.5"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="pm10_average"
              v-if="queryModel.filterIsAll"
              label="PM10"
              sortable="custom"
              width="150"
            /> -->
            <el-table-column
              prop="CPMAvg"
              label="CPM"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="NoiseAvg"
              label="噪音"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="WindDirectionAvg"
              v-if="queryModel.filterIsAll"
              label="风向"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="WindSpeedAvg"
              v-if="queryModel.filterIsAll"
              label="风速"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="TemperatureAvg"
              v-if="queryModel.filterIsAll"
              label="温度"
              sortable="custom"
              width="150"
            />
            <el-table-column
              v-if="queryModel.filterIsAll"
              prop="HumidityAvg"
              label="湿度"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="AirPressureAvg"
              v-if="queryModel.filterIsAll"
              label="大气压"
              sortable="custom"
              width="150"
            />
          </el-table>
        </el-main>
        <!-- <el-footer>
          <el-pagination
            class="table-pagination"
            v-model:currentPage="pageInfo.current"
            v-model:pageSize="pageInfo.pageSize"
            :page-sizes="[10, 20, 50]"
            layout="total, prev, pager, next, sizes, jumper"
            :total="pageInfo.total"
            :pager-count="5"
          ></el-pagination>
        </el-footer> -->
      </el-container>
    </el-card>
  </div>
</template>

<script lang="ts">
import * as vue from "vue";
import map from "lodash.map";
import VueBarcode from "vue3-barcode";
import EquipmentEditor from "../EditModal/EquipmentEditor.vue";
import { datetimeFormat } from "@/utils/formatter";
import moment from "moment";
import cloneDeep from "lodash.clonedeep";
import { toRaw } from "vue";
import {
  Equipment,
  EnvironmentalSensorDaily,
  EnvironmentalSensorMinute,
} from "@/defs/Entity";
import { getActiveEntityMixin } from "@/utils/tableMixin";
import { EnvironmentQueryModel } from "./QueryModel";

import ODataSelector from "@/components/ODataSelector.vue";
import { oDataQuery } from "@/utils/odata";
import { ElForm, ElLoading, ElMessage } from "element-plus";

// import ODataSelector from "@/components/ODataSelector.vue";

export default vue.defineComponent({
  name: "EnvironmentBaseData",
  props: {
    stage: {
      type: Number,
      default: undefined,
    },
    stageName: {
      type: String,
      default: "分钟数据",
    },
    stageQuery: {
      type: String,
      default: "EnvironmentalSensorMinute",
    },
  },
  setup(props) {
    const {
      entityName,
      filterBuilder,
      loadData,
      onSortChange,
      pageInfo,
      query,
      tableData,
      withoutPaging,
      queryModalVisible,
    } = getActiveEntityMixin<EnvironmentalSensorMinute>();
    withoutPaging.value = true;
    query.$orderby =  "Id desc";

    const queryForm = vue.ref<typeof ElForm>();
    const queryModel = vue.ref<EnvironmentQueryModel>({
      filterIsAll: false,
    });

    const dataRef = vue.ref<typeof ODataSelector>();
    const options = [
      {
        value: false,
        label: "导出主要参数(扬尘噪音)",
      },
      {
        value: true,
        label: "导出全部参数",
      },
    ];

    var pickerOptions = [
      {
        text: "昨天",
        value: () => {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24);
          return [start, end];
        },
      },
      {
        text: "今天",
        value: () => {
          let sDate = new Date();
          let start = new Date(
            sDate.getFullYear(),
            sDate.getMonth(),
            sDate.getDate(),
            0,
            0,  
            0,
            0
          );
          let end = new Date(
            sDate.getFullYear(),
            sDate.getMonth(),
            sDate.getDate(),
            23,
            59,
            59,
            0
          );
          return [start, end];
        },
      },
      {
        text: "最近7天",
        value: () => {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
          return [start, end];
        },
      },
      {
        text: "最近30天",
        value: () => {
          const end = new Date();
          const start = new Date();
          start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
          return [start, end];
        },
      },
    ];
    // 虽然列表中没用到环境，没确认所有弹窗是否使用到，因此保留环境的导航
    //  const defaultExpand = ``;
    //  query.$expand = defaultExpand;
    // query.$select = `Id,DeviceIdNode,PM2_5Average,PM10Average,TemperatureAverage,WindSpeedAverage`;
    entityName.value = props.stageQuery; //"EnvironmentalSensorMinute";

    filterBuilder.value = () => {
      return new Promise(async (resolve) => {
        let filterStr = new Array<string>();
        if (
          queryModel.value.startDate &&
          Array.isArray(queryModel.value.startDate)
        ) {
          var endDate = new Date();
          const values = queryModel.value.startDate as string[];
          // if (values.length > 1) {
          //   filterStr.push(`EndDate ge ${moment(values[0]).toISOString()}`);
          //   filterStr.push(
          //     `EndDate le ${moment(values[0]).add(1, "day").toISOString()}`
          //   );
          // }
          filterStr.push(`Date ge ${moment(values[0]).toISOString()}`);
          var setDate = Date.parse(values[1]);
          endDate.setTime(setDate + 3600 * 1000 * 24 - 1000);
          filterStr.push(`Date le ${endDate.toISOString()}`);
        } else {
          ElMessage.error("请先输入时间");
        }
        if (!queryModel.value?.filterIsAll) {
          query.$select = `Id,Date,EquipmentId, CPMAvg,NoiseAvg,ParticulateMatterAvg`;
        } else {
          query.$select = `Id,Date,EquipmentId, CPMAvg,NoiseAvg,ParticulateMatterAvg,pm2_5_average,pm10_average,TemperatureAvg,WindSpeedAvg,WindDirectionAvg,HumidityAvg,AirPressureAvg`;
        }
        if (queryModel.value?.equipmentId) {
          // oDataQuery("Equipment", {});

          // const equipmentEntities = (
          //   await oDataQuery("EquipmentMN", {
          //     $select: "EntityId, Belongs, Status",
          //     $filter: `(contains(SerialNumber, '${queryModel.value?.equipmentId}')) or (contains(Name, '${queryModel.value?.equipmentId}')) `,
          //     // $expand: "ApprovalItems",
          //   })
          // ).value as Partial<Equipment>[];
          // if (equipmentEntities.length > 0) {
          //   let equipmentIds = equipmentEntities.map((e) => e.Id).join(",");
          // }
          filterStr.push(`EquipmentId eq ${queryModel.value?.equipmentId}`);
        }

        resolve(filterStr);
      });
    };

    function onResetClick() {
      queryForm.value?.clearValidate();
      queryModel.value = {};
      loadData();
    }

    function querySearchAsync(queryString, cb) {
      if (!queryModel.value?.equipmentIdAndName) {
        oDataQuery("MonitorDevice", {
          $select: "DeviceId, DeviceName",
        }).then((res) => {
          cb(res.value.map((e) => e));
        });
      } else {
        oDataQuery("MonitorDevice", {
          $select: "DeviceId, DeviceName",
          $filter: `contains(DeviceId, '${queryModel.value.equipmentIdAndName}') or contains(DeviceName, '${queryModel.value.equipmentIdAndName}') `,
        }).then((res) => {
          cb(res.value.map((e) => e));
        });
      }
    }

    function selectDeviceId(ev) {
      queryModel.value.equipmentId = ev.DeviceId;
      queryModel.value.equipmentIdAndName = ev.DeviceId + "_" + ev.DeviceName;
    }
    //关闭自动加载
    // vue.onMounted(loadData);
    // vue.onMounted(() => {
    //   vue.nextTick(() => {
    //     dataRef.value?.loadData();
    //   });
    // });
    // vue.watch(() => tableData.value, showResult, { deep: true });

    return {
      pickerOptions,
      options,
      dataRef,
      tableData,
      filterBuilder,
      queryModel,
      pageInfo,
      queryModalVisible,
      queryForm,
      loadData,
      onResetClick,
      datetimeFormat,
      onSortChange,
      querySearchAsync,
      selectDeviceId,
    };
  },
  data() {
    return {
      value1: "",
      value2: "",
    };
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.table-pagination {
  float: right;
  margin-top: 16px;
}
.el-card {
  height: calc(/*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding);
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.sub-table {
  height: calc(100vh - 84px - 2 * $--app-main-padding - 96px);
}
.button-set {
  width: 90%;
}

.table-pagination {
  float: right;
  margin-top: 16px;
}

.el-card {
  height: calc(/*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding);
  margin: -16px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.sub-table {
  height: calc(84px + 35 * $--app-main-padding);
  margin-top: 20px;
}
</style>

<style>
.el-tabs {
  height: 100%;
}
.el-descriptions__body > table {
  table-layout: fixed;
}
.barcode-father {
  position: relative;
  height: 160px;
}
.barcode-show {
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0%);
}
</style>
