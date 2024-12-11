import { requestRaw } from "@/utils/request";
import { Method } from "axios";
export default async function DownloadFile(
  url: string,
  fileName: string,
  method: Method = "POST",
  data: any = ""
) {
  requestRaw({
    url,
    method,
    data,
  }).then((res) => {
    let data = res.data; //文件数据
    if (!fileName && res.headers["content-disposition"]) {
      //Content-Disposition: attachment; filename=report.pdf; filename*=UTF-8''report.pdf
      // TODO filename* 或者中文
      // const cd: Record<string, string> = {};
      // res.headers["content-disposition"].split(";").forEach((str) => {
      //   const kvp = str.split("=");
      //   cd[kvp[0]] = kvp[1];
      // });

      // if (cd["filename"]) {
      //   fileName = cd["filename"];
      // }
      const contentDisposition = res.headers["content-disposition"];
      const filenameStarMatch = contentDisposition.match(
        /filename\*=UTF-8''(.+)/
      );
      if (filenameStarMatch && filenameStarMatch.length > 1) {
        // 对 URL 编码的字符串进行解码
        fileName = decodeURIComponent(filenameStarMatch[1]);
      }
    }

    if (!fileName) {
      fileName = "File";
    }
    // Others
    let a = document.createElement("a"),
      url = URL.createObjectURL(data);
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    setTimeout(function () {
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    }, 10);
  });
}
