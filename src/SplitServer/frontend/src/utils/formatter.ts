import moment from "moment";
import store from "@/store";
import { i18n } from "@/i18n";
export function booleanFormat(row: any, column: any, val?: any) {
  const valInner =
    val ?? (column?.property && row ? row[column.property] : undefined);

  if (valInner == true) {
    return i18n.global.t("label.yes");
  } else if (valInner == false) {
    return i18n.global.t("label.no");
  } else {
    return "";
  }
}

export function dateFormat(row: any, column: any, val?: any) {
  const valInner =
    val ?? (column?.property && row ? row[column.property] : undefined);
  if (!valInner) {
    return "";
  }
  return moment(valInner).format(store.state.user.userSettings.DateFormat);
}
export function datetimeFormat(row: any, column: any, val?: any) {
  const valInner =
    val ?? (column?.property && row ? row[column.property] : undefined);
  if (!valInner) {
    return "";
  }
  return moment(valInner).format(store.state.user.userSettings.DateTimeFormat);
}

export function datetimeFormatExceptMax(row: any, column: any, val?: any) {
  const valInner =
    val ?? (column?.property && row ? row[column.property] : undefined);
  if (!valInner) {
    return "";
  }
  const momentVal = moment(valInner);
  if (momentVal.year() >= 9999) {
    return "";
  } else {
    return momentVal.format(store.state.user.userSettings.DateTimeFormat);
  }
}

export function durationFormat(val?: any) {
  if (!val) {
    return "";
  }
  const duration = moment.duration(val);
  let hour = duration.days() * 24 + duration.hours();
  let minutes = duration.minutes();
  let seconds = duration.seconds();
  return `${hour < 10 ? "0" + hour : hour}:${
    minutes < 10 ? "0" + minutes : minutes
  }:${seconds < 10 ? "0" + seconds : seconds}`;
}

export function dateOnlyFormat(row: any, column: any, val?: any) {
  const valInner =
    val ?? (column?.property && row ? row[column.property] : undefined);
  if (!valInner) {
    return "";
  }
  return moment(valInner).format("YYYY-MM-DD");
}
