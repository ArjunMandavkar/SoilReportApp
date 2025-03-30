using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoilReportApp.Migrations
{
    /// <inheritdoc />
    public partial class RequestFKNullable : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "SoilTypeId",
                table: "Requests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CropStageId",
                table: "Requests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CropId",
                table: "Requests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "SoilTypeId",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CropStageId",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CropId",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_CropStage_CropStageId",
                table: "Requests",
                column: "CropStageId",
                principalTable: "CropStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Crop_CropId",
                table: "Requests",
                column: "CropId",
                principalTable: "Crop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_SoilType_SoilTypeId",
                table: "Requests",
                column: "SoilTypeId",
                principalTable: "SoilType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
