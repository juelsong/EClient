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
        void AddSampleView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS SampleV;

CREATE VIEW SampleV
AS
SELECT s.Id                                                 AS SampleId,
       s.ParentSampleId,
       s.ParentDilutionId,
       t.TimeFrameDilutionId,
       s.Name,
       s.TestId,
       t.TestTypeId,
       t.SiteId,
       st.Name                                              AS SiteName,
       st.LocationId,
       l.Name                                               AS LocationName,
       l.Breadcrumb,
       s.MediaId,
       s.EquipmentId,
       s.PersonnelSiteId,
       IFNULL(ps.Description, ps.Name) AS PersonnelSiteDescription,
       s.PersonnelUserId,
       u.Account                                            AS PersonnelUsername,
       u.EmployeeId                                         AS PersonnelEmpId,
       u.RealName                                           AS PersonnelRealName,
       u.LastMonitoredDate,
       u.InitialQualificationDate,
       u.NextQualificationDate,
       s.StartDate,
       s.EndDate,
       s.BarCode,
       s.MeasurementUOM,
       s.Vloume,
       u2.Account                                           AS PerformedUsername,
       u2.Id                                                AS PerformedUserId,
       s.NoTest,
       s.IsNegative,
       s.Approved,
       s.ApprovedUserId,
       s.ApprovedDate,
       s.DeviationNote,
       s.IsEmEdit,
       s.IsFdcEdit,
       s.IsCompleted,
       s.CompleteDate,
       e.Name                                               AS Environment,
       s.ParentOrgFoundId,
       s.ScheduledStartDate,
       s.SamplingCompleted,
       (SELECT COUNT(s1.Id)
        FROM Sample s1
        WHERE s1.ParentSampleId = s.Id
          AND s1.ParentOrgFoundId IS NOT NULL)              AS MaxChildOrgCount,
       s.CreateBy,
       s.CreatedTime,
       s.UpdateBy,
       s.UpdatedTime,
       s.IsActive
FROM Sample s
         INNER JOIN Test t ON s.TestId = t.Id
         INNER JOIN Site st ON t.SiteId = st.Id
         INNER JOIN LocationV l ON st.LocationId = l.LocationId
         INNER JOIN EnvironmentDef e ON e.Id = s.EnvironmentId
         LEFT JOIN PersonnelSite ps ON s.PersonnelSiteId = ps.Id
         LEFT JOIN User u ON s.PersonnelUserId = u.Id
         LEFT JOIN User u2 ON s.PerformedUserId = u2.Id;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS SampleV;
");
        }
    }
}
