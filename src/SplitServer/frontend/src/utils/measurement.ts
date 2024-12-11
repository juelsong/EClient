import { Measurement } from "@/defs/Entity";
import Decimal from "decimal.js";

export declare type IMeasurementResult = {
  MeasurementId: number;
  Value?: string;
};

const defaultCode = `
function Average(data) {
  var count = 0;
  for (let index = 0; index < data.length; index++) {
    count += data[index];
  }
  return count/data.length
};
function Min(data) {
  return Math.min(data);
}
function Max(data) {
  return Math.max(data);
}
`;

function caculate(
  measurements: Measurement[],
  result: IMeasurementResult[],
  formula: string
): string | undefined {
  const varNames = formula.split("$");
  let jsCode = defaultCode;
  varNames.forEach((varName) => {
    const varMeasurement = measurements.find((m) => m.Name == varName);
    if (varMeasurement) {
      const varResult = result.find(
        (r) => r.MeasurementId == varMeasurement.Id
      );
      if (varResult) {
        switch (varMeasurement.DataTypeId) {
          case 1: //string
            jsCode += `var $${varMeasurement.Name}$ = '${varResult.Value}';\r\n`;
            break;
          case 2: //int
          case 3: //number
            jsCode += `var $${varMeasurement.Name}$ = ${varResult.Value};\r\n`;
            break;
        }
      } else {
        // TODO
        jsCode += `var $${varMeasurement.Name}$ = 0;\r\n`;
      }
    }
  });
  jsCode += formula;
  try {
    const result = eval(jsCode);
    return result;
  } catch (err) {
    return undefined;
  }
}

function splitVarNames(formula: string) {
  // $细菌$+$霉菌$
  const varNames = new Array<string>();
  let localFormula = formula;
  // 2个$夹住的为变量名，正则不会，笨办法吧
  let idx = localFormula.indexOf("$");
  while (idx > -1) {
    let sub = localFormula.substring(idx + 1);
    idx = sub.indexOf("$");
    const varName = sub.substring(0, idx);
    varNames.push(varName);
    localFormula = sub.substring(idx + 1);
    idx = localFormula.indexOf("$");
  }
  return varNames;
}

export function usedByMeasurement(
  varName: string,
  measurements: Measurement[]
) {
  return measurements
    .filter(
      (m) =>
        m.Name != varName &&
        m.Formula &&
        m.Formula.length > 0 &&
        splitVarNames(m.Formula).includes(varName)
    )
    .map((m) => m.Name!);
}

export function checkMeasurement(
  validMeasurements: Measurement[],
  formula?: string
): boolean {
  if (formula) {
    const varNames = splitVarNames(formula);
    // 公式用到的变量，在列表中不存在
    const lackVarName = varNames.some((name) =>
      validMeasurements.every((vm) => vm.Name != name)
    );
    if (lackVarName) {
      return false;
    }
    const result = caculate(
      validMeasurements,
      validMeasurements.map((m) => {
        return {
          MeasurementId: m.Id,
          Value: `${Math.random()}`,
        };
      }),
      formula
    );
    return result != undefined;
  } else {
    return true;
  }
}

export function caculateMeasurement(
  result: IMeasurementResult[],
  timeFrameMeasurements: Measurement[][]
) {
  const constMeasurements = new Array<Measurement>();
  const ret = new Array<IMeasurementResult>();
  const localResult = new Array<IMeasurementResult>();

  localResult.push(
    ...result.map((r) => {
      return {
        ...r,
      };
    })
  );

  timeFrameMeasurements.forEach((measurements) => {
    constMeasurements.push(
      ...measurements.filter((m) => !m.Formula || m.Formula.length == 0)
    );
    const sorted = measurements
      .filter((m) => m.Formula && m.Formula.length > 0)
      .sort((a, b) => {
        return (a.Sequence ?? 0) - (b.Sequence ?? 0);
      });
    sorted.forEach((m) => {
      // TODO process NaN?
      let val = caculate(constMeasurements, localResult, m.Formula!);
      if (val) {
        switch (m.DataTypeId) {
          case 1: //string
            break;
          case 2: //int
            break;
          case 3: //number
            if (m.DecimalLength != undefined) {
              val = new Decimal(val).toFixed(m.DecimalLength);
            }
        }
      }
      const measurementResult: IMeasurementResult = {
        MeasurementId: m.Id,
        Value: val,
      };

      ret.push(measurementResult);
      localResult.push(measurementResult);
    });
  });

  return ret;
}
