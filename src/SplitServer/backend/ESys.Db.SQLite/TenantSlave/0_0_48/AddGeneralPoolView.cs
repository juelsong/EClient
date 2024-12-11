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
        void AddGeneralPoolV(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS GeneralPoolV;
CREATE VIEW `GeneralPoolV`
AS
SELECT CWS.Id AS CurrentWorkSpaceId,
  l.LocationId,
  l.LocationPath as LocationName,
  l.Barcode as LocationBarcode,
  l.Breadcrumb as LocationBreadcrumb,
  S.Id as SiteId,
  S.Name AS SiteName,
  S.Description AS SiteDescription,
  S.Barcode AS SiteBarcode,
  CWS.SampleId,
  CWS.SampleBarcode,
  SV.Approved AS SampleApproved,
  SV.StartDate AS SampleStartDate,
  SV.EndDate AS SampleEndDate,
  SV.MeasurementUOM as SampleUOM,
  SV.PersonnelSiteId,
  SV.PersonnelUserId,
  PU.Account as PersonnelUsername,
  IFNULL(SV.IsNegative, T.NegativeControl) as NegativeControl,
  SV.Vloume as SampleVolume,
  SV.DeviationNote as SampleDeviationNote,
  PU.Account as SamplePerformedUsername,
  CWS.UserId,
  U.Account as Username,
  CWS.GroupId,
  G.NAME AS GroupName,
  CWS.PlanId,
  P.PlanGroupId,
  P.Name AS PlanName,
  P.Barcode as PlanBarcode,
  P.Description AS PlanDescription,
  P.Version AS PlanVersion,
  P.Enabled AS PlanEnabled,
  P.EffectiveDate AS PlanEffectiveDate,
  P.IneffectiveDate AS PlanIneffectiveDate,
  P.CompletedDate AS PlanCompletedDate,
  P.Status AS PlanStatus,
  P.ApprovedDate AS PlanApproveDdate,
  P.Approved AS PlanApproved,
  CWS.ScheduledDate,
  CWS.IneffectiveDate,
  CWS.ReplicateNumber,
  CWS.Status,
  CWS.Scheduled,
  CWS.Completed,
  CWS.CompletedDate,
  CWS.ReplicateReason,
  CWS.WorkflowCompleted,
  CWS.WorkflowCompletedDate,
  CWS.LastEsigUserName,
  CWS.NoTest,
  CWS.TestId,
  T.Name AS TestName,
  T.Description AS TestDescription,
  Env.Name as TestEnvironment,
  T.TestTypeId,
  T.TestFrequencyId,
  T.TestFrequencyTypeId,
  TT.Description AS TestTypeDescription,
  TT.TestCategoryId,
  TG.Description as TestCategoryDescription,
  TC.Description as TestClassDescription,
  CWS.TestStageId,
  TS.Description AS TestStageDescription,
  CWS.TimeFrameId,
  TF.Sequence as TimeFrameSequence,
  TF.Description AS TimeFrameDescription,
  TF.VerifyPrevious,
  TF.MaxCalAlignment,
  CWS.ParentCurrentWorkSpaceId,
  CWS.BatchId,
  IFNULL(B.ProductName, B1.ProductName) as ProductName,
  IFNULL(B.BatchLotNumber, B1.BatchLotNumber) as BatchLotNumber,
  (IIF(TMIN.Sequence = TF.Sequence, true, false)) as IsSamplingStage,
  (IIF(TMIN.Sequence = TF.Sequence AND IFNULL(CWS.ScheduledDate, date('now')) < date('now') AND Completed = 0, true, false)) as IsSampleLate,
  (IIF(EXISTS (SELECT 1 FROM `LimitExceeded` le WHERE le.SampleId = CWS.SampleId and le.IsActive = 1), true, false)) as /**/HasExceededLimits,
  CWS.CreateBy,
  CWS.CreatedTime,
  CWS.UpdateBy,
  CWS.UpdatedTime,
  CWS.IsActive
FROM `CurrentWorkSpace` CWS
    INNER JOIN `Test` T ON CWS.TestId = T.Id
    INNER JOIN `EnvironmentDef` Env ON T.EnvironmentId = Env.Id
    INNER JOIN `Site` S ON S.Id = T.SiteId
    INNER JOIN `TestType` TT ON TT.Id = T.TestTypeId
    INNER JOIN `TimeFrame` TF ON CWS.TimeFrameId = TF.Id
    INNER JOIN `TestStage` TS ON TF.TestStageId = TS.Id
    LEFT OUTER JOIN `Sample` SV ON CWS.SampleId = SV.Id
    LEFT OUTER JOIN `User` PU ON PU.Id = SV.PerformedUserId
    INNER JOIN `Plan` P ON CWS.PlanId = P.Id
    LEFT OUTER JOIN `User` U ON CWS.UserId = U.Id
    LEFT OUTER JOIN `Group` G ON CWS.GroupId = G.Id
    LEFT OUTER JOIN `TestCategory` TG ON TG.Id = TT.TestCategoryId
    LEFT OUTER JOIN `TestClass` TC ON TC.Id = TG.TestClassId
    INNER JOIN `LocationV` l ON l.LocationId = S.LocationId
    INNER JOIN
      (
        SELECT
        TestTypeId,
        MIN(Sequence) AS Sequence
        FROM
        `TimeFrame` WHERE IsActive = 1 GROUP BY TestTypeId
      ) TMIN
      ON
      TF.TestTypeId = TMIN.TestTypeId
    LEFT JOIN
    (
        SELECT SP1.SampleId, MIN(SP1.BatchId) as BatchId
        FROM `WorkSpaceProduct` SP1 where exists (select 1 from WorkSpaceCache where SampleId = SP1.SampleId) and SP1.IsActive = 1
        GROUP BY SP1.SampleId
    ) SP ON SP.SampleId = CWS.SampleId
    LEFT JOIN
    (
      SELECT
      B.Id as BatchId,
      P.Name as ProductName,
      B.BatchLotNumber FROM `Batch` B INNER JOIN
         `Product` P ON B.ProductId = P.Id
         WHERE B.IsActive = 1 and P.IsActive = 1
    ) B ON B.BatchId = SP.BatchId
    LEFT JOIN
    (
      SELECT
      B.Id as BatchId,
      P.Name as ProductName,
      B.BatchLotNumber FROM `Batch` B INNER JOIN
         `Product` P ON B.ProductId = P.Id
         WHERE B.IsActive = 1 and P.IsActive = 1
    ) B1 ON B1.BatchId = CWS.BatchId
  WHERE (CWS.IsActive = 1) AND (CWS.IsActive = 1);
");
            downBuilder.Sql(@"
DROP VIEW IF EXISTS GeneralPoolV;
");
        }
    }
}
