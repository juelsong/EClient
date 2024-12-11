<template>
  <el-container>
    <el-header>
      <el-form ref="queryForm" :inline="true" :model="queryModel">
        <el-row :gutter="24">
          <el-col :xl="4" :lg="5" :md="6" :sm="24">
            <el-form-item
              :label="$t('User.filter.Department')"
              prop="Department"
            >
              <o-data-selector
                :placeholder="`${$t('template.select', [
                  $t('User.filter.Department'),
                ])}`"
                :multiple="false"
                :clearable="true"
                v-model="queryModel.Department"
                entity="Department"
                label="Name"
                value="Id"
              />
            </el-form-item>
          </el-col>
          <el-col :xl="4" :lg="5" :md="6" :sm="24">
            <el-form-item :label="$t('User.filter.RealName')" prop="RealName">
              <el-input
                ref="queryModel.RealName"
                v-model="queryModel.RealName"
                :placeholder="$t('User.filter.RealName')"
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
          <el-col :xl="4" :lg="6" :md="8" :sm="24">
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
          prop="Account"
          :label="$t('User.column.Account')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="RealName"
          :label="$t('User.column.RealName')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="EmployeeId"
          :label="$t('User.column.EmployeeId')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="EMail"
          :label="$t('User.column.EMail')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="Phone"
          :label="$t('User.column.Phone')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          :label="$t('User.column.Department')"
          prop="Department.Name"
          width="150"
        />
        <el-table-column
          prop="Title"
          :label="$t('User.column.Title')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          :label="$t('User.column.Location')"
          prop="Location.Name"
          width="150"
        />
        <el-table-column
          :label="$t('User.column.Roles')"
          :formatter="formatRolesName"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          :width="$store.getters.isESysSecurity ? 150 : 100"
          v-has="['User:Add', 'User:Edit', 'User:Password', 'User:Disable']"
        >
          <template #header>
            <div class="table-operation-header">
              <span>{{ $t("template.operation") }}</span>
              <el-button
                v-has="'User:Add'"
                type="primary"
                link
                @click="onAddClick"
                v-if="$store.getters.isESysSecurity"
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
              v-has="'User:Disable'"
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
              v-has="'User:Edit'"
            >
              {{ $t("template.edit") }}
            </el-button>
            <el-button
              v-if="$store.getters.isESysSecurity"
              type="primary"
              link
              @click.prevent="handlePasswordClick(scope.$index)"
              v-has="'User:Password'"
            >
              {{ $t("UserPassword.button.password") }}
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
    <user-query
      v-model:visible="queryModalVisible"
      v-model:queryModel="queryModel"
      @search="loadData"
    />
    <user-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      @accept="onEditAccept"
    />
    <user-password
      v-model:visible="passModalVisible"
      :id="passModel.Id"
      :account="passModel.Account"
    />
  </el-container>
</template>

<script>
import { request } from "@/utils/request";
import { defineComponent, toRaw } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import ODataSelector from "@/components/ODataSelector.vue";
import UserQuery from "./QueryModal/UserQuery.vue";
import UserEditor from "./EditModal/UserEditor.vue";
import UserPassword from "./EditModal/UserPassword.vue";
import cloneDeep from "lodash.clonedeep";
import SvgIcon from "@/components/SvgIcon";
export default defineComponent({
  name: "UserList",
  components: { ODataSelector, SvgIcon, UserQuery, UserEditor, UserPassword },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "User",
      passModalVisible: false,
      queryModel: {
        Department: undefined,
        RealName: undefined,
        IsActive: undefined,
      },
      query: {
        $expand:
          "Department($select=Id,Name),Location($select=Id,Name),Roles($select=Id,Name;$filter=IsActive eq true)",
        $select:
          "Id,Account,RealName,EmployeeId,EMail,Phone,Title,DepartmentId,LocationId,IsActive",
      },
      editModel: {
        Account: undefined,
        RealName: undefined,
        Password: undefined,
        Password2: undefined,
        EmployeeId: undefined,
        EMail: undefined,
        Phone: undefined,
        DepartmentId: undefined,
        Title: undefined,
        Location: undefined,
        Roles: undefined,
      },
      passModel: {
        Id: undefined,
        Account: undefined,
        Password: undefined,
        Password2: undefined,
      },
    };
  },
  methods: {
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.Department) {
        filterStr.push(`DepartmentId eq ${this.queryModel.Department}`);
      }
      if (this.queryModel.RealName && this.queryModel.RealName.length > 0) {
        filterStr.push(`contains(RealName,'${this.queryModel.RealName}')`);
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
    formatRolesName(row) {
      return map(row.Roles, (role) => role.Name).join(",");
    },
    onEditRowOverride(data) {
      data.Roles = map(data.Roles, (r) => r.Id);
    },
    onEditAcceptOverride(data) {
      delete data.Department;
      delete data.Location;
      delete data.Password2;
      data.UserRoles = map(data.Roles, (r) => {
        return { RoleId: r, UserId: this.editModel.Id };
      });
      delete data.Roles;
    },
    onEditAccept() {
      let data = cloneDeep(toRaw(this.editModel));
      let that = this;
      if (this.onEditAcceptOverride) {
        this.onEditAcceptOverride(data);
      }
      if (data.LocationId === undefined) {
        data.LocationId = null;
      }

      if (this.createNew) {
        request({
          url: `/api/user`,
          method: "post",
          data,
          headers: { "Content-Type": "application/json;" },
        }).then((ret) => {
          that.loadData();
        });
      } else {
        delete data.Password;
        this.$update(this.entityName, data).then((ret) => {
          that.loadData();
        });
      }
    },
    handlePasswordClick(index) {
      let data = toRaw(this.tableData.data[index]);
      this.passModel.Id = data.Id;
      this.passModel.Account = data.Account;
      this.passModel.Password = undefined;
      this.passModel.Password2 = undefined;
      this.passModalVisible = true;
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
