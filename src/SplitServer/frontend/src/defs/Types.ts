import type { RuleItem } from "async-validator";
import { RouteMeta } from "vue-router";
export declare interface FormItemRule extends RuleItem {
  trigger?: string;
}
export declare type ValueOf<T> = T[keyof T];

export declare interface IOptions {
  label: string;
  value: string | number;
}
export declare interface ITag {
  fullPath: string;
  path: string;
  name: string;
  meta: RouteMeta;
}
/**
 * 用户权限
 */
export type UserPermission = {
  /**
   * 编码
   */
  Code: string;
  /**
   * 类型
   */
  Type?: number;
};
