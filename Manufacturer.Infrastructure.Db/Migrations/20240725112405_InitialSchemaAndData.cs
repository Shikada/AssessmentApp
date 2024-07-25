using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Manufacturer.Infrastructure.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllChassis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllChassis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChassisManufactureItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChassisId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChassisManufactureItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EngineManufactureItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EngineId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineManufactureItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Killowatts = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionPackManufactureItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OptionPackId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VehicleOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionPackManufactureItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionPacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionPacks", x => x.Id);
                });

            var engine1Id = Guid.NewGuid();
            var engine2Id = Guid.NewGuid();
            var engine3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "SerialNumber", "Model", "Type", "Killowatts"],
                [engine1Id, "SRE000001", "EP1", "Petrol 1.2L", 90]);

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "SerialNumber", "Model", "Type", "Killowatts"],
                [engine2Id, "SRE000002", "EP2", "Petrol 1.6L", 120]);

            migrationBuilder.InsertData(
                "Engines",
                ["Id", "SerialNumber", "Model", "Type", "Killowatts"],
                [engine3Id, "SRE000003", "ED1", "Diesel 1.5L", 120]);

            var chassis1Id = Guid.NewGuid();
            var chassis2Id = Guid.NewGuid();
            var chassis3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "SerialNumber", "Model", "Type"],
                [chassis1Id, "SRC000001", "Model 1", "Hatchback"]);

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "SerialNumber", "Model", "Type"],
                [chassis2Id, "SRC000002", "Model 2", "Sedan"]);

            migrationBuilder.InsertData(
                "AllChassis",
                ["Id", "SerialNumber", "Model", "Type"],
                [chassis3Id, "SRC000003", "Model 3", "SUV"]);

            var optionPack1Id = Guid.NewGuid();
            var optionPack2Id = Guid.NewGuid();
            var optionPack3Id = Guid.NewGuid();

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "SerialNumber", "Name"],
                [optionPack1Id, "SRO000001", "Comfort"]);

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "SerialNumber", "Name"],
                [optionPack2Id, "SRO000002", "Elegance"]);

            migrationBuilder.InsertData(
                "OptionPacks",
                ["Id", "SerialNumber", "Name"],
                [optionPack3Id, "SRO000003", "Executive"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllChassis");

            migrationBuilder.DropTable(
                name: "ChassisManufactureItems");

            migrationBuilder.DropTable(
                name: "EngineManufactureItems");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "OptionPackManufactureItems");

            migrationBuilder.DropTable(
                name: "OptionPacks");
        }
    }
}
