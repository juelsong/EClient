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
        void AddLocationView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS LocationV;
CREATE VIEW LocationV as
  SELECT l.Id as LocationId, 
    l.Name,
    l.Description,
    l.Barcode,
    l.Code,
    l.CreateBy, 
    l.CreatedTime,
    l.UpdateBy, 
    l.UpdatedTime, 
    l.IsActive,
    l.ParentId,
    l.LocationTypeId,
    l.ClassificationId,
    l.SecondClassificationId,
    l.VisioDiagramId,
    le.Breadcrumb,
    le.LocationPath
  FROM `Location` l INNER JOIN 
    `LocationExtra` le ON le.LocationId = l.Id
");
            downBuilder.Sql(@"
DROP VIEW IF EXISTS LocationV;
");
        }
    }
}
