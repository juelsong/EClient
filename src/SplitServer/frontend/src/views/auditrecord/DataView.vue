<script lang="ts">
import * as vue from "vue";
import { OdataQuery } from "odata";
import { oDataQuery } from "@/utils/odata";

import { IEntity, showUserName } from "./AuditData";
import { getEntityMixin } from "@/utils/tableMixin";

export default vue.defineComponent({
  name: "DataView",
  props: {
    entity: {
      type: Object as vue.PropType<IEntity>,
      required: false,
    },
  },
  setup(props) {
    const { pageInfo, tableData, loadData, query, entityName } =
      getEntityMixin<any>();
    query.$apply = "groupby((EntityId))";
    const entityGroupData = vue.ref(new Array<any>());
    vue.watch(
      () => props.entity,
      async (entity) => {
        if (entity) {
          entityName.value = entity.name;
          await loadData();
        }
      }
    );
    vue.watch(
      tableData,
      async (newData) => {
        entityGroupData.value.splice(0, entityGroupData.value.length);
        if (props.entity && newData) {
          // newData 为分组数据
          const entityIdArray = newData.map<number>(
            (auditEntity) => auditEntity.EntityId
          );
          if (entityIdArray.length == 0) {
            return;
          }
          const auditOptions: OdataQuery = {
            $orderby: "AuditTime desc",
            $filter: `EntityId in [${entityIdArray.join(",")}]`,
          };

          const auditRsp = await oDataQuery(props.entity.name, auditOptions);
          if (auditRsp && auditRsp.value) {
            const auditEntities = auditRsp.value as Record<string, any>[];

            await showUserName(auditEntities);
            auditEntities.forEach((auditEntity) => {
              const id = auditEntity.EntityId;
              const exist = entityGroupData.value.find((e) => e.EntityId == id);
              // auditEntities AuditTime 倒序排列
              if (exist) {
                if (!exist.children) {
                  exist.children = new Array<any>();
                }
                exist.children.push(auditEntity);
              } else {
                // auditEntity.History = new Array<any>();
                // auditEntity.HasHistory = true;
                entityGroupData.value.push(auditEntity);
              }
            });
          }
        }
      },
      {
        deep: true,
      }
    );

    return { tableData, pageInfo, entityGroupData };
  },
});
</script>

<template>
  <div style="height: 100%; padding: 1px" v-show="entity != undefined">
    <div style="height: calc(100% - 72px)">
      <el-table
        :data="entityGroupData"
        style="width: 100%; max-height: 100%"
        row-key="Id"
        stripe
        :border="true"
        highlight-current-row
        height="100%"
      >
        <el-table-column
          v-for="p in entity?.properties ?? []"
          :key="p.name"
          :prop="p.name"
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
</template>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.table-pagination {
  float: right;
  margin-top: 16px;
  margin-right: 20px;
}
</style>
