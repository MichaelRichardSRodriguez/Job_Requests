using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Modify_CreateByUserId_To_CreateUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedByUserId",
                table: "JobPriority");

            migrationBuilder.DropColumn(
                name: "Created By",
                table: "JobPriority");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "JobPriority",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPriority_CreatedByUserId",
                table: "JobPriority",
                newName: "IX_JobPriority_CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JobPriority",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPriority_CreatedUserId",
                table: "JobPriority",
                newName: "IX_JobPriority_CreatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "Created By",
                table: "JobPriority",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedByUserId",
                table: "JobPriority",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
