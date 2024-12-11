<template>
  <el-container>
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
        @selection-change="onSelectionChange"
        @current-change="onCurrentChange"
      >
        <el-table-column
          prop="Account"
          :label="$t('Subscription.column.UserName')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="RealName"
          :label="$t('User.column.RealName')"
          sortable="custom"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          width="60"
          v-has="'Subscription:Edit'"
        >
          <template #default="scope">
            <el-button
              type="primary"
              link
              @click.prevent="onClickRow(scope.$index)"
              v-has="'Subscription:Edit'"
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
</template>

<script lang="ts">
import { defineComponent } from "vue";
import map from "lodash.map";
import { ElTable } from "element-plus";
import { ListMixin } from "@/mixins/ListMixin";
export default defineComponent({
  name: "SimpleUser",
  mixins: [ListMixin],
  emits: ["selected", "editSubscription"],
  data() {
    return {
      entityName: "User",
      queryModel: {},
      query: {
        $select: "Id,Account,RealName,IsActive",
      },
      editModel: {},
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = new Array<String>();
      filterStr.push("IsActive eq true");
      filterStr.push("Status eq 'Normal'");
      if (filterStr.length > 1) {
        return map(filterStr, (f) => `(${f})`).join(" and ");
      } else {
        return filterStr.join("");
      }
    },
    onCurrentChange(currentRow) {
      this.$emit("selected", currentRow?.Id);
    },
    clearSelection() {
      (this.$refs.dataTable as typeof ElTable).setCurrentRow();
    },
    onClickRow(idx: number) {
      const userId = (this.tableData as any).data[idx].Id;
      this.$emit("editSubscription", userId);
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
