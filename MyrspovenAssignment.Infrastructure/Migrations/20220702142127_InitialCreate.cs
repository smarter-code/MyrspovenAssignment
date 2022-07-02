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
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSetStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SquaredMeters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignalData",
                columns: table => new
                {
                    Value = table.Column<double>(type: "float", nullable: false),
                    ReadUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignalData", x => new { x.BuildingId, x.Value, x.ReadUtc });
                    table.ForeignKey(
                        name: "FK_SignalData_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignalData");

            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
