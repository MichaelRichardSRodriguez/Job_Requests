using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Modify_DeleteRestriction_In_JobPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_UpdatedUserId",
                table: "JobPriority");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_UpdatedUserId",
                table: "JobPriority",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_UpdatedUserId",
                table: "JobPriority");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_UpdatedUserId",
                table: "JobPriority",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
