<script lang="ts">
import * as vue from "vue";
import moment, { Moment } from "moment";
import store from "@/store";
import { IOptions } from "@/defs/Types";

export enum CalanderViewMode {
  Week,
  Month,
}

declare interface ICalenderCell {
  Start: Moment;
  Stop: Moment;
  IsToday: boolean;
  IsNormal: boolean;
  Data: any[];
}

declare interface IColumnHeader {
  Start: Moment;
  Title: string;
  Highlight: boolean;
}

export default vue.defineComponent({
  name: "CalenderView",
  emits: ["update:mode", "rangeChanged"],
  props: {
    mode: {
      type: Number as vue.PropType<CalanderViewMode>,
      default: CalanderViewMode.Week,
    },
    data: {
      type: Array as vue.PropType<any[]>,
      default: [],
    },
    timestamp: {
      type: String,
      default: "",
    },
  },
  setup(props, ctx) {
    const cells = vue.ref(new Array<ICalenderCell[]>());
    const columnHeaders = vue.ref(new Array<IColumnHeader>());
    const currentMoment = vue.ref(moment());

    const startOfWeek = vue.computed(() => {
      // 英文周日，中文周一 作为周起始日
      // return store.getters.locale == "en" ? 0 : 1;
      // 固定周日为起始，周六为结束
      return 0;
    });
    const endOfWeek = vue.computed(() => {
      // 英文周一，中文周六 作为周结束日
      // return store.getters.locale == "en" ? 6 : 0;
      // 固定周日为起始，周六为结束
      return 6;
    });

    const modeInner = vue.computed({
      get: () => props.mode,
      set: (newVal) => ctx.emit("update:mode", newVal),
    });
    const selectedDate = vue.computed({
      get: () => currentMoment.value.format("yyyy-MM-DD HH:mm:ss"),
      set: (val) => {
        if (val) {
          const tmp = moment(val, "yyyy-MM-DD HH:mm:ss");
          if (tmp.isValid()) {
            currentMoment.value = tmp;
            return;
          }
        }
        currentMoment.value = moment();
      },
    });
    const isMonthMode = vue.computed(
      () => props.mode == CalanderViewMode.Month
    );

    const isWeekMode = vue.computed(() => props.mode == CalanderViewMode.Week);
    const modes = new Array<IOptions>();
    function getMoveStep() {
      switch (props.mode) {
        case CalanderViewMode.Month:
          return "months";
        case CalanderViewMode.Week:
          return "weeks";
        default:
          throw "not implement";
      }
    }
    function getRowStep() {
      switch (props.mode) {
        case CalanderViewMode.Month:
          return "weeks";
        case CalanderViewMode.Week:
          return "hours";
        default:
          throw "not implement";
      }
    }

    function getCellStop(start: Moment) {
      switch (props.mode) {
        case CalanderViewMode.Month:
          return moment(start).add(1, "days");
        case CalanderViewMode.Week:
          return moment(start).add(1, "hours");
        default:
          throw "not implement";
      }
    }
    function getDateTimeRange(forStart: boolean) {
      const unit = props.mode == CalanderViewMode.Month ? "month" : "day";
      const tmp = forStart
        ? moment(new Date(currentMoment.value.format())).startOf(unit)
        : moment(new Date(currentMoment.value.format())).endOf(unit);
      switch (props.mode) {
        case CalanderViewMode.Month:
          {
            const targetDay = forStart ? startOfWeek.value : endOfWeek.value;
            const dayStep = forStart ? -1 : 1;
            while (tmp.day() != targetDay) {
              tmp.add(dayStep, "days");
            }
          }
          break;
        case CalanderViewMode.Week:
          {
            const targetDay = startOfWeek.value;
            while (tmp.day() != targetDay) {
              tmp.add(-1, "days");
            }
          }
          break;
        default:
          throw "not implement";
      }
      return tmp;
    }
    function movePrevious() {
      currentMoment.value = moment(currentMoment.value).subtract(
        1,
        getMoveStep()
      );
    }
    function moveNext() {
      currentMoment.value = moment(currentMoment.value).add(1, getMoveStep());
    }
    function getFirstColumnStart(start: Moment, stop: Moment) {
      let tmp = moment(start);
      const ret = new Array<Moment>();
      const rowStep = getRowStep();
      while (tmp.isBefore(stop)) {
        ret.push(tmp);
        tmp = moment(tmp).add(1, rowStep);
      }
      return ret;
    }

    function updateRange() {
      // cells.value.splice(0, cells.value.length);
      columnHeaders.value.splice(0, columnHeaders.value.length);
      const start = getDateTimeRange(true);
      const stop = getDateTimeRange(false);
      const todayMomentDay = moment().startOf("days");
      const isSameMonth = (val: Moment) =>
        val.isSame(currentMoment.value, "month");
      const isToDay = (val: Moment) =>
        moment(val).isSame(todayMomentDay, "day");

      cells.value = getFirstColumnStart(start, stop).map((columnStart) => {
        const row = new Array<ICalenderCell>();
        for (let i = 0; i < 7; i++) {
          const cellStart = moment(columnStart).add(i, "days");
          row.push({
            Start: cellStart,
            Stop: getCellStop(cellStart),
            IsToday: props.mode == CalanderViewMode.Month && isToDay(cellStart),
            IsNormal: isSameMonth(cellStart),
            Data: [],
          });
        }
        return row;
      });
      const firstRow = cells.value[0];
      const lastRow = cells.value[cells.value.length - 1];
      const firstCell = firstRow[0];
      const lastCell = lastRow[lastRow.length - 1];
      ctx.emit("rangeChanged", [firstCell.Start, lastCell.Stop]);
      switch (props.mode) {
        case CalanderViewMode.Month: {
          columnHeaders.value.push(
            ...cells.value[0].map((c) => {
              return {
                Start: c.Start,
                Title: c.Start.format("dddd"),
                Highlight: false,
              };
            })
          );
          break;
        }
        case CalanderViewMode.Week: {
          columnHeaders.value.push(
            ...cells.value[0].map((c) => {
              return {
                Start: c.Start,
                Title: c.Start.format("dddd DD"),
                Highlight: isToDay(c.Start),
              };
            })
          );
          break;
        }
        default:
          throw "not implement";
      }
    }
    function handleClickToday() {
      currentMoment.value = moment().endOf("day");
    }
    vue.watch(currentMoment, updateRange, {
      deep: true,
    });
    vue.watch(() => props.mode, updateRange);
    vue.watch(startOfWeek, updateRange);
    vue.watch(
      () => store.getters.locale,
      (val: string) => {
        vue.nextTick(() => {
          columnHeaders.value.forEach((h) => {
            h.Start.locale(val);
            h.Title =
              props.mode == CalanderViewMode.Month
                ? h.Start.format("ddd")
                : h.Start.format("dddd DD");
          });
        });
      }
    );
    vue.watch(
      () => props.data,
      () => {
        cells.value.forEach((row) => {
          row.forEach((c) => {
            c.Data = props.data
              .filter(
                (d) =>
                  moment(d[props.timestamp]).isSameOrAfter(c.Start) &&
                  moment(d[props.timestamp]).isBefore(c.Stop)
              )
              .sort(
                (a, b) =>
                  moment(a[props.timestamp]).valueOf() -
                  moment(b[props.timestamp]).valueOf()
              );
          });
        });
      },
      { deep: true }
    );
    modes.push({
      label: "Week",
      value: CalanderViewMode.Week,
    });
    modes.push({
      label: "Month",
      value: CalanderViewMode.Month,
    });
    vue.onMounted(() => (currentMoment.value = moment().endOf("day")));
    return {
      movePrevious,
      moveNext,
      handleClickToday,
      selectedDate,
      modeInner,
      modes,
      cells,
      currentMoment,
      isMonthMode,
      isWeekMode,
      columnHeaders,
    };
  },
});
</script>

