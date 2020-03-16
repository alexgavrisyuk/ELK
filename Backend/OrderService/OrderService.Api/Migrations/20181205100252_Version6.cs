using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OrderService.Api.Migrations
{
    public partial class Version6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    OrderCreationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ApplicationType = table.Column<int>(nullable: false),
                    MinimumRate = table.Column<decimal>(nullable: false),
                    UpchargeType = table.Column<int>(nullable: false),
                    UpchargeValue = table.Column<decimal>(nullable: false),
                    PricingType = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lanes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PricingType = table.Column<int>(nullable: false),
                    LaneType = table.Column<int>(nullable: false),
                    MatrixLaneType = table.Column<int>(nullable: false),
                    RateType = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    SourceRegionId = table.Column<int>(nullable: false),
                    DestinationRegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lanes_Regions_DestinationRegionId",
                        column: x => x.DestinationRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lanes_Regions_SourceRegionId",
                        column: x => x.SourceRegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conditional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PackagingType = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    TariffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conditional_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractXrefTariff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ContractId = table.Column<int>(nullable: false),
                    TariffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractXrefTariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractXrefTariff_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractXrefTariff_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Type = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    MinValue = table.Column<decimal>(nullable: false),
                    MaxValue = table.Column<decimal>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    TariffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffExtras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ApplicationType = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    FuelRateType = table.Column<int>(nullable: false),
                    AccessorialRateType = table.Column<int>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    TariffId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffExtras_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TariffXrefLane",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TariffId = table.Column<int>(nullable: false),
                    LaneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TariffXrefLane", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TariffXrefLane_Lanes_LaneId",
                        column: x => x.LaneId,
                        principalTable: "Lanes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TariffXrefLane_Tariffs_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conditional_TariffId",
                table: "Conditional",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractXrefTariff_ContractId",
                table: "ContractXrefTariff",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractXrefTariff_TariffId",
                table: "ContractXrefTariff",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_TariffId",
                table: "Discount",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Lanes_DestinationRegionId",
                table: "Lanes",
                column: "DestinationRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Lanes_SourceRegionId",
                table: "Lanes",
                column: "SourceRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffExtras_TariffId",
                table: "TariffExtras",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffXrefLane_LaneId",
                table: "TariffXrefLane",
                column: "LaneId");

            migrationBuilder.CreateIndex(
                name: "IX_TariffXrefLane_TariffId",
                table: "TariffXrefLane",
                column: "TariffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conditional");

            migrationBuilder.DropTable(
                name: "ContractXrefTariff");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "TariffExtras");

            migrationBuilder.DropTable(
                name: "TariffXrefLane");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Lanes");

            migrationBuilder.DropTable(
                name: "Tariffs");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
