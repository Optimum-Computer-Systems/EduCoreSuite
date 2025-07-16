-- Add CountyID and SubCountyID columns to Campuses table
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'CountyID' AND Object_ID = Object_ID(N'Campuses'))
BEGIN
    ALTER TABLE Campuses ADD CountyID int NOT NULL DEFAULT 1;
END

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'SubCountyID' AND Object_ID = Object_ID(N'Campuses'))
BEGIN
    ALTER TABLE Campuses ADD SubCountyID int NOT NULL DEFAULT 1;
END

-- Create indexes for the new columns
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Campuses_CountyID' AND object_id = OBJECT_ID('Campuses'))
BEGIN
    CREATE INDEX IX_Campuses_CountyID ON Campuses(CountyID);
END

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Campuses_SubCountyID' AND object_id = OBJECT_ID('Campuses'))
BEGIN
    CREATE INDEX IX_Campuses_SubCountyID ON Campuses(SubCountyID);
END

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Campuses_Counties_CountyID')
BEGIN
    ALTER TABLE Campuses ADD CONSTRAINT FK_Campuses_Counties_CountyID 
    FOREIGN KEY (CountyID) REFERENCES Counties(CountyID);
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Campuses_SubCounties_SubCountyID')
BEGIN
    ALTER TABLE Campuses ADD CONSTRAINT FK_Campuses_SubCounties_SubCountyID 
    FOREIGN KEY (SubCountyID) REFERENCES SubCounties(SubCountyID);
END