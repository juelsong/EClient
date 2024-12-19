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
        <el-button v-no-more-click v-show="active > 1" @click="pre">
          上一步
        </el-button>
        <el-button v-show="active < 3" :disabled="disableNext" @click="next">
          下一步
        </el-button>
        <!-- <el-button v-no-more-click v-show="active == 3" @click="exportPdf">
          导出
        </el-button> -->
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button
          type="primary"
          v-no-more-click
          :disabled="active != 3"
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
              <span class="form-span">实测值1 mg/m³</span>
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
          <el-row style="width: 400px">
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
          </el-row>
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
        <el-form-item
          label="校标"
          class="form-lable"
        >
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
import { equipmentTMPExport} from "@/utils/ExportPdf";
import { oDataQuery } from "@/utils/odata";
import { ElMessageBox } from "element-plus";

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
    const modelInner = vue.ref<Equipment>();
    const equipmentTMP = vue.ref<EquipmentTPM>({ Id: 0, EquipmentId: 0 });
    if (getTenant() === "Longdi") {
      equipmentTMP.value!.Name = "上海龙涤环保技术工程有限公司";
    } else {
      equipmentTMP.value!.Name = "上海爱启环境技术工程有限公司";
    }
    equipmentTMP.value.StartDate = new Date();

    vue.watch(visibleInner, (newVal) => {
      if (newVal) {
        let copyQuery = cloneDeep(vue.toRaw(props.model));
        modelInner.value = copyQuery;
      }
      console.log(store.state.user.userName);

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
      equipmentTMP.value.EquipmentSerialNumber = modelInner.value!.SerialNumber;

      equipmentTMP.value.EquipmentZero = "0";
      equipmentTMP.value.EquipmentSpan =
        modelInner.value!.EquipmentTypeId == 1 ? "6.254" : "5";
      equipmentTMP.value.BeforeEquipmentQualified = true;
      equipmentTMP.value.BehindEquipmentQualified = true;
    });
    return { visibleInner, modelInner, equipmentTMP };
  },
  data() {
    return {
      //默认第一步
      active: 1,
      disableNext: false,
      rules: {},
    };
  },
  deactivated() {
    this.active = 1;
    this.disableNext = false;
  },
  methods: {
    next() {
      if (this.active++ > 3) this.active = 1;
      switch (this.active) {
        case 1:
          break;
        case 2:
          this.disableNext = true;

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
          this.equipmentTMP.StartDate = new Date();
          this.equipmentTMP.C1StartDate = undefined;
          break;
        case 2:
          this.disableNext = true;
          this.setC1Data() ;
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
        this.equipmentTMP.BeforeSetFlow = difference > new Decimal("10");
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
          this.equipmentTMP.BehindSetFlow = difference > new Decimal("10");
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
        this.equipmentTMP.BehindEquipmentQualified =
          difference < new Decimal("10");
      }
    },
    async onAcceptClick() {
      for (let prop in this.equipmentTMP) {
        if (this.equipmentTMP[prop] == undefined) this.equipmentTMP[prop] = "";
      }
      this.equipmentTMP.C2EndDate = new Date();
      this.equipmentTMP.EndDate = new Date();

      let data = cloneDeep(vue.toRaw(this.equipmentTMP));

      const rsp = (await this.$insert("EquipmentTPM", data)) as any;

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

      ElMessageBox.confirm("是否导出pdf?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(async () => {
          const lastData = (
            await oDataQuery("EquipmentTPM", {
              $filter: `(Id eq ${rsp.Id})`,
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
<style scoped>
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
</style>
