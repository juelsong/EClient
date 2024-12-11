export const WindowResizeMixin = {
    methods: {
        emitResize() {
            setTimeout(() => {
                window.dispatchEvent(new Event("resize"));
                this.setMoveYIfNeed(this);
            }, 10);
        },
        setMoveYIfNeed(component) {
            if (component && component.$refs) {
                for (const subName in component.$refs) {
                    const sub = component.$refs[subName];
                    if (subName == 'barRef') {
                        if (isNaN(sub.moveY)) {
                            sub.moveY = 0;
                        }
                        if (isNaN(sub.moveX)) {
                            sub.moveX = 0;
                        }
                    } else {
                        this.setMoveYIfNeed(sub);
                    }
                }
            }
        }
    }
}