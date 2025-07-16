-- Create Counties table
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

-- Create SubCounties table
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

-- Update Students table to make CountyID and SubCountyID nullable
IF EXISTS (SELECT * FROM sys.columns WHERE name = 'CountyID' AND object_id = OBJECT_ID('Students'))
BEGIN
    -- Drop foreign key constraints if they exist
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_Counties_CountyID')
    BEGIN
        ALTER TABLE Students DROP CONSTRAINT FK_Students_Counties_CountyID;
    END
    
    IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_SubCounties_SubCountyID')
    BEGIN
        ALTER TABLE Students DROP CONSTRAINT FK_Students_SubCounties_SubCountyID;
    END
    
    -- Drop indexes if they exist
    IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Students_CountyID')
    BEGIN
        DROP INDEX IX_Students_CountyID ON Students;
    END
    
    IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Students_SubCountyID')
    BEGIN
        DROP INDEX IX_Students_SubCountyID ON Students;
    END
    
    -- Alter columns to be nullable
    ALTER TABLE Students ALTER COLUMN CountyID int NULL;
    ALTER TABLE Students ALTER COLUMN SubCountyID int NULL;
    
    -- Recreate indexes
    CREATE INDEX IX_Students_CountyID ON Students(CountyID);
    CREATE INDEX IX_Students_SubCountyID ON Students(SubCountyID);
    
    -- Add foreign key constraints
    ALTER TABLE Students ADD CONSTRAINT FK_Students_Counties_CountyID 
    FOREIGN KEY (CountyID) REFERENCES Counties(CountyID);
    
    ALTER TABLE Students ADD CONSTRAINT FK_Students_SubCounties_SubCountyID 
    FOREIGN KEY (SubCountyID) REFERENCES SubCounties(SubCountyID);
END