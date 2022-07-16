using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Learning_I3332_MVC.Migrations
{
    public partial class ChangedRelationsh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Specializations_C_SpecializationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_SC_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_SC_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specializations_S_SpecializationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_S_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Specializations_T_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_T_UserId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teaches_Courses_T_CourseId",
                table: "Teaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teaches_Teachers_T_TeacherId",
                table: "Teaches");

            migrationBuilder.RenameColumn(
                name: "T_TeacherId",
                table: "Teaches",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "T_CourseId",
                table: "Teaches",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Teaches_T_TeacherId",
                table: "Teaches",
                newName: "IX_Teaches_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Teaches_T_CourseId",
                table: "Teaches",
                newName: "IX_Teaches_CourseId");

            migrationBuilder.RenameColumn(
                name: "T_UserId",
                table: "Teachers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "T_SpecializationId",
                table: "Teachers",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_T_UserId",
                table: "Teachers",
                newName: "IX_Teachers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_T_SpecializationId",
                table: "Teachers",
                newName: "IX_Teachers_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "S_UserId",
                table: "Students",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "S_SpecializationId",
                table: "Students",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_S_UserId",
                table: "Students",
                newName: "IX_Students_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_S_SpecializationId",
                table: "Students",
                newName: "IX_Students_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "SC_StudentId",
                table: "StudentCourses",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "SC_CourseId",
                table: "StudentCourses",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_SC_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_SC_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_CourseId");

            migrationBuilder.RenameColumn(
                name: "C_SpecializationId",
                table: "Courses",
                newName: "SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_C_SpecializationId",
                table: "Courses",
                newName: "IX_Courses_SpecializationId");

            migrationBuilder.AddColumn<int>(
                name: "TSpecializationId",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Specializations_SpecializationId",
                table: "Courses",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specializations_SpecializationId",
                table: "Students",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teaches_Courses_CourseId",
                table: "Teaches",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teaches_Teachers_TeacherId",
                table: "Teaches",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Specializations_SpecializationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CourseId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Specializations_SpecializationId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Users_UserId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Specializations_SpecializationId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Users_UserId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teaches_Courses_CourseId",
                table: "Teaches");

            migrationBuilder.DropForeignKey(
                name: "FK_Teaches_Teachers_TeacherId",
                table: "Teaches");

            migrationBuilder.DropColumn(
                name: "TSpecializationId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Teaches",
                newName: "T_TeacherId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Teaches",
                newName: "T_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Teaches_TeacherId",
                table: "Teaches",
                newName: "IX_Teaches_T_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Teaches_CourseId",
                table: "Teaches",
                newName: "IX_Teaches_T_CourseId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Teachers",
                newName: "T_UserId");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Teachers",
                newName: "T_SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_UserId",
                table: "Teachers",
                newName: "IX_Teachers_T_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_SpecializationId",
                table: "Teachers",
                newName: "IX_Teachers_T_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "S_UserId");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Students",
                newName: "S_SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_UserId",
                table: "Students",
                newName: "IX_Students_S_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_SpecializationId",
                table: "Students",
                newName: "IX_Students_S_SpecializationId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentCourses",
                newName: "SC_StudentId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "StudentCourses",
                newName: "SC_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_SC_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_SC_CourseId");

            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Courses",
                newName: "C_SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_SpecializationId",
                table: "Courses",
                newName: "IX_Courses_C_SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Specializations_C_SpecializationId",
                table: "Courses",
                column: "C_SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_SC_CourseId",
                table: "StudentCourses",
                column: "SC_CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_SC_StudentId",
                table: "StudentCourses",
                column: "SC_StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Specializations_S_SpecializationId",
                table: "Students",
                column: "S_SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Users_S_UserId",
                table: "Students",
                column: "S_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Specializations_T_SpecializationId",
                table: "Teachers",
                column: "T_SpecializationId",
                principalTable: "Specializations",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Users_T_UserId",
                table: "Teachers",
                column: "T_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teaches_Courses_T_CourseId",
                table: "Teaches",
                column: "T_CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teaches_Teachers_T_TeacherId",
                table: "Teaches",
                column: "T_TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
