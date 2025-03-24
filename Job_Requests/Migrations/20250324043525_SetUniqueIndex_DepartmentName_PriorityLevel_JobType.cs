using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job_Requests.Migrations
{
    /// <inheritdoc />
    public partial class SetUniqueIndex_DepartmentName_PriorityLevel_JobType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JobTypes_JobTypeName",
                table: "JobTypes",
                column: "JobTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPriority_PriorityLevel",
                table: "JobPriority",
                column: "PriorityLevel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentName",
                table: "Departments",
                column: "DepartmentName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobTypes_JobTypeName",
                table: "JobTypes");

            migrationBuilder.DropIndex(
                name: "IX_JobPriority_PriorityLevel",
                table: "JobPriority");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentName",
                table: "Departments");
        }
    }
}
