using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class modifyOnDeleteRestriction_InDepartments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Departments_DepartmentId",
                table: "JobRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
