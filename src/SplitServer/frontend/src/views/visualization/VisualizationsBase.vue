<template>
  <div class="view-root">
    <!-- <visualizations-result
      v-show="resultVisible"
      v-model:visible="resultVisible"
    /> -->
    <!-- <el-container v-show="!resultVisible"> -->
    <el-container>
      <el-header height="74px">
        <div class="header">
          <el-form ref="queryForm" :inline="true" :model="queryModel">
            <el-form-item
              :label="$t('Visualization.filter.StartDate')"
              prop="StartDate"
            >
              <el-date-picker
                v-model="queryModel.StartDate"
                type="daterange"
                :range-separator="$t('label.dateRange.separator')"
                :start-placeholder="$t('label.dateRange.start')"
                :end-placeholder="$t('label.dateRange.end')"
              ></el-date-picker>
              <el-form-item
                v-show="showOrganism"
                :label="$t('Visualization.organism.Name')"
              >
                <o-data-selector
                  :placeholder="`${$t('template.select', [
                    $t('Visualization.organism.Name'),
                  ])}`"
                  :multiple="false"
                  :clearable="true"
                  v-model="queryModel.Organism"
                  filter="IsActive eq true"
                  entity="Organism"
                  label="Description"
                  value="Id"
                />
              </el-form-item>
            </el-form-item>
            <el-button type="primary" @click="searchData">
              {{ $t("template.search") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="edit" />
              </el-icon>
            </el-button>
            <el-button type="primary" @click="reset">
              {{ $t("template.reset") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="refresh" />
              </el-icon>
            </el-button>
            <el-button type="primary" @click="queryModalVisible = true">
              {{ $t("template.advanced") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="operation" />
              </el-icon>
            </el-button>

            <el-button type="primary" @click="resultClick">
              {{ $t("template.result") }}
              <el-icon class="el-icon--right">
                <svg-icon icon-class="operation" />
              </el-icon>
            </el-button>
          </el-form>
        </div>
      </el-header>
      <el-container>
        <el-aside width="300px">
          <el-card class="box-card" body-style="height: calc(100% - 70px);">
            <template #header>
              <div class="card-header">
                <el-dropdown @command="onMapTreeOperation">
                  <el-button type="primary">
                    {{ $t("template.operation") }}
                    <el-icon class="el-icon--right">
                      <svg-icon icon-class="arrow-down" />
                    </el-icon>
                  </el-button>
                  <template #dropdown>
                    <el-dropdown-menu>
                      <el-dropdown-item command="expand">
                        {{ $t("Map.operation.Expand") }}
                      </el-dropdown-item>
                      <el-dropdown-item command="collapse">
                        {{ $t("Map.operation.Collapse") }}
                      </el-dropdown-item>
                    </el-dropdown-menu>
                  </template>
                </el-dropdown>
              </div>
            </template>
            <el-tree
              ref="mapTree"
              :data="mapCategorys"
              :props="mapTreeProps"
              draggable
              node-key="NodeKey"
              highlight-current
              @current-change="onMapTreeCurrentNodeChanged"
            >
              <template #default="{ data }">
                <span>
                  {{ data.Name }}
                </span>
              </template>
            </el-tree>
          </el-card>
        </el-aside>
        <el-main style="position: relative" ref="mainWin">
          <visual-diagram
            ref="diagram"
            :selectable="true"
            @updatePosition="onDiagramPosChange"
          />
          <el-button-group style="position: absolute; right: 10px; top: 10px">
            <el-button plain>
              <svg-icon icon-class="zoom-in" @click="onZoomInClick" />
            </el-button>
            <el-button plain>
              <svg-icon icon-class="zoom-out" @click="onZoomOutClick" />
            </el-button>
            <el-button plain>
              <svg-icon icon-class="refresh-left" @click="onResetClick" />
            </el-button>
          </el-button-group>
        </el-main>
      </el-container>
    </el-container>

    <visualization-query
      v-model:visible="queryModalVisible"
      v-model:queryModel="queryModel"
      @search="searchData"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, toRaw, PropType } from "vue";
import { requestRaw } from "@/utils/request";
import { AppMainMixin } from "@/mixins/AppMainMixin";
import {
  getMapTree,
  MapCategory,
  MapTreeNode,
  MapTreeNodeTypeMap,
  Map,
} from "@/api/map";
// import { Level } from "@/components/VisualSymbol/SymbolConfig";

import { getLocationNodeKey, getSiteNodeKey } from "@/api/location";
import { getVisioDiagramByMapId, VisioDiagram } from "@/api/visio";
import VisualDiagram from "@/components/VisualDiagram.vue";
import VisualizationQuery from "./QueryModal/VisualizationQuery.vue";
import MaxShow from "./PopUp/MaxShow.vue";
import map from "lodash.map";
import { DiagramPosition, MaxData } from "./Diagram";
import ODataSelector from "@/components/ODataSelector.vue";

import { ElForm, ElTree } from "element-plus";
import { Vue } from "vue-class-component";
declare interface Size {
  width: number;
  height: number;
}
async function getImageParams(file: Blob): Promise<Size> {
  return new Promise((resolve) => {
    let reader = new FileReader();
    reader.onload = async (e) => {
      let image = new Image();
      image.src = e.target?.result as string;
      await image.decode();
      resolve({
        width: image.width,
        height: image.height,
      });
    };
    reader.readAsDataURL(file);
  });
}

declare interface IQueryModel {
  StartDate: Array<Date>;
  ApproveDate: Array<Date>;
  BarCode?: string;
  Completed?: boolean;
  Approved?: boolean;
  Product?: number | string;
  TestType?: number | string;
  Organism?: number | string;
  Location?: number | string;
  User?: number | string;
}

const dragIconWidth = 24;
const dragIconHeight = 24;
export default defineComponent({
  name: "VisualizationBase",
  components: {
    VisualizationQuery,
    VisualDiagram,
    ODataSelector,
  },
  mixins: [AppMainMixin],
  emits: ["updateDiagramPos", "showResult"],
  props: {
    // name: {
    //   type: String,
    //   default: "",
    // },
    // typeQuery: {
    //   type: String,
    //   default: undefined,
    // },
    // showSite: {
    //   type: Boolean,
    //   default: true,
    // },
    // showLocation: {
    //   type: Boolean,
    //   default: true,
    // },
    // showMaxDatas: {
    //   type: Object,
    // },
    // getSiteLevel: {
    //   type: Function,
    //   default() {
    //     return "Default function";
    //   },
    // },
    // getLocationLevel: {
    //   type: Function,
    //   default() {
    //     return "Default function";
    //   },
    // },
    showOrganism: {
      type: Boolean,
      default: false,
    },
    getSiteLevel: {
      type: Function as PropType<(i) => Promise<Object>>,
      default: () =>
        new Promise((resolve) => {
          resolve({ data: "default value" });
        }),
    },
    getLocationLevel: {
      type: Function as PropType<(i) => Promise<Object>>,
      default: () =>
        new Promise((resolve) => {
          resolve({ data: "default value" });
        }),
    },
  },
  setup() {
    const currentDiagram = ref<VisioDiagram>();
    const diagramPosition = ref<DiagramPosition>();

    // const m: IQueryModel = {
    //   StartDate: new Array<Date>(),
    //   ApproveDate: new Array<Date>(),
    // };
    const queryModel = reactive<IQueryModel>({
      StartDate: new Array<Date>(),
      ApproveDate: new Array<Date>(),
    });
    return { queryModel, currentDiagram, diagramPosition };
  },
  data() {
    return {
      resultVisible: false,
      queryModalVisible: false,
      mapCategorys: new Array<MapCategory>(), // 地图树数据
      mapTreeProps: {
        label: "Name",
        children: "Children",
        class: (d: { IsActive: boolean }) => {
          return false === d.IsActive ? "node-deactive" : null;
        },
      },
      popupTop: 0,
      popupRight: 0,
      currentSymbol: undefined, // 当前选择的符号
      currentMapNode: undefined, // 地图树当前节点
      tmp: undefined,
      locationId: 0,
      mapId: 0,
    };
  },
  watch: {
    currentMapNode(val?: MapTreeNode) {
      const diagram = this.$refs.diagram as any;
      diagram.clear();
      diagram.setBackground();
      if (val && MapTreeNodeTypeMap === val.NodeType) {
        const map = val as Map;
        requestRaw({
          url: `/File/Image/${map.Path}`,
          method: "get",
        })
          .then((ret) => {
            let tmp = URL.createObjectURL(ret.data);
            getImageParams(ret.data).then((size) => {
              diagram.setBackground(tmp, size.width, size.height);
            });
          })
          .catch(() => {
            console.warn("get map error");
          });
        const locationIds =
          this.$store.state.user.locations === undefined
            ? new Array<number>()
            : this.$store.state.user.locations.map((l) => l.LocationId);
        getVisioDiagramByMapId(map.Id, locationIds).then((visioDiagram) => {
          this.currentDiagram = visioDiagram;
          if (this.currentDiagram) {
            this.currentDiagram.VisioLocations.forEach((visioLocation) => {
              if (visioLocation.Location) {
                const nodeKey = getLocationNodeKey(visioLocation.Location);
                this.getNodeIcon(visioLocation.Location.LocationType.Icon).then(
                  (icon) => {
                    var ss = diagram.addLocation(
                      {
                        id: nodeKey,
                        x: visioLocation.X,
                        y: visioLocation.Y,
                        width: visioLocation.Width,
                        height: visioLocation.Height,
                        icon: icon,
                        scale: 1.0,
                        title: visioLocation.Location?.Name,
                        drawRect: true,
                        canMove: false,
                        locationId: visioLocation.Location?.Id,
                      },

                      this.onSymbolEnter,
                      this.onSymbolLeave,
                      (_: any) => {
                        _;
                      }
                    );
                    if (visioLocation.Location?.Id) {
                      this.getLocationLevel(visioLocation.Location?.Id).then(
                        (i) => (ss.level = i)
                      );
                    }
                  }
                );
              }
            });

            this.currentDiagram.VisioSites.forEach((visioSite) => {
              if (visioSite.Site) {
                const nodeKey = getSiteNodeKey(visioSite.Site);
                /* eslint-disable */
                this.getNodeIcon(visioSite.Site.SiteType.Icon).then((icon) => {
                  var ss = diagram.addSite(
                    {
                      id: nodeKey,
                      x: visioSite.X,
                      y: visioSite.Y,
                      icon: icon,
                      title: visioSite.Site?.Name,
                      canMove: false,
                    },
                    this.onSymbolEnter,
                    this.onSymbolLeave,
                    (_: any) => {
                      _;
                    }
                  );
                  if (visioSite.Site?.Id) {
                    this.getSiteLevel(visioSite.Site?.Id).then(
                      (i) => (ss.level = i)
                    );
                  }
                });
                /* eslint-enable */
              }
            });
          } else {
            this.currentDiagram = {
              MapId: map.Id,
              VisioLocations: [],
              VisioSites: [],
            };
          }
        });
      } else {
        this.currentDiagram = undefined;
      }
    },
  },
  mounted() {
    this.updateTreeView();
  },
  methods: {
    reset() {
      for (let prop in this.queryModel) {
        this.queryModel[prop] = undefined;
      }
      (this.$refs.queryForm as typeof ElForm).resetFields();
    },
    //筛选器 内容
    getQueryStringArray() {
      return this.queryModel;
    },
    onMapTreeCurrentNodeChanged(data: undefined) {
      this.currentMapNode = data;
    },
    searchData() {
      this.updateTreeView();
    },
    onMapTreeOperation(cmd: string) {
      if (cmd == "expand" || cmd == "collapse") {
        let expand = cmd == "expand";
        let nodes = (this.$refs.mapTree as any).store._getAllNodes();
        nodes.forEach((n: { expanded: boolean }) => (n.expanded = expand));
      } else {
        console.log(cmd);
      }
    },
    updateTreeView() {
      getMapTree(false).then((categorys) => {
        this.mapCategorys = categorys;
        this.$nextTick(() => {
          const mt = this.$refs.mapTree as typeof ElTree;
          this.setTreeSelection(mt, null);
        });
      });
    },
    onZoomInClick() {
      (this.$refs.diagram as any).zoomin();
    },
    onZoomOutClick() {
      (this.$refs.diagram as any).zoomout();
    },
    onResetClick() {
      (this.$refs.diagram as any).reset();
    },
    onSymbolEnter(symbol: {
      config: { id; locationId };
      group: { y: any; x: any };
      parent: { y: any; x: any };
    }) {
      if (symbol) {
        let mainRef = (this.$refs.mainWin as any).$el;

        // let popup = this.$refs.popupRef.$el;
        // popup.offsetLeft = 100;

        this.diagramPosition = {
          PopupTop: symbol.group.y + mainRef.offsetTop + symbol.parent.y,
          PopupRight:
            mainRef.offsetWidth - symbol.group.x - symbol.parent.x + 15,
          MapId: symbol.config.id,
          LocationId: symbol.config.id,
        };

        this.updatePosition();
      }
    },

    updatePosition() {
      this.$emit("updateDiagramPos", this.diagramPosition);
    },
    resultClick() {
      this.$emit("showResult");
    },
    onSymbolLeave(symbol: any) {
      if (symbol) {
        (this.diagramPosition as any).MapId = undefined;
        this.updatePosition();
      }
    },
    onDiagramPosChange() {
      if (this.currentSymbol) {
        let mainRef = (this.$refs.mainWin as any).$el;
        let symbol = this.currentSymbol as any;
        this.diagramPosition = {
          PopupTop: symbol.group.y + mainRef.offsetTop + symbol.parent.y,
          PopupRight:
            mainRef.offsetWidth - symbol.group.x - symbol.parent.x + 15,
          MapId: symbol.config.id,
          LocationId: symbol.config.id,
        };
        this.updatePosition();
      }
    },
    clear() {
      const diagram = this.$refs.diagram as any;
      diagram.clear();
      diagram.setBackground();
    },
    setTreeSelection(tree: typeof ElTree, nodeKey: null) {
      tree.setCurrentKey(nodeKey);
      let node = { data: null };
      if (nodeKey) {
        node = tree.getNode(nodeKey);
      }
      tree.$emit("current-change", node ? node.data : null, node);
    },
    async loadDiagram() {
      const diagramUI = this.$refs.diagram as any;
      let queryResult = await this.$query("VisioDiagram", {
        // $filter: `Id eq ${this.current.VisioDiagramId}`,
        $select: "Id,MapId",
        $expand:
          "VisioLocations($select=Id,LocationId,X,Y,Width,Height;$filter=IsActive eq true;$expand=Location($select=Id,Name;$expand=LocationType($select=Id,Icon))),Map($select=Id,Path),VisioSites($select=Id,SiteId,X,Y;$expand=Site($select=Id,Name;$expand=SiteType($select=Id,Icon));$filter=IsActive eq true)",
        $top: 1,
      });
      if (!queryResult || !queryResult.value || queryResult.value.length != 1) {
        // TODO query failed
        this.$message.error("query failed");
      } else {
        const diagram = queryResult.value[0];
        const mapPath = diagram.Map.Path;
        const imgRsp = await requestRaw({
          url: `/File/Image/${mapPath}`,
          method: "get",
        });
        let tmp = URL.createObjectURL(imgRsp.data);
        getImageParams(imgRsp.data).then((size) => {
          diagramUI.setBackground(tmp, size.width, size.height);
        });

        for (let i = 0; i < diagram.VisioLocations.length; i++) {
          const visioLocation = diagram.VisioLocations[i];
          const location = visioLocation.Location;
          const locationType = location.LocationType;
          if (locationType?.Icon) {
            const icon = await this.getNodeIcon(locationType.Icon);
            diagramUI.addLocation(
              {
                id: visioLocation.LocationId,
                x: visioLocation.X,
                y: visioLocation.Y,
                width: visioLocation.Width,
                height: visioLocation.Height,
                icon: icon,
                scale: 1.0,
                title: location.Name,
                drawRect: false,
                canMove: false,
              },
              this.onSymbolEnter,
              this.onSymbolLeave,
              (_: any) => {
                _;
              }
            );
          }
        }
        for (let i = 0; i < diagram.VisioSites.length; i++) {
          const visioSite = diagram.VisioSites[i];
          const site = visioSite.Site;
          const siteType = site.SiteType;
          /* eslint-disable */
          if (siteType?.Icon) {
            const icon = await this.getNodeIcon(siteType.Icon);
            diagramUI.addSite(
              {
                id: visioSite.SiteId,
                x: visioSite.X,
                y: visioSite.Y,
                icon: icon,
                title: site.Name,
                canMove: false,
              },
              this.onSymbolEnter,
              this.onSymbolLeave,
              (_: any) => {
                _;
              }
            );
          }
          /* eslint-enable */
        }
      }
    },
    getNodeIcon(icon: string) {
      return new Promise<HTMLImageElement>((resolve) => {
        let iconName = `icon-${icon}`;
        let icons = document.getElementById(
          "__SVG_SPRITE_NODE__"
        ) as any as SVGSVGElement;
        for (let i = 0; i < icons.childNodes.length; i++) {
          const n = icons.childNodes[i] as SVGSymbolElement;
          if (n.id == iconName) {
            let xml = n.outerHTML;
            let img = new Image();
            let svgStr = `<svg width="${dragIconWidth}" height="${dragIconHeight}" viewBox="0 0 ${dragIconWidth} ${dragIconHeight}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">${xml}<use xlink:href="#${iconName}" x="0" y="0" width="${dragIconWidth}" height="${dragIconHeight}"/></svg>`;
            img.src = `data:image/svg+xml;base64,${window.btoa(svgStr)}`;
            this.$nextTick(() => {
              resolve(img);
            });
            break;
          }
        }
      });
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.header {
  background: white;
  border-radius: 4px 4px 0px 0px;
  height: 100%;
  padding: 0px 12px;
  align-content: center;
  display: flex;
  align-items: center;

  :deep(.el-form-item) {
    margin: 0px 16px 0px 0px;
  }
}

.el-card {
  height: 100%;
}

.el-aside {
  margin: 0;
  padding: 0px 6px 0px 12px;
  background: unset;
}

.el-main {
  margin: 0;
  padding: 0px 12px 0px 6px;
}

.hover_con {
  position: fixed;
  width: 220px;
  max-height: 380px;
  margin: 0px;
  padding: 0px;
}

.advanced_data {
  width: 550px;
  height: 450px;
  padding: 10px;

  border: 1px solid rgb(8, 8, 8);
  background: rgb(255, 255, 255);
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  margin: auto;
  z-index: 999;
}

.view-root {
  height: 100%;

  :deep(.el-container) {
    height: 100%;
  }

  :deep(.el-container:not(.is-vertical)) {
    margin: 12px 0 0 0;
  }

  :deep(.el-header) {
    --el-header-padding: 0;

    .el-button {
      margin-left: 8px;
    }
  }

  :deep(.el-aside) {
    padding: 0;
    margin: 0 12px 0 0;
  }

  :deep(.el-main) {
    padding: 0;
    background: white;
    border-radius: 4px;
  }
}
</style>
