import {
    ZRenderType,
    Rect,
    RectShape,
    Image,
    Text,
    PathStyleProps,
    ImageStyleProps,
    TextStyleProps,
    Group,
    ElementProps,
    Displayable,
    ElementEvent
} from "echarts/node_modules/zrender"
import {
    Dictionary,
    ZRRawMouseEvent
} from "echarts/node_modules/zrender/lib/core/types";
import Eventful, { EventCallback, EventProcessor } from "echarts/node_modules/zrender/lib/core/Eventful";

import {
    LocationConfig,
    SpotHeight,
    SpotWidth,
    SpotRectStyle,
    Symbol,
    SymbolStroke,
    Level,
    changeSvgImageColor
} from "./SymbolConfig";

const NormalBackground: string = "#1890FF20";
const NormalForegroud: string = "black";
declare interface LocationEvent extends Dictionary<EventCallback<any[]>> {
    click: EventCallback<Location>,
}

declare interface ResizeSpotMul {
    x: number,
    y: number,
    width: number,
    height: number,
}
class LocationEventProcessor implements EventProcessor<LocationEvent>{ }

class Location extends Eventful<LocationEvent> implements Symbol<LocationConfig> {
    private _selected: boolean;
    private _scale: number;
    private _level: Level = Level.Normal;

