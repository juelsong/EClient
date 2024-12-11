export const AppMainMixin = {
    inject: ["setAppMainInnerEnabled"],
    mounted() {
        this.setAppMainInnerEnabled(false);
    },
    unmounted() {
        this.setAppMainInnerEnabled(true);
    }
};