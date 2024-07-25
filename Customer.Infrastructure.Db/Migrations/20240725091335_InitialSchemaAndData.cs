using System;
using Customer.Core;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customer.Infrastructure.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EngineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChassisId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PreassembledVehicleId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PartsAwaitingManufacture = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Paid = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_VehicleOrders_VehicleOrderId",
                        column: x => x.VehicleOrderId,
                        principalTable: "VehicleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllChassis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllChassis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllChassis_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Killowatts = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engines_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OptionPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionPacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionPacks_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreassembledVehicle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EngineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChassisId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreassembledVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreassembledVehicle_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllChassis_WarehouseId",
                table: "AllChassis",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Engines_WarehouseId",
                table: "Engines",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VehicleOrderId",
                table: "Invoices",
                column: "VehicleOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionPacks_WarehouseId",
                table: "OptionPacks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PreassembledVehicle_WarehouseId",
                table: "PreassembledVehicle",
                column: "WarehouseId");

            migrationBuilder.InsertData(
                "Warehouses",
                ["Id"],
                [Warehouse.MainWarehouseId]);

            var engine1Id = Guid.NewGuid();
            var engine2Id = Guid.NewGuid();
            var engine3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "Model", "Type", "Killowatts", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [engine1Id, "EP1", "Petrol 1.2L", 90, 3, 0, 2000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "Model", "Type", "Killowatts", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [engine2Id, "EP2", "Petrol 1.6L", 120, 5, 0, 3000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "Model", "Type", "Killowatts", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [engine3Id, "ED1", "Diesel 1.5L", 120, 2, 0, 2500, Warehouse.MainWarehouseId]);

            var chassis1Id = Guid.NewGuid();
            var chassis2Id = Guid.NewGuid();
            var chassis3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "Model", "Type", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [chassis1Id, "Model 1", "Hatchback", 8, 0, 15000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "Model", "Type", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [chassis2Id, "Model 2", "Sedan", 4, 0, 19000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "Model", "Type", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [chassis3Id, "Model 3", "SUV", 10, 0, 22000, Warehouse.MainWarehouseId]);

            var optionPack1Id = Guid.NewGuid();
            var optionPack2Id = Guid.NewGuid();
            var optionPack3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "Name", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [optionPack1Id, "Comfort", 12, 0, 1200, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "Name", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [optionPack2Id, "Elegance", 10, 0, 3100, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "Name", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [optionPack3Id, "Executive", 4, 0, 4800, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "PreassembledVehicle",
                ["Id", "EngineId", "ChassisId", "OptionPackId", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [Guid.NewGuid(), engine1Id, chassis2Id, optionPack2Id, 3, 0, 25000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "PreassembledVehicle",
                ["Id", "EngineId", "ChassisId", "OptionPackId", "AvailableQuantity", "ReservedQuantity", "Price", "WarehouseId"],
                [Guid.NewGuid(), engine3Id, chassis3Id, optionPack3Id, 2, 0, 35000, Warehouse.MainWarehouseId]);

            migrationBuilder.InsertData(
                "Customers",
                ["Id", "FirstName", "LastName", "Address"],
                [Guid.NewGuid(), "John", "Doe", "Random Street"]);

            migrationBuilder.InsertData(
                "Customers",
                ["Id", "FirstName", "LastName", "Address"],
                [Guid.NewGuid(), "Steve", "Jobless", "Pear Street"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllChassis");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "OptionPacks");

            migrationBuilder.DropTable(
                name: "PreassembledVehicle");

            migrationBuilder.DropTable(
                name: "VehicleOrders");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
