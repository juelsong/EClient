import { oDataQuery } from "@/utils/odata";
import { OdataQuery } from "odata";
import * as vue from "vue";
import { i18n } from "@/i18n";
import { jsPDF, CellConfig, GState } from "jspdf";
import font from "./simsun-normal";
import autoTable, { UserOptions } from "jspdf-autotable";
import moment from "moment";
import {
  ElDialog,
  DialogBeforeCloseFn,
  ElMessageBox,
  ElProgress,
} from "element-plus";
declare type ProcessorCallback = (data: any[]) => Promise<void>;
export type PdfExportparameter = {
  entity: string;
  query: OdataQuery;
  fileName?: string;
  title?: string;
  pdfTitle?: string;
  headers: CellConfig[];
  processor?: ProcessorCallback;
};
const DefaultPageSize = 1000;

export async function exportPdf(param: PdfExportparameter) {
  let userCanceled = false;
  const visiable = vue.ref(true);
  const container = document.createElement("div");
  let exportCompleted = false;
  const progress = vue.ref(0);
  const closeConfirm: DialogBeforeCloseFn = (done) => {
    if (exportCompleted) {
      done();
    } else {
      ElMessageBox.confirm(
        i18n.global.t("Export.confirm.message"),
        i18n.global.t("confirm.title"),
        {
          confirmButtonText: i18n.global.t("template.accept"),
          cancelButtonText: i18n.global.t("template.cancel"),
          type: "warning",
          confirmButtonClass: "el-button--danger",
        }
      )
        .then(() => {
          userCanceled = true;
          done();
        })
        .catch(() => {
          // resume export
        });
    }
  };
  const dialogTitle = param.title ?? i18n.global.t("Export.export.title");
  const vnode = vue.createVNode(
    ElDialog,
    {
      title: dialogTitle,
      width: "220px",
      modelValue: visiable.value,
      destroyOnClose: true,
      // showClose:false,
      closeOnClickModal: false,
      closeOnPressEscape: false,
      onClosed: () => {
        vue.render(null, container);
      },
      beforeClose: closeConfirm,
    },
    {
      default: () =>
        vue.h(ElProgress, {
          type: "circle",
          percentage: progress.value,
          width: 180,
        }),
    }
  );
  vue.render(vnode, container);
  document.body.appendChild(container.firstElementChild!);

  const query: OdataQuery = {
    ...param.query,
    $count: true,
    $top: DefaultPageSize,
    $skip: 0,
  };
  let total = 0;
  let exported = 0;
  let totalDocumentCount = 0;
  let currentDocumentIdx = 1;
  const nowStr = moment().format("yyyy-MM-DD HHmmss");
  while (!userCanceled) {
    let result = await oDataQuery(param.entity, query);
    total = result["@odata.count"];

    totalDocumentCount = Math.floor(total / DefaultPageSize);
    if (total == 0 || totalDocumentCount * DefaultPageSize != total) {
      totalDocumentCount++;
    }
    let data = result.value as any[];
    if (param.processor) {
      await param.processor(data);
    }

    exported += data.length;
    query.$skip = exported;

    const doc = new jsPDF({
      orientation: "landscape",
      unit: "pt",
      encryption: {
        userPermissions: ["print"],
      },
    });
    doc.addFileToVFS("test-normal.ttf", font.SongtiSCBlack); //SongtiSCBlack
    doc.addFont("test-normal.ttf", "test-normal", "normal");
    doc.setFont("test-normal");
    // 处理 null
    data.forEach((d) => {
      for (let key in d) {
        if (param.headers.some((h) => h.name == key)) {
          if (d[key] == null) {
            d[key] = "N/A";
          }
        } else {
          delete d[key];
        }
      }
    });

    const tableOptions: UserOptions = {
      styles: {
        //设置表格的字体，不然表格中文也乱码
        fillColor: [255, 255, 255],
        font: "test-normal",
        textColor: [0, 0, 0],
        halign: "left",
        fontSize: 12,
      },
      headStyles: {
        lineWidth: 1,
        lineColor: "#c6e2ff",
        halign: "center",
        fillColor: "#409eff",
        textColor: "#fff",
      },
      // columnStyles: {
      //   0: { valign: "middle", cellWidth: 50 },
      //   1: { valign: "middle", cellWidth: 50 },
      //   2: { valign: "middle" },
      //   3: { valign: "middle", cellWidth: 200 },
      //   4: { valign: "middle", minCellWidth: 30 }, // 第4列居中,宽度最小30
      // },
      theme: "striped", // 主题
      startY: 0, // 距离上边的距离
      margin: 20, // 距离左右边的距离
      body: data, // 表格内容
      horizontalPageBreak: true,
      horizontalPageBreakRepeat: "EntityId",
      columns: param.headers.map((h) => {
        return {
          header: h.prompt,
          dataKey: h.name,
        };
      }),
    };
    if (param.pdfTitle) {
      tableOptions.startY = 80;
      const titleOffset = doc.internal.pageSize.width / 2;
      doc.setFontSize(20);
      doc.text(param.pdfTitle, titleOffset, 40, { align: "center" });
    }
    autoTable(doc, tableOptions);
    const pageCount = doc.getNumberOfPages();
    for (let pageIdx = 1; pageIdx <= pageCount; pageIdx++) {
      doc.setPage(pageIdx);
      doc.setFontSize(80);
      doc.saveGraphicsState();
      doc.setGState(new GState({ opacity: 0.1, "stroke-opacity": 0.1 }));
      doc.setTextColor("#AAAAA");
      doc.text(
        "EMIS",
        doc.internal.pageSize.width / 2,
        doc.internal.pageSize.height / 2,
        { align: "center", angle: 45 }
      );
      doc.restoreGraphicsState();
    }
    let pdfFileName = `export_${nowStr}.pdf`;
    if (param.fileName) {
      pdfFileName = `${param.fileName}_${nowStr}_${currentDocumentIdx}_${totalDocumentCount}.pdf`;
    }
    doc.save(pdfFileName);
    if (total) {
      // TODO 2位小数？
      progress.value = Number.parseFloat(((exported * 100) / total).toFixed(2));
    } else {
      progress.value = 100;
    }
    if (exported >= total) {
      break;
    }
    currentDocumentIdx++;
  }
  if (!userCanceled) {
    exportCompleted = true;
    visiable.value = false;
    setTimeout(() => {
      vue.render(null, container);
    }, 500);
  }
}
