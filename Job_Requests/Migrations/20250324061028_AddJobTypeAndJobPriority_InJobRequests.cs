using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class AddJobTypeAndJobPriority_InJobRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobPriorityId",
                table: "JobRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobTypeId",
                table: "JobRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_JobPriorityId",
                table: "JobRequests",
                column: "JobPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_JobTypeId",
                table: "JobRequests",
                column: "JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_JobPriority_JobPriorityId",
                table: "JobRequests",
                column: "JobPriorityId",
                principalTable: "JobPriority",
                principalColumn: "JobPriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_JobTypes_JobTypeId",
                table: "JobRequests",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "JobTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_JobPriority_JobPriorityId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_JobTypes_JobTypeId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_JobPriorityId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_JobTypeId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "JobPriorityId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "JobTypeId",
                table: "JobRequests");
        }
    }
}
