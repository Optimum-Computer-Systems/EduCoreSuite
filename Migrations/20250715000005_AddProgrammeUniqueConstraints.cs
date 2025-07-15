using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddProgrammeUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add unique constraints for Programme table
            migrationBuilder.CreateIndex(
                name: "IX_Programmes_Name",
                table: "Programmes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_Code",
                table: "Programmes",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove unique constraints
            migrationBuilder.DropIndex(
                name: "IX_Programmes_Name",
                table: "Programmes");

            migrationBuilder.DropIndex(
                name: "IX_Programmes_Code",
                table: "Programmes");
        }
    }
}