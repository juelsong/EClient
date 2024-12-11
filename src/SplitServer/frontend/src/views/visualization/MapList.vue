<template>
  <div>
    <el-container>
      <el-aside width="450px">
        <el-card class="box-card" :body-style="{ height: 'calc(100% - 73px)' }">
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
                    <el-dropdown-item
                      command="add"
                      v-has="'Map:Add'"
                      :disabled="1 != currentMapNode?.NodeType"
                    >
                      {{ $t("Map.operation.Add") }}
                    </el-dropdown-item>
                    <el-dropdown-item command="expand">
                      {{ $t("Map.operation.Expand") }}
                    </el-dropdown-item>
                    <el-dropdown-item command="collapse">
                      {{ $t("Map.operation.Collapse") }}
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>
              <el-button
                plain
                v-has="'Map:Edit'"
                :disabled="2 != currentMapNode?.NodeType"
                @click="onEditMapClick"
              >
                {{ $t("template.edit") }}
              </el-button>

              <el-button
                plain
                v-has="'Map:Disable'"
                :disabled="2 != currentMapNode?.NodeType"
                @click="setMapNodeIsActive"
              >
                {{
                  currentMapNode == undefined
                    ? $t("template.disable")
                    : currentMapNode.IsActive
                    ? $t("template.disable")
                    : $t("template.enable")
                }}
              </el-button>
              <el-switch
                v-model="showDeactive"
                :active-value="true"
                :inactive-value="false"
                :active-text="$t('template.showDisabled')"
                @change="handleMapActiveChange"
              />
            </div>
          </template>
          <div class="wrapper">
            <el-scrollbar max-height="100%">
              <el-tree
                ref="mapTree"
                :data="mapCategorys"
                :props="mapTreeProps"
                node-key="NodeKey"
                highlight-current
                @current-change="onMapTreeCurrentNodeChanged"
                :filter-node-method="filterMapNode"
              >
                <template #default="{ data }">
                  <span>
                    {{ data.Name }}
                  </span>
                </template>
              </el-tree>
            </el-scrollbar>
          </div>
        </el-card>
      </el-aside>
      <el-main>
        <el-container class="el-card">
          <el-aside class="inner">
            <el-card
              class="box-card"
              shadow="never"
              :body-style="{ height: 'calc(100% - 62px)' }"
            >
              <template #header>
                <div>
                  <span>{{ $t("Location.entity") }}</span>
                  <el-dropdown @command="onLocationTreeOperation">
                    <el-button plain style="margin-left: 10px">
                      {{ $t("template.operation") }}
                      <el-icon class="el-icon--right">
                        <svg-icon icon-class="arrow-down" />
                      </el-icon>
                    </el-button>
                    <template #dropdown>
                      <el-dropdown-menu>
                        <el-dropdown-item command="expand">
                          {{ $t("Location.tree.Expand") }}
                        </el-dropdown-item>
                        <el-dropdown-item command="collapse">
                          {{ $t("Location.tree.Collapse") }}
                        </el-dropdown-item>
                      </el-dropdown-menu>
                    </template>
                  </el-dropdown>
                </div>
              </template>
              <div class="wrapper">
                <el-scrollbar max-height="100%">
                  <el-tree
                    ref="locationTree"
                    :data="locations"
                    :props="locationTreeProps"
                    draggable
                    node-key="NodeKey"
                    highlight-current
                    :allow-drag="allowLocationTreeDrag"
                    :allow-drop="allowLocationTreeDrop"
                    @node-drag-start="handleLocationTreeDragStart"
                  >
                    <template #default="{ data }">
                      <span>
                        {{ data.Name }}
                      </span>
                    </template>
                  </el-tree>
                </el-scrollbar>
              </div>
            </el-card>
          </el-aside>
          <el-main class="inner">
            <el-card
              shadow="never"
              :body-style="{
                height: 'calc(100% - 62px)',
                position: 'relative',
              }"
            >
              <template #header>
                <div>
                  <span>{{ $t("Map.entity") }}</span>
                  <el-button
                    plain
                    Disable
                    v-has="'Map:Edit'"
                    style="margin-left: 10px"
                    :disabled="currentDiagram == undefined"
                    @click="onSaveClick"
                  >
                    {{ $t("label.save") }}
                  </el-button>
                </div>
              </template>
              <visual-diagram
                ref="diagram"
                :selectable="true"
                v-model="selectedSymbolId"
                @dragover="onMapDragOver"
                @drop="onMapDrop"
              />
              <el-button-group
                style="position: absolute; right: 10px; top: 10px"
              >
                <el-button plain>
                  <svg-icon icon-class="zoom-in" @click="onZoomInClick" />
                </el-button>
                <el-button plain>
                  <svg-icon icon-class="zoom-out" @click="onZoomOutClick" />
                </el-button>
                <el-button plain>
                  <svg-icon icon-class="refresh-left" @click="onResetClick" />
                </el-button>
                <el-button plain :disabled="selectedSymbolId == undefined">
                  <svg-icon
                    icon-class="delete"
                    class-name="icon-danger"
                    @click="onDeleteSymbolClick"
                  />
                </el-button>
              </el-button-group>
            </el-card>
          </el-main>
        </el-container>
      </el-main>
    </el-container>

    <map-editor
      v-model:categorys="mapCategorys"
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      v-model:createNew="createNew"
      @accept="onEditMapAccept"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, toRaw, ref, reactive } from "vue";
