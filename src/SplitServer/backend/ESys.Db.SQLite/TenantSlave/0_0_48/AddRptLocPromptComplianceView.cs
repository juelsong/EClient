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
        void AddRptLocPromptComplianceView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS RptLocPromptComplianceV;
CREATE VIEW RptLocPromptComplianceV (`LocationId`, `LocationName`) AS
SELECT DISTINCT l.Id,
                l.Name
FROM AllSystemPromptV a
         INNER JOIN
     Location l ON a.ParentId = l.Id
         INNER JOIN
     LocationType lt ON l.LocationTypeId = lt.Id
UNION
SELECT DISTINCT l.Id,
                l.Name
FROM AllSystemPromptV a
         INNER JOIN
     Location l ON a.LocationId = l.Id
         INNER JOIN
     LocationType lt ON l.LocationTypeId = lt.Id
UNION
SELECT DISTINCT a.SystemId,
                a.SystemName
FROM AllSystemPromptV a
         INNER JOIN
     LocationType lt ON a.SystemTypeId = lt.Id;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS RptLocPromptComplianceV;
");
        }
    }
}