<template>
  <el-tree-select
    v-if="dropDown && multiple"
    class="region-tree region-tree-select"
    ref="treeSelectRef"
    v-model="selected"
    :data="locations"
    :multiple="true"
    check-strictly
    :render-after-expand="false"
    node-key="NodeKey"
    :props="treeProps"
    :clearable="clearable"
    :placeholder="placeholder"
    :expand-on-click-node="false"
    @node-click="handleNodeClick"
  >
    <template #default="{ data, node }">
      <el-button-group
        v-show="multiple && data.Children && data.Children.length > 0"
      >
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, true)"
        >
          <svg-icon icon-class="folder-checked" />
        </el-button>
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, false)"
        >
          <svg-icon icon-class="folder-delete" />
        </el-button>
      </el-button-group>
      <span>{{ data.Name }}</span>
    </template>
  </el-tree-select>
  <!--multiple 切换貌似el-select会出问题-->
  <el-tree-select
    v-if="dropDown && !multiple"
    class="region-tree region-tree-select"
    ref="treeSelectRef"
    v-model="selected"
    :data="locations"
    :multiple="false"
    check-strictly
    :render-after-expand="false"
    node-key="NodeKey"
    :props="treeProps"
    :clearable="clearable"
    :placeholder="placeholder"
    :expand-on-click-node="false"
    @node-click="handleNodeClick"
  >
    <template #default="{ data, node }">
      <el-button-group
        v-show="multiple && data.Children && data.Children.length > 0"
      >
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, true)"
        >
          <svg-icon icon-class="folder-checked" />
        </el-button>
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, false)"
        >
          <svg-icon icon-class="folder-delete" />
        </el-button>
      </el-button-group>
      <span>{{ data.Name }}</span>
    </template>
  </el-tree-select>
  <el-tree
    v-if="!dropDown"
    class="region-tree"
    ref="treeRef"
    :data="locations"
    :multiple="multiple"
    check-strictly
    show-checkbox
    :render-after-expand="false"
    node-key="NodeKey"
    :props="treeProps"
    :expand-on-click-node="false"
    @check-change="handleCheckChange"
  >
    <template #default="{ data, node }">
      <el-button-group
        v-show="multiple && data.Children && data.Children.length > 0"
      >
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, true)"
        >
          <svg-icon icon-class="folder-checked" />
        </el-button>
        <el-button
          circle
          size="small"
          @click="handleSelectChildren(node, false)"
        >
          <svg-icon icon-class="folder-delete" />
        </el-button>
      </el-button-group>
      <span>{{ data.Name }}</span>
    </template>
  </el-tree>
</template>

