<script lang="ts">
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import { defineComponent, ref, computed } from "vue";
import { request } from "@/utils/request";
import Searcher from "@/components/Searcher.vue";

import { IEntity, IProperty, formatterFunc } from "./AuditData";
// import DataView from "./DataView.vue";
import DataView from "./DataView.vue";
import TableView from "./TableView.vue";
export default defineComponent({
  name: "AuditRecord",
  components: { Searcher, TableView, DataView },
  setup() {
    const currentEntity = ref<IEntity>();
    const activeName = ref("all");
    const entityName = computed(() => {
      return currentEntity.value?.name;
    });
    return {
      currentEntity,
      activeName,
      entityName,
    };
  },
  data() {
    return {
      entities: new Array<IEntity>(),
      filteredEntities: new Array<IEntity>(),
      disableMountedLoad: true,
      query: {},
      entityData: new Array<any>(),
    };
  },
  mounted() {
    this.loadEntitiesData().then(() => {
      this.onSearch("");
    });
  },
  methods: {
    auditFormatter: function (row, column) {
      if (this.getCanFormatter(row.name)) {
        return this.$t(`AuditEntities.entitiseNames.${row.name}`);
      } else {
        return "";
      }
    },
    tableRowClassName: function (row, index) {
      if (row.row.name && !this.getCanFormatter(row.row.name)) {
        return "hidden-row";
      }
      return "";
    },
    getCanFormatter(name) {
      switch (name) {
        case "EquipmentAudit":
        case "EquipmentTypeAudit":
        case "LimitTokenAudit":
        case "ClassificationAudit":
        case "LocationAudit":
        case "LocationTypeAudit":
        case "SiteAudit":
        case "SiteTypeAudit":
        case "MediaAudit":
        case "MediaTypeAudit":
        case "OrganismAudit":
        case "OrganismCharAudit":
        case "OrganismGenusAudit":
        case "OrganismSeverityAudit ":
        case "OrganismTypeAudit":
        case "OrgFoundMethodAudit":
        case "SampleAudit":
        case "SeverityLocationAudit":
        case "ProductAudit":
        case "LimitDefAudit":
        case "MeasurementAudit":
        case "UserAudit":
        case "UserRoleAudit":
        case "NotificationAudit":
        case "SubscriptionAudit":
        case "LimitAudit":
        case "PlanAudit":
        case "RolePermissionAudit":
        case "RoleAudit":
          return true;
      }
      return false;
    },
    async loadEntitiesData() {
      this.entities.splice(0);
      const meta = (await request({
        url: "OData/$metadata",
        params: {
          lang: "json",
        },
        method: "GET",
      })) as object;

      for (const key in meta) {
        if (key.includes(".EMIS.")) {
          const ns = meta[key];
          for (const entityName in ns) {
            if (entityName.endsWith("Audit")) {
              const entityItem = ns[entityName] as object;
              const entity: IEntity = {
                name: `${entityName}`,
                upperName: `${entityName}`.toUpperCase(),
                properties: Object.keys(entityItem)
                  .filter(
                    (propertyName) =>
                      !propertyName.includes("$") &&
                      !propertyName.includes("@") &&
                      propertyName != "Id"
                  )
                  .map((s) => {
                    const propertyMeta = entityItem[s];
                    const p: IProperty = {
                      name: s,
                      formatter: this.getFormatter(propertyMeta["$Type"]),
                    };
                    return p;
                  }),
              };
              this.entities.push(entity);
            }
          }
        }
      }
    },
    getFormatter(edmType: string): formatterFunc | undefined {
      if (edmType) {
        if (edmType == "Edm.Boolean") {
          return this.boolFormat;
        } else if (edmType == "Edm.DateTimeOffset") {
          return this.datetimeFormatUI;
        } else if (edmType.endsWith(".AuditAction")) {
          return this.actionFormat;
        }
      }
      return undefined;
    },
    datetimeFormatUI: function (row, column) {
      return datetimeFormat(row, column);
    },
    boolFormat: function (row, column) {
      if (null == row[column.property]) {
        return "";
      }
      return this.$t(`label.${true == row[column.property] ? "yes" : "no"}`);
    },
    actionFormat(row, column) {
      if (null == row[column.property]) {
        return "";
      }
      return this.$t(`AuditRecord.actions.${row[column.property]}`);
    },
    onSearch(val: string) {
      const upperVal = val.toUpperCase();
      const that = this;
      this.filteredEntities =
        val.length > 0
          ? this.entities.filter(
              (e) =>
                e.upperName.includes(upperVal) ||
                that.$t(`AuditEntities.entitiseNames.${e.name}`).includes(val)
            )
          : this.entities;
      if (
        this.currentEntity &&
        this.filteredEntities.every((a) => a.name != this.currentEntity?.name)
      ) {
        this.currentEntity = undefined;
      }

      // TODO scroll to currentEntity
    },
    onCurrentChange(currentRow: IEntity) {
      this.currentEntity = currentRow;
    },
  },
});
</script>

<template>
  <div :style="{ height: '100%' }">
    <el-card :style="{ width: '400px', float: 'left' }">
      <template #header>
        <div class="card-header">
          <span>{{ $t("AuditRecord.title.Type") }}</span>
          <searcher @search="onSearch" style="width: calc(100% - 60px)" />
        </div>
      </template>
      <el-table
        ref="entityTable"
        :data="filteredEntities"
        style="width: 100%; max-height: 100%"
        stripe
        :border="true"
        highlight-current-row
        @current-change="onCurrentChange"
        height="100%"
        :row-class-name="tableRowClassName"
      >
        <el-table-column
          prop="name"
          :label="$t('AuditRecord.Name')"
          :formatter="auditFormatter"
        />
      </el-table>
    </el-card>

    <el-tabs v-model="activeName">
      <el-tab-pane :label="$t('AuditRecord.title.TableView')" name="all">
        <table-view :entity="currentEntity" />
      </el-tab-pane>
      <el-tab-pane :label="$t('AuditRecord.title.HistoryView')" name="group">
        <data-view :entity="currentEntity" />
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.el-card {
  height: 100%;
  :deep(.el-card__body) {
    height: calc(100% - 73px);
  }
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.el-tabs {
  height: 100%;
  width: calc(100% - 424px);
  float: left;
  margin-left: 24px;
  :deep(.el-tabs__content) {
    height: calc(100% - 55px);
    .el-tab-pane {
      height: 100%;
      .el-card {
        height: 100%;
      }
    }
  }
}

.el-table {
  :deep(.hidden-row) {
    display: none;
  }
}
</style>
