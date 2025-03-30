using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoilReportApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatedRequestDomainObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CropStage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NAvg = table.Column<double>(type: "double precision", nullable: false),
                    PAvg = table.Column<double>(type: "double precision", nullable: false),
                    KAvg = table.Column<double>(type: "double precision", nullable: false),
                    MoistureAvg = table.Column<double>(type: "double precision", nullable: false),
                    SoilTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CropId = table.Column<Guid>(type: "uuid", nullable: false),
                    CropStageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Report = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_CropStage_CropStageId",
                        column: x => x.CropStageId,
                        principalTable: "CropStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Crop_CropId",
                        column: x => x.CropId,
                        principalTable: "Crop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_SoilType_SoilTypeId",
                        column: x => x.SoilTypeId,
                        principalTable: "SoilType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Readings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Test = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    N = table.Column<double>(type: "double precision", nullable: false),
                    P = table.Column<double>(type: "double precision", nullable: false),
                    K = table.Column<double>(type: "double precision", nullable: false),
                    Moisture = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Readings_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Readings_RequestId",
                table: "Readings",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CropId",
                table: "Requests",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CropStageId",
                table: "Requests",
                column: "CropStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SoilTypeId",
                table: "Requests",
                column: "SoilTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Readings");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "CropStage");

            migrationBuilder.DropTable(
                name: "Crop");

            migrationBuilder.DropTable(
                name: "SoilType");
        }
    }
}
