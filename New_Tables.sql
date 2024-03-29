USE [MikroDB_V16_Demo]
GO
/****** Object:  Table [dbo].[_ChangeLog]    Script Date: 5/21/2020 6:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_ChangeLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityName] [nvarchar](50) NULL,
	[PropertyName] [nvarchar](50) NULL,
	[PrimaryKeyValue] [nvarchar](50) NULL,
	[OldValue] [nvarchar](200) NULL,
	[NewValue] [nvarchar](200) NULL,
	[DateChanged] [datetime] NULL,
	[ChangedUser] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_Orders]    Script Date: 5/21/2020 6:43:34 PM ******/
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
	[CloseReason] [nvarchar](200) NULL,
	[CustomerWillAnswer] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_Stores]    Script Date: 5/21/2020 6:43:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_Stores](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[StoreCode] [nvarchar](50) NULL,
	[StoreName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_UserRoles]    Script Date: 5/21/2020 6:43:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK__UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_UserRolesMapping]    Script Date: 5/21/2020 6:43:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[_UserRolesMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RoleID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_Users]    Script Date: 5/21/2020 6:43:34 PM ******/
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
	[IsActive] [bit] NOT NULL,
	[EditDate] [int] NULL,
 CONSTRAINT [PK__Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[_VisitImages]    Script Date: 5/21/2020 6:43:34 PM ******/
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
/****** Object:  Table [dbo].[_Visits]    Script Date: 5/21/2020 6:43:35 PM ******/
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
	[FinalPrice] [float] NULL,
	[IsDeclined] [bit] NULL,
	[DeclineReason] [nvarchar](200) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[_UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[_UserRoles] ([ID])
GO
ALTER TABLE [dbo].[_UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[_Users] ([UserID])
GO
ALTER TABLE [dbo].[_UserRolesMapping]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[_Users] ([UserID])
GO
