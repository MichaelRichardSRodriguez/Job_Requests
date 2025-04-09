using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Modify_Nullable_CreateUserId_To_NonNullable : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedUserId",
                table: "JobPriority",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
