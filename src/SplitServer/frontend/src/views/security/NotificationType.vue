<template>
  <el-container>
    <el-main>
      <el-table
        ref="dataTable"
        :always="true"
        :data="tableData.data"
        :row-class-name="deactiveRowClassName"
        :border="true"
        stripe
        highlight-current-row
        height="100%"
        max-height="100%"
        width="100%"
        @sort-change="onSortChange"
        @selection-change="onSelectionChange"
        @current-change="onCurrentChange"
      >
        <el-table-column
          prop="ZhName"
          :label="$t('NotificationType.column.Name')"
          sortable="custom"
          v-if="getLableDescription() == 'Zh'"
          width="150"
        />
        <el-table-column
          prop="ZhDescription"
          v-if="getLableDescription() == 'Zh'"
          :label="$t('NotificationType.column.Description')"
        />
        <el-table-column
          prop="EnName"
          :label="$t('NotificationType.column.Name')"
          sortable="custom"
          v-if="getLableDescription() == 'En'"
          width="150"
        />
        <el-table-column
          prop="EnDescription"
          v-if="getLableDescription() == 'En'"
          :label="$t('NotificationType.column.Description')"
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
import { ListMixin } from "@/mixins/ListMixin";
import { ElTable } from "element-plus";
import getLableDescription from "@/i18n/localeHelper";

export default defineComponent({
  name: "NotificationTypeList",
  mixins: [ListMixin],
  emits: ["selected", "editSubscription"],
  data() {
    return {
      entityName: "NotificationType",
      queryModel: {
        CreateBy: undefined,
      },
      query: {
        $select:
          "Id,EnName,EnDescription,ZhName,ZhDescription,CreateBy,CreatedTime,IsActive",
      },
      editModel: {
        CreateBy: undefined,
        CreatedTime: undefined,
        IsActive: undefined,
      },
      tableHeight: 600,
    };
  },
  methods: {
    getLableDescription,
    buildFilterStr() {
      let filterStr = new Array<String>();
      filterStr.push("IsActive eq true");
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
      const notificationTypeId = (this.tableData as any).data[idx].Id;
      this.$emit("editSubscription", notificationTypeId);
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
