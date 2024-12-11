<template>
  <el-container>
    <el-header>
      <el-form
        class="card-header"
        ref="queryForm"
        :inline="true"
        :model="queryModel"
      >
        <el-form-item :label="$t('template.showDisabled')" prop="IsActive">
          <el-switch
            v-model="queryModel.IsActive"
            :active-value="true"
            :inactive-value="false"
            @change="onSearchClick"
          />
        </el-form-item>
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
          prop="OrganismGenus.OrganismType.Description"
          :label="$t('Organism.column.OrganismType')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="OrganismGenus.Description"
          :label="$t('Organism.column.OrganismGenus')"
          sortable="custom"
          width="170"
        />
        <el-table-column
          prop="Species"
          :label="$t('Organism.column.Species')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="Strain"
          :label="$t('Organism.column.Strain')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="Description"
          :label="$t('Organism.column.Description')"
          sortable="custom"
          width="150"
        />
        <el-table-column
          prop="OrganismSeverity.Zh"
          :label="$t('Organism.column.OrganismSeverity')"
          sortable="custom"
          v-if="getLableDescription() == 'Zh'"
          width="170"
        />
        <el-table-column
          prop="OrganismSeverity.En"
          :label="$t('Organism.column.OrganismSeverity')"
          sortable="custom"
          v-if="getLableDescription() == 'En'"
          width="170"
        />
        <el-table-column
          fixed="right"
          :label="$t('template.operation')"
          width="100"
          v-has="['Organism:Add', 'Organism:Edit', 'Organism:Disable']"
        >
          <template #header>
            <div class="table-operation-header">
              <span>{{ $t("template.operation") }}</span>
              <el-button
                v-has="'Organism:Add'"
                :disabled="OrganismTypeId == undefined"
                type="primary"
                link
                @click="onAddOrganismClick"
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
              v-has="'Organism:Disable'"
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
              @click.prevent="editOrganismRow(scope.$index)"
              v-has="'Organism:Edit'"
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
    <organism-editor
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      v-model:OrganismTypeId="OrganismTypeId"
      @accept="onOrganismAccept"
    />
  </el-container>
</template>

<script>
import { defineComponent } from "vue";
import map from "lodash.map";
import { ListMixin } from "@/mixins/ListMixin";
import OrganismEditor from "./EditModal/OrganismEditor.vue";
import { toRaw } from "vue";
import cloneDeep from "lodash.clonedeep";
import getLableDescription from "@/i18n/localeHelper";

export default defineComponent({
  name: "OrganismList",
  components: { OrganismEditor },
  mixins: [ListMixin],
  data() {
    return {
      entityName: "Organism",
      sevLocEntityName: "SeverityLocation",
      barcodeVisable: false,
      barcode: "",
      OrganismTypeId: undefined,
      queryModel: {
        OrganismTypeId: undefined,
        OrganismGenusId: undefined,
        Species: undefined,
        IsActive: undefined,
      },
      query: {
        $expand:
          "OrganismGenus($select=Id,Description;$expand=OrganismType($select=Id,Description))," +
          "OrganismSeverity($select=Id,En,Zh),SeverityLocations($select=Id,OrganismId,OrganismSeverityId,LocationId,IsActive)",
        $select:
          "Id,Species,Description,Strain,IsGramStrain,PicturePath,OrganismGenusId,OrganismSeverityId,IsActive",
      },
      // querySevLoc: {
      //   $expand:
      //     "Location($select=Id,Name),OrganismSeverity($select=Id,Description)",
      //   $select:
      //     "Id,OrganismId,OrganismSeverityId,LocationId,IsActive",
      // },
      editModel: {
        Organism: {
          OrganismGenusId: undefined,
          Species: undefined,
          Description: undefined,
          Strain: undefined,
          IsGramStrain: undefined,
          OrganismSeverityId: undefined,
          PicturePath: undefined,
        },
        SeverityLocations: [],
      },
    };
  },
  methods: {
    getLableDescription,
    buildFilterStr() {
      let filterStr = [];

      if (this.queryModel.OrganismTypeId) {
        filterStr.push(
          `OrganismGenus/OrganismTypeId eq ${this.queryModel.OrganismTypeId}`
        );
      }
      if (this.queryModel.Species && this.queryModel.Species.length > 0) {
        filterStr.push(`contains(Species,'${this.queryModel.Species}')`);
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
    // buildSevLocFilterStr(organismId) {
    //   return `OrganismId eq ${organismId}`
    // },
    searchOrganismType(id) {
      if (id) {
        this.queryModel.OrganismTypeId = id;
        this.editModel.OrganismTypeId = id;
        this.OrganismTypeId = id;
        this.loadData();
      } else {
        this.tableData.data.splice(0, this.tableData.data.length);
      }
    },
    onAddOrganismClick() {
      this.createNew = true;
      for (let prop in this.editModel.Organism) {
        this.editModel.Organism[prop] = undefined;
      }
      this.editModel.SeverityLocations = [];
      this.editModalVisible = true;
    },
    editOrganismRow(index) {
      let data = cloneDeep(toRaw(this.tableData.data[index]));
      if (this.onEditRowOverride) {
        this.onEditRowOverride(data);
      }
      this.createNew = false;
      this.editModel.Organism = data;
      this.editModel.SeverityLocations = data.SeverityLocations;
      this.editModalVisible = true;
      // todo: 查询致病性区域
      // let filter = this.buildSevLocFilterStr(data.Id);
      // if (filter && filter.length > 0) {
      //     // console.log(filter);
      //     this.querySevLoc.$filter = filter;
      // } else {
      //     delete this.querySevLoc.$filter;
      // }
      // this.$query(this.sevLocEntityName, this.querySevLoc).then((sevLocs) => {
      //   this.editModel.SeverityLocations = sevLocs.value;
      // })
    },
    onOrganismAccept() {
      let data = cloneDeep(toRaw(this.editModel));
      if (this.onEditAcceptOverride) {
        this.onEditAcceptOverride(data);
      }
      let api = this.createNew ? this.$insert : this.$update;
      delete data.Organism.SeverityLocations;
      api(this.entityName, data.Organism).then((organism) => {
        const organismId = (this.createNew ? organism : data.Organism).Id;
        // 保存致病性区域
        data.SeverityLocations.forEach((sevLoc) => {
          if (sevLoc.Id) {
            let newData = {
              Id: sevLoc.Id,
              OrganismSeverityId: sevLoc.OrganismSeverityId,
              LocationId: sevLoc.LocationId,
              OrganismId: organismId,
              IsActive: sevLoc.IsActive,
            };
            this.$update(this.sevLocEntityName, newData);
          } else {
            let newData = {
              OrganismSeverityId: sevLoc.OrganismSeverityId,
              LocationId: sevLoc.LocationId,
              OrganismId: organismId,
              IsActive: sevLoc.IsActive,
            };
            this.$insert(this.sevLocEntityName, newData);
          }
        });
        this.loadData();
      });
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.table-pagination {
  float: right;
  margin-top: 16px;
}
.el-card {
  height: calc(
    /*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding - 2 *
      $--app-main-inner-padding - 55px
  );
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style>

<style>
.el-tabs {
  height: 100%;
}
.el-descriptions__body > table {
  table-layout: fixed;
}
.barcode-father {
  position: relative;
  height: 160px;
}
.barcode-show {
  position: absolute;
  left: 50%;
  transform: translate(-50%, 0%);
}
</style>
