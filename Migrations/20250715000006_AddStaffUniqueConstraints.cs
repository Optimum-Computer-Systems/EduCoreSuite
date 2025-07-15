using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add unique constraints for Staff table
            migrationBuilder.CreateIndex(
                name: "IX_Staff_FullName",
                table: "Staff",
                column: "FullName",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_StaffNumber",
                table: "Staff",
                column: "StaffNumber",
                unique: true,
                filter: "[StaffNumber] IS NOT NULL AND [IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove unique constraints
            migrationBuilder.DropIndex(
                name: "IX_Staff_FullName",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_StaffNumber",
                table: "Staff");
        }
    }
}