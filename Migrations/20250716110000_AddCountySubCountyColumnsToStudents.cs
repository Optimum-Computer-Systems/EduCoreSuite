using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCoreSuite.Migrations
{
    public partial class AddCountySubCountyColumnsToStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if CountyID column exists, if not add it
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'CountyID' AND Object_ID = Object_ID(N'Students'))
                BEGIN
                    ALTER TABLE Students ADD CountyID int NULL;
                END
            ");

            // Check if SubCountyID column exists, if not add it
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'SubCountyID' AND Object_ID = Object_ID(N'Students'))
                BEGIN
                    ALTER TABLE Students ADD SubCountyID int NULL;
                END
            ");

            // Create indexes if they don't exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_CountyID' AND object_id = OBJECT_ID('Students'))
                BEGIN
                    CREATE INDEX IX_Students_CountyID ON Students(CountyID);
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_SubCountyID' AND object_id = OBJECT_ID('Students'))
                BEGIN
                    CREATE INDEX IX_Students_SubCountyID ON Students(SubCountyID);
                END
            ");

            // Add foreign key constraints if they don't exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_Counties_CountyID')
                BEGIN
                    ALTER TABLE Students ADD CONSTRAINT FK_Students_Counties_CountyID 
                    FOREIGN KEY (CountyID) REFERENCES Counties(CountyID);
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_SubCounties_SubCountyID')
                BEGIN
                    ALTER TABLE Students ADD CONSTRAINT FK_Students_SubCounties_SubCountyID 
                    FOREIGN KEY (SubCountyID) REFERENCES SubCounties(SubCountyID);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove foreign key constraints if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_Counties_CountyID')
                BEGIN
                    ALTER TABLE Students DROP CONSTRAINT FK_Students_Counties_CountyID;
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_SubCounties_SubCountyID')
                BEGIN
                    ALTER TABLE Students DROP CONSTRAINT FK_Students_SubCounties_SubCountyID;
                END
            ");

            // Remove indexes if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_CountyID' AND object_id = OBJECT_ID('Students'))
                BEGIN
                    DROP INDEX IX_Students_CountyID ON Students;
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_SubCountyID' AND object_id = OBJECT_ID('Students'))
                BEGIN
                    DROP INDEX IX_Students_SubCountyID ON Students;
                END
            ");

            // Remove columns if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'CountyID' AND Object_ID = Object_ID(N'Students'))
                BEGIN
                    ALTER TABLE Students DROP COLUMN CountyID;
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'SubCountyID' AND Object_ID = Object_ID(N'Students'))
                BEGIN
                    ALTER TABLE Students DROP COLUMN SubCountyID;
                END
            ");
        }
    }
}