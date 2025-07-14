using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    public partial class AddCountyAndSubCountyFKsToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1️⃣  Add new FK columns as NULLABLE (no default)
            migrationBuilder.AddColumn<int>(
                name: "CountyID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCountyID",
                table: "Students",
                type: "int",
                nullable: true);

            // 2️⃣  Copy existing data (if any) from name → ID
            //     ⚠️ Requires Students.County / Students.SubCounty columns to exist
            migrationBuilder.Sql(@"
                -- County
                UPDATE  s
                SET     CountyID = c.CountyID
                FROM    Students s
                JOIN    Counties c ON c.CountyName = s.County;

                -- Sub‑county
                UPDATE  s
                SET     SubCountyID = sc.SubCountyID
                FROM    Students s
                JOIN    SubCounties sc ON sc.SubCountyName = s.SubCounty;
            ");

            // 3️⃣  Make the columns NOT NULL (will succeed only if all rows now have IDs)
            migrationBuilder.Sql(@"
                -- Fail gracefully if any NULLs remain
                IF EXISTS (SELECT 1 FROM Students WHERE CountyID IS NULL OR SubCountyID IS NULL)
                    RAISERROR ('Some students could not be matched to County/Sub‑county IDs. Fix the data and rerun the migration.', 16, 1);
            ");

            migrationBuilder.AlterColumn<int>(
                name: "CountyID",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubCountyID",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 4️⃣  Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Students_CountyID",
                table: "Students",
                column: "CountyID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SubCountyID",
                table: "Students",
                column: "SubCountyID");

            // 5️⃣  Add FK constraints (Restrict delete to keep data safe)
            migrationBuilder.AddForeignKey(
                name: "FK_Students_Counties_CountyID",
                table: "Students",
                column: "CountyID",
                principalTable: "Counties",
                principalColumn: "CountyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubCounties_SubCountyID",
                table: "Students",
                column: "SubCountyID",
                principalTable: "SubCounties",
                principalColumn: "SubCountyID",
                onDelete: ReferentialAction.Restrict);

            // 6️⃣  Drop the old varchar columns
            migrationBuilder.DropColumn(name: "County", table: "Students");
            migrationBuilder.DropColumn(name: "SubCounty", table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Roll‑back: add text columns, drop FKs, drop indexes, drop ID columns

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubCounty",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropForeignKey("FK_Students_Counties_CountyID", "Students");
            migrationBuilder.DropForeignKey("FK_Students_SubCounties_SubCountyID", "Students");

            migrationBuilder.DropIndex("IX_Students_CountyID", "Students");
            migrationBuilder.DropIndex("IX_Students_SubCountyID", "Students");

            migrationBuilder.DropColumn("CountyID", "Students");
            migrationBuilder.DropColumn("SubCountyID", "Students");
        }
    }
}
