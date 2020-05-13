using System;
using Microsoft.EntityFrameworkCore.Migrations;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "AppUpdates",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UpdateAddress = table.Column<string>(nullable: false),
            //            Vresion = table.Column<int>(nullable: false),
            //            Isforce = table.Column<bool>(nullable: false),
            //            UpdateMessage = table.Column<string>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AppUpdates", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetRoles",
            //        columns: table => new
            //        {
            //            Id = table.Column<string>(nullable: false),
            //            Name = table.Column<string>(maxLength: 256, nullable: true),
            //            NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
            //            ConcurrencyStamp = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "BankInputs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            BankNumber = table.Column<string>(nullable: false),
            //            OriginalAmount = table.Column<long>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_BankInputs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Blogs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: false),
            //            Img = table.Column<string>(nullable: false),
            //            ShortDesc = table.Column<string>(nullable: false),
            //            EDate = table.Column<DateTime>(nullable: true),
            //            SeenCount = table.Column<long>(nullable: false),
            //            Editor = table.Column<string>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Blogs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Category",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: false),
            //            Img = table.Column<string>(nullable: true),
            //            CategoryType = table.Column<int>(nullable: false),
            //            CategoryFinancialType = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Category", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ChatLogs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            ContextConnectionId = table.Column<string>(nullable: true),
            //            IsConnected = table.Column<bool>(nullable: false),
            //            IsReConnected = table.Column<bool>(nullable: false),
            //            IsDisconnected = table.Column<bool>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ChatLogs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Countries",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Countries", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "DeviceCodes",
            //        columns: table => new
            //        {
            //            UserCode = table.Column<string>(maxLength: 200, nullable: false),
            //            DeviceCode = table.Column<string>(maxLength: 200, nullable: false),
            //            SubjectId = table.Column<string>(maxLength: 200, nullable: true),
            //            ClientId = table.Column<string>(maxLength: 200, nullable: false),
            //            CreationTime = table.Column<DateTime>(nullable: false),
            //            Expiration = table.Column<DateTime>(nullable: false),
            //            Data = table.Column<string>(maxLength: 50000, nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "DividendAmountHistories",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            OriginalAmount = table.Column<long>(nullable: false),
            //            Month = table.Column<string>(nullable: true),
            //            Year = table.Column<string>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_DividendAmountHistories", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ExchangeInputs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            ExchangeNumber = table.Column<string>(nullable: false),
            //            OriginalAmount = table.Column<long>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ExchangeInputs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "FieldAndOrientations",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: false),
            //            ParentId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_FieldAndOrientations", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_FieldAndOrientations_FieldAndOrientations_ParentId",
            //                column: x => x.ParentId,
            //                principalTable: "FieldAndOrientations",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "GeneologyTypes",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: true),
            //            RowType = table.Column<int>(nullable: false),
            //            Type = table.Column<int>(nullable: false),
            //            CalculationTime = table.Column<int>(nullable: false),
            //            SystemType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_GeneologyTypes", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "InsuranceInputs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            InsuranceNumber = table.Column<string>(nullable: false),
            //            OriginalAmount = table.Column<long>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_InsuranceInputs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Levels",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true),
            //            Number = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Levels", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "NotifiLogs",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            NotifiId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false),
            //            Result = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_NotifiLogs", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PersistedGrants",
            //        columns: table => new
            //        {
            //            Key = table.Column<string>(maxLength: 200, nullable: false),
            //            Type = table.Column<string>(maxLength: 50, nullable: false),
            //            SubjectId = table.Column<string>(maxLength: 200, nullable: true),
            //            ClientId = table.Column<string>(maxLength: 200, nullable: false),
            //            CreationTime = table.Column<DateTime>(nullable: false),
            //            Expiration = table.Column<DateTime>(nullable: true),
            //            Data = table.Column<string>(maxLength: 50000, nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PersistedGrants", x => x.Key);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanBinaries",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            NumberOfSets = table.Column<int>(nullable: false),
            //            AmountBalanceBinary = table.Column<long>(nullable: false),
            //            AmountWageBinary = table.Column<long>(nullable: false),
            //            FrequencyOfPayments = table.Column<int>(nullable: false),
            //            FlashAmount = table.Column<long>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanBinaries", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanBreakAWays",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            NumberOfConditions = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanBreakAWays", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanUnis",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            MaxLevel = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanUnis", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PointTypes",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PointTypes", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Producters",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            FullName = table.Column<string>(nullable: false),
            //            Img = table.Column<string>(nullable: true),
            //            Description = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Producters", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Resources",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true),
            //            ResourcesType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Resources", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Seens",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            PostId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Seens", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ShopHomeSliders",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: false),
            //            Img = table.Column<string>(nullable: false),
            //            ImgSize = table.Column<long>(nullable: true),
            //            Order = table.Column<int>(nullable: true),
            //            producterType = table.Column<int>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            CUserId = table.Column<string>(nullable: true),
            //            EDate = table.Column<DateTime>(nullable: true),
            //            EUserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ShopHomeSliders", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UserPushTokens",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            Token = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UserPushTokens", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Wallets",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            Amount = table.Column<double>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            CUserId = table.Column<string>(nullable: false),
            //            EDate = table.Column<DateTime>(nullable: true),
            //            EUserId = table.Column<string>(nullable: true),
            //            RefId = table.Column<string>(nullable: true),
            //            Statue = table.Column<int>(nullable: false),
            //            TransactionType = table.Column<int>(nullable: false),
            //            BillId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Wallets", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetRoleClaims",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            RoleId = table.Column<string>(nullable: false),
            //            ClaimType = table.Column<string>(nullable: true),
            //            ClaimValue = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //                column: x => x.RoleId,
            //                principalTable: "AspNetRoles",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "BlogRelation",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            BlogId = table.Column<int>(nullable: false),
            //            BlogType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_BlogRelation", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_BlogRelation_Blogs_BlogId",
            //                column: x => x.BlogId,
            //                principalTable: "Blogs",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "BlogCategory",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            BlogId = table.Column<int>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_BlogCategory", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_BlogCategory_Blogs_BlogId",
            //                column: x => x.BlogId,
            //                principalTable: "Blogs",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_BlogCategory_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Category_Category",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Percent = table.Column<int>(nullable: false),
            //            Priority = table.Column<int>(nullable: false),
            //            ParentCatId = table.Column<int>(nullable: true),
            //            ChildrenCatId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Category_Category", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Category_Category_Category_ChildrenCatId",
            //                column: x => x.ChildrenCatId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Category_Category_Category_ParentCatId",
            //                column: x => x.ParentCatId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "CategoryLevels",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: false),
            //            Min = table.Column<int>(nullable: false),
            //            Max = table.Column<int>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_CategoryLevels", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_CategoryLevels_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "FinancialAdvices",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            AgeStart = table.Column<int>(nullable: true),
            //            AgeEnd = table.Column<int>(nullable: true),
            //            SolderStatusId = table.Column<int>(nullable: true),
            //            EducationalStatusId = table.Column<int>(nullable: true),
            //            HealthStatusId = table.Column<int>(nullable: true),
            //            JobStatusId = table.Column<int>(nullable: true),
            //            FreeTimeStart = table.Column<int>(nullable: true),
            //            FreeTimeEnd = table.Column<int>(nullable: true),
            //            AmountOfSavingsStart = table.Column<long>(nullable: true),
            //            AmountOfSavingsEnd = table.Column<long>(nullable: true),
            //            IncomeStart = table.Column<long>(nullable: true),
            //            IncomeEnd = table.Column<long>(nullable: true),
            //            InitialInvestmentStart = table.Column<long>(nullable: true),
            //            InitialInvestmentEnd = table.Column<long>(nullable: true),
            //            EarningsGoalStart = table.Column<long>(nullable: true),
            //            EarningsGoalEnd = table.Column<long>(nullable: true),
            //            MonthlyIntervalStart = table.Column<long>(nullable: true),
            //            MonthlyIntervalEnd = table.Column<long>(nullable: true),
            //            CategoryId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_FinancialAdvices", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_FinancialAdvices_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ProjectSettings",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: true),
            //            Logo = table.Column<string>(nullable: true),
            //            DefultCategoryId = table.Column<int>(nullable: false),
            //            MaxCapacity = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ProjectSettings", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ProjectSettings_Category_DefultCategoryId",
            //                column: x => x.DefultCategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Provinces",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true),
            //            CountryId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Provinces", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Provinces_Countries_CountryId",
            //                column: x => x.CountryId,
            //                principalTable: "Countries",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "CommissionHistories",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserGeneologyId = table.Column<int>(nullable: false),
            //            AmountFees = table.Column<long>(nullable: false),
            //            DividendAmountHistoryId = table.Column<int>(nullable: true),
            //            CommissionPerMonthId = table.Column<int>(nullable: false),
            //            Month = table.Column<string>(nullable: true),
            //            Year = table.Column<string>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            FeeType = table.Column<int>(nullable: false),
            //            PlanEnum = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_CommissionHistories", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_CommissionHistories_DividendAmountHistories_DividendAmountHistoryId",
            //                column: x => x.DividendAmountHistoryId,
            //                principalTable: "DividendAmountHistories",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanBreakAWayLevels",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            PlanBreakAWayId = table.Column<int>(nullable: false),
            //            PercentBreakAWay = table.Column<int>(nullable: false),
            //            AmountBreakAWay = table.Column<long>(nullable: false),
            //            CalcMethod = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanBreakAWayLevels", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_PlanBreakAWayLevels_PlanBreakAWays_PlanBreakAWayId",
            //                column: x => x.PlanBreakAWayId,
            //                principalTable: "PlanBreakAWays",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "GeneologyPlans",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Subject = table.Column<string>(nullable: true),
            //            GeneologyTypeId = table.Column<int>(nullable: false),
            //            Percent = table.Column<int>(nullable: false),
            //            PlanUniId = table.Column<int>(nullable: true),
            //            PlanBreakAWayId = table.Column<int>(nullable: true),
            //            PlanBinaryId = table.Column<int>(nullable: true),
            //            Plan = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_GeneologyPlans", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_GeneologyPlans_GeneologyTypes_GeneologyTypeId",
            //                column: x => x.GeneologyTypeId,
            //                principalTable: "GeneologyTypes",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_GeneologyPlans_PlanBinaries_PlanBinaryId",
            //                column: x => x.PlanBinaryId,
            //                principalTable: "PlanBinaries",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_GeneologyPlans_PlanBreakAWays_PlanBreakAWayId",
            //                column: x => x.PlanBreakAWayId,
            //                principalTable: "PlanBreakAWays",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_GeneologyPlans_PlanUnis_PlanUniId",
            //                column: x => x.PlanUniId,
            //                principalTable: "PlanUnis",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanUniLevels",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            PlanUniId = table.Column<int>(nullable: false),
            //            LevelNumber = table.Column<int>(nullable: false),
            //            Percent = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanUniLevels", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_PlanUniLevels_PlanUnis_PlanUniId",
            //                column: x => x.PlanUniId,
            //                principalTable: "PlanUnis",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Points",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            Count = table.Column<long>(nullable: false),
            //            PointTypeId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Points", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Points_PointTypes_PointTypeId",
            //                column: x => x.PointTypeId,
            //                principalTable: "PointTypes",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ProducterType",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ProducterId = table.Column<int>(nullable: false),
            //            Type = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ProducterType", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ProducterType_Producters_ProducterId",
            //                column: x => x.ProducterId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Products",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: false),
            //            Rate = table.Column<float>(nullable: false),
            //            ReferralRight = table.Column<long>(nullable: false),
            //            Price = table.Column<double>(nullable: false),
            //            Description = table.Column<string>(maxLength: 500, nullable: false),
            //            Date = table.Column<DateTime>(nullable: false),
            //            LanguageId = table.Column<int>(nullable: false),
            //            Discount = table.Column<double>(nullable: false),
            //            PriceWithDiscount = table.Column<double>(nullable: true),
            //            Type = table.Column<int>(nullable: false),
            //            ShareholderPercentForSell = table.Column<int>(nullable: false),
            //            ShareholderUnitPrice = table.Column<long>(nullable: false),
            //            ShareholderPercentSold = table.Column<int>(nullable: false),
            //            Img = table.Column<string>(nullable: true),
            //            SoldCount = table.Column<long>(nullable: false),
            //            ProductId = table.Column<int>(nullable: true),
            //            IsFavorite = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Products", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Products_Resources_LanguageId",
            //                column: x => x.LanguageId,
            //                principalTable: "Resources",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Products_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Cities",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Name = table.Column<string>(nullable: true),
            //            ProvinceId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Cities", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Cities_Provinces_ProvinceId",
            //                column: x => x.ProvinceId,
            //                principalTable: "Provinces",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PlanDeltas",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Subject = table.Column<string>(nullable: true),
            //            PercentDelta = table.Column<int>(nullable: false),
            //            LevelDelta = table.Column<int>(nullable: false),
            //            GeneologyPlanId = table.Column<int>(nullable: true),
            //            PlanDeltaId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PlanDeltas", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_PlanDeltas_GeneologyPlans_GeneologyPlanId",
            //                column: x => x.GeneologyPlanId,
            //                principalTable: "GeneologyPlans",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_PlanDeltas_PlanDeltas_PlanDeltaId",
            //                column: x => x.PlanDeltaId,
            //                principalTable: "PlanDeltas",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Books",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            AuthorId = table.Column<int>(nullable: true),
            //            TranslatorId = table.Column<int>(nullable: true),
            //            PublisherId = table.Column<int>(nullable: false),
            //            SpeakerId = table.Column<int>(nullable: true),
            //            ProductId = table.Column<int>(nullable: false),
            //            PageCount = table.Column<int>(nullable: false),
            //            Barcode = table.Column<string>(maxLength: 50, nullable: true),
            //            FileAudio = table.Column<string>(nullable: true),
            //            FileText = table.Column<string>(nullable: false),
            //            Size = table.Column<double>(nullable: false),
            //            Partofbook = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Books", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Books_Producters_AuthorId",
            //                column: x => x.AuthorId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Books_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Books_Producters_PublisherId",
            //                column: x => x.PublisherId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Books_Producters_SpeakerId",
            //                column: x => x.SpeakerId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Books_Producters_TranslatorId",
            //                column: x => x.TranslatorId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Courses",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            SampleofCourse = table.Column<string>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            TeacherId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Courses", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Courses_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Courses_Producters_TeacherId",
            //                column: x => x.TeacherId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Exams",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ExamType = table.Column<int>(nullable: false),
            //            AcceptancePerecentage = table.Column<double>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            Status = table.Column<int>(nullable: false),
            //            ExamTime = table.Column<int>(nullable: false),
            //            DesignerId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Exams", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Exams_Producters_DesignerId",
            //                column: x => x.DesignerId,
            //                principalTable: "Producters",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Exams_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ProductScale",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Priority = table.Column<float>(nullable: false),
            //            CatId = table.Column<int>(nullable: true),
            //            LevelId = table.Column<int>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            Credit = table.Column<int>(nullable: false),
            //            Point = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ProductScale", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ProductScale_Category_CatId",
            //                column: x => x.CatId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_ProductScale_Levels_LevelId",
            //                column: x => x.LevelId,
            //                principalTable: "Levels",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_ProductScale_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ProductSeenInfoes",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            ProductId = table.Column<int>(nullable: false),
            //            Credit = table.Column<double>(nullable: false),
            //            Point = table.Column<double>(nullable: false),
            //            IsComplete = table.Column<bool>(nullable: false),
            //            BookLastseenPage = table.Column<string>(nullable: true),
            //            LastseenDate = table.Column<DateTime>(nullable: true),
            //            LastseenCDate = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ProductSeenInfoes", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ProductSeenInfoes_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetUsers",
            //        columns: table => new
            //        {
            //            Id = table.Column<string>(nullable: false),
            //            UserName = table.Column<string>(maxLength: 256, nullable: true),
            //            NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
            //            Email = table.Column<string>(maxLength: 256, nullable: true),
            //            NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
            //            EmailConfirmed = table.Column<bool>(nullable: false),
            //            PasswordHash = table.Column<string>(nullable: true),
            //            SecurityStamp = table.Column<string>(nullable: true),
            //            ConcurrencyStamp = table.Column<string>(nullable: true),
            //            PhoneNumber = table.Column<string>(nullable: true),
            //            PhoneNumberConfirmed = table.Column<bool>(nullable: false),
            //            TwoFactorEnabled = table.Column<bool>(nullable: false),
            //            LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
            //            LockoutEnabled = table.Column<bool>(nullable: false),
            //            AccessFailedCount = table.Column<int>(nullable: false),
            //            FullName = table.Column<string>(nullable: true),
            //            NickName = table.Column<string>(nullable: true),
            //            Point = table.Column<double>(nullable: false),
            //            Age = table.Column<int>(nullable: true),
            //            NationalCode = table.Column<string>(nullable: true),
            //            Phone = table.Column<string>(nullable: true),
            //            BirthDate = table.Column<DateTime>(nullable: true),
            //            Desc = table.Column<string>(nullable: true),
            //            Img = table.Column<string>(nullable: true),
            //            UserInfoComplatePercent = table.Column<double>(nullable: false),
            //            Address = table.Column<string>(nullable: true),
            //            Type = table.Column<int>(nullable: true),
            //            WalletCredit = table.Column<long>(nullable: false),
            //            Active = table.Column<bool>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: true),
            //            MDate = table.Column<DateTime>(nullable: true),
            //            IsDeleted = table.Column<bool>(nullable: true),
            //            CUser = table.Column<string>(nullable: true),
            //            MUser = table.Column<string>(nullable: true),
            //            DUser = table.Column<string>(nullable: true),
            //            DDate = table.Column<DateTime>(nullable: true),
            //            Credit = table.Column<double>(nullable: false),
            //            GooglePlus = table.Column<string>(nullable: true),
            //            WebSite = table.Column<string>(nullable: true),
            //            GitHub = table.Column<string>(nullable: true),
            //            LinkedIn = table.Column<string>(nullable: true),
            //            WhatsApp = table.Column<string>(nullable: true),
            //            Instageram = table.Column<string>(nullable: true),
            //            Telegram = table.Column<string>(nullable: true),
            //            Twitter = table.Column<string>(nullable: true),
            //            AboutMe = table.Column<string>(nullable: true),
            //            CityId = table.Column<int>(nullable: true),
            //            GenderId = table.Column<int>(nullable: true),
            //            HealthStatusId = table.Column<int>(nullable: true),
            //            SolderStatusId = table.Column<int>(nullable: true),
            //            EducationalStatusId = table.Column<int>(nullable: true),
            //            JobStatusId = table.Column<int>(nullable: true),
            //            MaritalStatusId = table.Column<int>(nullable: true),
            //            IsFavorite = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_AspNetUsers_Cities_CityId",
            //                column: x => x.CityId,
            //                principalTable: "Cities",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Videos",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            File = table.Column<string>(nullable: true),
            //            VideoThumbnail = table.Column<string>(nullable: true),
            //            CourseId = table.Column<int>(nullable: false),
            //            Size = table.Column<double>(nullable: false),
            //            VideoTime = table.Column<TimeSpan>(nullable: false),
            //            Title = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Videos", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Videos_Courses_CourseId",
            //                column: x => x.CourseId,
            //                principalTable: "Courses",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Questions",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ExamId = table.Column<int>(nullable: false),
            //            QuestionText = table.Column<string>(maxLength: 500, nullable: false),
            //            QuesFile = table.Column<string>(nullable: true),
            //            QuestionType = table.Column<int>(nullable: false),
            //            StatusType = table.Column<int>(nullable: false),
            //            Score = table.Column<float>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Questions", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Questions_Exams_ExamId",
            //                column: x => x.ExamId,
            //                principalTable: "Exams",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetUserClaims",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            ClaimType = table.Column<string>(nullable: true),
            //            ClaimValue = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetUserLogins",
            //        columns: table => new
            //        {
            //            LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
            //            ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
            //            ProviderDisplayName = table.Column<string>(nullable: true),
            //            UserId = table.Column<string>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //            table.ForeignKey(
            //                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetUserRoles",
            //        columns: table => new
            //        {
            //            UserId = table.Column<string>(nullable: false),
            //            RoleId = table.Column<string>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //            table.ForeignKey(
            //                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //                column: x => x.RoleId,
            //                principalTable: "AspNetRoles",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "AspNetUserTokens",
            //        columns: table => new
            //        {
            //            UserId = table.Column<string>(nullable: false),
            //            LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
            //            Name = table.Column<string>(maxLength: 128, nullable: false),
            //            Value = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //            table.ForeignKey(
            //                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Bills",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            TotalUnitPrice = table.Column<double>(nullable: false),
            //            TotalDiscount = table.Column<double>(nullable: false),
            //            TotalPrice = table.Column<double>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            PaymentDate = table.Column<DateTime>(nullable: true),
            //            RefId = table.Column<string>(nullable: true),
            //            Status = table.Column<int>(nullable: false),
            //            BillType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Bills", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Bills_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Carts",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            SubmitDate = table.Column<DateTime>(nullable: false),
            //            UserId = table.Column<string>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            CartType = table.Column<int>(nullable: false),
            //            RecieverUserId = table.Column<string>(nullable: true),
            //            Credit = table.Column<double>(nullable: true),
            //            ShareholderPercent = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Carts", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Carts_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Carts_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Chats",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Text = table.Column<string>(nullable: true),
            //            SenderId = table.Column<string>(nullable: true),
            //            ReceiverId = table.Column<string>(nullable: true),
            //            Date = table.Column<DateTime>(nullable: false),
            //            File = table.Column<string>(nullable: true),
            //            FileType = table.Column<string>(nullable: true),
            //            FileName = table.Column<string>(nullable: true),
            //            VideoThumbnail = table.Column<string>(nullable: true),
            //            ImageThumbnail = table.Column<string>(nullable: true),
            //            FileSize = table.Column<long>(nullable: false),
            //            Seen = table.Column<bool>(nullable: false),
            //            IsDeleted = table.Column<bool>(nullable: false),
            //            DateIsDeleted = table.Column<DateTime>(nullable: true),
            //            ParentId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Chats", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Chats_Chats_ParentId",
            //                column: x => x.ParentId,
            //                principalTable: "Chats",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Chats_AspNetUsers_ReceiverId",
            //                column: x => x.ReceiverId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Chats_AspNetUsers_SenderId",
            //                column: x => x.SenderId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Commissions",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            SubsetId = table.Column<string>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            Amount = table.Column<double>(nullable: false),
            //            Percent = table.Column<int>(nullable: false),
            //            Fee = table.Column<double>(nullable: false),
            //            FeeSubsets = table.Column<double>(nullable: false),
            //            Datetime = table.Column<DateTime>(nullable: false),
            //            CommissionType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Commissions", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Commissions_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Commissions_AspNetUsers_SubsetId",
            //                column: x => x.SubsetId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "EducationalRecords",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            FieldId = table.Column<int>(nullable: false),
            //            OrientationId = table.Column<int>(nullable: false),
            //            UniversityId = table.Column<int>(nullable: false),
            //            Average = table.Column<double>(nullable: true),
            //            StartDate = table.Column<DateTime>(nullable: false),
            //            EndDate = table.Column<DateTime>(nullable: true),
            //            Grade = table.Column<int>(nullable: false),
            //            Desc = table.Column<string>(nullable: true),
            //            UserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_EducationalRecords", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_EducationalRecords_FieldAndOrientations_FieldId",
            //                column: x => x.FieldId,
            //                principalTable: "FieldAndOrientations",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_EducationalRecords_FieldAndOrientations_OrientationId",
            //                column: x => x.OrientationId,
            //                principalTable: "FieldAndOrientations",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_EducationalRecords_Resources_UniversityId",
            //                column: x => x.UniversityId,
            //                principalTable: "Resources",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_EducationalRecords_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ExamResults",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            ExamId = table.Column<int>(nullable: true),
            //            CorrectAnswersCount = table.Column<int>(nullable: false),
            //            CorrectAnswerPercent = table.Column<double>(nullable: false),
            //            CorrectAnswerScore = table.Column<double>(nullable: false),
            //            StatusExam = table.Column<int>(nullable: false),
            //            ExamDateTime = table.Column<DateTime>(nullable: false),
            //            ExamType = table.Column<int>(nullable: false),
            //            ExamResultId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ExamResults", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ExamResults_Exams_ExamId",
            //                column: x => x.ExamId,
            //                principalTable: "Exams",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_ExamResults_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "JobResumes",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            JobTitle = table.Column<string>(nullable: false),
            //            StartDate = table.Column<DateTime>(nullable: false),
            //            EndDate = table.Column<DateTime>(nullable: true),
            //            Desc = table.Column<string>(nullable: true),
            //            JobPositionId = table.Column<int>(nullable: false),
            //            CompanyId = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_JobResumes", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_JobResumes_Resources_CompanyId",
            //                column: x => x.CompanyId,
            //                principalTable: "Resources",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_JobResumes_Resources_JobPositionId",
            //                column: x => x.JobPositionId,
            //                principalTable: "Resources",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_JobResumes_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Post",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: true),
            //            Img = table.Column<string>(nullable: true),
            //            ImgThumbnail = table.Column<string>(nullable: true),
            //            Video = table.Column<string>(nullable: true),
            //            VideoThumbnail = table.Column<string>(nullable: true),
            //            File = table.Column<string>(nullable: true),
            //            DocumentFile = table.Column<string>(nullable: true),
            //            Desc = table.Column<string>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: false),
            //            LevelId = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: true),
            //            Like = table.Column<int>(nullable: false),
            //            Rate = table.Column<int>(nullable: false),
            //            DisLike = table.Column<int>(nullable: false),
            //            Seen = table.Column<int>(nullable: false),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            MDate = table.Column<DateTime>(nullable: true),
            //            Type = table.Column<int>(nullable: false),
            //            AdsType = table.Column<int>(nullable: false),
            //            IsDeleted = table.Column<bool>(nullable: false),
            //            CommentCount = table.Column<int>(nullable: false),
            //            UserCredit = table.Column<double>(nullable: false),
            //            IsFavorite = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Post", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Post_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Post_Levels_LevelId",
            //                column: x => x.LevelId,
            //                principalTable: "Levels",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Post_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "SellPerMonths",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            SellYourself = table.Column<long>(nullable: false),
            //            BinarySalesVolume = table.Column<long>(nullable: false),
            //            BreakSalesVolume = table.Column<long>(nullable: false),
            //            SellSets = table.Column<long>(nullable: false),
            //            Month = table.Column<string>(nullable: true),
            //            Year = table.Column<string>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            SystemType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_SellPerMonths", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_SellPerMonths_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Shareholders",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            Percent = table.Column<int>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Shareholders", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Shareholders_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Shareholders_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Skills",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Percent = table.Column<float>(nullable: false),
            //            SkillType = table.Column<int>(nullable: false),
            //            IsUpdate = table.Column<bool>(nullable: false),
            //            Lvl1 = table.Column<bool>(nullable: false),
            //            Lvl2 = table.Column<bool>(nullable: false),
            //            Lvl3 = table.Column<bool>(nullable: false),
            //            Lvl4 = table.Column<bool>(nullable: false),
            //            Credit = table.Column<long>(nullable: false),
            //            Point = table.Column<long>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: true),
            //            IsPassRatingExam = table.Column<bool>(nullable: false),
            //            LevelId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Skills", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Skills_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Skills_Levels_LevelId",
            //                column: x => x.LevelId,
            //                principalTable: "Levels",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Skills_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "SuggestedProducts",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ProductPriorityFV = table.Column<float>(nullable: false),
            //            SkillPriorityFV = table.Column<int>(nullable: true),
            //            IsReadProduct = table.Column<bool>(nullable: false),
            //            ProductId = table.Column<int>(nullable: false),
            //            SkillCatId = table.Column<int>(nullable: false),
            //            VisionCatId = table.Column<int>(nullable: true),
            //            UserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_SuggestedProducts", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_SuggestedProducts_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_SuggestedProducts_Category_SkillCatId",
            //                column: x => x.SkillCatId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_SuggestedProducts_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_SuggestedProducts_Category_VisionCatId",
            //                column: x => x.VisionCatId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UserCategories",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            CategoryId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UserCategories", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_UserCategories_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_UserCategories_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UserGeneology",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            ParentId = table.Column<string>(nullable: true),
            //            GeneologyTypeId = table.Column<int>(nullable: false),
            //            Capacity = table.Column<int>(nullable: false),
            //            CityId = table.Column<int>(nullable: true),
            //            Point = table.Column<double>(nullable: false),
            //            IsDefault = table.Column<bool>(nullable: false),
            //            IsInGeneology = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UserGeneology", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_UserGeneology_Cities_CityId",
            //                column: x => x.CityId,
            //                principalTable: "Cities",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_UserGeneology_GeneologyTypes_GeneologyTypeId",
            //                column: x => x.GeneologyTypeId,
            //                principalTable: "GeneologyTypes",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_UserGeneology_AspNetUsers_ParentId",
            //                column: x => x.ParentId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_UserGeneology_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UsersEpubBookInfo",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            ProductId = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: true),
            //            PageNumber = table.Column<int>(nullable: false),
            //            HrefBook = table.Column<string>(nullable: true),
            //            CfiRange = table.Column<string>(nullable: true),
            //            HighlightColor = table.Column<string>(nullable: true),
            //            Date = table.Column<DateTime>(nullable: false),
            //            CDate = table.Column<string>(nullable: true),
            //            HighlightText = table.Column<string>(nullable: true),
            //            Note = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UsersEpubBookInfo", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_UsersEpubBookInfo_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_UsersEpubBookInfo_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UserSettings",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            AdviceNotification = table.Column<bool>(nullable: false),
            //            IntroFirstPage = table.Column<bool>(nullable: false),
            //            IntroAdvice = table.Column<bool>(nullable: false),
            //            IntroProfile = table.Column<bool>(nullable: false),
            //            IntroGoals = table.Column<bool>(nullable: false),
            //            IntroShop = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UserSettings", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_UserSettings_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Vision",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Percent = table.Column<float>(nullable: false),
            //            IsUpdate = table.Column<bool>(nullable: false),
            //            UserId = table.Column<string>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: false),
            //            VisionStatus = table.Column<int>(nullable: false),
            //            VisionType = table.Column<int>(nullable: false),
            //            Priority = table.Column<float>(nullable: false),
            //            FreeTime = table.Column<int>(nullable: true),
            //            AmountOfSavings = table.Column<long>(nullable: true),
            //            Income = table.Column<long>(nullable: true),
            //            InitialInvestment = table.Column<long>(nullable: true),
            //            EarningsGoal = table.Column<long>(nullable: true),
            //            MonthlyInterval = table.Column<long>(nullable: true),
            //            DevelopmentManagerUserId = table.Column<string>(nullable: true),
            //            DirectorOfEducationUserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Vision", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Vision_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Vision_AspNetUsers_DevelopmentManagerUserId",
            //                column: x => x.DevelopmentManagerUserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Vision_AspNetUsers_DirectorOfEducationUserId",
            //                column: x => x.DirectorOfEducationUserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Vision_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "WorkSamples",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: false),
            //            ImgAddress = table.Column<string>(nullable: true),
            //            ImgThumbnail = table.Column<string>(nullable: true),
            //            Date = table.Column<DateTime>(nullable: false),
            //            Desc = table.Column<string>(nullable: true),
            //            CategoryId = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_WorkSamples", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_WorkSamples_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_WorkSamples_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "VideoSeenInfo",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            VideoId = table.Column<int>(nullable: false),
            //            IsComplete = table.Column<bool>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_VideoSeenInfo", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_VideoSeenInfo_Videos_VideoId",
            //                column: x => x.VideoId,
            //                principalTable: "Videos",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Answers",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Text = table.Column<string>(nullable: false),
            //            QuestionId = table.Column<int>(nullable: false),
            //            CorrectAnswer = table.Column<bool>(nullable: false),
            //            ApplicationUserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Answers", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Answers_AspNetUsers_ApplicationUserId",
            //                column: x => x.ApplicationUserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Answers_Questions_QuestionId",
            //                column: x => x.QuestionId,
            //                principalTable: "Questions",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "FactorforsaleProducts",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            ProductId = table.Column<int>(nullable: false),
            //            Count = table.Column<int>(nullable: false),
            //            UnitPrice = table.Column<double>(nullable: false),
            //            Discount = table.Column<double>(nullable: false),
            //            TotalPrice = table.Column<double>(nullable: false),
            //            BuyType = table.Column<int>(nullable: false),
            //            BillId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_FactorforsaleProducts", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_FactorforsaleProducts_Bills_BillId",
            //                column: x => x.BillId,
            //                principalTable: "Bills",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_FactorforsaleProducts_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_FactorforsaleProducts_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Gift",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserSend = table.Column<string>(nullable: true),
            //            UserResiv = table.Column<string>(nullable: true),
            //            PorductId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false),
            //            Status = table.Column<bool>(nullable: false),
            //            BillId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Gift", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Gift_Bills_BillId",
            //                column: x => x.BillId,
            //                principalTable: "Bills",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Gift_Products_PorductId",
            //                column: x => x.PorductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Gift_AspNetUsers_UserResiv",
            //                column: x => x.UserResiv,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Gift_AspNetUsers_UserSend",
            //                column: x => x.UserSend,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "ChatContacts",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            ContactId = table.Column<string>(nullable: true),
            //            UnSeenCount = table.Column<int>(nullable: false),
            //            MyProperty = table.Column<int>(nullable: false),
            //            UpdateDate = table.Column<DateTime>(nullable: false),
            //            ChatId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_ChatContacts", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_ChatContacts_Chats_ChatId",
            //                column: x => x.ChatId,
            //                principalTable: "Chats",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_ChatContacts_AspNetUsers_ContactId",
            //                column: x => x.ContactId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "UserAnswers",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: false),
            //            QuestionId = table.Column<int>(nullable: false),
            //            Answer = table.Column<int>(nullable: false),
            //            Score = table.Column<float>(nullable: false),
            //            ExamResultId = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_UserAnswers", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_UserAnswers_ExamResults_ExamResultId",
            //                column: x => x.ExamResultId,
            //                principalTable: "ExamResults",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_UserAnswers_Questions_QuestionId",
            //                column: x => x.QuestionId,
            //                principalTable: "Questions",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_UserAnswers_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Cat_Posts",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            PostId = table.Column<int>(nullable: false),
            //            CatId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Cat_Posts", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Cat_Posts_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Cat_Posts_Post_PostId",
            //                column: x => x.PostId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Comment",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Desc = table.Column<string>(nullable: true),
            //            Rank = table.Column<int>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            ParentId = table.Column<int>(nullable: true),
            //            FirstParentId = table.Column<int>(nullable: true),
            //            PostId = table.Column<int>(nullable: true),
            //            ProductId = table.Column<int>(nullable: true),
            //            BlogId = table.Column<int>(nullable: true),
            //            UserId = table.Column<string>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Comment", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Comment_Blogs_BlogId",
            //                column: x => x.BlogId,
            //                principalTable: "Blogs",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Comment_Comment_FirstParentId",
            //                column: x => x.FirstParentId,
            //                principalTable: "Comment",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Comment_Comment_ParentId",
            //                column: x => x.ParentId,
            //                principalTable: "Comment",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Comment_Post_PostId",
            //                column: x => x.PostId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Comment_Products_ProductId",
            //                column: x => x.ProductId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Comment_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Favorites",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            FavedType = table.Column<int>(nullable: false),
            //            UserId = table.Column<string>(nullable: true),
            //            ProductFavedId = table.Column<int>(nullable: true),
            //            UserFavedId = table.Column<string>(nullable: true),
            //            PostFavedId = table.Column<int>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Favorites", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Favorites_Post_PostFavedId",
            //                column: x => x.PostFavedId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Favorites_Products_ProductFavedId",
            //                column: x => x.ProductFavedId,
            //                principalTable: "Products",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Favorites_AspNetUsers_UserFavedId",
            //                column: x => x.UserFavedId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Likes",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            PostId = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false),
            //            IsLike = table.Column<bool>(nullable: false),
            //            Count = table.Column<int>(nullable: false),
            //            Point = table.Column<long>(nullable: false),
            //            Rate = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Likes", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Likes_Post_PostId",
            //                column: x => x.PostId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //            table.ForeignKey(
            //                name: "FK_Likes_AspNetUsers_UserId",
            //                column: x => x.UserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Notifis",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(nullable: true),
            //            Text = table.Column<string>(nullable: true),
            //            ReceiverId = table.Column<string>(nullable: true),
            //            SenderId = table.Column<string>(nullable: true),
            //            CommentId = table.Column<int>(nullable: true),
            //            PostId = table.Column<int>(nullable: true),
            //            NotifiType = table.Column<int>(nullable: false),
            //            Date = table.Column<DateTime>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Notifis", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Notifis_Post_PostId",
            //                column: x => x.PostId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Notifis_AspNetUsers_ReceiverId",
            //                column: x => x.ReceiverId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_Notifis_AspNetUsers_SenderId",
            //                column: x => x.SenderId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "PostChangeRequests",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserId = table.Column<string>(nullable: true),
            //            IsExist = table.Column<bool>(nullable: false),
            //            LevelId = table.Column<int>(nullable: true),
            //            PostId = table.Column<int>(nullable: false),
            //            CategoryId = table.Column<int>(nullable: true),
            //            ApplicationUserId = table.Column<string>(nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_PostChangeRequests", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_PostChangeRequests_AspNetUsers_ApplicationUserId",
            //                column: x => x.ApplicationUserId,
            //                principalTable: "AspNetUsers",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_PostChangeRequests_Category_CategoryId",
            //                column: x => x.CategoryId,
            //                principalTable: "Category",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_PostChangeRequests_Levels_LevelId",
            //                column: x => x.LevelId,
            //                principalTable: "Levels",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Restrict);
            //            table.ForeignKey(
            //                name: "FK_PostChangeRequests_Post_PostId",
            //                column: x => x.PostId,
            //                principalTable: "Post",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "CommissionPerMonths",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            UserGeneologyId = table.Column<int>(nullable: false),
            //            AmountFees = table.Column<long>(nullable: false),
            //            Month = table.Column<string>(nullable: true),
            //            Year = table.Column<string>(nullable: true),
            //            CDate = table.Column<DateTime>(nullable: false),
            //            UDate = table.Column<DateTime>(nullable: false),
            //            FeeType = table.Column<int>(nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_CommissionPerMonths", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_CommissionPerMonths_UserGeneology_UserGeneologyId",
            //                column: x => x.UserGeneologyId,
            //                principalTable: "UserGeneology",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.InsertData(
            //        table: "AspNetRoles",
            //        columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //        values: new object[,]
            //        {
            //            { "1", "ad491a04-485f-45fb-a18a-ba0d478701bb", "Administrator", null },
            //            { "2", "9aac5089-4e11-4213-8017-96ae3998a709", "Admin", null },
            //            { "3", "384ab110-0428-4e8b-a923-4ef40b085ad0", "Operator", null },
            //            { "4", "6b1c9ff4-3c1e-4386-a19e-02667229bd46", "User", null }
            //        });

            //    migrationBuilder.InsertData(
            //        table: "Category",
            //        columns: new[] { "Id", "CategoryFinancialType", "CategoryType", "Img", "Title" },
            //        values: new object[] { 1, 0, 0, null, "فرصت" });

            //    migrationBuilder.InsertData(
            //        table: "GeneologyTypes",
            //        columns: new[] { "Id", "CalculationTime", "RowType", "SystemType", "Title", "Type" },
            //        values: new object[,]
            //        {
            //            { 1, 0, 1, 4, "معرف", 7 },
            //            { 2, 0, 1, 0, "مدیر توسعه بیمه", 0 },
            //            { 3, 0, 1, 0, "مدیر آموزش بیمه", 1 },
            //            { 4, 0, 1, 2, "مدیر توسعه بورس", 4 },
            //            { 5, 0, 1, 2, "مدیر آموزش بورس", 5 }
            //        });

            //    migrationBuilder.InsertData(
            //        table: "Levels",
            //        columns: new[] { "Id", "Name", "Number" },
            //        values: new object[,]
            //        {
            //            { 4, "فوق پیشرفته", 4 },
            //            { 3, "پیشرفته", 3 },
            //            { 1, "مقدماتی", 1 },
            //            { 2, "متوسط", 2 }
            //        });

            //    migrationBuilder.InsertData(
            //        table: "PointTypes",
            //        columns: new[] { "Id", "Name" },
            //        values: new object[,]
            //        {
            //            { 1, "Like" },
            //            { 2, "DisLike" }
            //        });

            //    migrationBuilder.InsertData(
            //        table: "Resources",
            //        columns: new[] { "Id", "Name", "ResourcesType" },
            //        values: new object[,]
            //        {
            //            { 5, "فارسی", 6 },
            //            { 1, "عمومی", 2 },
            //            { 2, "تخصصی", 2 },
            //            { 3, "نیمه تخصصی", 2 },
            //            { 4, "فوق تخصصی", 2 },
            //            { 6, "انگلیسی", 6 }
            //        });

            //    migrationBuilder.InsertData(
            //        table: "ProjectSettings",
            //        columns: new[] { "Id", "DefultCategoryId", "Logo", "MaxCapacity", "Title" },
            //        values: new object[] { 1, 1, null, 15, "فرصت" });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Answers_ApplicationUserId",
            //        table: "Answers",
            //        column: "ApplicationUserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Answers_QuestionId",
            //        table: "Answers",
            //        column: "QuestionId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_AspNetRoleClaims_RoleId",
            //        table: "AspNetRoleClaims",
            //        column: "RoleId");

            //    migrationBuilder.CreateIndex(
            //        name: "RoleNameIndex",
            //        table: "AspNetRoles",
            //        column: "NormalizedName",
            //        unique: true,
            //        filter: "[NormalizedName] IS NOT NULL");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_AspNetUserClaims_UserId",
            //        table: "AspNetUserClaims",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_AspNetUserLogins_UserId",
            //        table: "AspNetUserLogins",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_AspNetUserRoles_RoleId",
            //        table: "AspNetUserRoles",
            //        column: "RoleId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_AspNetUsers_CityId",
            //        table: "AspNetUsers",
            //        column: "CityId");

            //    migrationBuilder.CreateIndex(
            //        name: "EmailIndex",
            //        table: "AspNetUsers",
            //        column: "NormalizedEmail");

            //    migrationBuilder.CreateIndex(
            //        name: "UserNameIndex",
            //        table: "AspNetUsers",
            //        column: "NormalizedUserName",
            //        unique: true,
            //        filter: "[NormalizedUserName] IS NOT NULL");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Bills_UserId",
            //        table: "Bills",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_BlogCategory_BlogId",
            //        table: "BlogCategory",
            //        column: "BlogId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_BlogCategory_CategoryId",
            //        table: "BlogCategory",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_BlogRelation_BlogId",
            //        table: "BlogRelation",
            //        column: "BlogId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Books_AuthorId",
            //        table: "Books",
            //        column: "AuthorId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Books_ProductId",
            //        table: "Books",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Books_PublisherId",
            //        table: "Books",
            //        column: "PublisherId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Books_SpeakerId",
            //        table: "Books",
            //        column: "SpeakerId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Books_TranslatorId",
            //        table: "Books",
            //        column: "TranslatorId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Carts_ProductId",
            //        table: "Carts",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Carts_UserId",
            //        table: "Carts",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Cat_Posts_CategoryId",
            //        table: "Cat_Posts",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Cat_Posts_PostId",
            //        table: "Cat_Posts",
            //        column: "PostId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Category_Category_ChildrenCatId",
            //        table: "Category_Category",
            //        column: "ChildrenCatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Category_Category_ParentCatId",
            //        table: "Category_Category",
            //        column: "ParentCatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_CategoryLevels_CategoryId",
            //        table: "CategoryLevels",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ChatContacts_ChatId",
            //        table: "ChatContacts",
            //        column: "ChatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ChatContacts_ContactId",
            //        table: "ChatContacts",
            //        column: "ContactId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Chats_ParentId",
            //        table: "Chats",
            //        column: "ParentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Chats_ReceiverId",
            //        table: "Chats",
            //        column: "ReceiverId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Chats_SenderId",
            //        table: "Chats",
            //        column: "SenderId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Cities_ProvinceId",
            //        table: "Cities",
            //        column: "ProvinceId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_BlogId",
            //        table: "Comment",
            //        column: "BlogId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_FirstParentId",
            //        table: "Comment",
            //        column: "FirstParentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_ParentId",
            //        table: "Comment",
            //        column: "ParentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_PostId",
            //        table: "Comment",
            //        column: "PostId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_ProductId",
            //        table: "Comment",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Comment_UserId",
            //        table: "Comment",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_CommissionHistories_DividendAmountHistoryId",
            //        table: "CommissionHistories",
            //        column: "DividendAmountHistoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_CommissionPerMonths_UserGeneologyId",
            //        table: "CommissionPerMonths",
            //        column: "UserGeneologyId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Commissions_ProductId",
            //        table: "Commissions",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Commissions_SubsetId",
            //        table: "Commissions",
            //        column: "SubsetId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Courses_ProductId",
            //        table: "Courses",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Courses_TeacherId",
            //        table: "Courses",
            //        column: "TeacherId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_DeviceCodes_DeviceCode",
            //        table: "DeviceCodes",
            //        column: "DeviceCode",
            //        unique: true);

            //    migrationBuilder.CreateIndex(
            //        name: "IX_DeviceCodes_Expiration",
            //        table: "DeviceCodes",
            //        column: "Expiration");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_EducationalRecords_FieldId",
            //        table: "EducationalRecords",
            //        column: "FieldId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_EducationalRecords_OrientationId",
            //        table: "EducationalRecords",
            //        column: "OrientationId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_EducationalRecords_UniversityId",
            //        table: "EducationalRecords",
            //        column: "UniversityId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_EducationalRecords_UserId",
            //        table: "EducationalRecords",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ExamResults_ExamId",
            //        table: "ExamResults",
            //        column: "ExamId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ExamResults_UserId",
            //        table: "ExamResults",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Exams_DesignerId",
            //        table: "Exams",
            //        column: "DesignerId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Exams_ProductId",
            //        table: "Exams",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_FactorforsaleProducts_BillId",
            //        table: "FactorforsaleProducts",
            //        column: "BillId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_FactorforsaleProducts_ProductId",
            //        table: "FactorforsaleProducts",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_FactorforsaleProducts_UserId",
            //        table: "FactorforsaleProducts",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Favorites_PostFavedId",
            //        table: "Favorites",
            //        column: "PostFavedId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Favorites_ProductFavedId",
            //        table: "Favorites",
            //        column: "ProductFavedId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Favorites_UserFavedId",
            //        table: "Favorites",
            //        column: "UserFavedId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_FieldAndOrientations_ParentId",
            //        table: "FieldAndOrientations",
            //        column: "ParentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_FinancialAdvices_CategoryId",
            //        table: "FinancialAdvices",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_GeneologyPlans_GeneologyTypeId",
            //        table: "GeneologyPlans",
            //        column: "GeneologyTypeId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_GeneologyPlans_PlanBinaryId",
            //        table: "GeneologyPlans",
            //        column: "PlanBinaryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_GeneologyPlans_PlanBreakAWayId",
            //        table: "GeneologyPlans",
            //        column: "PlanBreakAWayId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_GeneologyPlans_PlanUniId",
            //        table: "GeneologyPlans",
            //        column: "PlanUniId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Gift_BillId",
            //        table: "Gift",
            //        column: "BillId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Gift_PorductId",
            //        table: "Gift",
            //        column: "PorductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Gift_UserResiv",
            //        table: "Gift",
            //        column: "UserResiv");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Gift_UserSend",
            //        table: "Gift",
            //        column: "UserSend");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_JobResumes_CompanyId",
            //        table: "JobResumes",
            //        column: "CompanyId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_JobResumes_JobPositionId",
            //        table: "JobResumes",
            //        column: "JobPositionId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_JobResumes_UserId",
            //        table: "JobResumes",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Likes_PostId",
            //        table: "Likes",
            //        column: "PostId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Likes_UserId",
            //        table: "Likes",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Notifis_PostId",
            //        table: "Notifis",
            //        column: "PostId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Notifis_ReceiverId",
            //        table: "Notifis",
            //        column: "ReceiverId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Notifis_SenderId",
            //        table: "Notifis",
            //        column: "SenderId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PersistedGrants_Expiration",
            //        table: "PersistedGrants",
            //        column: "Expiration");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PersistedGrants_SubjectId_ClientId_Type",
            //        table: "PersistedGrants",
            //        columns: new[] { "SubjectId", "ClientId", "Type" });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PlanBreakAWayLevels_PlanBreakAWayId",
            //        table: "PlanBreakAWayLevels",
            //        column: "PlanBreakAWayId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PlanDeltas_GeneologyPlanId",
            //        table: "PlanDeltas",
            //        column: "GeneologyPlanId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PlanDeltas_PlanDeltaId",
            //        table: "PlanDeltas",
            //        column: "PlanDeltaId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PlanUniLevels_PlanUniId",
            //        table: "PlanUniLevels",
            //        column: "PlanUniId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Points_PointTypeId",
            //        table: "Points",
            //        column: "PointTypeId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Post_CategoryId",
            //        table: "Post",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Post_LevelId",
            //        table: "Post",
            //        column: "LevelId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Post_UserId",
            //        table: "Post",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PostChangeRequests_ApplicationUserId",
            //        table: "PostChangeRequests",
            //        column: "ApplicationUserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PostChangeRequests_CategoryId",
            //        table: "PostChangeRequests",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PostChangeRequests_LevelId",
            //        table: "PostChangeRequests",
            //        column: "LevelId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_PostChangeRequests_PostId",
            //        table: "PostChangeRequests",
            //        column: "PostId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProducterType_ProducterId",
            //        table: "ProducterType",
            //        column: "ProducterId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Products_LanguageId",
            //        table: "Products",
            //        column: "LanguageId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Products_ProductId",
            //        table: "Products",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProductScale_CatId",
            //        table: "ProductScale",
            //        column: "CatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProductScale_LevelId",
            //        table: "ProductScale",
            //        column: "LevelId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProductScale_ProductId",
            //        table: "ProductScale",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProductSeenInfoes_ProductId",
            //        table: "ProductSeenInfoes",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProjectSettings_DefultCategoryId",
            //        table: "ProjectSettings",
            //        column: "DefultCategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Provinces_CountryId",
            //        table: "Provinces",
            //        column: "CountryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Questions_ExamId",
            //        table: "Questions",
            //        column: "ExamId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SellPerMonths_UserId",
            //        table: "SellPerMonths",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Shareholders_ProductId",
            //        table: "Shareholders",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Shareholders_UserId",
            //        table: "Shareholders",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Skills_CategoryId",
            //        table: "Skills",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Skills_LevelId",
            //        table: "Skills",
            //        column: "LevelId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Skills_UserId",
            //        table: "Skills",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SuggestedProducts_ProductId",
            //        table: "SuggestedProducts",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SuggestedProducts_SkillCatId",
            //        table: "SuggestedProducts",
            //        column: "SkillCatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SuggestedProducts_UserId",
            //        table: "SuggestedProducts",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_SuggestedProducts_VisionCatId",
            //        table: "SuggestedProducts",
            //        column: "VisionCatId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserAnswers_ExamResultId",
            //        table: "UserAnswers",
            //        column: "ExamResultId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserAnswers_QuestionId",
            //        table: "UserAnswers",
            //        column: "QuestionId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserAnswers_UserId",
            //        table: "UserAnswers",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserCategories_CategoryId",
            //        table: "UserCategories",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserCategories_UserId",
            //        table: "UserCategories",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserGeneology_CityId",
            //        table: "UserGeneology",
            //        column: "CityId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserGeneology_GeneologyTypeId",
            //        table: "UserGeneology",
            //        column: "GeneologyTypeId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserGeneology_ParentId",
            //        table: "UserGeneology",
            //        column: "ParentId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserGeneology_UserId",
            //        table: "UserGeneology",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UsersEpubBookInfo_ProductId",
            //        table: "UsersEpubBookInfo",
            //        column: "ProductId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UsersEpubBookInfo_UserId",
            //        table: "UsersEpubBookInfo",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_UserSettings_UserId",
            //        table: "UserSettings",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Videos_CourseId",
            //        table: "Videos",
            //        column: "CourseId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_VideoSeenInfo_VideoId",
            //        table: "VideoSeenInfo",
            //        column: "VideoId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Vision_CategoryId",
            //        table: "Vision",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Vision_DevelopmentManagerUserId",
            //        table: "Vision",
            //        column: "DevelopmentManagerUserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Vision_DirectorOfEducationUserId",
            //        table: "Vision",
            //        column: "DirectorOfEducationUserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Vision_UserId",
            //        table: "Vision",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_WorkSamples_CategoryId",
            //        table: "WorkSamples",
            //        column: "CategoryId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_WorkSamples_UserId",
            //        table: "WorkSamples",
            //        column: "UserId");
            //migrationBuilder.AddColumn<ProductType>(name: "producterType", table: "ShopHomeSliders", nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AppUpdates");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BankInputs");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropTable(
                name: "BlogRelation");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Cat_Posts");

            migrationBuilder.DropTable(
                name: "Category_Category");

            migrationBuilder.DropTable(
                name: "CategoryLevels");

            migrationBuilder.DropTable(
                name: "ChatContacts");

            migrationBuilder.DropTable(
                name: "ChatLogs");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommissionHistories");

            migrationBuilder.DropTable(
                name: "CommissionPerMonths");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "EducationalRecords");

            migrationBuilder.DropTable(
                name: "ExchangeInputs");

            migrationBuilder.DropTable(
                name: "FactorforsaleProducts");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "FinancialAdvices");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "InsuranceInputs");

            migrationBuilder.DropTable(
                name: "JobResumes");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "NotifiLogs");

            migrationBuilder.DropTable(
                name: "Notifis");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "PlanBreakAWayLevels");

            migrationBuilder.DropTable(
                name: "PlanDeltas");

            migrationBuilder.DropTable(
                name: "PlanUniLevels");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "PostChangeRequests");

            migrationBuilder.DropTable(
                name: "ProducterType");

            migrationBuilder.DropTable(
                name: "ProductScale");

            migrationBuilder.DropTable(
                name: "ProductSeenInfoes");

            migrationBuilder.DropTable(
                name: "ProjectSettings");

            migrationBuilder.DropTable(
                name: "Seens");

            migrationBuilder.DropTable(
                name: "SellPerMonths");

            migrationBuilder.DropTable(
                name: "Shareholders");

            migrationBuilder.DropTable(
                name: "ShopHomeSliders");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SuggestedProducts");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "UserCategories");

            migrationBuilder.DropTable(
                name: "UserPushTokens");

            migrationBuilder.DropTable(
                name: "UsersEpubBookInfo");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "VideoSeenInfo");

            migrationBuilder.DropTable(
                name: "Vision");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "WorkSamples");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "DividendAmountHistories");

            migrationBuilder.DropTable(
                name: "UserGeneology");

            migrationBuilder.DropTable(
                name: "FieldAndOrientations");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "GeneologyPlans");

            migrationBuilder.DropTable(
                name: "PointTypes");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "GeneologyTypes");

            migrationBuilder.DropTable(
                name: "PlanBinaries");

            migrationBuilder.DropTable(
                name: "PlanBreakAWays");

            migrationBuilder.DropTable(
                name: "PlanUnis");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Producters");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
