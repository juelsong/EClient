import { requestRaw } from "@/utils/request";

export function createUploadImg() {
  //TODO check request&response body
  return requestRaw({
    url: `/File/UploadImg`,
    method: "post"
  })
}

export function queryImage(name: string) {
  //TODO check request&response body
  return requestRaw({
    url: `/File/Image/${name}`,
    method: "get"
  })
}
