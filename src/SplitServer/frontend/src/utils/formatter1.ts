import moment from "moment";
import store from "@/store";
export function dateFormat(row: any, column: any, val?: any) {
  const valInner = val ?? row[column.property];
  if (valInner == val) {
    return moment(val).format(store.state.user.userSettings.DateFormat);
  }
  return moment(valInner).format(store.state.user.userSettings.DateFormat);
}
export function datetimeFormat(row: any, column: any, val?: any) {
  const valInner = val ?? row[column.property];
  if (valInner == val) {
    return moment(val).format(store.state.user.userSettings.DateTimeFormat);
  }
  return moment(valInner).format(store.state.user.userSettings.DateTimeFormat);
}