    private render: ZRenderType;
    private config: LocationConfig;
    private parent: Group;
    private group: Group;
    private rect: Rect;
    private icon: Image;
    private title: Text;
    private sizeSpot: Array<Rect>;
    // 组内部元素zorder使用z2属性，组间zorder使用z属性,选中为200，没选中为100
    constructor(zr: ZRenderType, cfg: LocationConfig, parent: Group) {
        super(new LocationEventProcessor());
        this.render = zr;

        this.config = Object.assign({
            x: 0.0,
            y: 0.0,
            width: 200,
            height: 100,
            title: "",
            drawRect: true
        }, cfg);
        this._selected = false;
        this._scale = 1.0;
        const rectShape = new RectShape()
        rectShape.x = 0;
        rectShape.y = 0;
        rectShape.width = this.width;
        rectShape.height = this.height;

        const rectStyle: PathStyleProps = {
            fill: NormalBackground,
            stroke: SymbolStroke,
            lineWidth: 0,
        };
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

        this.group = new Group(groupProp);
        this.rect = new Rect({ shape: rectShape, style: rectStyle, cursor: "default", z: 100, z2: 0, invisible: !this.config.drawRect });
        this.icon = new Image({ style: iconStyle, cursor: "default", z: 100, z2: 100 });
        this.title = new Text({ style: titleStyle, cursor: "default", z: 100, z2: 100 });

        let lastX: number | undefined = undefined, lastY: number | undefined = undefined;
        let lastResizeSpotMul: ResizeSpotMul | undefined = undefined;
        const resizeSpotOnMouseDown = (ev: ElementEvent, rs: ResizeSpotMul, cursor: string) => {
            // console.log(`resizeSpotOnMouseDown`);
            if (lastResizeSpotMul != undefined) {
                return;
            }
            const e = ev.event as ZRRawMouseEvent;
            lastX = e.screenX;
            lastY = e.screenY;
            lastResizeSpotMul = rs;

            this.rect.attr({ cursor: cursor });
            this.icon.attr({ cursor: cursor });
            this.title.attr({ cursor: cursor });
        };
        const resizeSpotOnMouseUp = (ev: ElementEvent) => {
            lastX = lastY = undefined;
            lastResizeSpotMul = undefined;
            this.rect.attr({ cursor: "pointer" });
            this.icon.attr({ cursor: "pointer" });
            this.title.attr({ cursor: "pointer" });
        };

        const resizeSpotOnMouseMove = (ev: ElementEvent) => {
            const e = ev.event as ZRRawMouseEvent;
            if (lastX && lastY && lastResizeSpotMul && e.screenX && e.screenY) {
                const diffX = e.screenX - lastX;
                const diffY = e.screenY - lastY;
                this.x += lastResizeSpotMul.x * diffX;
                this.y += lastResizeSpotMul.y * diffY;
                this.width += lastResizeSpotMul.width * diffX;
                this.height += lastResizeSpotMul.height * diffY;
                lastX = e.screenX;
                lastY = e.screenY;
            }
        }

        const resizeCursor: Array<string> = ["nw-resize", "n-resize", "ne-resize", "w-resize", "e-resize", "sw-resize", "s-resize", "se-resize"];
        const resizeMul: Array<ResizeSpotMul> = [
            { x: 1, y: 1, width: -1, height: -1 },
            { x: 0, y: 1, width: 0, height: -1 },
            { x: 0, y: 1, width: 1, height: -1 },

            { x: 1, y: 0, width: -1, height: 0 },
            { x: 1, y: 1, width: 0, height: 0 },
            { x: 0, y: 0, width: 1, height: 0 },

            { x: 1, y: 0, width: -1, height: 1 },
            { x: 0, y: 0, width: 0, height: 1 },
            { x: 0, y: 0, width: 1, height: 1 }
        ];
        this.sizeSpot = new Array<Rect>();
        const halfWidth = this.width / 2;
        const halfHeight = this.height / 2;
        const halfSpotWidth = SpotWidth / 2;
        const halfSpotHeight = SpotHeight / 2;
        for (let j = 0; j < resizeCursor.length; j++) {
            const i = j < 4 ? j : j + 1;
            const resizeRect = new Rect({
                shape: {
                    x: (i % 3) * halfWidth - halfSpotWidth,
                    y: Math.floor(i / 3) * halfHeight - halfSpotHeight,
                    width: SpotWidth,
                    height: SpotHeight
                },
                style: SpotRectStyle,
                cursor: resizeCursor[j],
                invisible: true,
                silent: true,
                z2: 200
            });
            resizeRect.on("mousedown", ev => resizeSpotOnMouseDown(ev, resizeMul[i], resizeCursor[j]));
            this.sizeSpot.push(resizeRect);
            this.group.add(resizeRect);
        }

        if (cfg.canMove) {
            this.group.on("mousedown", ev => {
                resizeSpotOnMouseDown(ev, resizeMul[4], 'move');
                this.trigger('click');
            });
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

        this.group.add(this.rect);
        this.group.add(this.icon);
        this.group.add(this.title);

        this.parent = parent;
        this.parent.add(this.group);
        // this.render.add(this.group);
    }
    public getConfig(): LocationConfig {
        return this.config;
    }
    public get selected() {
        return this._selected;
    }
    public set selected(val: boolean) {
        this._selected = val;
        this.sizeSpot.forEach(resizeRect => {
            resizeRect.attr({ silent: !val, invisible: !val });
        })
        const groupZordr = val ? 200 : 100;
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
        this.rect.attr({
            shape: {
                width: this.config.width * val,
                height: this.config.height * val
            }
        });
        this.group.attr({ x: this.x, y: this.y });
        this.updateSpotPosition(this.width, this.height);
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

    public get width() {
        return this.config.width * this.scale;
    }
    public set width(val: number) {
        this.config.width = val / this.scale;
        this.rect.attr({ shape: { width: val } });
        this.updateSpotPosition(val, undefined);
    }

    public get height() {
        return this.config.height * this.scale;
    }
    public set height(val: number) {
        this.config.height = val / this.scale;
        this.rect.attr({ shape: { height: val } });
        this.updateSpotPosition(undefined, val);
    }
    public get level(): Level {
        return this._level;
    }
    public set level(val: Level) {
        this._level = val;
        changeSvgImageColor(this.config.icon as HTMLImageElement, val);
        this.icon.attr({ style: { image: this.config.icon } });
        //this.render.refresh();
    }

    public dispose() {
        this.parent.remove(this.group);
    }

    public contain(x: number, y: number): boolean {
        let ret = this.rect.contain(x, y);
        if (this.selected) {
            this.sizeSpot.forEach(spot => {
                ret = ret || spot.contain(x, y);
            });
        }
        return ret;
    }

    updateSpotPosition(rectWidth?: number, rectHeight?: number): void {
        const shapes: Array<Partial<RectShape>> = new Array<Partial<RectShape>>();
        for (let i = 0; i < this.sizeSpot.length; i++) {
            shapes.push({});
        }
        if (rectWidth) {
            const halfWidth = rectWidth / 2;
            const halfSpotWidth = SpotWidth / 2;
            shapes.forEach((shape, idx) => {
                shape.x = ((idx < 4 ? idx : idx + 1) % 3) * halfWidth - halfSpotWidth;
            })
        }
        if (rectHeight) {
            const halfHeight = rectHeight / 2;
            const halfSpotHeight = SpotHeight / 2;
            shapes.forEach((shape, idx) => {
                shape.y = Math.floor((idx < 4 ? idx : idx + 1) / 3) * halfHeight - halfSpotHeight;
            })
        }
        this.sizeSpot.forEach((s, idx) => {
            s.attr({ shape: shapes[idx] });
        });
    }
}

export default Location;