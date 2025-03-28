import { jsPDF } from "jspdf";
import "jspdf-autotable";

// import "@/assets/font/test-normal-normal.js";
// import "@/utils/test-normal-normal.js";
import font from "./simsun-normal";

import { EquipmentTPM } from "@/defs/Entity";

function getDate(date) {
  var datetime = new Date(date);
  const year = datetime.getFullYear();
  const month = (datetime.getMonth() + 1).toString().padStart(2, "0");
  const day = datetime.getDate().toString().padStart(2, "0");
  return `${year}-${month}-${day}`;
}
function getTime(date) {
  var datetime = new Date(date);

  const hours = datetime.getHours().toString().padStart(2, "0");
  const minutes = datetime.getMinutes().toString().padStart(2, "0");
  const seconds = datetime.getSeconds().toString().padStart(2, "0");
  return `${hours}:${minutes}`;
}

export function equipmentTMPExport(equipmentTPM: EquipmentTPM) {
  // addCustomFontToJsPDF();
  // var equipmentTPM = tableData[index] as EquipmentTPM;
  for (const prop in equipmentTPM) {
    if (equipmentTPM[prop] == null || equipmentTPM[prop] == undefined)
      equipmentTPM[prop] = "";
  }
  const doc = new jsPDF();

  doc.addFileToVFS("test-normal.ttf", font.SongtiSCBlack); //SongtiSCBlack
  doc.addFont("test-normal.ttf", "test-normal", "normal");
  doc.setFont("test-normal");
  
  // doc.setFont(`times`, `normal`);
  // doc.text(`扬尘在线监测系统运行维护记录表`, 105, 80, undefined, `center`);
  // doc.text(`运行维护单位:${equipmentSelection.Name}`, 20, 20);
  // doc.text(`This is the default font.`, 20, 20);
  // 设置字体和标题
  doc.setFont(`test-normal`, `normal`);

  doc.setFontSize(16);
  doc.text(`扬尘在线监测系统运行维护记录表`, 105, 20, { align: `center` });
  doc.setFontSize(12);
  // 添加表格数据
  doc.text(`运行维护单位:${equipmentTPM.Name} `, 10, 50);
  doc.text(`编号:${equipmentTPM.EquipmentSerialNumber}`, 10, 60);
  doc.text(`站点名称:${equipmentTPM.Equipment?.Name}`, 10, 70);
  doc.text(`维护日期:${getDate(equipmentTPM.StartDate)}`, 10, 80);
  doc.text(`开始维护时间: ${getTime(equipmentTPM.StartDate)}`, 10, 90);
  doc.text(`结束维护时间: ${getTime(equipmentTPM.EndDate)}`, 10, 100);

  (doc as any).autoTable({
    head: [[`部件`, `检查项目`, `检查情况`, `处理方法`]],
    body: [
      [
        `电力系统`,
        `电力供应情况`,
        `${equipmentTPM.PowerSupplyIsError ? "不正常" : "正常"}`,
        `${equipmentTPM.PowerSupplyMsg}`,
      ],
      [
        `噪音系统`,
        `采样头、链接线路`,
        `${equipmentTPM.SampleIsNormal ? "不正常" : "正常"}`,
        `${equipmentTPM.SampleMsg}`,
      ],
      [
        `加热除湿`,
        `加热除湿温度测温结果(℃)`,
        `${equipmentTPM.TemperatureData}(℃)`,
        `${equipmentTPM.TemperatureMsg}`,
      ],
      [
        `扬尘系统`,
        `检查仪器线路、采样泵、校准系统是否正常`,
        `${equipmentTPM.DeviceIsError ? "不正常" : "正常"}`,
        `${equipmentTPM.DeviceMsg}`,
      ],
      [
        ``,
        `检查仪器滤罐是否需更换`,
        `${equipmentTPM.ResetEquipment ? "是" : "否"}`,
        `${equipmentTPM.ResetEquipmentMsg}`,
      ],
      [
        `气象参数`,
        `检查温度、湿度、大气压、风速、风向是否正常`,
        `${equipmentTPM.EnvironmentIsError ? "不正常" : "正常"}`,
        `${equipmentTPM.EnvironmentMsg}`,
      ],
      [
        `数据采集传输`,
        `数据传输是否正常`,
        `${equipmentTPM.DataTransmissionIsError ? "不正常" : "正常"}`,
        `${equipmentTPM.DataTransmissionMsg}`,
      ],
      [
        ``,
        `视频监控是否正常`,
        `${equipmentTPM.VedioIsError ? "不正常" : "正常"}`,
        `${equipmentTPM.VedioMsg}`,
      ],
    ],
    margin: { top: 120 },
    startY: 140, //

    styles: {
      font: `test-normal`, // 设置表格字体
      fontStyle: `normal`, // 设置表格字体样式
    },
  });
  doc.text(`设备现场情况(设备现场位置是否合格是否有喷淋遮挡、设备有无损坏):${equipmentTPM.DeviceLocation}`, 10, 220);

  doc.text(`操作人:${equipmentTPM.UserName}`, 10, 280);

  // doc.text(`第一页`, 105, 285, {
  //   align: `center`
  // });
  doc.addPage();

  doc.text(`附录C`, 105, 30, { align: `center` });
  doc.text(`（资料性附录）`, 105, 40, { align: `center` });
  doc.text(`校准记录表`, 105, 50, { align: `center` });
  doc.setFontSize(14);

  doc.text(`C.1 流量校准记录表`, 105, 60, { align: `center` });
  doc.setFontSize(12);

  doc.text(`工地名称:${equipmentTPM.EquipmentName}`, 10, 70);
  doc.text(
    `仪器名称:${equipmentTPM.Equipment?.EquipmentType?.Description}`,
    10,
    80
  );
  doc.text(`使用单位名称:`, 10, 90);
  doc.text(`工程报建号:`, 10, 100);
  doc.text(`仪器编号:${equipmentTPM.EquipmentControlNumber}`, 10, 110);
  doc.text(`校准时间:${getTime(equipmentTPM.C1StartDate)}`, 10, 120);
  doc.setFontSize(14);
  doc.text(`校准前`, 105, 150);
  doc.setFontSize(12);

  (doc as any).autoTable({
    head: [
      [
        `设定流量`,
        `实测流量1`,
        `实测流量2`,
        `实测流量3`,
        `相对误差`,
        `是否需要调整流量`,
      ],
    ],
    body: [
      [
        `${equipmentTPM.SetFlow}L/min`,
        `${equipmentTPM.BeforeFlow1}L/min`,
        `${equipmentTPM.BeforeFlow2}L/min`,
        `${equipmentTPM.BeforeFlow3}L/min`,
        `${equipmentTPM.BeforeRelativeError}`,
        `${equipmentTPM.BeforeSetFlow ? `是` : `否`}`,
      ],
    ],
    margin: { top: 140 },
    startY: 160, //
    styles: {
      font: `test-normal`, // 设置表格字体
      fontStyle: `normal`, // 设置表格字体样式
    },
  });
  doc.setFontSize(14);
  doc.text(`校准后`, 105, 190);
  doc.setFontSize(12);

  (doc as any).autoTable({
    head: [
      [
        `设定流量`,
        `实测流量1`,
        `实测流量2`,
        `实测流量3`,
        `相对误差`,
        `是否需要调整流量`,
      ],
    ],
    body: [
      [
        `${equipmentTPM.SetFlow}L/min`,
        `${equipmentTPM.BehindFlow1}L/min`,
        `${equipmentTPM.BehindFlow2}L/min`,
        `${equipmentTPM.BehindFlow3}L/min`,
        `${equipmentTPM.BehindRelativeError}`,
        `${equipmentTPM.BehindSetFlow ? `是` : `否`}`,
      ],
    ],
    margin: { top: 300 },
    startY: 200, //

    styles: {
      font: `test-normal`, // 设置表格字体
      fontStyle: `normal`, // 设置表格字体样式
    },
  });
  doc.text(`备注信息:${equipmentTPM.DescriptionC1}`, 10, 240);
  doc.text(
    `检查单位:${equipmentTPM.Name}             检查人:${
      equipmentTPM.UserName
    }             日期:${getDate(equipmentTPM.C2StartDate)}`,
    10,
    280
  );

  doc.addPage();

  // doc.text(`附录C`, 105, 30, { align: `center` });
  // doc.text(`（资料性附录）`, 105, 40, { align: `center` });
  // doc.text(`校准记录表`, 105, 50, { align: `center` });
  doc.setFontSize(14);

  doc.text(`C.2 颗粒物监测仪校准记录表`, 105, 40, { align: `center` });
  doc.setFontSize(12);

  doc.text(`工地名称:${equipmentTPM.Equipment?.Name}`, 10, 70);
  doc.text(
    `仪器名称:${equipmentTPM.Equipment?.EquipmentType?.Description}`,
    10,
    80
  );
  doc.text(`使用单位名称:`, 10, 90);
  doc.text(`工程报建号:`, 10, 100);
  doc.text(`仪器编号:${equipmentTPM.EquipmentControlNumber}`, 10, 110);
  doc.text(`校准时间:${getTime(equipmentTPM.C2StartDate)}`, 10, 120);
  doc.setFontSize(14);
  doc.text(`校零`, 105, 150);
  doc.setFontSize(12);

  (doc as any).autoTable({
    head: [[`仪器零值`, `实测值`,/*  `实测值2`, */ `相对误差`, `校零`, `是否合格`]],
    body: [
      [
        `${equipmentTPM.EquipmentZero}mg/m³`,
        `${equipmentTPM.EquipmentReal1}mg/m³`,
/*         `${equipmentTPM.EquipmentReal2}mg/m³`,
 */        `${equipmentTPM.EquipmentRelativeError}`,
        ``,
        `${equipmentTPM.BeforeEquipmentQualified ? `是` : `否`}`,
      ],
    ],
    margin: { top: 140 },
    startY: 160, //

    styles: {
      font: `test-normal`, // 设置表格字体
      fontStyle: `normal`, // 设置表格字体样式
    },
  });
  doc.setFontSize(14);
  doc.text(`校标`, 105, 200);
  doc.setFontSize(12);

  (doc as any).autoTable({
    head: [[`仪器跨度值`, `实测值`, `相对误差`, `校标`, `是否合格`]],
    body: [
      [
        `${equipmentTPM.EquipmentSpan}mg/m³`,
        `${equipmentTPM.EquipmentReal3}mg/m³`,
        `${equipmentTPM.BehindEquipmentRelativeError}`,
        `${equipmentTPM.BehindCalibration}`,
        `${equipmentTPM.BehindEquipmentQualified ? `是` : `否`}`,
      ],
    ],
    margin: { top: 210 },
    startY: 210, //

    styles: {
      font: `test-normal`, // 设置表格字体
      fontStyle: `normal`, // 设置表格字体样式
    },
  });
  doc.text(`备注信息:${equipmentTPM.DescriptionC1}`, 10, 260);
  doc.text(
    `检查单位:${equipmentTPM.Name}             检查人:${
      equipmentTPM.UserName
    }             日期:${getDate(equipmentTPM.C2StartDate)}`,
    10,
    280
  );

  //保存PDF
  doc.save(`${equipmentTPM.EquipmentName}-${getDate(new Date())}-运维台账.pdf`);
  // doc.text(`Hello world!`, 10, 10);
  // editModalVisible = true;
}
