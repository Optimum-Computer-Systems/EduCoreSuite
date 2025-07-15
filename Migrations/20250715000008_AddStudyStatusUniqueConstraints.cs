using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyStatusUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add unique constraint for StudyStatus name
            migrationBuilder.CreateIndex(
                name: "IX_StudyStatuses_Name",
                table: "StudyStatuses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove unique constraint
            migrationBuilder.DropIndex(
                name: "IX_StudyStatuses_Name",
                table: "StudyStatuses");
        }
    }
}