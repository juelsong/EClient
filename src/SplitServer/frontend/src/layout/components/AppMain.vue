<template>
  <router-view :key="key" v-slot="{ Component }">
    <transition name="fade-transform" mode="out-in">
      <keep-alive :include="cachedViews">
        <component :is="Component"></component>
      </keep-alive>
    </transition>
  </router-view>
</template>

<script>
export default {
  name: "AppMain",
  computed: {
    cachedViews() {
      return this.$store.state.tagsView.cachedViews;
    },
    key() {
      return this.$route.path;
    },
  },
};
</script>
