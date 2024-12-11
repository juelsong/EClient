import { request, requestRaw } from "@/utils/request";
import { CurrentWorkSpaceParameter, DelayedTestsParameter, PlanParameter, Result, PlanResult } from "@/defs/Model";

export function createCurrentWorkSpace(data: CurrentWorkSpaceParameter) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/CurrentWorkSpace/CurrentWorkSpace`,
    method: "post",
    data
  })
}

export function createTests(data: DelayedTestsParameter) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/CurrentWorkSpace/Tests`,
    method: "post",
    data
  })
}

export function createPlan(data: PlanParameter) {
  return request.request<Result<PlanResult>, Result<PlanResult>>({
    url: `/CurrentWorkSpace/Plan`,
    method: "post",
    data
  })
}

export function createTest(cwId: number, userId: number) {
  //TODO check request&response body
  return requestRaw({
    url: `/CurrentWorkSpace/Test/${cwId}/${userId}`,
    method: "post"
  })
}
