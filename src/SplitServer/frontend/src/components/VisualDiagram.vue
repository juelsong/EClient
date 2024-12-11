<template>
  <div ref="mapContainer" style="height: 100%; width: 100%"></div>
</template>

<script lang="ts">
let lastX: number | undefined = undefined;
let lastY: number | undefined = undefined;
import { computed, defineComponent } from "vue";
import {
  SymbolConfig,
  Symbol,
  LocationConfig,
} from "@/components/VisualSymbol/SymbolConfig";
import {
  init as zInit,
  Image as zImage,
  ZRenderType,
  Group,
  ElementEvent,
} from "echarts/node_modules/zrender";

import {
  ImageLike,
  WithThisType,
  ZRRawMouseEvent,
} from "echarts/node_modules/zrender/lib/core/types";

import Site from "./VisualSymbol/Site";
import Location from "./VisualSymbol/Location";
export default defineComponent({
  setup(props, { emit }) {
    const symbols: Array<Symbol> = new Array<Symbol>();
    const selected = computed({
      get: () => props.modelValue ?? undefined,
      set: (val) => emit("update:modelValue", val),
    });
    return {
      symbols,
      selected,
    };
  },
  data() {
    return {
      zr: {} as ZRenderType,
      backImg: new zImage({
        style: {
          x: 0,
          y: 0,
        },
      }),
      scale: 1,
      symbolGroup: new Group({ x: 0, y: 0, scaleX: 1, scaleY: 1 }),
      backGroup: new Group({ x: 0, y: 0, scaleX: 1, scaleY: 1 }),
    };
  },
  props: {
    selectable: {
      type: Boolean,
      default: false,
      required: false,
    },
    modelValue: {
      type: String,
      default: undefined,
      required: false,
    },
  },
  mounted() {
    this.init();
    window.addEventListener("resize", this.onWindowResize);
  },
  unmounted() {
    this.dispose();
    window.removeEventListener("resize", this.onWindowResize);
  },
  watch: {
    scale(val) {
      this.backGroup.attr({ scaleX: val, scaleY: val });
      this.symbols.forEach((s) => {
        s.scale = val;
      });
    },
    modelValue(newVal) {
      this.setSelectedSymbolId(newVal);
    },
    selectable(val) {
      this.symbolGroup.attr({ silent: !val });
    },
  },
  methods: {
    init() {
      this.zr = zInit(this.$refs.mapContainer as HTMLElement); //{ renderer: "svg", }
      this.backGroup.add(this.backImg as zImage);
      this.zr.add(this.symbolGroup as Group);
      this.zr.add(this.backGroup as Group);
      this.zr.on("mousewheel", this.onMouseWheel);
      this.zr.on("mousedown", this.onMouseDown);
      this.zr.on("mouseup", this.onMouseUp);
      this.zr.on("mousemove", this.onMouseMove);
      this.symbolGroup.attr({ silent: !this.selectable });
    },
    dispose() {
      this.zr.dispose();
    },
    onWindowResize() {
      this.zr.resize();
    },
    zoomin() {
      this.scale *= 1.25;
    },
    zoomout() {
      this.scale *= 0.8;
    },
    reset() {
      this.scale = 1;
      this.backGroup.attr({
        x: 0,
        y: 0,
      });
      this.symbolGroup.attr({
        x: 0,
        y: 0,
      });
    },
    addSite(
      siteCfg: SymbolConfig,
      enter_callback?: Function,
      leave_callback?: Function,
      click_callback?: Function
    ) {
      const render = this.zr as ZRenderType;
      let site = new Site(render, siteCfg, this.symbolGroup as Group);
      site.scale = this.scale;
      const sss = () => {
        (click_callback ? click_callback : this.setSelectedSymbol)(site);
      };
      site.on("click", sss as WithThisType<never, Site>);
      if (enter_callback != undefined) {
        const fun_enter = () => {
          enter_callback(site);
        };
        site.on("mouseenter", fun_enter as WithThisType<never, Site>);
      }
      if (leave_callback != undefined) {
        const fun_leave = () => {
          leave_callback(site);
        };
        site.on("mouseleave", fun_leave as WithThisType<never, Site>);
      }
      this.symbols.push(site);
      return site;
    },
    addLocation(
      locationCfg: LocationConfig,
      enter_callback?: Function,
      leave_callback?: Function,
      click_callback?: Function
    ) {
      const render = this.zr as ZRenderType;
      let symbol = new Location(render, locationCfg, this.symbolGroup as Group);
      const sss = () => {
        click_callback ? click_callback : this.setSelectedSymbol(symbol);
      };
      symbol.on("click", sss as WithThisType<never, Location>);
      if (enter_callback != undefined) {
        const fun_enter = () => {
          enter_callback(symbol);
        };
        symbol.on("mouseenter", fun_enter as WithThisType<never, Location>);
      }
      if (leave_callback != undefined) {
        const fun_leave = () => {
          leave_callback(symbol);
        };
        symbol.on("mouseleave", fun_leave as WithThisType<never, Location>);
      }
      symbol.scale = this.scale;
      this.symbols.push(symbol);
      return symbol;
    },
    removeSymbol(id: string) {
      let idx = this.symbols.findIndex((s) => s.id == id);
      if (idx > -1) {
        let removed = this.symbols.splice(idx, 1);
        removed.forEach((r) => {
          if (r.selected) {
            this.setSelectedSymbol(undefined);
          }
          r.dispose();
        });
      }
    },
    setBackground(img?: ImageLike, width?: number, height?: number) {
      if (img && width && height) {
        this.backImg.attr({ style: { image: img, width, height } });
        this.backImg.attr({ invisible: false });
      } else {
        this.backImg.attr({ invisible: true });
      }
    },

    getAllSymbolConfig() {
      return this.symbols.map((s) => s.getConfig());
    },
    clear() {
      this.symbols.forEach((s) => s.dispose());
      this.symbols = [];
      this.selected = undefined;
    },
    onMouseWheel(ev: ElementEvent) {
      const { event } = ev;
      if (event instanceof WheelEvent) {
        const wheelEvent = event as WheelEvent;
        if (wheelEvent.deltaY > 0) {
          this.zoomout();
        } else {
          this.zoomin();
        }
        this.$nextTick(() => {
          this.$emit("updatePosition");
        });
      }
    },
    onMouseDown(ev: ElementEvent) {
      const mouseEvent = ev.event as ZRRawMouseEvent;
      if (mouseEvent && mouseEvent.zrX && mouseEvent.zrY) {
        if (
          !this.selectable ||
          this.symbols.every(
            (s) =>
              !s.contain(mouseEvent.zrX, mouseEvent.zrY) ||
              !s.getConfig().canMove
          )
        ) {
          lastX = mouseEvent.screenX;
          lastY = mouseEvent.screenY;
        }
      }
    },
    onMouseUp(ev: ElementEvent) {
      lastY = lastX = undefined;
      if (ev.target == undefined || ev.target.id == this.backImg.id) {
        this.setSelectedSymbol(undefined);
      }
    },
    onMouseMove(ev: ElementEvent) {
      const { event } = ev;
      if (lastX && lastY && event instanceof MouseEvent) {
        const mouseEvent = event as MouseEvent;
        const diffX = mouseEvent.screenX - lastX;
        const diffY = mouseEvent.screenY - lastY;
        lastX = mouseEvent.screenX;
        lastY = mouseEvent.screenY;
        this.backGroup.attr({
          x: this.backGroup.x + diffX,
          y: this.backGroup.y + diffY,
        });
        this.symbolGroup.attr({
          x: this.symbolGroup.x + diffX,
          y: this.symbolGroup.y + diffY,
        });
        this.$emit("updatePosition");
      }
    },
    setSelectedSymbol(symbol: Symbol | undefined): void {
      if (this.selectable) {
        for (let i = 0; i < this.symbols.length; i++) {
          const item = this.symbols[i];
          item.selected = false;
        }
        if (symbol) {
          this.selected = symbol.id;
          symbol.selected = true;
        } else {
          this.selected = undefined;
        }
      }
    },
    setSelectedSymbolId(id: string | undefined): void {
      for (let i = 0; i < this.symbols.length; i++) {
        const item = this.symbols[i];
        item.selected = item.id == id;
      }
      this.selected = id;
    },
  },
});
</script>
