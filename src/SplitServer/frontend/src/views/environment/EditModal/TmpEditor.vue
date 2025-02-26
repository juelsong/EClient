<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :show-close="false"
    :title="`设备维护`"
    width="600px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button
          @click="handleTempSave"
          v-show="
            equipmentTMP.EndDate == `0001-01-01T00:00:00Z` ||
            equipmentTMP.EndDate == null
          "
        >
          暂存
        </el-button>
        <el-button v-no-more-click v-show="active > 1" @click="pre">
          上一步
        </el-button>
        <el-button v-show="active < 3" :disabled="disableNext" @click="next">
          下一步
        </el-button>
        <!-- <el-button v-no-more-click v-show="active == 3" @click="exportPdf">
          导出
        </el-button> -->
        <el-button @click="onCancelClick">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button
          type="primary"
          :disabled="accecptDisabled"
          @click="onAcceptClick"
        >
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <div class="steps-container">
      <el-steps :active="active" finish-status="success">
        <el-step title="第一步" />
        <el-step title="第二步" />
        <el-step title="第三步" />
      </el-steps>
    </div>
    <div class="remaining-time">
      剩余时间:
      <span class="time" :style="{ opacity: opacityValue }">
        {{
          Math.floor(remainingTime / 60)
            .toString()
            .padStart(2, "0")
        }}:
        {{ (remainingTime % 60).toString().padStart(2, "0") }}
      </span>
    </div>
    <el-form
      ref="dataForm"
      :model="equipmentTMP"
      :rules="rules"
      label-position="left"
      label-width="100px"
      style="width: 500px; margin-left: 50px"
    >
      <div v-show="active == 1">
        <div class="info-title">
          <span>扬尘在线监测系统运行维护记录表</span>
        </div>
        <!-- <div class="info-container">
          <span>运行维护单位:</span>
          <span>上海龙涤环保技术工程有限公司</span>
          <span>编号:</span>
          <span>LDHB-64505</span>
          <span>站点名称:</span>
          <span>静安区灵石社区N070402单元081b-03地块租赁住房项目</span>
          <span>维护日期:</span>
          <span>2024-11-01</span>
          <span>开始维护时间:</span>
          <span>14:30</span>
          <span>结束维护时间:</span>
          <span>14:45</span>
          <span>检查人:</span>
          <span>周义华</span>
        </div> -->
        <el-form-item label="电力系统" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">供应状态</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.PowerSupplyIsError"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.PowerSupplyIsError"
                v-model="equipmentTMP.PowerSupplyMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="噪音系统" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">采样头 /链接线路</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.SampleIsNormal"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.SampleIsNormal"
                v-model="equipmentTMP.SampleMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="加热除湿" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">加热除湿测温结果</span>
            </el-col>
            <el-col :span="4">
              <el-input
                class="align-right"
                v-model="equipmentTMP.TemperatureData"
                placeholder="温度℃"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-model="equipmentTMP.TemperatureMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="扬尘系统" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">仪器线路、采样泵、校准系统是否正常</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.DeviceIsError"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.DeviceIsError"
                v-model="equipmentTMP.DeviceMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">仪器滤罐是否需更换</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.ResetEquipment"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.ResetEquipment"
                v-model="equipmentTMP.ResetEquipmentMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="气象参数" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">
                温度、湿度、大气压、风速、风向是否正常
              </span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.EnvironmentIsError"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.EnvironmentIsError"
                v-model="equipmentTMP.EnvironmentMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="数据采集传输" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">数据传输是否正常</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.DataTransmissionIsError"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.DataTransmissionIsError"
                v-model="equipmentTMP.DataTransmissionMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">视频监控是否正常</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="equipmentTMP.VedioIsError"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="不正常"
                inactive-text="正常"
              />
            </el-col>
            <el-col :span="12">
              <el-input
                class="align-right"
                v-show="equipmentTMP.VedioIsError"
                v-model="equipmentTMP.VedioMsg"
                placeholder="处理方法"
              />
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="设备现场位置" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="equipmentTMP.DeviceLocation"
              placeholder="设备现场位置"
            />
          </el-row>
        </el-form-item>
      </div>
      <div v-show="active == 2">
        <div class="info-title">
          <span>C.1 流量校准记录表</span>
        </div>
        <!-- <div class="info-container">
          <span>工地名称:</span>
          <span>静安区灵石社区N070402单元081b-03地块租赁住房项目</span>
          <span>仪器名称:</span>
          <span>朗亿粉尘仪</span>
          <span>仪器编号:</span>
          <span>测试编号</span>
          <span>校准时间:</span>
          <span>2024-11-01 14:30:00</span>
          <span>检查单位:</span>
          <span>上海龙涤环保技术工程有限公司</span>
          <span>检查人:</span>
          <span>周义华</span>
        </div> -->
        <!-- <el-form-item label="使用单位名称" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="equipmentTMP.remark"
              placeholder="单位名称"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="工程报建号" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="equipmentTMP.remark"
              placeholder="工程报建号"
            />
          </el-row>
        </el-form-item> -->
        <el-form-item label="校准前" class="form-lable">
          <!-- <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">设定流量 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.remark"
                placeholder="设定流量"
              />
            </el-col>
          </el-row> -->
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量1 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BeforeFlow1"
                @change="setC1Data"
                placeholder="实测流量1"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量2 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BeforeFlow2"
                @change="setC1Data"
                placeholder="实测流量2"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量3 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BeforeFlow3"
                @change="setC1Data"
                placeholder="实测流量3"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差 %</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                :readonly="true"
                v-model="equipmentTMP.BeforeRelativeError"
                placeholder="相对误差"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否需要调整流量</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="equipmentTMP.BeforeSetFlow"
                class="ml-2"
                width="55px"
                inline-prompt
                :disabled="true"
                active-text="是"
                inactive-text="否"
              />
            </el-col>
            <el-col :span="10" />
          </el-row>
        </el-form-item>
        <el-form-item
          label="校准后"
          class="form-lable"
          v-show="equipmentTMP.BeforeSetFlow"
        >
          <!-- <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">设定流量 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.remark"
                placeholder="设定流量"
              />
            </el-col>
          </el-row> -->
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量1 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BehindFlow1"
                @change="setC1Data"
                placeholder="实测流量1"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量2 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BehindFlow2"
                @change="setC1Data"
                placeholder="实测流量2"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量3 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BehindFlow3"
                @change="setC1Data"
                placeholder="实测流量3"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差 %</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                :readonly="true"
                v-model="equipmentTMP.BehindRelativeError"
                placeholder="相对误差"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否需要调整流量</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="equipmentTMP.BehindSetFlow"
                class="ml-2"
                :disabled="true"
                width="55px"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-col>
            <el-col :span="10" />
          </el-row>
        </el-form-item>
        <el-form-item
          v-show="equipmentTMP.BeforeSetFlow || equipmentTMP.BehindSetFlow"
          label="是否更换备机"
          class="form-lable"
        >
          <el-input
            class="align-right"
            v-model="equipmentTMP.ResetC1Equipment"
            placeholder="是否更换备机"
          />
        </el-form-item>
        <el-form-item label="备注信息" class="form-lable">
          <el-input
            class="align-right"
            v-model="equipmentTMP.DescriptionC1"
            placeholder="备注信息"
          />
        </el-form-item>
      </div>
      <div v-show="active == 3">
        <div class="info-title">
          <span>C.2 颗粒物监测仪校准记录表</span>
        </div>
        <!-- <div class="info-container">
          <span>工地名称:</span>
          <span>静安区灵石社区N070402单元081b-03地块租赁住房项目</span>
          <span>仪器名称:</span>
          <span>朗亿粉尘仪</span>
          <span>仪器编号:</span>
          <span>测试编号</span>
          <span>校准时间:</span>
          <span>2024-11-01 14:30:00</span>
          <span>检查单位:</span>
          <span>上海龙涤环保技术工程有限公司</span>
          <span>检查人:</span>
          <span>周义华</span>
        </div> -->
        <!-- <el-form-item label="使用单位名称" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="equipmentTMP.remark"
              placeholder="单位名称"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="工程报建号" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="equipmentTMP.remark"
              placeholder="工程报建号"
            />
          </el-row>
        </el-form-item> -->
        <el-form-item label="校零" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">仪器零值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                :readonly="true"
                v-model="equipmentTMP.EquipmentZero"
                placeholder="仪器零值"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.EquipmentReal1"
                placeholder="实测值1"
              />
            </el-col>
          </el-row>
          <!--           <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测值2 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.EquipmentReal2"
                placeholder="实测值2"
              />
            </el-col>
          </el-row> -->
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差 %</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.EquipmentRelativeError"
                placeholder="相对误差"
              />
            </el-col>
          </el-row>
          <!-- <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">校零</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.CalibrationZero"
                placeholder="校零"
              />
            </el-col>
          </el-row> -->
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否合格</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="equipmentTMP.BeforeEquipmentQualified"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-col>
            <el-col :span="10" />
          </el-row>
        </el-form-item>
        <el-form-item label="校标" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">仪器跨度值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.EquipmentSpan"
                :readonly="true"
                placeholder="仪器跨度值"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.EquipmentReal3"
                placeholder="实测值"
                @change="setC2Data"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差 %</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                :readonly="true"
                v-model="equipmentTMP.BehindEquipmentRelativeError"
                placeholder="相对误差"
              />
            </el-col>
          </el-row>
          <!-- <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">校标</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="equipmentTMP.BehindCalibration"
                placeholder="校标"
              />
            </el-col>
          </el-row> -->
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否合格</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="equipmentTMP.BehindEquipmentQualified"
                class="ml-2"
                width="55px"
                inline-prompt
                active-text="是"
                inactive-text="否"
              />
            </el-col>
            <el-col :span="10" />
          </el-row>
        </el-form-item>

        <el-form-item
          v-show="
            !equipmentTMP.BeforeEquipmentQualified ||
            !equipmentTMP.BehindEquipmentQualified
          "
          label="更换备机编号"
          class="form-lable"
        >
          <el-input
            class="align-right"
            v-model="equipmentTMP.ResetC2Equipment"
            placeholder="备机编号"
          />
        </el-form-item>
        <el-form-item label="备注信息" class="form-lable">
          <el-input
            class="align-right"
            v-model="equipmentTMP.DescriptionC2"
            placeholder="备注信息"
          />
        </el-form-item>
      </div>
      <div v-show="active == 4"></div>
    </el-form>
  </el-dialog>
