
const { body } = document
const WIDTH = 992 // refer to Bootstrap's responsive design

export default {
    watch: {
        $route: {
            handler() {
                if (this.device === 'mobile' && this.sidebar.opened) {
                    this.$store.dispatch('app/closeSideBar', { withoutAnimation: false })
                }
            },
            deep: false,
        },
    },
    beforeMount() {
        window.addEventListener('resize', this.$_resizeHandler)
    },
    beforeUnmount() {
        window.removeEventListener('resize', this.$_resizeHandler)
    },
    mounted() {
        const isMobile = this.$_isMobile()
        if (isMobile) {
            this.$store.dispatch('app/toggleDevice', 'mobile')
            this.$store.dispatch('app/closeSideBar', { withoutAnimation: true })
        }
    },
    methods: {
        // use $_ for mixins properties
        // https://vuejs.org/v2/style-guide/index.html#Private-property-names-essential
        $_isMobile() {
            const rect = body.getBoundingClientRect()
            return rect.width - 1 < WIDTH
        },
        $_resizeHandler() {
            if (!document.hidden) {
                const isMobile = this.$_isMobile()
                this.$store.dispatch('app/toggleDevice', isMobile ? 'mobile' : 'desktop')

                if (isMobile) {
                    this.$store.dispatch('app/closeSideBar', { withoutAnimation: true })
                }
            }
        }
    }
}
