<script lang="ts">
import * as vue from "vue";
import { i18n } from "@/i18n";
import { datetimeFormat } from "@/utils/formatter";
import {
  ApprovalEntityParameter,
  getApprovalRecord,
  ApprovalRecord,
} from "@/utils/ApprovalRecord";
export default vue.defineComponent({
  name: "ApprovalRecord",
  props: {
    parameter: { type: Object as vue.PropType<ApprovalEntityParameter> },
    autoLoad: {
      type: Boolean,
      default: true,
    },
  },
  setup(props, ctx) {
    const approvalRecords = vue.ref<ApprovalRecord[]>([]);
    async function updateRecords() {
      const items =
        props.parameter && props.parameter.EntityId
          ? await getApprovalRecord(props.parameter)
          : [];
      approvalRecords.value.splice(0, approvalRecords.value.length, ...items);
    }
    function statusFormatter(data: ApprovalRecord) {
      if (data.DisplayStatus) {
        return i18n.global.t(`Approval.Status.${data.DisplayStatus}`);
      }
    }
    function operationFormatter(data: ApprovalRecord) {
      if (data.Operation) {
        return i18n.global.t(`Approval.Operation.${data.Operation}`);
      }
    }
    vue.onMounted(() => {
      if (props.autoLoad) {
        updateRecords();
      }
    });
    vue.watch(() => props.parameter, updateRecords, { deep: true });
    return {
      approvalRecords,
      statusFormatter,
      operationFormatter,
      datetimeFormat,
      updateRecords,
    };
  },
});
</script>

<template>
  <div style="height: 100%">
    <el-card class="single-box-card" :header="$t('Approval.Title')">
      <el-table
        :data="approvalRecords"
        height="100%"
        max-height="100%"
        width="100%"
        stripe
        highlight-current-row
      >
        <el-table-column
          prop="DisplayStatus"
          :label="$t('Approval.Column.Status')"
          :formatter="statusFormatter"
        />
        <el-table-column
          prop="User"
          :label="$t('Approval.Column.User')"
          width="100"
        />
        <el-table-column
          prop="Operation"
          :label="$t('Approval.Column.Operation')"
          width="100"
          :formatter="operationFormatter"
        />
        <el-table-column
          prop="Data.Comment"
          :label="$t('Approval.Column.Comment')"
        />
        <el-table-column
          prop="OperationTime"
          :label="$t('Approval.Column.Timestamp')"
          :formatter="datetimeFormat"
          width="200"
          fixed="right"
        />
      </el-table>
    </el-card>
  </div>
</template>
