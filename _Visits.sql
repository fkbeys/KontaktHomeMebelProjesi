USE [MikroDB_V16_Demo]
GO

/****** Object:  Table [dbo].[_Visits]    Script Date: 5/7/2020 1:49:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[_Visits](
	[VisitID] [int] IDENTITY(1,1) NOT NULL,
	[CreateOn] [datetime] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[LastUpdate] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[OrderId] [int] NULL,
	[DWidth] [float] NULL,
	[DLenght] [float] NULL,
	[DHeight] [float] NULL,
	[Accessory] [nvarchar](30) NULL,
	[Mirror] [nvarchar](30) NULL,
	[Note] [nvarchar](200) NULL,
	[VisitGuid] [uniqueidentifier] NULL,
	[ProductCode] [nvarchar](50) NULL,
	[ProductName] [nvarchar](50) NULL,
	[PanelType] [nvarchar](10) NULL,
	[PanelColour] [nvarchar](15) NULL,
	[MaterialType] [nvarchar](10) NULL,
	[MaterialColour] [nvarchar](15) NULL,
	[DoorType] [nvarchar](10) NULL,
	[DoorColour] [nvarchar](15) NULL,
	[Price] [float] NULL,
	[IsDeclined] [bit] NULL,
	[DeclineReason] [nvarchar](200) NULL
) ON [PRIMARY]
GO


