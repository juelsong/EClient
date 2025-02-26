<template>
  <div>
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>设备维护结果列表</span>
        </div>
      </template>
      <el-container>
        <el-header>
          <el-form ref="queryForm" :inline="true" :model="queryModel">
            <el-form-item label="维护时间" prop="SampleDate">
              <el-date-picker
                v-model="queryModel.startDate"
                type="daterange"
                :range-separator="$t('label.dateRange.separator')"
                :start-placeholder="$t('label.dateRange.start')"
                :end-placeholder="$t('label.dateRange.end')"
              ></el-date-picker>
            </el-form-item>
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
              <el-input v-model="queryModel.equipmentId" />
            </el-form-item>
            <el-form-item label="设备名称" prop="EquipmentName">
              <el-input v-model="queryModel.EquipmentName" />
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
            ref="dataTable"
            :always="true"
            :data="tableData"
            max-height="100%"
            width="100%"
            height="100%"
            highlight-current-row
            :border="true"
            stripe
            @sort-change="onSortChange"
            :cell-class-name="tableCellStyle"
          >
            <el-table-column
              prop="EquipmentSerialNumber"
              :label="`设备Id`"
              sortable="custom"
              width="100"
            />
            <el-table-column
              prop="Equipment.EquipmentType.Description"
              :label="`设备类型`"
              sortable="custom"
              width="110"
            />
            <el-table-column
              prop="Equipment.Name"
              :label="`设备名称`"
              sortable="custom"
            />
            <el-table-column
              prop="EquipmentControlNumber"
              :label="`粉尘仪编号`"
              sortable="custom"
              width="120"
            />
            <el-table-column
              prop="Equipment.Location.Name"
              :label="`区域`"
              sortable="custom"
              width="80"
            />
            <el-table-column
              prop="Name"
              :label="`运行维护单位`"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="EndDate"
              :formatter="dateFormatUI"
              :label="`维护日期`"
              sortable="custom"
              width="170"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="100"
              v-has="['equipmentTPM:Export']"
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
                  @click.prevent="equipmentTMPExport(scope.$index)"
                  v-has="'equipmentTPM:Export'"
                >
                  导出
                </el-button>
              </template>
            </el-table-column>
          </el-table>
        </el-main>
        <el-footer>
          <el-pagination
            class="table-pagination"
            v-model:currentPage="pageInfo.current"
            v-model:pageSize="pageInfo.pageSize"
            :page-sizes="[10, 20, 50]"
            layout="total, prev, pager, next, sizes, jumper"
            :total="pageInfo.total"
            :pager-count="5"
          ></el-pagination>
        </el-footer>
      </el-container>
    </el-card>
  </div>
</template>

<script lang="ts">
import * as vue from "vue";

import { dateFormat, datetimeFormat } from "@/utils/formatter";
import { getActiveEntityMixin } from "@/utils/tableMixin";
import { EquipmentTPM } from "@/defs/Entity";
import { ElForm } from "element-plus";
import { EquipmentQueryModel } from "./QueryModel";
import ODataSelector from "@/components/ODataSelector.vue";
import moment from "moment";

import { equipmentTMPExport } from "@/utils/ExportPdf";

export default vue.defineComponent({
  name: "EquipmentTMPList",
  components: { ODataSelector },
  // mixins: [ListMixin],
  setup(props) {
    const {
      entityName,
      filterBuilder,
      loadData,
      onSortChange,
      pageInfo,
      query,
      tableData,
      queryModalVisible,
    } = getActiveEntityMixin<EquipmentTPM>();
    const equipmentSelection = vue.ref<EquipmentTPM>();

    const queryForm = vue.ref<typeof ElForm>();
    const queryModel = vue.ref<EquipmentQueryModel>({});

    const openDate = vue.ref<string>();

    const defaultExpand = `Equipment($select=Id,Name;$expand=Location($select=Name),EquipmentType($select=Description))`;
    query.$expand = defaultExpand;
    entityName.value = "EquipmentTPM";

    const editModalVisible = vue.ref(false);

    filterBuilder.value = () => {
      return new Promise((resolve) => {
        let filterStr = new Array<string>();
          filterStr.push(`(EndDate ne 0001-01-01T00:00:00Z)`);

        if (queryModel.value.equipmentId) {
          filterStr.push(`Equipment/SerialNumber eq '${queryModel.value.equipmentId}'`);
        }
        if (queryModel.value.EquipmentName) {
          filterStr.push(
            `contains(Equipment/Name, '${queryModel.value.EquipmentName}')`
          );
        }
        if (queryModel.value.locationId) {
          filterStr.push(
            `Equipment/LocationId eq ${queryModel.value.locationId}`
          );
        }
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
          filterStr.push(`EndDate ge ${moment(values[0]).toISOString()}`);
          var setDate = Date.parse(values[1]);
          endDate.setTime(setDate + 3600 * 1000 * 24 - 1000);
          filterStr.push(`EndDate le ${endDate.toISOString()}`);
        }

        resolve(filterStr);
      });
    };

    function onResetClick() {
      queryForm.value?.clearValidate();
      queryModel.value = {};
      loadData();
    }

    vue.onMounted(loadData);
    vue.onMounted(() => {
      vue.nextTick(() => {
        // regionTreeRef.value?.loadData();
      });
    });

    return {
      // regionTreeRef,
      tableData,
      queryModel,
      pageInfo,
      queryModalVisible,
      editModalVisible,
      equipmentSelection,
      queryForm,
      openDate,
      loadData,
      onResetClick,
      datetimeFormat,
      onSortChange,
    };
  },
  methods: {
    dateFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    tableCellStyle({ row, column, rowIndex, columnIndex }) {
      var date = new Date();
      if (
        (row.ScheduledDate && date > new Date(row.ScheduledDate)) ||
        (row.EarlyExecutionDate && date < new Date(row.EarlyExecutionDate))
      ) {
        if (columnIndex === 9) {
          return "alertClass";
        } else {
          return "alertRow";
        }
      } else {
        return "";
      }
    },
    getDate(date) {
      var datetime = new Date(date);
      const year = datetime.getFullYear();
      const month = (datetime.getMonth() + 1).toString().padStart(2, "0");
      const day = datetime.getDate().toString().padStart(2, "0");
      return `${year}-${month}-${day}`;
    },
    getTime(date) {
      var datetime = new Date(date);

      const hours = datetime.getHours().toString().padStart(2, "0");
      const minutes = datetime.getMinutes().toString().padStart(2, "0");
      const seconds = datetime.getSeconds().toString().padStart(2, "0");
      return `${hours}:${minutes}`;
    },
    equipmentTMPExport(index) {
      var equipmentTPM = this.tableData[index] as EquipmentTPM;
      equipmentTMPExport(equipmentTPM);
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

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
.alertClass {
  color: red;
  background-color: #ffe8e4 !important;
}
.alertRow {
  background-color: #ffe8e4 !important;
}
</style>