<script lang="ts">
import * as vue from "vue";
import store from "@/store";
import {
  getLocationTreeByBreadcrumb,
  LocationTreeChildNode,
  Location as BizLocation,
  getLocationNodeKey,
  getSiteNodeKey,
  Site as BizSite,
} from "@/api/location";
import type Node from "element-plus/es/components/tree/src/model/node";
import { ElTreeSelect, ElTree } from "element-plus";
export default vue.defineComponent({
  name: "RegionTree",
  props: {
    modelValue: {
      type: [Array, Number] as vue.PropType<number[] | number>,
    },
    clearable: {
      type: Boolean,
      default: true,
    },
    placeholder: {
      type: String,
      required: false,
      default: "请选择",
    },
    autoLoad: {
      type: Boolean,
      default: false,
    },
    selectSite: {
      type: Boolean,
      default: false,
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    dropDown: {
      type: Boolean,
      default: true,
    },
    rootLocationId: {
      type: Number,
      default: 0,
      required: false,
    },
  },
  emits: ["update:modelValue", "SelectionChanged"],
  setup(props, ctx) {
    function checkLocationDisable(data: LocationTreeChildNode) {
      return props.selectSite && data["LocationId"] == undefined;
    }
    const treeProps = {
      label: "Name",
      children: "Children",
      disabled: checkLocationDisable,
    };
    const selected = vue.ref();
    const locations = vue.ref<BizLocation[]>([]);
    const treeSelectRef = vue.ref<typeof ElTreeSelect>();
    const treeRef = vue.ref<typeof ElTree>();
    const selectedItem = vue.computed<LocationTreeChildNode[]>(() => {
      if (selected.value == undefined) {
        return [];
      } else {
        const nodeIds = Array.isArray(selected.value)
          ? (selected.value as string[])
          : [selected.value as string];
        const ids = nodeIds.map((id) => Number.parseInt(id.split("_")[1]));
        return getLocationRecursive(locations.value, ids);
      }
    });

    const getLocationRecursive = (
      locationArray: LocationTreeChildNode[],
      ids: number[]
    ): LocationTreeChildNode[] => {
      const ret = new Array<LocationTreeChildNode>();
      for (let i = 0; i < locationArray.length; i++) {
        const item = locationArray[i];
        if (
          !props.selectSite &&
          item["LocationId"] === undefined &&
          ids.includes(item.Id)
        ) {
          ret.push(item);
        } else if (
          props.selectSite &&
          item["LocationId"] &&
          ids.includes(item.Id)
        ) {
          ret.push(item);
        }
        if (item.Children) {
          const tmp = getLocationRecursive(item.Children, ids);
          ret.push(...tmp);
        }
      }
      return ret;
    };
    function handleNodeClick(data: LocationTreeChildNode, node: Node) {
      if (props.selectSite) {
        node.expanded = !node.expanded;
      }
    }

    function containsNodeKey(nodeKey: string) {
      if (selected.value && Array.isArray(selected.value)) {
        const idx = selected.value.findIndex((item) => item == nodeKey);
        return idx > -1;
      }
      return false;
    }
    function removeNodeKey(nodeKey: string) {
      if (selected.value && Array.isArray(selected.value)) {
        const idx = selected.value.findIndex((item) => item == nodeKey);
        if (idx > -1) {
          selected.value.splice(idx, 1);
        }
      }
    }
    function handleCheckChange(
      data: LocationTreeChildNode,
      isChecked: boolean
    ) {
      if (props.selectSite && data["LocationId"] == undefined) {
        return;
      }
      if (props.multiple) {
        if (isChecked) {
          if (!selected.value) {
            selected.value = new Array<string>();
          }

          if (!containsNodeKey(data.NodeKey!)) {
            selected.value.push(data.NodeKey);
          }
        } else if (selected.value) {
          removeNodeKey(data.NodeKey!);
        }
      } else {
        if (isChecked) {
          selected.value = data.NodeKey;
        } else {
          selected.value = undefined;
        }
      }
    }

    function setCheckedRecursive(node: Node, isChecked: boolean) {
      const data = node.data as LocationTreeChildNode;
      if (props.selectSite) {
        if (data["LocationId"]) {
          node.checked = isChecked;
        }
        if (props.multiple && node.childNodes) {
          node.childNodes.forEach((n) => setCheckedRecursive(n, isChecked));
        }
      } else {
        node.checked = isChecked;
      }
    }
    function handleSelectChildren(node: Node, isSelected: boolean) {
      const data = node.data as LocationTreeChildNode;
      if (!props.multiple) {
        return;
      }
      if (props.dropDown) {
        if (selected.value) {
          const isContains = containsNodeKey(data.NodeKey!);
          if (isSelected) {
            // 以前没选中 && （选择Location或者node是Site节点）
            if (!isContains && (!props.selectSite || data["LocationId"])) {
              selected.value.push(data.NodeKey);
            }
          } else if (isContains) {
            removeNodeKey(data.NodeKey!);
          }
        }
      } else {
        // checked 会触发 check-change 事件，无需单独处理
        setCheckedRecursive(node, isSelected);
      }
      if (node.childNodes) {
        node.childNodes.forEach((c) => handleSelectChildren(c, isSelected));
      }
    }
    vue.watch(selectedItem, (val) => {
      if (props.multiple) {
        ctx.emit(
          "update:modelValue",
          val.map((item) => item.Id)
        );
      } else {
        if (val.length > 0) {
          ctx.emit("update:modelValue", val[0].Id);
        } else {
          ctx.emit("update:modelValue", undefined);
        }
      }
      ctx.emit("SelectionChanged", val);
    });

    function setModelValue(val?: number[] | number) {
      if (val) {
        const nodeIds = Array.isArray(val) ? (val as number[]) : [val];
        const nodes = getLocationRecursive(locations.value, nodeIds);
        if (nodes.length > 0) {
          const keys = props.selectSite
            ? nodes.map((n) => getSiteNodeKey(n as BizSite))
            : nodes.map((n) => getLocationNodeKey(n as BizLocation));
          if (props.multiple) {
            if (selected.value == undefined || !Array.isArray(selected.value)) {
              selected.value = keys;
            } else {
              const added = keys.filter((k) => !selected.value.includes(k));
              const removed = selected.value.filter((k) => !keys.includes(k));
              removed.forEach((r) => {
                removeNodeKey(r);
              });
              selected.value.push(...added);
            }
          } else {
            if (
              selected.value == undefined ||
              Array.isArray(selected.value) ||
              selected.value != nodes[0].NodeKey
            ) {
              selected.value = nodes[0].NodeKey;
            }
          }
        } else {
          if (selected.value == undefined || !Array.isArray(selected.value)) {
            selected.value = [];
          } else if (selected.value.length > 0) {
            selected.value.splice(0, selected.value.length);
          }
        }
        if (treeRef.value) {
          treeRef.value.setCheckedKeys(
            nodes.map((n) => n.NodeKey!),
            true
          );
        }
      } else {
        if (props.multiple) {
          if (
            selected.value == undefined ||
            !Array.isArray(selected.value) ||
            selected.value.length > 0
          ) {
            selected.value = [];
          }
        } else {
          if (selected.value != undefined) {
            selected.value = undefined;
          }
        }
        if (treeRef.value) {
          treeRef.value.setCheckedKeys([], true);
        }
      }
    }
    vue.watch(() => props.modelValue, setModelValue);

    vue.onMounted(() => {
      if (props.autoLoad) {
        loadData();
      }
    });

    async function loadData() {
      const locationId = props.rootLocationId
        ? props.rootLocationId
        : (store.state.user.locationId as number);

      const setDisabled = (node: LocationTreeChildNode) => {
        node["disabled"] = true;
        if (node.Children) {
          node.Children.forEach((c) => setDisabled(c));
        }
      };
      const locationRet = await getLocationTreeByBreadcrumb(
        false,
        props.selectSite,
        locationId
      );
      if (locationRet && props.selectSite) {
        locationRet.forEach((l) => setDisabled(l));
      }
      locations.value.splice(0, locations.value.length, ...locationRet);
      setModelValue(props.modelValue);
    }
    function resetHeight() {
      if (treeSelectRef.value) {
        const nodes = window.document.querySelectorAll(
          ".region-tree .el-input__inner"
        );
        nodes.forEach((node) => {
          const input = node as HTMLInputElement;
          input.style.height = "unset";
        });
      }
    }
    return {
      selected,
      locations,
      treeProps,
      handleNodeClick,
      handleCheckChange,
      selectedItem,
      resetHeight,
      treeSelectRef,
      treeRef,
      loadData,
      handleSelectChildren,
    };
  },
});
</script>

<style lang="scss">
.el-tree-select__popper .el-select-dropdown__item {
  height: 24px;
}
</style>

<style lang="scss" scoped>
.region-tree-select {
  width: 100%;
}
</style>
