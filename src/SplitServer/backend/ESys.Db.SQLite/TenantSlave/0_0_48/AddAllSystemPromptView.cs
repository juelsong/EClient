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
        void AddAllSystemPromptV(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"DROP VIEW IF EXISTS AllSystemPromptV;
CREATE VIEW AllSystemPromptV AS
SELECT (IIF(L.ParentId IS NULL, L.LocationId, L.ParentId))        AS LocationId,
       (IIF(L.ParentId IS NULL, L.LocationPath, LP.LocationPath)) AS LocationName,
       (IIF(L.ParentId IS NULL, L.LocationTypeId, LP.LocationTypeId))
                                                                 AS LocationTypeId,
       LP.ParentId,
       L.LocationId                                              AS SystemId,
       L.LocationTypeId                                          AS SystemTypeId,
       LT.Name                                                   AS SystemTypeDesc,
       L.ClassificationId                                        AS SystemClassificationId,
       CL.Name                                                   AS SystemClassificationDesc,
       L.Name                                                    AS SystemName,
       L.Description                                             AS SystemDescription,
       L.Barcode                                                 AS SystemBarcode,
       S.Id                                                      AS SiteId,
       S.SiteTypeId,
       ST.Name                                                   AS SiteTypeDesc,
       S.ClassificationId                                        AS SiteClassificationId,
       CS.Name                                                   AS SiteClassificationDesc,
       S.Barcode                                                 AS SiteBarcode,
       S.Name                                                    As SiteName,
       S.Description                                             AS SiteDescription
FROM LocationV L
         INNER JOIN
     Site S ON L.LocationId = S.LocationId
         INNER JOIN
     Classification CL ON L.ClassificationId = CL.Id
         INNER JOIN
     LocationType LT ON L.LocationTypeId = LT.Id
         INNER JOIN
     Classification CS ON S.ClassificationId = CS.Id
         INNER JOIN
     SiteType ST ON S.SiteTypeId = ST.Id
         LEFT OUTER JOIN
     LocationV LP ON L.ParentId = LP.LocationId;
");


            downBuilder.Sql(@"
DROP VIEW IF EXISTS AllSystemPromptV;
");
        }
    }
}
