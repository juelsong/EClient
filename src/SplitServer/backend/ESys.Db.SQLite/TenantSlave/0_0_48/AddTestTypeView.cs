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
        void AddTestTypeView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS TestTypeV;
CREATE VIEW TestTypeV
AS
SELECT
	tt.Id AS TestTypeId,
	tt.Description,
	tt.TestCategoryId,
	tt.TestTypeCodeId,
	tc.Description AS TestCategoryDesc,
	tc.TestClassId,
	cl.Description AS TestClassDesc,
	ttc.Code,
	ttl.Code AS LabelCode,
	tt.NegativeControl,
	tt.RequireProductSelection,
	tt.ProdSelectionTimeFrameId,
	tf.Description AS TimeFrameDescription,
	l.Name AS LocationName,
	l.LocationPath AS LocationFullName,
	tt.LocationId,
	l.Breadcrumb,
	IIF (
		(
		SELECT
			COUNT( * ) 
		FROM
			TimeFrame tf 
		WHERE
			tf.TestTypeId = tt.Id 
			AND tf.IsActive = 1 
			AND EXISTS ( SELECT 1 FROM TimeFrameDilution tfd WHERE tfd.TimeFrameId = tf.Id AND tfd.TestTypeId IS NOT NULL AND tfd.IsActive = 1 ) 
		) = 0,
		false,
		true 
	) AS HasSubTestTypes,
	IIF (
		(
		SELECT
			COUNT( * ) 
		FROM
			TestType tt1
			INNER JOIN TimeFrame tf1 ON tf1.TestTypeId = tt1.Id 
		WHERE
			tf1.IsActive = 1 
			AND tt1.IsActive = 1 
			AND EXISTS (
			SELECT
				1 
			FROM
				TimeFrameDilution tfd 
			WHERE
				tfd.TimeFrameId = tf1.Id 
				AND tfd.TestTypeId = tt.Id 
				AND tfd.IsActive = 1 
			) 
		) = 0,
		false,
		true 
	) AS IsSubTestType,
	IIF (
		(
		SELECT
			COUNT( * ) 
		FROM
			TimeFrame tf 
		WHERE
			tf.TestTypeId = tt.Id 
			AND tf.IsActive = 1 
			AND EXISTS ( SELECT 1 FROM Measurement m WHERE m.TimeFrameId = tf.Id AND m.Result = 1 AND m.IsActive = 1 ) 
		) = 0,
		false,
		true 
	) AS HasResultMeasurement,
	IIF (
		(
		SELECT
			COUNT( * ) 
		FROM
			TimeFrame tf 
		WHERE
			tf.TestTypeId = tt.Id 
			AND tf.IsActive = 1 
			AND tf.ShowOrgid = 1 
		) = 0,
		false,
		true 
	) AS HasShowOrgId,
	tt.Price,
	tt.SystemRecord,
	tt.CreateBy,
	tt.CreatedTime,
	tt.UpdateBy,
	tt.UpdatedTime,
	tt.IsActive 
FROM
	TestType tt
	INNER JOIN TestCategory tc ON tt.TestCategoryId = tc.Id
	INNER JOIN TestTypeCode ttc ON tt.TestTypeCodeId = ttc.Id
	INNER JOIN TestTypeLabel ttl ON tt.TestTypeLabelId = ttl.Id
	INNER JOIN TestClass cl ON tc.TestClassId = cl.Id
	LEFT OUTER JOIN TimeFrame tf ON tt.ProdSelectionTimeFrameId = tf.Id
	LEFT OUTER JOIN LocationV l ON tt.LocationId = l.LocationId;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS TestTypeV;
");
        }
    }
}
