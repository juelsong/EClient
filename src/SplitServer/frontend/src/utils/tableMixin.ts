import { ref, reactive, computed, watch, Ref } from "vue";
import { OdataQuery } from "odata";
import { dateFormat, datetimeFormat } from "@/utils/formatter";
import { i18n } from "@/i18n";
import {
  oDataQuery,
  oDataPatch,
  oDataBatchDelete,
  BatchDelete,
} from "@/utils/odata";
import { ElMessage, ElMessageBox } from "element-plus";
function doNothing() {}
declare type OrderArg = {
  column: any;
  prop?: string;
  order: "ascending" | "descending" | null;
};

function getTableMixin<T>() {
  const pageInfo = reactive({
    pageSize: 10,
    total: 0,
    current: 1,
  });
  const tableData = ref(new Array<T>());
  const queryModalVisible = ref(false);
  const withoutPaging = ref(false);
  const query = reactive<OdataQuery>({});
  const multipleSelection = ref<T[]>([]);
  const skip = computed(() => pageInfo.pageSize * (pageInfo.current - 1));
  const filterBuilder = ref<() => Promise<string[]>>();
  const entityName = ref<string>();
  watch(
    skip,
    (val) => {
      query.$skip = val;
    },
    { immediate: false }
  );
  watch(
    () => pageInfo.current,
    () => {
      loadData();
    },
    { immediate: false }
  );
  watch(
    () => pageInfo.pageSize,
    (val) => {
      const loadFlag = pageInfo.current === 1;
      pageInfo.current = 1;
      query.$top = val;
      if (loadFlag) {
        loadData();
      }
    },
    { immediate: false }
  );

  const loadData = async () => {
    if (!entityName.value) {
      return;
    }
    const filter =
      filterBuilder.value === undefined
        ? undefined
        : await filterBuilder.value();
    if (filter !== undefined && filter.length > 0) {
      const filterStr =
        filter.length > 1
          ? filter.map((f) => `(${f})`).join(" and ")
          : filter[0];
      query.$filter = filterStr;
    } else {
      delete query.$filter;
    }
    query.$count = true;
    if (withoutPaging.value) {
      delete query.$skip;
      delete query.$top;
    } else {
      query.$skip = skip.value;
      query.$top = pageInfo.pageSize;
    }
    const data = await oDataQuery(entityName.value, query);
    tableData.value.splice(0, tableData.value.length, ...data.value);
    if (data["@odata.count"] != undefined) {
      pageInfo.total = data["@odata.count"];
    }
  };
  const onSelectionChange = (val?: T[]) => {
    const multipleSelectionRef = multipleSelection as Ref<T[]>;
    if (val === undefined) {
      multipleSelectionRef.value.splice(0, multipleSelectionRef.value.length);
    } else {
      multipleSelectionRef.value.splice(
        0,
        multipleSelectionRef.value.length,
        ...val
      );
    }
  };
  const onSortChange = (sortArgs: OrderArg) => {
    if (sortArgs && sortArgs.prop) {
      query.$orderby = `${sortArgs.prop} ${
        sortArgs.order == "descending" ? "desc" : "asc"
      }`;
    } else {
      delete query.$orderby;
    }
    return loadData();
  };
  const booleanFormat = (row: any, column: any) => {
    if (null == row[column.property]) {
      return "";
    }
    return row[column.property] === true
      ? i18n.global.t("label.yes")
      : i18n.global.t("label.no");
  };
  return {
    entityName,
    pageInfo,
    tableData,
    queryModalVisible,
    withoutPaging,
    query,
    multipleSelection,
    skip,
    filterBuilder,
    dateFormat,
    datetimeFormat,
    booleanFormat,
    loadData,
    onSortChange,
    onSelectionChange,
  };
}

export function getViewMixin<T>() {
  return getTableMixin<T>();
}

export function getEntityMixin<T>() {
  const common = getTableMixin<T>();
  const editModalVisible = ref(false);
  const createNew = ref(false);

  const deleteRow = (index: number) => {
    const tableDataRef = common.tableData as Ref<T[]>;
    return deletCore([tableDataRef.value[index]["Id"]]);
  };
  const batchDelete = async () => {
    const multipleSelection = common.multipleSelection as Ref<T[]>;
    if (multipleSelection.value.length > 0) {
      let ids = multipleSelection.value.map((item) => item["Id"]);
      await deletCore(ids);
    } else {
      ElMessage.error(i18n.global.t("prompt.pleaseSelectDeletintData"));
    }
  };
  const deletCore = async (ids: number[]) => {
    if (!common.entityName.value) {
      return;
    }
    if (
      await ElMessageBox.confirm(
        i18n.global.t("confirm.batchDelete", [ids.length]),
        i18n.global.t("confirm.title"),
        {
          confirmButtonText: i18n.global.t("template.accept"),
          cancelButtonText: i18n.global.t("template.cancel"),
          type: "warning",
          confirmButtonClass: "el-button--danger",
        }
      )
    ) {
      const batchDelete: BatchDelete = {};
      batchDelete[common.entityName.value] = ids;

      const data = (await oDataBatchDelete(batchDelete)) as Response[];
      if (data.length === ids.length && data.every((d) => d.status === 204)) {
        ElMessage.success(i18n.global.t("prompt.success"));
        return common.loadData();
      } else {
        ElMessage.error(i18n.global.t("prompt.failed"));
      }
    }
  };

  return Object.assign(
    {
      editModalVisible,
      createNew,
      deleteRow,
      batchDelete,
    },
    common
  );
}
export function getActiveEntityMixin<T>() {
  const common = getEntityMixin<T>();
  const setIsActive = async (id: number, isActive: boolean) => {
    if (!common.entityName.value) {
      return;
    }
    const data = { Id: id, IsActive: isActive };

    const ret = await oDataPatch(common.entityName.value, data).catch(
      doNothing
    );
    if (ret !== undefined) {
      await common.loadData();
    }
    return ret as T;
  };
  return Object.assign(
    {
      setIsActive,
    },
    common
  );
}

declare type EntityColumnCls<T = any> = {
  row: T;
  rowIndex: number;
};

export function deactiveRowClassName(col: EntityColumnCls) {
  return col.row.IsActive === false ? "is-disabled" : "";
}
