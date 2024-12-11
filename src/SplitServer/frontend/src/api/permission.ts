import { oDataQuery } from '@/utils/odata'
import { OdataQuery as oDataOptions } from "odata";

export declare interface PermissionTreeNode {
    /**
       * 树节点Id，等同于permissionID
       */
    NodeKey?: string,
    /**是否启用 */
    IsActive: boolean,
    ParentId?: number,
    Children: Array<Permission>,
    Parent?: Permission,
}

export declare interface Permission extends PermissionTreeNode {
    Id: number,
    Description?: string,
    Code?: string,
}


export async function getPermissionTree() {
    const permissionOptions: oDataOptions = {
        $select: "Id,Description,Code,ParentId",
    };
    const permissionResult = await oDataQuery("Permission", permissionOptions);
    const permissions = permissionResult.value as Array<Permission>;

    permissions.forEach(permission => {
        permission.NodeKey = `PERMISSION_${permission.Id}`;
        permission.Children = new Array<Permission>();
    });
    permissions.forEach(permission => {
        if (permission.ParentId) {
            const parent = permissions.find(p => p.Id == permission.ParentId);
            parent?.Children.push(permission);
            permission.Parent = parent;
        }
    });
    return permissions.filter(mc => mc.ParentId == undefined);
}