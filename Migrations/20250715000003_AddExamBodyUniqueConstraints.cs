using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddExamBodyUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add unique constraint for ExamBody name
            migrationBuilder.CreateIndex(
                name: "IX_ExamBodies_Name",
                table: "ExamBodies",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove unique constraint
            migrationBuilder.DropIndex(
                name: "IX_ExamBodies_Name",
                table: "ExamBodies");
        }
    }
}