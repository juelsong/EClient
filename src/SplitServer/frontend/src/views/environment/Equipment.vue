<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Equipment.entity") }}</span>
        </div>
      </template>
      <el-container>
        <el-header>
          <el-form ref="queryForm" :inline="true" :model="queryModel">
            <el-form-item
              :label="$t('Missions.filter.Location')"
              prop="locationId"
            >
              <o-data-selector
                :placeholder="`所属区县`"
                :multiple="false"
                v-model="queryModel.locationId"
                :clearable="true"
                :auto-select="false"
                entity="Location"
                label="Name"
                value="Id"
              />
            </el-form-item>
            <el-form-item label="设备Id和名称" prop="equipmentId">
              <el-input v-model="queryModel.equipmentIdAndName" />
            </el-form-item>
            <!-- <el-form-item label="设备名称" prop="EquipmentName">
              <el-input v-model="queryModel.EquipmentName" />
            </el-form-item> -->
            <el-form-item>
              <el-button type="primary" @click="loadData">
                {{ $t("template.search") }}
                <el-icon class="el-icon--right">
                  <svg-icon icon-class="edit" />
                </el-icon>
              </el-button>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onResetSearchClick">
                {{ $t("template.reset") }}
                <el-icon class="el-icon--right">
                  <svg-icon icon-class="refresh" />
                </el-icon>
              </el-button>
            </el-form-item>
            <el-switch
              :style="{ float: 'right' }"
              v-model="queryModel.isOperation"
              :active-value="true"
              :inactive-value="false"
              active-text="是否运维打卡"
              @change="handleActiveChange"
            />
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
            ref="dataTable"
            :always="true"
            :row-class-name="deactiveRowClassName"
            highlight-current-row
            :data="tableData.data"
            :border="true"
            stripe
            height="100%"
            max-height="100%"
            width="100%"
            @sort-change="onSortChange"
            @selection-change="onSelectionChange"
          >
            <el-table-column
              prop="SerialNumber"
              label="设备Id"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Name"
              :label="$t('Equipment.column.Name')"
              sortable="custom"
            />
            <el-table-column
              prop="Location.Name"
              :label="$t('Equipment.column.Location')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="EquipmentType.Description"
              label="粉尘仪型号"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="ControlNumber"
              label="粉尘仪编号"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="IsOperation"
              label="是否运维打卡"
              sortable="custom"
              :formatter="stateFormatter"
              width="150"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="110"
              v-has="['Equipment:Add', 'Equipment:Edit', 'Equipment:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'Equipment:Add'"
                    type="primary"
                    link
                    @click="onAddClick"
                  >
                    {{ $t("template.add") }}
                  </el-button>
                </div>
              </template>
              <template #default="scope">
                <!-- <el-button
                  type="primary"
                  link
                  @click.prevent="
                    setIsActive(scope.$index, !scope.row.IsActive)
                  "
                  v-has="'Equipment:Disable'"
                >
                  {{
                    scope.row.IsActive
                      ? $t("template.disable")
                      : $t("template.enable")
                  }}
                </el-button> -->
                <el-button
                  type="primary"
                  link
                  @click.prevent="editRow(scope.$index)"
                  v-has="'Equipment:Edit'"
                >
                  {{ $t("template.edit") }}
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
        <el-footer>
          <el-pagination
            class="table-pagination"
            v-model:currentPage="tableData.current"
            v-model:pageSize="tableData.pageSize"
            :page-sizes="[10, 20, 50]"
            layout="total, prev, pager, next, sizes, jumper"
            :total="tableData.total"
            :pager-count="5"
          ></el-pagination>
        </el-footer>
      </el-container>
    </el-card>
    <equipment-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      v-model:equipmentTypeId="equipmentTypeId"
      @accept="onEditAccept"
    />
    <el-dialog v-model="barcodeVisable" width="30%" center>
      <div class="barcode-father">
        <vue-barcode v-model:value="barcode" class="barcode-show" />
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
import VueBarcode from "vue3-barcode";
import { ListMixin } from "@/mixins/ListMixin";
import EquipmentEditor from "./EditModal/EquipmentEditor.vue";
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import moment from "moment";
import cloneDeep from "lodash.clonedeep";
import { toRaw } from "vue";
import { EquipmentQueryModel } from "./QueryModel";
import ODataSelector from "@/components/ODataSelector.vue";

