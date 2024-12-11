<template>
  <el-dialog
    v-model="visibleInner"
    :close-on-click-modal="false"
    :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t(
      'MediaType.entity'
    )}`"
    width="400px"
  >
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">
          {{ $t("template.cancel") }}
        </el-button>
        <el-button type="primary" v-no-more-click @click="onAcceptClick">
          {{ $t("template.accept") }}
        </el-button>
      </span>
    </template>
    <el-form
      ref="editForm"
      :model="modelInner"
      :rules="rules"
      label-width="76px"
      label-position="right"
    >
      <el-form-item :label="$t('MediaType.editor.Name')" prop="Name">
        <el-input
          ref="modelInner.Name"
          v-model="modelInner.Name"
          :placeholder="$t('MediaType.editor.Name')"
        />
      </el-form-item>
      <el-form-item
        :label="$t('MediaType.editor.Description')"
        prop="Description"
      >
        <el-input
          ref="modelInner.Description"
          v-model="modelInner.Description"
          :placeholder="$t('MediaType.editor.Description')"
        />
      </el-form-item>
      <el-form-item
        :label="$t('MediaType.editor.ProductNumber')"
        prop="ProductNumber"
      >
        <el-input
          ref="modelInner.ProductNumber"
          v-model="modelInner.ProductNumber"
          :placeholder="$t('MediaType.editor.ProductNumber')"
        />
      </el-form-item>
      <el-form-item
        :label="$t('MediaType.editor.InventoryControl')"
        prop="InventoryControl"
      >
        <el-checkbox
          ref="modelInner.InventoryControl"
          v-model="modelInner.InventoryControl"
        />
      </el-form-item>
      <el-form-item
        :label="$t('MediaType.editor.NotificationPercentage')"
        prop="NotificationPercentage"
      >
        <el-input-number
          ref="modelInner.NotificationPercentage"
          v-model="modelInner.NotificationPercentage"
          :step="5"
          :min="0"
          :disabled="!modelInner.InventoryControl"
        />
        %
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, toRaw, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
// import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "MediaTypeEditor",
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
      if (newVal) {
        let copyQuery = cloneDeep(toRaw(this.model));
        this.modelInner = copyQuery;
        if (this.modelInner.InventoryControl == undefined)
          this.modelInner.InventoryControl = false;
        if (this.modelInner.NotificationPercentage == undefined)
          this.modelInner.NotificationPercentage = 0;
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        this.$refs["modelInner.Name"].focus();
      });
    },
  },
  data() {
    return {
      modelInner: {},
      rules: {
        Name: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("MediaType.editor.Name"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "MediaType",
                  "Name",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("MediaType.editor.Name"),
                        ])
                      )
                    );
                  } else {
                    callback();
                  }
                });
              }
            },
            trigger: "blur",
          },
        ],
        Description: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("MediaType.editor.Description"),
              ]),
            trigger: "blur",
          },
        ],
      },
    };
  },
  methods: {
    onAcceptClick() {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$emit("update:model", this.modelInner);
          this.visibleInner = false;
          this.$emit("accept");
        } else {
          return false;
        }
      });
    },
  },
});
</script>
