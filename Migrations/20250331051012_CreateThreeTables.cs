using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoilReportApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateThreeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_CropStage_CropStageId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Crop_CropId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_SoilType_SoilTypeId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoilType",
                table: "SoilType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CropStage",
                table: "CropStage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crop",
                table: "Crop");

            migrationBuilder.RenameTable(
                name: "SoilType",
                newName: "SoilTypes");

            migrationBuilder.RenameTable(
                name: "CropStage",
                newName: "CropStages");

            migrationBuilder.RenameTable(
                name: "Crop",
                newName: "Crops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoilTypes",
                table: "SoilTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CropStages",
                table: "CropStages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crops",
                table: "Crops",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoilTypes",
                table: "SoilTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CropStages",
                table: "CropStages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Crops",
                table: "Crops");

            migrationBuilder.RenameTable(
                name: "SoilTypes",
                newName: "SoilType");

            migrationBuilder.RenameTable(
                name: "CropStages",
                newName: "CropStage");

            migrationBuilder.RenameTable(
                name: "Crops",
                newName: "Crop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoilType",
                table: "SoilType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CropStage",
                table: "CropStage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Crop",
                table: "Crop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CropStage_CropStageId",
                table: "Requests",
                column: "CropStageId",
                principalTable: "CropStage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Crop_CropId",
                table: "Requests",
                column: "CropId",
                principalTable: "Crop",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_SoilType_SoilTypeId",
                table: "Requests",
                column: "SoilTypeId",
                principalTable: "SoilType",
                principalColumn: "Id");
        }
    }
}
