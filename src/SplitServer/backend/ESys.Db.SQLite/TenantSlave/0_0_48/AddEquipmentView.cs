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
        void AddEquipmentView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS EquipmentV;
CREATE VIEW EquipmentV as
SELECT 
  E.Id as EquipmentId, 
  E.Name,
  E.Description,
  L.Name as LocationName,
  E.LocationId,
  L.Breadcrumb as LocationBreadcrumb,
  E.EquipmentTypeId,
  ET.Description AS EquipmentTypeDescription,
  E.CalibrationDate,
  E.CalibrationValue,
  M.Description as UOM,
  E.NextCalibrationDate,
  E.Barcode,
  E.ControlNumber,
  U.Account AS PerformedBy,
  E.PerformedBy as PerformedByUserId,
  E.CreateBy,
  E.CreatedTime,
  E.UpdateBy,
  E.UpdatedTime,
  E.IsActive
FROM `Equipment` E LEFT OUTER JOIN EquipmentType ET ON E.EquipmentTypeId = ET.Id
    LEFT OUTER JOIN `LocationV` L ON E.LocationId = L.LocationId
    LEFT OUTER JOIN `User` U ON E.PerformedBy = U.Id
    INNER JOIN `Measurement` M ON E.UnitOfMeasureId = M.Id;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS EquipmentV;
");
        }
    }
}
