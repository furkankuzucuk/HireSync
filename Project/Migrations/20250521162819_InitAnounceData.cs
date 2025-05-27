using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
<<<<<<< Updated upstream:Project/Migrations/20250521162819_InitAnounceData.cs
    /// <inheritdoc />
    public partial class InitAnounceData : Migration
=======
    public partial class AddJobIdToJobListFixed : Migration
>>>>>>> Stashed changes:Project/Migrations/20250514094222_AddJobIdToJobListFixed.cs
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
<<<<<<< Updated upstream:Project/Migrations/20250521162819_InitAnounceData.cs
            migrationBuilder.DropForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists");

            migrationBuilder.AddForeignKey(
                name: "FK_JobLists_Jobs_JobId",
                table: "JobLists",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId");
=======
            // İşlemler kaldırıldı, çünkü zaten var.
>>>>>>> Stashed changes:Project/Migrations/20250514094222_AddJobIdToJobListFixed.cs
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
<<<<<<< Updated upstream:Project/Migrations/20250521162819_InitAnounceData.cs
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
=======
            // Geri alma işlemi yok, çünkü Up metodunda işlem yapılmadı.
>>>>>>> Stashed changes:Project/Migrations/20250514094222_AddJobIdToJobListFixed.cs
        }
    }
}
