<template>
  <div style="height: 100%">
    <div class="subCard left">
      <MediaType @selectMediaType="onMediaChange" />
    </div>
    <div class="subCard right">
      <Media ref="compMedia" />
    </div>
  </div>
</template>

<script>
import MediaType from "./MediaType.vue";
import Media from "./Media.vue";
import { AppMainMixin } from "@/mixins/AppMainMixin";
export default {
  name: "Medium",
  components: { MediaType, Media },
  mixins: [AppMainMixin],
  methods: {
    onMediaChange(id) {
      let compMedia = this.$refs.compMedia;
      compMedia.searchMediaType(id);
    },
  },
};
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
$subCardLeftWidth: 670px;
$subCardLeftMargin: 18px;
.subCard {
  height: 100%;
}
.subCard.left {
  float: left;
  width: $subCardLeftWidth;
}
.subCard.right {
  float: right;
  width: calc(100% - $subCardLeftWidth - $subCardLeftMargin);
}
</style>
