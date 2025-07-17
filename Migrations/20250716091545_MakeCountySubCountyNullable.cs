using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class MakeCountySubCountyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Counties_CountyID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubCounties_SubCountyID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SubCountyID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountyID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Counties_CountyID",
                table: "Students",
                column: "CountyID",
                principalTable: "Counties",
                principalColumn: "CountyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubCounties_SubCountyID",
                table: "Students",
                column: "SubCountyID",
                principalTable: "SubCounties",
                principalColumn: "SubCountyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Counties_CountyID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_SubCounties_SubCountyID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "SubCountyID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountyID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Counties_CountyID",
                table: "Students",
                column: "CountyID",
                principalTable: "Counties",
                principalColumn: "CountyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_SubCounties_SubCountyID",
                table: "Students",
                column: "SubCountyID",
                principalTable: "SubCounties",
                principalColumn: "SubCountyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
