import { PathStyleProps } from "echarts/node_modules/zrender";
import { ImageLike } from "echarts/node_modules/zrender/lib/core/types";

export declare class SymbolConfig {
  id: string;
  x: number;
  y: number;
  icon: string | ImageLike;
  title: string;
  canMove: boolean;
}

export declare class LocationConfig extends SymbolConfig {
  width: number;
  height: number;
  drawRect: boolean;
}

export const SpotWidth: number = 8;
export const SpotHeight: number = 8;

export const SymbolStroke: string = "#18A0FB";

export const SpotRectStyle: PathStyleProps = {
  fill: "#ADD8E6",
  stroke: "#1E90FF",
  lineWidth: 2,
};

export enum Level {
  Normal,
  Action,
  Alert,
}
export const imgBase64Header = "data:image/svg+xml;base64,";

const parser = new DOMParser();
const serializer = new XMLSerializer();
declare type SetUseColorFunc = (SVGUseElement) => void;

export function changeSvgImageColor(img: HTMLImageElement, level: Level) {
  const src = window.atob(img.src.substring(imgBase64Header.length));
  const xmlDoc = parser.parseFromString(src, "image/svg+xml");
  const useElements = xmlDoc.getElementsByTagName("use");
  const pathElements = xmlDoc.getElementsByTagName("path");
  const colorFunc: SetUseColorFunc =
    level == Level.Normal
      ? (e) => {
          e.removeAttribute("fill");
          e.removeAttribute("stroke");
        }
      : level == Level.Action
      ? (e) => {
          e.setAttribute("fill", "#FF0000");
          e.setAttribute("stroke", "#FF0000");
        }
      : (e) => {
          e.setAttribute("fill", "#FF9B00");
          e.setAttribute("stroke", "#FF9B00");
        };
  const pathColorFunc: SetUseColorFunc = (e) => {
    if (level == Level.Normal) {
      e.setAttribute("fill", "currentColor");
    } else if (level == Level.Action) {
      e.setAttribute("fill", "#FF0000");
    } else if (level == Level.Alert) {
      e.setAttribute("fill", "#FF9B00");
    }
  };
  for (let i = 0; i < useElements.length; i++) {
    const use = useElements[i];
    colorFunc(use);
  }
  for (let i = 0; i < pathElements.length; i++) {
    const path = pathElements[i];
    pathColorFunc(path);
  }
  const dst = serializer.serializeToString(xmlDoc);
  img.src = `${imgBase64Header}${window.btoa(dst)}`;
}

export declare interface Symbol<CFG extends SymbolConfig = SymbolConfig> {
  get scale(): number;
  set scale(val: number);
  get selected(): boolean;
  set selected(val: boolean);
  get id(): string;
  get level(): Level;
  set level(val: Level);
  dispose(): void;
  getConfig(): CFG;
  contain(x: number, y: number): boolean;
}
