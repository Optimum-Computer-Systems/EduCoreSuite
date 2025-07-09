using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyModeStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyModes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyModes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "StudyModes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Daytime attendance on campus", "Full-Time" },
                    { 2, "Evening/weekend attendance", "Part-Time" },
                    { 3, "Remote/online learning", "Distance Learning" }
                });

            migrationBuilder.InsertData(
                table: "StudyStatuses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Currently enrolled", "Active" },
                    { 2, "Graduated successfully", "Completed" },
                    { 3, "Repeating a class or year", "Repeating" },
                    { 4, "Exited before completion", "Withdrawn" },
                    { 5, "Temporarily barred for discipline", "Suspended" },
                    { 6, "Permanently removed from programme", "Expelled" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyModes");

            migrationBuilder.DropTable(
                name: "StudyStatuses");
        }
    }
}
