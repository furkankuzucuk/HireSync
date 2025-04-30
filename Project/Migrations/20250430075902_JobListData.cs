using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class JobListData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "JobApplications",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "JobLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "JobLists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JobApplications",
                newName: "CandidateId");
        }
    }
}
