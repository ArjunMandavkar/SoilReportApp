using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoilReportApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsersInRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpertId",
                table: "Requests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FarmerId",
                table: "Requests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ExpertId",
                table: "Requests",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_FarmerId",
                table: "Requests",
                column: "FarmerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_ExpertId",
                table: "Requests",
                column: "ExpertId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_FarmerId",
                table: "Requests",
                column: "FarmerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_ExpertId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_FarmerId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ExpertId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_FarmerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ExpertId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Requests");
        }
    }
}
