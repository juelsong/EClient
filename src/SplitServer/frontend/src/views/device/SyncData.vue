<script lang="ts">
import * as vue from "vue";
import { oDataQuery } from "@/utils/odata";
import { ElLoading, ElMessage } from "element-plus";
import { EquipmentType, Equipment, User } from "@/defs/Entity";
import { getActiveEntityMixin } from "@/utils/tableMixin";
import { datetimeFormat } from "@/utils/formatter";
import { showPrompt } from "@/utils/esign-inject";
import { i18n } from "@/i18n";
import moment from "moment";

declare type ElLoadingType = ReturnType<typeof ElLoading.service>;
declare function showMainBackFunc(show: boolean): void;
declare type EquipmentTypeView = EquipmentType & {
  LastSyncUser?: User;
};
export default vue.defineComponent({
  name: "SyncData",
  setup() {
    const equipmentTypeTableMixin = getActiveEntityMixin<EquipmentType>();
    const equipmentTableMixin = getActiveEntityMixin<Equipment>();
    const equipmentTypeIds = vue.ref(new Array<number>());
    const selectedEquipmentType = vue.ref<EquipmentType>();
    const deviceSyncService = (window as any).deviceSyncService;
    const equipmentTypeViews = vue.ref(new Array<EquipmentTypeView>());
    const setAppMainInnerEnabled = vue.inject<typeof showMainBackFunc>(
      "setAppMainInnerEnabled",
      () => {}
    );
    equipmentTypeTableMixin.entityName.value = "EquipmentType";
    equipmentTypeTableMixin.filterBuilder.value = () =>
      new Promise<string[]>((resolve) => {
        const filterArray = [`IsActive eq true`];
        if (equipmentTypeIds.value.length) {
          filterArray.push(`Id in [${equipmentTypeIds.value.join(",")}]`);
        }
        resolve(filterArray);
      });
    equipmentTableMixin.entityName.value = "Equipment";
    equipmentTableMixin.filterBuilder.value = () =>
      new Promise<string[]>((resolve) => {
        const filterArray = [`IsActive eq true`, "DeviceConfigId ne null"];
        filterArray.push(
          `((NextCalibrationDate gt ${moment().toISOString()}) or (NextCalibrationDate eq null))`
        );
        if (selectedEquipmentType.value) {
          filterArray.push(
            `EquipmentTypeId eq ${selectedEquipmentType.value.Id}`
          );
        }
        resolve(filterArray);
      });

    async function loadEquipmentTypes() {
      const equipmentTypes = (
        await oDataQuery("Equipment", {
          $apply: `filter((IsActive eq true) and ((NextCalibrationDate gt ${moment().toISOString()}) or (NextCalibrationDate eq null)) and (DeviceConfigId ne null))/groupby((EquipmentTypeId))`,
        })
      ).value as Equipment[];

      equipmentTypeIds.value.splice(
        0,
        equipmentTypeIds.value.length,
        ...equipmentTypes
          .filter((e) => e.EquipmentTypeId)
          .map((e) => e.EquipmentTypeId!)
      );
    }

    function onEquipmentTypeRowClick(row: EquipmentType) {
      selectedEquipmentType.value = row;
    }
    let loadingInstance: ElLoadingType | undefined = undefined;
    let loadingCnt = 0;
    function showLoading() {
      loadingCnt++;
      loadingInstance = ElLoading.service({
        fullscreen: true,
      });
    }
    function closeLoading() {
      loadingCnt--;
      if (loadingCnt == 0) {
        loadingInstance?.close();
      }
    }
    function callback(code: number): void {
      closeLoading();
      if (code == 0) {
        ElMessage.success(i18n.global.t("prompt.success"));
      } else {
        showPrompt(code);
      }
      equipmentTableMixin.loadData();
    }
    async function onEquipmentSyncClick(row: Equipment) {
      showLoading();
      await deviceSyncService.syncDataAsync(row.Id, callback);
    }
    vue.onMounted(loadEquipmentTypes);
    vue.onMounted(() => setAppMainInnerEnabled(false));
    vue.watch(equipmentTypeIds, equipmentTypeTableMixin.loadData, {
      deep: true,
    });
    vue.watch(
      () => equipmentTypeTableMixin.pageInfo.current,
      () => {
        selectedEquipmentType.value = undefined;
      }
    );
    vue.watch(selectedEquipmentType, () => {
      equipmentTableMixin.loadData();
    });
    vue.watch(
      equipmentTableMixin.tableData,
      async (val) => {
        const userIds = Array.from(
          new Set(val.map((e) => e.LastSyncUserId).filter((id) => id))
        );
        let users = new Array<User>();
        if (userIds.length) {
          users = (
            await oDataQuery("User", {
              $filter: `Id in [${userIds.join(",")}]`,
            })
          ).value as User[];
        }

        equipmentTypeViews.value.splice(
          0,
          equipmentTypeViews.value.length,
          ...val.map((e) => {
            const view: EquipmentTypeView = {
              ...e,
              LastSyncUser: users.find((u) => u.Id == e.LastSyncUserId),
            };
            return view;
          })
        );
      },
      { deep: true }
    );
    return {
      equipmentTypeData: equipmentTypeTableMixin.tableData,
      equipmentTypePageInfo: equipmentTypeTableMixin.pageInfo,
      equipmentData: equipmentTypeViews,
      equipmentPageInfo: equipmentTableMixin.pageInfo,
      onEquipmentTypeRowClick,
      datetimeFormat,
      onEquipmentSyncClick,
    };
  },
});
</script>