export default defineComponent({
  name: "EquipmentList",
  components: { EquipmentEditor, VueBarcode, ODataSelector },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Equipment",
      barcodeVisable: false,
      barcode: "",
      equipmentTypeId: undefined,
      queryModel: {
        quipmentId: undefined,
        locationId: undefined,
        EquipmentName: undefined,
        isOperation: false,
        equipmentIdAndName:undefined,
      },
      query: {
        $expand:
          "EquipmentType($select=Id,Description),Location($select=Id,Name),EquipmentConfig($select=Id,Version)",
        $select:
          "Id,EquipmentTypeId,Name,Description,Barcode,LocationId,CalibrationDate," +
          "NextCalibrationDate,CalibrationValue,ControlNumber,IsOperation,SerialNumber,IsActive,EquipmentConfigId",
      },
      editModel: {
        EquipmentTypeId: undefined,
        Name: undefined,
        Description: undefined,
        Barcode: undefined,
        UnitOfMeasureId: undefined,
        LocationId: undefined,
        CalibrationDate: undefined,
        NextCalibrationDate: undefined,
        CalibrationValue: undefined,
        ControlNumber: undefined,
        EquipmentConfig: {
          OfficialConfig: undefined,
          Version: undefined,
        },
      },
    };
  },
  methods: {
    stateFormatter: function (row, column) {
      return row.IsOperation ? "是" : "否";
    },
    buildFilterStr() {
      let filterStr = [];
      // if (this.queryModel.equipmentId) {
      //   filterStr.push(`c`);
      // }
      if (this.queryModel.equipmentIdAndName) {
        filterStr.push(`(contains(SerialNumber, '${this.queryModel.equipmentIdAndName}')) or (contains(Name, '${this.queryModel.equipmentIdAndName}')) `);
      }
      if (this.queryModel.locationId) {
        filterStr.push(`LocationId eq ${this.queryModel.locationId}`);
      }
      // if (this.queryModel.EquipmentTypeId) {
      //   filterStr.push(`EquipmentTypeId eq ${this.queryModel.EquipmentTypeId}`);
      // }
      // if (this.queryModel.Name && this.queryModel.Name.length > 0) {
      //   filterStr.push(`contains(Name,'${this.queryModel.Name}')`);
      // }
      // if (
      //   this.queryModel.Description &&
      //   this.queryModel.Description.length > 0
      // ) {
      //   filterStr.push(
      //     `contains(Description,'${this.queryModel.Description}')`
      //   );
      // }
      // if (this.queryModel.Barcode && this.queryModel.Barcode.length > 0) {
      //   filterStr.push(`contains(Barcode,'${this.queryModel.Barcode}')`);
      // }
      if (this.queryModel.isOperation == true) {
        filterStr.push(`IsOperation eq true`);
      }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    dateFormatUI: function (row, column) {
      return dateFormat(row, column);
    },
    showBarcode(barcode) {
      this.barcode = barcode;
      this.barcodeVisable = true;
    },
    handleActiveChange() {
      this.loadData();
    },
    searchEquipmentType(id) {
      this.queryModel.EquipmentTypeId = id;
      this.editModel.EquipmentTypeId = id;
      this.equipmentTypeId = id;
      this.loadData();
    },
    onEditAccept() {
      let data = cloneDeep(toRaw(this.editModel));
      if (this.onEditAcceptOverride) {
        this.onEditAcceptOverride(data);
      }
      if (data.LocationId === undefined) {
        data.LocationId = null;
      }

      let api = this.createNew ? this.$insert : this.$update;
      delete data.EquipmentConfig;

      api(this.entityName, data).then(() => {
        this.loadData();
      });
      this.$message.success("成功");
    },
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
