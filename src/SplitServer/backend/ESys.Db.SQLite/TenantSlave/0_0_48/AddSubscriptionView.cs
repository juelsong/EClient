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
        void AddSubscriptionView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS SubscriptionV;
CREATE VIEW SubscriptionV as
SELECT s.Id                 as SubscriptionId,
       u.UserId             as UserId,
       u.RealName           as RealName,
       u.DepartmentId       as DepartmentId,
       u.LocationId         as UserLocationId,
       u.LocationBreadcrumb as UserLocationBreadcrumb,
       u.EmployeeId         as EmployeeId,
       u.Title              as Title,
       u.UserName           as UserName,
       u.EMail              as EMail,
       u.Status             as UserStatus,
       s.NotificationTypeId as NotificationTypeId,
       n.Name               as NotificationTypeName,
       n.Description        as NotificationTypeDescription,
       n.IsActive           as IsNotificationTypeActive,
       s.PlanGroupId        as PlanGroupId,
       s.LocationId         as LocationId,
       l.Name               as LocationName,
       l.IsActive           as IsLocationActive,
       s.CreateBy           as CreateBy,
       s.CreatedTime        as CreatedTime,
       s.UpdateBy           as UpdateBy,
       s.UpdatedTime        as UpdatedTime,
       s.IsActive
FROM Subscription s
         INNER JOIN UserV u on u.UserId = s.UserId
         INNER JOIN NotificationType n ON n.Id = s.NotificationTypeId
         LEFT OUTER JOIN LocationV l on s.LocationId = l.LocationId;
");


            downBuilder.Sql(@"
DROP VIEW IF EXISTS SubscriptionV;
");
        }
    }
}
