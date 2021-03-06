USE [master]
GO
/****** Object:  Database [Notification_DB]    Script Date: 08-Jul-16 1:22:51 PM ******/
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
/****** Object:  Table [dbo].[Device]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
 CONSTRAINT [PK_dbo.Device] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeviceNotification]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  Table [dbo].[Receiver]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
	[AccessToken] [varchar](36) NULL,
	[TokenExpiredTime] [datetime] NULL,
 CONSTRAINT [PK_dbo.Receiver] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReceiverNotification]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  View [dbo].[ReadReceiverNotification]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Device]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[GetAllPendingDeviceNotificationInfo]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[InsertDevice]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RegisterOrRenewToken]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RemoveAllReadNotification]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RemoveAllReceiverHaveNoDevice]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[RemoveDevice]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
/****** Object:  StoredProcedure [dbo].[ResetDB]    Script Date: 08-Jul-16 1:22:52 PM ******/
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
