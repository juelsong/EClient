/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑,代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Db.SQLite.TenantSlave
{
    using Microsoft.EntityFrameworkCore.Migrations;

    partial class v0048
    {
        void AddUserView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS UserV;
CREATE VIEW UserV as
SELECT u.Id           as UserId,
       u.RealName     as RealName,
       u.DepartmentId as DepartmentId,
       d.Name         as DepartmentName,
       u.LocationId   as LocationId,
       l.Name         as LocationName,
       l.LocationPath as LocationFullName,
       l.Breadcrumb   as LocationBreadcrumb,
       u.EmployeeId   as EmployeeId,
       u.Title        as Title,
       u.Account      as UserName,
       u.EMail        as EMail,
       u.Status       as Status,
       u.CreateBy     as CreateBy,
       u.CreatedTime  as CreatedTime,
       u.UpdateBy     as UpdateBy,
       u.UpdatedTime  as UpdatedTime,
       u.IsHidden     as IsHidden,
       u.IsActive     as IsActive,
       (
           IIF((
                  SELECT count(*)
                  from Permission as p
                  where exists(
                                SELECT 1
                                FROM RolePermission as rp
                                         INNER JOIN Role as r on r.Id = rp.RoleId
                                         INNER JOIN UserRole as ur on ur.RoleId = r.Id
                                WHERE ur.UserId = u.Id
                                  and rp.PermissionId = p.Id
                                  and (r.IsActive = 1 or r.IsHidden = 1)
                            )
              ) = 0, false, true)
           )          as HasAccess
FROM User u
         LEFT OUTER JOIN LocationV l on u.LocationId = l.LocationId
         LEFT OUTER JOIN Department d on u.DepartmentId = d.Id;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS UserV;
");
        }
    }
}
