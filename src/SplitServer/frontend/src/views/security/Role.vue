<template>
  <div style="height: 100%">
    <el-card class="single-box-card subCard left">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Role.entity") }}</span>
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
            @row-click="handleRowClick"
          >
            <el-table-column
              prop="Name"
              :label="$t('Role.column.Name')"
              sortable="custom"
              width="150"
            />
            <el-table-column
              prop="Description"
              :label="$t('Role.column.Description')"
            />
            <el-table-column
              fixed="right"
              :label="$t('template.operation')"
              width="100"
              v-has="['Role:Add', 'Role:Edit', 'Role:Disable']"
            >
              <template #header>
                <div class="table-operation-header">
                  <span>{{ $t("template.operation") }}</span>
                  <el-button
                    v-has="'Role:Add'"
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
                  v-has="'Role:Disable'"
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
                  v-has="'Role:Edit'"
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
    <el-card class="single-box-card subCard right">
      <template #header>
        <div class="card-header">
          <span>{{ $t("Permission.entity") }}</span>
        </div>
      </template>
      <el-container>
        <el-main>
          <el-table
            ref="tbPermissions"
            :always="true"
            :data="permissions"
            row-key="Id"
            :row-class-name="deactiveRowClassName"
            highlight-current-row
            :border="true"
            stripe
            :tree-props="{ children: 'Children' }"
            default-expand-all
            height="100%"
            max-height="100%"
            width="100%"
          >
            <el-table-column
              type="selection"
              width="40"
              class-name="disable_click"
            />
            <!-- <el-table-column prop="Code"
                                 :label="$t('Permission.column.Feature')"
                                 width="350" /> -->
            <el-table-column :label="$t('Permission.column.Description')">
              <template #default="scope">
                {{
                  scope.row && scope.row.Code
                    ? $t(`permission.${scope.row.Code}`)
                    : ""
                }}
              </template>
            </el-table-column>
          </el-table>
        </el-main>
      </el-container>
    </el-card>
    <role-query
      v-model:visible="queryModalVisible"
      v-model:queryModel="queryModel"
      @search="loadData"
    />
    <role-editor
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
// import { AppMainMixin } from "@/mixins/AppMainMixin";
import RoleQuery from "./QueryModal/RoleQuery.vue";
import RoleEditor from "./EditModal/RoleEditor.vue";
import { getPermissionTree } from "@/api/permission";
export default defineComponent({
  name: "RoleList",
  components: { RoleQuery, RoleEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Role",
      queryModel: {
        IsActive: undefined,
      },
      query: {
        $expand: "Permissions($select=Id,Description)",
        $select: "Id,Name,Description,IsActive",
      },
      editModel: {
        Name: undefined,
        Description: undefined,
        Permissions: undefined,
      },
      permissions: [],
      updateFlag: undefined,
    };
  },
  mounted() {
    window.addEventListener("resize", this.handleWidowResize);
    this.loadPermissionData();
    // this.$query("Permission").then(
    //   (data) => {
    //     if (data.value) {
    //       data.value.forEach((val) => {
    //         val.selected = false;
    //       });
    //     }
    //     this.permissions = data.value;
    //   }
    // );
  },
  unmounted() {
    window.removeEventListener("resize", this.handleWidowResize);
  },
  methods: {
    i18t(permissions) {
      permissions.forEach((permission) => {
        if (permission.Children) this.i18t(permission.Children);
        permission.Description = this.$t("permission." + permission.Code);
      });
    },
    loadPermissionData() {
      getPermissionTree().then((permissions) => {
        this.i18t(permissions);
        this.permissions = permissions;
      });
    },
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
    onEditRowOverride(data) {
      data.Permissions = map(data.Permissions, (r) => r.Id);
      delete data.selected;
    },
    onEditAcceptOverride(data) {
      data.RolePermissions = map(data.Permissions, (r) => {
        return { PermissionId: r, RoleId: this.editModel.Id };
      });
      delete data.Permissions;
    },
    beforeLoadData() {
      this.$refs.tbPermissions.clearSelection();
    },
    handleActiveChange() {
      this.loadData();
    },
    checkPermissionsId(permissions, row) {
      permissions.forEach((permission) => {
        if (permission.Children)
          this.checkPermissionsId(permission.Children, row);
        if (row.Permissions.some((rp) => rp.Id == permission.Id)) {
          this.$refs.tbPermissions.toggleRowSelection(permission, true);
        }
      });
    },
    handleRowClick(row) {
      //console.log(event);
      this.tableData.data.forEach((d) => {
        d.selected = false;
      });
      row.selected = true;
      this.$refs.tbPermissions.clearSelection();
      this.checkPermissionsId(this.permissions, row);
    },
    getRowClassName({ row }) {
      if (true == row.selected) {
        return "table-selected";
      }
      return "";
    },
    handleWidowResize() {
      // if (!this.updateFlag) {
      //   this.updateFlag = true;
      //   this.$nextTick(() => {
      //     this.$refs.scrollbar.update();
      //     this.updateFlag = false;
      //   });
      // }
      // if (this.updateFlag) {
      //   clearTimeout(this.updateFlag);
      //   this.updateFlag = setTimeout(() => {
      //     console.log("update");
      //     this.$refs.scrollbar.update();
      //   }, 1000);
      // }
    },
  },
});
</script>
