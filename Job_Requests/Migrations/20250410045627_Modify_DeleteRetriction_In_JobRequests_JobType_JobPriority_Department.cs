using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class Modify_DeleteRetriction_In_JobRequests_JobType_JobPriority_Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_JobPriority_JobPriorityId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_JobTypes_JobTypeId",
                table: "JobRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_JobPriority_JobPriorityId",
                table: "JobRequests",
                column: "JobPriorityId",
                principalTable: "JobPriority",
                principalColumn: "JobPriorityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_JobTypes_JobTypeId",
                table: "JobRequests",
                column: "JobTypeId",
                principalTable: "JobTypes",
                principalColumn: "JobTypeId",
                onDelete: ReferentialAction.Restrict);
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
    }
}
