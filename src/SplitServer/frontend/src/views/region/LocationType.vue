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
          ref="locationType"
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
            :label="$t('LocationType.column.Name')"
            sortable="custom"
            width="150"
          />
          <el-table-column
            prop="Weight"
            :label="$t('LocationType.column.Weight')"
            sortable="custom"
            width="150"
          >
            <template #default="scope">
              <span>
                {{ $t("LocationType.WeightFormat", [scope.row.Weight]) }}
              </span>
            </template>
          </el-table-column>
          <el-table-column width="100" :label="$t('LocationType.column.Icon')">
            <template #default="scope">
              <svg-icon :icon-class="scope.row.Icon" />
            </template>
          </el-table-column>
          <el-table-column
            prop="Description"
            :label="$t('LocationType.column.Description')"
          />
          <el-table-column
            :label="$t('template.operation')"
            width="100"
            v-has="[
              'LocationType:Add',
              'LocationType:Edit',
              'LocationType:Disable',
            ]"
          >
            <template #header>
              <div class="table-operation-header">
                <span>{{ $t("template.operation") }}</span>
                <el-button
                  v-has="'LocationType:Add'"
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
                v-has="'LocationType:Disable'"
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
                v-has="'LocationType:Edit'"
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
    <location-type-editor
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
import LocationTypeEditor from "./EditModal/LocationTypeEditor.vue";
export default defineComponent({
  name: "LocationTypeList",
  components: { LocationTypeEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "LocationType",
      queryModel: {
        IsActive: undefined,
      },
      query: {
        $select: "Id,Name,Description,Weight,Icon,IsActive",
      },
      editModel: {
        Name: undefined,
        Description: undefined,
        Weight: undefined,
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