import MapEditor from "./EditModal/MapEditor.vue";
import cloneDeep from "lodash.clonedeep";
import VisualDiagram from "@/components/VisualDiagram.vue";
import { getVisioDiagramByMapId, VisioDiagram } from "@/api/visio";
import type Node from "element-plus/es/components/tree/src/model/node";
import { Symbol } from "@/components/VisualSymbol/SymbolConfig";
import * as VisioLocationUI from "@/components/VisualSymbol/Location";
import * as VisioSiteUI from "@/components/VisualSymbol/Site";
import {
  getMapTree,
  MapCategory,
  MapTreeNode,
  Map,
  MapTreeNodeTypeMap,
} from "@/api/map";
import {
  getLocationTree,
  Location,
  getLocationNodeKey,
  getSiteNodeKey,
  LocationTreeNode,
  LocationTreeNodeTypeLocation,
  LocationTreeNodeTypeSite,
  Site,
} from "@/api/location";
import { ElTree } from "element-plus";
import { request, requestRaw } from "@/utils/request";
import { IReply } from "@/defs/Reply";
import {
  TreeNodeData,
  TreeOptionProps,
} from "element-plus/es/components/tree/src/tree.type";
// let userOptFlag = false;
const dragRectWidth = 200;
const dragRectHeight = 100;
const dragIconWidth = 24;
const dragIconHeight = 24;
const dragRectStrokeWidth = 2;
const dragRectStrokeColor = "#1890FF";
const dragRectFillColor = "#1890FF";
const dragRectFillOpacity = 0.2;
const dragIconX = 10;
const dragIconY = 10;
const dragImage = new Image();
declare interface Size {
  width: number;
  height: number;
}
async function getImageParams(file): Promise<Size> {
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

export default defineComponent({
  name: "MapList",
  components: { MapEditor, VisualDiagram },
  setup() {
    const currentDiagram = ref<VisioDiagram>();
    const draggingLocationTreeNode = ref<LocationTreeNode>();
    const selectedSymbolId = ref<string>();
    const usedLocation = reactive(new Array<string>());
    const currentMapNode = ref<MapTreeNode>(); // 地图树当前节点
    const locationTreeProps: TreeOptionProps = {
      label: "Name",
      children: "Children",
      class: (d: TreeNodeData, node: Node): string => {
        return usedLocation.includes(
          (node.data as LocationTreeNode).NodeKey as string
        )
          ? "node-deactive"
          : "";
      },
    };
    return {
      currentMapNode,
      currentDiagram,
      draggingLocationTreeNode,
      selectedSymbolId,
      usedLocation,
      locationTreeProps,
    };
  },
  data() {
    return {
      entityName: "Map",
      mapCategorys: new Array<MapCategory>(), // 地图树数据
      createNew: false, // 是否新建地图
      showDeactive: false, // 是否显示无效的地图
      editModalVisible: false, // 编辑地图模态窗是否显示
      editModel: {
        Category: undefined,
        Name: undefined,
        Path: undefined,
      },

      mapTreeProps: {
        label: "Name",
        children: "Children",
        class: (d) => {
          return false === d.IsActive ? "node-deactive" : null;
        },
      },
      locations: new Array<Location>(), // 区域树数据
    };
  },
  watch: {
    currentMapNode(val?: MapTreeNode) {
      const diagram = this.$refs.diagram as any;
      diagram.clear();
      diagram.setBackground();
      this.usedLocation.splice(0);
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
            let nodes = (this.$refs.locationTree as any).store._getAllNodes();
            nodes.forEach((n) => (n.expanded = false));
            this.currentDiagram.VisioLocations.forEach((visioLocation) => {
              if (visioLocation.Location) {
                const nodeKey = getLocationNodeKey(visioLocation.Location);
                this.getNodeIcon(visioLocation.Location.LocationType.Icon).then(
                  (icon) => {
                    diagram.addLocation({
                      id: nodeKey,
                      x: visioLocation.X,
                      y: visioLocation.Y,
                      width: visioLocation.Width,
                      height: visioLocation.Height,
                      icon: icon,
                      scale: 1.0,
                      title: visioLocation.Location?.Name,
                      drawRect: true,
                      canMove: true,
                    });
                  }
                );
                this.usedLocation.push(nodeKey);
              }
            });

            this.currentDiagram.VisioSites.forEach((visioSite) => {
              if (visioSite.Site) {
                const nodeKey = getSiteNodeKey(visioSite.Site);
                this.getNodeIcon(visioSite.Site.SiteType.Icon).then((icon) => {
                  diagram.addSite({
                    id: nodeKey,
                    x: visioSite.X,
                    y: visioSite.Y,
                    icon: icon,
                    title: visioSite.Site?.Name,
                    canMove: true,
                  });
                });
                this.usedLocation.push(nodeKey);
              }
            });
            nodes.forEach((n) => {
              if (
                this.usedLocation.includes(
                  (n.data as LocationTreeNode).NodeKey as string
                )
              ) {
                let that = n;
                while (that.parent && !that.parent.expanded) {
                  that.parent.expanded = true;
                  that = that.parent;
                }
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
    this.loadData(true);
  },
  methods: {
    loadData(reloadLocation?: boolean) {
      const locationIds =
        this.$store.state.user.locations === undefined
          ? new Array<number>()
          : this.$store.state.user.locations.map((l) => l.LocationId);
      getMapTree(true).then((categorys) => {
        this.mapCategorys = categorys;
        this.$nextTick(() => {
          const mt = this.$refs.mapTree as typeof ElTree;
          mt.filter(this.showDeactive);
          this.setTreeSelection(mt, null);
        });
      });
      if (true === reloadLocation) {
        getLocationTree(false, true, locationIds).then((locations) => {
          this.locations = locations;
        });
      }
    },
    setTreeSelection(tree: typeof ElTree, nodeKey) {
      tree.setCurrentKey(nodeKey);
      let node = { data: null };
      if (nodeKey) {
        node = tree.getNode(nodeKey);
      }
      tree.$emit("current-change", node ? node.data : null, node);
    },
    setMapNodeIsActive() {
      if (
        this.currentMapNode &&
        MapTreeNodeTypeMap == (this.currentMapNode as MapTreeNode).NodeType
      ) {
        const map = this.currentMapNode as Map;
        let data = {
          Id: map.Id,
          IsActive: !map.IsActive,
        };
        this.$update(this.entityName, data).then((rsp) => {
          // let { success, message } = rsp ;
          if ((rsp as any).status == 204) {
            map.IsActive = !map.IsActive;
            this.$message.success(this.$t("prompt.success"));
            this.updateMapTreeFilter();
          } else {
            this.$message.error(this.$t("prompt.failed"));
          }
        });
      }
    },
    onMapTreeCurrentNodeChanged(data) {
      this.currentMapNode = data;
    },
    handleMapActiveChange() {
      this.updateMapTreeFilter();
    },
    filterMapNode(showInactived, data) {
      return showInactived || data.IsActive == true;
    },
    updateMapTreeFilter() {
      if (
        this.currentMapNode &&
        !this.showDeactive &&
        true !== (this.currentMapNode as MapTreeNode).IsActive
      ) {
        this.currentMapNode = undefined;
        this.setTreeSelection(this.$refs.mapTree as typeof ElTree, null);
      }
      this.$nextTick(() => {
        (this.$refs.mapTree as any).filter(this.showDeactive);
      });
    },
    onMapTreeOperation(cmd) {
      if (cmd == "expand" || cmd == "collapse") {
        let expand = cmd == "expand";
        let nodes = (this.$refs.mapTree as any).store._getAllNodes();
        nodes.forEach((n) => (n.expanded = expand));
      } else if (cmd == "add") {
        this.createNew = true;
        for (let prop in this.editModel) {
          this.editModel[prop] = undefined;
        }
        if (this.currentMapNode) {
          this.editModel["MapCategoryId"] = (
            this.currentMapNode as MapCategory
          ).Id;
        }
        this.editModalVisible = true;
      } else {
        console.log(cmd);
      }
    },
    onEditMapClick() {
      let data = cloneDeep(toRaw(this.currentMapNode));
      this.createNew = false;
      this.editModel = data;
      this.editModalVisible = true;
    },
    onEditMapAccept() {
      let data = cloneDeep(toRaw(this.editModel));
      let api = this.createNew ? this.$insert : this.$update;
      delete data["NodeKey"];
      delete data["NodeType"];
      api(this.entityName, data).then((rsp) => {
        // let { success, message, data } = rsp;
        const mapTree = this.$refs.mapTree as typeof ElTree;
        {
          const categoryNode = mapTree.getNode(
            `CATEGORY_${data["MapCategoryId"]}`
          );
          const mapNode = mapTree.getNode(`MAP_${data["Id"]}`);
          let appendMapData: Map = {
            Id: data["Id"],
            Name: data["Name"],
            Description: data["Description"],
            Path: data["Path"],
            MapCategoryId: data["MapCategoryId"],
            IsActive: data["IsActive"],
            NodeKey: `MAP_${data["Id"]}`,
            NodeType: MapTreeNodeTypeMap,
          };
          if (mapNode) {
            mapTree.remove(mapNode);
          }
          mapTree.append(appendMapData, categoryNode);
          this.$nextTick(() => {
            this.setTreeSelection(mapTree, appendMapData.NodeKey);
          });
        }
        this.$message.success(
          this.createNew
            ? this.$t("template.addSuccess")
            : this.$t("template.updateSuccess")
        );
      });
    },
    onLocationTreeOperation(cmd) {
      if (cmd == "expand" || cmd == "collapse") {
        let expand = cmd == "expand";
        let nodes = (this.$refs.locationTree as any).store._getAllNodes();
        nodes.forEach((n) => (n.expanded = expand));
      } else {
        console.log(cmd);
      }
    },
    allowLocationTreeDrag(draggingNode: Node) {
      return (
        this.currentDiagram != undefined &&
        !this.usedLocation.includes(draggingNode.key as string)
      );
    },
    allowLocationTreeDrop() {
      return false;
    },
    handleLocationTreeDragStart(draggingNode: Node, event: DragEvent) {
      let iconName = `icon-`;
      const nodeData = draggingNode.data as LocationTreeNode;
      this.draggingLocationTreeNode = nodeData;
      if (LocationTreeNodeTypeLocation == nodeData.NodeType) {
        const location = nodeData as Location;
        iconName = `icon-${location.LocationType.Icon}`;
      } else if (LocationTreeNodeTypeSite == nodeData.NodeType) {
        const site = nodeData as Site;
        iconName = `icon-${site.SiteType.Icon}`;
      } else {
        console.error(`handleLocationTreeDragStart error:${nodeData.NodeType}`);
        return;
      }
      let icons = document.getElementById(
        "__SVG_SPRITE_NODE__"
      ) as any as SVGSVGElement;
      for (let i = 0; i < icons.childNodes.length; i++) {
        const n = icons.childNodes[i] as SVGSymbolElement;
        if (n.id == iconName) {
          let xml = n.outerHTML;
          let svgStr =
            nodeData.NodeType == 1
              ? `<svg width="${dragRectWidth}" height="${dragRectHeight}" viewBox="0 0 ${dragRectWidth} ${dragRectHeight}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">${xml}<rect x="0" y="0" width="${dragRectWidth}" height="${dragRectHeight}" stroke="${dragRectStrokeColor}" stroke-width="${dragRectStrokeWidth}" fill="${dragRectFillColor}" fill-opacity="${dragRectFillOpacity}"/><use xlink:href="#${iconName}" x="${dragIconX}" y="${dragIconY}" width="${dragIconWidth}" height="${dragIconHeight}"/></svg>`
              : `<svg width="${dragIconWidth}" height="${dragIconHeight}" viewBox="0 0 ${dragIconWidth} ${dragIconHeight}" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">${xml}<use xlink:href="#${iconName}" x="0" y="0" width="${dragIconWidth}" height="${dragIconHeight}"/></svg>`;
          dragImage.src = `data:image/svg+xml;base64,${window.btoa(svgStr)}`;
          this.$nextTick(() => {
            event.dataTransfer?.setDragImage(dragImage, 0, 0);
          });
          break;
        }
      }
    },
    onMapDrop(ev: DragEvent) {
      ev.preventDefault();
      if (this.draggingLocationTreeNode) {
        let iconName = "";
        if (
          LocationTreeNodeTypeLocation == this.draggingLocationTreeNode.NodeType
        ) {
          const location = this.draggingLocationTreeNode as Location;
          iconName = location.LocationType.Icon;
        } else if (
          LocationTreeNodeTypeSite == this.draggingLocationTreeNode.NodeType
        ) {
          const site = this.draggingLocationTreeNode as Site;
          iconName = site.SiteType.Icon;
        } else {
          console.error(
            `handleLocationTreeDragStart error:${this.draggingLocationTreeNode.NodeType}`
          );
          return;
        }

        this.getNodeIcon(iconName).then((img) => {
          const diagram = this.$refs.diagram as any;
          let scale: number = diagram.scale;
          if (
            this.draggingLocationTreeNode?.NodeType ==
            LocationTreeNodeTypeLocation
          ) {
            const location = this.draggingLocationTreeNode as Location;
            diagram.addLocation({
              id: this.draggingLocationTreeNode?.NodeKey,
              x: ev.offsetX / scale,
              y: ev.offsetY / scale,
              width: dragRectWidth / scale,
              height: dragRectHeight / scale,
              icon: img,
              title: location.Name,
              drawRect: true,
              canMove: true,
            });
          } else {
            const site = this.draggingLocationTreeNode as Site;
            diagram.addSite({
              id: this.draggingLocationTreeNode?.NodeKey,
              x: ev.offsetX / scale,
              y: ev.offsetY / scale,
              icon: img,
              title: site.Name,
              canMove: true,
            });
          }
          this.selectedSymbolId = this.draggingLocationTreeNode?.NodeKey;
          this.usedLocation.push(this.selectedSymbolId as string);
          this.draggingLocationTreeNode = undefined;
        });
      }
    },
    onMapDragOver(ev) {
      ev.preventDefault();
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
    async onSaveClick() {
      if (this.currentDiagram == undefined) {
        return;
      }
      const createFlag = this.currentDiagram.Id == undefined;
      const httpMethod = createFlag ? "post" : "patch";
      const diagram = this.$refs.diagram as any;
      const locationTree = this.$refs.locationTree as typeof ElTree;
      this.currentDiagram.VisioLocations.splice(0);
      this.currentDiagram.VisioSites.splice(0);
      const cfg = diagram.getAllSymbolConfig() as Array<Symbol>;
      cfg.forEach((symbol) => {
        const treeNode = locationTree.getNode(symbol.id)
          .data as LocationTreeNode;
        if (treeNode.NodeType == LocationTreeNodeTypeLocation) {
          const visioLocation = symbol as VisioLocationUI.default;
          const location = treeNode as Location;
          this.currentDiagram?.VisioLocations.push({
            LocationId: location.Id,
            Width: visioLocation.width,
            Height: visioLocation.height,
            X: visioLocation.x,
            Y: visioLocation.y,
          });
        } else if (treeNode.NodeType == LocationTreeNodeTypeSite) {
          const visioSite = symbol as VisioSiteUI.default;
          const site = treeNode as Site;
          this.currentDiagram?.VisioSites.push({
            SiteId: site.Id,
            X: visioSite.x,
            Y: visioSite.y,
          });
        } else {
          console.error(`onSaveClick wrong type ${treeNode.NodeType}`);
        }
      });

      const visioRsp = (await request({
        url: "/Visualization/VisioDiagram",
        method: httpMethod,
        data: this.currentDiagram,
        headers: { "Content-Type": "application/json;" },
      })) as any as IReply<VisioDiagram>;
      if (visioRsp.success) {
        const visioEntity = visioRsp.data as VisioDiagram;
        this.currentDiagram.Id = visioEntity.Id;
        this.currentDiagram.MapId = visioEntity.MapId;
        this.currentDiagram.VisioLocations.splice(0);
        this.currentDiagram.VisioSites.splice(0);

        this.currentDiagram.VisioLocations.push(...visioEntity.VisioLocations);
        this.currentDiagram.VisioSites.push(...visioEntity.VisioSites);
        this.$message.success(this.$t("prompt.success"));
      } else {
        this.$message.error(this.$t("prompt.failedReason", [visioRsp.message]));
      }
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
    onDeleteSymbolClick() {
      if (this.selectedSymbolId) {
        let idx = this.usedLocation.indexOf(this.selectedSymbolId);
        if (idx > -1) {
          this.usedLocation.splice(idx, 1);
        }
        (this.$refs.diagram as any).removeSymbol(this.selectedSymbolId);
      }
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.inner > .el-card {
  height: 100%;
  background: transparent;
  border: unset;
  :deep(.el-card__header) {
    background: #a3d3ff;
    padding: 12px 18px;
    font-size: 16px;
    line-height: 24px;
    border: #108de9 1px solid;
    border-radius: 4px;
    font-weight: 700;
  }

  :deep(.el-card__body) {
    border: #108de9 1px solid;
    border-radius: 4px;
  }
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
</style>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

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
.wrapper {
  height: 100%;
}
</style>

<style>
.el-tabs {
  height: 100%;
}
.el-descriptions__body > table {
  table-layout: fixed;
}
</style>
