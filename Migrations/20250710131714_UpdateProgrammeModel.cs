using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgrammeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccreditationDate",
                table: "Programmes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccreditedBy",
                table: "Programmes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Programmes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Programmes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Programmes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationYears",
                table: "Programmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Programmes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Programmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SemestersPerYear",
                table: "Programmes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Programmes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccreditationDate",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "AccreditedBy",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "DurationYears",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "SemestersPerYear",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Programmes");
        }
    }
}
