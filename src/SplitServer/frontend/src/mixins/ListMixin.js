import map from "lodash.map";
import { toRaw } from "vue";
import cloneDeep from "lodash.clonedeep";

export const ListMixin = {
  data() {
    return {
      entityName: "",
      queryModalVisible: false,
      editModalVisible: false,
      createNew: false,
      tableData: {
        data: [],
        pageSize: 10,
        total: 0,
        current: 1,
      },
      multipleSelection: [],
      queryModel: {},
      filterMore: false,
    };
  },
  computed: {
    skip() {
      return this.tableData.pageSize * (this.tableData.current - 1);
    },
  },
  watch: {
    "tableData.pageSize": {
      handler(val) {
        let loadFlag = this.tableData.current == 1;
        this.tableData.current = 1;
        this.query.$top = val;
        if (loadFlag) {
          this.loadData();
        }
      },
      immediate: false,
    },
    skip: {
      handler(val) {
        this.query.$skip = val;
      },
      immediate: true,
    },
    "tableData.current": {
      handler() {
        this.loadData();
      },
      immediate: false,
    },
  },
  mounted() {
    this.query.$skip = this.skip;
    this.query.$top = this.tableData.pageSize;
    if (!this.disableMountedLoad) {
      this.loadData(this.query);
    }
  },
  methods: {
    loadData() {
      if (this.beforeLoadData) {
        this.beforeLoadData();
      }
      let filter =
        this.buildFilterStr == undefined ? undefined : this.buildFilterStr();
      if (filter && filter.length > 0) {
        this.query.$filter = filter;
      } else {
        delete this.query.$filter;
      }
      let q = Object.assign(this.query, { $count: true });
      if (this.withoutPaging) {
        delete q.$skip;
        delete q.$top;
      }
      return this.$query(this.entityName, q).then((data) => {
        if (this.processLoadedData) {
          this.processLoadedData(data.value);
        }
        this.tableData.data = data.value;
        this.tableData.total = data["@odata.count"];
        if (this.onDataLoaded) {
          this.onDataLoaded();
        }
        return Promise.resolve();
      });
    },
    onSelectionChange(val) {
      this.multipleSelection = val;
    },
    onToggleFilterMoreClick() {
      this.filterMore = !this.filterMore;
    },
    onCurrentPageChange(val) {
      this.tableData.current = val;
    },
    onPageSizeChange(val) {
      this.tableData.current = 1;
      this.tableData.pageSize = val;
    },
    onSortChange(sortArgs) {
      if (sortArgs && sortArgs.prop) {
        this.query.$orderby = `${sortArgs.prop} ${
          sortArgs.order == "descending" ? "desc" : "asc"
        }`;
      } else {
        delete this.query.$orderby;
      }
      this.loadData();
    },
    onSearchClick() {
      let that = this;
      this.$refs.queryForm.validate((valid) => {
        if (valid) {
          that.loadData();
        }
      });
    },
    onResetSearchClick() {
      // 高级查询中属性
      for (let prop in this.queryModel) {
        this.queryModel[prop] = undefined;
      }
      this.$refs.queryForm.resetFields();
      this.loadData();
    },
    onClearSelected() {
      this.$refs.dataTable.clearSelection();
    },
    deleteRow(index) {
      return this.deletCore([this.tableData.data[index].Id]);
    },
    async batchDelete() {
      if (this.multipleSelection && this.multipleSelection.length > 0) {
        let ids = map(this.multipleSelection, (item) => item.Id);
        await this.deletCore(ids);
      } else {
        this.$message.error(this.$t("prompt.pleaseSelectData"));
      }
    },
    async deletCore(ids) {
      if (
        await this.$confirm(
          this.$t("template.beforeDelete"),
          this.$t("template.tips"),
          {
            confirmButtonText: this.$t("template.accept"),
            cancelButtonText: this.$t("template.cancel"),
          }
        )
      ) {
        const data = await this.$delete(this.entityName, ids);
        // if (data.success) {
        this.loadData();
        // } else {
        //     this.$message.error(this.$t("prompt.failed"));
        // }
        return data;
      }
    },
    setIsActive(index, isActive) {
      let id = this.tableData.data[index].Id;
      let data = {
        Id: id,
        IsActive: isActive,
      };
      this.$update(this.entityName, data).then(() => {
        this.loadData();
      });
    },
    onAddClick() {
      this.createNew = true;
      for (let prop in this.editModel) {
        this.editModel[prop] = undefined;
      }
      if (this.onAddOverride) {
        this.onAddOverride();
      }
      this.editModalVisible = true;
    },
    editRow(index) {
      let data = cloneDeep(toRaw(this.tableData.data[index]));
      if (this.onEditRowOverride) {
        this.onEditRowOverride(data);
      }
      this.createNew = false;
      this.editModel = data;
      this.editModalVisible = true;
    },
    onEditAccept() {
      let data = cloneDeep(toRaw(this.editModel));
      if (this.onEditAcceptOverride) {
        this.onEditAcceptOverride(data);
      }
      let api = this.createNew ? this.$insert : this.$update;
      api(this.entityName, data).then(() => {
        this.loadData();
        this.$message.success(
          this.createNew
            ? this.$t("template.addSuccess")
            : this.$t("template.updateSuccess")
        );
      });
    },
  },
};
