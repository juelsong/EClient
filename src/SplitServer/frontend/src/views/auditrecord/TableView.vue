<script lang="ts">
declare interface IQueryModel {
  startDate?: Date;
  endDate?: Date;
  action?: string;
  user?: string;
}
import { OdataQuery } from "odata";
import * as vue from "vue";
import Searcher from "@/components/Searcher.vue";
import { ElForm } from "element-plus";
import { oDataQuery } from "@/utils/odata";
import { IEntity, auditActions, showUserName } from "./AuditData";
import { getEntityMixin } from "@/utils/tableMixin";
import { exportPdf, PdfExportparameter } from "@/utils/pdf-exportor";
import { CellConfig } from "jspdf";
import { i18n } from "@/i18n";
export default vue.defineComponent({
  name: "TableView",
  components: { Searcher },
  props: {
    entity: {
      type: Object as vue.PropType<IEntity>,
      required: false,
    },
  },
  setup(props) {
    const {
      pageInfo,
      tableData,
      query,
      loadData,
      filterBuilder,
      entityName,
      onSortChange,
    } = getEntityMixin<any>();
    filterBuilder.value = async () => {
      const filterStr = new Array<string>();
      if (queryModel.value.user && queryModel.value.user.trim().length > 0) {
        const userOptions: OdataQuery = {
          $select: "Id",
          $filter: `(contains(Account,'${queryModel.value.user}')) or (contains(RealName,'${queryModel.value.user}'))`,
        };
        const usrRsp = await oDataQuery("User", userOptions);
        if (usrRsp.value && usrRsp.value.length > 0) {
          const userIds: string = usrRsp.value.map((u) => u.Id).join(",");
          filterStr.push(
            `(CreateBy in [${userIds}]) or (UpdateBy in [${userIds}])`
          );
        }
      }
      if (queryModel.value.startDate) {
        filterStr.push(
          `AuditTime ge ${queryModel.value.startDate.toISOString()}`
        );
      }
      if (queryModel.value.endDate) {
        filterStr.push(
          `AuditTime le ${queryModel.value.endDate.toISOString()}`
        );
      }
      if (queryModel.value.action && queryModel.value.action.length > 0) {
        filterStr.push(`Action eq ${queryModel.value.action}`);
      }
      return filterStr;
    };
    const queryModel = vue.ref<IQueryModel>({});
    const disabledStartDate = (time: Date) => {
      return time.getTime() > Date.now();
    };
    const queryForm = vue.ref<typeof ElForm>();

    const disabledEndDate = (time: Date) => {
      const greaterThenNow = disabledStartDate(time);
      if (queryModel.value.startDate) {
        return (
          greaterThenNow ||
          time.getTime() < queryModel.value.startDate.getTime()
        );
      } else {
        return greaterThenNow;
      }
    };
    const exportParam: PdfExportparameter = {
      entity: "",
      query: {},
      headers: [],
      processor: porcessExportData,
    };
    async function porcessExportData(data: any[]) {
      await showUserName(data);
      data.forEach((d) => {
        props.entity?.properties.forEach((p, idx) => {
          if (p.formatter) {
            d[p.name] = p.formatter(d, { property: p.name }, d[p.name], idx);
          }
        });
      });
    }
    async function exportAuditPdf() {
      if (props.entity) {
        const entityKey = props.entity?.name?.replaceAll(".", "/");
        exportParam.query = { ...query };
        exportParam.pdfTitle = i18n.global.t(
          `AuditRecord.entities.${entityKey}.Title`
        );
        exportParam.fileName = i18n.global.t(
          `AuditEntities.entitiseNames.${entityKey}`
        );
        exportParam.headers = props.entity.properties.map((p) => {
          const ret: CellConfig = {
            name: p.name,
            prompt: i18n.global.t(
              `AuditRecord.entities.${entityKey}.properties.${p.name}.Title`
            ),
            align: "left",
            padding: 0,
            width: 100,
          };
          return ret;
        });
        exportPdf(exportParam);
      }
    }

    async function searchAuditData() {
      if (queryForm.value && props.entity) {
        const valid = await queryForm.value.validate();
        if (!valid) {
          return;
        }
        await loadData();
      }
    }
    async function onResetSearchClick() {
      if (queryForm.value) {
        queryForm.value.resetFields();
        await loadData();
      }
    }
    vue.watch(
      () => props.entity,
      async (entity) => {
        delete query.$orderby;
        if (entity) {
          entityName.value = entity.name;
          exportParam.entity = entity.name;
          await searchAuditData();
        }
      }
    );

    vue.watch(
      () => tableData,
      async (newData) => {
        await showUserName(newData.value);
      },
      {
        deep: true,
      }
    );

    return {
      auditActions,
      tableData,
      queryModel,
      disabledStartDate,
      disabledEndDate,
      searchAuditData,
      queryForm,
      onResetSearchClick,
      pageInfo,
      onSortChange,
      exportAuditPdf,
    };
  },
});
</script>

