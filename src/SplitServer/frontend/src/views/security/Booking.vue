<template>
  <div style="height: 100%">
    <el-card
      @click="onClickCard"
      class="single-box-card subCard left"
      :header="$t('Location.entity')"
    >
      <div class="wrapper">
        <el-scrollbar max-height="100%">
          <el-tree
            ref="locationTree"
            :data="locations"
            :props="locationTreeProps"
            draggable
            node-key="NodeKey"
            @current-change="onLocationChange"
            highlight-current
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
    <el-tabs
      v-model="activeTabName"
      class="single-tabs subCard half"
      type="border-card"
    >
      <el-tab-pane
        :label="$t('NotificationType.entity')"
        name="notificationType"
      >
        <el-container>
          <el-main>
            <notification-type
              ref="notificationTypes"
              @selected="onNotificationTypeSelected"
              @editSubscription="onEditByNotificationType"
            />
          </el-main>
        </el-container>
      </el-tab-pane>
      <el-tab-pane :label="$t('Booking.title.User')" name="user">
        <el-container>
          <el-main>
            <simple-user
              ref="users"
              @selected="onUserSelected"
              @editSubscription="onEditByUser"
            />
          </el-main>
        </el-container>
      </el-tab-pane>
    </el-tabs>
    <el-card
      class="single-box-card subCard half"
      :header="
        isUserDetail ? $t('Booking.title.User') : $t('NotificationType.entity')
      "
    >
      <subscription
        ref="subscription"
        :showUser="isUserDetail"
        :locationId="selectedLocationId"
        :notificationTypeId="selectedNotificationTypeId"
        :userId="selectedUserId"
      />
    </el-card>
    <subscription-editor
      ref="subscriptionEditor"
      :createNew="false"
      :selectUser="isUserDetail"
      v-model:visible="editModalVisible"
      v-model:model="editModel"
      @accept="onEditAccept"
    />
  </div>
</template>

<script lang="ts">
import {
  defineComponent,
  ref,
  unref,
  reactive,
  toRaw,
  computed,
  Ref,
} from "vue";
import { Location, getLocationTree } from "@/api/location";
import NotificationType from "./NotificationType.vue";
import SimpleUser from "./SimpleUser.vue";
import Subscription from "./Subscription.vue";
import { request } from "@/utils/request";
import { BooleanReply } from "@/defs/Reply";
import { WindowResizeMixin } from "@/mixins/WindowResizeMixin";

import SubscriptionEditor from "./EditModal/SubscriptionEditor.vue";
import { IEditModel } from "./EditModal/SubscriptionEditor.vue";

declare interface ISubscriptionModel extends IEditModel {
  Subscribers: Array<number>;
}

export default defineComponent({
  name: "Booking",
  setup() {
    const activeTabName = ref("notificationType");
    const isUserDetail = computed(
      () => activeTabName.value == "notificationType"
    );
    //const editModel:
    const selectedLocationId: Ref<number | undefined> = ref();
    const selectedNotificationTypeId: Ref<number | undefined> = ref();
    const selectedUserId: Ref<number | undefined> = ref();
    return {
      activeTabName, //选中tab的
      isUserDetail, //详情为user
      selectedLocationId, //选中的Location Id
      selectedNotificationTypeId, // 选中的 NotificationType Id
      selectedUserId, //选中的User Id
      editModel: reactive<IEditModel>({}),
    };
  },
  mixins: [WindowResizeMixin],
  mounted() {
    this.loadData();
  },
  watch: {
    isUserDetail(val: Boolean) {
      if (val) {
        (this.$refs.users as any).clearSelection();
      } else {
        (this.$refs.notificationTypes as any).clearSelection();
      }
      (this as any).emitResize();
    },
  },
  components: {
    NotificationType,
    SimpleUser,
    Subscription,
    SubscriptionEditor,
  },
  data() {
    return {
      locations: new Array<Location>(), // 区域树数据
      locationTreeProps: {
        label: "Name",
        children: "Children",
      },
      editModalVisible: false,
    };
  },
  methods: {
    loadData() {
      const locationIds =
        this.$store.state.user.locations === undefined
          ? new Array<number>()
          : this.$store.state.user.locations.map((l) => l.LocationId);

      getLocationTree(false, false, locationIds).then((locations) => {
        this.locations = locations;
      });
    },
    onClickCard(e?: Event) {
      if (
        e &&
        e.target &&
        (e.target as HTMLElement).contains(
          (this.$refs.locationTree as any).$el as HTMLElement
        )
      ) {
        let nodes = (this.$refs.locationTree as any).store._getAllNodes();
        nodes.forEach((n) => {
          n.setChecked(false, false);
        });
        this.clearTreeSelection();
      }
    },
    clearTreeSelection() {
      (this.$refs.locationTree as any).setCurrentKey(null);
      (this.$refs.locationTree as any).$emit("current-change");
    },
    onLocationChange(data?: Location) {
      this.selectedLocationId = data?.Id;
    },
    onUserSelected(userId?: number) {
      this.selectedUserId = userId;
    },
    onNotificationTypeSelected(notificationTypeId?: number) {
      this.selectedNotificationTypeId = notificationTypeId;
    },
    onEditByNotificationType(id: number) {
      this.editModel.NotificationTypeId = id;
      this.editModel.UserId = undefined;
      this.editModel.LocationId = this.selectedLocationId;
      this.editModalVisible = true;
    },
    onEditByUser(id: number) {
      this.editModel.UserId = id;
      this.editModel.NotificationTypeId = undefined;
      this.editModel.LocationId = this.selectedLocationId;
      this.editModalVisible = true;
    },
    onEditAccept(selected: Array<number>) {
      const model = toRaw(this.editModel);
      const param: ISubscriptionModel = { Subscribers: unref(selected) };
      Object.assign(param, model);
      request({
        url: `/Notification/Subscription`,
        method: "patch",
        data: param,
      }).then((ret) => {
        const reply: BooleanReply = ret as any as BooleanReply;
        if (reply.success) {
          this.$message.success(this.$t("prompt.success"));
          (this.$refs.subscription as any).loadData();
        } else {
          this.$message.error(this.$t("prompt.failed"));
        }
      });
    },
  },
});
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.subCard {
  height: 100%;
}
.subCard.left {
  float: left;
  width: 280px;
  height: 100%;
}
.subCard.half {
  float: left;
  width: calc((100% - 300px) / 2);
  height: 100%;
  margin: 0px 0px 0px 10px;
}
</style>
