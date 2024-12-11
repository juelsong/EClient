import {
    ZRenderType,
    Image,
    Text,
    ImageStyleProps,
    TextStyleProps,
    Group,
    ElementProps,
    Displayable,
    ElementEvent,
    Rect
} from "echarts/node_modules/zrender"
import Eventful, { EventCallback, EventProcessor } from "echarts/node_modules/zrender/lib/core/Eventful";
import { Dictionary, ZRRawMouseEvent } from "echarts/node_modules/zrender/lib/core/types";
import { SymbolConfig, Symbol, SymbolStroke, Level, changeSvgImageColor } from "./SymbolConfig";

const NormalBackground: string = "#A989EE20";
const NormalForegroud: string = "black";
declare interface SiteEvent extends Dictionary<EventCallback<any[]>> {
    click: EventCallback<Location>,
}

class SiteEventProcessor implements EventProcessor<SiteEvent>{ }


class Site extends Eventful<SiteEvent> implements Symbol<SymbolConfig> {
    private _selected: boolean;
    private _scale: number;
    private _level: Level = Level.Normal;
    private render: ZRenderType;
    private config: SymbolConfig;
    private parent: Group;
    private rect: Rect;
    private group: Group;
    private icon: Image;
    private title: Text;
    // private moveSpot: Rect;
    // 组内部元素zorder使用z2属性，组间zorder使用z属性,选中为400，没选中为300,比Location的200、100要大
    constructor(zr: ZRenderType, cfg: SymbolConfig, parent: Group) {
        super(new SiteEventProcessor());
        this.render = zr;
        this.config = Object.assign({
            x: 0.0,
            y: 0.0,
            title: "",
        }, cfg);
        this._selected = false;
        this._scale = 1.0;
        const iconStyle: ImageStyleProps = {
            image: cfg.icon,
            x: 8,
            y: 8,
            width: 30,
            height: 30,
        };
        const titleStyle: TextStyleProps = {
            text: cfg.title,
            x: 46,
            y: 14,
            fontSize: 18,
            fontWeight: "bold",
            fill: NormalForegroud
        };
        const groupProp: ElementProps = {
            x: this.x,
            y: this.y,
            scaleX: 1.0,
            scaleY: 1.0,
        };
        let lastX: number | undefined = undefined, lastY: number | undefined = undefined;

        const resizeSpotOnMouseDown = (ev: ElementEvent) => {
            const e = ev.event as ZRRawMouseEvent;
            lastX = e.screenX;
            lastY = e.screenY;
            this.icon.attr({ cursor: "move" });
            this.title.attr({ cursor: "move" });
            this.trigger("click");
        };
        const resizeSpotOnMouseUp = (ev: ElementEvent) => {
            lastX = lastY = undefined;
            this.icon.attr({ cursor: "pointer" });
            this.title.attr({ cursor: "pointer" });
        };

        const resizeSpotOnMouseMove = (ev: ElementEvent) => {
            const e = ev.event as ZRRawMouseEvent;
            if (lastX && lastY && e.screenX && e.screenY) {
                const diffX = e.screenX - lastX;
                const diffY = e.screenY - lastY;
                this.x += diffX;
                this.y += diffY;
                lastX = e.screenX;
                lastY = e.screenY;
            }
        }

        this.group = new Group(groupProp);
        this.icon = new Image({ style: iconStyle, cursor: "default", z: 300, z2: 100 });
        this.title = new Text({ style: titleStyle, cursor: "default", z: 300, z2: 100 });

        if (cfg.canMove) {
            this.group.on("mousedown", resizeSpotOnMouseDown);

            this.render.on("mouseup", resizeSpotOnMouseUp);
            this.render.on("mousemove", resizeSpotOnMouseMove);
        }

        this.group.on("click", () => {
            this.trigger("click");
        });

        this.group.on("mouseover", () => {
            this.trigger("mouseenter");
        });

        this.group.on("mouseout", () => {
            this.trigger("mouseleave");
        });

        this.group.add(this.icon);
        this.group.add(this.title);
        // this.group.add(this.moveSpot);

        const sss = this.group.getBoundingRect();
        this.rect = new Rect({
            shape: {
                x: 0,
                y: 0,
                width: sss.width + 16,
                height: sss.height + 16,
            },
            style: {
                fill: NormalBackground,
                stroke: SymbolStroke,
                lineWidth: 0,
            },
            cursor: "default",
            z: 300,
            z2: 0
        });
        this.group.add(this.rect);
        this.parent = parent;
        this.parent.add(this.group);
        // this.render.add(this.group);
    }
    public getConfig(): SymbolConfig {
        return this.config;
    }
    public get selected() {
        return this._selected;
    }
    public set selected(val: boolean) {
        this._selected = val;
        const groupZordr = val ? 400 : 300;
        let displayable: Displayable;
        this.group.eachChild((el) => {
            if (el instanceof Displayable) {
                displayable = el as Displayable;
                displayable.attr({ z: groupZordr });
            }
        });
        this.rect.attr({ style: { lineWidth: val ? 1 : 0 } });
    }
    public get id(): string {
        return this.config.id;
    }
    public get scale() {
        return this._scale;
    }
    public set scale(val: number) {
        this._scale = val;
        this.group.attr({ x: this.x, y: this.y });
    }
    public get x() {
        return this.config.x * this.scale;
    }
    public set x(val: number) {
        this.config.x = val / this.scale;
        this.group.attr({ x: val });
    }

    public get y() {
        return this.config.y * this.scale;
    }
    public set y(val: number) {
        this.config.y = val / this.scale;
        this.group.attr({ y: val });
    }
    public get level(): Level {
        return this._level;
    }
    public set level(val: Level) {
        this._level = val;
        changeSvgImageColor(this.config.icon as HTMLImageElement, val);
        this.icon.attr({ style: { image: this.config.icon } });
    }
    public dispose() {
        this.parent.remove(this.group);
    }
    public contain(x: number, y: number): boolean {
        return this.rect.contain(x, y);
    }
}

export default Site;