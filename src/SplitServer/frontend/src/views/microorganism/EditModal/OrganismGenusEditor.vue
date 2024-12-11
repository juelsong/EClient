
<template>
  <el-dialog v-model="visibleInner"
             :close-on-click-modal="false"
             :title="`${createNew ? $t('template.new') : $t('template.edit')}-${$t('OrganismGenus.entity')}`"
             width="450px">
    <template #footer>
      <span class="dialog-footer">
        <el-button @click="visibleInner = false">{{$t('template.cancel')}}</el-button>
        <el-button type="primary"
                   @click="onAcceptClick">{{$t('template.accept')}}</el-button>
      </span>
    </template>
    <el-form ref="editForm"
             :model="modelInner"
             :rules="rules"
             label-width="56px"
             label-position="right">
      <el-form-item :label="$t('OrganismGenus.editor.OrganismType')"
                    prop="OrganismType">
        <o-data-selector ref="OrganismType"
                         :placeholder="`${$t('template.select', [$t('OrganismGenus.editor.OrganismType')])}`"
                         :multiple="false"
                         v-model="modelInner.OrganismTypeId"
                         entity="OrganismType"
                         label="Description"
                         value="Id" />
      </el-form-item>
      <el-form-item :label="$t('OrganismGenus.editor.Description')"
                    prop="Description">
        <el-input ref="modelInner.Description"
                  v-model="modelInner.Description"
                  :placeholder="$t('OrganismGenus.editor.Description')"></el-input>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script>
import { defineComponent, unref, computed } from "vue";
import cloneDeep from "lodash.clonedeep";
import ODataSelector from "@/components/ODataSelector.vue";
export default defineComponent({
  name: "OrganismGenusEditor",
  components: { ODataSelector },
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
    OrganismTypeId: {
      type: Number,
      default: null
    }
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
        let copyQuery = cloneDeep(unref(this.model));
        this.modelInner = copyQuery;
        if (this.modelInner.OrganismTypeId == undefined) {
          if (this.OrganismTypeId) {
            this.modelInner.OrganismTypeId = this.OrganismTypeId;
          }
        }
      }
      this.$nextTick(() => {
        this.$refs.editForm.clearValidate();
        this.$refs["OrganismType"].loadData();
        this.$refs["modelInner.Description"].focus();
      });
    },
  },
  data() {
    return {
      modelInner: {},
      rules: {
        Description: [
          {
            required: true,
            message: () =>
              this.$t("validator.template.required", [
                this.$t("OrganismGenus.editor.Description"),
              ]),
            trigger: "blur",
          },
          {
            validator: (rule, value, callback) => {
              if (value) {
                this.$exist(
                  "OrganismGenus",
                  "Description",
                  value,
                  this.modelInner.Id
                ).then((exist) => {
                  if (exist) {
                    callback(
                      new Error(
                        this.$t("validator.template.exist", [
                          this.$t("OrganismGenus.editor.Description"),
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
