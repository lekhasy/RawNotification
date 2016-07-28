USE [master]
GO
/****** Object:  Database [Notification_DB]    Script Date: 29-Jul-16 1:22:58 AM ******/
CREATE DATABASE [Notification_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Notification_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Notification_DB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Notification_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Notification_DB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Notification_DB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Notification_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Notification_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Notification_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Notification_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Notification_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Notification_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Notification_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Notification_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Notification_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Notification_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Notification_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Notification_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Notification_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Notification_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Notification_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Notification_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Notification_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Notification_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Notification_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Notification_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Notification_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Notification_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Notification_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Notification_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Notification_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Notification_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Notification_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Notification_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Notification_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Notification_DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Notification_DB', N'ON'
GO
USE [Notification_DB]
GO
/****** Object:  Table [dbo].[Device]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Device](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[IMEI] [varchar](50) NOT NULL,
	[ReceiverNewId] [bigint] NOT NULL,
	[URI] [nvarchar](max) NOT NULL,
	[OSId] [int] NOT NULL,
	[DeviceToken] [varchar](36) NOT NULL,
	[TokenExperiedTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Device] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceNotification]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceNotification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReceiverNotificationId] [bigint] NOT NULL,
	[DeviceId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.DeviceNotification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginToken]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginToken](
	[Id] [bigint] NOT NULL,
	[ReceiverId] [bigint] NOT NULL,
	[AccessToken] [varchar](36) NOT NULL,
	[TokenExpiredTime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationContent] [varbinary](max) NOT NULL,
	[NotificationAccessKey] [varchar](36) NOT NULL,
	[NotificationPreviewContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_dbo.Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Receiver]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Receiver](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OldId] [varchar](100) NOT NULL,
	[DeviceCount] [int] NOT NULL CONSTRAINT [DF_Receiver_DeviceCount]  DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Receiver] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReceiverNotification]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiverNotification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReceiverNewId] [bigint] NOT NULL,
	[NotificationId] [bigint] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ReceiverNotification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[ReadReceiverNotification]    Script Date: 29-Jul-16 1:22:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReadReceiverNotification]
AS
SELECT        Id
FROM            dbo.ReceiverNotification
WHERE        (IsRead = 1)

GO
SET IDENTITY_INSERT [dbo].[Device] ON 

INSERT [dbo].[Device] ([Id], [IMEI], [ReceiverNewId], [URI], [OSId], [DeviceToken], [TokenExperiedTime]) VALUES (83, N'924e8359-1b20-3452-739e-e69572656cd4', 23, N'https://hk2.notify.windows.com/?token=AwYAAAAOCleNLbm6Syyj9fc05Oh7p7wNlojHVibLxegwEfGfTvXxB2fzPgKE4LM31uEAptJVmZBe4kYh3gLP4zvnCkHQ3RRAr%2fWjy7oNLY894m57pl0i0d%2bqBANLJV25Q0XOBuc%3d', 1, N'924e8359-1b20-3452-739e-e69572656cd4', CAST(N'2016-08-15 00:00:00.000' AS DateTime))
INSERT [dbo].[Device] ([Id], [IMEI], [ReceiverNewId], [URI], [OSId], [DeviceToken], [TokenExperiedTime]) VALUES (85, N'21eca7ea-7b15-35c8-181b-a6ac7b9832bf', 23, N'https://hk2.notify.windows.com/?token=AwYAAABP00scxlJsV3oN62CHvEfZIfgoa4we2%2bzR9wyKi1FTrWB5qWjDaju6KQOeqHg1DzPHCIQhPLPpV7qxOaXBdusR3lk9TIDOnoJ4ASMnl%2bpEE9Sq7duO1gRz2ZaApLmU9hQ%3d', 1, N'924e8359-1b20-3452-739e-e69572656cd4', CAST(N'2016-08-15 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Device] OFF
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (83, 0x2231323322, N'6de2b943-eb39-48e4-acfe-17ee42ec367f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (84, 0x2231323322, N'1075417b-48dd-4970-9245-2f9a10cd5289', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (85, 0x2231323322, N'9abdf2db-6b14-4d87-9bae-7cdf8892d095', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (86, 0x2231323322, N'4f6df5e6-344a-4863-9016-7b7ff4d71ae6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (87, 0x2231323322, N'2cac455e-9944-4bff-9612-c50c29a44f74', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (88, 0x2231323322, N'57e3537c-a43b-4f27-88d2-e3e1e44cc993', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (89, 0x2231323322, N'08bf8f17-931b-4e4c-b348-77ec3fdb45ea', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (90, 0x2231323322, N'42732fc3-b09f-4bfc-9abd-7eba4c3a30ee', 0x224CE1BB8B636820746869206DC3B46E204353444C22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (91, 0x2231323322, N'fa8e371c-a287-4066-aa36-cbc3abde25f7', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (92, 0x2231323322, N'693c7196-a2bd-46d0-8327-4a863429ddb2', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (93, 0x2231323322, N'b1a06b35-b9fe-4484-9931-af8dea19994a', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (94, 0x2231323322, N'd6a45e00-079e-4f51-97e8-1384265ef3ab', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (95, 0x2231323322, N'3a93109c-55bb-4d99-973d-e3e24e1f5d57', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (96, 0x2231323322, N'9c13428f-ddbb-4e21-af40-5de853e619c5', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (97, 0x2231323322, N'659c3c58-5d0b-4e4b-a5fb-4f0377eb978e', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (98, 0x2231323322, N'4191b269-8757-4075-a64b-c60bd7791299', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (99, 0x2231323322, N'de06b222-2f0f-45a2-8ab5-2979e36e49e6', 0x2253C3A16E67206D61692063E1BAA3206CE1BB9B70206E6768E1BB892068E1BB8D63206E68612122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (100, 0x2231323322, N'016c7c59-da71-4cd2-9188-03fe5ff82d21', 0x224368C3BA6E672074C3B46920C491616E672063C3B320432E7472C3AC6E68206B687579E1BABF6E206DC3A369206CE1BB9B6E2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (101, 0x2231323322, N'ed160cfa-4770-42f3-907a-e0a783cacf00', 0x225468C3B46E672062C3A16F206B68E1BAA96E2063E1BAA57021212122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (102, 0x2231323322, N'4487280b-f79a-4c36-8e98-6dc55db82563', 0x225468C3B46E672062C3A16F2063E1BBB163206BE1BBB3206B68E1BAA96E2063E1BAA57021212122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (103, 0x2231323322, N'dbe07d31-f08f-4e9a-b4a3-67be6dc707d6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (104, 0x2231323322, N'8530cf7b-dc02-4f7c-90be-ab43363fa6fa', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (105, 0x2231323322, N'68de4ed8-6edd-4e89-946c-9280698d2601', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (106, 0x2231323322, N'bf8e2e47-79a7-4f4a-bc13-89a329c78e60', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (107, 0x2231323322, N'b98226ad-c72e-4812-a67b-2a713efd08d3', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (108, 0x2231323322, N'3c140d40-d895-469c-a077-45fb087fbc6d', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (109, 0x2231323322, N'9c8aebaa-95d7-4edb-ac1a-4cfc5b21447e', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (110, 0x2231323322, N'7dc59fe9-1d90-4359-a475-078c45249773', 0x2250726576696577205469746C6520313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (111, 0x2231323322, N'048b8b84-b8a5-4778-a9f7-90a787d1a546', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (112, 0x2231323322, N'6feb19c4-1462-470d-a0a2-7655d9e864cd', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (113, 0x2231323322, N'092b653c-ddb2-419f-9588-92f490609006', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (114, 0x2231323322, N'32315303-7d9b-4588-a66d-e7448f7f3611', 0x2250726576696577205469746C652031323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (115, 0x22313233343522, N'186ae31c-7b34-4dbe-a5d7-0c7e1ea2551e', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (116, 0x22313233343522, N'2147cd02-d090-4804-9f1f-634b752d68a2', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (117, 0x22313233343522, N'2b997b15-aa08-4800-a521-b87a702eb837', 0x2250726576696577205469746C65203122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (118, 0x2253C3A16E67206D6169206CE1BB9B70203135544830303031206E6768E1BB892068E1BB8D63206DC3B46E205054544B2E22, N'6a65ecec-763f-465e-b854-94faaff638f4', 0x2253C3A16E67206D61692063E1BAA3206CE1BB9B70206E6768E1BB892068E1BB8D63206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (119, 0x2253C3A16E67206D6169206CE1BB9B70203135544830303031206E6768E1BB892068E1BB8D63206DC3B46E205054544B20636F70792E22, N'632ec0a7-17db-4f49-95c0-f6d1bfb15c56', 0x2253C3A16E67206D61692063E1BAA3206CE1BB9B70206E6768E1BB892068E1BB8D63206E686120636F70792E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (120, 0x22436869207469E1BABF74206B687579E1BABF6E206DC3A3692E22, N'7ab5b133-a052-4d9c-b9d2-546541529e8b', 0x22432E7472C3AC6E68206B687579E1BABF6E206DC3A3692E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (121, 0x22436869207469E1BABF74206B687579E1BABF6E206DC3A3692E22, N'482b76eb-ae26-4d46-ba58-74597a1d7487', 0x22432E7472C3AC6E68206B687579E1BABF6E206DC3A3692E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (122, 0x2231323322, N'3d9bf9fa-fd1f-46f5-bdb2-ffb0c7653d12', 0x2250726576696577205469746C65206E657722)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (123, 0x2244656D6F22, N'5eacc84c-fea0-43ff-8f24-cba667051ef8', 0x225468C3B46E672042C3A16F2044656D6F22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (124, 0x2253C3A16E67206D61692C206E67C3A0792032355C2F385C2F323031362C206368C3BA6E672074C3B4692063C3B3206368C6B0C6A16E67207472C3AC6E68206B687579E1BABF6E206DC3A369206CE1BB9B6E2E22, N'9e548591-0362-4a9d-8820-470cec9a052d', 0x225468C3B46E672074696E206B687579E1BABF6E206DC3A3692E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (125, 0x2253C3A16E67206D61692C206E67C3A0792032355C2F385C2F323031362C206368C3BA6E672074C3B4692063C3B3206368C6B0C6A16E67207472C3AC6E68206B687579E1BABF6E206DC3A369206CE1BB9B6E2E22, N'ea52e8b0-2c64-462c-86ce-dd8bd82ee6a9', 0x225468C3B46E672074696E206B687579E1BABF6E206DC3A3692E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (126, 0x224368C3BA63206DE1BBAB6E672073756E68206E68E1BAAD742062E1BAA16E2E204368C3BA632062E1BAA16E2063C3B3206DE1BB9974206E67C3A0792073696E68206E68E1BAAD74207675692076E1BABB2E22, N'92d1b7df-c367-4755-83a4-126c835e7b02', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (127, 0x2244E1BB8B63682076E1BBA52063E1BBA761206368C3BA6E672074C3B4692073E1BABD2074E1BAA16D206E67C6B06E6720C491E1BB832062E1BAA36F207472C3AC2076C3A06F206E67C3A079206D61692C206D6F6E672062E1BAA16E207468C3B46E672063E1BAA36D2E22, N'e5f76cd2-5966-4d55-aed9-1b81e071d0f0', 0x225468C3B46E672062C3A16F2074E1BAA16D206E67C6B06E672064E1BB8B63682076E1BBA52E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (128, 0x2248617070792042697274686461792E203D292922, N'21253d61-bac4-40a9-b307-5d9799c5f13f', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (129, 0x2248617070792042697274686461792E203D292922, N'53d7049e-43ec-4b6b-9254-1037620701f2', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (130, 0x2248617070792042697274686461792E203D2929202E202E202E2022, N'a994d4c3-bf63-4128-bad8-13be6bbfa37e', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742E2E2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (131, 0x22616122, N'850459cf-3e41-44be-aca6-27597d978f71', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (132, 0x22616122, N'9c609596-e695-4e16-93b5-08fb2802f2bf', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (133, 0x22616122, N'd15c38e9-8535-458b-bdee-49f65af2ead1', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (134, 0x22616122, N'f4b6196f-1e0c-472a-8b3d-1043c2821bf3', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (135, 0x2231323322, N'59bba559-25ed-4cf8-aeed-30a9f23eeb9a', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (136, 0x2231323322, N'805d65a2-a379-4542-a5d1-eacafe700626', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (137, 0x2231323322, N'bd5318ea-4aec-45d5-bdbd-8a3923d0efd5', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (138, 0x2231323322, N'2060b1aa-4938-4ae0-b440-5b60b6d8c604', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (139, 0x2231323322, N'37b40eca-b80d-460a-b09f-d61d56226fd6', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (140, 0x2231323322, N'e24e66f6-54db-4b84-8556-1122e9e4400b', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (141, 0x2231323322, N'db0d2acd-f420-4d9c-a778-73fce52012a8', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (142, 0x2231323322, N'57acaf3c-02a7-4b62-b646-acd77414641e', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (143, 0x2231323322, N'd47e60be-213f-4b18-a813-9b5c9ee898f5', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (144, 0x2231323322, N'9b231f02-6829-4168-8068-508e528ee652', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (145, 0x2231323322, N'7fddf0ff-d239-4c75-81cb-03f937e5d68a', 0x2250726576696577205469746C6531323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (146, 0x22313233343522, N'05877b23-c74b-4f02-a1fc-cf738b2fc5ec', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (147, 0x22313233343522, N'1d7f240e-b8c6-4bcd-9112-de2f8cb16a30', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (148, 0x22313233343522, N'adbcbcac-5c1e-45be-977a-36fe35ed9db9', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (149, 0x22313233343522, N'6d841b3b-9f46-4520-9ca0-bf08b2903701', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (150, 0x22313233343522, N'b398a134-c16b-4501-b39a-594a3e8d42f9', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (151, 0x22313233343522, N'91daaaed-841f-43c8-b492-3d551b3e3968', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (152, 0x22313233343522, N'e1a6ee85-f265-445f-9fcc-d08844c26743', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (153, 0x22313233343522, N'91eacb27-f794-45f8-808a-de5314c6c0e8', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (154, 0x22313233343522, N'3bf76bd1-1e8a-4375-b71d-0d10e62d381a', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (155, 0x22313233343522, N'9e924699-eb44-4dcc-81c4-9027bc62ab9f', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (156, 0x22313233343522, N'f5962d31-7155-42f3-8073-5fdbbbb650db', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (157, 0x22313233343522, N'79214d41-c93d-426a-98e7-969c00191bbf', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (158, 0x22313233343522, N'f87be3de-0e13-4588-b7a2-ba1fdacdb10f', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (159, 0x22313233343522, N'e440748f-e8e5-431d-90bc-fd76f36c82f9', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (160, 0x22313233343522, N'5566f079-f62b-458b-9782-934a30b8f613', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (161, 0x22313233343522, N'50ad63d7-aece-4774-9109-dbb480d5b9a2', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (162, 0x22313233343522, N'aa8c4bca-1ca4-44d6-a0d1-9bb8488548ae', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (163, 0x22313233343522, N'c62fbbfe-08a3-461a-bd77-04936d5f6efa', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (164, 0x22313233343522, N'be5ed968-67e5-4192-8e5d-6040de005a63', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (165, 0x22313233343522, N'e760ed70-2377-4126-ae38-d33179befaa4', 0x2250726576696577205469746C65313233313222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (166, 0x22313233343522, N'e9dd8809-6423-45b6-99ea-e5d0113de939', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (167, 0x22313233343522, N'0951595d-e8ee-442d-adae-75f43304b2d5', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (168, 0x22313233343522, N'd9a1bb3b-4002-46ca-9720-f53109dce139', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (169, 0x22313233343522, N'db160705-7c03-48b4-a6c5-7e1e0ee0c848', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (170, 0x22313233343522, N'd51b6a7e-5c91-4e51-be79-948834f84042', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (171, 0x22313233343522, N'042786c8-71bd-4f26-8662-4e4e5445c373', 0x2250726576696577205469746C6531323331323422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (172, 0x2231323322, N'd09a5026-949d-46c1-b1d7-027d9e112191', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (173, 0x2231323322, N'03de5563-38b9-4ccd-bd43-d9eb4e50ae39', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (174, 0x2231323322, N'bbd6c491-fb3a-4fd9-834b-08931456af8a', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (175, 0x2231323322, N'd9346004-94e2-433d-807e-62a461ee07a8', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (176, 0x2231323322, N'76af0e28-8045-4c8c-b7b1-206ab2ea4172', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (177, 0x2231323322, N'4ce9ebb2-6a46-4d94-bf71-62dc74b671bb', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (178, 0x2231323322, N'c2e61eb5-6d11-49b9-9c4a-df1bc35ea765', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (179, 0x2231323322, N'c268e6cd-e06b-4e2a-866d-c7de2377d3e6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (180, 0x2231323322, N'5a1dc4b5-84f4-4ad3-9d12-cf3ee3647cb9', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (181, 0x2231323322, N'd83fa6a3-d4d8-4cb3-b866-5458a0d48b68', 0x2250726576696577205469746C6522)
GO
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (182, 0x2231323322, N'705eac5f-4749-4fab-82b1-7638eb97caa1', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (183, 0x2231323322, N'f49cbcb3-69fe-45a5-8c50-452344d39658', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (184, 0x2231323322, N'c32a708d-ad32-460e-8506-2bf7e9eac155', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (185, 0x2231323322, N'3dad766e-fef7-4786-b54f-6a9721c36101', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (186, 0x2231323322, N'9c399da8-4dd6-4c20-83c9-2061a8262e23', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (187, 0x2231323322, N'65d0baf0-cbf7-4124-8074-c0d9dc32ab2d', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (188, 0x2231323322, N'b6c5e40f-6a57-465f-8514-431049544f43', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (189, 0x2231323322, N'a414c51e-6758-48e4-97bf-623de9464ab6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (190, 0x2231323322, N'abaea96f-d22b-416c-a399-10811d29609f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (191, 0x2231323322, N'5f2a7489-e878-4a8f-9ac1-55fd4a0b4a0d', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (192, 0x2231323322, N'817fa626-be3d-486a-9095-2510f9069480', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (193, 0x2231323322, N'f3d3ef9f-976b-4cac-bdb0-fa5a26252548', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (194, 0x2231323322, N'697f1fea-295f-45ec-8de4-be5998e7bf12', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (195, 0x2231323322, N'f45c43db-834f-4358-bc7a-1b2e98a24beb', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (196, 0x2231323322, N'4da8db0e-e64e-474d-9cb5-5b700a51fda2', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (197, 0x2231323322, N'3188121e-96d2-4684-a74b-e69e9708d6d8', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (198, 0x2231323322, N'55199bef-0dfb-45ef-a9c0-27a0a9f249a9', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (199, 0x2231323322, N'8f1547f4-f019-4f06-b36d-587cb7d6a4fe', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (200, 0x2231323322, N'88e584dd-0884-4b45-bca7-ff848d364b30', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (201, 0x2231323322, N'4fc79b98-5c9a-4d07-99e5-5ef9d837eacd', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (202, 0x2231323322, N'49391ac1-b3e2-4adb-9d7b-f1924516a3a7', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (203, 0x2231323322, N'889845d4-4c3d-4d2b-b288-222c2bf7cc3d', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (204, 0x2231323322, N'9e036d6f-d05c-4b6a-b0d5-15f315239074', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (205, 0x2231323322, N'bea76a43-84ae-4c0c-a959-14f49ef0b33a', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (206, 0x2231323322, N'69b9fefc-d3ca-4ae4-8da1-bbdd9614facb', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (207, 0x2231323322, N'fdf97372-37ed-4523-9ffa-c9a18378a708', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (208, 0x2231323322, N'627b788a-0945-4940-9c2f-65e9e82554c8', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (209, 0x2231323322, N'0ebcbc1e-2c6e-4f06-ad92-055f72a2c29c', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (210, 0x2231323322, N'bfb544a6-2622-49e7-be72-000ceb6c093a', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (211, 0x2231323322, N'e87b1f7c-f6ef-44eb-8e1c-88aacdf3f690', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (212, 0x2231323322, N'b0bdeb4b-9c98-4275-84f1-0b70a46d51a9', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (213, 0x2231323322, N'8a6d6271-b9b7-4fc8-bbbe-ee92a5611d09', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (214, 0x2231323322, N'7c80a909-1082-4784-b3cf-d54a1c14e23b', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (215, 0x2231323322, N'9a11e395-92a1-4853-83c4-76b52518e0b3', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (216, 0x224861707079206269727468206461792E203A2922, N'099f8f0a-98f8-44ac-8fdc-18cf5d2da059', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (217, 0x224861707079206269727468206461792E203A2922, N'cb7855e5-805d-43b9-9a20-e9ca24a6c88c', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420312E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (218, 0x224861707079206269727468206461792E203A2922, N'c8e4e1fa-d85c-409f-b646-19a216d1fc2c', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420312E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (219, 0x224861707079206269727468206461792E203A2922, N'026b5cb8-7d38-4726-9838-711077a76aac', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420312E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (220, 0x224861707079206269727468206461792E203A2922, N'cbf0b357-999d-4993-8a17-2c93ccbf98bc', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420312E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (221, 0x224861707079206269727468206461792E203A2922, N'96689f1c-2338-4914-a880-71ff53421cda', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (222, 0x224861707079206269727468206461792E203A2922, N'70c201c3-d7b4-4c78-b4f6-e7f8dc1a0cd1', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (223, 0x224861707079206269727468206461792E203A2922, N'24c1b130-ed8f-4bd0-aa84-5a02873c8c1f', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (224, 0x224861707079206269727468206461792E203A2922, N'a23d84e2-6305-430f-956a-e471071a8f02', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (225, 0x224861707079206269727468206461792E203A2922, N'0315ac16-2764-4feb-9a29-7d03176de085', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (226, 0x224861707079206269727468206461792E203A2922, N'7f13fd97-1dff-49a1-aad5-4d19b438eedd', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (227, 0x224861707079206269727468206461792E203A2922, N'06d0465f-6ca5-427e-8357-b69f2f966a44', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (228, 0x224861707079206269727468206461792E203A2922, N'a5a051fc-e8ce-4917-957a-052294f50f59', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (229, 0x224861707079206269727468206461792E203A2922, N'cd63f9db-a7cd-4a99-a4c9-b839f4fd2472', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (230, 0x224861707079206269727468206461792E203A2922, N'4e4b4fea-acce-49ee-82d6-16f118c1908c', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (231, 0x224861707079206269727468206461792E203A2922, N'103998a6-c0f4-461e-8d80-6275d7b35f22', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (232, 0x224861707079206269727468206461792E203A2922, N'41078e35-f124-4837-a86f-1e6944d4104e', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7420322E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (233, 0x2234343422, N'16c6d4b1-7bf1-4846-87f8-d21975af8e70', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (234, 0x2234343422, N'f7cfe7fc-8a57-49f8-9ff2-a7f887a410f8', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (235, 0x2234343422, N'82ee36a0-dfdc-4743-8f7a-d6ca09c337a0', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (236, 0x2234343422, N'014c4563-0a0a-4222-8ae7-14884db575b9', 0x2250726576696577205469746C652031323322)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (237, 0x2234343422, N'53741cda-e20a-4854-b537-c3e16861ec86', 0x2250726576696577205469746C65203132333422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (238, 0x2231323322, N'5c91bd03-73e3-4560-a3e0-9508bbb68965', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (239, 0x2231323322, N'333f1e5c-2486-4dfa-8ef1-09a59533a9fe', 0x2243C3BA63206DE1BBAB6E672073696E68206E68E1BAAD7422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (240, 0x223133323422, N'12f0fce0-d463-49cf-9d92-71aa19906172', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (241, 0x223133323422, N'0246b1f6-c693-4e18-b486-fc1f688f6c74', 0x22476F6F64206D6F726E696E672122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (242, 0x223133323422, N'61317d36-ea46-4afb-8e8c-555e5e135a2e', 0x22476F6F64206D6F726E696E672122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (243, 0x223133323422, N'd6339970-c395-4c53-b836-aa5b75363a9f', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (244, 0x223133323422, N'c99a65c8-4d23-4b72-9e73-dbbab6eea575', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (245, 0x223133323422, N'97b46c84-25f1-4b14-b4ce-ecfcd1801bf6', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (246, 0x223133323422, N'a1d5220e-d9a2-47c1-a7e8-8687a1cc3ff8', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (247, 0x223133323422, N'b8c29583-b195-4b28-afda-53b5fc60c671', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (248, 0x223133323422, N'7d392ff9-525f-4211-9444-3853d972c4ac', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (249, 0x223133323422, N'70a24945-eab6-40da-96ba-c5bcbb041738', 0x22476F6F64206D6F726E696E67207369722122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (250, 0x2231323322, N'75709e02-b16c-47e4-9868-8126a9c225f6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (251, 0x2248617070792062697268206461792E22, N'f7cee384-9144-42bd-986b-a28eef245ec5', 0x224368E1BBA963206DE1BBAB6E672073696E68206E68E1BAAD742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (252, 0x2248617070792062697268206461792E22, N'18498f0f-e291-4880-8084-565f0d92f496', 0x224368E1BBA963206DE1BBAB6E672073696E68206E68E1BAAD742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (253, 0x2231323322, N'c103d92b-2e7c-4450-8876-61d885b7f286', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (254, 0x2231323322, N'e46b6aa9-757f-440d-93e9-dc1d2d2665b1', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (255, 0x2231323322, N'65100876-2c04-41d2-89d4-78405dc1d4b7', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (256, 0x2231323322, N'4b522410-e67f-412c-b27c-3a8e7d0359b4', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (257, 0x224861707079206269727468206461792122, N'1c891a7e-690d-4020-8cfc-2152bb638c99', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (258, 0x224861707079206269727468206461792122, N'd69fa3c5-a117-4c21-9d55-c22fa5342298', 0x224368C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (259, 0x2231323322, N'eaf4a436-c4f4-437c-b864-73129a231d67', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (260, 0x2231323322, N'c6bb6885-0b01-436d-acd7-5b931588dc1c', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (261, 0x2231323322, N'd61376c6-5ed9-4b62-be49-943ffa14d244', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (262, 0x2231323322, N'1c4db6d1-5214-4952-a612-f55df22fbb84', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (263, 0x2231323322, N'bcca884f-5380-4e32-affc-90346464403f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (264, 0x2231323322, N'4e740585-0864-4dee-a509-0edd1f1fdaf1', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (265, 0x2231323322, N'a7c022f2-43f9-41eb-95c2-a258785984a6', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (266, 0x2231323322, N'd6ba4eb4-1683-4cf9-b20e-26e9319ad159', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (267, 0x2231323322, N'b619c55f-b40d-4e6d-842f-c866db9abb12', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (268, 0x2231323322, N'2d1ce3b1-7b61-4201-aad3-d0fb7cf126eb', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (269, 0x2231323322, N'fb7ee1dc-f5c5-436c-a4de-156d4d3d312f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (270, 0x224348C3BA63206DE1BBAB6E672073696E68206E68E1BAAD74207175C3BD206B68C3A163682E22, N'093a28fb-47a1-4104-bd8a-93dd615a6c41', 0x2248617070792062697274682064617922)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (271, 0x224348C3BA63206DE1BBAB6E672073696E68206E68E1BAAD74207175C3BD206B68C3A163682E22, N'430cf939-bada-4e1a-8275-6191fd36b14d', 0x2248617070792062697274682064617922)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (272, 0x2231323322, N'89b58bac-0248-4cca-a9f7-5fb75717b018', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (273, 0x2231323322, N'c2043054-373f-4203-8d19-7f468e497351', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (274, 0x2231323322, N'69b81d79-661b-4c47-8fc8-2918f6a1b898', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (275, 0x2231323322, N'8cbd64b9-c46e-49b1-9200-0bd53828889c', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (276, 0x2231323322, N'd0400955-195a-4bcd-a64f-1387f93d7b97', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (277, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'0945b3a3-6e9b-494b-a15a-789d6e24c314', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (278, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'71426cf8-494b-4796-b24b-2f80c890241a', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (279, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'd703737f-46fa-440f-8340-7277817c6c34', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (280, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'54442b0d-e17c-4452-b016-a3228655f1e7', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (281, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'acd21293-e870-4b80-980b-9a7741a22b3d', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
GO
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (282, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'36c1c5f1-5db4-4546-a461-d3a521e7cb9b', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (283, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'7bf1dedb-852d-4054-9377-14917727d9e3', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (284, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'28ad6bff-a411-4bcc-9006-e99a65517c4c', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (285, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'a3d358e2-2dbd-494a-8f73-83dd7225fea6', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (286, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'afe83b9f-17a4-478b-ba6a-2a760d14dea3', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (287, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'758d0e8f-edd7-4cc3-9cf8-215db61d6f94', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (288, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'a9dc4fff-4fdd-4cd7-8680-791ed3e21639', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (289, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'd461e44f-2032-44d6-ad69-3e568d7d3ee2', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (290, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'bedab3ff-bd11-49c2-8fe1-1aaa62774757', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (291, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'd2f898a3-396e-419a-8e53-12ba949858a0', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (292, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'04830fd3-0cde-448d-8b0c-7b047355952b', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (293, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'd9d3a2ec-d40a-4710-82d5-360f04573155', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (294, 0x224E67C3A079206D6169206CE1BB9B7020313554482030303031206E6768E1BB892E22, N'c5828646-7f44-41a1-94e8-5a2add179228', 0x224E67C3A079206D61692063E1BAA3206CE1BB9B70206E6768E1BB89206E68612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (295, 0x2231323322, N'ee4d8fec-2691-4ccd-b24c-ca9b6ec7c3a7', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (296, 0x2231323322, N'6c549182-9831-41d5-93e5-4cc91ac3781c', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (297, 0x2231323322, N'e6ab3bf7-7d62-4754-8b2e-2c8320a96a80', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (298, 0x2231323322, N'c08a35d2-5bec-491e-afa7-4ad59445148c', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (299, 0x2231323322, N'a167e123-6647-4ff7-86a2-adfc591cca42', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (300, 0x2231323322, N'20613a70-890c-4ec3-93b1-d4f6ced6cf26', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (301, 0x2231323322, N'498a3325-6c24-49fa-986a-74d742cf851a', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (302, 0x2231323322, N'5c652859-0322-415d-b46c-b424cb0e04f8', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (303, 0x2231323322, N'36899cd9-c141-4469-ad06-1942190f2447', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (304, 0x2231323322, N'80312b22-3a3a-4976-b295-07f4a1be6eb4', 0x22416E682079C3AA7520656D2E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (305, 0x2231323322, N'b625385e-71c1-451d-affc-ce29e615f167', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (306, 0x2231323322, N'192ad641-7282-4a37-a2bd-c1a75ffef7ea', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (307, 0x2231323322, N'b7370ba7-59a3-4ec2-b609-6c36f52c3be4', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (308, 0x2231323322, N'791095ef-ed0e-45b9-b06b-39b8a306533f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (309, 0x224348C3BA63206DE1BBAB6E672073696E68206E68E1BAAD742E22, N'd7c16b04-51e5-4354-9f05-885ece97c155', 0x2248617070792042697274682044617922)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (310, 0x22426C6120626C6122, N'7ab6dc11-a39f-4c87-bddd-70cee8164279', 0x2250726576696577205469746C65203122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (311, 0x223A2922, N'1c6937d3-faad-47fa-ba37-a0f4be53a097', 0x22416E682059C3AA7520456D2122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (312, 0x2231323322, N'622141b5-194f-4d06-a2d9-a87d8abb4ae4', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (313, 0x2231323322, N'1a8565bc-a6ae-4db5-8ee8-172278d6383e', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (314, 0x2231323322, N'9db1d404-c480-40b1-bcde-55a55b03b7ee', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (315, 0x2231323322, N'da17a8ac-a30d-45c7-858a-bb4d98e7a436', 0x2249204C6F6E6520596F7522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (316, 0x223C3322, N'6b4d862c-0c2d-479b-817f-0a14b18d2963', 0x2249204C6F766520596F7522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (317, 0x2231323322, N'a50c1037-8aaf-4b14-9fa4-dcd9fb40b18f', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (318, 0x2231323322, N'6d9ccefa-a966-4d02-8c41-50ed9c02908a', 0x224E6F74696669636174696F6E2073656E742066726F6D203367206E6574776F726B22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (319, 0x2231323322, N'2a4f0102-859e-47d0-9448-0f18d677bac0', 0x224E6F74696669636174696F6E2073656E742066726F6D203367206E6574776F726B22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (320, 0x6E756C6C, N'86896304-4121-45f4-b948-30ffeb85a445', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (321, 0x6E756C6C, N'6aa589d6-a2fa-43a5-a0e7-9bf515fbf7cd', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (322, 0x6E756C6C, N'393f4d07-15e0-4625-86d7-eb63c250b7cc', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (323, 0x6E756C6C, N'f27bfec7-4963-4cd2-abc8-43e8a51c83c3', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (324, 0x6E756C6C, N'd087c7a5-78c7-40b7-a7ee-d0a0e23910b4', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (325, 0x6E756C6C, N'bbfde3b1-73a6-4222-997c-b67f94f9574c', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (326, 0x6E756C6C, N'2686b48e-1c41-4adc-918a-68772b2a3039', 0x6E756C6C)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (327, 0x2255575022, N'793eab55-4ce4-4d27-a69f-3f4ebd85d0fd', 0x225557502C206B616B612C22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (328, 0x224E6F74696669636174696F6E2066726F6D2055575022, N'11a28bc0-d324-4c98-b919-352a0ddd216a', 0x224E6F74696669636174696F6E20436F6E74656E742E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (329, 0x22436F6E74656E742E22, N'2c3cc579-c0b1-482a-ba17-0b348a51a708', 0x225072657669657720446174612E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (330, 0x224461746122, N'a2404c67-5da2-446c-b6c2-d6a6cdae30a4', 0x225072657669657722)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (331, 0x22426C6120626C6122, N'e9624c4d-3b0f-4dad-8bf5-40fe17ec28fc', 0x224B616B6122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (332, 0x2255575022, N'2d4c24aa-9967-4315-9801-a6f179d1274b', 0x2255575022)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (333, 0x2255575022, N'fd206ddf-9d06-460f-8d60-83ef0083db33', 0x2255575022)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (334, 0x2275777022, N'bc53333a-ff3c-4300-8552-e1ba7bfbeb2f', 0x227577703122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (335, 0x2275777022, N'7d18d8dd-cdda-4749-9fde-0efd144a4cd1', 0x227577703122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (336, 0x2275777022, N'679cf0d6-3633-4b70-bc4a-6d16d4a2dbb4', 0x227577703122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (337, 0x2275777022, N'0a88fb28-696a-4208-b94c-ab27fe6f36ec', 0x227577703222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (338, 0x2275777022, N'b32f5d33-b6e5-4511-9519-e3121bf7d6fc', 0x227577703222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (339, 0x2275777022, N'ee88129d-40dd-41e4-8ee5-99b461bf76e4', 0x227577703222)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (340, 0x2261616122, N'98e67375-d8bd-4ac4-8c34-dbea2ae277c2', 0x22616122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (341, 0x224861707079204269727468204461792122, N'ed9b869a-4f4c-44d3-839d-d8875c0dbfa5', 0x224368C3BA63204DE1BBAB6E672053696E68204E68E1BAAD74205175C3BD204B68C3A163682122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (342, 0x2231323322, N'1501dc25-b67c-4250-ae1d-49938a6967ed', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (343, 0x2231323322, N'7e7b5c73-f837-4111-a1e2-ed6d24108964', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (344, 0x2231323322, N'7cee8fae-df2b-4089-850c-94804e8fb763', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (345, 0x22313233524622, N'6708ad3b-c006-43c7-8601-682d50a2aa99', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (346, 0x225465737422, N'a2bf7b8b-06ba-49a1-973c-f42224447da3', 0x2255777022)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (347, 0x225465737422, N'ebb06246-05be-482c-b9af-01bb4e8038a7', 0x224861707079202062642E22)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (348, 0x6E756C6C, N'266bc39a-e6bc-4e10-9a0d-04a4939d790d', 0x224764686422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (349, 0x6E756C6C, N'5a76e19f-61dc-4c0e-b78e-afa2d2d965c7', 0x224864686468666822)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (350, 0x22487878686468646822, N'3cd885b2-c317-4a0a-8094-82323f286d18', 0x224864686468666822)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (351, 0x224261626122, N'10a397c4-8156-4228-8d4d-4e0c11450c1a', 0x224861686122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (352, 0x224261626122, N'a7a41f8b-c836-48df-abf1-31d3bb18576b', 0x224861686122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (353, 0x22486468676422, N'671e065f-9601-4009-88a7-1b984603287c', 0x224864686622)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (354, 0x22486468676422, N'e7c8cece-6802-4168-a00d-6fff23b4012c', 0x224864686622)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (355, 0x224861686122, N'f7cdd5d1-b2e2-4091-ae41-7ffdfc8124be', 0x224B616B6122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (356, 0x2231323322, N'a855dbbe-c9ba-4935-86fe-49837cf90470', 0x223522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (357, 0x224864686422, N'74b1e566-c896-4c27-b263-d7534195ed91', 0x22C39D6822)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (358, 0x22C3A16422, N'7dfbaa87-da93-4176-b774-81a5ec57a8af', 0x22C3A16422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (359, 0x22C3A16422, N'0d9545b8-45b9-437c-953a-c47d992702b9', 0x22C3A16422)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (360, 0x224A73686422, N'9e0d4e2e-e7ad-4b28-ad29-d4ab2d83e46c', 0x224BE1BAA122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (361, 0x226E6122, N'2ec0c836-3f00-40fd-bf35-505356e2e730', 0x2262C3A022)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (362, 0x223122, N'24494754-a620-4cb5-9072-92574077c40b', 0x223122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (363, 0x223522, N'00ee3d1a-c8b9-4c7d-bc0d-ffa3bf9f2ffc', 0x2250726576696577205469746C6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (364, 0x223522, N'44cded44-7810-440d-b907-4215a2c9ad39', 0x226C6F6E672074696D6522)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (365, 0x223522, N'c2b74b6b-f11b-4838-9dd8-22cfcb6ae7bb', 0x226C6F6E672074696D653122)
INSERT [dbo].[Notification] ([Id], [NotificationContent], [NotificationAccessKey], [NotificationPreviewContent]) VALUES (366, 0x224C657420706C61792E22, N'a3050fb9-d372-4c07-a538-6acda9816b31', 0x22486920677579732E22)
SET IDENTITY_INSERT [dbo].[Notification] OFF
SET IDENTITY_INSERT [dbo].[Receiver] ON 

INSERT [dbo].[Receiver] ([Id], [OldId], [DeviceCount]) VALUES (23, N'13', 2)
SET IDENTITY_INSERT [dbo].[Receiver] OFF
SET IDENTITY_INSERT [dbo].[ReceiverNotification] ON 

INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10251, 23, 96, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10252, 23, 97, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10253, 23, 98, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10254, 23, 99, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10255, 23, 100, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10256, 23, 101, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10257, 23, 102, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10258, 23, 103, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10259, 23, 104, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10260, 23, 105, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10261, 23, 106, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10262, 23, 107, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10263, 23, 108, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10264, 23, 109, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10265, 23, 110, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10266, 23, 111, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10267, 23, 112, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10268, 23, 113, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10269, 23, 114, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10270, 23, 115, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10271, 23, 116, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10272, 23, 117, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10273, 23, 118, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10274, 23, 119, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10275, 23, 120, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10276, 23, 121, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10277, 23, 122, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10278, 23, 123, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10279, 23, 124, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10280, 23, 125, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10281, 23, 126, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10282, 23, 127, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10283, 23, 128, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10284, 23, 129, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10285, 23, 130, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10286, 23, 131, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10287, 23, 132, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10288, 23, 133, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10289, 23, 134, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10290, 23, 135, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10291, 23, 136, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10292, 23, 137, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10293, 23, 138, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10294, 23, 139, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10295, 23, 140, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10296, 23, 141, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10297, 23, 142, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10298, 23, 143, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10299, 23, 144, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10300, 23, 145, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10301, 23, 146, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10302, 23, 147, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10303, 23, 148, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10304, 23, 149, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10305, 23, 150, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10306, 23, 151, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10307, 23, 152, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10308, 23, 153, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10309, 23, 154, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10310, 23, 155, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10311, 23, 156, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10312, 23, 157, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10313, 23, 158, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10314, 23, 159, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10315, 23, 160, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10316, 23, 161, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10317, 23, 162, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10318, 23, 163, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10319, 23, 164, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10320, 23, 165, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10321, 23, 166, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10322, 23, 167, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10323, 23, 168, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10324, 23, 169, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10325, 23, 170, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10326, 23, 171, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10327, 23, 172, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10328, 23, 173, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10329, 23, 174, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10330, 23, 175, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10331, 23, 176, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10332, 23, 177, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10333, 23, 178, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10334, 23, 179, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10335, 23, 180, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10336, 23, 181, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10337, 23, 182, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10338, 23, 183, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10339, 23, 184, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10340, 23, 185, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10341, 23, 186, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10342, 23, 187, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10343, 23, 188, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10344, 23, 189, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10345, 23, 190, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10346, 23, 191, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10347, 23, 192, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10348, 23, 193, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10349, 23, 194, 0)
GO
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10350, 23, 195, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10351, 23, 196, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10352, 23, 197, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10353, 23, 198, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10354, 23, 199, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10355, 23, 200, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10356, 23, 201, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10357, 23, 202, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10358, 23, 203, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10359, 23, 204, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10360, 23, 205, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10361, 23, 206, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10362, 23, 207, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10363, 23, 208, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10364, 23, 209, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10365, 23, 210, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10366, 23, 211, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10367, 23, 212, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10368, 23, 213, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10369, 23, 214, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10370, 23, 215, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10371, 23, 216, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10372, 23, 217, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10373, 23, 218, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10374, 23, 219, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10375, 23, 220, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10376, 23, 221, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10377, 23, 222, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10378, 23, 223, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10379, 23, 224, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10380, 23, 225, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10381, 23, 226, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10382, 23, 227, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10383, 23, 228, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10384, 23, 229, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10385, 23, 230, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10386, 23, 231, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10387, 23, 232, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10388, 23, 233, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10389, 23, 234, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10390, 23, 235, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10391, 23, 236, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10392, 23, 237, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10393, 23, 238, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10394, 23, 239, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10395, 23, 240, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10396, 23, 241, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10397, 23, 242, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10398, 23, 243, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10399, 23, 244, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10400, 23, 245, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10401, 23, 246, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10402, 23, 247, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10403, 23, 248, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10404, 23, 249, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10405, 23, 250, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10406, 23, 251, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10407, 23, 252, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10408, 23, 253, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10409, 23, 254, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10410, 23, 255, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10411, 23, 256, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10412, 23, 257, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10413, 23, 258, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10414, 23, 259, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10415, 23, 260, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10416, 23, 261, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10417, 23, 262, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10418, 23, 263, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10419, 23, 264, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10420, 23, 265, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10421, 23, 266, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10422, 23, 267, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10423, 23, 268, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10424, 23, 269, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10425, 23, 270, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10426, 23, 271, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10427, 23, 272, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10428, 23, 273, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10429, 23, 274, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10430, 23, 275, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10431, 23, 276, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10432, 23, 277, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10433, 23, 278, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10434, 23, 279, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10435, 23, 280, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10436, 23, 281, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10437, 23, 282, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10438, 23, 283, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10439, 23, 284, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10440, 23, 285, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10441, 23, 286, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10442, 23, 287, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10443, 23, 288, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10444, 23, 289, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10445, 23, 290, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10446, 23, 291, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10447, 23, 292, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10448, 23, 293, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10449, 23, 294, 0)
GO
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10450, 23, 295, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10451, 23, 296, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10452, 23, 297, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10453, 23, 298, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10454, 23, 299, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10455, 23, 300, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10456, 23, 301, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10457, 23, 302, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10458, 23, 303, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10459, 23, 304, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10460, 23, 305, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10461, 23, 306, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10462, 23, 307, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10463, 23, 308, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10464, 23, 309, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10465, 23, 310, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10466, 23, 311, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10467, 23, 312, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10468, 23, 313, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10469, 23, 314, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10470, 23, 315, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10471, 23, 316, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10472, 23, 317, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10473, 23, 318, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10474, 23, 319, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10475, 23, 324, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10476, 23, 325, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10477, 23, 326, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10478, 23, 327, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10479, 23, 328, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10480, 23, 329, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10481, 23, 330, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10482, 23, 331, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10483, 23, 332, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10484, 23, 333, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10485, 23, 334, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10486, 23, 335, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10487, 23, 336, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10488, 23, 337, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10489, 23, 338, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10490, 23, 339, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10491, 23, 340, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10492, 23, 341, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10493, 23, 342, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10494, 23, 343, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10495, 23, 344, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10496, 23, 345, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10497, 23, 346, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10498, 23, 347, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10499, 23, 348, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10500, 23, 350, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10501, 23, 352, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10502, 23, 354, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10503, 23, 355, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10504, 23, 356, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10505, 23, 357, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10506, 23, 358, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10507, 23, 359, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10508, 23, 360, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10509, 23, 361, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10510, 23, 362, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10511, 23, 363, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10512, 23, 364, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10513, 23, 365, 0)
INSERT [dbo].[ReceiverNotification] ([Id], [ReceiverNewId], [NotificationId], [IsRead]) VALUES (10514, 23, 366, 0)
SET IDENTITY_INSERT [dbo].[ReceiverNotification] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Device]    Script Date: 29-Jul-16 1:22:58 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_Device] ON [dbo].[Device]
(
	[IMEI] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [FK_Device_Receiver] FOREIGN KEY([ReceiverNewId])
REFERENCES [dbo].[Receiver] ([Id])
GO
ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [FK_Device_Receiver]
GO
ALTER TABLE [dbo].[DeviceNotification]  WITH CHECK ADD  CONSTRAINT [FK_DeviceNotification_Device] FOREIGN KEY([DeviceId])
REFERENCES [dbo].[Device] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeviceNotification] CHECK CONSTRAINT [FK_DeviceNotification_Device]
GO
ALTER TABLE [dbo].[DeviceNotification]  WITH CHECK ADD  CONSTRAINT [FK_DeviceNotification_ReceiverNotification] FOREIGN KEY([ReceiverNotificationId])
REFERENCES [dbo].[ReceiverNotification] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DeviceNotification] CHECK CONSTRAINT [FK_DeviceNotification_ReceiverNotification]
GO
ALTER TABLE [dbo].[LoginToken]  WITH CHECK ADD  CONSTRAINT [FK_LoginToken_Receiver] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Receiver] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LoginToken] CHECK CONSTRAINT [FK_LoginToken_Receiver]
GO
ALTER TABLE [dbo].[ReceiverNotification]  WITH CHECK ADD  CONSTRAINT [FK_ReceiverNotification_Notification] FOREIGN KEY([NotificationId])
REFERENCES [dbo].[Notification] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceiverNotification] CHECK CONSTRAINT [FK_ReceiverNotification_Notification]
GO
ALTER TABLE [dbo].[ReceiverNotification]  WITH CHECK ADD  CONSTRAINT [FK_ReceiverNotification_Receiver] FOREIGN KEY([ReceiverNewId])
REFERENCES [dbo].[Receiver] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceiverNotification] CHECK CONSTRAINT [FK_ReceiverNotification_Receiver]
GO
/****** Object:  StoredProcedure [dbo].[GetAllPendingDeviceNotificationInfo]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPendingDeviceNotificationInfo]
AS
BEGIN
	SELECT 
	Notification.NotificationAccessKey as NotificationAccessKey,
	Receiver.Id as ReceiverId,
	DeviceNotification.Id as DeviceNotificationId,
	Device.Id as DeviceId, Device.IMEI as DeviceIMEI, 
	Device.OSId as DeviceOSId, Device.URI as DeviceURI, 
	ReceiverNotification.NotificationId as NotificationId,
	Notification.NotificationPreviewContent as NotificationPreviewContent,
	ReceiverNotification.Id ReceiverNotificationId
	from ReceiverNotification, DeviceNotification, Notification, Device, Receiver
	WHERE
	Receiver.Id = ReceiverNotification.ReceiverNewId AND
	ReceiverNotification.NotificationId = Notification.Id AND
	ReceiverNotification.Id = DeviceNotification.ReceiverNotificationId AND
	DeviceNotification.ReceiverNotificationId = ReceiverNotification.Id AND
	DeviceNotification.DeviceId = Device.Id
END

GO
/****** Object:  StoredProcedure [dbo].[InsertDevice]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDevice]
	@ipReceiverNewID bigint,
	@ipIMEI varchar(50),
	@ipURI nvarchar(max),
	@ipOSId int
AS
BEGIN
	INSERT INTO Device(ReceiverNewId, IMEI, URI, OSId) values (@ipReceiverNewID, @ipIMEI, @ipURI, @ipOSId);
	UPDATE Receiver SET DeviceCount = DeviceCount+1 where Receiver.Id = @ipReceiverNewID;
END

GO
/****** Object:  StoredProcedure [dbo].[RegisterOrRenewToken]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterOrRenewToken]
@ipReceiverOldID varchar(100),
@ipAccessToken char(36),
@ipExpiredTime datetime
AS
BEGIN
	DECLARE @Id bigint;
	DECLARE @AccessToken char(36);
	DECLARE @ExpiredTime datetime;

	DECLARE cur_Receiver CURSOR FOR SELECT Receiver.Id, Receiver.AccessToken, Receiver.TokenExpiredTime FROM dbo.Receiver WHERE dbo.Receiver.OldId = @ipReceiverOldID;
	OPEN cur_Receiver;

	IF @@CURSOR_ROWS = 0
	BEGIN
		INSERT INTO dbo.Receiver(OldId, DeviceCount, AccessToken, TokenExpiredTime) values(@ipReceiverOldID, 1, @ipAccessToken, @ipExpiredTime);
		SELECT @@IDENTITY;
	END
	ELSE
	BEGIN
		FETCH NEXT FROM cur_Receiver INTO @Id, @AccessToken, @ExpiredTime;
		UPDATE dbo.Receiver SET TokenExpiredTime = @ipExpiredTime, AccessToken = @ipAccessToken WHERE dbo.Receiver.OldId = @ipReceiverOldID
		SELECT @Id;
	END
END

GO
/****** Object:  StoredProcedure [dbo].[RemoveAllReadNotification]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveAllReadNotification]
AS
BEGIN
	
	DELETE FROM DeviceNotification WHERE DeviceNotification.ReceiverNotificationId 
	in
	(SELECT ReceiverNotification.Id from ReceiverNotification WHERE ReceiverNotification.IsRead =1);

	DELETE FROM ReceiverNotification WHERE ReceiverNotification.IsRead = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[RemoveAllReceiverHaveNoDevice]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveAllReceiverHaveNoDevice]
AS
BEGIN
	DELETE FROM Receiver WHERE Receiver.Id IN 
	(
	SELECT Receiver.Id FROM Receiver
	WHERE Receiver.DeviceCount = 0
	);
END

GO
/****** Object:  StoredProcedure [dbo].[RemoveDevice]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveDevice]
	@ipDeviceId bigint,
	@ipReceiverId bigint
AS
BEGIN
	DELETE FROM dbo.DeviceNotification WHERE DBO.DeviceNotification.DeviceId = @ipDeviceId;
	DELETE FROM DBO.Device WHERE Device.Id = @ipDeviceId;
	UPDATE Receiver SET DeviceCount=DeviceCount-1 WHERE Receiver.Id = @ipReceiverId;
END

GO
/****** Object:  StoredProcedure [dbo].[ResetDB]    Script Date: 29-Jul-16 1:22:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ResetDB]
AS
BEGIN
	DELETE Notification;
	DELETE Receiver;
	DELETE Device;
	DELETE DeviceNotification;
	DELETE ReceiverNotification;
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ReceiverNotification"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 204
               Right = 271
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 1710
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReadReceiverNotification'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReadReceiverNotification'
GO
USE [master]
GO
ALTER DATABASE [Notification_DB] SET  READ_WRITE 
GO
