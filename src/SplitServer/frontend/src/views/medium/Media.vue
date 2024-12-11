<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Media.entity") }}</span>
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
            :data="tableData.data"
            :row-class-name="deactiveRowClassName"
            highlight-current-row
            :border="true"
            stripe
            height="100%"
            max-height="100%"
            width="100%"
            @sort-change="onSortChange"
            @selection-change="onSelectionChange"
          >
            <el-table-column
              prop="MediaType.Name"
              :label="$t('Media.column.MediaType')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Name"
              :label="$t('Media.column.Name')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Location.Name"
              :label="$t('Media.column.Location')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Barcode"
              :label="$t('Media.column.Barcode')"
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
              prop="LotNumber"
              :label="$t('Media.column.LotNumber')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="LotNumberExpDate"
              :formatter="dateFormatUI"
              :label="$t('Media.column.LotNumberExpDate')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Vendor"
              :label="$t('Media.column.Vendor')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="100"
              v-has="['Media:Add', 'Media:Edit', 'Media:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'Media:Add'"
                    type="primary"
                    link
                    :disabled="MediaTypeId == undefined"
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
                  v-has="'Media:Disable'"
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
                  v-has="'Media:Edit'"
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
    <media-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      v-model:MediaTypeId="MediaTypeId"
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
import MediaEditor from "./EditModal/MediaEditor.vue";
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import moment from "moment";
export default defineComponent({
  name: "MediaList",
  components: { MediaEditor, VueBarcode },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Media",
      barcodeVisable: false,
      barcode: "",
      MediaTypeId: undefined,
      queryModel: {
        MediaTypeId: undefined,
        Name: undefined,
        LocationId: undefined,
        Barcode: undefined,
        LotNumber: undefined,
        LotNumberExpDate: undefined,
        Vendor: undefined,
        CalibrationValue: undefined,
        ControlNumber: undefined,
        IsActive: undefined,
      },
      query: {
        $expand:
          "MediaType($select=Id,Name),GrowthPromotionStatus($select=Id,Name),Location($select=Id,Name)",
        $select:
          "Id,MediaTypeId,Name,Barcode,LotNumber,LotNumberExpDate,LocationId,Vendor," +
          "ManufacturerLotNumber,GrowthPromotionStatusId,GrowthPromotionResult,Inventory,InventoryAdj,IsActive",
      },
      editModel: {
        MediaTypeId: undefined,
        Name: undefined,
        LocationId: undefined,
        Barcode: undefined,
        LotNumber: undefined,
        LotNumberExpDate: undefined,
        Vendor: undefined,
        ManufacturerLotNumber: undefined,
        GrowthPromotionStatus: undefined,
        GrowthPromotionResult: undefined,
        Inventory: undefined,
        InventoryAdj: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.MediaTypeId) {
        filterStr.push(`MediaTypeId eq ${this.queryModel.MediaTypeId}`);
      }
      if (this.queryModel.Name && this.queryModel.Name.length > 0) {
        filterStr.push(`contains(Name,'${this.queryModel.Name}')`);
      }
      if (this.queryModel.IsActive != true) {
        filterStr.push(`IsActive eq true`);
      }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    onEditAcceptOverride(data) {
      if (data.LocationId === undefined) {
        data.LocationId = null;
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
    searchMediaType(id) {
      this.queryModel.MediaTypeId = id;
      this.editModel.MediaTypeId = id;
      this.MediaTypeId = id;
      this.loadData();
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
