import { OdataQuery } from "odata";
import { User } from "@/defs/Entity";
import { oDataQuery } from "@/utils/odata";

export declare type formatterFunc = (
  row: any,
  column: any,
  cellValue: any,
  index: number
) => string;
export declare interface IProperty {
  name: string;
  formatter?: formatterFunc;
}
export declare interface IEntity {
  name: string;
  upperName: string;
  properties: Array<IProperty>;
}

export async function showUserName(newData: any[]) {
  let userIds = newData.filter((e) => e.CreateBy).map<any>((e) => e.CreateBy);
  userIds.push(...newData.filter((e) => e.UpdateBy).map((e) => e.UpdateBy));
  userIds = Array.from(new Set(userIds));
  if (
    userIds.length &&
    !(userIds[0] instanceof String || typeof userIds[0] === "string")
  ) {
    const options: OdataQuery = {
      $select: "Id,Account,RealName",
      $filter: `Id in [${userIds.join(",")}]`,
    };
    const userRsp = await oDataQuery("User", options);
    if (userRsp.value) {
      const users = userRsp.value as User[];
      newData.forEach((entity) => {
        // TODO 账户？真名？
        if (entity.CreateBy) {
          const user = users.find((u) => u.Id == entity.CreateBy);
          if (user) {
            entity.CreateBy = user.Account;
          } else if (entity.CreateBy == 1 || entity.CreateBy == 2) {
            entity.CreateBy = "SYSTEM";
          }
        }
        if (entity.UpdateBy) {
          const user = users.find((u) => u.Id == entity.UpdateBy);
          if (user) {
            entity.UpdateBy = user.Account;
          } else if (entity.UpdateBy == 1 || entity.UpdateBy == 2) {
            entity.UpdateBy = "SYSTEM";
          }
        }
      });
    }
  }
}
export const auditActions = ["Insert", "Delete", "Update"];
