<template>
  <div class="single-box-card">
    <!-- 查询区域 -->
    <el-container>
      <el-header>
        <el-switch
          :style="{ float: 'right' }"
          v-model="queryModel.IsActive"
          :active-value="true"
          :inactive-value="false"
          :active-text="$t('template.showDisabled')"
          @change="loadData"
        />
      </el-header>
      <el-main>
        <!-- table区域-begin -->
        <el-table
          ref="siteType"
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
            :label="$t('SiteType.column.Name')"
            sortable="custom"
          />
          <el-table-column width="100" :label="$t('SiteType.column.Icon')">
            <template #default="scope">
              <svg-icon v-if="scope.row.Icon" :icon-class="scope.row.Icon" />
            </template>
          </el-table-column>
          <el-table-column
            :label="$t('template.operation')"
            width="100"
            v-has="['SiteType:Add', 'SiteType:Edit', 'SiteType:Disable']"
          >
            <template #header>
              <div class="table-operation-header">
                <span>{{ $t("template.operation") }}</span>
                <el-button
                  v-has="'SiteType:Add'"
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
                v-has="'SiteType:Disable'"
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
                v-has="'SiteType:Edit'"
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
    <site-type-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      @accept="onEditAccept"
    />
  </div>
</template>

<script>
import SvgIcon from "@/components/SvgIcon";
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import SiteTypeEditor from "./EditModal/SiteTypeEditor.vue";
export default defineComponent({
  name: "SiteTypeList",
  components: { SiteTypeEditor, SvgIcon },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "SiteType",
      queryModel: {
        IsActive: undefined,
      },
      query: {
        $select: "Id,Name,Icon,IsActive",
      },
      editModel: {
        Name: undefined,
        Icon: undefined,
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
  },
});
</script>

<style lang="scss" scoped>
.table-pagination {
  float: right;
  margin-top: 16px;
}
</style>
