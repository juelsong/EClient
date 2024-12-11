<template>
  <el-container>
    <el-header>
      <el-form
        class="card-header"
        ref="queryForm"
        :inline="true"
        :model="queryModel"
      >
        <el-form-item :label="$t('template.showDisabled')" prop="IsActive">
          <el-switch
            v-model="queryModel.IsActive"
            :active-value="true"
            :inactive-value="false"
            @change="onSearchClick"
          />
        </el-form-item>
      </el-form>
    </el-header>
    <el-main>
      <el-table
        ref="dataTable"
        :always="true"
        :data="tableData.data"
        :row-class-name="deactiveRowClassName"
        highlight-current-row
        :border="true"
        height="100%"
        max-height="100%"
        width="100%"
        stripe
        @sort-change="onSortChange"
      >
        <el-table-column
          prop="Name"
          :label="$t('OrgFoundMethod.column.Name')"
          sortable="custom"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          width="100"
          v-has="['Organism:Add', 'Organism:Edit', 'Organism:Disable']"
        >
          <template #header>
            <div class="table-operation-header">
              <span>{{ $t("template.operation") }}</span>
              <el-button
                v-has="'Organism:Add'"
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
              v-has="'Organism:Disable'"
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
              v-has="'Organism:Edit'"
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
    <OrgFoundMethod-editor
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
import OrgFoundMethodEditor from "./EditModal/OrgFoundMethodEditor.vue";
export default defineComponent({
  name: "OrgFoundMethodList",
  components: { OrgFoundMethodEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "OrgFoundMethod",
      queryModel: {
        Name: undefined,
      },
      query: {
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
  height: calc(
    /*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding - 2 *
      $--app-main-inner-padding - 55px
  );
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>

<style>
.el-tabs {
  height: 100%;
}
.el-Names__body > table {
  table-layout: fixed;
}
</style>