</template>

<script lang="ts">
import * as vue from "vue";
import cloneDeep from "lodash.clonedeep";
import { getBaseUrl, getTenant, getHeader } from "@/utils/global";

// import ODataSelector from "@/components/ODataSelector.vue";
// import RegionTree from "@/components/RegionTree.vue";
import { Equipment, EquipmentTPM } from "@/defs/Entity";
import store from "@/store";
import Decimal from "decimal.js";
import { equipmentTMPExport } from "@/utils/ExportPdf";
import { oDataQuery, oDataPost, oDataPatch } from "@/utils/odata";
import { ElMessageBox } from "element-plus";
import moment from "moment";
const WAIT_TIME = 480; // 等待时间（秒）
const INTERVAL_DELAY = 1000; // 更新间隔（毫秒）

export default vue.defineComponent({
  name: "EquipmentTMPAdd",
  // components: { ODataSelector, RegionTree },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Equipment,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = vue.computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });

    const active = vue.ref(1);
    const startTime = vue.ref<number>(0);

    const disableNext = vue.ref(false);

    const modelInner = vue.ref<Equipment>();

    const today = new Date();
    today.setHours(0, 0, 0, 0); // 设置为当天的开始时间
    const tomorrow = new Date(today);
    tomorrow.setDate(tomorrow.getDate() + 1); // 设置为明天的开始时间

    const equipmentTMP = vue.ref<EquipmentTPM>({ Id: 0, EquipmentId: 0 });
    // const doubleCount = vue.computed(() => count.value * 2);

    const remainingTime = vue.ref(WAIT_TIME);
    /**
     * 更新时间和状态
     * Update time and state
     */

    vue.watch(visibleInner, async (newVal) => {
      if (newVal) {
        let copyQuery = cloneDeep(vue.toRaw(props.model));

        const loadDatas = (
          await oDataQuery("EquipmentTPM", {
            $filter: `(EquipmentId eq ${
              props.model?.Id ?? 0
            })and (StartDate ge ${today.toISOString()} and StartDate lt ${tomorrow.toISOString()})`,
            $orderby: "Id desc",
            $top: 1,
            $expand:
              "Equipment($select=Id,Name;$expand=Location($select=Name),EquipmentType($select=Description))",
          })
        ).value as Partial<EquipmentTPM>[];

        if (loadDatas.length > 0) {
          // 创建新对象而不是直接引用
          if (loadDatas.length > 0) {
            for (let prop in loadDatas[0]) {
              if (loadDatas[0][prop] !== undefined) {
                equipmentTMP.value[prop] = loadDatas[0][prop];
              }
            }
          }

          // 恢复保存的步骤和剩余时间
          active.value = equipmentTMP.value.Stage || 1;
          // 设置保存的剩余时间
          if (
            equipmentTMP.value.RemainSecond !== undefined &&
            equipmentTMP.value.RemainSecond !== null
          ) {
            remainingTime.value = equipmentTMP.value.RemainSecond;
          } else {
            remainingTime.value = WAIT_TIME;
          }
          console.log(equipmentTMP.value.EndDate);
        } else {
          equipmentTMP.value = { Id: 0, EquipmentId: 0 };
          for (let prop in equipmentTMP.value) {
            equipmentTMP.value[prop] = undefined;
          }
          modelInner.value = copyQuery;
          active.value = 1;
          disableNext.value = true;
          equipmentTMP.value.Stage = 1;

          remainingTime.value = WAIT_TIME;
          equipmentTMP.value.RemainSecond = WAIT_TIME;

          if (getTenant() === "Longdi") {
            equipmentTMP.value!.Name = "上海龙涤环保技术工程有限公司";
          } else {
            equipmentTMP.value!.Name = "上海爱启环境技术工程有限公司";
          }
          equipmentTMP.value.StartDate = new Date();
          equipmentTMP.value.EndDate = `0001-01-01T00:00:00Z`;
          equipmentTMP.value.EquipmentId = modelInner.value!.Id;
          equipmentTMP.value.UserName = store.state.user.userName;
          //1为绿林 2 为朗亿

          equipmentTMP.value.SetFlow =
            modelInner.value!.EquipmentTypeId == 1 ? "2.0" : "1.2";
          equipmentTMP.value.SetFlow2 =
            modelInner.value!.EquipmentTypeId == 1 ? "2.0" : "1.2";

          equipmentTMP.value.EquipmentName = modelInner.value!.Name;
          equipmentTMP.value.EquipmentControlNumber =
            modelInner.value!.ControlNumber;
          equipmentTMP.value.EquipmentSerialNumber =
            modelInner.value!.SerialNumber;

          equipmentTMP.value.EquipmentZero = "0";
          equipmentTMP.value.EquipmentSpan =
            modelInner.value!.EquipmentTypeId == 1 ? "6.254" : "5";
          equipmentTMP.value.BeforeEquipmentQualified = true;
          equipmentTMP.value.BehindEquipmentQualified = true;

          const rsp = (await oDataPost(
            "EquipmentTPM",
            equipmentTMP.value
          )) as EquipmentTPM;
          equipmentTMP.value.Id = rsp.Id;
        }
        // 在数据更新和计时器启动后，手动触发更新
        vue.nextTick(() => {
          startUpdatingTimer();

          console.log("数据加载完成，手动触发更新");
        });
      }
    });
    let interval: ReturnType<typeof setInterval> | null = null; // Initialize as null

    const updateInterval = 10; // 每10秒更新一次数据库
    let updateCount = 0; // 计数器，用于控制oDataPatch的调用
    let isVisible = true; // 页面是否可见
    let lastUpdateTime = Date.now(); // Initialize lastUpdateTime

    async function updateTimerRecord() {
      try {
        // Load the latest remaining time from the database
        const loadDatas = (
          await oDataQuery("EquipmentTPM", {
            $filter: `(EquipmentId eq ${
              props.model?.Id ?? 0
            }) and (StartDate ge ${today.toISOString()} and StartDate lt ${tomorrow.toISOString()})`,
            $orderby: "Id desc",
            $top: 1,
            $select: "StartDate,EndDate,RemainSecond",
          })
        ).value as Partial<EquipmentTPM>[];

        if (loadDatas.length > 0) {
          // 假设 loadDatas[0].StartDate 是一个 MomentInput
          const dstDate = moment(loadDatas[0].StartDate)
            .add(8, "minutes")
            .valueOf(); // 在开始时间上加8分钟
          const currentTime = new Date().getTime();
          remainingTime.value = Math.max(
            0,
            Math.floor((dstDate - currentTime) / 1000)
          ); // Ensure it's not less than zero
          console.log(remainingTime.value);
        }
      } catch (error) {
        console.error("Error updating timer record:", error);
      }
    }

    const startUpdatingTimer = () => {
      if (interval) return; // If the timer is already running, do not start it again

      let updateCount = 0; // Counter to track update frequency

      interval = setInterval(() => {
        if (remainingTime.value > 0) {
          updateTimerRecord();
        } else {
          clearTimer();
        }
      }, 1000); // Update every 1 second
    };
    // 处理页面可见性变化
    document.addEventListener("visibilitychange", () => {
      isVisible = !document.hidden; // 更新页面可见性状态
      if (!isVisible && interval) {
        clearTimer();
      } else if (isVisible && !interval) {
        startUpdatingTimer(); // 页面可见时重新启动计时器
      }
    });

    // 处理页面隐藏和显示
    window.addEventListener("pagehide", () => {
      if (interval) {
        clearTimer();
      }
    });

    window.addEventListener("pageshow", () => {
      if (!interval) {
        startUpdatingTimer(); // 页面显示时重新启动计时器
      }
    });
    function clearTimer() {
      if (interval) {
        clearInterval(interval); // 清除定时器
        interval = null; // 重置 interval 为 null
      }
    }
    vue.onBeforeUnmount(() => {
      clearTimer();
    });

    const opacityValue = vue.ref(1); // 初始透明度

    const onTransitionEnd = () => {
      opacityValue.value = 1; // 过渡结束后恢复透明度
    };
    function dataDisable(data) {
      return data == "" || data == undefined || data == null;
    }

    const accecptDisabled = vue.computed(() => {
      console.log("计算 accecptDisabled", {
        EndDate: equipmentTMP.value.EndDate,
        active: active.value,
        remainingTime: remainingTime.value,
        EquipmentReal1: equipmentTMP.value.EquipmentReal1,
        EquipmentReal3: equipmentTMP.value.EquipmentReal3,
      });
      // 如果 EndDate 有数值，直接返回 true
      if (equipmentTMP.value.EndDate != `0001-01-01T00:00:00Z`) {
        return true;
      }

      return (
        active.value != 3 ||
        remainingTime.value != 0 ||
        dataDisable(equipmentTMP.value.EquipmentReal1) ||
        dataDisable(equipmentTMP.value.EquipmentReal3)
      );
    });
    return {
      interval,
      remainingTime,
      clearTimer,
      opacityValue,
      onTransitionEnd,
      active,
      disableNext,
      visibleInner,
      modelInner,
      equipmentTMP,
      accecptDisabled,
    };
  },
  data() {
    return {
      //默认第一步
      // active: 1,
      // disableNext: false,
      //state: false,
      rules: {},
    };
  },
  // deactivated() {
  //   this.active = 1;
  //   this.disableNext = false;
  // },
  // created() {
  //   // 当组件被创建后，设置一个定时器
  //   setTimeout(() => {
  //     this.state = true; // 10分钟后将状态更改为true
  //   }, 1000 * 60); // 600000毫秒等于10分钟
  // },
  watch: {
    equipmentTMP: {
      handler: function (newVal, oldVal) {
        this.disableNext = true;

        switch (this.active) {
          case 1:
            if (
              this.equipmentTMP.DeviceLocation &&
              this.equipmentTMP.TemperatureData
            ) {
              this.disableNext = false;
            }
            break;
          case 2:
            if (
              this.equipmentTMP.BeforeFlow1 &&
              this.equipmentTMP.BeforeFlow1 &&
              this.equipmentTMP.BeforeFlow1
            ) {
              if (this.equipmentTMP.BeforeSetFlow) {
                if (
                  this.equipmentTMP.BehindFlow1 &&
                  this.equipmentTMP.BehindFlow2 &&
                  this.equipmentTMP.BehindFlow3
                ) {
                  this.disableNext = false;
                }
              } else {
                this.disableNext = false;
              }
            }
            break;
          case 3:
            if (
              this.equipmentTMP.EquipmentReal1 &&
              this.equipmentTMP.EquipmentReal2 &&
              this.equipmentTMP.EquipmentReal3
            ) {
              this.disableNext = false;
            }
            break;
          default:
            break;
        }
        // 这个回调会在`obj`或其嵌套属性变化时触发
      },
      deep: true,
    },
  },
  methods: {
    next() {
      if (this.active++ > 3) this.active = 1;

      switch (this.active) {
        case 1:
          break;
        case 2:
          this.equipmentTMP.C1StartDate = new Date();

          break;
        case 3:
          this.equipmentTMP.C1EndDate = new Date();
          this.equipmentTMP.C2StartDate = new Date();

          break;
      }
    },
    // 步骤条上一步的方法
    pre() {
      if (this.active-- < 2) this.active = 1;
      switch (this.active) {
        case 1:
          this.disableNext = false;
          this.equipmentTMP.C1StartDate = undefined;
          break;
        case 2:
          this.disableNext = true;
          this.setC1Data();
          this.equipmentTMP.C1StartDate = new Date();
          this.equipmentTMP.C2StartDate = undefined;
          // this.setC1Data();
          break;
        case 3:
          break;
      }
    },
    setC1Data() {
      // Math.abs(this.number
      if (
        this.equipmentTMP.BeforeFlow1 &&
        this.equipmentTMP.BeforeFlow2 &&
        this.equipmentTMP.BeforeFlow3 &&
        this.equipmentTMP.SetFlow
      ) {
        let averageFlow = new Decimal(this.equipmentTMP.BeforeFlow1)
          .plus(new Decimal(this.equipmentTMP.BeforeFlow2))
          .plus(new Decimal(this.equipmentTMP.BeforeFlow3))
          .div(3);

        let difference = averageFlow
          .minus(new Decimal(this.equipmentTMP.SetFlow))
          .div(new Decimal(this.equipmentTMP.SetFlow))
          .times(100)
          .abs(); // 取绝对值
        // 保留两位小数
        this.equipmentTMP.BeforeRelativeError = difference.toFixed(2) + "%";
        this.equipmentTMP.BeforeSetFlow = difference.greaterThan(10);

        if (!this.equipmentTMP.BeforeSetFlow) {
          this.disableNext = false;
        }
      }
      if (this.equipmentTMP.BeforeSetFlow) {
        if (
          this.equipmentTMP.BehindFlow1 &&
          this.equipmentTMP.BehindFlow2 &&
          this.equipmentTMP.BehindFlow3 &&
          this.equipmentTMP.SetFlow2
        ) {
          let averageFlow = new Decimal(this.equipmentTMP.BehindFlow1)
            .plus(new Decimal(this.equipmentTMP.BehindFlow2))
            .plus(new Decimal(this.equipmentTMP.BehindFlow3))
            .div(3);

          let difference = averageFlow
            .minus(new Decimal(this.equipmentTMP.SetFlow2))
            .div(new Decimal(this.equipmentTMP.SetFlow2))
            .times(100)
            .abs(); // 取绝对值
          // 保留两位小数
          this.equipmentTMP.BehindRelativeError = difference.toFixed(2) + "%";
          this.equipmentTMP.BehindSetFlow = difference.greaterThan(10);
          this.disableNext = false;
        }
      }
    },
    setC2Data() {
      // if (
      //   this.equipmentTMP.EquipmentZero &&
      //   this.equipmentTMP.EquipmentReal1 &&
      //   this.equipmentTMP.EquipmentReal2 &&
      //   this.equipmentTMP.SetFlow
      // ) {
      //   let averageFlow = new Decimal(this.equipmentTMP.EquipmentReal1)
      //     .plus(new Decimal(this.equipmentTMP.EquipmentReal2))
      //     .div(2);

      //   let difference = averageFlow
      //     .minus(new Decimal(this.equipmentTMP.EquipmentZero))
      //     .div(new Decimal(this.equipmentTMP.EquipmentZero))
      //     .times(100)
      //     .abs(); // 取绝对值
      //   // 保留两位小数
      //   this.equipmentTMP.EquipmentRelativeError = difference.toFixed(2) + "%";
      //   this.equipmentTMP.BeforeSetFlow = difference > new Decimal("10");
      // }
      if (this.equipmentTMP.EquipmentSpan && this.equipmentTMP.EquipmentReal3) {
        let averageFlow = new Decimal(this.equipmentTMP.EquipmentReal3);
        // .plus(new Decimal(this.equipmentTMP.BehindFlow2))
        // .plus(new Decimal(this.equipmentTMP.BehindFlow3))
        // .div(3);

        let difference = averageFlow
          .minus(new Decimal(this.equipmentTMP.EquipmentSpan))
          .div(new Decimal(this.equipmentTMP.EquipmentSpan))
          .times(100)
          .abs(); // 取绝对值
        // 保留两位小数
        this.equipmentTMP.BehindEquipmentRelativeError =
          difference.toFixed(2) + "%";
        this.equipmentTMP.BehindEquipmentQualified = new Decimal(
          "10"
        ).greaterThan(difference);
      }
    },
    // 暂存处理函数
    async handleTempSave() {
      this.equipmentTMP.Stage = this.active;
      this.equipmentTMP.RemainSecond = this.remainingTime;

      // 确保克隆前的数据包含 Id
      console.log("Before clone Id:", this.equipmentTMP.Id); // 调试用

      let data = cloneDeep(vue.toRaw(this.equipmentTMP)) as EquipmentTPM;

      // 确保克隆后的数据包含 Id
      console.log("After clone Id:", data.Id); // 调试用

      // 显式检查并确保 Id 存在
      if (!data.Id) {
        data.Id = this.equipmentTMP.Id; // 如果克隆过程丢失了 Id，从原对象恢复
      }

      // 确保 api 调用使用正确的方法
      const api = data.Id && data.Id !== 0 ? this.$update : this.$insert;

      try {
        const rsp = (await api("EquipmentTPM", data)) as any;

        // 保存返回的 Id
        if (rsp && rsp.Id) {
          this.equipmentTMP.Id = rsp.Id;
          console.log("Saved Id:", this.equipmentTMP.Id); // 调试用
        }

        this.$message.success("成功");
      } catch (error) {
        console.error("Save error:", error);
        this.$message.error("保存失败");
      }
    },
    onCancelClick() {
      this.visibleInner = false;
      this.clearTimer();
    },
    async onAcceptClick() {
      this.equipmentTMP.C2EndDate = new Date();
      this.equipmentTMP.EndDate = new Date();
      this.equipmentTMP.RemainSecond = 0;

      // 确保克隆前的数据包含 Id
      console.log("Before clone Id:", this.equipmentTMP.Id); // 调试用

      let data = cloneDeep(vue.toRaw(this.equipmentTMP)) as EquipmentTPM;

      // 确保克隆后的数据包含 Id
      console.log("After clone Id:", data.Id); // 调试用

      // 显式检查并确保 Id 存在
      if (!data.Id) {
        data.Id = this.equipmentTMP.Id; // 如果克隆过程丢失了 Id，从原对象恢复
      }
      for (let prop in data) {
        if (prop != "Id") {
          if (data[prop] == undefined || data[prop] == null || data[prop] == "")
            delete data[prop];
        }
      }

      delete data.Equipment;
      const api =
        data.Id == 0 || data.Id == undefined ? this.$insert : this.$update;

      const rsp = (await api("EquipmentTPM", data)) as any;

      // this.$message.success(this.$t("template.addSuccess"));

      // let equipment = cloneDeep(vue.toRaw(this.modelInner)) as Equipment;
      var updateData = false;
      let equipment = {
        Id: this.modelInner?.Id,
      } as Equipment;

      if (this.equipmentTMP.ResetC2Equipment) {
        equipment.ControlNumber = this.equipmentTMP.ResetC2Equipment;
        updateData = true;
      } else if (this.equipmentTMP.ResetC1Equipment) {
        equipment.ControlNumber = this.equipmentTMP.ResetC1Equipment;
        updateData = true;
      }
      if (updateData) {
        await this.$update("Equipment", equipment);
      }

      ElMessageBox.confirm("是否下载PDF?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(async () => {
          const lastData = (
            await oDataQuery("EquipmentTPM", {
              $filter: `(Id eq ${
                data.Id == 0 || data.Id == undefined ? rsp.Id : data.Id
              })`,
              $expand:
                "Equipment($select=Id,Name;$expand=Location($select=Name),EquipmentType($select=Description))",
            })
          ).value as Partial<EquipmentTPM>[];
          equipmentTMPExport(lastData[0] as EquipmentTPM);

          this.$message.success("成功");
          this.$emit("accept");
        })
        .catch(() => {
          this.$message.success("成功");

          this.$emit("accept");
        });
    },
    exportPdf() {},
  },
});
</script>
<style>
.el-message-box {
  width: 200px;
}
</style>

