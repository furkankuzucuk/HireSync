using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialCandidateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobApplications",
                newName: "JobListId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                newName: "IX_JobApplications_JobListId");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Logins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CandidateId",
                table: "JobApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobLists_JobListId",
                table: "JobApplications",
                column: "JobListId",
                principalTable: "JobLists",
                principalColumn: "JobListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobLists_JobListId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "JobApplications");

            migrationBuilder.RenameColumn(
                name: "JobListId",
                table: "JobApplications",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobListId",
                table: "JobApplications",
                newName: "IX_JobApplications_JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
