using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestingAndReceivingDepartment_InJobRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceivingDepartmentId",
                table: "JobRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestingDepartmentId",
                table: "JobRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_ReceivingDepartmentId",
                table: "JobRequests",
                column: "ReceivingDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_RequestingDepartmentId",
                table: "JobRequests",
                column: "RequestingDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Departments_ReceivingDepartmentId",
                table: "JobRequests",
                column: "ReceivingDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Departments_RequestingDepartmentId",
                table: "JobRequests",
                column: "RequestingDepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Departments_ReceivingDepartmentId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Departments_RequestingDepartmentId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_ReceivingDepartmentId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_RequestingDepartmentId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "ReceivingDepartmentId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "RequestingDepartmentId",
                table: "JobRequests");
        }
    }
}
