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
        void AddMediaView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS MediaV;
CREATE VIEW MediaV as
SELECT     
	M.Id as MediaId, 
	M.Name, 
	M.MediaTypeId,
	MT.Name AS MediaTypeName,
	MT.Description AS MediaTypeDescription,
	L.Name as LocationName,
	M.LocationId,
    L.Breadcrumb as LocationBreadcrumb,
	M.Vendor, 
	M.Barcode,
	M.LotNumber, 
	M.LotNumberExpDate, 
	M.ManufacturerLotNumber,
	M.GrowthPromotionStatusId,
	G.Name AS GrowthPromotionStatusName, 
	M.GrowthPromotionResult, 
	M.Inventory,
	M.InventoryAdj,
	M.Notified,
	MT.InventoryControl,
	MT.NotificationPercentage,
	M.CreateBy,
	M.CreatedTime,
	M.UpdateBy,
	M.UpdatedTime,
	M.IsActive
	FROM  `Media` M LEFT OUTER JOIN MediaType MT ON M.MediaTypeId = MT.Id
		LEFT OUTER JOIN `LocationV` L ON M.LocationId = L.LocationId
		LEFT OUTER JOIN `GrowthPromotionStatus` G ON G.Id = M.GrowthPromotionStatusId;
");
            downBuilder.Sql(@"
DROP VIEW IF EXISTS MediaV;
");
        }
    }
}
