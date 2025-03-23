using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class removeDepartmentId_FromJobRequests2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_DepartmentId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "JobRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "JobRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_DepartmentId",
                table: "JobRequests",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }
    }
}
