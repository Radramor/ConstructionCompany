using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace СonstructionСompany.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brigades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brigades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CountMaterials = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NameOfOrganization = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    FirstNameOfSupervisor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SecondNameOfSupervisor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PatronymicOfSupervisor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    INN = table.Column<string>(type: "text", nullable: true),
                    KPP = table.Column<string>(type: "text", nullable: true),
                    OGRN = table.Column<string>(type: "text", nullable: true),
                    SettlementAccount = table.Column<string>(type: "text", nullable: true),
                    CorrespondentAccount = table.Column<string>(type: "text", nullable: true),
                    BIK = table.Column<string>(type: "text", nullable: true),
                    BankId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Builders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SecondName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResidentialAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LengthOfService = table.Column<int>(type: "integer", nullable: false),
                    Specialties = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Builders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Builders_Brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountingForWorkPerformeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BrigadeId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkDescription = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingForWorkPerformeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingForWorkPerformeds_Brigades_BrigadeId",
                        column: x => x.BrigadeId,
                        principalTable: "Brigades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingForWorkPerformeds_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estimates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountMaterials = table.Column<List<int>>(type: "integer[]", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimates_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterialDistributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountMaterials = table.Column<List<int>>(type: "integer[]", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterialDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialDistributions_Objects_ObjectId",
                        column: x => x.ObjectId,
                        principalTable: "Objects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialDistributions_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingMaterials_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuyingBuildingMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CountMaterials = table.Column<List<int>>(type: "integer[]", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyingBuildingMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyingBuildingMaterials_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyingBuildingMaterials_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterialBuildingMaterialDistribution",
                columns: table => new
                {
                    BuildingMaterialsDistributionId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingMaterialsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterialBuildingMaterialDistribution", x => new { x.BuildingMaterialsDistributionId, x.BuildingMaterialsId });
                    table.ForeignKey(
                        name: "FK_BuildingMaterialBuildingMaterialDistribution_BuildingMateri~",
                        column: x => x.BuildingMaterialsDistributionId,
                        principalTable: "BuildingMaterialDistributions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialBuildingMaterialDistribution_BuildingMater~1",
                        column: x => x.BuildingMaterialsId,
                        principalTable: "BuildingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterialEstimate",
                columns: table => new
                {
                    BuildingMaterialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstimatesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterialEstimate", x => new { x.BuildingMaterialsId, x.EstimatesId });
                    table.ForeignKey(
                        name: "FK_BuildingMaterialEstimate_BuildingMaterials_BuildingMaterial~",
                        column: x => x.BuildingMaterialsId,
                        principalTable: "BuildingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialEstimate_Estimates_EstimatesId",
                        column: x => x.EstimatesId,
                        principalTable: "Estimates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterialWarehouse",
                columns: table => new
                {
                    BuildingMaterialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehousesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterialWarehouse", x => new { x.BuildingMaterialsId, x.WarehousesId });
                    table.ForeignKey(
                        name: "FK_BuildingMaterialWarehouse_BuildingMaterials_BuildingMateria~",
                        column: x => x.BuildingMaterialsId,
                        principalTable: "BuildingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialWarehouse_Warehouses_WarehousesId",
                        column: x => x.WarehousesId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingMaterialBuyingBuildingMaterials",
                columns: table => new
                {
                    BuildingMaterialsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyingBuildingMaterialsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterialBuyingBuildingMaterials", x => new { x.BuildingMaterialsId, x.BuyingBuildingMaterialsId });
                    table.ForeignKey(
                        name: "FK_BuildingMaterialBuyingBuildingMaterials_BuildingMaterials_B~",
                        column: x => x.BuildingMaterialsId,
                        principalTable: "BuildingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingMaterialBuyingBuildingMaterials_BuyingBuildingMater~",
                        column: x => x.BuyingBuildingMaterialsId,
                        principalTable: "BuyingBuildingMaterials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingForWorkPerformeds_BrigadeId",
                table: "AccountingForWorkPerformeds",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingForWorkPerformeds_ObjectId",
                table: "AccountingForWorkPerformeds",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Builders_BrigadeId",
                table: "Builders",
                column: "BrigadeId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialBuildingMaterialDistribution_BuildingMateri~",
                table: "BuildingMaterialBuildingMaterialDistribution",
                column: "BuildingMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialBuyingBuildingMaterials_BuyingBuildingMater~",
                table: "BuildingMaterialBuyingBuildingMaterials",
                column: "BuyingBuildingMaterialsId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialDistributions_ObjectId",
                table: "BuildingMaterialDistributions",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialDistributions_WarehouseId",
                table: "BuildingMaterialDistributions",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialEstimate_EstimatesId",
                table: "BuildingMaterialEstimate",
                column: "EstimatesId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterials_SupplierId",
                table: "BuildingMaterials",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingMaterialWarehouse_WarehousesId",
                table: "BuildingMaterialWarehouse",
                column: "WarehousesId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyingBuildingMaterials_SupplierId",
                table: "BuyingBuildingMaterials",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyingBuildingMaterials_WarehouseId",
                table: "BuyingBuildingMaterials",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Estimates_ObjectId",
                table: "Estimates",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_BankId",
                table: "Suppliers",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingForWorkPerformeds");

            migrationBuilder.DropTable(
                name: "Builders");

            migrationBuilder.DropTable(
                name: "BuildingMaterialBuildingMaterialDistribution");

            migrationBuilder.DropTable(
                name: "BuildingMaterialBuyingBuildingMaterials");

            migrationBuilder.DropTable(
                name: "BuildingMaterialEstimate");

            migrationBuilder.DropTable(
                name: "BuildingMaterialWarehouse");

            migrationBuilder.DropTable(
                name: "Brigades");

            migrationBuilder.DropTable(
                name: "BuildingMaterialDistributions");

            migrationBuilder.DropTable(
                name: "BuyingBuildingMaterials");

            migrationBuilder.DropTable(
                name: "Estimates");

            migrationBuilder.DropTable(
                name: "BuildingMaterials");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Objects");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
