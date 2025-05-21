using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class InitDataCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SatisfactionSurveys_Departments_DepartmentId",
                table: "SatisfactionSurveys");

            migrationBuilder.DropIndex(
                name: "IX_SatisfactionSurveys_DepartmentId",
                table: "SatisfactionSurveys");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "SatisfactionSurveys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "SatisfactionSurveys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SatisfactionSurveys_DepartmentId",
                table: "SatisfactionSurveys",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SatisfactionSurveys_Departments_DepartmentId",
                table: "SatisfactionSurveys",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
