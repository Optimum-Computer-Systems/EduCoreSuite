using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddCampusUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add unique constraints for Campus table
            migrationBuilder.CreateIndex(
                name: "IX_Campuses_Code",
                table: "Campuses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_Name",
                table: "Campuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_Email",
                table: "Campuses",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_PhoneNumber",
                table: "Campuses",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove unique constraints
            migrationBuilder.DropIndex(
                name: "IX_Campuses_Code",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_Name",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_Email",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_PhoneNumber",
                table: "Campuses");
        }
    }
}