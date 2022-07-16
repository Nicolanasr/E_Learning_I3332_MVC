using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_I3332_MVC.Migrations
{
    public partial class TeacherSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TSpecializationId",
                table: "Teachers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TSpecializationId",
                table: "Teachers",
                type: "int",
                nullable: true);
        }
    }
}
