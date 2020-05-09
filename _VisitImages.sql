USE [MikroDB_V16_Demo]
GO

/****** Object:  Table [dbo].[_VisitImages]    Script Date: 5/7/2020 1:48:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[_VisitImages](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[VisitGuid] [uniqueidentifier] NOT NULL,
	[ImageName] [nvarchar](200) NULL,
	[ImagePath] [nvarchar](200) NULL,
	[ImageType] [int] NULL
) ON [PRIMARY]
GO


