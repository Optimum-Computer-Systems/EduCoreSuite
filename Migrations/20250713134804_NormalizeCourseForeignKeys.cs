using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeCourseForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Campus",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExamBody",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Programme",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyLevel",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyStatus",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "Courses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CampusID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamBodyID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgrammeID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyModeID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyStatusID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CampusID",
                table: "Courses",
                column: "CampusID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ExamBodyID",
                table: "Courses",
                column: "ExamBodyID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgrammeID",
                table: "Courses",
                column: "ProgrammeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudyModeID",
                table: "Courses",
                column: "StudyModeID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudyStatusID",
                table: "Courses",
                column: "StudyStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Campuses_CampusID",
                table: "Courses",
                column: "CampusID",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ExamBodies_ExamBodyID",
                table: "Courses",
                column: "ExamBodyID",
                principalTable: "ExamBodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Programmes_ProgrammeID",
                table: "Courses",
                column: "ProgrammeID",
                principalTable: "Programmes",
                principalColumn: "ProgrammeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_StudyModes_StudyModeID",
                table: "Courses",
                column: "StudyModeID",
                principalTable: "StudyModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_StudyStatuses_StudyStatusID",
                table: "Courses",
                column: "StudyStatusID",
                principalTable: "StudyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Campuses_CampusID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Departments_DepartmentID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ExamBodies_ExamBodyID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Programmes_ProgrammeID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_StudyModes_StudyModeID",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_StudyStatuses_StudyStatusID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CampusID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ExamBodyID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProgrammeID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudyModeID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudyStatusID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CampusID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ExamBodyID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProgrammeID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyModeID",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyStatusID",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Campus",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExamBody",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Programme",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudyLevel",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudyStatus",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
