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
        void AddRptFrequencySummaryView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS RptFrequencySummaryV;
CREATE view RptFrequencySummaryV
            (
             TestId,
             TestName,
             TestDescription,
             SampleBarcode,
             SampleCreatedDate,
             EnvironmentId,
             Environment,
             Volume,
             VolumeUOM,
             StartDate,
             EndDate,
             PerformedUsername,
             SampleId,
             Approved,
             PersonnelUserId,
             PersonnelSiteId,
             NoTest,
             SampleYear,
             LocationId,
             LocationName,
             SiteId,
             SiteClassificationId,
             SiteClassificationDesc,
             SystemId,
             SystemName,
             TestTypeId,
             TestTypeDescription,
             TestResult,
             TestResultNumber,
             UOM,
             ParticleSize,
             TestResultCreatedDate,
             DeviationId,
             LimitType,
             `Exception`,
             DeviationNumber
                )
AS
SELECT t.Id                                   as TestId,
       t.Name                                 AS TestName,
       t.Description                          AS TestDescription,
       s.BarCode                              AS SampleBarcode,
       s.CreatedTime                          AS SampleCreatedDate,
       s.EnvironmentId,
       IFNULL(e.Name, 'N/A')                  AS Environment,
       s.Vloume,
       s.MeasurementUOM                       AS VolumeUOM,
       s.StartDate,
       s.EndDate,
       ur.Account                             AS PerformedUsername,
       s.Id,
       s.Approved,
       s.PersonnelUserId,
       s.PersonnelSiteId,
       s.NoTest,
       strftime('%Y', s.CreatedTime) AS SampleYear,
       a.LocationId,
       a.LocationName,
       t.SiteId,
       a.SiteClassificationId,
       a.SiteClassificationDesc,
       a.SystemId,
       a.SystemName,
       t.TestTypeId,
       tt.Description                         as TestTypeDescription,
       r.TestResultValue                      AS TestResult,
       r.TestResultNumberValue                AS TestResultNumber,
       IFNULL(r.UOM, 'N/A')                   AS UOM,
       IFNULL(r.ParticleSize, -99)            AS ParticleSize,
       r.UpdateDate                           AS TestResultCreatedDate,
       r.MostServerDeviationId                AS DeviationId,
       r.MostServerLimit                      AS LimitType,
       r.MostServerLimit                      AS Exception,
       s.DeviationNote                        AS DeviationNumber
FROM Test t
         INNER JOIN
     Sample s ON t.Id = s.TestId
         INNER JOIN
     TestType tt ON t.TestTypeId = tt.Id
         INNER JOIN
     AllSystemPromptV a ON a.SiteId = t.SiteId
         LEFT OUTER JOIN
     EnvironmentDef e ON s.EnvironmentId = e.Id
         INNER JOIN
     RptSampleMart r ON s.Id = r.SampleId
         LEFT OUTER JOIN
     User ur ON s.PerformedUserId = ur.Id
WHERE s.NoTest <> 1
  AND s.IsNegative <> 1;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS RptFrequencySummaryV;
");
        }
    }
}