<template>
  <el-card
    :style="{ height: '100%' }"
    :body-style="{ height: 'calc(100% - 69px)' }"
  >
    <template #header>
      <div class="card-header">
        <!-- <span>{{ $t("AuditRecord.title.Record") }}</span> -->

        <el-form ref="queryForm" :inline="true" :model="queryModel">
          <el-form-item
            :label="$t('AuditRecord.filter.Date')"
            class="hidden-md-and-down"
            prop="startDate"
            :style="{ marginRight: '12px' }"
          >
            <el-date-picker
              v-model="queryModel.startDate"
              type="date"
              :disabled-date="disabledStartDate"
              :range-separator="$t('label.dateRange.separator')"
              :start-placeholder="$t('label.dateRange.start')"
              :end-placeholder="$t('label.dateRange.end')"
            ></el-date-picker>
          </el-form-item>
          <el-form-item label="~" class="hidden-md-and-down" prop="endDate">
            <el-date-picker
              v-model="queryModel.endDate"
              type="date"
              :disabled-date="disabledEndDate"
              :range-separator="$t('label.dateRange.separator')"
              :start-placeholder="$t('label.dateRange.start')"
              :end-placeholder="$t('label.dateRange.end')"
            ></el-date-picker>
          </el-form-item>
          <el-form-item
            :label="$t('AuditRecord.filter.Type')"
            class="hidden-lg-down"
            prop="action"
          >
            <el-select :clearable="true" v-model="queryModel.action">
              <el-option
                v-for="action in auditActions"
                :key="action"
                :value="`'${action}'`"
                :label="$t(`AuditRecord.actions.${action}`)"
              />
            </el-select>
          </el-form-item>
          <el-form-item
            :label="$t('AuditRecord.filter.User')"
            class="hidden-md-and-down"
            prop="user"
          >
            <el-input v-model="queryModel.user" />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="searchAuditData">
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
            <el-button
              style="margin-left: 8px"
              type="primary"
              @click="exportAuditPdf"
            >
              {{ $t("Export.label.export") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="memo" />
              </el-icon>
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </template>
    <div style="height: 100%" v-show="entity != undefined">
      <div style="height: calc(100% - 52px); max-height: calc(100% - 52px)">
        <el-table
          :data="tableData"
          style="width: 100%; max-height: 100%"
          stripe
          :border="true"
          highlight-current-row
          @sort-change="onSortChange"
          height="100%"
        >
          <el-table-column
            v-for="p in entity?.properties ?? []"
            :key="p.name"
            :prop="p.name"
            sortable="custom"
            :label="
              $t(
                `AuditRecord.entities.${entity?.name?.replaceAll(
                  '.',
                  '/'
                )}.properties.${p.name}.Title`
              )
            "
            :formatter="p.formatter"
          />
        </el-table>
      </div>
      <el-pagination
        class="table-pagination"
        v-model:currentPage="pageInfo.current"
        v-model:pageSize="pageInfo.pageSize"
        :page-sizes="[10, 20, 50]"
        layout="total, prev, pager, next, sizes, jumper"
        :total="pageInfo.total"
        :pager-count="5"
      ></el-pagination>
    </div>
  </el-card>
</template>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.table-pagination {
  float: right;
  margin-top: 16px;
}
.el-card {
  height: 100%;
  :deep(.el-card__body) {
    height: calc(100% - 69px);
  }

  :deep(.el-form-item) {
    margin-bottom: 0px;
  }
  :deep(.el-date-editor) {
    --el-date-editor-width: 140px;
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

.el-descriptions__body > table {
  table-layout: fixed;
}

.el-table {
  :deep(.hidden-row) {
    display: none;
  }
}
</style>
