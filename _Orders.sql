USE [MikroDB_V16_Demo]
GO

/****** Object:  Table [dbo].[_Orders]    Script Date: 5/7/2020 1:47:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[_Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[CreateOn] [datetime] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[LastUpdate] [datetime] NULL,
	[UpdateUser] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](50) NULL,
	[CustomerSurname] [nvarchar](50) NULL,
	[CustomerFatherName] [nvarchar](50) NULL,
	[SellerCode] [nvarchar](50) NULL,
	[Tel1] [nvarchar](50) NULL,
	[Tel2] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[Location] [nvarchar](50) NULL,
	[VisitDate] [datetime] NULL,
	[OrderStore] [nvarchar](50) NULL,
	[OrderType1] [bit] NULL,
	[OrderType2] [bit] NULL,
	[OrderType3] [bit] NULL,
	[ItemCount] [float] NULL,
	[ItemDescription] [nvarchar](150) NULL,
	[Price] [float] NULL,
	[Note] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[OrderStatus] [int] NULL,
	[IsVisitorAdded] [bit] NULL,
	[VisitorCode] [nvarchar](20) NULL,
	[VisitorName] [nvarchar](50) NULL,
	[VisitorStatus] [int] NULL,
	[IsDesignerAdded] [bit] NULL,
	[DesignerCode] [nvarchar](20) NULL,
	[DesignerName] [nvarchar](20) NULL,
	[DesignerStatus] [int] NULL,
	[CloseReason] [nvarchar](200) NULL
) ON [PRIMARY]
GO


