<template>
  <div style="height: 100%">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ $t("ProductType.entity") }}</span>
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
            @row-click="onRowClick"
          >
            <el-table-column
              prop="Name"
              :label="$t('ProductType.column.Name')"
              sortable="custom"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="100"
              v-has="['Equipment:Add', 'Equipment:Edit', 'Equipment:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'ProductType:Add'"
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
                  v-has="'ProductType:Disable'"
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
                  v-has="'ProductType:Edit'"
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
    <ProductTypeEditor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      @accept="onEditAccept"
    />
  </div>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import ProductTypeEditor from "./EditModal/ProductTypeEditor.vue";

export default defineComponent({
  name: "ProductType",
  components: { ProductTypeEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "ProductType",
      queryModel: {
        Name: undefined,
        ChainOfCustody: undefined,
        IsActive: undefined,
      },
      query: {
        // $expand: "Location($select=Id,Name)",
        $select: "Id,Name,IsActive",
      },
      editModel: {
        Name: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

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
      delete data.Location;
      if (data.LocationId === undefined) {
        data.LocationId = null;
      }
    },
    handleActiveChange() {
      this.loadData();
    },
    onRowClick(row) {
      this.$emit("selectProductType", row.Id);
    },
    onClickCard() {
      let dataTable = this.$refs.dataTable;
      dataTable.setCurrentRow();
      this.$emit("selectProductType", null);
    },
    onDataLoaded() {
      if (this.tableData.data.length > 0) {
        this.$refs.dataTable.setCurrentRow(this.tableData.data[0]);
        this.$emit("selectProductType", this.tableData.data[0].Id);
      }
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
</style>
