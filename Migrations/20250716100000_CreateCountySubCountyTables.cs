using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    /// <summary>
    /// Migration to create Counties and SubCounties tables
    /// </summary>
    public partial class CreateCountySubCountyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if Counties table exists, if not create it
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Counties')
                BEGIN
                    CREATE TABLE Counties (
                        CountyID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
                        CountyName nvarchar(100) NOT NULL
                    );
                END
            ");

            // Check if SubCounties table exists, if not create it
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SubCounties')
                BEGIN
                    CREATE TABLE SubCounties (
                        SubCountyID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
                        SubCountyName nvarchar(100) NOT NULL,
                        CountyID int NOT NULL,
                        CONSTRAINT FK_SubCounties_Counties FOREIGN KEY (CountyID) REFERENCES Counties(CountyID)
                    );
                END
            ");

            // Add some sample data if the tables are empty
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM Counties)
                BEGIN
                    INSERT INTO Counties (CountyName) VALUES ('Nairobi');
                    INSERT INTO Counties (CountyName) VALUES ('Mombasa');
                    INSERT INTO Counties (CountyName) VALUES ('Kisumu');
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM SubCounties)
                BEGIN
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Westlands', 1);
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Embakasi', 1);
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Nyali', 2);
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisauni', 2);
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisumu Central', 3);
                    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisumu West', 3);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop SubCounties table if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SubCounties')
                BEGIN
                    DROP TABLE SubCounties;
                END
            ");

            // Drop Counties table if it exists
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Counties')
                BEGIN
                    DROP TABLE Counties;
                END
            ");
        }
    }
}