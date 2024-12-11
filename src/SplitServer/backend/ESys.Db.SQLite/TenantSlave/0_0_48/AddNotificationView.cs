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
        internal const string NotificationViewSql = @"
CREATE VIEW NotificationV as
SELECT n.Id as NotificationId
      ,n.NotificationTypeId
	  ,nt.Name as NotificationTypeName
	  ,nt.Description as NotificationTypeDesc
      ,n.NotificationDate
      ,n.UserId
	  ,u.LocationId as UserLocationId
      ,u.LocationBreadcrumb as UserLocationBreadcrumb
      ,n.SiteId
	  ,s.SiteName as SiteName
	  ,s.SiteDesc as SiteDesc
	  ,s.LocationId as SiteLocationId
      ,s.LocationBreadcrumb as SiteLocationBreadcrumb
      ,n.SampleId
      ,n.DeviationId
	  ,n.EquipmentId
	  ,e.LocationId as EquipmentLocationId
      ,e.LocationBreadcrumb as EquipmentLocationBreadcrumb
	  ,n.MediaId
	  ,m.LocationId as MediaLocationId
      ,m.LocationBreadcrumb as MediaLocationBreadcrumb
      ,n.Content
      ,n.CreateBy
      ,n.CreatedTime
      ,n.UpdateBy
      ,n.UpdatedTime
  FROM `Notification` n 
  INNER JOIN `NotificationType` nt on nt.Id = n.NotificationTypeId
  LEFT OUTER JOIN `UserV` u on u.UserId = n.UserId
  LEFT OUTER JOIN `SiteV` s on s.SiteId = n.SiteId
  LEFT OUTER JOIN `EquipmentV` e on e.EquipmentId = n.EquipmentId
  LEFT OUTER JOIN `MediaV` m on m.MediaId = n.MediaId;
";

        internal const string DropNotificationView = @"
DROP VIEW IF EXISTS NotificationV;";

        private void AddNotificationView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(DropNotificationView);
            upBuilder.Sql(NotificationViewSql);
            downBuilder.Sql(DropNotificationView);
        }
    }
}
