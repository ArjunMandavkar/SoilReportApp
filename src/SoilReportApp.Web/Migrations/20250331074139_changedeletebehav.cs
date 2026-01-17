using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoilReportApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class changedeletebehav : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CropStages_CropStageId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Crops_CropId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_SoilTypes_SoilTypeId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CropStages_CropStageId",
                table: "Requests",
                column: "CropStageId",
                principalTable: "CropStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Crops_CropId",
                table: "Requests",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_SoilTypes_SoilTypeId",
                table: "Requests",
                column: "SoilTypeId",
                principalTable: "SoilTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CropStages_CropStageId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Crops_CropId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_SoilTypes_SoilTypeId",
                table: "Requests");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CropStages_CropStageId",
                table: "Requests",
                column: "CropStageId",
                principalTable: "CropStages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Crops_CropId",
                table: "Requests",
                column: "CropId",
                principalTable: "Crops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_SoilTypes_SoilTypeId",
                table: "Requests",
                column: "SoilTypeId",
                principalTable: "SoilTypes",
                principalColumn: "Id");
        }
    }
}
