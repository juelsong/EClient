<template>
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
        @selection-change="onSelectionChange"
      >
        <el-table-column
          :label="$t('Subscription.column.UserName')"
          prop="User.Account"
          v-if="showUser"
          width="150"
        />
        <el-table-column
          :label="$t('Subscription.column.UserRealName')"
          prop="User.RealName"
          v-if="showUser"
        />
        <el-table-column
          :label="$t('Subscription.column.NotificationTypeName')"
          prop="NotificationType.Name"
          v-show="!showUser"
          width="150"
        />
        <el-table-column
          :label="$t('Subscription.column.NotificationTypeDescription')"
          prop="NotificationType.Description"
          v-show="!showUser"
          width="250"
        />
        <el-table-column
          :label="$t('Subscription.column.Location')"
          prop="Location.Location"
          width="150"
          v-if="locationId != undefined"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          width="100"
          v-has="'Subscription:Disable'"
        >
          <template #default="scope">
            <el-button
              type="primary"
              link
              @click.prevent="setIsActive(scope.$index, !scope.row.IsActive)"
              v-has="'Subscription:Disable'"
            >
              {{
                scope.row.IsActive
                  ? $t("template.disable")
                  : $t("template.enable")
              }}
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

export default defineComponent({
  name: "SubscriptionList",
  mixins: [ListMixin],
  props: {
    locationId: Number,
    showUser: Boolean,
    userId: Number,
    notificationTypeId: Number,
  },
  data() {
    return {
      isLoading: false,
      disableMountedLoad: true,
      entityName: "Subscription",
      queryModel: {},
      query: {
        $expand:
          "User($select=Id,RealName,Account),NotificationType($select=Id,Name,Description),Location($select=Id,Name)",
        $select: "Id,IsActive",
      },
      editModel: {},
    };
  },
  watch: {
    locationId() {
      this.locaDataOverride();
    },
    userId() {
      this.locaDataOverride();
    },
    notificationTypeId() {
      this.locaDataOverride();
    },
  },
  methods: {
    locaDataOverride() {
      if (this.isLoading) {
        return;
      }
      this.$nextTick(() => {
        if (this.userId || this.notificationTypeId) {
          return this.loadData();
        } else {
          const tmp = this.tableData as any;
          tmp.total = 0;
          tmp.current = 1;
          tmp.data.splice(0);
          return Promise.resolve();
        }
      }).then(() => {
        this.isLoading = false;
      });
    },
    buildFilterStr() {
      let filterStr = new Array<String>();
      filterStr.push("IsActive eq true");
      if (this.locationId) {
        filterStr.push(`LocationId eq ${this.locationId}`);
        filterStr.push("Location/IsActive eq true");
      } else {
        filterStr.push("LocationId eq null");
      }
      if (this.userId && !this.showUser) {
        filterStr.push(`UserId eq ${this.userId}`);
        filterStr.push("User/IsActive eq true");
        filterStr.push("User/Status eq 'Normal'");
      } else {
        filterStr.push("User/IsHidden eq false");
      }
      if (this.notificationTypeId && this.showUser) {
        filterStr.push(`NotificationTypeId eq ${this.notificationTypeId}`);
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
