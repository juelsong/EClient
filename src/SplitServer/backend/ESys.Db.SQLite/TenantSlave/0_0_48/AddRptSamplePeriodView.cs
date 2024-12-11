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
        void AddRptSamplePeriodView(MigrationBuilder upBuilder, MigrationBuilder downBuilder)
        {
            upBuilder.Sql(@"
DROP VIEW IF EXISTS RptSamplePeriodV;
CREATE VIEW RptSamplePeriodV (`PeriodType`, `SampleYear`, `SampleYearText`, `SamplePeriod`) AS
SELECT
	'QUARTERLY',
	CAST ( strftime ( '%Y', s.StartDate ) AS INT ) AS `SampleYear`,
	strftime ( '%Y', s.StartDate ) AS `SampleYearText`,
	round( strftime ( '%m', StartDate ) / 3.0 + 0.495 ) AS `SamplePeriod` 
FROM
	Sample s 
WHERE
	s.StartDate IS NOT NULL 
	AND s.NoTest <> 1 
	AND s.IsNegative <> 1 
	AND s.IsActive = 1 
GROUP BY
	`SampleYear`,
	`SamplePeriod` UNION
SELECT
	'MONTHLY',
	CAST ( strftime ( '%Y', s.StartDate ) AS INT ) AS `SampleYear`,
	strftime ( '%Y', s.StartDate ) AS ` SampleYearText `,
	CAST ( strftime ( '%m', s.StartDate ) AS INT ) AS `SamplePeriod` 
FROM
	Sample s 
WHERE
	s.StartDate IS NOT NULL 
	AND s.NoTest <> 1 
	AND s.IsNegative <> 1 
	AND s.IsActive = 1 
GROUP BY
	`SampleYear`,
	`SamplePeriod`;
");

            downBuilder.Sql(@"
DROP VIEW IF EXISTS RptSamplePeriodV;
");
        }
    }
}
