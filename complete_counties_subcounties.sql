USE [ForgeDB]
GO

/****** Object:  Table [dbo].[Counties]    Script Date: 7/13/2025 8:24:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Drop existing foreign key constraints if they exist
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_Counties_CountyID')
BEGIN
    ALTER TABLE [Students] DROP CONSTRAINT [FK_Students_Counties_CountyID];
END

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Students_SubCounties_SubCountyID')
BEGIN
    ALTER TABLE [Students] DROP CONSTRAINT [FK_Students_SubCounties_SubCountyID];
END

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SubCounties_Counties_CountyID')
BEGIN
    ALTER TABLE [SubCounties] DROP CONSTRAINT [FK_SubCounties_Counties_CountyID];
END

-- Drop existing tables if they exist
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SubCounties')
BEGIN
    DROP TABLE [SubCounties];
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Counties')
BEGIN
    DROP TABLE [Counties];
END

-- Create Counties table
CREATE TABLE [dbo].[Counties](
    [CountyID] [int] IDENTITY(1,1) NOT NULL,
    [CountyName] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_Counties] PRIMARY KEY CLUSTERED ([CountyID] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SubCounties]    Script Date: 7/13/2025 8:24:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubCounties](
    [SubCountyID] [int] IDENTITY(1,1) NOT NULL,
    [SubCountyName] [nvarchar](max) NOT NULL,
    [CountyID] [int] NOT NULL,
    CONSTRAINT [PK_SubCounties] PRIMARY KEY CLUSTERED ([SubCountyID] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Counties] ON 
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (1, N'Mombasa')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (2, N'Kwale')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (3, N'Kilifi')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (4, N'Tana River')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (5, N'Lamu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (6, N'Taita-Taveta')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (7, N'Garissa')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (8, N'Wajir')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (9, N'Mandera')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (10, N'Marsabit')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (11, N'Isiolo')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (12, N'Meru')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (13, N'Tharaka-Nithi')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (14, N'Embu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (15, N'Kitui')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (16, N'Machakos')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (17, N'Makueni')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (18, N'Nyandarua')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (19, N'Nyeri')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (20, N'Kirinyaga')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (21, N'Murang''a')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (22, N'Kiambu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (23, N'Turkana')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (24, N'West Pokot')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (25, N'Samburu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (26, N'Trans-Nzoia')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (27, N'Uasin Gishu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (28, N'Elgeyo-Marakwet')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (29, N'Nandi')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (30, N'Baringo')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (31, N'Laikipia')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (32, N'Nakuru')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (33, N'Narok')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (34, N'Kajiado')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (35, N'Kericho')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (36, N'Bomet')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (37, N'Kakamega')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (38, N'Vihiga')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (39, N'Bungoma')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (40, N'Busia')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (41, N'Siaya')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (42, N'Kisumu')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (43, N'Homa Bay')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (44, N'Migori')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (45, N'Kisii')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (46, N'Nyamira')
INSERT [dbo].[Counties] ([CountyID], [CountyName]) VALUES (47, N'Nairobi')
SET IDENTITY_INSERT [dbo].[Counties] OFF
GO

SET IDENTITY_INSERT [dbo].[SubCounties] ON 
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (1, N'Changamwe', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (2, N'Jomvu', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (3, N'Kisauni', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (4, N'Likoni', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (5, N'Mvita', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (6, N'Nyali', 1)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (7, N'Kinango', 2)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (8, N'Lungalunga', 2)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (9, N'Matuga', 2)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (10, N'Msambweni', 2)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (11, N'Ganze', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (12, N'Kaloleni', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (13, N'Magarini', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (14, N'Malindi', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (15, N'Rabai', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (16, N'Kilifi North', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (17, N'Kilifi South', 3)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (18, N'Bura', 4)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (19, N'Galole', 4)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (20, N'Garsen', 4)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (21, N'Lamu East', 5)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (22, N'Lamu West', 5)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (23, N'Mwatate', 6)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (24, N'Taveta', 6)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (25, N'Voi', 6)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (26, N'Wundanyi', 6)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (27, N'Dadaab', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (28, N'Fafi', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (29, N'Ijara', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (30, N'Lagdera', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (31, N'Balambala', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (32, N'Garissa Township', 7)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (33, N'Eldas', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (34, N'Tarbaj', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (35, N'Wajir East', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (36, N'Wajir West', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (37, N'Wajir North', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (38, N'Wajir South', 8)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (39, N'Banissa', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (40, N'Lafey', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (41, N'Mandera East', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (42, N'Mandera North', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (43, N'Mandera South', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (44, N'Mandera West', 9)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (45, N'Laisamis', 10)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (46, N'Moyale', 10)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (47, N'North Horr', 10)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (48, N'Saku', 10)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (49, N'Garbatulla', 11)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (50, N'Isiolo', 11)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (51, N'Merti', 11)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (52, N'Buuri', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (53, N'Central Imenti', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (54, N'Igembe Central', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (55, N'Igembe North', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (56, N'Igembe South', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (57, N'North Imenti', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (58, N'South Imenti', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (59, N'Tigania East', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (60, N'Tigania West', 12)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (61, N'Chuka', 13)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (62, N'Maara', 13)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (63, N'Tharaka', 13)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (64, N'Manyatta', 14)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (65, N'Mbeere North', 14)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (66, N'Mbeere South', 14)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (67, N'Runyenjes', 14)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (68, N'Kitui Central', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (69, N'Kitui East', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (70, N'Kitui Rural', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (71, N'Kitui South', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (72, N'Kitui West', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (73, N'Mwingi Central', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (74, N'Mwingi North', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (75, N'Mwingi West', 15)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (76, N'Athi River', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (77, N'Kangundo', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (78, N'Kathiani', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (79, N'Machakos Town', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (80, N'Masinga', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (81, N'Matungulu', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (82, N'Mwala', 16)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (83, N'Kaiti', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (84, N'Kibwezi East', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (85, N'Kibwezi West', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (86, N'Kilome', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (87, N'Makueni', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (88, N'Mbooni', 17)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (89, N'Kinangop', 18)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (90, N'Kipipiri', 18)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (91, N'Ndaragwa', 18)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (92, N'Ol Kalou', 18)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (93, N'Ol Jorok', 18)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (94, N'Kieni East', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (95, N'Kieni West', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (96, N'Mathira East', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (97, N'Mathira West', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (98, N'Mukurweini', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (99, N'Nyeri Central', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (100, N'Nyeri South', 19)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (101, N'Kirinyaga Central', 20)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (102, N'Kirinyaga East', 20)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (103, N'Kirinyaga West', 20)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (104, N'Mwea East', 20)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (105, N'Mwea West', 20)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (106, N'Gatanga', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (107, N'Kandara', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (108, N'Kangema', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (109, N'Kigumo', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (110, N'Kiharu', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (111, N'Maragwa', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (112, N'Mathioya', 21)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (113, N'Gatundu North', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (114, N'Gatundu South', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (115, N'Githunguri', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (116, N'Juja', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (117, N'Kabete', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (118, N'Kiambaa', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (119, N'Kiambu Town', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (120, N'Kikuyu', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (121, N'Lari', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (122, N'Limuru', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (123, N'Ruiru', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (124, N'Thika Town', 22)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (125, N'Turkana Central', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (126, N'Turkana East', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (127, N'Turkana North', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (128, N'Turkana South', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (129, N'Turkana West', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (130, N'Loima', 23)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (131, N'Kapenguria', 24)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (132, N'Pokot Central', 24)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (133, N'Pokot North', 24)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (134, N'Pokot South', 24)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (135, N'Samburu Central', 25)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (136, N'Samburu East', 25)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (137, N'Samburu North', 25)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (138, N'Cherangany', 26)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (139, N'Endebess', 26)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (140, N'Kwanza', 26)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (141, N'Saboti', 26)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (142, N'Kiminini', 26)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (143, N'Ainabkoi', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (144, N'Kapseret', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (145, N'Kesses', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (146, N'Moiben', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (147, N'Soy', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (148, N'Turbo', 27)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (149, N'Keiyo North', 28)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (150, N'Keiyo South', 28)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (151, N'Marakwet East', 28)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (152, N'Marakwet West', 28)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (153, N'Aldai', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (154, N'Chesumei', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (155, N'Emgwen', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (156, N'Mosop', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (157, N'Nandi Hills', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (158, N'Tinderet', 29)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (159, N'Baringo Central', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (160, N'Baringo North', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (161, N'Baringo South', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (162, N'Eldama Ravine', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (163, N'Mogotio', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (164, N'Tiaty', 30)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (165, N'Laikipia Central', 31)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (166, N'Laikipia East', 31)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (167, N'Laikipia North', 31)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (168, N'Laikipia West', 31)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (169, N'Bahati', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (170, N'Gilgil', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (171, N'Kuresoi North', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (172, N'Kuresoi South', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (173, N'Molo', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (174, N'Njoro', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (175, N'Naivasha', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (176, N'Nakuru East', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (177, N'Nakuru West', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (178, N'Rongai', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (179, N'Subukia', 32)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (180, N'Narok East', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (181, N'Narok North', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (182, N'Narok South', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (183, N'Narok West', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (184, N'Trans Mara East', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (185, N'Trans Mara West', 33)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (186, N'Kajiado Central', 34)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (187, N'Kajiado East', 34)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (188, N'Kajiado North', 34)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (189, N'Kajiado South', 34)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (190, N'Kajiado West', 34)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (191, N'Ainamoi', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (192, N'Belgut', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (193, N'Bureti', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (194, N'Kipkelion East', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (195, N'Kipkelion West', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (196, N'Soin', 35)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (197, N'Bomet Central', 36)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (198, N'Bomet East', 36)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (199, N'Chepalungu', 36)
INSERT [dbo].[SubCounties] ([SubCountyID], [SubCountyName], [CountyID]) VALUES (200, N'Konoin', 36)
SET IDENTITY_INSERT [dbo].[SubCounties] OFF
GO

ALTER TABLE [dbo].[SubCounties] WITH CHECK ADD CONSTRAINT [FK_SubCounties_Counties_CountyID] FOREIGN KEY([CountyID])
REFERENCES [dbo].[Counties] ([CountyID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SubCounties] CHECK CONSTRAINT [FK_SubCounties_Counties_CountyID]
GO

-- Re-add foreign keys to Students table if needed
IF EXISTS (SELECT * FROM sys.columns WHERE name = 'CountyID' AND object_id = OBJECT_ID('Students'))
BEGIN
    ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_Counties_CountyID] 
    FOREIGN KEY ([CountyID]) REFERENCES [Counties] ([CountyID]);
    
    ALTER TABLE [Students] ADD CONSTRAINT [FK_Students_SubCounties_SubCountyID] 
    FOREIGN KEY ([SubCountyID]) REFERENCES [SubCounties] ([SubCountyID]);
END
GO