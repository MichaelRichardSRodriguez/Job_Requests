using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedByAndUpdatedBy_In_JobType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateUserId",
                table: "JobTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateUserId",
                table: "JobTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_CreateUserId",
                table: "JobTypes",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_UpdateUserId",
                table: "JobTypes",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTypes_AspNetUsers_CreateUserId",
                table: "JobTypes",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobTypes_AspNetUsers_UpdateUserId",
                table: "JobTypes",
                column: "UpdateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTypes_AspNetUsers_CreateUserId",
                table: "JobTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_JobTypes_AspNetUsers_UpdateUserId",
                table: "JobTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobTypes_CreateUserId",
                table: "JobTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobTypes_UpdateUserId",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "JobTypes");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "JobTypes");
        }
    }
}
