-- Create Counties table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Counties')
BEGIN
    CREATE TABLE Counties (
        CountyID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        CountyName nvarchar(100) NOT NULL
    );
    
    -- Insert sample data
    INSERT INTO Counties (CountyName) VALUES ('Nairobi');
    INSERT INTO Counties (CountyName) VALUES ('Mombasa');
    INSERT INTO Counties (CountyName) VALUES ('Kisumu');
    INSERT INTO Counties (CountyName) VALUES ('Nakuru');
    INSERT INTO Counties (CountyName) VALUES ('Kiambu');
END

-- Create SubCounties table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SubCounties')
BEGIN
    CREATE TABLE SubCounties (
        SubCountyID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
        SubCountyName nvarchar(100) NOT NULL,
        CountyID int NOT NULL,
        CONSTRAINT FK_SubCounties_Counties FOREIGN KEY (CountyID) REFERENCES Counties(CountyID)
    );
    
    -- Insert sample data
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Westlands', 1);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Embakasi', 1);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Nyali', 2);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisauni', 2);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisumu Central', 3);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kisumu West', 3);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Nakuru East', 4);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Nakuru West', 4);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kiambaa', 5);
    INSERT INTO SubCounties (SubCountyName, CountyID) VALUES ('Kikuyu', 5);
END

-- Add CountyID and SubCountyID columns to Students table if they don't exist
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'CountyID' AND Object_ID = Object_ID(N'Students'))
BEGIN
    ALTER TABLE Students ADD CountyID int NULL;
END

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'SubCountyID' AND Object_ID = Object_ID(N'Students'))
BEGIN
    ALTER TABLE Students ADD SubCountyID int NULL;
END

-- Create indexes if they don't exist
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_CountyID' AND object_id = OBJECT_ID('Students'))
BEGIN
    CREATE INDEX IX_Students_CountyID ON Students(CountyID);
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Students_SubCountyID' AND object_id = OBJECT_ID('Students'))
BEGIN
    CREATE INDEX IX_Students_SubCountyID ON Students(SubCountyID);
END

-- Add foreign key constraints if they don't exist
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_Counties_CountyID')
BEGIN
    ALTER TABLE Students ADD CONSTRAINT FK_Students_Counties_CountyID 
    FOREIGN KEY (CountyID) REFERENCES Counties(CountyID);
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_SubCounties_SubCountyID')
BEGIN
    ALTER TABLE Students ADD CONSTRAINT FK_Students_SubCounties_SubCountyID 
    FOREIGN KEY (SubCountyID) REFERENCES SubCounties(SubCountyID);
END