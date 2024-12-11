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
        void AddSiteView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS SiteV;
CREATE VIEW SiteV as
  SELECT s.Id as SiteId, 
    s.Name AS SiteName,
    s.Description AS SiteDesc, 
    s.SiteTypeId,
    st.Name AS SiteTypeDesc, 
    s.LocationId,
    l.Name as LocationName,
    l.Description as LocationDesc,
    l.Breadcrumb as LocationBreadcrumb,
    l.LocationPath as LocationFullName,
    s.ClassificationId,
    c.Name AS ClassificationDesc,
    s.SecondClassificationId,
    c1.Name AS SecondClassificationDesc,
    s.Barcode, 
    s.CreateBy, 
    s.CreatedTime,
    s.UpdateBy, 
    s.UpdatedTime, 
    s.IsActive
  FROM `Site` s INNER JOIN 
    `LocationV` l ON s.LocationId = l.LocationId INNER JOIN
    `SiteType` st ON s.SiteTypeId = st.Id INNER JOIN
    `Classification` c ON c.Id = s.ClassificationId LEFT OUTER JOIN
    `Classification` c1 ON c1.Id = s.SecondClassificationId;
");
            downBuilder.Sql(@"
DROP VIEW IF EXISTS SiteV;
");
        }
    }
}
