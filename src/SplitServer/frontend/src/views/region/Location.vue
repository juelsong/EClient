<template>
  <div>
    <el-row :gutter="24">
      <el-col :span="8">
        <el-card
          class="box-card"
          @click="onClickCard"
          :body-style="{ height: 'calc(100% - 56px)' }"
        >
          <template #header>
            <div class="card-header">
              <el-dropdown @command="onTreeOperation">
                <el-button type="primary">
                  {{ $t("Location.tree.TreeOperation") }}
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

              <el-dropdown
                @command="onNodeOperation"
                v-has="['Location:Add', 'Location:Edit', 'Location:Disable']"
              >
                <el-button>
                  {{ $t("Location.tree.Operation") }}
                  <el-icon class="el-icon--right">
                    <svg-icon icon-class="arrow-down" />
                  </el-icon>
                </el-button>
                <template #dropdown>
                  <el-dropdown-menu>
                    <div v-has="'Location:Add'" v-if="hasAllLocationPermission">
                      <el-dropdown-item command="topmost">
                        {{
                          $t("Location.tree.New", [$t("Location.tree.Topmost")])
                        }}
                      </el-dropdown-item>
                    </div>
                    <div v-has="'Location:Add'">
                      <el-dropdown-item
                        command="subLocation"
                        :disabled="currentType !== 1 || !current.IsActive"
                      >
                        {{
                          $t("Location.tree.New", [
                            $t("Location.tree.SubLocation"),
                          ])
                        }}
                      </el-dropdown-item>
                    </div>
                    <div v-has="'Location:Add'">
                      <el-dropdown-item
                        command="site"
                        :disabled="currentType !== 1 || !current.IsActive"
                      >
                        {{
                          $t("Location.tree.New", [$t("Location.tree.Site")])
                        }}
                      </el-dropdown-item>
                    </div>
                    <div v-has="'Location:Edit'">
                      <el-dropdown-item
                        command="edit"
                        :disabled="currentType === 0 || !current.IsActive"
                      >
                        {{ $t("template.edit") }}
                      </el-dropdown-item>
                    </div>
                    <div v-has="'Location:Disable'">
                      <el-dropdown-item
                        command="disable"
                        :disabled="currentType === 0"
                      >
                        {{
                          current == undefined || !current.IsActive
                            ? $t("template.enable")
                            : $t("template.disable")
                        }}
                      </el-dropdown-item>
                    </div>
                  </el-dropdown-menu>
                </template>
              </el-dropdown>

              <el-switch
                :style="{ float: 'right' }"
                v-model="showInactived"
                :active-value="true"
                :inactive-value="false"
                :active-text="$t('template.showDisabled')"
                @change="handleActiveChange"
              />
            </div>
          </template>
          <div class="wrapper">
            <el-scrollbar max-height="100%">
              <el-tree
                :default-expanded-keys="expanded"
                ref="locationTreeView"
                :data="locationTree"
                :props="treeProps"
                draggable
                node-key="NodeKey"
                highlight-current
                :check-strictly="true"
                :expand-on-click-node="false"
                :allow-drag="nodeAllowDrag"
                :allow-drop="nodeAllowDrop"
                @current-change="onCurrentNodeChanged"
                @node-click="onNodeClick"
                @node-drop="onNodeDrop"
                :filter-node-method="filterNode"
              >
                <template #default="{ node, data }">
                  <span>
                    <el-icon
                      v-if="data.Meta.Icon"
                      :size="node.checked ? 24 : 16"
                    >
                      <svg-icon :icon-class="data.Meta.Icon" />
                    </el-icon>
                    {{ data.Name }}
                  </span>
                </template>
              </el-tree>
            </el-scrollbar>
          </div>
        </el-card>
      </el-col>
      <el-col :span="16" style="height: 100%">
        <el-tabs type="border-card" class="single-tabs">
          <el-tab-pane :label="$t('label.detail.title')">
            <el-scrollbar>
              <el-descriptions
                v-if="currentType === 1"
                :column="2"
                label-class-name="location-label"
                class-name="location-content"
                :border="true"
              >
                <el-descriptions-item
                  :label="$t('Location.column.Name')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Name }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.creator')"
                  label-align="right"
                  align="left"
                >
                  {{ currentCreator ? currentCreator.RealName : "" }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Location.column.Description')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Description }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.creatDate')"
                  label-align="right"
                  align="left"
                >
                  {{ $moment(current.CreatedTime).format("lll") }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Location.column.LocationType')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Meta.TypeName }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.updated')"
                  label-align="right"
                  align="left"
                >
                  {{ currentUpdateUser ? currentUpdateUser.RealName : "" }}
                </el-descriptions-item>
                <!-- <el-descriptions-item
                  :label="$t('Location.column.Classification')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.Classification ? current.Classification.Name : ""
                  }}
                </el-descriptions-item> -->
                <el-descriptions-item
                  :label="$t('label.detail.updateDate')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.UpdatedTime
                      ? $moment(current.UpdatedTime).format("lll")
                      : ""
                  }}
                </el-descriptions-item>
                <!-- <el-descriptions-item
                  :label="$t('Location.column.SecondClassification')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.SecondClassification
                      ? current.SecondClassification.Name
                      : ""
                  }}
                </el-descriptions-item> -->
                <el-descriptions-item
                  :label="$t('Location.column.Barcode')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Barcode }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Location.column.Code')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Code }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Location.column.IsActive')"
                  label-align="right"
                  align="left"
                >
                  {{ $t(current.IsActive ? "label.yes" : "label.no") }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Location.column.Map')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Name }}
                </el-descriptions-item>
              </el-descriptions>
              <el-descriptions
                v-if="currentType === 2"
                :column="2"
                label-class-name="location-label"
                class-name="location-content"
                :border="true"
              >
                <el-descriptions-item
                  :label="$t('Site.column.Name')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Name }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.creator')"
                  label-align="right"
                  align="left"
                >
                  {{ currentCreator ? currentCreator.RealName : "" }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.Description')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Description }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.creatDate')"
                  label-align="right"
                  align="left"
                >
                  {{ $moment(current.CreatedTime).format("lll") }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.SiteType')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Meta.TypeName }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.updated')"
                  label-align="right"
                  align="left"
                >
                  {{ currentUpdateUser ? currentUpdateUser.RealName : "" }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.Classification')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.Classification ? current.Classification.Name : ""
                  }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('label.detail.updateDate')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.UpdatedTime
                      ? $moment(current.UpdatedTime).format("lll")
                      : ""
                  }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.SecondClassification')"
                  label-align="right"
                  align="left"
                >
                  {{
                    current.SecondClassification
                      ? current.SecondClassification.Name
                      : ""
                  }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.Location')"
                  label-align="right"
                  align="left"
                >
                  {{ getLocationNameFromSite() }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.Barcode')"
                  label-align="right"
                  align="left"
                >
                  {{ current.Barcode }}
                </el-descriptions-item>
                <el-descriptions-item
                  :label="$t('Site.column.IsActive')"
                  label-align="right"
                  align="left"
                >
                  {{ $t(current.IsActive ? "label.yes" : "label.no") }}
                </el-descriptions-item>
              </el-descriptions>
            </el-scrollbar>
          </el-tab-pane>
          <el-tab-pane :label="$t('label.history')"></el-tab-pane>
        </el-tabs>
      </el-col>
    </el-row>
    <location-editor
      ref="locationEditor"
      v-model:visible="locationEditModalVisible"
      v-model:model="locationEditModel"
      v-model:createNew="createNew"
      :maxLevelWeight="maxLevelWeight"
      :minLevelWeight="minLevelWeight"
      @accept="onEditAccept(1)"
    />
    <site-editor
      v-model:visible="siteEditModalVisible"
      v-model:model="siteEditModel"
      v-model:createNew="createNew"
      @accept="onEditAccept(2)"
    />
  </div>
</template>

<script>
import {
  oDataPatch,
  oDataPost,
  oDataQuery,
  oDataBatchUpdate,
  oDataBatchDelete,
} from "@/utils/odata";
import { defineComponent, provide, reactive, ref } from "vue";
import LocationEditor from "./EditModal/LocationEditor.vue";
import SiteEditor from "./EditModal/SiteEditor.vue";
import SvgIcon from "@/components/SvgIcon";
import { request } from "@/utils/request";
import { toRaw } from "vue";
import cloneDeep from "lodash.clonedeep";
import {
  getLocationTree,
  locationQueryExpand,
  getLocationNodeKey,
  getSiteNodeKey,
  siteQueryExpand,
  LocationTreeNodeTypeLocation,
  LocationTreeNodeTypeSite,
} from "@/api/location";

export default defineComponent({
  name: "Location",
  components: { LocationEditor, SvgIcon, SiteEditor },
  setup(props, ctx) {
    const createTopmost = ref(false);
    const createNew = ref(false);
    const locationTree = ref([]);
    const current = ref(null);
    const locationTreeView = ref(null);
    const existSameName = (name, id, type) => {
      let sameLevelNodes = [];
      if (createTopmost.value) {
        sameLevelNodes = locationTree.value;
      } else if (createNew.value) {
        sameLevelNodes = current.value.Children;
      } else {
        let node = locationTreeView.value.getNode(
          current.value.Parent?.NodeKey
        );
        if (node) {
          sameLevelNodes = node.data.Children;
        }
      }
      return sameLevelNodes.some(
        (n) => n.Id !== id && n.Name === name && n.NodeType === type
      );
    };
    provide("existSameName", existSameName);
    return {
      createTopmost,
      createNew,
      locationTree,
      current,
      locationTreeView,
    };
  },
  data() {
    return {
      showInactived: false,
      locationEditModalVisible: false,
      locationEditModel: {
        Name: undefined,
        Description: undefined,
        LocationTypeId: undefined,
        // ClassificationId: undefined,
        // SecondClassificationId: undefined,
        Barcode: undefined,
        Code: undefined,
      },
      siteEditModalVisible: false,
      siteEditModel: {
        Name: undefined,
        Description: undefined,
        SiteType: undefined,
        // Classification: undefined,
        // SecondClassification: undefined,
        Barcode: undefined,
      },
      treeProps: {
        label: "Name",
        children: "Children",
        class: (d) => {
          if (d.IsActive != true) {
            return "node-deactive";
          }
          return null;
        },
      },
      expanded: [],
      currentCreator: undefined,
      currentUpdateUser: undefined,
    };
  },
  computed: {
    // 0 没有 1 区域 2 采样点
    currentType() {
      if (this.current) {
        return this.current.NodeType;
      }
      return 0;
    },
    hasAllLocationPermission() {
      return (
        !this.$store.state.user.locations ||
        this.$store.state.user.locations.length === 0
      );
    },
    maxLevelWeight() {
      if (
        this.createTopmost ||
        this.current == undefined ||
        this.current.NodeType === LocationTreeNodeTypeSite
      ) {
        return Number.MIN_VALUE;
      }
      if (this.createNew) {
        //新建子节点
        return this.current.Meta.Weight;
      } else if (this.current && this.current.Parent) {
        //编辑子节点
        let parentNode = this.$refs.locationTreeView.getNode(
          this.current.Parent.NodeKey
        );
        return parentNode.data.Meta.Weight;
      } else {
        //编辑根节点
        return Number.MIN_VALUE;
      }
    },
    minLevelWeight() {
      if (
        this.createTopmost || //新建根节点
        this.createNew || //新建节点
        this.current == undefined ||
        this.current.NodeType === LocationTreeNodeTypeSite ||
        (this.current && this.current.ParentId == undefined) //编辑根节点
      ) {
        return Number.MAX_VALUE;
      }
      let val = Number.MAX_VALUE;
      if (this.current && this.current.Children) {
        this.current.Children.forEach((c) => {
          if (c.Meta.Weight && c.Meta.Weight < val) {
            val = c.Meta.Weight;
          }
        });
      }
      return val;
    },
  },
  mounted() {
    this.updateTreeView();
  },
  methods: {
    onCurrentNodeChanged(data) {
      this.current = data;
      if (this.current) {
        this.$traceUser(this.current.CreateBy, this.current.UpdateBy).then(
          (users) => {
            this.currentCreator = users[0];
            this.currentUpdateUser = users[1];
          }
        );
      } else {
        this.currentCreator = this.currentUpdateUser = undefined;
      }
    },
    onClickCard(e) {
      e = e || window.event;
      if (e.target.contains(this.$refs.locationTreeView.$el)) {
        let nodes = this.$refs.locationTreeView.store._getAllNodes();
        nodes.forEach((n) => {
          n.setChecked(false, false);
        });
        this.setTreeSelection(null);
      }
    },
    async addOrEditLocationNode({ data, createNew, parent }) {
      let defaultMeta = {
        TypeName: "",
        Icon: "",
      };
      const meta = Object.assign(defaultMeta, data.Meta);
      const children = data.Children || [];
      const nodeType = data.NodeType;
      delete data.Meta;
      delete data.Children;
      delete data.NodeKey;
      delete data.NodeType;
      let entityName =
        nodeType === LocationTreeNodeTypeLocation ? "Location" : "Site";
      let expand =
        nodeType === LocationTreeNodeTypeLocation
          ? locationQueryExpand
          : siteQueryExpand;
      let api = createNew ? oDataPost : oDataPatch;
      let rsp = await api(entityName, data);
      if (rsp.status == 204) {
        // 简单粗暴的重新查询一遍，更新导航属性
        const { value } = await oDataQuery(entityName, {
          $expand: expand,
          $filter: `Id eq ${data.Id}`,
        });
        if (value && value.length > 0) {
          let newData = value[0];
          newData.Meta = meta;
          newData.NodeType = nodeType;
          newData.Children = children;
          newData.NodeKey =
            nodeType === LocationTreeNodeTypeLocation
              ? getLocationNodeKey(newData)
              : getSiteNodeKey(newData);
          newData.Parent = parent;
          this.$message.success(this.$t("template.updateSuccess"));
          return newData;
        }
      } else if (rsp) {
        const { value } = await oDataQuery(entityName, {
          $expand: expand,
          $filter: `Id eq ${rsp.Id}`,
        });
        if (value && value.length > 0) {
          let newData = value[0];
          newData.Meta = meta;
          newData.NodeType = nodeType;
          newData.Children = children;
          newData.NodeKey =
            nodeType === LocationTreeNodeTypeLocation
              ? getLocationNodeKey(newData)
              : getSiteNodeKey(newData);
          newData.Parent = parent;
          this.$message.success(this.$t("template.addSuccess"));
          return newData;
        }
      } else {
        this.$message.error(this.$t("template.updateFail"));
      }
    },
    async onEditAccept(nodeType) {
      let data = cloneDeep(
        toRaw(
          nodeType === LocationTreeNodeTypeLocation
            ? this.locationEditModel
            : this.siteEditModel
        )
      );
      let parent = data.Parent;
      delete data.LocationType;
      delete data.SiteType;
      // delete data.Classification;
      // delete data.SecondClassification;
      delete data.Parent;
      data.NodeType = nodeType;
      if (this.createTopmost === false && this.createNew) {
        parent = this.$refs.locationTreeView.currentNode.data;
        if (nodeType === LocationTreeNodeTypeLocation) {
          data.ParentId = parent.Id;
        } else {
          data.LocationId = parent.Id;
        }
      }
      const newData = await this.addOrEditLocationNode({
        data,
        createNew: this.createNew,
        parent,
      });
      let oriNode = this.$refs.locationTreeView.getNode(newData.NodeKey);
      if (oriNode) {
        Object.assign(oriNode.data, newData);
      } else if (parent) {
        this.$refs.locationTreeView.append(newData, parent.NodeKey);
      } else {
        this.locationTree.push(newData);
      }
    },
    onNodeClick(data) {
      let nodes = this.$refs.locationTreeView.store._getAllNodes();
      nodes.forEach((n) => {
        n.setChecked(false, false);
      });
      let node = this.$refs.locationTreeView.getNode(data.NodeKey);
      node.setChecked(true, false);
    },
    onTreeOperation(cmd) {
      if (cmd === "expand" || cmd === "collapse") {
        let expand = cmd === "expand";
        let nodes = this.$refs.locationTreeView.store._getAllNodes();
        nodes.forEach((n) => (n.expanded = expand));
      }
    },
    onNodeOperation(cmd) {
      if (cmd === "topmost" || cmd === "subLocation") {
        //新建最高区域、子区域
        this.createTopmost = cmd === "topmost";
        this.createNew = true;
        for (let prop in this.locationEditModel) {
          this.locationEditModel[prop] = undefined;
        }
        if (cmd === "subLocation") {
          // this.locationEditModel.ClassificationId =
          //   this.current.ClassificationId;
        }
        this.locationEditModalVisible = true;
      } else if (cmd === "site") {
        // 新建采样点
        this.createTopmost = false;
        this.createNew = true;
        for (let prop in this.siteEditModel) {
          this.siteEditModel[prop] = undefined;
        }
        this.siteEditModalVisible = true;
      } else if (cmd === "edit") {
        // 编辑
        this.createTopmost = false;
        this.createNew = false;
        if (this.currentType === 0) {
          console.error(`can not edit null`);
          return;
        }
        let data = cloneDeep(toRaw(this.current));
        if (this.currentType === LocationTreeNodeTypeLocation) {
          this.locationEditModel = data;
          this.locationEditModalVisible = true;
        } else {
          this.siteEditModel = data;
          this.siteEditModalVisible = true;
        }
      } else if (cmd === "disable") {
        // 禁用
        if (this.currentType === 0) {
          console.error(`can not disable null`);
          return;
        }
        this.toggleNodeActive(this.current);
      } else {
        console.error(`can not process tree command ${cmd}`);
      }
    },
    toggleNodeActive(data) {
      const locationArray = [];
      const siteArray = [];
      const getUpdateDataRecrusive = (d, isDown, isActive) => {
        const array =
          d.NodeType === LocationTreeNodeTypeLocation
            ? locationArray
            : siteArray;
        if (d.IsActive !== isActive) {
          array.push({ Id: d.Id, IsActive: isActive, NodeKey: d.NodeKey });
        }
        if (isDown) {
          if (d.Children) {
            d.Children.forEach((c) =>
              getUpdateDataRecrusive(c, isDown, isActive)
            );
          }
        } else {
          if (d.Parent) {
            getUpdateDataRecrusive(d.Parent, isDown, isActive);
          }
        }
      };

      // 禁用节点向下递归禁用，启用节点向上递归启用
      const isDown = data.IsActive ? true : false;
      const isActive = data.IsActive ? false : true;
      getUpdateDataRecrusive(data, isDown, isActive);

      const batch = {};
      // warning 不要改变加入batch的顺序，在处理response时有顺序
      if (locationArray.length > 0) {
        batch.Location = locationArray.map((item) => {
          return { Id: item.Id, IsActive: item.IsActive };
        });
      }
      if (siteArray.length > 0) {
        batch.Site = siteArray.map((item) => {
          return { Id: item.Id, IsActive: item.IsActive };
        });
      }
      var param = {
        LocationId: locationArray.length ? data.Id : 0,
        SiteId: locationArray.length == 0 ? siteArray[0].Id : 0,
        IsActive: isActive,
      };
      request({
        url: `/Location/EditLocationStatus`,
        method: "post",
        data: param,
      }).then((ret) => {
        if (ret.data.Result) {
          locationArray.forEach((element) => {
            const nodeKey = element.NodeKey;
            const data = this.$refs.locationTreeView.getNode(nodeKey).data;
            data.IsActive = isActive;
          });
          siteArray.forEach((element) => {
            const nodeKey = element.NodeKey;
            const data = this.$refs.locationTreeView.getNode(nodeKey).data;
            data.IsActive = isActive;
          });
          // const nodeKey =
          //   idx < locationArray.length
          //     ? locationArray[idx].NodeKey
          //     : siteArray[idx - locationArray.length].NodeKey;
          // const data = this.$refs.locationTreeView.getNode(nodeKey).data;
          // data.IsActive = isActive;
        }
        this.setTreeSelection(
          isActive || this.showInactived ? data.NodeKey : undefined
        );
        this.updateExpanded();
        this.$refs.locationTreeView.filter(this.showInactived);
        let nodes = this.$refs.locationTreeView.store._getAllNodes();
        nodes.forEach((n) => {
          n.expanded = this.expanded.some((str) => str === n.data.NodeKey);
        });

        if (!this.showInactived) {
          // 改变到禁用状态是需要更新下
          this.$nextTick(() => this.updateExpanded());
        }

        //TODO 根据返回是否能禁用内容显示关联的计划
        //TOTO 刷新节点，和关于展开和折叠
        // this.updateTreeView();
      });
    },
    updateTreeView() {
      const locationIds =
        this.$store.state.user.locations == undefined
          ? []
          : this.$store.state.user.locations.map((l) => l.LocationId);
      getLocationTree(true, true, locationIds).then((locations) => {
        this.locationTree = locations;
        this.$nextTick(() => {
          this.$refs.locationTreeView.filter(this.showInactived);
        });
      });
    },
    updateExpanded() {
      this.expanded = this.$refs.locationTreeView.store
        ._getAllNodes()
        .filter(
          (n) =>
            n.expanded === true &&
            (this.showInactived || n.data.IsActive === true)
        )
        .map((n) => n.data.NodeKey);
      this.$emit("setExpanded");
    },
    handleActiveChange() {
      this.updateExpanded();
      if (
        this.current &&
        this.current.IsActive === false &&
        !this.showInactived
      ) {
        this.setTreeSelection(undefined);
      }
      this.$refs.locationTreeView.filter(this.showInactived);
      let nodes = this.$refs.locationTreeView.store._getAllNodes();
      nodes.forEach((n) => {
        n.expanded = this.expanded.some((str) => str === n.data.NodeKey);
      });

      if (!this.showInactived) {
        // 改变到禁用状态是需要更新下
        this.$nextTick(() => this.updateExpanded());
      }
    },
    nodeAllowDrag(draggingNode) {
      return draggingNode.data.IsActive === true;
    },
    nodeAllowDrop(draggingNode, dropNode, type) {
      let draggingData = draggingNode.data;
      let dropData = dropNode.data;
      // 不能drop到采样点  不能drop到禁用节点
      // 当前用户是否有权限操作根
      if (
        dropData.NodeType === LocationTreeNodeTypeSite ||
        dropData.IsActive !== true
      ) {
        return false;
      }

      if (
        !this.hasAllLocationPermission &&
        type !== "inner" &&
        !dropNode.Parent
      ) {
        // 用户被设定了区域，不能直接往树的根节点释放
        return false;
      }

      if (draggingData.NodeType === LocationTreeNodeTypeLocation) {
        // 区域不能drop到低级区域
        if (
          type === "inner" &&
          draggingData.Meta.Weight <= dropData.Meta.Weight
        ) {
          return false;
        }
      } else if (draggingData.NodeType === LocationTreeNodeTypeSite) {
        // 采样点不能drop到最高级
        if (dropData.ParentId == undefined && type !== "inner") {
          return false;
        }
      }
      return true;
    },
    onNodeDrop(draggingNode, dropNode, dropType) {
      const dragData = draggingNode.data;
      const entityName =
        dragData.NodeType === LocationTreeNodeTypeLocation
          ? "Location"
          : "Site";
      const updateData = {
        Id: dragData.Id,
      };
      let newParentData = undefined;
      if (dropType === "inner") {
        newParentData = dropNode.data;
      } else {
        newParentData = dropNode.data.Parent;
      }
      const newParentId = newParentData == undefined ? null : newParentData.Id;
      if (dragData.NodeType === LocationTreeNodeTypeLocation) {
        updateData.ParentId = newParentId;
      } else {
        updateData.LocationId = newParentId;
      }
      this.$update(entityName, updateData).then((ret) => {
        Object.assign(dragData, updateData);
        dragData.Parent = newParentData;
        this.setTreeSelection(dragData.NodeKey);
      });
    },
    filterNode(showInactived, data, node) {
      const ret = showInactived || data.IsActive === true;
      if (ret) {
        if (data.Children && data.Children.length > 0) {
          node.isLeaf = !data.Children.some(
            (c) => showInactived || c.IsActive === true
          );
        } else {
          node.isLeaf = true;
        }
      }
      return ret;
    },
    setTreeSelection(nodeKey) {
      this.$refs.locationTreeView.setCurrentKey(nodeKey);
      let node = null;
      if (nodeKey) {
        node = this.$refs.locationTreeView.getNode(nodeKey);
      }
      this.$refs.locationTreeView.$emit(
        "current-change",
        node ? node.data : null,
        node
      );
    },
    getLocationNameFromSite() {
      if (this.current && this.current.NodeType === LocationTreeNodeTypeSite) {
        let node = this.$refs.locationTreeView.getNode(
          this.current.Parent.NodeKey
        );
        return node ? node.data.Name : "";
      }
      return "";
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
      $--app-main-inner-padding - 56px
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

.el-tabs {
  height: calc(
    /*55px tab header*/ 100vh - 84px - 2 * $--app-main-padding - 2 *
      $--app-main-inner-padding - 56px
  );
}
</style>

<!-- <style>
.el-tabs {
  height: 100%;
}
.el-descriptions__body > table {
  table-layout: fixed;
}
</style> -->
