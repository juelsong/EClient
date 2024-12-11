<template>
  <div>
    <!-- 查询区域 -->
    <div class="table-search">
      <el-form ref="queryForm" :inline="true" :model="queryModel">
        <el-row :gutter="24">
          <el-col :xl="6" :lg="7" :md="8" :sm="24">
            <el-form-item :label="$t('template.showDisabled')" prop="IsActive">
              <el-switch
                v-model="queryModel.IsActive"
                :active-value="true"
                :inactive-value="false"
                @change="handleActiveChange"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </div>
    <!-- table区域-begin -->
    <el-table
      ref="dataTable"
      :always="true"
      :data="tableData.data"
      :border="true"
      stripe
      :row-class-name="deactiveRowClassName"
      highlight-current-row
      style="width: 100%"
      @sort-change="onSortChange"
      @selection-change="onSelectionChange"
    >
      <el-table-column
        prop="Name"
        :label="$t('MapCategory.column.Name')"
        sortable="custom"
        width="250"
      />
      <el-table-column
        prop="Description"
        :label="$t('MapCategory.column.Description')"
      />
      <el-table-column
        fixed="right"
        :label="$t('template.operation')"
        width="100"
        v-has="['MapCategory:Add', 'MapCategory:Edit', 'MapCategory:Disable']"
      >
        <template #header>
          <div class="table-operation-header">
            <span>{{ $t("template.operation") }}</span>
            <el-button
              v-has="'MapCategory:Add'"
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
            @click.prevent="setIsActive(scope.$index, !scope.row.IsActive)"
            v-has="'MapCategory:Disable'"
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
            v-has="'MapCategory:Edit'"
          >
            {{ $t("template.edit") }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      class="table-pagination"
      v-model:currentPage="tableData.current"
      v-model:pageSize="tableData.pageSize"
      :page-sizes="[10, 20, 50]"
      layout="total, prev, pager, next, sizes, jumper"
      :total="tableData.total"
      :pager-count="5"
    ></el-pagination>
    <map-category-editor
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
import MapCategoryEditor from "./EditModal/MapCategoryEditor.vue";
export default defineComponent({
  name: "MapCategoryList",
  components: { MapCategoryEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "MapCategory",
      queryModel: {
        IsActive: undefined,
      },
      query: {
        $select: "Id,Name,Description,IsActive",
      },
      editModel: {
        Name: undefined,
        Description: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.IsActive != true) {
        filterStr.push(`IsActive eq true`);
      }
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    handleActiveChange() {
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
