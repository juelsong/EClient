<template>
  <div>
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>设备维护</span>
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
            <el-form-item label="设备Id" prop="equipmentId">
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
              prop="SerialNumber"
              label="设备ID"
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
              fixed="right"
              :label="$t('template.operation')"
              width="150"
              v-has="['equipmentTPM:Add']"
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
                  @click.prevent="equipmentTMP(scope.$index)"
                  v-has="'equipmentTPM:Add'"
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
    <TmpEditor
      v-model:visible="editModalVisible"
      v-model:model="equipmentSelection"
      @accept="onAcceptClick"
    />
  </div>
</template>

<script lang="ts">
import * as vue from "vue";

import { dateFormat, datetimeFormat } from "@/utils/formatter";

import TmpEditor from "./EditModal/TmpEditor.vue";
import { getActiveEntityMixin } from "@/utils/tableMixin";
import { Equipment, EquipmentTPM } from "@/defs/Entity";
import { ElForm } from "element-plus";
import { EquipmentQueryModel } from "./QueryModel";
import ODataSelector from "@/components/ODataSelector.vue";

export default vue.defineComponent({
  name: "EquipmentTMP",
  components: { TmpEditor, ODataSelector },
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
    } = getActiveEntityMixin<Equipment>();
    const equipmentSelection = vue.ref<Equipment>();

    const queryForm = vue.ref<typeof ElForm>();
    const queryModel = vue.ref<EquipmentQueryModel>({});

    const openDate = vue.ref<string>();

    const defaultExpand = `EquipmentType($select=Id,Description),Location($select=Id,Name)`;

    query.$expand = defaultExpand;
    entityName.value = "Equipment";

    const editModalVisible = vue.ref(false);

    filterBuilder.value = () => {
      return new Promise((resolve) => {
        let filterStr = new Array<string>();
        filterStr.push(`IsOperation eq true`);

        if (queryModel.value.locationId) {
          filterStr.push(`LocationId eq ${queryModel.value.locationId}`);
        }
        if (queryModel.value.EquipmentName) {
          filterStr.push(
            `contains(Name, '${queryModel.value.EquipmentName}')`
          );
        }
        if (queryModel.value.equipmentId) {
          filterStr.push(`SerialNumber eq '${queryModel.value.equipmentId}'`);
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
      return dateFormat(row, column);
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
    equipmentTMP(index) {
      this.equipmentSelection = this.tableData[index];
      this.editModalVisible = true;
    },
    onAcceptClick() {
      this.editModalVisible = false;
      this.loadData();

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
