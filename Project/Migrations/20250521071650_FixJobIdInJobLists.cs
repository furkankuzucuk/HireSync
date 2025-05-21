using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class FixJobIdInJobLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