<template>
  <el-row class="single-box-card" :gutter="10">
    <el-col :xs="8" :sm="8" :md="8" :lg="7" :xl="5">
      <el-card class="single-box-card" :header="$t('EquipmentType.entity')">
        <el-container>
          <el-main>
            <el-table
              :data="equipmentTypeData"
              height="100%"
              max-height="100%"
              width="100%"
              stripe
              highlight-current-row
              @row-click="onEquipmentTypeRowClick"
            >
              <el-table-column
                prop="Description"
                :label="$t('EquipmentType.column.Description')"
                sortable="custom"
              />
            </el-table>
          </el-main>
          <el-footer>
            <el-pagination
              class="table-pagination"
              v-model:currentPage="equipmentTypePageInfo.current"
              v-model:pageSize="equipmentTypePageInfo.pageSize"
              :page-sizes="[10, 20, 50]"
              layout="total, prev, pager, next, sizes, jumper"
              :total="equipmentTypePageInfo.total"
              :pager-count="5"
            ></el-pagination>
          </el-footer>
        </el-container>
      </el-card>
    </el-col>

    <el-col :xs="16" :sm="16" :md="16" :lg="17" :xl="19">
      <el-card class="single-box-card" :header="$t('Equipment.entity')">
        <el-container>
          <el-main>
            <el-table
              :data="equipmentData"
              height="100%"
              max-height="100%"
              width="100%"
              stripe
              highlight-current-row
            >
              <el-table-column
                prop="Name"
                :label="$t('Equipment.column.Name')"
                sortable="custom"
                width="250"
              />
              <el-table-column
                prop="Description"
                :label="$t('Equipment.column.Description')"
                sortable="custom"
              />
              <el-table-column
                prop="SerialNumber"
                :label="$t('Equipment.column.SerialNumber')"
                sortable="custom"
                width="150"
              />
              <el-table-column
                prop="LastSyncDataTimestamp"
                :label="$t('Equipment.column.LastSyncDataTimestamp')"
                sortable="custom"
                :formatter="datetimeFormat"
                width="180"
              />
              <el-table-column
                prop="LastSyncUser.RealName"
                :label="$t('Equipment.column.LastSyncUser')"
                width="100"
              />
              <el-table-column
                fixed="right"
                :label="$t('template.operation')"
                width="80"
              >
                <template #default="scope">
                  <el-button
                    type="primary"
                    link
                    @click="onEquipmentSyncClick(scope.row)"
                  >
                    {{ $t("Equipment.column.Upload") }}
                  </el-button>
                </template>
              </el-table-column>
            </el-table>
          </el-main>
          <el-footer>
            <el-pagination
              class="table-pagination"
              v-model:currentPage="equipmentPageInfo.current"
              v-model:pageSize="equipmentPageInfo.pageSize"
              :page-sizes="[10, 20, 50]"
              layout="total, prev, pager, next, sizes, jumper"
              :total="equipmentPageInfo.total"
              :pager-count="5"
            ></el-pagination>
          </el-footer>
        </el-container>
      </el-card>
    </el-col>
  </el-row>
</template>

<style lang="scss" scoped>
.single-box-card {
  .el-aside {
    height: 100%;
    .el-header {
      height: unset;
      padding: 0px;
    }

    .el-main {
      padding: 0px;
    }

    .el-footer {
      height: unset;
      padding: 0px;
    }
  }
}
</style>
