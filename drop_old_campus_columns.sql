-- Drop the old County and Constituency columns from Campuses table
IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'County' AND Object_ID = Object_ID(N'Campuses'))
BEGIN
    ALTER TABLE Campuses DROP COLUMN County;
    PRINT 'Dropped County column from Campuses table';
END
ELSE
BEGIN
    PRINT 'County column does not exist in Campuses table';
END

IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'Constituency' AND Object_ID = Object_ID(N'Campuses'))
BEGIN
    ALTER TABLE Campuses DROP COLUMN Constituency;
    PRINT 'Dropped Constituency column from Campuses table';
END
ELSE
BEGIN
    PRINT 'Constituency column does not exist in Campuses table';
END