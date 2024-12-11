import { requestRaw } from "@/utils/request";

export function createUploadDocument() {
  //TODO check request&response body
  return requestRaw({
    url: `/FileAttachment/UploadDocument`,
    method: "post"
  })
}

export function queryDownloadDocument(name: string) {
  //TODO check request&response body
  return requestRaw({
    url: `/FileAttachment/DownloadDocument/${name}`,
    method: "get"
  })
}
