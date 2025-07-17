using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    public partial class UpdateCampusWithCountyAndSubCountyIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add new CountyID and SubCountyID columns
            migrationBuilder.AddColumn<int>(
                name: "CountyID",
                table: "Campuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubCountyID",
                table: "Campuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Create indexes for the new columns
            migrationBuilder.CreateIndex(
                name: "IX_Campuses_CountyID",
                table: "Campuses",
                column: "CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_SubCountyID",
                table: "Campuses",
                column: "SubCountyID");

            // Add foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Campuses_Counties_CountyID",
                table: "Campuses",
                column: "CountyID",
                principalTable: "Counties",
                principalColumn: "CountyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campuses_SubCounties_SubCountyID",
                table: "Campuses",
                column: "SubCountyID",
                principalTable: "SubCounties",
                principalColumn: "SubCountyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_Counties_CountyID",
                table: "Campuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_SubCounties_SubCountyID",
                table: "Campuses");

            // Drop indexes
            migrationBuilder.DropIndex(
                name: "IX_Campuses_CountyID",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_SubCountyID",
                table: "Campuses");

            // Drop columns
            migrationBuilder.DropColumn(
                name: "CountyID",
                table: "Campuses");

            migrationBuilder.DropColumn(
                name: "SubCountyID",
                table: "Campuses");
        }
    }
}