USE [master]
GO
/****** Object:  Database [Notification_DB]    Script Date: 05-May-16 10:45:07 AM ******/
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
/****** Object:  Table [dbo].[Device]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Device](
	[ReceiverNewID] [bigint] NOT NULL,
	[DeviceID] [varchar](100) NOT NULL,
	[URI] [nvarchar](max) NOT NULL,
	[OSID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Device] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceNotification]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeviceNotification](
	[ReceiverNotificationID] [bigint] NOT NULL,
	[DeviceID] [varchar](100) NOT NULL,
	[DeviceNotificationID] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.DeviceNotification] PRIMARY KEY CLUSTERED 
(
	[DeviceNotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationID] [bigint] IDENTITY(1,1) NOT NULL,
	[NotificationContent] [image] NOT NULL,
 CONSTRAINT [PK_dbo.Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OperatingSystem]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatingSystem](
	[OSID] [int] IDENTITY(1,1) NOT NULL,
	[OSName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.OperatingSystem] PRIMARY KEY CLUSTERED 
(
	[OSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Receiver]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Receiver](
	[NewID] [bigint] IDENTITY(1,1) NOT NULL,
	[OldID] [varchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Receiver] PRIMARY KEY CLUSTERED 
(
	[NewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReceiverNotification]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiverNotification](
	[ReceiverNewID] [bigint] NOT NULL,
	[NotificationID] [bigint] NOT NULL,
	[ReceiverNotificationID] [bigint] IDENTITY(1,1) NOT NULL,
	[Readed] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ReceiverNotification] PRIMARY KEY CLUSTERED 
(
	[ReceiverNotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Token]    Script Date: 05-May-16 10:45:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[TokenCodeID] [bigint] IDENTITY(1,1) NOT NULL,
	[ReceiverNewID] [bigint] NOT NULL,
	[TokenCode] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Token] PRIMARY KEY CLUSTERED 
(
	[TokenCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Device] ([ReceiverNewID], [DeviceID], [URI], [OSID]) VALUES (1, N'2', N'âsd', 2)
GO
INSERT [dbo].[Device] ([ReceiverNewID], [DeviceID], [URI], [OSID]) VALUES (1, N'21eca7ea-7b15-35c8-181b-a6ac7b9832bf', N'https://hk2.notify.windows.com/?token=AwYAAACnPbQKAXA1qoOavdt1tUunYKZxCQxiuT6uQty4IhrNujJhRSpkcqhSdJka1aqwUHa8KXNJm1zXMUhXjY6Fgf1e1Fg1qFuFSv8teKarfMMuqEKXVpdOVHfGpHfhltYJIPI%3d
', 1)
GO
INSERT [dbo].[Device] ([ReceiverNewID], [DeviceID], [URI], [OSID]) VALUES (1, N'924e8359-1b20-3452-739e-e69572656cd4', N'https://hk2.notify.windows.com/?token=AwYAAACnPbQKAXA1qoOavdt1tUunYKZxCQxiuT6uQty4IhrNujJhRSpkcqhSdJka1aqwUHa8KXNJm1zXMUhXjY6Fgf1e1Fg1qFuFSv8teKarfMMuqEKXVpdOVHfGpHfhltYJIPI%3d
', 1)
GO
SET IDENTITY_INSERT [dbo].[DeviceNotification] ON 

GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10047, N'2', 10119)
GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10048, N'2', 10122)
GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10048, N'924e8359-1b20-3452-739e-e69572656cd4', 10124)
GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10049, N'2', 10125)
GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10049, N'924e8359-1b20-3452-739e-e69572656cd4', 10127)
GO
INSERT [dbo].[DeviceNotification] ([ReceiverNotificationID], [DeviceID], [DeviceNotificationID]) VALUES (10050, N'2', 10128)
GO
SET IDENTITY_INSERT [dbo].[DeviceNotification] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationContent]) VALUES (10048, 0x7B224C696E6531223A22C490E1BAA1692068E1BB8D632042C3AC6E682044C6B0C6A16E67222C224C696E6532223A22C3A164617364227D)
GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationContent]) VALUES (10049, 0x7B224C696E6531223A22C490E1BAA1692068E1BB8D632042C3AC6E682044C6B0C6A16E67222C224C696E6532223A22C3A164617364227D)
GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationContent]) VALUES (10050, 0x7B224C696E6531223A22C490E1BAA1692068E1BB8D632042C3AC6E682044C6B0C6A16E67222C224C696E6532223A22C3A164617364227D)
GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationContent]) VALUES (10051, 0x7B224C696E6531223A22C490E1BAA1692068E1BB8D632042C3AC6E682044C6B0C6A16E67222C224C696E6532223A22C3A164617364227D)
GO
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[OperatingSystem] ON 

GO
INSERT [dbo].[OperatingSystem] ([OSID], [OSName]) VALUES (1, N'Windows')
GO
INSERT [dbo].[OperatingSystem] ([OSID], [OSName]) VALUES (2, N'Android')
GO
SET IDENTITY_INSERT [dbo].[OperatingSystem] OFF
GO
SET IDENTITY_INSERT [dbo].[Receiver] ON 

GO
INSERT [dbo].[Receiver] ([NewID], [OldID]) VALUES (1, N'd')
GO
SET IDENTITY_INSERT [dbo].[Receiver] OFF
GO
SET IDENTITY_INSERT [dbo].[ReceiverNotification] ON 

GO
INSERT [dbo].[ReceiverNotification] ([ReceiverNewID], [NotificationID], [ReceiverNotificationID], [Readed]) VALUES (1, 10048, 10047, 0)
GO
INSERT [dbo].[ReceiverNotification] ([ReceiverNewID], [NotificationID], [ReceiverNotificationID], [Readed]) VALUES (1, 10049, 10048, 0)
GO
INSERT [dbo].[ReceiverNotification] ([ReceiverNewID], [NotificationID], [ReceiverNotificationID], [Readed]) VALUES (1, 10050, 10049, 0)
GO
INSERT [dbo].[ReceiverNotification] ([ReceiverNewID], [NotificationID], [ReceiverNotificationID], [Readed]) VALUES (1, 10051, 10050, 0)
GO
SET IDENTITY_INSERT [dbo].[ReceiverNotification] OFF
GO
ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [OperatingSystem_Device] FOREIGN KEY([OSID])
REFERENCES [dbo].[OperatingSystem] ([OSID])
GO
ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [OperatingSystem_Device]
GO
ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [Receiver_Device] FOREIGN KEY([ReceiverNewID])
REFERENCES [dbo].[Receiver] ([NewID])
GO
ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [Receiver_Device]
GO
ALTER TABLE [dbo].[DeviceNotification]  WITH CHECK ADD  CONSTRAINT [Device_DeviceNotification] FOREIGN KEY([DeviceID])
REFERENCES [dbo].[Device] ([DeviceID])
GO
ALTER TABLE [dbo].[DeviceNotification] CHECK CONSTRAINT [Device_DeviceNotification]
GO
ALTER TABLE [dbo].[DeviceNotification]  WITH CHECK ADD  CONSTRAINT [ReceiverNotification_DeviceNotification] FOREIGN KEY([ReceiverNotificationID])
REFERENCES [dbo].[ReceiverNotification] ([ReceiverNotificationID])
GO
ALTER TABLE [dbo].[DeviceNotification] CHECK CONSTRAINT [ReceiverNotification_DeviceNotification]
GO
ALTER TABLE [dbo].[ReceiverNotification]  WITH CHECK ADD  CONSTRAINT [Notification_ReceiverNotification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[Notification] ([NotificationID])
GO
ALTER TABLE [dbo].[ReceiverNotification] CHECK CONSTRAINT [Notification_ReceiverNotification]
GO
ALTER TABLE [dbo].[ReceiverNotification]  WITH CHECK ADD  CONSTRAINT [Receiver_ReceiverNotification] FOREIGN KEY([ReceiverNewID])
REFERENCES [dbo].[Receiver] ([NewID])
GO
ALTER TABLE [dbo].[ReceiverNotification] CHECK CONSTRAINT [Receiver_ReceiverNotification]
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [Receiver_Token] FOREIGN KEY([ReceiverNewID])
REFERENCES [dbo].[Receiver] ([NewID])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [Receiver_Token]
GO
USE [master]
GO
ALTER DATABASE [Notification_DB] SET  READ_WRITE 
GO
