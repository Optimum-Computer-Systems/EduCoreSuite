USE [EBUCOREDB]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 24/06/2025 16:40:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Author] [nvarchar](255) NOT NULL,
	[YearPublished] [int] NOT NULL,
	[Genre] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (1, N'The Pragmatic Programmer', N'Andrew Hunt', 1999, N'Programming')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (2, N'Clean Code', N'Robert C. Martin', 2008, N'Programming')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (3, N'To Kill a Mockingbird', N'Harper Lee', 1960, N'Fiction')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (4, N'1984', N'George Orwell', 1949, N'Dystopian')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (5, N'The Great Gatsby', N'F. Scott Fitzgerald', 1925, N'Classic')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (6, N'Introduction to Algorithms', N'Thomas H. Cormen', 2009, N'Education')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (7, N'Thinking, Fast and Slow', N'Daniel Kahneman', 2011, N'Psychology')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (8, N'The Art of War', N'Sun Tzu', -500, N'Philosophy')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (9, N'Harry Potter and the Sorcerer''s Stone', N'J.K. Rowling', 1997, N'Fantasy')
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublished], [Genre]) VALUES (10, N'The Lean Startup', N'Eric Ries', 2011, N'Business')
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
