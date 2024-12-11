<template>
  <el-container>
    <el-header>
      <el-form ref="queryForm" :inline="true" :model="queryModel">
        <el-row :gutter="24">
          <el-col :xl="4" :lg="5" :md="6" :sm="24">
            <el-form-item :label="$t('Department.filter.Name')" prop="Name">
              <el-input
                ref="queryModel.Name"
                v-model="queryModel.Name"
                :placeholder="$t('Department.filter.Name')"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :xl="4" :lg="5" :md="6" :sm="24">
            <el-form-item :label="$t('template.showDisabled')" prop="IsActive">
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
    </el-header>
    <el-main>
      <el-table
        ref="dataTable"
        :always="true"
        :data="tableData.data"
        :border="true"
        stripe
        :row-class-name="deactiveRowClassName"
        highlight-current-row
        height="100%"
        max-height="100%"
        width="100%"
        @sort-change="onSortChange"
      >
        <el-table-column
          prop="Name"
          :label="$t('Department.column.Name')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="Code"
          :label="$t('Department.column.Code')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          :label="$t('Department.column.Manager')"
          prop="Manager.RealName"
          width="150"
        />
        <el-table-column
          prop="Description"
          :label="$t('Department.column.Description')"
          sortable="custom"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          width="100"
          v-has="['Department:Add', 'Department:Edit', 'Department:Disable']"
        >
          <template #header>
            <div class="table-operation-header">
              <span>{{ $t("template.operation") }}</span>
              <el-button
                v-has="'Department:Add'"
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
              v-has="'Department:Disable'"
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
              v-has="'Department:Edit'"
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
    <department-query
      v-model:visible="queryModalVisible"
      v-model:queryModel="queryModel"
      @search="loadData"
    />
    <department-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      @accept="onEditAccept"
    />
  </el-container>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import SvgIcon from "@/components/SvgIcon";
import DepartmentQuery from "./QueryModal/DepartmentQuery.vue";
import DepartmentEditor from "./EditModal/DepartmentEditor.vue";
export default defineComponent({
  name: "DepartmentList",
  components: { SvgIcon, DepartmentQuery, DepartmentEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Department",
      queryModel: {
        Name: undefined,
        Code: undefined,
        IsActive: undefined,
      },
      query: {
        $expand: "Manager($select=Id,RealName)",
        $select: "Id,Name,Code,Description,ManagerId,IsActive",
      },
      editModel: {
        Name: undefined,
        Description: undefined,
        Code: undefined,
        ManagerId: undefined,
        Manager: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.Name && this.queryModel.Name.length > 0) {
        filterStr.push(`contains(Name,'${this.queryModel.Name}')`);
      }
      if (this.queryModel.Code && this.queryModel.Code.length > 0) {
        filterStr.push(`contains(Code,'${this.queryModel.Code}')`);
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
      delete data.Manager;
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
