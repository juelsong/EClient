<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>用户管理</span>
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
              prop="Account"
              :label="$t('User.column.Account')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="RealName"
              :label="$t('User.column.RealName')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="EmployeeId"
              :label="$t('User.column.EmployeeId')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="EMail"
              :label="$t('User.column.EMail')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Phone"
              :label="$t('User.column.Phone')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              :label="$t('User.column.Department')"
              prop="Department.Name"
              width="150"
            />
            <el-table-column
              prop="Title"
              :label="$t('User.column.Title')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              :label="$t('User.column.Location')"
              prop="Location.Name"
              width="150"
            />
            <el-table-column
              :label="$t('User.column.Roles')"
              :formatter="formatRolesName"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              :width="$store.getters.isESysSecurity ? 150 : 100"
              v-has="['User:Add', 'User:Edit', 'User:Password', 'User:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'User:Add'"
                    type="primary"
                    link
                    @click="onAddClick"
                    v-if="$store.getters.isESysSecurity"
                  >
                    {{ $t("template.add") }}
                  </el-button>
                </div>
              </template>
              <template #default="scope">
                <el-button
                  type="primary"
                  link
                  @click.prevent="
                    setIsActive(scope.$index, !scope.row.IsActive)
                  "
                  v-has="'User:Disable'"
                >
                  {{
                    scope.row.IsActive
                      ? $t("template.disable")
                      : $t("template.enable")
                  }}
                </el-button>
                <el-button
                  type="primary"
                  link
                  @click.prevent="editRow(scope.$index)"
                  v-has="'User:Edit'"
                >
                  {{ $t("template.edit") }}
                </el-button>
                <el-button
                  v-if="$store.getters.isESysSecurity"
                  type="primary"
                  link
                  @click.prevent="handlePasswordClick(scope.$index)"
                  v-has="'User:Password'"
                >
                  {{ $t("UserPassword.button.password") }}
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

export default defineComponent({
  name: "UserList",
  components: { EquipmentEditor, VueBarcode },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "User",
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
        EquipmentConfig: {
          Version: undefined,
        },
      },
      query: {
        $expand:
          "Department($select=Id,Name),Location($select=Id,Name),Roles($select=Id,Name;$filter=IsActive eq true)",
        $select:
          "Id,Account,RealName,EmployeeId,EMail,Phone,Title,DepartmentId,LocationId,IsActive",
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
