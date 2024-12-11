<template>
  <el-dialog
    v-model="visibleInner"
    :before-close="closeDialog"
    :close-on-click-modal="false"
    :show-close="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Equipment.entity'
    )}`"
    width="600px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button v-no-more-click v-show="active > 1" @click="pre">
          上一步
        </el-button>
        <el-button v-no-more-click v-show="active < 3" @click="next">
          下一步
        </el-button>
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
    <el-steps style="max-width: 600px" :active="active" finish-status="success">
      <el-step title="第一步" />
      <el-step title="第二步" />
      <el-step title="第三步" />
    </el-steps>
    <el-form
      ref="dataForm"
      :model="form"
      :rules="formRules"
      label-position="left"
      label-width="100px"
      style="width: 500px; margin-left: 50px"
    >
      <div v-show="active == 1">
        <div class="info-title">
          <span>扬尘在线监测系统运行维护记录表</span>
        </div>
        <div class="info-container">
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
        </div>
        <el-form-item label="电力系统" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">供应状态</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
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
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
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
              <el-switch
                v-if="false"
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
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
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">仪器滤罐是否需更换</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
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
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
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
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="8">
              <span class="form-span">视频监控是否正常</span>
            </el-col>
            <el-col :span="4">
              <el-switch
                v-model="form.delivery"
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
                v-model="form.remark"
                placeholder="处理方法"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
        </el-form-item>
      </div>
      <div v-show="active == 2">
        <div class="info-title">
          <span>C.1 流量校准记录表</span>
        </div>
        <div class="info-container">
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
        </div>
        <el-form-item label="使用单位名称" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="form.remark"
              placeholder="单位名称"
              :suffix-icon="Calendar"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="工程报建号" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="form.remark"
              placeholder="工程报建号"
              :suffix-icon="Calendar"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="校准前" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">设定流量 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="设定流量"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量1 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="实测流量1"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测流量2"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测流量3"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="相对误差"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否需要调整流量</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="form.delivery"
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
        <el-form-item label="校准后" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">设定流量 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="设定流量"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">实测流量1 L/min</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="实测流量1"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测流量2"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测流量3"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="相对误差"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否需要调整流量</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="form.delivery"
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
        <el-form-item label="是否更换备机" class="form-lable">
          <el-switch
            v-model="form.delivery"
            class="ml-2"
            width="55px"
            inline-prompt
            active-text="是"
            inactive-text="否"
          />
        </el-form-item>
      </div>
      <div v-show="active == 3">
        <div class="info-title">
          <span>C.2 颗粒物监测仪校准记录表</span>
        </div>
        <div class="info-container">
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
        </div>
        <el-form-item label="使用单位名称" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="form.remark"
              placeholder="单位名称"
              :suffix-icon="Calendar"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="工程报建号" class="form-lable">
          <el-row style="width: 400px">
            <el-input
              class="align-right"
              v-model="form.remark"
              placeholder="工程报建号"
              :suffix-icon="Calendar"
            />
          </el-row>
        </el-form-item>
        <el-form-item label="校零" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">仪器零值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="仪器零值"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测值1"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测值2"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="相对误差"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">校零</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="校零"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否合格</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="form.delivery"
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
        <el-form-item label="校准后" class="form-lable">
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">仪器零值 mg/m³</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="仪器零值"
                :suffix-icon="Calendar"
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
                v-model="form.remark"
                placeholder="实测值"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">相对误差</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="相对误差"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">校标</span>
            </el-col>
            <el-col :span="4" />
            <el-col :span="10">
              <el-input
                class="align-right"
                v-model="form.remark"
                placeholder="校标"
                :suffix-icon="Calendar"
              />
            </el-col>
          </el-row>
          <el-row style="width: 400px">
            <el-col :span="10">
              <span class="form-span">是否合格</span>
            </el-col>
            <el-col :span="10">
              <el-switch
                v-model="form.delivery"
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
        <el-form-item label="是否更换备机" class="form-lable">
          <el-switch
            v-model="form.delivery"
            class="ml-2"
            width="55px"
            inline-prompt
            active-text="是"
            inactive-text="否"
          />
        </el-form-item>
      </div>
      <div v-show="active == 4"></div>
    </el-form>

    <!-- <el-form
      ref="editForm"
      :model="modelInner"
      :rules="rules"
      label-width="110px"
      label-position="right"
    >
      <el-form-item
        :label="$t('Equipment.editor.EquipmentType')"
        prop="EquipmentType"
      >
        <o-data-selector
          ref="EquipmentType"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.EquipmentType'),
          ])}`"
          :multiple="false"
          v-model="modelInner.EquipmentTypeId"
          entity="EquipmentType"
          label="Description"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('Equipment.editor.Name')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.Description')"
        prop="Description"
      >
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('Equipment.editor.Description')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.Barcode')" prop="Barcode">
        <el-input
          ref="modelInner.Barcode"
          v-model="modelInner.Barcode"
          :placeholder="$t('Equipment.editor.Barcode')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.UnitOfMeasure')"
        prop="UnitOfMeasure"
      >
        <o-data-selector
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.UnitOfMeasure'),
          ])}`"
          :multiple="false"
          :clearable="true"
          v-model="modelInner.UnitOfMeasureId"
          entity="UnitOfMeasure"
          label="Description"
          value="Id"
        />
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.Location')" prop="Location">
        <region-tree ref="regionTreeRef" v-model="modelInner.LocationId" />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.CalibrationDate')"
        prop="CalibrationDate"
      >
        <el-date-picker
          ref="calibrationDate"
          v-model="modelInner.CalibrationDate"
          type="date"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.CalibrationDate'),
          ])}`"
          @change="changeCalibrationDate"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.NextCalibrationDate')"
        prop="NextCalibrationDate"
      >
        <el-date-picker
          ref="nextCalibrationDate"
          v-model="modelInner.NextCalibrationDate"
          type="date"
          :placeholder="`${$t('template.select', [
            $t('Equipment.editor.NextCalibrationDate'),
          ])}`"
          :disabled-date="disabledNextCalibrationDate"
        />
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.CalibrationValue')"
        prop="CalibrationValue"
      >
        <el-input-number
          ref="modelInner.CalibrationValue"
          style="width: 350px"
          v-model="modelInner.CalibrationValue"
          :max="999999999.999999"
          :step="0.000001"
          :placeholder="$t('Equipment.editor.CalibrationValue')"
        ></el-input-number>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.ControlNumber')"
        prop="ControlNumber"
      >
        <el-input
          ref="modelInner.ControlNumber"
          v-model="modelInner.ControlNumber"
          :maxlength="15"
          :placeholder="$t('Equipment.editor.ControlNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item
        :label="$t('Equipment.editor.SerialNumber')"
        prop="SerialNumber"
      >
        <el-input
          ref="modelInner.SerialNumber"
          v-model="modelInner.SerialNumber"
          :maxlength="15"
          :placeholder="$t('Equipment.editor.SerialNumber')"
        ></el-input>
      </el-form-item>
      <el-form-item :label="$t('Equipment.editor.ConfigDocument')">
        <el-row style="width: 100%">
          <el-col :span="8">
            <span>
              {{
                `${
                  modelInner.DeviceConfig == undefined
                    ? $t("Equipment.editor.configEmpty")
                    : $t("Equipment.editor.configHasData")
                }`
              }}
            </span>
          </el-col>
          <el-col :span="7">
            <el-upload
              ref="upload"
              :auto-upload="false"
              action=""
              :on-change="openFile"
              :multiple="false"
              :show-file-list="false"
              v-has="'Equipment:UpdateConfig'"
            >
              <el-button type="primary">
                {{ $t("Equipment.editor.UpLoad") }}
              </el-button>
            </el-upload>
          </el-col>
          

          <el-col :span="7">
            <el-button
              type="primary"
              @click="downLoadXml"
              v-has="'Equipment:UpdateConfig'"
            >
              {{ $t("Equipment.editor.DownLoad") }}
            </el-button>
          </el-col>
        </el-row>
      </el-form-item>
    </el-form> -->
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed, ref } from "vue";
import cloneDeep from "lodash.clonedeep";
// import ODataSelector from "@/components/ODataSelector.vue";
// import RegionTree from "@/components/RegionTree.vue";
import { saveAs } from "file-saver";
import { reactive } from "vue";
import { Check, Close } from "@element-plus/icons-vue";

export default defineComponent({
  name: "EquipmentEditor",
  // components: { ODataSelector, RegionTree },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Object,
    },
    createNew: {
      type: Boolean,
      default: false,
    },
    equipmentTypeId: {
      type: Number,
      default: null,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    const modelInner = ref({});
    return { visibleInner, modelInner };
  },
  watch: {
    visibleInner(newVal) {
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
        // if (this.modelInner.EquipmentTypeId == undefined) {
        //   if (this.equipmentTypeId) {
        //     this.modelInner.EquipmentTypeId = this.equipmentTypeId;
        //   }
        // }
      }
      // this.$nextTick(() => {
      //   if (newVal) {
      //     this.$refs.regionTreeRef.loadData();
      //   }
      //   this.$refs.editForm.clearValidate();
      //   this.$refs["EquipmentType"].loadData();
      //   this.$refs["modelInner.Name"].focus();
      // });
    },
  },
  computed: {},
  data() {
    return {
      //默认第一步
      active: 1,
      form: {
        name: "",
        remark: "",
        region: "",
        date1: "",
        date2: "",
        delivery: false,
        type: [],
        resource: "",
        desc: "",
      },
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("Equipment.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "Equipment",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("Equipment.editor.Name"),
                        ])
                      )
                    );
                  } else {
                    callback();
                  }
                });
              }
            },
            trigger: "blur",
          },
        ],
        CalibrationValue: [
          {
            pattern: /^[-,+]?\d+.?\d*$/,
            message: () =>
              this.$t("validator.template.isNumber", [
                this.$t("Equipment.editor.CalibrationValue"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    next() {
      if (this.active++ > 3) this.active = 1;
    },
    // 步骤条上一步的方法
    pre() {
      if (this.active-- < 2) this.active = 1;
    },
    downLoadXml() {
      var data = this.modelInner.DeviceConfig.OfficialConfig;
      let str = new Blob([data], { type: "xml/plain;charset=utf-8" });
      saveAs(str, `config.xml`);
    },
    openFile(file) {
      const fileName = file.name;
      const fileType = fileName.substring(fileName.lastIndexOf("."));

      if (fileType != ".xml") {
        this.$refs.upload.clearFiles();
        this.$message.error(this.$t("Only upload xml Document!"));
        return;
      }
      var reader = new FileReader();

      reader.onload = async () => {
        let that = this;
        if (reader.result) {
          //打印文件内容
          console.log(reader.result);
          // if (that.modelInner.DeviceConfig) {
          //   that.modelInner.DeviceConfig.OfficialConfig = reader.result;
          //   that.modelInner.DeviceConfig.UpLoad = true;
          // } else {
          //   that.modelInner.DeviceConfig = {
          //     OfficialConfig: reader.result,
          //   };
          // }
          let config = {};
          let configIsNew =
            that.createNew || that.modelInner.DeviceConfig == null;
          let configApi = that.$insert;

          if (configIsNew) {
            config.Version = 1;
          } else {
            config.Version = 1 + that.modelInner.DeviceConfig.Version;
          }
          config.OfficialConfig = reader.result;
          configApi("DeviceConfig", config)
            .then((t) => {
              console.log(t);
              that.modelInner.DeviceConfigId = t.Id;
              that.modelInner.DeviceConfig = {
                UpLoad: true,
                OfficialConfig: reader.result,
              };
            })
            .catch((error) => {
              console.log(error);
            });
        }
      };
      reader.readAsText(file.raw);
    },
    changeCalibrationDate(date) {
      if (this.modelInner.NextCalibrationDate) {
        if (date.getTime() >= this.modelInner.NextCalibrationDate) {
          this.modelInner.NextCalibrationDate = undefined;
        }
      }
    },
    disabledNextCalibrationDate(date) {
      if (this.modelInner.CalibrationDate) {
        if (date.getTime() <= this.modelInner.CalibrationDate) {
          return true;
        } else {
          return false;
        }
      }
      return false;
    },
    // changeInput() {
    //   if (this.modelInner.CalibrationValue.indexOf(".") >= 0) {
    //     this.modelInner.CalibrationValue =this.modelInner.CalibrationValue.substring(
    //       0,
    //       this.modelInner.CalibrationValue.indexOf(".") + 6
    //     );
    //   }
    // },

    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
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
