using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class AddJobIdToJobListFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // İşlemler kaldırıldı, çünkü zaten var.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Geri alma işlemi yok, çünkü Up metodunda işlem yapılmadı.
        }
    }
}
