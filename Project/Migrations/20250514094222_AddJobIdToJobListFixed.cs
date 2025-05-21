using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class AddJobIdToJobListFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobLists_JobId",
                table: "JobLists",
                column: "JobId");

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

            migrationBuilder.DropIndex(
                name: "IX_JobLists_JobId",
                table: "JobLists");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobLists");
        }
    }
}
