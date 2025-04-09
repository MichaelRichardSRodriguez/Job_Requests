using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Add_Nullable_CreatedByUserId_In_JobPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Created By",
                table: "JobPriority",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "JobPriority",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPriority_CreatedByUserId",
                table: "JobPriority",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedByUserId",
                table: "JobPriority",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPriority_AspNetUsers_CreatedByUserId",
                table: "JobPriority");

            migrationBuilder.DropIndex(
                name: "IX_JobPriority_CreatedByUserId",
                table: "JobPriority");

            migrationBuilder.DropColumn(
                name: "Created By",
                table: "JobPriority");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "JobPriority");
        }
    }
}
