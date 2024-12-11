<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Equipment.entity") }}</span>
          <el-switch
            :style="{ float: 'right' }"
            v-model="queryModel.IsActive"
            :active-value="true"
            :inactive-value="false"
            :active-text="$t('template.showDisabled')"
            @change="handleActiveChange"
          />
        </div>
      </template>
      <el-container>
        <el-main>
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
              prop="EquipmentType.Description"
              :label="$t('Equipment.column.EquipmentType')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Name"
              :label="$t('Equipment.column.Name')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Description"
              :label="$t('Equipment.column.Description')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Barcode"
              :label="$t('Equipment.column.Barcode')"
              sortable="custom"
              width="150"
            >
              <template #default="scope">
                <el-button
                  type="primary"
                  link
                  @click.prevent="showBarcode(scope.row.Barcode)"
                >
                  {{ scope.row.Barcode }}
                </el-button>
              </template>
            </el-table-column>
            <el-table-column
              prop="Location.Name"
              :label="$t('Equipment.column.Location')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="UnitOfMeasure.Description"
              :label="$t('Equipment.column.UnitOfMeasure')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="CalibrationDate"
              :formatter="dateFormatUI"
              :label="$t('Equipment.column.CalibrationDate')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="NextCalibrationDate"
              :formatter="dateFormatUI"
              :label="$t('Equipment.column.NextCalibrationDate')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="CalibrationValue"
              :label="$t('Equipment.column.CalibrationValue')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="ControlNumber"
              :label="$t('Equipment.column.ControlNumber')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="SerialNumber"
              :label="$t('Equipment.column.SerialNumber')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="150"
              v-has="['Equipment:Add', 'Equipment:Edit', 'Equipment:Disable']"
            >
              <!-- <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'Equipment:Add'"
                    :disabled="equipmentTypeId == undefined"
                    type="primary"
                    link
                    @click="onAddClick"
                  >
                    {{ $t("template.add") }}
                  </el-button>
                </div>
              </template> -->
              <template #default="scope">
                <el-button
                  type="primary"
                  link
                  @click.prevent="editRow(scope.$index)"
                  v-has="'Equipment:Edit'"
                >
                  维护
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
    <TmpEditor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      v-model:equipmentTypeId="equipmentTypeId"
      @accept="onEditAccept"
    />
    <!-- <el-dialog v-model="barcodeVisable" width="30%" center>
      <div class="barcode-father">
        <vue-barcode v-model:value="barcode" class="barcode-show" />
      </div>
    </el-dialog> -->
  </div>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
// import VueBarcode from "vue3-barcode";
import { ListMixin } from "@/mixins/ListMixin";
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import moment from "moment";
import cloneDeep from "lodash.clonedeep";
import { toRaw } from "vue";
import TmpEditor from "./EditModal/TmpEditor.vue";

export default defineComponent({
  name: "EquipmentList",
  components: {  TmpEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Equipment",
      barcodeVisable: false,
      barcode: "",
      equipmentTypeId: undefined,
      queryModel: {
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
        IsActive: undefined,
        DeviceConfig: {
          OfficialConfig: undefined,
          Version: undefined,
        },
      },
      query: {
        $expand: "EquipmentType($select=Id,Description)",
        $select:
          "Id,EquipmentTypeId,Name,Description,Barcode,CalibrationDate," +
          "NextCalibrationDate,CalibrationValue,ControlNumber,SerialNumber,IsActive",
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
        DeviceConfig: {
          OfficialConfig: undefined,
          Version: undefined,
        },
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.EquipmentTypeId) {
        filterStr.push(`EquipmentTypeId eq ${this.queryModel.EquipmentTypeId}`);
      }
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
      if (this.queryModel.IsActive != true) {
        filterStr.push(`IsActive eq true`);
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
      delete data.DeviceConfig;

      api(this.entityName, data).then(() => {
        this.loadData();
      });
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
