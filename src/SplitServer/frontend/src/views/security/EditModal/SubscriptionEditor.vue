<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'Subscription.entity'
    )}`"
    width="700px"
    @open="onDialogOpen"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <div>
      <el-form
        ref="editForm"
        :model="modelInner"
        label-width="82px"
        label-position="right"
      >
        <el-form-item
          :label="$t('Subscription.editor.NotificationType')"
          prop="NotificationType"
          v-if="selectUser"
        >
          <el-select
            v-model="modelInner.NotificationTypeId"
            :disabled="selectUser"
          >
            <el-option
              v-for="val in notificationTypes"
              :key="val.key"
              :label="val.label"
              :value="val.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          :label="$t('Subscription.editor.User')"
          v-else
          prop="User"
        >
          <el-select v-model="modelInner.UserId" :disabled="!selectUser">
            <el-option
              v-for="val in users"
              :key="val.key"
              :label="val.label"
              :value="val.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          :label="$t('Subscription.editor.Location')"
          prop="Location"
        >
          <region-tree ref="regionTreeRef" v-model="modelInner.LocationId" />
        </el-form-item>
      </el-form>
      <div style="text-align: center">
        <el-transfer
          :data="selectOptions"
          v-model="selected"
          :titles="
            selectUser
              ? [
                  $t('Subscription.editor.User'),
                  $t('Subscription.editor.SubscribedUser'),
                ]
              : [
                  $t('Subscription.editor.NotificationType'),
                  $t('Subscription.editor.SubscribedNotificationType'),
                ]
          "
        />
      </div>
    </div>
  </el-dialog>
</template>

<script lang="ts">
import { oDataQuery } from "@/utils/odata";
import { OdataQuery as oDataOptions } from "odata";
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import RegionTree from "@/components/RegionTree.vue";
import { IEntityOption } from "@/defs/EntityOptions";
import getLableDescription from "@/i18n/localeHelper";

export declare interface IEditModel {
  LocationId?: number;
  UserId?: number;
  NotificationTypeId?: number;
}

export default defineComponent({
  name: "SubscriptionEditor",
  components: { RegionTree },
  props: {
    visible: {
      type: Boolean,
      default: false,
    },
    model: {
      type: Object,
    },
    createNew: {
      type: Boolean,
      default: false,
    },
    selectUser: {
      type: Boolean,
      default: true,
    },
  },
  emits: ["update:visible", "update:model", "accept"],
  setup(props, ctx) {
    const visibleInner = computed({
      get: () => props.visible,
      set: (newVal) => ctx.emit("update:visible", newVal),
    });
    return { visibleInner };
  },
  watch: {
    visibleInner(newVal) {
      this.refreshNoticeType();

      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
        this.$nextTick(() => {
          (this.$refs.regionTreeRef as any).loadData();
        });
      }
    },
  },
  mounted() {
    this.refreshNoticeType();
  },
  data() {
    return {
      modelInner: {} as IEditModel,
      notificationTypes: new Array<IEntityOption>(),
      users: new Array<IEntityOption>(),
      selected: new Array<Number>(),
    };
  },
  computed: {
    selectOptions() {
      return this.selectUser ? this.users : this.notificationTypes;
    },
  },
  methods: {
    getLableDescription,
    refreshNoticeType() {
      const notificationTypeOptions: oDataOptions = {
        $select: "Id,Name,EnName,ZhName",
        $filter: "IsActive eq true",
      };
      this.notificationTypes.splice(0);
      oDataQuery("NotificationType", notificationTypeOptions).then(
        ({ value }) => {
          this.notificationTypes = value.map((v) => {
            switch (this.getLableDescription()) {
              case "Zh":
                return {
                  key: v.Id,
                  label: v.ZhName,
                };
              case "En":
                return {
                  key: v.Id,
                  label: v.EnName,
                };
              default:
                return {
                  key: v.Id,
                  label: v.Name,
                };
            }
          });
        }
      );
    },
    onAcceptClick() {
      this.$emit("update:model", this.modelInner);
      this.visibleInner = false;
      this.$emit("accept", this.selected);
    },
    async onDialogOpen() {
      const userOptions: oDataOptions = {
        $select: "Id,RealName",
        $filter: "(IsActive eq true) and (Status eq 'Normal')",
      };

      this.users.splice(0);
      const userResult = await oDataQuery("User", userOptions);
      userResult.value.forEach((v) => {
        this.users.push({
          key: v.Id,
          label: v.RealName,
        });
      });
      await this.loadSubscribed();
    },
    async loadSubscribed() {
      const subscriptionOptions: oDataOptions = {
        $select: "Id,UserId,NotificationTypeId",
        $filter: "(IsActive eq true)",
      };
      if (this.modelInner.LocationId) {
        subscriptionOptions.$filter += ` and (LocationId eq ${this.modelInner.LocationId})`;
      } else {
        subscriptionOptions.$filter += " and (LocationId eq null)";
      }
      if (this.selectUser) {
        subscriptionOptions.$filter += ` and (NotificationTypeId eq ${this.modelInner.NotificationTypeId})`;
      } else {
        subscriptionOptions.$filter += ` and (UserId eq ${this.modelInner.UserId})`;
      }
      const subScriptionResult = await oDataQuery(
        "Subscription",
        subscriptionOptions
      );
      this.selected = this.selectUser
        ? subScriptionResult.value.map((s) => s.UserId)
        : subScriptionResult.value.map((s) => s.NotificationTypeId);
    },
  },
});
</script>
