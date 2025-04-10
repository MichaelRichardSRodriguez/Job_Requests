using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedByAndUpdatedBy_CreatedDateAndUpdatedDate_In_Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Departments",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Departments",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CreatedUserId",
                table: "Departments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_UpdatedUserId",
                table: "Departments",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedUserId",
                table: "Departments",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_UpdatedUserId",
                table: "Departments",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_CreatedUserId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_UpdatedUserId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_CreatedUserId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_UpdatedUserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "Departments");
        }
    }
}
