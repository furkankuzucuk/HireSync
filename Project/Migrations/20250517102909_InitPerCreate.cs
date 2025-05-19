using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class InitPerCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Users_UserId",
                table: "PerformanceReviews");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PerformanceReviews",
                newName: "UserExamId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceReviews_UserId",
                table: "PerformanceReviews",
                newName: "IX_PerformanceReviews_UserExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_UserExams_UserExamId",
                table: "PerformanceReviews",
                column: "UserExamId",
                principalTable: "UserExams",
                principalColumn: "UserExamId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_UserExams_UserExamId",
                table: "PerformanceReviews");

            migrationBuilder.RenameColumn(
                name: "UserExamId",
                table: "PerformanceReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceReviews_UserExamId",
                table: "PerformanceReviews",
                newName: "IX_PerformanceReviews_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Users_UserId",
                table: "PerformanceReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
