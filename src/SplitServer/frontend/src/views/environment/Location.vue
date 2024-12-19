<template>
  <div>
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>区域</span>
          <el-switch
            :style="{ float: 'right' }"
            v-model="queryModel.IsActive"
            :active-value="true"
            :inactive-value="false"
            :active-text="$t('template.showDisabled')"
            @change="onSortChange"
          />
        </div>
      </template>
      <el-container>
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
            :row-class-name="deactiveRowClassName"
            @sort-change="onSortChange"
            :cell-class-name="tableCellStyle"
          >
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
                    v-has="'Location:Add'"
                    type="primary"
                    link
                    @click="onAddClick"
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
                  v-has="'Location:Edit'"
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
                  @click.prevent="edit(scope.$index)"
                  v-has="'Location:Edit'"
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
    <LocationEditor
      v-model:visible="editModalVisible"
      v-model:model="locationSelection"
      v-model:createNew="createNew"
      @accept="onAcceptClick"
    />
  </div>
</template>

<script lang="ts">
import * as vue from "vue";
import { dateFormat, datetimeFormat } from "@/utils/formatter";

import LocationEditor from "./EditModal/LocationEditor.vue";
import { getActiveEntityMixin } from "@/utils/tableMixin";
import { Location } from "@/defs/Entity";
import { ElForm, selectGroupKey } from "element-plus";
import { LocationQueryModel } from "./QueryModel";
import { deactiveRowClassName } from "@/utils/tableMixin";

import {
  oDataPatch,
  oDataPost,
  oDataQuery,
  oDataBatchUpdate,
  oDataBatchDelete,
  oDataDelete,
} from "@/utils/odata";
import cloneDeep from "lodash.clonedeep";

export default vue.defineComponent({
  name: "Location",
  components: { LocationEditor },
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
    } = getActiveEntityMixin<Location>();
    const locationSelection = vue.ref<Location>();

    const queryForm = vue.ref<typeof ElForm>();
    const queryModel = vue.ref<LocationQueryModel>({});

    const openDate = vue.ref<string>();

    const defaultExpand = `LocationType($select=Id,Description)`;

    query.$expand = defaultExpand;
    entityName.value = "Location";

    const editModalVisible = vue.ref(false);
    const createNew = vue.ref(false);

    filterBuilder.value = () => {
      return new Promise((resolve) => {
        let filterStr = new Array<string>();
        if (queryModel.value.IsActive != true) {
          filterStr.push(`IsActive eq true`);
        }
        if (queryModel.value.locationId) {
          filterStr.push(`LocationId eq ${queryModel.value.locationId}`);
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
      createNew,
      queryModalVisible,
      editModalVisible,
      locationSelection,
      queryForm,
      openDate,
      loadData,
      onResetClick,
      datetimeFormat,
      onSortChange,
      deactiveRowClassName,
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
    onAddClick() {
      this.locationSelection = {
        Name: undefined,
        Description: undefined,
        LocationTypeId: undefined,
        // ClassificationId: undefined,
        // SecondClassificationId: undefined,
        Barcode: undefined,
        Code: undefined,
      } as Location;

      this.editModalVisible = true;
      this.createNew = true;
    },
    edit(index) {
      this.locationSelection = this.tableData[index];
      this.editModalVisible = true;
      this.createNew = false;
    },
    async deleteData(index) {
      let data = cloneDeep(vue.toRaw(this.tableData[index]));
      let rsp = (await oDataDelete("Location", [data.Id])) as any;
      if (rsp.status === 204) {
        this.$message.success("成功");
        this.loadData();
      }
    },
    setIsActive(index, isActive) {
      let id = this.tableData[index].Id;
      let data = {
        Id: id,
        IsActive: isActive,
      };
      this.$update("Location", data).then(() => {
        this.$message.success("成功");
        this.loadData();
      });
    },
    async onAcceptClick() {
      this.editModalVisible = false;
      let data = cloneDeep(vue.toRaw(this.locationSelection));
      delete data.LocationType;
      delete data.SiteType;
      // delete data.Classification;
      // delete data.SecondClassification;
      delete data.Parent;

      let api = this.createNew ? oDataPost : oDataPatch;
      let rsp = (await api("Location", data)) as any;
      this.$message.success("成功");
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
