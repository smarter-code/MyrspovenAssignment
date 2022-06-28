using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyrspovenAssignment.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSetStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    squaredMeters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    DataUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignalData_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    ExternalBuildingId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signals_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignalData_BuildingId",
                table: "SignalData",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Signals_BuildingId",
                table: "Signals",
                column: "BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignalData");

            migrationBuilder.DropTable(
                name: "Signals");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
