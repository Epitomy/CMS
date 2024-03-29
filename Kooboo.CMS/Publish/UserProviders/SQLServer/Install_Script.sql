
/****** Object:  Table [dbo].[Connect_User]    Script Date: 05/26/2011 15:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Connect_User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Connect_User](
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](20) NULL,
	[MiddleName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Gender] [smallint] NOT NULL,
	[Birthday] [datetime] NULL,
	[Country] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Address] [nvarchar](150) NULL,
	[Postcode] [nvarchar](30) NULL,
	[Telphone] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[CreateDate] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[LastLockoutDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[Password] [nvarchar](128) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[CustomerId] [char](36) NULL,
	[Comment] [nvarchar](2000) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Connect_User] ([Name], [Email], [FirstName], [MiddleName], [LastName], [Gender], [Birthday], [Country], [City], [Address], [Postcode], [Telphone], [Mobile], [CreateDate], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [IsApproved], [IsLockedOut], [LastLockoutDate], [LastLoginDate], [LastPasswordChangedDate], [Password], [PasswordAnswer], [PasswordQuestion], [PasswordSalt], [CustomerId], [Comment]) VALUES (N'admin', N'admin@cc.cc', NULL, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(0x00009E3300FC20CD AS DateTime), 0, NULL, 0, NULL, 1, 0, NULL, NULL, NULL, N'FYOL44w+oWRo53/9NQCnqa8RS00=', NULL, NULL, N'aKEwnA2Riwo4SCzyYFfzFw==', NULL, NULL)
/****** Object:  Table [dbo].[CMS_Account_Users]    Script Date: 05/26/2011 15:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_Account_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CMS_Account_Users](
	[Email] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](25) NULL,
	[IsAdministrator] [bit] NOT NULL,
	[UICulture] [nvarchar](50) NULL,
	[CustomFieldsXml] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
INSERT [dbo].[CMS_Account_Users] ([Email], [UserName], [IsAdministrator], [CustomFieldsXml]) VALUES (N'admin@cc.cc', N'admin', 1, N'')
/****** Object:  Table [dbo].[CMS_Account_Roles]    Script Date: 05/26/2011 15:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_Account_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CMS_Account_Roles](
	[Name] [nvarchar](128) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[CMS_Account_Roles] ([Name]) VALUES (N'Content manager')
INSERT [dbo].[CMS_Account_Roles] ([Name]) VALUES (N'Developer')
/****** Object:  Table [dbo].[CMS_Account_Permissions]    Script Date: 05/26/2011 15:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CMS_Account_Permissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CMS_Account_Permissions](
	[Id] [nvarchar](128) NOT NULL,
	[RoleName] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[AreaName] [nvarchar](256) NULL,
	[Group] [nvarchar](256) NULL,
	[DisplayName] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Contents__Content', N'Content manager', N'Content', N'Contents', N'', N'Content')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Contents__Content', N'Developer', N'Content', N'Contents', N'', N'Content')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Contents__Folder', N'Developer', N'Folder', N'Contents', N'', N'Folder')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Contents__Schema', N'Developer', N'Schema', N'Contents', N'', N'Schema')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Contents__Setting', N'Developer', N'Setting', N'Contents', N'', N'Setting')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Extensions_Plugin', N'Developer', N'Plugin', N'Sites', N'Extensions', N'Plugin')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Pages_Inline Editing', N'Content manager', N'Inline Editing', N'Sites', N'Pages', N'Inline Editing')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Pages_Inline Editing', N'Developer', N'Inline Editing', N'Sites', N'Pages', N'Inline Editing')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Pages_Page', N'Developer', N'Page', N'Sites', N'Pages', N'Page')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_Custom error', N'Developer', N'Custom error', N'Sites', N'Settings', N'Custom error')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_Page url', N'Developer', N'Page url', N'Sites', N'Settings', N'Page url')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_Robots.txt', N'Developer', N'Robots.txt', N'Sites', N'Settings', N'Robots.txt')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_System', N'Developer', N'System', N'Sites', N'Settings', N'System')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_Url redirect', N'Developer', N'Url redirect', N'Sites', N'Settings', N'Url redirect')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Settings_User', N'Developer', N'User', N'Sites', N'Settings', N'User')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_File', N'Developer', N'File', N'Sites', N'Templates', N'File')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_Label', N'Developer', N'Label', N'Sites', N'Templates', N'Label')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_Layout', N'Developer', N'Layout', N'Sites', N'Templates', N'Layout')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_Script', N'Developer', N'Script', N'Sites', N'Templates', N'Script')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_Theme', N'Developer', N'Theme', N'Sites', N'Templates', N'Theme')
INSERT [dbo].[CMS_Account_Permissions] ([Id], [RoleName], [Name], [AreaName], [Group], [DisplayName]) VALUES (N'Sites_Templates_View', N'Developer', N'View', N'Sites', N'Templates', N'View')
/****** Object:  ForeignKey [Role_Permissions]    Script Date: 05/26/2011 15:32:09 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Role_Permissions]') AND parent_object_id = OBJECT_ID(N'[dbo].[CMS_Account_Permissions]'))
ALTER TABLE [dbo].[CMS_Account_Permissions]  WITH CHECK ADD  CONSTRAINT [Role_Permissions] FOREIGN KEY([RoleName])
REFERENCES [dbo].[CMS_Account_Roles] ([Name])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Role_Permissions]') AND parent_object_id = OBJECT_ID(N'[dbo].[CMS_Account_Permissions]'))
ALTER TABLE [dbo].[CMS_Account_Permissions] CHECK CONSTRAINT [Role_Permissions]
GO
