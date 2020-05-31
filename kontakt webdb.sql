USE [MikroDB_V16_Demo]
GO
/****** Object:  Table [dbo].[_ChangeLog]    Script Date: 29.05.2020 18:03:00 ******/
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
/****** Object:  Table [dbo].[_Orders]    Script Date: 29.05.2020 18:03:01 ******/
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
/****** Object:  Table [dbo].[_Stores]    Script Date: 29.05.2020 18:03:01 ******/
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
/****** Object:  Table [dbo].[_UserRoles]    Script Date: 29.05.2020 18:03:01 ******/
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
/****** Object:  Table [dbo].[_UserRolesMapping]    Script Date: 29.05.2020 18:03:02 ******/
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
/****** Object:  Table [dbo].[_Users]    Script Date: 29.05.2020 18:03:02 ******/
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
/****** Object:  Table [dbo].[_VisitImages]    Script Date: 29.05.2020 18:03:02 ******/
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
/****** Object:  Table [dbo].[_Visits]    Script Date: 29.05.2020 18:03:02 ******/
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
SET IDENTITY_INSERT [dbo].[_ChangeLog] ON 
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (1, N'Orders', N'OrderStatus', N'1', N'1', N'2', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (2, N'Orders', N'VisitorCode', N'1', NULL, N'admin', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (3, N'Orders', N'VisitorName', N'1', NULL, N'admin', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (4, N'Orders', N'IsVisitorAdded', N'1', N'False', N'True', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (5, N'Orders', N'VisitorStatus', N'1', N'0', N'1', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (6, N'Orders', N'DesignerCode', N'1', NULL, N'', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (7, N'Orders', N'DesignerName', N'1', NULL, N'', CAST(N'2020-05-22T10:14:02.617' AS DateTime), N'admin')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (8, N'Users', N'StoreCode', N'2', NULL, N'Magaza1', CAST(N'2020-05-25T14:31:53.093' AS DateTime), N'')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (9, N'Users', N'StoreName', N'2', NULL, N'Magaza1', CAST(N'2020-05-25T14:31:53.093' AS DateTime), N'')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (10, N'Users', N'StoreCode', N'1', N'', N'Magaza1', CAST(N'2020-05-25T14:32:10.813' AS DateTime), N'')
GO
INSERT [dbo].[_ChangeLog] ([Id], [EntityName], [PropertyName], [PrimaryKeyValue], [OldValue], [NewValue], [DateChanged], [ChangedUser]) VALUES (11, N'Users', N'StoreName', N'1', N'', N'Magaza1', CAST(N'2020-05-25T14:32:10.813' AS DateTime), N'')
GO
SET IDENTITY_INSERT [dbo].[_ChangeLog] OFF
GO
SET IDENTITY_INSERT [dbo].[_Orders] ON 
GO
INSERT [dbo].[_Orders] ([OrderId], [CreateOn], [CreateUser], [LastUpdate], [UpdateUser], [CustomerName], [CustomerSurname], [CustomerFatherName], [SellerCode], [Tel1], [Tel2], [Address], [Location], [VisitDate], [OrderStore], [OrderType1], [OrderType2], [OrderType3], [ItemCount], [ItemDescription], [Price], [Note], [IsActive], [OrderStatus], [IsVisitorAdded], [VisitorCode], [VisitorName], [VisitorStatus], [IsDesignerAdded], [DesignerCode], [DesignerName], [DesignerStatus], [CloseReason], [CustomerWillAnswer]) VALUES (1, CAST(N'2020-05-21T18:13:23.883' AS DateTime), N'administrator', CAST(N'2020-05-22T13:14:02.600' AS DateTime), N'admin', N'Resad', N'Memmedov', N'Cefer', N'administrator', N'(666)666-66-66', N'(777)777-77-77', N'Baki', N'Nəriman Nərimanov', CAST(N'2020-05-31T00:00:00.000' AS DateTime), N'', 1, 1, 0, 2, NULL, 2600, NULL, 1, 2, 1, N'admin', N'admin', 1, 0, N'', N'', 0, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[_Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[_Stores] ON 
GO
INSERT [dbo].[_Stores] ([StoreID], [StoreCode], [StoreName]) VALUES (1, N'Magaza1', N'Magaza1')
GO
INSERT [dbo].[_Stores] ([StoreID], [StoreCode], [StoreName]) VALUES (2, N'Magaza2', N'Magaza2')
GO
SET IDENTITY_INSERT [dbo].[_Stores] OFF
GO
SET IDENTITY_INSERT [dbo].[_UserRoles] ON 
GO
INSERT [dbo].[_UserRoles] ([ID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[_UserRoles] ([ID], [RoleName]) VALUES (2, N'Kordinator')
GO
INSERT [dbo].[_UserRoles] ([ID], [RoleName]) VALUES (3, N'Vizitor')
GO
INSERT [dbo].[_UserRoles] ([ID], [RoleName]) VALUES (4, N'Satici')
GO
INSERT [dbo].[_UserRoles] ([ID], [RoleName]) VALUES (5, N'Dizayner')
GO
SET IDENTITY_INSERT [dbo].[_UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[_UserRolesMapping] ON 
GO
INSERT [dbo].[_UserRolesMapping] ([ID], [UserID], [RoleID]) VALUES (1, 1, 1)
GO
INSERT [dbo].[_UserRolesMapping] ([ID], [UserID], [RoleID]) VALUES (2, 2, 1)
GO
INSERT [dbo].[_UserRolesMapping] ([ID], [UserID], [RoleID]) VALUES (3, 2, 3)
GO
INSERT [dbo].[_UserRolesMapping] ([ID], [UserID], [RoleID]) VALUES (4, 2, 5)
GO
SET IDENTITY_INSERT [dbo].[_UserRolesMapping] OFF
GO
SET IDENTITY_INSERT [dbo].[_Users] ON 
GO
INSERT [dbo].[_Users] ([UserID], [UserName], [UserPassword], [UserDisplayName], [StoreCode], [StoreName], [IsActive], [EditDate]) VALUES (1, N'administrator', N'administrator', N'Administrator', N'Magaza1', N'Magaza1', 1, 1)
GO
INSERT [dbo].[_Users] ([UserID], [UserName], [UserPassword], [UserDisplayName], [StoreCode], [StoreName], [IsActive], [EditDate]) VALUES (2, N'admin', NULL, N'admin', N'Magaza1', N'Magaza1', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[_Users] OFF
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
