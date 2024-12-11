import { IReply } from "@/defs/Reply";
import ErrorCode, { GetPromptKeyFromCode } from "@/defs/ErrorCode";
import store from "@/store";
import { ElMessage } from "element-plus";
import { i18n } from "@/i18n";
export function showPrompt(
  code: number,
  duration: number = 5,
  reason?: string
) {
  let prompt = i18n.global.t(GetPromptKeyFromCode(code));
  if (reason) {
    prompt = `${prompt}\r${reason}`;
  }
  ElMessage({
    message: prompt,
    type: "error",
    duration: duration * 1000,
  });
}

export declare class DeactivateAgainst {
  NavigationProperty: string;
  Values: Array<string>;
  EntityName: string;
}

export function buildDeactivateAgainstPrompt(data: DeactivateAgainst): string {
  const entityName = i18n.global.t(
    `AuditRecord.entities.${data.EntityName.replaceAll(".", "/")}.Title`
  );
  // const propertyName = i18n.global.t(`AuditRecord.entities.${data.EntityName.replaceAll('.', '/')}.properties.${data.NavigationProperty}`);
  return `${entityName}:${data.Values.join(",")}`;
}

export const checkEsignReply = (
  reply: IReply<any>,
  url: string,
  method: string,
  headerCount?: string
): Promise<any> => {
  return new Promise((resolve, reject) => {
    switch (reply.code) {
      case ErrorCode.NoError:
        store.commit("esign/SET_NEED_ESIGN", false);
        resolve(reply);
        break;
      case ErrorCode.Device.ConfigError:
      case ErrorCode.Device.ConnectError:
      case ErrorCode.Device.GetDataError:
      case ErrorCode.Device.SNError:
      case ErrorCode.Service.DataExpired:
      case ErrorCode.Service.InnerError:
      case ErrorCode.User.WrongPassword:
      case ErrorCode.User.NotExist:
      case ErrorCode.User.Forbidden:
      case ErrorCode.User.NotAuthorized:
      case ErrorCode.User.CodeError:
      case ErrorCode.Service.DuplicateESign:
      case ErrorCode.Workflow.LoadError:
        showPrompt(reply.code);
        reject(reply.code);
        break;
      case ErrorCode.User.NotValidPassword:
        showPrompt(reply.code, 5, reply.message);
        reject(reply.code);
        break;
      case ErrorCode.Service.NeedESign:
        if (reply.data.Current === 0) {
          store.dispatch("esign/popESign", {
            serialNumber: reply.data.SerialNumber,
            category: reply.data.Category,
            resolve: (v: any) => {
              store.commit("esign/SET_NEED_ESIGN", false);
              resolve(v);
            },
            reject: (v: any) => {
              store.commit("esign/SET_NEED_ESIGN", false);
              reject(v);
            },
            url: url,
            method: method,
            total: reply.data.Total,
            current: reply.data.Current,
            headerCount: headerCount,
          });
        } else {
          // 后续签名不更新resolve reject
          store.dispatch("esign/popESign", {
            serialNumber: reply.data.SerialNumber,
            category: reply.data.Category,
            url: url,
            method: method,
            total: reply.data.Total,
            current: reply.data.Current,
            headerCount: headerCount,
          });
        }
        break;
      case ErrorCode.User.TokenExpired:
        showPrompt(reply.code);
        break;
      case ErrorCode.Service.DeactiveInvalid:
        {
          const da = reply.data as Array<DeactivateAgainst>;
          const msg = da.map(buildDeactivateAgainstPrompt).join("\r");
          showPrompt(reply.code, 10, msg);
        }
        break;
      case ErrorCode.Import.DataError:
        showPrompt(reply.code);
        resolve(reply);

        break;

      default:
        reject("not implement error");
        break;
    }
  });
};
