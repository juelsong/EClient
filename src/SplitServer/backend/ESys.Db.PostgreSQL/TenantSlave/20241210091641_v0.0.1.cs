using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ESys.Db.PostgreSQL.TenantSlave
{
    public partial class v001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Property = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigItemAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Property = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigItemAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    LoadingMode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESignConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    SignCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESignConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESignConfigAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    SignCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESignConfigAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    LocationTypeId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Weight = table.Column<long>(type: "bigint", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationTypeAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<long>(type: "bigint", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypeAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    MapCategoryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapCategoryAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapCategoryAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EnName = table.Column<string>(type: "text", nullable: true),
                    ZhName = table.Column<string>(type: "text", nullable: true),
                    EnDescription = table.Column<string>(type: "text", nullable: true),
                    ZhDescription = table.Column<string>(type: "text", nullable: true),
                    ProcessorType = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DepartFormatter = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Permission_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: true),
                    ChainOfCustody = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    SiteTypeId = table.Column<int>(type: "integer", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteTypeAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTypeAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Account = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    EMail = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LastMonitoredDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    InitialQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NextQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PasswordExpiryPeriod = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastPasswordModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisioDiagramAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioDiagramAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisioLocationAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioLocationAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisioSiteAudit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EntityId = table.Column<int>(type: "integer", nullable: false),
                    AuditTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Action = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioSiteAudit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SerialNumber = table.Column<string>(type: "text", nullable: true),
                    LastSyncDataTimestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CalibrationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CalibrationValue = table.Column<decimal>(type: "numeric(20,6)", precision: 20, scale: 6, nullable: true),
                    NextCalibrationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Barcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ControlNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    PerformedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EquipmentTypeId = table.Column<int>(type: "integer", nullable: true),
                    UnitOfMeasureId = table.Column<int>(type: "integer", nullable: true),
                    LastSyncUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: true),
                    ChainOfCustody = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    ManagerId = table.Column<int>(type: "integer", nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Department_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ElectronicSignature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Account = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SignDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsSystemOperation = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicSignature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectronicSignatureItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: true),
                    PrimaryKey = table.Column<long>(type: "bigint", nullable: true),
                    ElectronicSignatureId = table.Column<long>(type: "bigint", nullable: false),
                    LastAuditKey = table.Column<long>(type: "bigint", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronicSignatureItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectronicSignatureItem_ElectronicSignature_ElectronicSigna~",
                        column: x => x.ElectronicSignatureId,
                        principalTable: "ElectronicSignature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    IsHtmlBody = table.Column<bool>(type: "boolean", nullable: false),
                    SendDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMailAttachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    ContentId = table.Column<string>(type: "text", nullable: true),
                    UsedInBody = table.Column<bool>(type: "boolean", nullable: false),
                    EMailId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMailAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EMailAttachment_EMail_EMailId",
                        column: x => x.EMailId,
                        principalTable: "EMail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Barcode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    LocationTypeId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Location_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_LocationType_LocationTypeId",
                        column: x => x.LocationTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocationExtra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Breadcrumb = table.Column<string>(type: "text", nullable: true),
                    LocationPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationExtra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationExtra_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Barcode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    SiteTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Site_SiteType_SiteTypeId",
                        column: x => x.SiteTypeId,
                        principalTable: "SiteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Account = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    RealName = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    EMail = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LastMonitoredDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    InitialQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NextQualificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    PasswordExpiryPeriod = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastPasswordModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    NotificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NotificationTypeId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    SiteId = table.Column<int>(type: "integer", nullable: true),
                    EquipmentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    NotificationTypeId = table.Column<int>(type: "integer", nullable: false),
                    PlanGroupId = table.Column<int>(type: "integer", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Subscription_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Logined = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPasswordHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DashboardConfig = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Locale = table.Column<string>(type: "text", nullable: true),
                    UserSettings = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationEMail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NotificationId = table.Column<long>(type: "bigint", nullable: false),
                    EMailId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationEMail_EMail_EMailId",
                        column: x => x.EMailId,
                        principalTable: "EMail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationEMail_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Map",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MapCategoryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Map", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MapCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapCategory_Map_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Map",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisioDiagram",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MapId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioDiagram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisioDiagram_Map_MapId",
                        column: x => x.MapId,
                        principalTable: "Map",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VisioLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    Width = table.Column<double>(type: "double precision", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisioLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisioLocation_VisioDiagram_VisioDiagramId",
                        column: x => x.VisioDiagramId,
                        principalTable: "VisioDiagram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisioSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteId = table.Column<int>(type: "integer", nullable: false),
                    VisioDiagramId = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreateBy = table.Column<int>(type: "integer", nullable: false),
                    UpdateBy = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisioSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisioSite_Site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisioSite_VisioDiagram_VisioDiagramId",
                        column: x => x.VisioDiagramId,
                        principalTable: "VisioDiagram",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConfigItem",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Property", "UpdateBy", "UpdatedTime", "Value" },
                values: new object[] { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "LOCATION_TYPE_WEIGHT_LEVEL_COUNT", null, null, "8" });

            migrationBuilder.InsertData(
                table: "ESignConfig",
                columns: new[] { "Id", "Category", "CreateBy", "CreatedTime", "IsActive", "Permissions", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "plan", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Add,Plan:Edit", null, null },
                    { 2, "addPlan", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Add", null, null },
                    { 3, "editPlan", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Edit", null, null },
                    { 4, "planReview", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Review", null, null },
                    { 5, "planApprove", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Approve", null, null },
                    { 6, "planEffective", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Effective", null, null },
                    { 7, "planRetire", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:Retire", null, null },
                    { 8, "planAddToPool", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Plan:AddToPool", null, null },
                    { 9, "receiveTask", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Missions:Receive", null, null },
                    { 10, "assignTask", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MissionManagement:Assign", null, null },
                    { 11, "executeTask", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "Missions:Execute", null, null },
                    { 12, "returnTask", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MissionManagement:Return,MyMissions:Return", null, null },
                    { 13, "eSignTasks", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MyMissions:Execute,SampleCollection:Execute,ExperimentalOperations:Execute,Incubation:Execute,InspectionRecord:Execute", null, null },
                    { 14, "noTest", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MissionManagement:NoTest,MyMissions:NoTest,SampleCollection:NoTest,ExperimentalOperations:NoTest,Incubation:NoTest,InspectionRecord:NoTest", null, null },
                    { 15, "reTest", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MissionManagement:Resume", null, null },
                    { 16, "auditReview", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "SampleAudit:Review", null, null },
                    { 17, "auditApprove", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "SamplePrompt:Approval", null, null },
                    { 18, "auditReApprove", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "SampleCompleted:ReApprove", null, null },
                    { 19, "auditEdit", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), true, "MissionManagement:Edit,SampleAudit:Edit", null, null }
                });

            migrationBuilder.InsertData(
                table: "EquipmentType",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Description", "IsActive", "LoadingMode", "Model", "Path", "Type", "UpdateBy", "UpdatedTime", "Version" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "No equipment type", true, null, null, null, null, null, null, 0 },
                    { 2, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Incubator", true, null, null, null, null, null, null, 0 },
                    { 3, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Active Air Sampler", true, null, null, null, null, null, null, 0 },
                    { 4, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Particulate Counter", true, null, null, null, null, null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Description", "EnDescription", "EnName", "IsActive", "Name", "ProcessorType", "Type", "UpdateBy", "UpdatedTime", "ZhDescription", "ZhName" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Excursion", "Excursion", "Excursion", true, "Excursion", null, 1, null, null, "触发限度时发送通知", "超限" },
                    { 2, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Login Failure", "Login Failure", "Login Failure", true, "Login Failure", null, 2, null, null, "登录失败", "登录失败" },
                    { 3, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Invalid electronic signature was attempted.", "Invalid electronic signature was attempted.", "Invalid ESig", true, "Invalid ESig", null, 3, null, null, "无效电子签名", "无效电子签名" },
                    { 4, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "A user account was locked.", "A user account was locked.", "Account Locked", true, "Account Locked", null, 4, null, null, "账户锁定", "账户锁定" },
                    { 5, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Work items in their first stage that have been scheduled but are not yet assigned.", "Work items in their first stage that have been scheduled but are not yet assigned.", "Scheduled Samples Not Assigned", true, "Scheduled Samples Not Assigned", null, 5, null, null, "任务项目已经添加进检验池如未被分配和领取，则在每日下午3点发送通知", "任务未启动" },
                    { 6, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "The weekly test has not been completed.", "The weekly test has not been completed.", "Weekly Incomplete", true, "Weekly Incomplete", null, 6, null, null, "测试频率为每周的检验项目如果在周五还未完成，将会在下午3点发送通知", "周任务未完成" },
                    { 7, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "The monthly test has not been completed.", "The monthly test has not been completed.", "Monthly Incomplete", true, "Monthly Incomplete", null, 7, null, null, "测试频率为每月的检验项目如果在当月25号还未完成，将会在下午3点发送通知", "月任务未完成" },
                    { 8, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "The quarterly test has not been completed.", null, null, false, "Quarterly Incomplete", null, 8, null, null, null, null },
                    { 9, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Work items with a configured max time for a stage that are due within the next hour.", null, null, false, "Max Time About to Exceed", null, 9, null, null, null, null },
                    { 10, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Work items that have been assigned and scheduled for today but have not yet been completed.", "Work items that have been assigned and scheduled for today but have not yet been completed.", "Work Incomplete ", true, "Work Incomplete ", null, 10, null, null, "计划检测日期为当天的任务，如已经启动但未完成的，在当天下午3点发送通知", "当日任务未完成" },
                    { 11, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Organism Found", null, null, false, "Organism Found", null, 11, null, null, null, null },
                    { 12, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Organism Added To Organism Dictionary", null, null, false, "Organism Added To Organism Dictionary", null, 12, null, null, null, null },
                    { 13, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "An error occurred processing a sample", null, null, false, "Workflow Error", null, 13, null, null, null, null },
                    { 14, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Equipment that is about to go out of calibration.", null, null, false, "Equipment About to Expire", null, 14, null, null, null, null },
                    { 15, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "Media inventory is getting low.", null, null, false, "Media Inventory is Low", null, 15, null, null, null, null },
                    { 16, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "User Qualification has lapsed.", null, null, false, "User Qualification Lapsed", null, 16, null, null, null, null },
                    { 17, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), "User Qualification is coming due.", null, null, false, "User Qualification Coming Due", null, 17, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 1, "system", null, "系统管理", 1, null, 1 },
                    { 58, "visualization", null, "可视化", 58, null, 1 },
                    { 67, "inspectionExecution", null, "检验执行", 67, null, 1 },
                    { 84, "auditPrompt", null, "审核批准", 84, null, 1 },
                    { 90, "plan", null, "检验计划", 90, null, 1 },
                    { 98, "analyse", null, "分析报表", 98, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "Description", "IsActive", "IsHidden", "Name", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, true, true, "超级管理员", null, null },
                    { 2, 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, true, false, "管理员", null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "CreateBy", "CreatedTime", "DepartmentId", "EMail", "EmployeeId", "Gender", "InitialQualificationDate", "IsActive", "IsHidden", "LastMonitoredDate", "LastPasswordModified", "LocationId", "NextQualificationDate", "Password", "PasswordExpiryPeriod", "Phone", "RealName", "Salt", "Title", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, "super", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, true, null, null, null, null, "l8YyUDcfKuGGgLaMKT4E6mg/8ClZOK8tczolqvakrA8=", null, null, null, "f5ZTUveTZ6szf7wR3qCmvg==", null, null, null },
                    { 2, "ESys_Admin", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, true, null, null, null, null, "gIbsDs1b73xjUyptJmm1RjjX8HiJ3ubnt1F/mS6mzio=", null, null, null, "kCXBD/crXrivLJwcfWEoHQ==", null, null, null },
                    { 3, "Admin", 1, new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)), null, null, null, null, null, true, false, null, null, null, null, "QnVVjchwQROIbFPB7PrnYD3htnV5F4AJrfWnsej4JQk=", null, null, "Admin", "tJ48HTFvFBHIWRnO8pxLpQ==", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 2, "region", null, "区域", 2, 1, 1 },
                    { 15, "production", null, "产品", 15, 1, 1 },
                    { 19, "testMethod", null, "测试方法", 19, 1, 1 },
                    { 23, "device", null, "设备", 23, 1, 1 },
                    { 28, "medium", null, "培养基", 28, 1, 1 },
                    { 32, "microorganism", null, "微生物", 32, 1, 1 },
                    { 36, "security", null, "安全", 36, 1, 1 },
                    { 53, "settings", null, "系统设置", 53, 1, 1 },
                    { 56, "auditRecord", null, "审计追踪", 56, 1, 1 },
                    { 57, "log", null, "日志", 57, 1, 1 },
                    { 59, "map", null, "地图管理", 59, 58, 1 },
                    { 66, "visualizations", null, "可视化呈现", 66, 58, 1 },
                    { 68, "missions", null, "任务管理", 68, 67, 1 },
                    { 77, "inspectionRecord", null, "结果录入", 77, 67, 1 },
                    { 85, "AuditPrompt:ReTest", null, "审核批准再测试", 85, 84, 2 },
                    { 86, "AuditPrompt:Notest", null, "审核批准无需测试", 86, 84, 1 },
                    { 87, "AuditPrompt:Approve", null, "批准", 87, 84, 2 },
                    { 88, "AuditPrompt:Review", null, "审核", 88, 84, 2 },
                    { 89, "AuditPrompt:Edit", null, "复核修改数据", 89, 84, 2 },
                    { 91, "Plan:Add", null, "计划添加", 91, 90, 2 },
                    { 92, "Plan:Edit", null, "计划编辑", 92, 90, 2 },
                    { 93, "Plan:Approve", null, "计划批准", 93, 90, 2 },
                    { 94, "Plan:Effective", null, "计划激活", 94, 90, 2 },
                    { 95, "Plan:Retire", null, "计划废弃", 95, 90, 2 },
                    { 96, "Plan:AddToPool", null, "计划添加到任务列表", 96, 90, 2 },
                    { 97, "Plan:Calender", null, "计划日历", 97, 90, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[] { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, 2, null, null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "RoleId", "UpdateBy", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, null, null, 1 },
                    { 2, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 3, "Classification:Add", null, "添加洁净级别", 3, 2, 2 },
                    { 4, "Classification:Edit", null, "编辑洁净级别", 4, 2, 2 },
                    { 5, "Classification:Disable", null, "禁用洁净级别", 5, 2, 2 },
                    { 6, "SiteType:Add", null, "添加采样点类型", 6, 2, 2 },
                    { 7, "SiteType:Edit", null, "编辑采样点类型", 7, 2, 2 },
                    { 8, "SiteType:Disable", null, "禁用采样点类型", 8, 2, 2 },
                    { 9, "LocationType:Add", null, "添加区域类型", 9, 2, 2 },
                    { 10, "LocationType:Edit", null, "编辑区域类型", 10, 2, 2 },
                    { 11, "LocationType:Disable", null, "禁用区域类型", 11, 2, 2 },
                    { 12, "Location:Add", null, "添加区域", 12, 2, 2 },
                    { 13, "Location:Edit", null, "编辑区域", 13, 2, 2 },
                    { 14, "Location:Disable", null, "禁用区域", 14, 2, 2 },
                    { 16, "Product:Add", null, "添加产品", 16, 15, 2 },
                    { 17, "Product:Edit", null, "编辑产品", 17, 15, 2 },
                    { 18, "Product:Disable", null, "禁用产品", 18, 15, 2 },
                    { 20, "TestMethod:Add", null, "添加测试方法", 20, 19, 2 },
                    { 21, "TestMethod:Edit", null, "编辑测试方法", 21, 19, 2 },
                    { 22, "TestMethod:Disable", null, "禁用测试方法", 22, 19, 2 },
                    { 24, "Equipment:Add", null, "添加设备", 24, 23, 2 },
                    { 25, "Equipment:Edit", null, "编辑设备", 25, 23, 2 },
                    { 26, "Equipment:Disable", null, "禁用设备", 26, 23, 2 },
                    { 27, "Equipment:UpdateConfig", null, "上传文件", 27, 23, 2 },
                    { 29, "Media:Add", null, "添加培养基", 29, 28, 2 },
                    { 30, "Media:Edit", null, "编辑培养基", 30, 28, 2 },
                    { 31, "Media:Disable", null, "禁用培养基", 31, 28, 2 },
                    { 33, "Organism:Add", null, "添加微生物", 33, 32, 2 },
                    { 34, "Organism:Edit", null, "编辑微生物", 34, 32, 2 },
                    { 35, "Organism:Disable", null, "禁用微生物", 35, 32, 2 },
                    { 37, "department", null, "部门管理", 37, 36, 1 },
                    { 41, "user", null, "用户管理", 41, 36, 1 },
                    { 46, "role", null, "角色管理", 46, 36, 1 },
                    { 50, "booking", null, "预订管理", 50, 36, 1 },
                    { 54, "Security:Password", null, "配置密码", 54, 53, 1 },
                    { 55, "Security:Email", null, "配置邮箱", 55, 53, 1 },
                    { 60, "Map:Add", null, "添加地图", 60, 59, 2 },
                    { 61, "Map:Disable", null, "禁用地图", 61, 59, 2 },
                    { 62, "Map:Edit", null, "编辑地图", 62, 59, 2 },
                    { 63, "MapCategory:Add", null, "添加地图分类", 63, 59, 2 },
                    { 64, "MapCategory:Disable", null, "禁用地图分类", 64, 59, 2 },
                    { 65, "MapCategory:Edit", null, "编辑地图分类", 65, 59, 2 },
                    { 69, "Missions:Receive", null, "任务管理领取", 69, 68, 2 },
                    { 70, "Missions:Assign", null, "任务管理分配", 70, 68, 2 },
                    { 71, "Missions:Copy", null, "任务管理复制", 71, 68, 2 },
                    { 72, "Missions:Return", null, "任务管理退回", 72, 68, 2 },
                    { 73, "Missions:Execute", null, "任务管理执行", 73, 68, 2 },
                    { 74, "Missions:NoTest", null, "任务管理无需测试", 74, 68, 2 },
                    { 75, "Missions:Calender", null, "任务日历", 75, 68, 1 },
                    { 76, "Missions:Printer", null, "任务条码打印", 76, 68, 2 },
                    { 78, "InspectionRecord:Sampling", null, "采样", 78, 77, 1 },
                    { 79, "InspectionRecord:Incubation", null, "孵化", 79, 77, 1 },
                    { 80, "InspectionRecord:Testing", null, "测试", 80, 77, 1 },
                    { 81, "InspectionRecord:ResultEntry", null, "录入", 81, 77, 1 },
                    { 82, "InspectionRecord:EditDeviceImport", null, "编辑设备导入数据", 82, 77, 1 },
                    { 83, "InspectionRecord:Notest", null, "结果录入无需测试", 83, 77, 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[] { 2, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, 2, null, null });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Code", "DepartFormatter", "Description", "Order", "ParentId", "Type" },
                values: new object[,]
                {
                    { 38, "Department:Add", null, "添加部门", 38, 37, 2 },
                    { 39, "Department:Edit", null, "编辑部门", 39, 37, 2 },
                    { 40, "Department:Disable", null, "禁用部门", 40, 37, 2 },
                    { 42, "User:Add", null, "添加用户", 42, 41, 2 },
                    { 43, "User:Edit", null, "编辑用户", 43, 41, 2 },
                    { 44, "User:Disable", null, "禁用用户", 44, 41, 2 },
                    { 45, "User:Password", null, "修改密码", 45, 41, 2 },
                    { 47, "Role:Add", null, "添加角色", 47, 46, 2 },
                    { 48, "Role:Edit", null, "编辑角色", 48, 46, 2 },
                    { 49, "Role:Disable", null, "禁用角色", 49, 46, 2 },
                    { 51, "Subscription:Edit", null, "编辑警告订阅", 51, 50, 2 },
                    { 52, "Subscription:Disable", null, "禁用警告订阅", 52, 50, 2 }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "Id", "CreateBy", "CreatedTime", "PermissionId", "RoleId", "UpdateBy", "UpdatedTime" },
                values: new object[,]
                {
                    { 3, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3, 2, null, null },
                    { 4, 0, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4, 2, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigItemAudit_EntityId",
                table: "ConfigItemAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ManagerId",
                table: "Department",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                table: "Department",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAudit_EntityId",
                table: "DepartmentAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicSignature_UserId",
                table: "ElectronicSignature",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicSignatureItem_ElectronicSignatureId",
                table: "ElectronicSignatureItem",
                column: "ElectronicSignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronicSignatureItem_TableName_PrimaryKey",
                table: "ElectronicSignatureItem",
                columns: new[] { "TableName", "PrimaryKey" });

            migrationBuilder.CreateIndex(
                name: "IX_EMail_UserId",
                table: "EMail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EMailAttachment_EMailId",
                table: "EMailAttachment",
                column: "EMailId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Name",
                table: "Equipment",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SerialNumber",
                table: "Equipment",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentType_Description",
                table: "EquipmentType",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_ESignConfig_Category",
                table: "ESignConfig",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_ESignConfig_Category_IsActive",
                table: "ESignConfig",
                columns: new[] { "Category", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_ESignConfigAudit_EntityId",
                table: "ESignConfigAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Id_ParentId",
                table: "Location",
                columns: new[] { "Id", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationTypeId",
                table: "Location",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParentId",
                table: "Location",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_VisioDiagramId",
                table: "Location",
                column: "VisioDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationAudit_EntityId",
                table: "LocationAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationExtra_LocationId",
                table: "LocationExtra",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_Name",
                table: "LocationType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LocationType_Name_Description_IsActive",
                table: "LocationType",
                columns: new[] { "Name", "Description", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_LocationTypeAudit_EntityId",
                table: "LocationTypeAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Map_MapCategoryId",
                table: "Map",
                column: "MapCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MapAudit_EntityId",
                table: "MapAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MapCategory_ParentId",
                table: "MapCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MapCategoryAudit_EntityId",
                table: "MapCategoryAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EquipmentId",
                table: "Notification",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SiteId",
                table: "Notification",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationEMail_EMailId",
                table: "NotificationEMail",
                column: "EMailId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationEMail_NotificationId",
                table: "NotificationEMail",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Code",
                table: "Permission",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ParentId",
                table: "Permission",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAudit_EntityId",
                table: "ProductAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_Name",
                table: "ProductType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeAudit_EntityId",
                table: "ProductTypeAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAudit_EntityId",
                table: "RoleAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionAudit_EntityId",
                table: "RolePermissionAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_IsActive_LocationId_Id",
                table: "Site",
                columns: new[] { "IsActive", "LocationId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Site_LocationId",
                table: "Site",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_LocationId_Id_Name",
                table: "Site",
                columns: new[] { "LocationId", "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Site_SiteTypeId",
                table: "Site",
                column: "SiteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAudit_EntityId",
                table: "SiteAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteType_Name",
                table: "SiteType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SiteTypeAudit_EntityId",
                table: "SiteTypeAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_LocationId",
                table: "Subscription",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_NotificationTypeId",
                table: "Subscription",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Account",
                table: "User",
                column: "Account",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LocationId",
                table: "User",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAudit_EntityId",
                table: "UserAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistory_UserId",
                table: "UserHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPasswordHistory_UserId",
                table: "UserPasswordHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAudit_EntityId",
                table: "UserRoleAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioDiagram_MapId",
                table: "VisioDiagram",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioDiagramAudit_EntityId",
                table: "VisioDiagramAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioLocation_LocationId",
                table: "VisioLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioLocation_VisioDiagramId",
                table: "VisioLocation",
                column: "VisioDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioLocationAudit_EntityId",
                table: "VisioLocationAudit",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioSite_SiteId",
                table: "VisioSite",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioSite_VisioDiagramId",
                table: "VisioSite",
                column: "VisioDiagramId");

            migrationBuilder.CreateIndex(
                name: "IX_VisioSiteAudit_EntityId",
                table: "VisioSiteAudit",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_ManagerId",
                table: "Department",
                column: "ManagerId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectronicSignature_User_UserId",
                table: "ElectronicSignature",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EMail_User_UserId",
                table: "EMail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_VisioDiagram_VisioDiagramId",
                table: "Location",
                column: "VisioDiagramId",
                principalTable: "VisioDiagram",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Map_MapCategory_MapCategoryId",
                table: "Map",
                column: "MapCategoryId",
                principalTable: "MapCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_ManagerId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Map_MapCategory_MapCategoryId",
                table: "Map");

            migrationBuilder.DropTable(
                name: "ConfigItem");

            migrationBuilder.DropTable(
                name: "ConfigItemAudit");

            migrationBuilder.DropTable(
                name: "DepartmentAudit");

            migrationBuilder.DropTable(
                name: "ElectronicSignatureItem");

            migrationBuilder.DropTable(
                name: "EMailAttachment");

            migrationBuilder.DropTable(
                name: "ESignConfig");

            migrationBuilder.DropTable(
                name: "ESignConfigAudit");

            migrationBuilder.DropTable(
                name: "LocationAudit");

            migrationBuilder.DropTable(
                name: "LocationExtra");

            migrationBuilder.DropTable(
                name: "LocationTypeAudit");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "MapAudit");

            migrationBuilder.DropTable(
                name: "MapCategoryAudit");

            migrationBuilder.DropTable(
                name: "NotificationEMail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductAudit");

            migrationBuilder.DropTable(
                name: "ProductTypeAudit");

            migrationBuilder.DropTable(
                name: "RoleAudit");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "RolePermissionAudit");

            migrationBuilder.DropTable(
                name: "SiteAudit");

            migrationBuilder.DropTable(
                name: "SiteTypeAudit");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserAudit");

            migrationBuilder.DropTable(
                name: "UserHistory");

            migrationBuilder.DropTable(
                name: "UserPasswordHistory");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserRoleAudit");

            migrationBuilder.DropTable(
                name: "VisioDiagramAudit");

            migrationBuilder.DropTable(
                name: "VisioLocation");

            migrationBuilder.DropTable(
                name: "VisioLocationAudit");

            migrationBuilder.DropTable(
                name: "VisioSite");

            migrationBuilder.DropTable(
                name: "VisioSiteAudit");

            migrationBuilder.DropTable(
                name: "ElectronicSignature");

            migrationBuilder.DropTable(
                name: "EMail");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "EquipmentType");

            migrationBuilder.DropTable(
                name: "SiteType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LocationType");

            migrationBuilder.DropTable(
                name: "VisioDiagram");

            migrationBuilder.DropTable(
                name: "MapCategory");

            migrationBuilder.DropTable(
                name: "Map");
        }
    }
}
