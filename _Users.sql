USE [MikroDB_V16_Demo]
GO

/****** Object:  Table [dbo].[_Users]    Script Date: 5/7/2020 1:48:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserPassword] [nvarchar](20) NULL,
	[UserDisplayName] [nvarchar](50) NOT NULL,
	[StoreCode] [nvarchar](50) NULL,
	[StoreName] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsVisitor] [bit] NOT NULL,
	[IsSeller] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsCord] [bit] NOT NULL,
	[IsDesigner] [bit] NULL,
	[EditDate] [int] NULL
) ON [PRIMARY]
GO


