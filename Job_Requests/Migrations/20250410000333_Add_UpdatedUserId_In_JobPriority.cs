using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Add_UpdatedUserId_In_JobPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "JobPriority",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "JobPriority",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPriority_UpdatedUserId",
                table: "JobPriority",
                column: "UpdatedUserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_UpdatedUserId",
                table: "JobPriority");

            migrationBuilder.DropIndex(
                name: "IX_JobPriority_UpdatedUserId",
                table: "JobPriority");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "JobPriority");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "JobPriority",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