<template>
  <div :style="{ height: '100%' }">
    <el-row>
      <el-button @click="handleClickToday">
        {{ $t("Calender.Label.Today") }}
      </el-button>
      <el-button-group :style="{ margin: '0 20px' }">
        <el-button @click="movePrevious">
          <el-icon>
            <svg-icon icon-class="arrow-left" />
          </el-icon>
        </el-button>
        <el-button @click="moveNext">
          <el-icon>
            <svg-icon icon-class="arrow-right" />
          </el-icon>
        </el-button>
      </el-button-group>

      <span class="month-header">
        {{ currentMoment.format($t("Calender.Label.MonthFormatter")) }}
      </span>
      <div class="float-right" :style="{ right: '160px' }">
        <el-date-picker type="date" v-model="selectedDate" :clearable="false" />
      </div>

      <el-radio-group v-model="modeInner" class="float-right">
        <el-radio-button v-for="m in modes" :key="m.value" :label="m.value">
          {{ $t(`Calender.Mode.${m.label}`) }}
        </el-radio-button>
      </el-radio-group>
    </el-row>
    <el-row :style="{ marginTop: '20px' }">
      <div :class="isWeekMode ? 'time-header' : 'time-header hide'"></div>

      <div
        :class="`calender-cell column-header ${
          idx == 0 ? 'first-column' : ''
        } ${isWeekMode ? 'with-time-header' : ''} ${
          n.Highlight ? 'column-header-highlight' : ''
        } `"
        v-for="(n, idx) in columnHeaders"
        :key="n.Title"
      >
        {{ n.Title }}
      </div>
    </el-row>

    <el-scrollbar class="calender">
      <el-row v-for="(row, rowIdx) in cells" :key="rowIdx">
        <div
          :class="`time-header  ${isWeekMode ? '' : 'hide'} ${
            rowIdx > 0 ? 'timer-header-content' : ''
          }`"
        >
          {{
            rowIdx == 0 ? "" : `${(Array(2).join("0") + rowIdx).slice(-2)}:00`
          }}
        </div>
        <div
          v-for="(cell, colIndex) in row"
          :class="`calender-cell ${isWeekMode ? 'with-time-header' : ''}
          ${rowIdx == 0 ? 'first-row' : ''} ${
            colIndex == 0 ? 'first-column' : ''
          }`"
          :key="cell.Start.format('yyyy-MM-DD HH:mm:ss')"
        >
          <div
            :class="`calender-cell-day ${
              cell.IsToday ? 'calender-cell-day-today' : ''
            } ${!cell.IsNormal ? 'calender-cell-day-not-this-period' : ''}`"
            v-show="isMonthMode"
          >
            {{ cell.Start.format("DD") }}
          </div>
          <slot
            :start="cell.Start"
            :stop="cell.Stop"
            :row="rowIdx"
            :column="colIndex"
            :cellData="cell.Data"
          />
        </div>
      </el-row>
    </el-scrollbar>
  </div>
</template>

<style lang="scss" scoped>
.month-header {
  color: #404040;
  font-size: 20px;
  line-height: 32px;
  vertical-align: middle;
}
.float-right {
  position: absolute !important;
  right: 0px;
}

$time-header-width: 60px;

.time-header {
  width: $time-header-width;
  transform: translateY(-10px);
  color: #525252;
  &.hide {
    display: none;
  }
  &.timer-header-content:after {
    content: "";
    display: inline-block;
    background: url(data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNyIgaGVpZ2h0PSIxMiIgdmlld0JveD0iMCAwIDcgMTIiIGZpbGw9Im5vbmUiIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwL3N2ZyI+CjxnIGlkPSJHcm91cCA5MSI+CjxwYXRoIGlkPSJWZWN0b3IiIGQ9Ik02Ljg3NDI1IDUuNzA2NjRMMC41NTA4OTggMC4wNzk0OTkyQzAuMzM1MzI5IC0wLjEwNzE4MyAtNS4yMjIyN2UtMDcgMC4wNTI4Mjk3IC01LjA4MjM5ZS0wNyAwLjM3Mjg1N0wtMS42Mjk4MWUtMDggMTEuNjI3MUMtMi4zMDkzMWUtMDkgMTEuOTQ3MiAwLjMzNTMyOSAxMi4xMDcyIDAuNTUwODk4IDExLjkyMDVMNi44NzQyNSA2LjI5MzM2QzcuMDQxOTIgNi4xMzMzNCA3LjA0MTkyIDUuODY2NjUgNi44NzQyNSA1LjcwNjY0WiIgZmlsbD0iIzQwOUVGRiIvPgo8L2c+Cjwvc3ZnPgo=)
      no-repeat center;
    width: 12px;
    height: 12px;
  }
}

.calender-cell {
  width: calc(100% / 7);
  border: 1px solid #dfdfdf;
  border-left-width: 0px;
  border-top-width: 0px;
  min-height: 50px;
  padding: 6px;
  &.first-column {
    border-left-width: 1px;
  }
  &.with-time-header {
    width: calc((100% - $time-header-width) / 7);
  }
  &:has(.calender-cell-day-today) {
    border: #409eff solid 1px;
  }
  .calender-cell-day {
    margin: 0px 6px 6px 0px;
    color: #525252;
    &.calender-cell-day-today {
      color: #409eff;
    }
    &.calender-cell-day-not-this-period {
      color: rgba(82, 82, 82, 0.4);
    }
  }
  &.column-header {
    background: #cce9ff;
    border: #409eff solid 2px;
    line-height: 40px;
    text-align: center;
    border-left-width: 0px;
    &.first-column {
      border-left-width: 2px;
    }
    &.column-header-highlight {
      color: #409eff;
    }
  }
}

.calender {
  height: calc(100% - 32px - 20px - 54px);
}
</style>