<style scoped>
.custom-tag {
  background-color: white !important; /* 设置背景颜色为白色 */
  color: black; /* 设置文本颜色为黑色，以确保可读性 */
  transition: opacity 0.5s; /* 过渡效果 */
}
.dotClass {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  display: block;
  margin-left: 10px;
}
.correct:before {
  content: "\2714";
  color: #008100;
}
.incorrect:before {
  content: "\2716";
  color: #b20610;
}
.spanTipsPrompt {
  color: #b20610;
  font-size: 18px;
  font-weight: bold;
}
.spanData {
  margin-right: 0x;
  margin-right: 24px;
}
.spanName {
  margin-right: 0px;
  font-weight: bold;
  text-align: center;
}
.steps-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
  width: 100%;
}
.steps-container .el-steps {
  width: 90%; /* 或者您希望的宽度 */
}
.info-title {
  display: block;
  grid-template-columns: auto 1fr;
  font-weight: bold;
  font-size: larger;
  text-align: center;
}
.info-container {
  display: grid;
  grid-template-columns: auto 1fr;
  margin-top: 10px;
}

.info-container span:nth-child(odd) {
  font-weight: bold;
  text-align: right;
}
.info-container span:nth-child(even) {
  margin-left: 8px;
  text-align: left;
}
.align-right {
  display: flex;
  align-items: flex-end;
  text-align: right;
}
.form-lable {
  font-weight: bold; /* 设置您想要的宽度 */
}
.form-span {
  font-weight: 100;
  width: 100%; /* 设置您想要的宽度 */
  display: flex; /* 或者 display: grid; */
}

.remaining-time {
  color: #333;
  align-items: center;
  display: flex;
  justify-content: center;
  margin: 15px 0;
  font-size: 18px; /* 增加字体大小 */
  font-weight: bold; /* 加粗字体 */
}

.remaining-time .time {
  padding: 5px 10px; /* 内边距 */
  border-radius: 4px; /* 圆角 */
  animation: blink 1s infinite; /* 添加闪烁动画 */
}
@keyframes blink {
  0% {
    opacity: 1;
  }
  50% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
</style>
