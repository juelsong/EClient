using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESys.Db.SQLite.TenantSlave
{
    public partial class v0048 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ApprovalEntity",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        EntityName = table.Column<string>(type: "TEXT", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: false),
            //        ApprovalResult = table.Column<int>(type: "INTEGER", nullable: false),
            //        Belongs = table.Column<int>(type: "INTEGER", nullable: true),
            //        Comment = table.Column<string>(type: "TEXT", nullable: true),
            //        IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApprovalEntity", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApprovalStepAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        StepId = table.Column<string>(type: "TEXT", nullable: false),
            //        UserIdStr = table.Column<string>(type: "TEXT", nullable: false),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        ExpectCount = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        ApprovalEntityId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApprovalStepAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BatchAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProductId = table.Column<int>(type: "INTEGER", nullable: false),
            //        BatchLotNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BatchAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Classification",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Classification", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ClassificationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ClassificationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CompareDataRemarkAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Remark = table.Column<string>(type: "TEXT", nullable: true),
            //        RemarkDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParticleCounterDataId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompareDataRemarkAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ConfigItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Property = table.Column<string>(type: "TEXT", nullable: true),
            //        Value = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ConfigItem", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ConfigItemAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Property = table.Column<string>(type: "TEXT", nullable: true),
            //        Value = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ConfigItemAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CurrentWorkSpaceAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SampleBarcode = table.Column<string>(type: "TEXT", nullable: true),
            //        EarlyExecutionDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduledDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        IneffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReplicateNumber = table.Column<int>(type: "INTEGER", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        Completed = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReplicateReason = table.Column<string>(type: "TEXT", nullable: true),
            //        WorkflowCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        WorkflowCompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PerformedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Scheduled = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LastEsigUserName = table.Column<string>(type: "TEXT", nullable: true),
            //        RemainingMinutes = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        RecieveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LastUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LastStepRemark = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ExecuteUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Remark = table.Column<string>(type: "TEXT", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        BatchId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestStageId = table.Column<int>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: false),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentCurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CurrentWorkSpaceAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CycleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CycleNumber = table.Column<int>(type: "INTEGER", nullable: false),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        IncludeCycleName = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CheckLimits = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CycleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DataType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DataType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DataTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DataTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DepartmentAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DepartmentAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DeviationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ReadingValue = table.Column<decimal>(type: "TEXT", nullable: false),
            //        DeviationNumber = table.Column<int>(type: "INTEGER", nullable: false),
            //        ValueDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DeviationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DeviceConfig",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        OfficialConfig = table.Column<string>(type: "TEXT", nullable: true),
            //        AdditionalConfig = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DeviceConfig", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DeviceConfigAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        OfficialConfig = table.Column<string>(type: "TEXT", nullable: true),
            //        AdditionalConfig = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DeviceConfigAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EnvironmentDef",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EnvironmentDef", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EnvironmentDefAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EnvironmentDefAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EquipmentAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        LastSyncDataTimestamp = table.Column<long>(type: "INTEGER", nullable: true),
            //        CalibrationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CalibrationValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        NextCalibrationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        ControlNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        PerformedBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DeviceConfigId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EquipmentAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EquipmentType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        Model = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        Path = table.Column<string>(type: "TEXT", nullable: true),
            //        LoadingMode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EquipmentType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EquipmentTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Type = table.Column<string>(type: "TEXT", nullable: true),
            //        Model = table.Column<string>(type: "TEXT", nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        Path = table.Column<string>(type: "TEXT", nullable: true),
            //        LoadingMode = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EquipmentTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ESignConfig",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Category = table.Column<string>(type: "TEXT", nullable: true),
            //        Permissions = table.Column<string>(type: "TEXT", nullable: true),
            //        SignCount = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ESignConfig", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ESignConfigAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Category = table.Column<string>(type: "TEXT", nullable: true),
            //        Permissions = table.Column<string>(type: "TEXT", nullable: true),
            //        SignCount = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ESignConfigAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FileAttachmentAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Path = table.Column<string>(type: "TEXT", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FileAttachmentAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Group",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Group", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GroupAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GroupAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GrowthPromotionStatus",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        StatusCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GrowthPromotionStatus", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GrowthPromotionStatusAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        StatusCode = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_GrowthPromotionStatusAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Prevalence = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        FreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SourceLimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SourceLimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        OccurrenceCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Period = table.Column<int>(type: "INTEGER", nullable: true),
            //        PeriodCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        LimitExceededId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Deviation = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EmailNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScreenNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RescheduleCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        RescheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ExecutePerCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReportableOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        ReportableValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ParentLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitDefAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Prevalence = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        FreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SourceLimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SourceLimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        OccurrenceCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Period = table.Column<int>(type: "INTEGER", nullable: true),
            //        PeriodCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Deviation = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EmailNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScreenNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RescheduleCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        RescheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ExecutePerCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReportableOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        ReportableValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitDefAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitExceededAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        FrequencyLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitExceededAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitRuleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        LimitRuleGroupId = table.Column<long>(type: "INTEGER", nullable: false),
            //        LimitTokenId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false),
            //        SignAndId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LimitValue = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitRuleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitRuleGroupAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        ParentId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LogicSymbol = table.Column<int>(type: "INTEGER", nullable: false),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitRuleGroupAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitToken",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        RelatedEntity = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitToken", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitTokenAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        RelatedEntity = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitTokenAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Color = table.Column<string>(type: "TEXT", nullable: true),
            //        Weight = table.Column<uint>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Color = table.Column<string>(type: "TEXT", nullable: true),
            //        Weight = table.Column<uint>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LocationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SecondClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LocationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LocationType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Weight = table.Column<uint>(type: "INTEGER", nullable: false),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LocationType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LocationTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Weight = table.Column<uint>(type: "INTEGER", nullable: false),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LocationTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Log",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserName = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Log", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MapAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Path = table.Column<string>(type: "TEXT", nullable: true),
            //        MapCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MapAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MapCategoryAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MapCategoryAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        DataTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 15, scale: 6, nullable: true),
            //        DefaultValue = table.Column<string>(type: "TEXT", nullable: true),
            //        MinValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        MaxValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        Formula = table.Column<string>(type: "TEXT", nullable: true),
            //        Result = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AlwaysDefault = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DefaultFromFormula = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ChildMeasurementId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Format = table.Column<string>(type: "TEXT", nullable: true),
            //        TimeFrameDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        DecimalLength = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementCycleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CycleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementCycleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementListDefAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        DisplayText = table.Column<string>(type: "TEXT", nullable: true),
            //        Operation = table.Column<string>(type: "TEXT", nullable: true),
            //        Value1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Value2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Selected = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementListDefAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementSignAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementSignAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementUOMAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementUOMAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MediaAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Vendor = table.Column<string>(type: "TEXT", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        LotNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        LotNumberExpDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ManufacturerLotNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        GrowthPromotionResult = table.Column<string>(type: "TEXT", nullable: true),
            //        Inventory = table.Column<int>(type: "INTEGER", nullable: false),
            //        InventoryAdj = table.Column<int>(type: "INTEGER", nullable: false),
            //        Notified = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MediaTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        GrowthPromotionStatusId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MediaAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MediaType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        ProductNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        InventoryControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NotificationPercentage = table.Column<int>(type: "INTEGER", nullable: false),
            //        ProductNumRegex = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        ProductNumGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        MediaNumRegex = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        MediaNumGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        LotNumRegex = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        LotNumGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        ExpDateRegex = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        ExpDateGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        ExpDateFormat = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        BarcodeRegex = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        BarcodeGroup = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MediaType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MediaTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        ProductNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        InventoryControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NotificationPercentage = table.Column<int>(type: "INTEGER", nullable: false),
            //        ProductNumRegex = table.Column<string>(type: "TEXT", nullable: true),
            //        ProductNumGroup = table.Column<string>(type: "TEXT", nullable: true),
            //        MediaNumRegex = table.Column<string>(type: "TEXT", nullable: true),
            //        MediaNumGroup = table.Column<string>(type: "TEXT", nullable: true),
            //        LotNumRegex = table.Column<string>(type: "TEXT", nullable: true),
            //        LotNumGroup = table.Column<string>(type: "TEXT", nullable: true),
            //        ExpDateRegex = table.Column<string>(type: "TEXT", nullable: true),
            //        ExpDateGroup = table.Column<string>(type: "TEXT", nullable: true),
            //        ExpDateFormat = table.Column<string>(type: "TEXT", nullable: true),
            //        BarcodeRegex = table.Column<string>(type: "TEXT", nullable: true),
            //        BarcodeGroup = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MediaTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Content = table.Column<string>(type: "TEXT", nullable: true),
            //        NotificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        NotificationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        DeviationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationFrequency",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationFrequency", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationFrequencyAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationFrequencyAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Type = table.Column<int>(type: "INTEGER", nullable: false),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        EnName = table.Column<string>(type: "TEXT", nullable: true),
            //        ZhName = table.Column<string>(type: "TEXT", nullable: true),
            //        EnDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        ZhDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        ProcessorType = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Type = table.Column<int>(type: "INTEGER", nullable: false),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        EnName = table.Column<string>(type: "TEXT", nullable: true),
            //        ZhName = table.Column<string>(type: "TEXT", nullable: true),
            //        EnDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        ZhDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        ProcessorType = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Species = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Strain = table.Column<string>(type: "TEXT", nullable: true),
            //        IsGramStrain = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PicturePath = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismGenusId = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismSeverityId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismCharAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        IsApiCreated = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismCharAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismGenusAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        IsApiCreated = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismGenusAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismSeverity",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismSeverity", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismSeverityAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismSeverityAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrgFoundMethod",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrgFoundMethod", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrgFoundMethodAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrgFoundMethodAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ParticleCounterDataAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        DataId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        RoomName = table.Column<string>(type: "TEXT", nullable: true),
            //        SiteName = table.Column<string>(type: "TEXT", nullable: true),
            //        SampleDuration = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        Volume = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        UM05 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM10 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM20 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM30 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM40 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM50 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM100 = table.Column<long>(type: "INTEGER", nullable: true),
            //        MatchedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ParticleCounterDataAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Permission",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        DepartFormatter = table.Column<string>(type: "TEXT", nullable: true),
            //        Type = table.Column<int>(type: "INTEGER", nullable: false),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Order = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Permission", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Permission_Permission_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Permission",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonnelSiteAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonnelSiteAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PlanAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        IneffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ApprovedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        WorkWeek = table.Column<int>(type: "INTEGER", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        FillDayOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillTime = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillHoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillMinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillLength = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillEffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        AutoRetiredDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        LastFillDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        FirstDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsRepeatFill = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        PlanGroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        AutoFillFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PlanAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PlanGroup",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        ChangeHistory = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PlanGroup", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PlanGroupAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        ChangeHistory = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PlanGroupAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PreDefinedNote",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Message = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PreDefinedNote", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PreDefinedNoteAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Message = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PreDefinedNoteAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ProductAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        ChainOfCustody = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ReadingAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentSampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        WorkspaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        DilutionId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CycleNumber = table.Column<int>(type: "INTEGER", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: true),
            //        PerformUserName = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Value = table.Column<string>(type: "TEXT", nullable: true),
            //        DataTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Formula = table.Column<string>(type: "TEXT", nullable: true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        Result = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReadingDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReadingAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Role",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        IsHidden = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Role", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RoleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        IsHidden = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RoleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RolePermissionAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        RoleId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PermissionId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RolePermissionAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RptSampleMartAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteName = table.Column<string>(type: "TEXT", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeName = table.Column<string>(type: "TEXT", nullable: true),
            //        StartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EndDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        BarCode = table.Column<string>(type: "TEXT", nullable: true),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Reviewed = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Completed = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Classification = table.Column<string>(type: "TEXT", nullable: true),
            //        Product1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Batch1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Product2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Batch2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Organism1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Organism2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        ReadingId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestResultValue = table.Column<string>(type: "TEXT", nullable: true),
            //        TestResultNumberValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        MostServerDeviationId = table.Column<long>(type: "INTEGER", nullable: true),
            //        MostServerDeviation = table.Column<string>(type: "TEXT", nullable: true),
            //        MostServerLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        MostServerLimit = table.Column<string>(type: "TEXT", nullable: true),
            //        ActionLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ActionLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        AlertLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        AlertLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ActionLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        AlertLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        LowActionLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LowActionLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        LowAlertLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LowAlertLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        LowActionLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        LowAlertLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        CSign = table.Column<string>(type: "TEXT", nullable: true),
            //        PersonnelSiteCode = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredUserId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ResultsEnteredEmpId = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredUserName = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredFirstName = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredLastName = table.Column<string>(type: "TEXT", nullable: true),
            //        LocationId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LocationFullName = table.Column<string>(type: "TEXT", nullable: true),
            //        LocationIdBreadCrumb = table.Column<string>(type: "TEXT", nullable: true),
            //        UpdateDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CompleteDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ApproveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReviewedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RptSampleMartAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SampleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ParentSampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ParentDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ParentOrgFoundId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduledStartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        SamplingCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        StartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EndDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PersonnelSiteId = table.Column<long>(type: "INTEGER", nullable: true),
            //        PersonnelUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        PerformedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        BarCode = table.Column<string>(type: "TEXT", nullable: true),
            //        MeasurementUOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Vloume = table.Column<int>(type: "INTEGER", nullable: true),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        IsNegative = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ApprovedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ApprovedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Reviewed = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReviewedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ReviewedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        DeviationNote = table.Column<string>(type: "TEXT", nullable: true),
            //        IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CompleteDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsEmEdit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        IsFdcEdit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProductNote = table.Column<string>(type: "TEXT", nullable: true),
            //        WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DeviationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SampleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SampleReviewAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Note = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SampleReviewAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SeverityLocationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismSeverityId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SeverityLocationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Sign",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Sign", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SignAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SignAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SiteAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SecondClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SiteAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SiteType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SiteType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SiteTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SiteTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SubscriptionAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PlanGroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SubscriptionAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SubWorkflowTemplateMappingAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        NodeId = table.Column<string>(type: "TEXT", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ParentId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ChindId = table.Column<long>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SubWorkflowTemplateMappingAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TableNoteAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EnteryDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Note = table.Column<string>(type: "TEXT", nullable: true),
            //        TableName = table.Column<string>(type: "TEXT", nullable: true),
            //        TableId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TableNoteAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Interval = table.Column<int>(type: "INTEGER", nullable: false),
            //        MinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        HoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        MonthsOfYear = table.Column<int>(type: "INTEGER", nullable: false),
            //        Randomize = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NotTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NegativeControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SequenceAdvice = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScheduleStartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduleOffset = table.Column<int>(type: "INTEGER", nullable: true),
            //        Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationFrequencyId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentTestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TimeFrameDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestFrequencyId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestFrequencyOccurrenceId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestCategoryAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        TestClassId = table.Column<int>(type: "INTEGER", nullable: false),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestCategoryAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestClass", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestClassAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestClassAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequencyAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Interval = table.Column<int>(type: "INTEGER", nullable: false),
            //        MinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        HoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        MonthsOfYear = table.Column<int>(type: "INTEGER", nullable: false),
            //        Randomize = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScheduleStartDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        ScheduleOffset = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        TestFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestFrequencyOccurrenceId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequencyAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequencyOccurrence",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequencyOccurrence", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequencyOccurrenceAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequencyOccurrenceAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequencyType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequencyType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequencyTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequencyTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestStage",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        ESign = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ESignVerification = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoAssignNext = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowPersonnelPanel = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LockStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequirePerformedUser = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PrintLabels = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowReadings = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowOrgid = table.Column<bool>(type: "INTEGER", nullable: false),
            //        UsePreviousResult = table.Column<bool>(type: "INTEGER", nullable: false),
            //        VerifyPrevious = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowDeviceControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleMedia = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowIncubationTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowAddCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowEnvironment = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestStage", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestStageAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        ESign = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ESignVerification = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoAssignNext = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowPersonnelPanel = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LockStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequirePerformedUser = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PrintLabels = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowReadings = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowOrgid = table.Column<bool>(type: "INTEGER", nullable: false),
            //        UsePreviousResult = table.Column<bool>(type: "INTEGER", nullable: false),
            //        VerifyPrevious = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowDeviceControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleMedia = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowIncubationTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowAddCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowEnvironment = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestStageAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        TestCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestTypeCodeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        NegativeControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SystemRecord = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
            //        RequireProductSelection = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProdSelectionTimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeLabelId = table.Column<int>(type: "INTEGER", nullable: false),
            //        NeedApprove = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ApproveCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        NeedReview = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ReviewCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        EngineVerion = table.Column<int>(type: "INTEGER", nullable: true),
            //        WorkflowTemplateId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeCode",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeCode", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeCodeAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Icon = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeCodeAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeEquipmentAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        RequiredNum = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeEquipmentAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeLabel",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeLabel", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeLabelAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeLabelAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeMediaAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        RequiredNum = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeMediaAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrameAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ClonedTimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        TestStageId = table.Column<int>(type: "INTEGER", nullable: false),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        PromptMsg = table.Column<string>(type: "TEXT", nullable: true),
            //        MinTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        MaxTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        StepId = table.Column<string>(type: "TEXT", nullable: true),
            //        ExecuteTime = table.Column<int>(type: "INTEGER", nullable: true),
            //        NextAllocation = table.Column<int>(type: "INTEGER", nullable: true),
            //        InputByManual = table.Column<bool>(type: "INTEGER", nullable: true),
            //        InputByEquipment = table.Column<bool>(type: "INTEGER", nullable: true),
            //        MinCycles = table.Column<int>(type: "INTEGER", nullable: false),
            //        MaxCycles = table.Column<int>(type: "INTEGER", nullable: false),
            //        ESign = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ESignVerification = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProxyOperation = table.Column<bool>(type: "INTEGER", nullable: true),
            //        AddOrganism = table.Column<bool>(type: "INTEGER", nullable: true),
            //        UpLoadDocuments = table.Column<bool>(type: "INTEGER", nullable: true),
            //        BulkOperation = table.Column<bool>(type: "INTEGER", nullable: true),
            //        PrintLabels = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DockingDeviceData = table.Column<bool>(type: "INTEGER", nullable: true),
            //        FrontResult = table.Column<bool>(type: "INTEGER", nullable: true),
            //        VerifyPrevious = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SampleStartDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        SampleEndDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ExecuteUserCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        EnvironmentCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Product = table.Column<bool>(type: "INTEGER", nullable: true),
            //        IncubationStartDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        IncubationEndDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        MinCalAlignment = table.Column<int>(type: "INTEGER", nullable: false),
            //        MaxCalAlignment = table.Column<int>(type: "INTEGER", nullable: false),
            //        AutoAssignNext = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowPersonnelPanel = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoStartDate = table.Column<bool>(type: "INTEGER", nullable: true),
            //        AutoEndDate = table.Column<bool>(type: "INTEGER", nullable: true),
            //        LockStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequirePerformedUser = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowReadings = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowOrgid = table.Column<bool>(type: "INTEGER", nullable: false),
            //        UsePreviousResult = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowDeviceControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleMedia = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowIncubationTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowAddCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowEnvironment = table.Column<bool>(type: "INTEGER", nullable: false),
            //        MatchingResultsOnly = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PlatformType = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrameAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrameDilutionAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        DisplayText = table.Column<string>(type: "TEXT", nullable: true),
            //        ListValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: false),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Selected = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoFill = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrameDilutionAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrameEventAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EventType = table.Column<string>(type: "TEXT", nullable: true),
            //        ActionType = table.Column<string>(type: "TEXT", nullable: true),
            //        NextTimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrameEventAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UnitOfMeasure",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UnitOfMeasureAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitOfMeasureAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Account = table.Column<string>(type: "TEXT", nullable: true),
            //        Password = table.Column<string>(type: "TEXT", nullable: true),
            //        Salt = table.Column<string>(type: "TEXT", nullable: true),
            //        RealName = table.Column<string>(type: "TEXT", nullable: true),
            //        EmployeeId = table.Column<string>(type: "TEXT", nullable: true),
            //        Title = table.Column<string>(type: "TEXT", nullable: true),
            //        Gender = table.Column<int>(type: "INTEGER", nullable: true),
            //        EMail = table.Column<string>(type: "TEXT", nullable: true),
            //        Phone = table.Column<string>(type: "TEXT", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        LastMonitoredDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        InitialQualificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        NextQualificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PasswordExpiryPeriod = table.Column<long>(type: "INTEGER", nullable: true),
            //        LastPasswordModified = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsHidden = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DepartmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRoleAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        RoleId = table.Column<int>(type: "INTEGER", nullable: false),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRoleAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioDiagramAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MapId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioDiagramAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioLocationAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: false),
            //        X = table.Column<double>(type: "REAL", nullable: false),
            //        Y = table.Column<double>(type: "REAL", nullable: false),
            //        Width = table.Column<double>(type: "REAL", nullable: false),
            //        Height = table.Column<double>(type: "REAL", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioLocationAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioSiteAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: false),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: false),
            //        X = table.Column<double>(type: "REAL", nullable: false),
            //        Y = table.Column<double>(type: "REAL", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioSiteAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkflowParameter",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: false),
            //        ContextJson = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkflowParameter", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkflowTemplate",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        GraphJson = table.Column<string>(type: "TEXT", nullable: false),
            //        ExecutorJson = table.Column<string>(type: "TEXT", nullable: false),
            //        WorkflowId = table.Column<string>(type: "TEXT", nullable: false),
            //        Category = table.Column<int>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkflowTemplate", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkflowTemplateAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        GraphJson = table.Column<string>(type: "TEXT", nullable: false),
            //        ExecutorJson = table.Column<string>(type: "TEXT", nullable: false),
            //        WorkflowId = table.Column<string>(type: "TEXT", nullable: false),
            //        Category = table.Column<int>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<long>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkflowTemplateAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceCache",
            //    columns: table => new
            //    {
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestStageId = table.Column<int>(type: "INTEGER", nullable: false),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceEquipmentAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceEquipmentAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceMediaAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceMediaAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceOrganismAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<int>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceOrganismAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceProductAudit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProductId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        BatchRecordId = table.Column<int>(type: "INTEGER", nullable: true),
            //        BatchId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EntityId = table.Column<uint>(type: "INTEGER", nullable: false),
            //        AuditTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        Action = table.Column<byte>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceProductAudit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApprovalItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: true),
            //        StepId = table.Column<string>(type: "TEXT", nullable: false),
            //        Abandoned = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Comment = table.Column<string>(type: "TEXT", nullable: true),
            //        OperationTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        FromUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ApprovalEntityId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApprovalItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ApprovalItem_ApprovalEntity_ApprovalEntityId",
            //            column: x => x.ApprovalEntityId,
            //            principalTable: "ApprovalEntity",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ApprovalStep",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        StepId = table.Column<string>(type: "TEXT", nullable: false),
            //        UserIdStr = table.Column<string>(type: "TEXT", nullable: false),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        ExpectCount = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        ApprovalEntityId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApprovalStep", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ApprovalStep_ApprovalEntity_ApprovalEntityId",
            //            column: x => x.ApprovalEntityId,
            //            principalTable: "ApprovalEntity",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismChar",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        IsApiCreated = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismTypeId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismChar", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_OrganismChar_OrganismType_OrganismTypeId",
            //            column: x => x.OrganismTypeId,
            //            principalTable: "OrganismType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrganismGenus",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        IsApiCreated = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismTypeId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_OrganismGenus", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_OrganismGenus_OrganismType_OrganismTypeId",
            //            column: x => x.OrganismTypeId,
            //            principalTable: "OrganismType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RolePermission",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        RoleId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PermissionId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RolePermission", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RolePermission_Permission_PermissionId",
            //            column: x => x.PermissionId,
            //            principalTable: "Permission",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_RolePermission_Role_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Role",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DataTypeSign",
            //    columns: table => new
            //    {
            //        DataTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DataTypeSign", x => new { x.DataTypeId, x.SignId });
            //        table.ForeignKey(
            //            name: "FK_DataTypeSign_DataType_DataTypeId",
            //            column: x => x.DataTypeId,
            //            principalTable: "DataType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_DataTypeSign_Sign_SignId",
            //            column: x => x.SignId,
            //            principalTable: "Sign",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitTokenSign",
            //    columns: table => new
            //    {
            //        LimitTokenId = table.Column<int>(type: "INTEGER", nullable: false),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitTokenSign", x => new { x.LimitTokenId, x.SignId });
            //        table.ForeignKey(
            //            name: "FK_LimitTokenSign_LimitToken_LimitTokenId",
            //            column: x => x.LimitTokenId,
            //            principalTable: "LimitToken",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LimitTokenSign_Sign_SignId",
            //            column: x => x.SignId,
            //            principalTable: "Sign",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestCategory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        TestClassId = table.Column<int>(type: "INTEGER", nullable: false),
            //        En = table.Column<string>(type: "TEXT", nullable: true),
            //        Zh = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestCategory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TestCategory_TestClass_TestClassId",
            //            column: x => x.TestClassId,
            //            principalTable: "TestClass",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestFrequency",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Interval = table.Column<int>(type: "INTEGER", nullable: false),
            //        MinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        HoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        MonthsOfYear = table.Column<int>(type: "INTEGER", nullable: false),
            //        Randomize = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScheduleStartDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        ScheduleOffset = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        TestFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestFrequencyOccurrenceId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestFrequency", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TestFrequency_TestFrequencyOccurrence_TestFrequencyOccurrenceId",
            //            column: x => x.TestFrequencyOccurrenceId,
            //            principalTable: "TestFrequencyOccurrence",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_TestFrequency_TestFrequencyType_TestFrequencyTypeId",
            //            column: x => x.TestFrequencyTypeId,
            //            principalTable: "TestFrequencyType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SubWorkflowTemplateMapping",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        NodeId = table.Column<string>(type: "TEXT", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ParentId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ChindId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SubWorkflowTemplateMapping", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SubWorkflowTemplateMapping_WorkflowTemplate_ChindId",
            //            column: x => x.ChindId,
            //            principalTable: "WorkflowTemplate",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SubWorkflowTemplateMapping_WorkflowTemplate_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "WorkflowTemplate",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Organism",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Species = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Strain = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        IsGramStrain = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PicturePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismGenusId = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismSeverityId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Organism", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Organism_OrganismGenus_OrganismGenusId",
            //            column: x => x.OrganismGenusId,
            //            principalTable: "OrganismGenus",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Organism_OrganismSeverity_OrganismSeverityId",
            //            column: x => x.OrganismSeverityId,
            //            principalTable: "OrganismSeverity",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Batch",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProductId = table.Column<int>(type: "INTEGER", nullable: false),
            //        BatchLotNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Batch", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CompareDataRemark",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Remark = table.Column<string>(type: "TEXT", nullable: true),
            //        RemarkDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParticleCounterDataId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompareDataRemark", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CurrentWorkSpace",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SampleBarcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        EarlyExecutionDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduledDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        IneffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReplicateNumber = table.Column<int>(type: "INTEGER", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        Completed = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReplicateReason = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        WorkflowCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        WorkflowCompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PerformedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Scheduled = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LastEsigUserName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        RemainingMinutes = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        RecieveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LastUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LastStepRemark = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ExecuteUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Remark = table.Column<string>(type: "TEXT", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        BatchId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestStageId = table.Column<int>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: false),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentCurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CurrentWorkSpace", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CurrentWorkSpace_Batch_BatchId",
            //            column: x => x.BatchId,
            //            principalTable: "Batch",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_CurrentWorkSpace_CurrentWorkSpace_ParentCurrentWorkSpaceId",
            //            column: x => x.ParentCurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_CurrentWorkSpace_Group_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "Group",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_CurrentWorkSpace_TestStage_TestStageId",
            //            column: x => x.TestStageId,
            //            principalTable: "TestStage",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Cycle",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CycleNumber = table.Column<int>(type: "INTEGER", nullable: false),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        IncludeCycleName = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        CheckLimits = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Cycle", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Department",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        ManagerId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Department", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Department_Department_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Department",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Deviation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ReadingValue = table.Column<decimal>(type: "TEXT", nullable: false),
            //        DeviationNumber = table.Column<int>(type: "INTEGER", nullable: false),
            //        ValueDescription = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Deviation", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Deviation_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Deviation_LimitType_LimitTypeId",
            //            column: x => x.LimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ElectronicSignature",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Account = table.Column<string>(type: "TEXT", nullable: true),
            //        RealName = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        SignDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        IpAddress = table.Column<string>(type: "TEXT", nullable: true),
            //        Category = table.Column<string>(type: "TEXT", nullable: true),
            //        Comment = table.Column<string>(type: "TEXT", nullable: true),
            //        Order = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsSystemOperation = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ElectronicSignature", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ElectronicSignatureItem",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TableName = table.Column<string>(type: "TEXT", nullable: true),
            //        PrimaryKey = table.Column<long>(type: "INTEGER", nullable: true),
            //        ElectronicSignatureId = table.Column<long>(type: "INTEGER", nullable: false),
            //        LastAuditKey = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ElectronicSignatureItem", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ElectronicSignatureItem_ElectronicSignature_ElectronicSignatureId",
            //            column: x => x.ElectronicSignatureId,
            //            principalTable: "ElectronicSignature",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EMail",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Subject = table.Column<string>(type: "TEXT", nullable: true),
            //        Body = table.Column<string>(type: "TEXT", nullable: true),
            //        IsHtmlBody = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SendDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EMail", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EMailAttachment",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        FilePath = table.Column<string>(type: "TEXT", nullable: true),
            //        ContentId = table.Column<string>(type: "TEXT", nullable: true),
            //        UsedInBody = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EMailId = table.Column<long>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EMailAttachment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EMailAttachment_EMail_EMailId",
            //            column: x => x.EMailId,
            //            principalTable: "EMail",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Equipment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        SerialNumber = table.Column<string>(type: "TEXT", nullable: true),
            //        LastSyncDataTimestamp = table.Column<long>(type: "INTEGER", nullable: true),
            //        CalibrationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CalibrationValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        NextCalibrationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        ControlNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
            //        PerformedBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DeviceConfigId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Equipment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Equipment_DeviceConfig_DeviceConfigId",
            //            column: x => x.DeviceConfigId,
            //            principalTable: "DeviceConfig",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Equipment_EquipmentType_EquipmentTypeId",
            //            column: x => x.EquipmentTypeId,
            //            principalTable: "EquipmentType",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Equipment_UnitOfMeasure_UnitOfMeasureId",
            //            column: x => x.UnitOfMeasureId,
            //            principalTable: "UnitOfMeasure",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceEquipment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceEquipment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceEquipment_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceEquipment_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FileAttachment",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Path = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FileAttachment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_FileAttachment_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Limit",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Prevalence = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        FreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SourceLimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SourceLimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        OccurrenceCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Period = table.Column<int>(type: "INTEGER", nullable: true),
            //        PeriodCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        LimitExceededId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Deviation = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EmailNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScreenNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RescheduleCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        RescheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ExecutePerCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReportableOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        ReportableValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ParentLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Limit", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Limit_Limit_ParentLimitId",
            //            column: x => x.ParentLimitId,
            //            principalTable: "Limit",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Limit_LimitType_LimitTypeId",
            //            column: x => x.LimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Limit_LimitType_SourceLimitTypeId",
            //            column: x => x.SourceLimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitDef",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Prevalence = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        FreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SourceLimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SourceLimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        OccurrenceCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Period = table.Column<int>(type: "INTEGER", nullable: true),
            //        PeriodCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        Deviation = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EmailNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScreenNotify = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RescheduleCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        RescheduleOffset = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ExecutePerCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReportableOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        ReportableValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitDef", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LimitDef_LimitDef_SourceLimitDefId",
            //            column: x => x.SourceLimitDefId,
            //            principalTable: "LimitDef",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_LimitDef_LimitType_LimitTypeId",
            //            column: x => x.LimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LimitDef_LimitType_SourceLimitTypeId",
            //            column: x => x.SourceLimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitRuleGroup",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        ParentId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LogicSymbol = table.Column<int>(type: "INTEGER", nullable: false),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitRuleGroup", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LimitRuleGroup_Limit_LimitId",
            //            column: x => x.LimitId,
            //            principalTable: "Limit",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_LimitRuleGroup_LimitDef_LimitDefId",
            //            column: x => x.LimitDefId,
            //            principalTable: "LimitDef",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LimitRuleGroup_LimitRuleGroup_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "LimitRuleGroup",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitExceeded",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        FrequencyLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LimitTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CountTowardFrequency = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ResetFreqLimit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Reschedule = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitExceeded", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LimitExceeded_Limit_FrequencyLimitId",
            //            column: x => x.FrequencyLimitId,
            //            principalTable: "Limit",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_LimitExceeded_Limit_LimitId",
            //            column: x => x.LimitId,
            //            principalTable: "Limit",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_LimitExceeded_LimitType_LimitTypeId",
            //            column: x => x.LimitTypeId,
            //            principalTable: "LimitType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LimitRule",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        LimitRuleGroupId = table.Column<long>(type: "INTEGER", nullable: false),
            //        LimitTokenId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false),
            //        SignAndId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LimitValue = table.Column<string>(type: "TEXT", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LimitRule", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LimitRule_LimitRuleGroup_LimitRuleGroupId",
            //            column: x => x.LimitRuleGroupId,
            //            principalTable: "LimitRuleGroup",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LimitRule_LimitToken_LimitTokenId",
            //            column: x => x.LimitTokenId,
            //            principalTable: "LimitToken",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LimitRule_Sign_SignAndId",
            //            column: x => x.SignAndId,
            //            principalTable: "Sign",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_LimitRule_Sign_SignId",
            //            column: x => x.SignId,
            //            principalTable: "Sign",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Location",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        Code = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SecondClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Location", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Location_Classification_ClassificationId",
            //            column: x => x.ClassificationId,
            //            principalTable: "Classification",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Location_Classification_SecondClassificationId",
            //            column: x => x.SecondClassificationId,
            //            principalTable: "Classification",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Location_Location_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Location_LocationType_LocationTypeId",
            //            column: x => x.LocationTypeId,
            //            principalTable: "LocationType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LocationExtra",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        Breadcrumb = table.Column<string>(type: "TEXT", nullable: true),
            //        LocationPath = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LocationExtra", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LocationExtra_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Media",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Vendor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        LotNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        LotNumberExpDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ManufacturerLotNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        GrowthPromotionResult = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true),
            //        Inventory = table.Column<int>(type: "INTEGER", nullable: false),
            //        InventoryAdj = table.Column<int>(type: "INTEGER", nullable: false),
            //        Notified = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MediaTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        GrowthPromotionStatusId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Media", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Media_GrowthPromotionStatus_GrowthPromotionStatusId",
            //            column: x => x.GrowthPromotionStatusId,
            //            principalTable: "GrowthPromotionStatus",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Media_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Media_MediaType_MediaTypeId",
            //            column: x => x.MediaTypeId,
            //            principalTable: "MediaType",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Plan",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Version = table.Column<int>(type: "INTEGER", nullable: false),
            //        Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        IneffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ApprovedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CompletedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false),
            //        WorkWeek = table.Column<int>(type: "INTEGER", nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
            //        FillDayOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillTime = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillHoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillMinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillLength = table.Column<int>(type: "INTEGER", nullable: false),
            //        FillEffectiveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        AutoRetiredDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        LastFillDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        FirstDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        IsRepeatFill = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        PlanGroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        AutoFillFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Plan", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Plan_EnvironmentDef_EnvironmentId",
            //            column: x => x.EnvironmentId,
            //            principalTable: "EnvironmentDef",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Plan_Group_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "Group",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Plan_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Plan_PlanGroup_PlanGroupId",
            //            column: x => x.PlanGroupId,
            //            principalTable: "PlanGroup",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Plan_TestFrequencyType_AutoFillFrequencyTypeId",
            //            column: x => x.AutoFillFrequencyTypeId,
            //            principalTable: "TestFrequencyType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        ChainOfCustody = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Product_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SeverityLocation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        OrganismSeverityId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SeverityLocation", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SeverityLocation_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SeverityLocation_Organism_OrganismId",
            //            column: x => x.OrganismId,
            //            principalTable: "Organism",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_SeverityLocation_OrganismSeverity_OrganismSeverityId",
            //            column: x => x.OrganismSeverityId,
            //            principalTable: "OrganismSeverity",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Site",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Barcode = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SecondClassificationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteTypeId = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Site", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Site_Classification_ClassificationId",
            //            column: x => x.ClassificationId,
            //            principalTable: "Classification",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Site_Classification_SecondClassificationId",
            //            column: x => x.SecondClassificationId,
            //            principalTable: "Classification",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Site_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Site_SiteType_SiteTypeId",
            //            column: x => x.SiteTypeId,
            //            principalTable: "SiteType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Account = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
            //        Password = table.Column<string>(type: "TEXT", nullable: false),
            //        Salt = table.Column<string>(type: "TEXT", nullable: true),
            //        RealName = table.Column<string>(type: "TEXT", nullable: true),
            //        EmployeeId = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        Title = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        Gender = table.Column<int>(type: "INTEGER", nullable: true),
            //        EMail = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
            //        Phone = table.Column<string>(type: "TEXT", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
            //        LastMonitoredDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        InitialQualificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        NextQualificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PasswordExpiryPeriod = table.Column<long>(type: "INTEGER", nullable: true),
            //        LastPasswordModified = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsHidden = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DepartmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_User_Department_DepartmentId",
            //            column: x => x.DepartmentId,
            //            principalTable: "Department",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_User_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceMedia",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceMedia", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceMedia_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceMedia_Media_MediaId",
            //            column: x => x.MediaId,
            //            principalTable: "Media",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RptSampleMart",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteName = table.Column<string>(type: "TEXT", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeName = table.Column<string>(type: "TEXT", nullable: true),
            //        StartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EndDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        BarCode = table.Column<string>(type: "TEXT", nullable: true),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Reviewed = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Completed = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Classification = table.Column<string>(type: "TEXT", nullable: true),
            //        Product1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Batch1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Product2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Batch2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Organism1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Organism2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading1Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading1UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading2Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading2UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3Cycle = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Seq = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Desc = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Value = table.Column<int>(type: "INTEGER", nullable: true),
            //        Reading3Text = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3Sign = table.Column<string>(type: "TEXT", nullable: true),
            //        Reading3UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        ReadingId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestResultValue = table.Column<string>(type: "TEXT", nullable: true),
            //        TestResultNumberValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        UOM = table.Column<string>(type: "TEXT", nullable: true),
            //        MostServerDeviationId = table.Column<long>(type: "INTEGER", nullable: true),
            //        MostServerDeviation = table.Column<string>(type: "TEXT", nullable: true),
            //        MostServerLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        MostServerLimit = table.Column<string>(type: "TEXT", nullable: true),
            //        ActionLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ActionLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        AlertLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        AlertLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        ActionLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        AlertLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        LowActionLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LowActionLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        LowAlertLimitId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LowAlertLimitValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        LowActionLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        LowAlertLimitOperator = table.Column<string>(type: "TEXT", nullable: true),
            //        CSign = table.Column<string>(type: "TEXT", nullable: true),
            //        PersonnelSiteCode = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredUserId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ResultsEnteredEmpId = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredUserName = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredFirstName = table.Column<string>(type: "TEXT", nullable: true),
            //        ResultsEnteredLastName = table.Column<string>(type: "TEXT", nullable: true),
            //        LocationId = table.Column<long>(type: "INTEGER", nullable: true),
            //        LocationFullName = table.Column<string>(type: "TEXT", nullable: true),
            //        LocationIdBreadCrumb = table.Column<string>(type: "TEXT", nullable: true),
            //        UpdateDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CompleteDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ApproveDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ReviewedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RptSampleMart", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RptSampleMart_EnvironmentDef_EnvironmentId",
            //            column: x => x.EnvironmentId,
            //            principalTable: "EnvironmentDef",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_RptSampleMart_Site_SiteId",
            //            column: x => x.SiteId,
            //            principalTable: "Site",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Subscription",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        PlanGroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Subscription", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Subscription_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Subscription_NotificationType_NotificationTypeId",
            //            column: x => x.NotificationTypeId,
            //            principalTable: "NotificationType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Subscription_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TableNote",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EnteryDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Note = table.Column<string>(type: "TEXT", nullable: true),
            //        TableName = table.Column<string>(type: "TEXT", nullable: true),
            //        TableId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TableNote", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TableNote_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserHistory",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        Logined = table.Column<long>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserHistory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserHistory_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserPasswordHistory",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Password = table.Column<string>(type: "TEXT", nullable: true),
            //        Salt = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserPasswordHistory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserPasswordHistory_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserProfile",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        DashboardConfig = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        Locale = table.Column<string>(type: "TEXT", nullable: true),
            //        UserSettings = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserProfile", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserProfile_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRole",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        RoleId = table.Column<int>(type: "INTEGER", nullable: false),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRole", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserRole_Role_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Role",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserRole_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Map",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Path = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        MapCategoryId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Map", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MapCategory",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        ParentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MapCategory", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MapCategory_Map_ParentId",
            //            column: x => x.ParentId,
            //            principalTable: "Map",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioDiagram",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MapId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioDiagram", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_VisioDiagram_Map_MapId",
            //            column: x => x.MapId,
            //            principalTable: "Map",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioLocation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: false),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: false),
            //        X = table.Column<double>(type: "REAL", nullable: false),
            //        Y = table.Column<double>(type: "REAL", nullable: false),
            //        Width = table.Column<double>(type: "REAL", nullable: false),
            //        Height = table.Column<double>(type: "REAL", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioLocation", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_VisioLocation_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_VisioLocation_VisioDiagram_VisioDiagramId",
            //            column: x => x.VisioDiagramId,
            //            principalTable: "VisioDiagram",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "VisioSite",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: false),
            //        VisioDiagramId = table.Column<int>(type: "INTEGER", nullable: false),
            //        X = table.Column<double>(type: "REAL", nullable: false),
            //        Y = table.Column<double>(type: "REAL", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_VisioSite", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_VisioSite_Site_SiteId",
            //            column: x => x.SiteId,
            //            principalTable: "Site",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_VisioSite_VisioDiagram_VisioDiagramId",
            //            column: x => x.VisioDiagramId,
            //            principalTable: "VisioDiagram",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Measurement",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        DataTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 15, scale: 6, nullable: true),
            //        DefaultValue = table.Column<string>(type: "TEXT", nullable: true),
            //        MinValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        MaxValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        Formula = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: true),
            //        Result = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AlwaysDefault = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DefaultFromFormula = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ChildMeasurementId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Format = table.Column<string>(type: "TEXT", nullable: true),
            //        TimeFrameDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        DecimalLength = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Measurement", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Measurement_DataType_DataTypeId",
            //            column: x => x.DataTypeId,
            //            principalTable: "DataType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Measurement_Measurement_ChildMeasurementId",
            //            column: x => x.ChildMeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementCycle",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CycleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementCycle", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MeasurementCycle_Cycle_CycleId",
            //            column: x => x.CycleId,
            //            principalTable: "Cycle",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MeasurementCycle_Measurement_MeasurementId",
            //            column: x => x.MeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementListDef",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        DisplayText = table.Column<string>(type: "TEXT", nullable: true),
            //        Operation = table.Column<string>(type: "TEXT", nullable: true),
            //        Value1 = table.Column<string>(type: "TEXT", nullable: true),
            //        Value2 = table.Column<string>(type: "TEXT", nullable: true),
            //        Selected = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementListDef", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MeasurementListDef_Measurement_MeasurementId",
            //            column: x => x.MeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementSign",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementSign", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MeasurementSign_Measurement_MeasurementId",
            //            column: x => x.MeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MeasurementSign_Sign_SignId",
            //            column: x => x.SignId,
            //            principalTable: "Sign",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MeasurementUOM",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MeasurementUOM", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_MeasurementUOM_Measurement_MeasurementId",
            //            column: x => x.MeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MeasurementUOM_UnitOfMeasure_UnitOfMeasureId",
            //            column: x => x.UnitOfMeasureId,
            //            principalTable: "UnitOfMeasure",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Notification",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Content = table.Column<string>(type: "TEXT", nullable: true),
            //        NotificationDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        NotificationTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        DeviationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Notification", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Notification_Deviation_DeviationId",
            //            column: x => x.DeviationId,
            //            principalTable: "Deviation",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Notification_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Notification_Media_MediaId",
            //            column: x => x.MediaId,
            //            principalTable: "Media",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Notification_NotificationType_NotificationTypeId",
            //            column: x => x.NotificationTypeId,
            //            principalTable: "NotificationType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Notification_Site_SiteId",
            //            column: x => x.SiteId,
            //            principalTable: "Site",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Notification_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NotificationEMail",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        NotificationId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EMailId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NotificationEMail", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_NotificationEMail_EMail_EMailId",
            //            column: x => x.EMailId,
            //            principalTable: "EMail",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_NotificationEMail_Notification_NotificationId",
            //            column: x => x.NotificationId,
            //            principalTable: "Notification",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ParticleCounterData",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        DataId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Status = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleDate = table.Column<long>(type: "INTEGER", nullable: false),
            //        RoomName = table.Column<string>(type: "TEXT", nullable: true),
            //        SiteName = table.Column<string>(type: "TEXT", nullable: true),
            //        SampleDuration = table.Column<TimeSpan>(type: "TEXT", nullable: true),
            //        Volume = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        UM05 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM10 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM20 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM30 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM40 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM50 = table.Column<long>(type: "INTEGER", nullable: true),
            //        UM100 = table.Column<long>(type: "INTEGER", nullable: true),
            //        MatchedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ParticleCounterData", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ParticleCounterData_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ParticleCounterData_Site_SiteId",
            //            column: x => x.SiteId,
            //            principalTable: "Site",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PersonnelSite",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Code = table.Column<string>(type: "TEXT", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PersonnelSite", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Reading",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentSampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        WorkspaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        DilutionId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MeasurementId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CycleNumber = table.Column<int>(type: "INTEGER", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: true),
            //        PerformUserName = table.Column<int>(type: "INTEGER", nullable: true),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Value = table.Column<string>(type: "TEXT", nullable: true),
            //        DataTypeId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Description = table.Column<string>(type: "TEXT", nullable: true),
            //        Formula = table.Column<string>(type: "TEXT", nullable: true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SignId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ParticleSize = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: true),
            //        Result = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReadingDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Reading", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Reading_Measurement_MeasurementId",
            //            column: x => x.MeasurementId,
            //            principalTable: "Measurement",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Reading_Organism_OrganismId",
            //            column: x => x.OrganismId,
            //            principalTable: "Organism",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Reading_Sign_SignId",
            //            column: x => x.SignId,
            //            principalTable: "Sign",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Reading_UnitOfMeasure_UnitOfMeasureId",
            //            column: x => x.UnitOfMeasureId,
            //            principalTable: "UnitOfMeasure",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Sample",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ParentSampleId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ParentDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ParentOrgFoundId = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduledStartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        SamplingCompleted = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Name = table.Column<string>(type: "TEXT", nullable: true),
            //        TestId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaId = table.Column<int>(type: "INTEGER", nullable: true),
            //        EquipmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        StartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        EndDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        PersonnelSiteId = table.Column<long>(type: "INTEGER", nullable: true),
            //        PersonnelUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        PerformedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        BarCode = table.Column<string>(type: "TEXT", nullable: true),
            //        MeasurementUOM = table.Column<string>(type: "TEXT", nullable: true),
            //        Vloume = table.Column<int>(type: "INTEGER", nullable: true),
            //        NoTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        IsNegative = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Approved = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ApprovedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ApprovedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        Reviewed = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ReviewedUserId = table.Column<int>(type: "INTEGER", nullable: true),
            //        ReviewedDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        DeviationNote = table.Column<string>(type: "TEXT", nullable: true),
            //        IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CompleteDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        IsEmEdit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        IsFdcEdit = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProductNote = table.Column<string>(type: "TEXT", nullable: true),
            //        WorkflowInstanceId = table.Column<string>(type: "TEXT", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        DeviationId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Sample", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Sample_Deviation_DeviationId",
            //            column: x => x.DeviationId,
            //            principalTable: "Deviation",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_EnvironmentDef_EnvironmentId",
            //            column: x => x.EnvironmentId,
            //            principalTable: "EnvironmentDef",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_Equipment_EquipmentId",
            //            column: x => x.EquipmentId,
            //            principalTable: "Equipment",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_Media_MediaId",
            //            column: x => x.MediaId,
            //            principalTable: "Media",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_PersonnelSite_PersonnelSiteId",
            //            column: x => x.PersonnelSiteId,
            //            principalTable: "PersonnelSite",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_Sample_ParentSampleId",
            //            column: x => x.ParentSampleId,
            //            principalTable: "Sample",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Sample_User_ApprovedUserId",
            //            column: x => x.ApprovedUserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_User_PerformedUserId",
            //            column: x => x.PerformedUserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Sample_User_PersonnelUserId",
            //            column: x => x.PersonnelUserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SampleReview",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        Note = table.Column<string>(type: "TEXT", nullable: true),
            //        UserId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SampleReview", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_SampleReview_Sample_SampleId",
            //            column: x => x.SampleId,
            //            principalTable: "Sample",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_SampleReview_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceOrganism",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        OrganismId = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceOrganism", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceOrganism_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceOrganism_Organism_OrganismId",
            //            column: x => x.OrganismId,
            //            principalTable: "Organism",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceOrganism_Sample_SampleId",
            //            column: x => x.SampleId,
            //            principalTable: "Sample",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorkSpaceProduct",
            //    columns: table => new
            //    {
            //        Id = table.Column<uint>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ProductId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SampleId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CurrentWorkSpaceId = table.Column<long>(type: "INTEGER", nullable: false),
            //        BatchRecordId = table.Column<int>(type: "INTEGER", nullable: true),
            //        BatchId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorkSpaceProduct", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceProduct_Batch_BatchId",
            //            column: x => x.BatchId,
            //            principalTable: "Batch",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceProduct_CurrentWorkSpace_CurrentWorkSpaceId",
            //            column: x => x.CurrentWorkSpaceId,
            //            principalTable: "CurrentWorkSpace",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceProduct_Product_ProductId",
            //            column: x => x.ProductId,
            //            principalTable: "Product",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_WorkSpaceProduct_Sample_SampleId",
            //            column: x => x.SampleId,
            //            principalTable: "Sample",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Test",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        Interval = table.Column<int>(type: "INTEGER", nullable: false),
            //        MinutesOfHour = table.Column<int>(type: "INTEGER", nullable: false),
            //        HoursOfDay = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfWeek = table.Column<int>(type: "INTEGER", nullable: false),
            //        DaysOfMonth = table.Column<int>(type: "INTEGER", nullable: false),
            //        MonthsOfYear = table.Column<int>(type: "INTEGER", nullable: false),
            //        Randomize = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NotTest = table.Column<bool>(type: "INTEGER", nullable: false),
            //        NegativeControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SequenceAdvice = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ScheduleStartDate = table.Column<long>(type: "INTEGER", nullable: true),
            //        ScheduleOffset = table.Column<int>(type: "INTEGER", nullable: true),
            //        Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
            //        EnvironmentId = table.Column<int>(type: "INTEGER", nullable: true),
            //        NotificationFrequencyId = table.Column<int>(type: "INTEGER", nullable: true),
            //        SiteId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        PlanId = table.Column<long>(type: "INTEGER", nullable: false),
            //        ParentTestId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TimeFrameDilutionId = table.Column<long>(type: "INTEGER", nullable: true),
            //        GroupId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestFrequencyId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestFrequencyTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        TestFrequencyOccurrenceId = table.Column<int>(type: "INTEGER", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Test", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Test_EnvironmentDef_EnvironmentId",
            //            column: x => x.EnvironmentId,
            //            principalTable: "EnvironmentDef",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Test_Group_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "Group",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Test_NotificationFrequency_NotificationFrequencyId",
            //            column: x => x.NotificationFrequencyId,
            //            principalTable: "NotificationFrequency",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Test_Plan_PlanId",
            //            column: x => x.PlanId,
            //            principalTable: "Plan",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_Test_Site_SiteId",
            //            column: x => x.SiteId,
            //            principalTable: "Site",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Test_Test_ParentTestId",
            //            column: x => x.ParentTestId,
            //            principalTable: "Test",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Test_TestFrequency_TestFrequencyId",
            //            column: x => x.TestFrequencyId,
            //            principalTable: "TestFrequency",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Test_TestFrequencyOccurrence_TestFrequencyOccurrenceId",
            //            column: x => x.TestFrequencyOccurrenceId,
            //            principalTable: "TestFrequencyOccurrence",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_Test_TestFrequencyType_TestFrequencyTypeId",
            //            column: x => x.TestFrequencyTypeId,
            //            principalTable: "TestFrequencyType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestType",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        TestCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
            //        LocationId = table.Column<int>(type: "INTEGER", nullable: true),
            //        TestTypeCodeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        NegativeControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SystemRecord = table.Column<bool>(type: "INTEGER", nullable: false),
            //        Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
            //        RequireProductSelection = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProdSelectionTimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeLabelId = table.Column<int>(type: "INTEGER", nullable: false),
            //        NeedApprove = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ApproveCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        NeedReview = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ReviewCount = table.Column<int>(type: "INTEGER", nullable: true),
            //        EngineVerion = table.Column<int>(type: "INTEGER", nullable: true),
            //        WorkflowTemplateId = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestType", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TestType_Location_LocationId",
            //            column: x => x.LocationId,
            //            principalTable: "Location",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.SetNull);
            //        table.ForeignKey(
            //            name: "FK_TestType_TestCategory_TestCategoryId",
            //            column: x => x.TestCategoryId,
            //            principalTable: "TestCategory",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TestType_TestTypeCode_TestTypeCodeId",
            //            column: x => x.TestTypeCodeId,
            //            principalTable: "TestTypeCode",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TestType_TestTypeLabel_TestTypeLabelId",
            //            column: x => x.TestTypeLabelId,
            //            principalTable: "TestTypeLabel",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrame",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        ClonedTimeFrameId = table.Column<long>(type: "INTEGER", nullable: true),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: false),
            //        TestStageId = table.Column<int>(type: "INTEGER", nullable: false),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
            //        PromptMsg = table.Column<string>(type: "TEXT", nullable: true),
            //        MinTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        MaxTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        StepId = table.Column<string>(type: "TEXT", nullable: true),
            //        ExecuteTime = table.Column<int>(type: "INTEGER", nullable: true),
            //        NextAllocation = table.Column<int>(type: "INTEGER", nullable: true),
            //        InputByManual = table.Column<bool>(type: "INTEGER", nullable: true),
            //        InputByEquipment = table.Column<bool>(type: "INTEGER", nullable: true),
            //        MinCycles = table.Column<int>(type: "INTEGER", nullable: false),
            //        MaxCycles = table.Column<int>(type: "INTEGER", nullable: false),
            //        ESign = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ESignVerification = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ProxyOperation = table.Column<bool>(type: "INTEGER", nullable: true),
            //        AddOrganism = table.Column<bool>(type: "INTEGER", nullable: true),
            //        UpLoadDocuments = table.Column<bool>(type: "INTEGER", nullable: true),
            //        BulkOperation = table.Column<bool>(type: "INTEGER", nullable: true),
            //        PrintLabels = table.Column<bool>(type: "INTEGER", nullable: false),
            //        DockingDeviceData = table.Column<bool>(type: "INTEGER", nullable: true),
            //        FrontResult = table.Column<bool>(type: "INTEGER", nullable: true),
            //        VerifyPrevious = table.Column<bool>(type: "INTEGER", nullable: false),
            //        SampleStartDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        SampleEndDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        ExecuteUserCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        EnvironmentCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        Product = table.Column<bool>(type: "INTEGER", nullable: true),
            //        IncubationStartDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        IncubationEndDateCanEdit = table.Column<bool>(type: "INTEGER", nullable: true),
            //        MinCalAlignment = table.Column<int>(type: "INTEGER", nullable: false),
            //        MaxCalAlignment = table.Column<int>(type: "INTEGER", nullable: false),
            //        AutoAssignNext = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowPersonnelPanel = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequireEndDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoStartDate = table.Column<bool>(type: "INTEGER", nullable: true),
            //        AutoEndDate = table.Column<bool>(type: "INTEGER", nullable: true),
            //        LockStartDate = table.Column<bool>(type: "INTEGER", nullable: false),
            //        RequirePerformedUser = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowReadings = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowOrgid = table.Column<bool>(type: "INTEGER", nullable: false),
            //        UsePreviousResult = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowDeviceControl = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleMedia = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowSampleTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowIncubationTimes = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowAddCycle = table.Column<bool>(type: "INTEGER", nullable: false),
            //        ShowEnvironment = table.Column<bool>(type: "INTEGER", nullable: false),
            //        MatchingResultsOnly = table.Column<bool>(type: "INTEGER", nullable: false),
            //        PlatformType = table.Column<int>(type: "INTEGER", nullable: true),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrame", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TimeFrame_TestStage_TestStageId",
            //            column: x => x.TestStageId,
            //            principalTable: "TestStage",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TimeFrame_TestType_TestTypeId",
            //            column: x => x.TestTypeId,
            //            principalTable: "TestType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TimeFrame_TimeFrame_ClonedTimeFrameId",
            //            column: x => x.ClonedTimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeEquipment",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        RequiredNum = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeEquipment", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TestTypeEquipment_EquipmentType_EquipmentTypeId",
            //            column: x => x.EquipmentTypeId,
            //            principalTable: "EquipmentType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TestTypeEquipment_TimeFrame_TimeFrameId",
            //            column: x => x.TimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TestTypeMedia",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        MediaTypeId = table.Column<int>(type: "INTEGER", nullable: false),
            //        RequiredNum = table.Column<int>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TestTypeMedia", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TestTypeMedia_MediaType_MediaTypeId",
            //            column: x => x.MediaTypeId,
            //            principalTable: "MediaType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TestTypeMedia_TimeFrame_TimeFrameId",
            //            column: x => x.TimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrameDilution",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        TestTypeId = table.Column<long>(type: "INTEGER", nullable: true),
            //        Sequence = table.Column<int>(type: "INTEGER", nullable: false),
            //        DisplayText = table.Column<string>(type: "TEXT", nullable: true),
            //        ListValue = table.Column<decimal>(type: "TEXT", precision: 20, scale: 6, nullable: false),
            //        UnitOfMeasureId = table.Column<int>(type: "INTEGER", nullable: true),
            //        Selected = table.Column<bool>(type: "INTEGER", nullable: false),
            //        AutoFill = table.Column<bool>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrameDilution", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TimeFrameDilution_TestType_TestTypeId",
            //            column: x => x.TestTypeId,
            //            principalTable: "TestType",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_TimeFrameDilution_TimeFrame_TimeFrameId",
            //            column: x => x.TimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TimeFrameDilution_UnitOfMeasure_UnitOfMeasureId",
            //            column: x => x.UnitOfMeasureId,
            //            principalTable: "UnitOfMeasure",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TimeFrameEvent",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "INTEGER", nullable: false)
            //            .Annotation("Sqlite:Autoincrement", true),
            //        TimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        LimitDefId = table.Column<long>(type: "INTEGER", nullable: false),
            //        EventType = table.Column<string>(type: "TEXT", nullable: true),
            //        ActionType = table.Column<string>(type: "TEXT", nullable: true),
            //        NextTimeFrameId = table.Column<long>(type: "INTEGER", nullable: false),
            //        CreatedTime = table.Column<long>(type: "INTEGER", nullable: false),
            //        UpdatedTime = table.Column<long>(type: "INTEGER", nullable: true),
            //        CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
            //        UpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
            //        IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TimeFrameEvent", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TimeFrameEvent_LimitDef_LimitDefId",
            //            column: x => x.LimitDefId,
            //            principalTable: "LimitDef",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TimeFrameEvent_TimeFrame_NextTimeFrameId",
            //            column: x => x.NextTimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_TimeFrameEvent_TimeFrame_TimeFrameId",
            //            column: x => x.TimeFrameId,
            //            principalTable: "TimeFrame",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApprovalItem_ApprovalEntityId",
            //    table: "ApprovalItem",
            //    column: "ApprovalEntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApprovalStep_ApprovalEntityId",
            //    table: "ApprovalStep",
            //    column: "ApprovalEntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ApprovalStepAudit_EntityId",
            //    table: "ApprovalStepAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Batch_ProductId",
            //    table: "Batch",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_BatchAudit_EntityId",
            //    table: "BatchAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Classification_Name",
            //    table: "Classification",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ClassificationAudit_EntityId",
            //    table: "ClassificationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CompareDataRemark_ParticleCounterDataId",
            //    table: "CompareDataRemark",
            //    column: "ParticleCounterDataId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CompareDataRemark_UserId",
            //    table: "CompareDataRemark",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CompareDataRemarkAudit_EntityId",
            //    table: "CompareDataRemarkAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ConfigItemAudit_EntityId",
            //    table: "ConfigItemAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_BatchId",
            //    table: "CurrentWorkSpace",
            //    column: "BatchId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_ExecuteUserId",
            //    table: "CurrentWorkSpace",
            //    column: "ExecuteUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_GroupId",
            //    table: "CurrentWorkSpace",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_LastUserId",
            //    table: "CurrentWorkSpace",
            //    column: "LastUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_LocationId",
            //    table: "CurrentWorkSpace",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_ParentCurrentWorkSpaceId",
            //    table: "CurrentWorkSpace",
            //    column: "ParentCurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_PerformedUserId",
            //    table: "CurrentWorkSpace",
            //    column: "PerformedUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_PlanId",
            //    table: "CurrentWorkSpace",
            //    column: "PlanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_SampleId",
            //    table: "CurrentWorkSpace",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_TestId",
            //    table: "CurrentWorkSpace",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_TestStageId",
            //    table: "CurrentWorkSpace",
            //    column: "TestStageId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_TimeFrameId",
            //    table: "CurrentWorkSpace",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpace_UserId",
            //    table: "CurrentWorkSpace",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CurrentWorkSpaceAudit_EntityId",
            //    table: "CurrentWorkSpaceAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Cycle_TimeFrameId",
            //    table: "Cycle",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CycleAudit_EntityId",
            //    table: "CycleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DataTypeAudit_EntityId",
            //    table: "DataTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DataTypeSign_SignId",
            //    table: "DataTypeSign",
            //    column: "SignId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Department_ManagerId",
            //    table: "Department",
            //    column: "ManagerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Department_Name",
            //    table: "Department",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Department_ParentId",
            //    table: "Department",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DepartmentAudit_EntityId",
            //    table: "DepartmentAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deviation_CurrentWorkSpaceId",
            //    table: "Deviation",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deviation_LimitDefId",
            //    table: "Deviation",
            //    column: "LimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deviation_LimitTypeId",
            //    table: "Deviation",
            //    column: "LimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deviation_SampleId",
            //    table: "Deviation",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Deviation_TestId",
            //    table: "Deviation",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DeviationAudit_EntityId",
            //    table: "DeviationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DeviceConfigAudit_EntityId",
            //    table: "DeviceConfigAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ElectronicSignature_UserId",
            //    table: "ElectronicSignature",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ElectronicSignatureItem_ElectronicSignatureId",
            //    table: "ElectronicSignatureItem",
            //    column: "ElectronicSignatureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ElectronicSignatureItem_TableName_PrimaryKey",
            //    table: "ElectronicSignatureItem",
            //    columns: new[] { "TableName", "PrimaryKey" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_EMail_UserId",
            //    table: "EMail",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EMailAttachment_EMailId",
            //    table: "EMailAttachment",
            //    column: "EMailId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EnvironmentDefAudit_EntityId",
            //    table: "EnvironmentDefAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_DeviceConfigId",
            //    table: "Equipment",
            //    column: "DeviceConfigId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_EquipmentTypeId",
            //    table: "Equipment",
            //    column: "EquipmentTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_LocationId",
            //    table: "Equipment",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_Name",
            //    table: "Equipment",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_SerialNumber",
            //    table: "Equipment",
            //    column: "SerialNumber");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Equipment_UnitOfMeasureId",
            //    table: "Equipment",
            //    column: "UnitOfMeasureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EquipmentAudit_EntityId",
            //    table: "EquipmentAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EquipmentType_Description",
            //    table: "EquipmentType",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_EquipmentTypeAudit_EntityId",
            //    table: "EquipmentTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ESignConfig_Category",
            //    table: "ESignConfig",
            //    column: "Category");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ESignConfig_Category_IsActive",
            //    table: "ESignConfig",
            //    columns: new[] { "Category", "IsActive" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ESignConfigAudit_EntityId",
            //    table: "ESignConfigAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileAttachment_CurrentWorkSpaceId",
            //    table: "FileAttachment",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileAttachment_SampleId",
            //    table: "FileAttachment",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileAttachmentAudit_EntityId",
            //    table: "FileAttachmentAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_GroupAudit_EntityId",
            //    table: "GroupAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_GrowthPromotionStatus_StatusCode",
            //    table: "GrowthPromotionStatus",
            //    column: "StatusCode");

            //migrationBuilder.CreateIndex(
            //    name: "IX_GrowthPromotionStatusAudit_EntityId",
            //    table: "GrowthPromotionStatusAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_Id_LimitTypeId",
            //    table: "Limit",
            //    columns: new[] { "Id", "LimitTypeId" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_LimitDefId",
            //    table: "Limit",
            //    column: "LimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_LimitExceededId",
            //    table: "Limit",
            //    column: "LimitExceededId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_LimitTypeId",
            //    table: "Limit",
            //    column: "LimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_ParentLimitId",
            //    table: "Limit",
            //    column: "ParentLimitId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_SiteId",
            //    table: "Limit",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_SourceLimitDefId",
            //    table: "Limit",
            //    column: "SourceLimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_SourceLimitTypeId",
            //    table: "Limit",
            //    column: "SourceLimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Limit_TestId",
            //    table: "Limit",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitAudit_EntityId",
            //    table: "LimitAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitDef_LimitTypeId",
            //    table: "LimitDef",
            //    column: "LimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitDef_SourceLimitDefId",
            //    table: "LimitDef",
            //    column: "SourceLimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitDef_SourceLimitTypeId",
            //    table: "LimitDef",
            //    column: "SourceLimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitDef_TestTypeId",
            //    table: "LimitDef",
            //    column: "TestTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitDefAudit_EntityId",
            //    table: "LimitDefAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitExceeded_FrequencyLimitId",
            //    table: "LimitExceeded",
            //    column: "FrequencyLimitId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitExceeded_LimitId",
            //    table: "LimitExceeded",
            //    column: "LimitId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitExceeded_LimitTypeId",
            //    table: "LimitExceeded",
            //    column: "LimitTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitExceeded_SampleId",
            //    table: "LimitExceeded",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitExceededAudit_EntityId",
            //    table: "LimitExceededAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRule_LimitRuleGroupId",
            //    table: "LimitRule",
            //    column: "LimitRuleGroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRule_LimitTokenId",
            //    table: "LimitRule",
            //    column: "LimitTokenId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRule_MeasurementId",
            //    table: "LimitRule",
            //    column: "MeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRule_SignAndId",
            //    table: "LimitRule",
            //    column: "SignAndId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRule_SignId",
            //    table: "LimitRule",
            //    column: "SignId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRuleAudit_EntityId",
            //    table: "LimitRuleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRuleGroup_LimitDefId",
            //    table: "LimitRuleGroup",
            //    column: "LimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRuleGroup_LimitId",
            //    table: "LimitRuleGroup",
            //    column: "LimitId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRuleGroup_ParentId",
            //    table: "LimitRuleGroup",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitRuleGroupAudit_EntityId",
            //    table: "LimitRuleGroupAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitTokenAudit_EntityId",
            //    table: "LimitTokenAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitTokenSign_SignId",
            //    table: "LimitTokenSign",
            //    column: "SignId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LimitTypeAudit_EntityId",
            //    table: "LimitTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_ClassificationId",
            //    table: "Location",
            //    column: "ClassificationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_Id_ParentId",
            //    table: "Location",
            //    columns: new[] { "Id", "ParentId" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_LocationTypeId",
            //    table: "Location",
            //    column: "LocationTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_Name",
            //    table: "Location",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_ParentId",
            //    table: "Location",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_SecondClassificationId",
            //    table: "Location",
            //    column: "SecondClassificationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Location_VisioDiagramId",
            //    table: "Location",
            //    column: "VisioDiagramId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LocationAudit_EntityId",
            //    table: "LocationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LocationExtra_LocationId",
            //    table: "LocationExtra",
            //    column: "LocationId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_LocationType_Name",
            //    table: "LocationType",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LocationType_Name_Description_IsActive",
            //    table: "LocationType",
            //    columns: new[] { "Name", "Description", "IsActive" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_LocationTypeAudit_EntityId",
            //    table: "LocationTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Map_MapCategoryId",
            //    table: "Map",
            //    column: "MapCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MapAudit_EntityId",
            //    table: "MapAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MapCategory_ParentId",
            //    table: "MapCategory",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MapCategoryAudit_EntityId",
            //    table: "MapCategoryAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Measurement_ChildMeasurementId",
            //    table: "Measurement",
            //    column: "ChildMeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Measurement_DataTypeId",
            //    table: "Measurement",
            //    column: "DataTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Measurement_TestTypeId",
            //    table: "Measurement",
            //    column: "TestTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Measurement_TimeFrameDilutionId",
            //    table: "Measurement",
            //    column: "TimeFrameDilutionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Measurement_TimeFrameId_Name",
            //    table: "Measurement",
            //    columns: new[] { "TimeFrameId", "Name" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementAudit_EntityId",
            //    table: "MeasurementAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementCycle_CycleId",
            //    table: "MeasurementCycle",
            //    column: "CycleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementCycle_MeasurementId_CycleId",
            //    table: "MeasurementCycle",
            //    columns: new[] { "MeasurementId", "CycleId" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementCycleAudit_EntityId",
            //    table: "MeasurementCycleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementListDef_MeasurementId",
            //    table: "MeasurementListDef",
            //    column: "MeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementListDefAudit_EntityId",
            //    table: "MeasurementListDefAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementSign_MeasurementId",
            //    table: "MeasurementSign",
            //    column: "MeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementSign_SignId",
            //    table: "MeasurementSign",
            //    column: "SignId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementSignAudit_EntityId",
            //    table: "MeasurementSignAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementUOM_MeasurementId",
            //    table: "MeasurementUOM",
            //    column: "MeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementUOM_UnitOfMeasureId",
            //    table: "MeasurementUOM",
            //    column: "UnitOfMeasureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MeasurementUOMAudit_EntityId",
            //    table: "MeasurementUOMAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_GrowthPromotionStatusId",
            //    table: "Media",
            //    column: "GrowthPromotionStatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_LocationId",
            //    table: "Media",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_MediaTypeId",
            //    table: "Media",
            //    column: "MediaTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_Name",
            //    table: "Media",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MediaAudit_EntityId",
            //    table: "MediaAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MediaType_Name_Description",
            //    table: "MediaType",
            //    columns: new[] { "Name", "Description" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_MediaTypeAudit_EntityId",
            //    table: "MediaTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_DeviationId",
            //    table: "Notification",
            //    column: "DeviationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_EquipmentId",
            //    table: "Notification",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_MediaId",
            //    table: "Notification",
            //    column: "MediaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_NotificationTypeId",
            //    table: "Notification",
            //    column: "NotificationTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_SampleId",
            //    table: "Notification",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_SiteId",
            //    table: "Notification",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notification_UserId",
            //    table: "Notification",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NotificationAudit_EntityId",
            //    table: "NotificationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NotificationEMail_EMailId",
            //    table: "NotificationEMail",
            //    column: "EMailId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NotificationEMail_NotificationId",
            //    table: "NotificationEMail",
            //    column: "NotificationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NotificationFrequencyAudit_EntityId",
            //    table: "NotificationFrequencyAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_NotificationTypeAudit_EntityId",
            //    table: "NotificationTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Organism_OrganismGenusId",
            //    table: "Organism",
            //    column: "OrganismGenusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Organism_OrganismSeverityId",
            //    table: "Organism",
            //    column: "OrganismSeverityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Organism_Species",
            //    table: "Organism",
            //    column: "Species");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismAudit_EntityId",
            //    table: "OrganismAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismChar_Name",
            //    table: "OrganismChar",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismChar_OrganismTypeId",
            //    table: "OrganismChar",
            //    column: "OrganismTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismCharAudit_EntityId",
            //    table: "OrganismCharAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismGenus_Description",
            //    table: "OrganismGenus",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismGenus_OrganismTypeId",
            //    table: "OrganismGenus",
            //    column: "OrganismTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismGenusAudit_EntityId",
            //    table: "OrganismGenusAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismSeverity_Description",
            //    table: "OrganismSeverity",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismSeverityAudit_EntityId",
            //    table: "OrganismSeverityAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismType_Description",
            //    table: "OrganismType",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrganismTypeAudit_EntityId",
            //    table: "OrganismTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrgFoundMethod_Name",
            //    table: "OrgFoundMethod",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrgFoundMethodAudit_EntityId",
            //    table: "OrgFoundMethodAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ParticleCounterData_EquipmentId",
            //    table: "ParticleCounterData",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ParticleCounterData_SampleId",
            //    table: "ParticleCounterData",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ParticleCounterData_SiteId",
            //    table: "ParticleCounterData",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ParticleCounterDataAudit_EntityId",
            //    table: "ParticleCounterDataAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Permission_Code",
            //    table: "Permission",
            //    column: "Code");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Permission_ParentId",
            //    table: "Permission",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PersonnelSite_TestTypeId",
            //    table: "PersonnelSite",
            //    column: "TestTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PersonnelSiteAudit_EntityId",
            //    table: "PersonnelSiteAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Plan_AutoFillFrequencyTypeId",
            //    table: "Plan",
            //    column: "AutoFillFrequencyTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Plan_EnvironmentId",
            //    table: "Plan",
            //    column: "EnvironmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Plan_GroupId",
            //    table: "Plan",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Plan_LocationId",
            //    table: "Plan",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Plan_PlanGroupId",
            //    table: "Plan",
            //    column: "PlanGroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PlanAudit_EntityId",
            //    table: "PlanAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PlanGroupAudit_EntityId",
            //    table: "PlanGroupAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PreDefinedNoteAudit_EntityId",
            //    table: "PreDefinedNoteAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_LocationId",
            //    table: "Product",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_Name",
            //    table: "Product",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProductAudit_EntityId",
            //    table: "ProductAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_MeasurementId",
            //    table: "Reading",
            //    column: "MeasurementId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_OrganismId",
            //    table: "Reading",
            //    column: "OrganismId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_ParentSampleId",
            //    table: "Reading",
            //    column: "ParentSampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_SampleId",
            //    table: "Reading",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_SignId",
            //    table: "Reading",
            //    column: "SignId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_TestId",
            //    table: "Reading",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reading_UnitOfMeasureId",
            //    table: "Reading",
            //    column: "UnitOfMeasureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReadingAudit_EntityId",
            //    table: "ReadingAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Role_Name",
            //    table: "Role",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_RoleAudit_EntityId",
            //    table: "RoleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RolePermission_PermissionId",
            //    table: "RolePermission",
            //    column: "PermissionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RolePermission_RoleId",
            //    table: "RolePermission",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RolePermissionAudit_EntityId",
            //    table: "RolePermissionAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RptSampleMart_EnvironmentId",
            //    table: "RptSampleMart",
            //    column: "EnvironmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RptSampleMart_SiteId",
            //    table: "RptSampleMart",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RptSampleMartAudit_EntityId",
            //    table: "RptSampleMartAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_ApprovedUserId",
            //    table: "Sample",
            //    column: "ApprovedUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_DeviationId",
            //    table: "Sample",
            //    column: "DeviationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_EnvironmentId",
            //    table: "Sample",
            //    column: "EnvironmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_EquipmentId",
            //    table: "Sample",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_Id_ParentSampleId",
            //    table: "Sample",
            //    columns: new[] { "Id", "ParentSampleId" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_MediaId",
            //    table: "Sample",
            //    column: "MediaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_ParentSampleId",
            //    table: "Sample",
            //    column: "ParentSampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_PerformedUserId",
            //    table: "Sample",
            //    column: "PerformedUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_PersonnelSiteId",
            //    table: "Sample",
            //    column: "PersonnelSiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_PersonnelUserId",
            //    table: "Sample",
            //    column: "PersonnelUserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sample_TestId",
            //    table: "Sample",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SampleAudit_EntityId",
            //    table: "SampleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SampleReview_SampleId",
            //    table: "SampleReview",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SampleReview_UserId",
            //    table: "SampleReview",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SampleReviewAudit_EntityId",
            //    table: "SampleReviewAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SeverityLocation_LocationId",
            //    table: "SeverityLocation",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SeverityLocation_OrganismId",
            //    table: "SeverityLocation",
            //    column: "OrganismId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SeverityLocation_OrganismSeverityId",
            //    table: "SeverityLocation",
            //    column: "OrganismSeverityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SeverityLocationAudit_EntityId",
            //    table: "SeverityLocationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Sign_Description",
            //    table: "Sign",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SignAudit_EntityId",
            //    table: "SignAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_ClassificationId",
            //    table: "Site",
            //    column: "ClassificationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_IsActive_LocationId_Id",
            //    table: "Site",
            //    columns: new[] { "IsActive", "LocationId", "Id" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_LocationId",
            //    table: "Site",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_LocationId_Id_Name",
            //    table: "Site",
            //    columns: new[] { "LocationId", "Id", "Name" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_SecondClassificationId",
            //    table: "Site",
            //    column: "SecondClassificationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Site_SiteTypeId",
            //    table: "Site",
            //    column: "SiteTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SiteAudit_EntityId",
            //    table: "SiteAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SiteType_Name",
            //    table: "SiteType",
            //    column: "Name");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SiteTypeAudit_EntityId",
            //    table: "SiteTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Subscription_LocationId",
            //    table: "Subscription",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Subscription_NotificationTypeId",
            //    table: "Subscription",
            //    column: "NotificationTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Subscription_UserId",
            //    table: "Subscription",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubscriptionAudit_EntityId",
            //    table: "SubscriptionAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubWorkflowTemplateMapping_ChindId",
            //    table: "SubWorkflowTemplateMapping",
            //    column: "ChindId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubWorkflowTemplateMapping_ParentId",
            //    table: "SubWorkflowTemplateMapping",
            //    column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_SubWorkflowTemplateMappingAudit_EntityId",
            //    table: "SubWorkflowTemplateMappingAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TableNote_UserId",
            //    table: "TableNote",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TableNoteAudit_EntityId",
            //    table: "TableNoteAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_EnvironmentId",
            //    table: "Test",
            //    column: "EnvironmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_GroupId",
            //    table: "Test",
            //    column: "GroupId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_NotificationFrequencyId",
            //    table: "Test",
            //    column: "NotificationFrequencyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_ParentTestId",
            //    table: "Test",
            //    column: "ParentTestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_PlanId",
            //    table: "Test",
            //    column: "PlanId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_SiteId",
            //    table: "Test",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_TestFrequencyId",
            //    table: "Test",
            //    column: "TestFrequencyId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_TestFrequencyOccurrenceId",
            //    table: "Test",
            //    column: "TestFrequencyOccurrenceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_TestFrequencyTypeId",
            //    table: "Test",
            //    column: "TestFrequencyTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_TestTypeId",
            //    table: "Test",
            //    column: "TestTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Test_TimeFrameDilutionId",
            //    table: "Test",
            //    column: "TimeFrameDilutionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestAudit_EntityId",
            //    table: "TestAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestCategory_Description_Id",
            //    table: "TestCategory",
            //    columns: new[] { "Description", "Id" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestCategory_TestClassId",
            //    table: "TestCategory",
            //    column: "TestClassId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestCategoryAudit_EntityId",
            //    table: "TestCategoryAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestClassAudit_EntityId",
            //    table: "TestClassAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestFrequency_TestFrequencyOccurrenceId",
            //    table: "TestFrequency",
            //    column: "TestFrequencyOccurrenceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestFrequency_TestFrequencyTypeId",
            //    table: "TestFrequency",
            //    column: "TestFrequencyTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestFrequencyAudit_EntityId",
            //    table: "TestFrequencyAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestFrequencyOccurrenceAudit_EntityId",
            //    table: "TestFrequencyOccurrenceAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestFrequencyTypeAudit_EntityId",
            //    table: "TestFrequencyTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestStageAudit_EntityId",
            //    table: "TestStageAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestType_LocationId",
            //    table: "TestType",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestType_ProdSelectionTimeFrameId",
            //    table: "TestType",
            //    column: "ProdSelectionTimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestType_TestCategoryId",
            //    table: "TestType",
            //    column: "TestCategoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestType_TestTypeCodeId",
            //    table: "TestType",
            //    column: "TestTypeCodeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestType_TestTypeLabelId",
            //    table: "TestType",
            //    column: "TestTypeLabelId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeAudit_EntityId",
            //    table: "TestTypeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeCodeAudit_EntityId",
            //    table: "TestTypeCodeAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeEquipment_EquipmentTypeId",
            //    table: "TestTypeEquipment",
            //    column: "EquipmentTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeEquipment_TimeFrameId",
            //    table: "TestTypeEquipment",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeEquipmentAudit_EntityId",
            //    table: "TestTypeEquipmentAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeLabelAudit_EntityId",
            //    table: "TestTypeLabelAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeMedia_MediaTypeId",
            //    table: "TestTypeMedia",
            //    column: "MediaTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeMedia_TimeFrameId",
            //    table: "TestTypeMedia",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TestTypeMediaAudit_EntityId",
            //    table: "TestTypeMediaAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrame_ClonedTimeFrameId",
            //    table: "TimeFrame",
            //    column: "ClonedTimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrame_TestStageId_TestTypeId_Sequence_MinTime_MaxTime",
            //    table: "TimeFrame",
            //    columns: new[] { "TestStageId", "TestTypeId", "Sequence", "MinTime", "MaxTime" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrame_TestTypeId_Sequence",
            //    table: "TimeFrame",
            //    columns: new[] { "TestTypeId", "Sequence" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameAudit_EntityId",
            //    table: "TimeFrameAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameDilution_TestTypeId",
            //    table: "TimeFrameDilution",
            //    column: "TestTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameDilution_TimeFrameId",
            //    table: "TimeFrameDilution",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameDilution_UnitOfMeasureId",
            //    table: "TimeFrameDilution",
            //    column: "UnitOfMeasureId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameDilutionAudit_EntityId",
            //    table: "TimeFrameDilutionAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameEvent_LimitDefId",
            //    table: "TimeFrameEvent",
            //    column: "LimitDefId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameEvent_NextTimeFrameId",
            //    table: "TimeFrameEvent",
            //    column: "NextTimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameEvent_TimeFrameId",
            //    table: "TimeFrameEvent",
            //    column: "TimeFrameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TimeFrameEventAudit_EntityId",
            //    table: "TimeFrameEventAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitOfMeasure_Description",
            //    table: "UnitOfMeasure",
            //    column: "Description");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitOfMeasureAudit_EntityId",
            //    table: "UnitOfMeasureAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_Account",
            //    table: "User",
            //    column: "Account",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_DepartmentId",
            //    table: "User",
            //    column: "DepartmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_LocationId",
            //    table: "User",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserAudit_EntityId",
            //    table: "UserAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserHistory_UserId",
            //    table: "UserHistory",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserPasswordHistory_UserId",
            //    table: "UserPasswordHistory",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserProfile_UserId",
            //    table: "UserProfile",
            //    column: "UserId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRole_RoleId",
            //    table: "UserRole",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRole_UserId",
            //    table: "UserRole",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoleAudit_EntityId",
            //    table: "UserRoleAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioDiagram_MapId",
            //    table: "VisioDiagram",
            //    column: "MapId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioDiagramAudit_EntityId",
            //    table: "VisioDiagramAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioLocation_LocationId",
            //    table: "VisioLocation",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioLocation_VisioDiagramId",
            //    table: "VisioLocation",
            //    column: "VisioDiagramId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioLocationAudit_EntityId",
            //    table: "VisioLocationAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioSite_SiteId",
            //    table: "VisioSite",
            //    column: "SiteId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioSite_VisioDiagramId",
            //    table: "VisioSite",
            //    column: "VisioDiagramId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_VisioSiteAudit_EntityId",
            //    table: "VisioSiteAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkflowParameter_WorkflowInstanceId",
            //    table: "WorkflowParameter",
            //    column: "WorkflowInstanceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkflowTemplate_WorkflowId",
            //    table: "WorkflowTemplate",
            //    column: "WorkflowId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkflowTemplateAudit_EntityId",
            //    table: "WorkflowTemplateAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceCache_CurrentWorkSpaceId",
            //    table: "WorkSpaceCache",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceCache_LocationId",
            //    table: "WorkSpaceCache",
            //    column: "LocationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceCache_SampleId",
            //    table: "WorkSpaceCache",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceCache_TestId",
            //    table: "WorkSpaceCache",
            //    column: "TestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceEquipment_CurrentWorkSpaceId",
            //    table: "WorkSpaceEquipment",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceEquipment_EquipmentId",
            //    table: "WorkSpaceEquipment",
            //    column: "EquipmentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceEquipmentAudit_EntityId",
            //    table: "WorkSpaceEquipmentAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceMedia_CurrentWorkSpaceId",
            //    table: "WorkSpaceMedia",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceMedia_MediaId",
            //    table: "WorkSpaceMedia",
            //    column: "MediaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceMediaAudit_EntityId",
            //    table: "WorkSpaceMediaAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceOrganism_CurrentWorkSpaceId",
            //    table: "WorkSpaceOrganism",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceOrganism_OrganismId",
            //    table: "WorkSpaceOrganism",
            //    column: "OrganismId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceOrganism_SampleId",
            //    table: "WorkSpaceOrganism",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceOrganismAudit_EntityId",
            //    table: "WorkSpaceOrganismAudit",
            //    column: "EntityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceProduct_BatchId",
            //    table: "WorkSpaceProduct",
            //    column: "BatchId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceProduct_CurrentWorkSpaceId",
            //    table: "WorkSpaceProduct",
            //    column: "CurrentWorkSpaceId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceProduct_ProductId",
            //    table: "WorkSpaceProduct",
            //    column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceProduct_SampleId",
            //    table: "WorkSpaceProduct",
            //    column: "SampleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WorkSpaceProductAudit_EntityId",
            //    table: "WorkSpaceProductAudit",
            //    column: "EntityId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Batch_Product_ProductId",
            //    table: "Batch",
            //    column: "ProductId",
            //    principalTable: "Product",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CompareDataRemark_ParticleCounterData_ParticleCounterDataId",
            //    table: "CompareDataRemark",
            //    column: "ParticleCounterDataId",
            //    principalTable: "ParticleCounterData",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CompareDataRemark_User_UserId",
            //    table: "CompareDataRemark",
            //    column: "UserId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_Location_LocationId",
            //    table: "CurrentWorkSpace",
            //    column: "LocationId",
            //    principalTable: "Location",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_Plan_PlanId",
            //    table: "CurrentWorkSpace",
            //    column: "PlanId",
            //    principalTable: "Plan",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_Sample_SampleId",
            //    table: "CurrentWorkSpace",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_Test_TestId",
            //    table: "CurrentWorkSpace",
            //    column: "TestId",
            //    principalTable: "Test",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_TimeFrame_TimeFrameId",
            //    table: "CurrentWorkSpace",
            //    column: "TimeFrameId",
            //    principalTable: "TimeFrame",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_User_ExecuteUserId",
            //    table: "CurrentWorkSpace",
            //    column: "ExecuteUserId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_User_LastUserId",
            //    table: "CurrentWorkSpace",
            //    column: "LastUserId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_User_PerformedUserId",
            //    table: "CurrentWorkSpace",
            //    column: "PerformedUserId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CurrentWorkSpace_User_UserId",
            //    table: "CurrentWorkSpace",
            //    column: "UserId",
            //    principalTable: "User",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Cycle_TimeFrame_TimeFrameId",
            //    table: "Cycle",
            //    column: "TimeFrameId",
            //    principalTable: "TimeFrame",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Department_User_ManagerId",
            //    table: "Department",
            //    column: "ManagerId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Deviation_LimitDef_LimitDefId",
            //    table: "Deviation",
            //    column: "LimitDefId",
            //    principalTable: "LimitDef",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Deviation_Sample_SampleId",
            //    table: "Deviation",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Deviation_Test_TestId",
            //    table: "Deviation",
            //    column: "TestId",
            //    principalTable: "Test",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ElectronicSignature_User_UserId",
            //    table: "ElectronicSignature",
            //    column: "UserId",
            //    principalTable: "User",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_EMail_User_UserId",
            //    table: "EMail",
            //    column: "UserId",
            //    principalTable: "User",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Equipment_Location_LocationId",
            //    table: "Equipment",
            //    column: "LocationId",
            //    principalTable: "Location",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FileAttachment_Sample_SampleId",
            //    table: "FileAttachment",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Limit_LimitDef_LimitDefId",
            //    table: "Limit",
            //    column: "LimitDefId",
            //    principalTable: "LimitDef",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Limit_LimitDef_SourceLimitDefId",
            //    table: "Limit",
            //    column: "SourceLimitDefId",
            //    principalTable: "LimitDef",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Limit_LimitExceeded_LimitExceededId",
            //    table: "Limit",
            //    column: "LimitExceededId",
            //    principalTable: "LimitExceeded",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Limit_Site_SiteId",
            //    table: "Limit",
            //    column: "SiteId",
            //    principalTable: "Site",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Limit_Test_TestId",
            //    table: "Limit",
            //    column: "TestId",
            //    principalTable: "Test",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimitDef_TestType_TestTypeId",
            //    table: "LimitDef",
            //    column: "TestTypeId",
            //    principalTable: "TestType",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimitExceeded_Sample_SampleId",
            //    table: "LimitExceeded",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_LimitRule_Measurement_MeasurementId",
            //    table: "LimitRule",
            //    column: "MeasurementId",
            //    principalTable: "Measurement",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Location_VisioDiagram_VisioDiagramId",
            //    table: "Location",
            //    column: "VisioDiagramId",
            //    principalTable: "VisioDiagram",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Map_MapCategory_MapCategoryId",
            //    table: "Map",
            //    column: "MapCategoryId",
            //    principalTable: "MapCategory",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Measurement_TestType_TestTypeId",
            //    table: "Measurement",
            //    column: "TestTypeId",
            //    principalTable: "TestType",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Measurement_TimeFrame_TimeFrameId",
            //    table: "Measurement",
            //    column: "TimeFrameId",
            //    principalTable: "TimeFrame",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Measurement_TimeFrameDilution_TimeFrameDilutionId",
            //    table: "Measurement",
            //    column: "TimeFrameDilutionId",
            //    principalTable: "TimeFrameDilution",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Notification_Sample_SampleId",
            //    table: "Notification",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.SetNull);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ParticleCounterData_Sample_SampleId",
            //    table: "ParticleCounterData",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PersonnelSite_TestType_TestTypeId",
            //    table: "PersonnelSite",
            //    column: "TestTypeId",
            //    principalTable: "TestType",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Reading_Sample_ParentSampleId",
            //    table: "Reading",
            //    column: "ParentSampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Reading_Sample_SampleId",
            //    table: "Reading",
            //    column: "SampleId",
            //    principalTable: "Sample",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Reading_Test_TestId",
            //    table: "Reading",
            //    column: "TestId",
            //    principalTable: "Test",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Sample_Test_TestId",
            //    table: "Sample",
            //    column: "TestId",
            //    principalTable: "Test",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Test_TestType_TestTypeId",
            //    table: "Test",
            //    column: "TestTypeId",
            //    principalTable: "TestType",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Test_TimeFrameDilution_TimeFrameDilutionId",
            //    table: "Test",
            //    column: "TimeFrameDilutionId",
            //    principalTable: "TimeFrameDilution",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TestType_TimeFrame_ProdSelectionTimeFrameId",
            //    table: "TestType",
            //    column: "ProdSelectionTimeFrameId",
            //    principalTable: "TimeFrame",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_Product_ProductId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_User_ExecuteUserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_User_LastUserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_User_PerformedUserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_User_UserId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_ManagerId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Sample_User_ApprovedUserId",
                table: "Sample");

            migrationBuilder.DropForeignKey(
                name: "FK_Sample_User_PerformedUserId",
                table: "Sample");

            migrationBuilder.DropForeignKey(
                name: "FK_Sample_User_PersonnelUserId",
                table: "Sample");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_Batch_BatchId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_Group_GroupId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Group_GroupId",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Group_GroupId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_Location_LocationId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Location_LocationId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Location_LocationId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Location_LocationId",
                table: "Plan");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Location_LocationId",
                table: "Site");

            migrationBuilder.DropForeignKey(
                name: "FK_TestType_Location_LocationId",
                table: "TestType");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_Plan_PlanId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Plan_PlanId",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentWorkSpace_Sample_SampleId",
                table: "CurrentWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_Deviation_Sample_SampleId",
                table: "Deviation");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitExceeded_Sample_SampleId",
                table: "LimitExceeded");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_Test_TestId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeFrame_TestStage_TestStageId",
                table: "TimeFrame");

            migrationBuilder.DropForeignKey(
                name: "FK_TestType_TimeFrame_ProdSelectionTimeFrameId",
                table: "TestType");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_LimitDef_LimitDefId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_LimitDef_SourceLimitDefId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_LimitType_LimitTypeId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_LimitType_SourceLimitTypeId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitExceeded_LimitType_LimitTypeId",
                table: "LimitExceeded");

            migrationBuilder.DropForeignKey(
                name: "FK_Limit_LimitExceeded_LimitExceededId",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_Map_MapCategory_MapCategoryId",
                table: "Map");

            migrationBuilder.DropTable(
                name: "ApprovalItem");

            migrationBuilder.DropTable(
                name: "ApprovalStep");

            migrationBuilder.DropTable(
                name: "ApprovalStepAudit");

            migrationBuilder.DropTable(
                name: "BatchAudit");

            migrationBuilder.DropTable(
                name: "ClassificationAudit");

            migrationBuilder.DropTable(
                name: "CompareDataRemark");

            migrationBuilder.DropTable(
                name: "CompareDataRemarkAudit");

            migrationBuilder.DropTable(
                name: "ConfigItem");

            migrationBuilder.DropTable(
                name: "ConfigItemAudit");

            migrationBuilder.DropTable(
                name: "CurrentWorkSpaceAudit");

            migrationBuilder.DropTable(
                name: "CycleAudit");

            migrationBuilder.DropTable(
                name: "DataTypeAudit");

            migrationBuilder.DropTable(
                name: "DataTypeSign");

            migrationBuilder.DropTable(
                name: "DepartmentAudit");

            migrationBuilder.DropTable(
                name: "DeviationAudit");

            migrationBuilder.DropTable(
                name: "DeviceConfigAudit");

            migrationBuilder.DropTable(
                name: "ElectronicSignatureItem");

            migrationBuilder.DropTable(
                name: "EMailAttachment");

            migrationBuilder.DropTable(
                name: "EnvironmentDefAudit");

            migrationBuilder.DropTable(
                name: "EquipmentAudit");

            migrationBuilder.DropTable(
                name: "EquipmentTypeAudit");

            migrationBuilder.DropTable(
                name: "ESignConfig");

            migrationBuilder.DropTable(
                name: "ESignConfigAudit");

            migrationBuilder.DropTable(
                name: "FileAttachment");

            migrationBuilder.DropTable(
                name: "FileAttachmentAudit");

            migrationBuilder.DropTable(
                name: "GroupAudit");

            migrationBuilder.DropTable(
                name: "GrowthPromotionStatusAudit");

            migrationBuilder.DropTable(
                name: "LimitAudit");

            migrationBuilder.DropTable(
                name: "LimitDefAudit");

            migrationBuilder.DropTable(
                name: "LimitExceededAudit");

            migrationBuilder.DropTable(
                name: "LimitRule");

            migrationBuilder.DropTable(
                name: "LimitRuleAudit");

            migrationBuilder.DropTable(
                name: "LimitRuleGroupAudit");

            migrationBuilder.DropTable(
                name: "LimitTokenAudit");

            migrationBuilder.DropTable(
                name: "LimitTokenSign");

            migrationBuilder.DropTable(
                name: "LimitTypeAudit");

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
                name: "MeasurementAudit");

            migrationBuilder.DropTable(
                name: "MeasurementCycle");

            migrationBuilder.DropTable(
                name: "MeasurementCycleAudit");

            migrationBuilder.DropTable(
                name: "MeasurementListDef");

            migrationBuilder.DropTable(
                name: "MeasurementListDefAudit");

            migrationBuilder.DropTable(
                name: "MeasurementSign");

            migrationBuilder.DropTable(
                name: "MeasurementSignAudit");

            migrationBuilder.DropTable(
                name: "MeasurementUOM");

            migrationBuilder.DropTable(
                name: "MeasurementUOMAudit");

            migrationBuilder.DropTable(
                name: "MediaAudit");

            migrationBuilder.DropTable(
                name: "MediaTypeAudit");

            migrationBuilder.DropTable(
                name: "NotificationAudit");

            migrationBuilder.DropTable(
                name: "NotificationEMail");

            migrationBuilder.DropTable(
                name: "NotificationFrequencyAudit");

            migrationBuilder.DropTable(
                name: "NotificationTypeAudit");

            migrationBuilder.DropTable(
                name: "OrganismAudit");

            migrationBuilder.DropTable(
                name: "OrganismChar");

            migrationBuilder.DropTable(
                name: "OrganismCharAudit");

            migrationBuilder.DropTable(
                name: "OrganismGenusAudit");

            migrationBuilder.DropTable(
                name: "OrganismSeverityAudit");

            migrationBuilder.DropTable(
                name: "OrganismTypeAudit");

            migrationBuilder.DropTable(
                name: "OrgFoundMethod");

            migrationBuilder.DropTable(
                name: "OrgFoundMethodAudit");

            migrationBuilder.DropTable(
                name: "ParticleCounterDataAudit");

            migrationBuilder.DropTable(
                name: "PersonnelSiteAudit");

            migrationBuilder.DropTable(
                name: "PlanAudit");

            migrationBuilder.DropTable(
                name: "PlanGroupAudit");

            migrationBuilder.DropTable(
                name: "PreDefinedNote");

            migrationBuilder.DropTable(
                name: "PreDefinedNoteAudit");

            migrationBuilder.DropTable(
                name: "ProductAudit");

            migrationBuilder.DropTable(
                name: "Reading");

            migrationBuilder.DropTable(
                name: "ReadingAudit");

            migrationBuilder.DropTable(
                name: "RoleAudit");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "RolePermissionAudit");

            migrationBuilder.DropTable(
                name: "RptSampleMart");

            migrationBuilder.DropTable(
                name: "RptSampleMartAudit");

            migrationBuilder.DropTable(
                name: "SampleAudit");

            migrationBuilder.DropTable(
                name: "SampleReview");

            migrationBuilder.DropTable(
                name: "SampleReviewAudit");

            migrationBuilder.DropTable(
                name: "SeverityLocation");

            migrationBuilder.DropTable(
                name: "SeverityLocationAudit");

            migrationBuilder.DropTable(
                name: "SignAudit");

            migrationBuilder.DropTable(
                name: "SiteAudit");

            migrationBuilder.DropTable(
                name: "SiteTypeAudit");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "SubscriptionAudit");

            migrationBuilder.DropTable(
                name: "SubWorkflowTemplateMapping");

            migrationBuilder.DropTable(
                name: "SubWorkflowTemplateMappingAudit");

            migrationBuilder.DropTable(
                name: "TableNote");

            migrationBuilder.DropTable(
                name: "TableNoteAudit");

            migrationBuilder.DropTable(
                name: "TestAudit");

            migrationBuilder.DropTable(
                name: "TestCategoryAudit");

            migrationBuilder.DropTable(
                name: "TestClassAudit");

            migrationBuilder.DropTable(
                name: "TestFrequencyAudit");

            migrationBuilder.DropTable(
                name: "TestFrequencyOccurrenceAudit");

            migrationBuilder.DropTable(
                name: "TestFrequencyTypeAudit");

            migrationBuilder.DropTable(
                name: "TestStageAudit");

            migrationBuilder.DropTable(
                name: "TestTypeAudit");

            migrationBuilder.DropTable(
                name: "TestTypeCodeAudit");

            migrationBuilder.DropTable(
                name: "TestTypeEquipment");

            migrationBuilder.DropTable(
                name: "TestTypeEquipmentAudit");

            migrationBuilder.DropTable(
                name: "TestTypeLabelAudit");

            migrationBuilder.DropTable(
                name: "TestTypeMedia");

            migrationBuilder.DropTable(
                name: "TestTypeMediaAudit");

            migrationBuilder.DropTable(
                name: "TimeFrameAudit");

            migrationBuilder.DropTable(
                name: "TimeFrameDilutionAudit");

            migrationBuilder.DropTable(
                name: "TimeFrameEvent");

            migrationBuilder.DropTable(
                name: "TimeFrameEventAudit");

            migrationBuilder.DropTable(
                name: "UnitOfMeasureAudit");

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
                name: "WorkflowParameter");

            migrationBuilder.DropTable(
                name: "WorkflowTemplateAudit");

            migrationBuilder.DropTable(
                name: "WorkSpaceCache");

            migrationBuilder.DropTable(
                name: "WorkSpaceEquipment");

            migrationBuilder.DropTable(
                name: "WorkSpaceEquipmentAudit");

            migrationBuilder.DropTable(
                name: "WorkSpaceMedia");

            migrationBuilder.DropTable(
                name: "WorkSpaceMediaAudit");

            migrationBuilder.DropTable(
                name: "WorkSpaceOrganism");

            migrationBuilder.DropTable(
                name: "WorkSpaceOrganismAudit");

            migrationBuilder.DropTable(
                name: "WorkSpaceProduct");

            migrationBuilder.DropTable(
                name: "WorkSpaceProductAudit");

            migrationBuilder.DropTable(
                name: "ApprovalEntity");

            migrationBuilder.DropTable(
                name: "ParticleCounterData");

            migrationBuilder.DropTable(
                name: "ElectronicSignature");

            migrationBuilder.DropTable(
                name: "LimitRuleGroup");

            migrationBuilder.DropTable(
                name: "LimitToken");

            migrationBuilder.DropTable(
                name: "Cycle");

            migrationBuilder.DropTable(
                name: "EMail");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "Sign");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "WorkflowTemplate");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Organism");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "DataType");

            migrationBuilder.DropTable(
                name: "OrganismGenus");

            migrationBuilder.DropTable(
                name: "OrganismSeverity");

            migrationBuilder.DropTable(
                name: "OrganismType");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Batch");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "LocationType");

            migrationBuilder.DropTable(
                name: "VisioDiagram");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "PlanGroup");

            migrationBuilder.DropTable(
                name: "Sample");

            migrationBuilder.DropTable(
                name: "Deviation");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "PersonnelSite");

            migrationBuilder.DropTable(
                name: "CurrentWorkSpace");

            migrationBuilder.DropTable(
                name: "DeviceConfig");

            migrationBuilder.DropTable(
                name: "EquipmentType");

            migrationBuilder.DropTable(
                name: "GrowthPromotionStatus");

            migrationBuilder.DropTable(
                name: "MediaType");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "EnvironmentDef");

            migrationBuilder.DropTable(
                name: "NotificationFrequency");

            migrationBuilder.DropTable(
                name: "TestFrequency");

            migrationBuilder.DropTable(
                name: "TimeFrameDilution");

            migrationBuilder.DropTable(
                name: "TestFrequencyOccurrence");

            migrationBuilder.DropTable(
                name: "TestFrequencyType");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropTable(
                name: "TestStage");

            migrationBuilder.DropTable(
                name: "TimeFrame");

            migrationBuilder.DropTable(
                name: "LimitDef");

            migrationBuilder.DropTable(
                name: "TestType");

            migrationBuilder.DropTable(
                name: "TestCategory");

            migrationBuilder.DropTable(
                name: "TestTypeCode");

            migrationBuilder.DropTable(
                name: "TestTypeLabel");

            migrationBuilder.DropTable(
                name: "TestClass");

            migrationBuilder.DropTable(
                name: "LimitType");

            migrationBuilder.DropTable(
                name: "LimitExceeded");

            migrationBuilder.DropTable(
                name: "Limit");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "Classification");

            migrationBuilder.DropTable(
                name: "SiteType");

            migrationBuilder.DropTable(
                name: "MapCategory");

            migrationBuilder.DropTable(
                name: "Map");
        }
    }
}
