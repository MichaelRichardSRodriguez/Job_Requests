using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Add_CreatedByAndUpdatedBy_In_JobRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUserId",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_AssignedTo",
                table: "JobRequests",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_CreatedUserId",
                table: "JobRequests",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_UpdatedUserId",
                table: "JobRequests",
                column: "UpdatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_AspNetUsers_AssignedTo",
                table: "JobRequests",
                column: "AssignedTo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_AspNetUsers_CreatedUserId",
                table: "JobRequests",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_AspNetUsers_UpdatedUserId",
                table: "JobRequests",
                column: "UpdatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_AspNetUsers_AssignedTo",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_AspNetUsers_CreatedUserId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_AspNetUsers_UpdatedUserId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_AssignedTo",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_CreatedUserId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_UpdatedUserId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedUserId",
                table: "JobRequests");
        }
    }
}
