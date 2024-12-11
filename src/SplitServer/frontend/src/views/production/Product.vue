<template>
  <div class="single-box-card">
    <el-card class="single-box-card">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Product.entity") }}</span>
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
        <!-- <el-header>
        <el-form ref="queryForm" :inline="true" :model="queryModel">
          <el-row :gutter="24">
            <el-col :xl="4" :lg="5" :md="6" :sm="24">
              <el-form-item :label="$t('Product.filter.Name')" prop="Name">
                <el-input
                  ref="queryModel.Name"
                  v-model="queryModel.Name"
                  :placeholder="$t('Product.filter.Name')"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :xl="4" :lg="5" :md="6" :sm="24">
              <el-form-item
                :label="$t('template.showDisabled')"
                prop="IsActive"
              >
                <el-switch
                  v-model="queryModel.IsActive"
                  :active-value="true"
                  :inactive-value="false"
                />
              </el-form-item>
            </el-col>
            <el-col :xl="6" :lg="8" :md="10" :sm="24">
              <span class="table-page-search-submitButtons">
                <el-button type="primary" @click="onSearchClick">
                  {{ $t("template.search") }}
                  <el-icon class="el-icon--right">
                    <svg-icon icon-class="edit" />
                  </el-icon>
                </el-button>
                <el-button
                  type="primary"
                  @click="onResetSearchClick"
                  style="margin-left: 8px"
                >
                  {{ $t("template.reset") }}
                  <el-icon class="el-icon--right">
                    <svg-icon icon-class="refresh" />
                  </el-icon>
                </el-button>
                <el-button
                  type="primary"
                  @click="queryModalVisible = true"
                  style="margin-left: 8px"
                >
                  {{ $t("template.advanced") }}
                  <el-icon class="el-icon--right">
                    <svg-icon icon-class="operation" />
                  </el-icon>
                </el-button>
              </span>
            </el-col>
          </el-row>
        </el-form>
      </el-header> -->
        <el-main>
          <!-- table区域-begin -->
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
              prop="Name"
              :label="$t('Product.column.Name')"
              sortable="custom"
              width="150"
            />
            <!-- <el-table-column
              prop="ChainOfCustody"
              :label="$t('Product.column.ChainOfCustody')"
              sortable="custom"
              width="150"
            /> -->
            <el-table-column
              :label="$t('Product.column.Location')"
              prop="Location.Name"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="100"
              v-has="['Product:Add', 'Product:Edit', 'Product:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'Product:Add'"
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
                  v-has="'Product:Disable'"
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
                  v-has="'Product:Edit'"
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
    <product-query
      v-model:visible="queryModalVisible"
      v-model:queryModel="queryModel"
      @search="loadData"
    />
    <product-editor
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
import ProductQuery from "./QueryModal/ProductQuery.vue";
import ProductEditor from "./EditModal/ProductEditor.vue";
export default defineComponent({
  name: "ProductList",
  components: { ProductQuery, ProductEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Product",
      queryModel: {
        ProductTypeId: undefined,
        Name: undefined,
        IsActive: undefined,
      },
      query: {
        $expand: "ProductType($select=Id,Name)",
        $select: "Id,Name,IsActive",
      },
      editModel: {
        Name: undefined,
        LocationId: undefined,
        Location: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.Name && this.queryModel.Name.length > 0) {
        filterStr.push(`contains(Name,'${this.queryModel.Name}')`);
      }
      if (this.queryModel.ProductTypeId) {
        filterStr.push(`ProductTypeId eq ${this.queryModel.ProductTypeId}`);
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
    searchProduct(id) {
      this.queryModel.ProductTypeId = id;
      this.loadData();
    },
  },
});
</script>

<style lang="scss" scoped>
.table-pagination {
  float: right;
  margin-top: 16px;
}
</style>
