using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schoole.Migrations
{
    /// <inheritdoc />
    public partial class AddLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_teacherID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassroomID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ClassroomID",
                table: "Students",
                newName: "ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassroomID",
                table: "Students",
                newName: "IX_Students_ClassroomId");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Grades",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Grades",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentID",
                table: "Grades",
                newName: "IX_Grades_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_CourseID",
                table: "Grades",
                newName: "IX_Grades_CourseId");

            migrationBuilder.RenameColumn(
                name: "teacherID",
                table: "Courses",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_teacherID",
                table: "Courses",
                newName: "IX_Courses_TeacherId");

            migrationBuilder.CreateTable(
                name: "LogModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogModels", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassroomId",
                table: "Students",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classrooms_ClassroomId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "LogModels");

            migrationBuilder.RenameColumn(
                name: "ClassroomId",
                table: "Students",
                newName: "ClassroomID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ClassroomId",
                table: "Students",
                newName: "IX_Students_ClassroomID");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Grades",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Grades",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                newName: "IX_Grades_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                newName: "IX_Grades_CourseID");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Courses",
                newName: "teacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                newName: "IX_Courses_teacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_teacherID",
                table: "Courses",
                column: "teacherID",
                principalTable: "Teachers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseID",
                table: "Grades",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classrooms_ClassroomID",
                table: "Students",
                column: "ClassroomID",
                principalTable: "Classrooms",
                principalColumn: "ID");
        }
    }
}
