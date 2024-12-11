using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkSpaceProductAudit",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorkSpaceProduct",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AddProducts",
                table: "TimeFrameAudit",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AddProducts",
                table: "TimeFrame",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequireProduct",
                table: "TestStageAudit",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireProduct",
                table: "TestStage",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ProductAudit",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Product",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
                    UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
                    CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
                    Action = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAudit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_Name",
                table: "ProductType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAudit_EntityId",
                table: "ProductTypeAudit",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product",
                column: "ProductTypeId",
                principalTable: "ProductType",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductType_ProductTypeId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "ProductTypeAudit");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkSpaceProductAudit");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorkSpaceProduct");

            migrationBuilder.DropColumn(
                name: "AddProducts",
                table: "TimeFrameAudit");

            migrationBuilder.DropColumn(
                name: "AddProducts",
                table: "TimeFrame");

            migrationBuilder.DropColumn(
                name: "RequireProduct",
                table: "TestStageAudit");

            migrationBuilder.DropColumn(
                name: "RequireProduct",
                table: "TestStage");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ProductAudit");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Product");
        }
    }
}
