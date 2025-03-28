import { request } from "@/utils/request";
import { SubscriptionModel, EMailConfig, Result } from "@/defs/Model";

export function editSubscription(data: SubscriptionModel) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/Notification/Subscription`,
    method: "patch",
    data
  })
}

export function queryEmailConfig() {
  return request.request<Result<EMailConfig>, Result<EMailConfig>>({
    url: `/Notification/email-config`,
    method: "get"
  })
}

export function editEmailConfig(data: EMailConfig) {
  return request.request<Result<boolean>, Result<boolean>>({
    url: `/Notification/email-config`,
    method: "patch",
    data
  })
}
