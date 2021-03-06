USE [master]
GO
/****** Object:  Database [QLKH]    Script Date: 08-Jul-16 1:23:43 PM ******/
CREATE DATABASE [QLKH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLKH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLKH.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLKH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLKH_log.ldf' , SIZE = 8512KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLKH] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLKH].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLKH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLKH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLKH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLKH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLKH] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLKH] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLKH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLKH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLKH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLKH] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLKH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLKH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLKH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLKH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLKH] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLKH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLKH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLKH] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLKH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLKH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLKH] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLKH] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLKH] SET RECOVERY FULL 
GO
ALTER DATABASE [QLKH] SET  MULTI_USER 
GO
ALTER DATABASE [QLKH] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLKH] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLKH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLKH] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLKH] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLKH', N'ON'
GO
USE [QLKH]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[ChucVuID] [int] IDENTITY(1,1) NOT NULL,
	[TenChucVu] [nvarchar](50) NULL,
	[QuyenID] [int] NULL,
 CONSTRAINT [PK_ChucVu] PRIMARY KEY CLUSTERED 
(
	[ChucVuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConNguoi]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConNguoi](
	[ConNguoiID] [int] IDENTITY(1,1) NOT NULL,
	[CMND] [varchar](12) NULL,
	[HoTen] [nvarchar](100) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[GioiTinh] [bit] NULL,
	[NgaySinh] [date] NULL,
	[Phone] [varchar](15) NULL,
	[Phone2] [varchar](15) NULL,
	[Email] [varchar](50) NULL,
	[MatKhau] [nvarchar](32) NULL,
 CONSTRAINT [PK_ConNguoi] PRIMARY KEY CLUSTERED 
(
	[ConNguoiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CuocGoi]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuocGoi](
	[KhachHangID] [int] NOT NULL,
	[ThoiDiemGoi] [datetime] NOT NULL,
	[ThoiDiemKetThuc] [datetime] NOT NULL,
	[GhiChu] [ntext] NULL,
	[NhanVienID] [int] NOT NULL,
	[LoaiCuocGoiID] [int] NOT NULL,
	[TragThaiKH] [int] NOT NULL,
	[CuocGoiID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CuocGoi] PRIMARY KEY CLUSTERED 
(
	[CuocGoiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DanhSachChucVu]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhSachChucVu](
	[NhanVienID] [int] NOT NULL,
	[ChucVuID] [int] NOT NULL,
 CONSTRAINT [PK_DanhSachChucVu] PRIMARY KEY CLUSTERED 
(
	[NhanVienID] ASC,
	[ChucVuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[KhachHangID] [int] IDENTITY(1,1) NOT NULL,
	[TongGTGiaoDich] [int] NULL,
	[ConNguoiID] [int] NOT NULL,
	[LoaiKhachHangID] [int] NULL,
 CONSTRAINT [PK_dbo.KhachHang] PRIMARY KEY CLUSTERED 
(
	[KhachHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiCuocGoi]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiCuocGoi](
	[LoaiCuocGoiID] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiCuocGoi] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiCuocGoi] PRIMARY KEY CLUSTERED 
(
	[LoaiCuocGoiID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiKhachHang]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiKhachHang](
	[LoaiKhachHangID] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiKhachHang] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiKhachHang] PRIMARY KEY CLUSTERED 
(
	[LoaiKhachHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiQuanHe]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiQuanHe](
	[LoaiQuanHeID] [int] IDENTITY(1,1) NOT NULL,
	[TenQuanHe] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.LoaiQuanHe] PRIMARY KEY CLUSTERED 
(
	[LoaiQuanHeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MuaHang]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MuaHang](
	[KhachHangID] [int] NOT NULL,
	[MaMuaHang] [int] NOT NULL,
	[GiaTri] [int] NULL,
	[NgayMua] [datetime] NULL,
 CONSTRAINT [PK_MuaHang] PRIMARY KEY CLUSTERED 
(
	[MaMuaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NguoiThan]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiThan](
	[NguoiThanID] [int] IDENTITY(1,1) NOT NULL,
	[KhachHangID] [int] NOT NULL,
	[LoaiQuanHeID] [int] NOT NULL,
	[ConNguoiID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.NguoiThan] PRIMARY KEY CLUSTERED 
(
	[NguoiThanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[NhanVienID] [int] IDENTITY(1,1) NOT NULL,
	[ConNguoiID] [int] NOT NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[NhanVienID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuaTang]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuaTang](
	[QuaTangID] [int] IDENTITY(1,1) NOT NULL,
	[KHID] [int] NOT NULL,
	[TenQua] [nvarchar](100) NULL,
	[GiaTri] [float] NOT NULL,
	[NgayGui] [date] NULL,
	[DaNhan] [bit] NULL,
	[NhanVienID] [int] NOT NULL,
	[DaGui] [bit] NOT NULL CONSTRAINT [DF_QuaTang_DaGui]  DEFAULT ((0)),
	[NgayLenKeHoach] [date] NOT NULL,
 CONSTRAINT [PK_TangQua] PRIMARY KEY CLUSTERED 
(
	[QuaTangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Quyen]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quyen](
	[QuyenID] [int] IDENTITY(1,1) NOT NULL,
	[PhanQuyen] [bit] NOT NULL,
	[HoTroGiaoTiepKH] [bit] NOT NULL,
	[XoaCuocGoi] [bit] NOT NULL,
	[QLTTCaNhanKH] [bit] NOT NULL,
	[SuaTTKH] [bit] NOT NULL,
	[XoaTTKH] [bit] NOT NULL,
	[QLTTNV] [bit] NOT NULL,
	[BaoCaoThongKe] [bit] NOT NULL,
	[QLTTQuaTang] [bit] NOT NULL,
	[XemTTQuaTang] [bit] NOT NULL,
	[XemTTNhanVien] [bit] NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[QuyenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SinhNhat]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SinhNhat](
	[KhachHangID] [int] NOT NULL,
	[DaGoi] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.SinhNhat] PRIMARY KEY CLUSTERED 
(
	[KhachHangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([ChucVuID], [TenChucVu], [QuyenID]) VALUES (1, N'Admin', 3)
INSERT [dbo].[ChucVu] ([ChucVuID], [TenChucVu], [QuyenID]) VALUES (2, N'Staff', 4)
INSERT [dbo].[ChucVu] ([ChucVuID], [TenChucVu], [QuyenID]) VALUES (3, N'Manager', 2)
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
SET IDENTITY_INSERT [dbo].[ConNguoi] ON 

INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (0, N'173898321', N'Administrator', N'Kí túc xá trung cấp nghề Bình Dương, Khu K8, Hiệp Thành, Thủ Dầu Một, Bình Dương', 1, CAST(N'2015-06-02' AS Date), N'0993142171', N'01678141401', N'gemini02061994@outlook.com', N'123')
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (18, N'173898321', N'Manager', N'Đông thanh', 1, CAST(N'2015-10-25' AS Date), N'12637', N'6218', N'672868@gmail.com', N'123')
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (19, N'173898321', N'Staff', NULL, NULL, CAST(N'2015-10-16' AS Date), N'0993142171', N'01678141401', NULL, N'123')
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (20, N'173898321', N'Super', NULL, NULL, CAST(N'2015-10-16' AS Date), N'0993142171', NULL, NULL, N'123')
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (21, N'17389821', N'user', N'bhjasjd', 1, CAST(N'2015-10-14' AS Date), N'2173', N'68276287', N'b6237', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (22, N'173898321', N'another user', NULL, NULL, CAST(N'2015-10-20' AS Date), N'1371871', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (24, N'0126895624', N'today', N'asbdh', 1, CAST(N'2015-10-16' AS Date), N'1237', N'62173', N'6283', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (25, N'173898321', N'Lê Khả Sỹ', N'Thủ Dầu Một', 1, CAST(N'2015-03-11' AS Date), N'0993142171', N'0912352648', N'gemini02061994@outlook.com', N'123')
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (26, NULL, N'Duy Linh', NULL, NULL, CAST(N'2015-10-21' AS Date), N'0996211495', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (27, NULL, N'Hồng Vân', NULL, NULL, CAST(N'1984-03-01' AS Date), N'01678141401', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (28, NULL, N'Tuyết Thanh', NULL, NULL, CAST(N'1985-03-01' AS Date), N'0906678267', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (29, N'173898564', N'Nguyễn Văn Duy', N'Q1, TP.HCM', 1, CAST(N'2015-10-29' AS Date), N'0991263', N'11273812', N'hoctrokha@yahoo.com', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (30, NULL, N'Con của Nguyễn Văn A', NULL, NULL, CAST(N'2015-10-29' AS Date), N'012564859', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (31, NULL, N'Vợ của Nguyễn Văn A', NULL, NULL, CAST(N'2015-10-20' AS Date), N'49815116165', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (32, N'111651651561', N'Nguyễn Văn B', N'Bến Cát, Bình Dương', 1, CAST(N'2015-10-16' AS Date), N'0993142161', N'15165161616', N'hoctrokha@gmail.com', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (33, N'16156161516', N'Nguyễn Văn C', N'TDM', 1, CAST(N'2015-10-16' AS Date), N'099256215', N'1561616156', N'gemini02061994@outlook.com', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (34, NULL, N'Vợ của Nguyễn Văn C', NULL, NULL, CAST(N'2015-10-12' AS Date), N'46511651651', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (35, NULL, N'Con của Nguyễn Văn C', NULL, NULL, CAST(N'2015-10-12' AS Date), N'12321432534', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (36, N'61', N'Nguyễn Văn D', N'16155', 1, CAST(N'2015-10-16' AS Date), N'151561', N'1', N'61', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (37, NULL, N'Vợ của Nguyễn Văn D', NULL, NULL, CAST(N'2015-10-12' AS Date), N'1', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (38, NULL, N'Con của Nguyễn Văn D', NULL, NULL, CAST(N'2015-10-12' AS Date), N'13123', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (1029, N'162783217836', N'Khả Sỹ', N'abc', 1, CAST(N'2015-10-08' AS Date), N'515351266162376', N'162368263817321', N'jashdkasdj', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (1030, N'', N'', N'', 1, CAST(N'2015-10-08' AS Date), N'', N'', N'', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (2029, N'126532512', N'Nguyễn Văn X', N'Thủ Dầu Một, Bình Dương', 1, CAST(N'2015-11-10' AS Date), N'123', N'123', N'hoctrokha@yahoo.com', NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (2031, NULL, N'Xuan Bình', NULL, NULL, CAST(N'1994-07-14' AS Date), N'12456', NULL, NULL, NULL)
INSERT [dbo].[ConNguoi] ([ConNguoiID], [CMND], [HoTen], [DiaChi], [GioiTinh], [NgaySinh], [Phone], [Phone2], [Email], [MatKhau]) VALUES (2032, N'123', N'Nhân viên mới', N'123', 1, CAST(N'2015-11-11' AS Date), N'1', N'12', N'hoctrokha@gmail.com', NULL)
SET IDENTITY_INSERT [dbo].[ConNguoi] OFF
SET IDENTITY_INSERT [dbo].[CuocGoi] ON 

INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (10, CAST(N'2015-10-25 04:40:37.263' AS DateTime), CAST(N'2015-10-25 04:40:42.370' AS DateTime), N'bla', 2, 1, 3, 2)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (10, CAST(N'2015-10-25 04:41:38.760' AS DateTime), CAST(N'2015-10-25 04:41:46.863' AS DateTime), N'Haizz', 2, 2, 6, 3)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (10, CAST(N'2015-10-28 15:18:26.597' AS DateTime), CAST(N'2015-10-28 15:18:40.203' AS DateTime), N'Ghi chu', 2, 1, 4, 4)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (13, CAST(N'2015-10-29 08:04:46.790' AS DateTime), CAST(N'2015-10-29 08:04:56.990' AS DateTime), N'Bla Bla', 2, 1, 6, 1004)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (10, CAST(N'2015-10-29 13:18:54.523' AS DateTime), CAST(N'2015-10-29 13:19:04.693' AS DateTime), N'I hate this', 2, 2, 5, 1005)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (13, CAST(N'2015-10-29 14:31:35.717' AS DateTime), CAST(N'2015-10-29 14:32:17.857' AS DateTime), N'Họ nói Bla Bla', 2, 1, 5, 1006)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (1014, CAST(N'2015-11-08 15:59:15.187' AS DateTime), CAST(N'2015-11-08 15:59:17.703' AS DateTime), N'12312312', 2, 1, 6, 2007)
INSERT [dbo].[CuocGoi] ([KhachHangID], [ThoiDiemGoi], [ThoiDiemKetThuc], [GhiChu], [NhanVienID], [LoaiCuocGoiID], [TragThaiKH], [CuocGoiID]) VALUES (10, CAST(N'2016-06-02 12:50:04.977' AS DateTime), CAST(N'2016-06-02 12:50:38.673' AS DateTime), N'bla bla', 2, 1, 7, 2008)
SET IDENTITY_INSERT [dbo].[CuocGoi] OFF
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (2, 1)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (2, 3)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (3, 3)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (4, 2)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (5, 1)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (5, 2)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (5, 3)
INSERT [dbo].[DanhSachChucVu] ([NhanVienID], [ChucVuID]) VALUES (6, 1)
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (10, 0, 18, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (13, NULL, 25, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (14, NULL, 29, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (15, NULL, 32, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (16, NULL, 33, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (17, NULL, 36, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (1014, NULL, 1029, NULL)
INSERT [dbo].[KhachHang] ([KhachHangID], [TongGTGiaoDich], [ConNguoiID], [LoaiKhachHangID]) VALUES (2014, NULL, 2029, NULL)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
SET IDENTITY_INSERT [dbo].[LoaiCuocGoi] ON 

INSERT [dbo].[LoaiCuocGoi] ([LoaiCuocGoiID], [TenLoaiCuocGoi]) VALUES (1, N'Phàn nàn')
INSERT [dbo].[LoaiCuocGoi] ([LoaiCuocGoiID], [TenLoaiCuocGoi]) VALUES (2, N'Thắc mắc')
INSERT [dbo].[LoaiCuocGoi] ([LoaiCuocGoiID], [TenLoaiCuocGoi]) VALUES (3, N'Yêu Cầu')
SET IDENTITY_INSERT [dbo].[LoaiCuocGoi] OFF
SET IDENTITY_INSERT [dbo].[LoaiQuanHe] ON 

INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (1, N'Vợ')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (2, N'Chồng')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (3, N'Cha')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (4, N'Mẹ')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (5, N'Ông')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (6, N'Bà')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (7, N'Anh')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (8, N'Chị')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (9, N'Con')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (10, N'Cháu')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (11, N'Em')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (12, N'Bạn')
INSERT [dbo].[LoaiQuanHe] ([LoaiQuanHeID], [TenQuanHe]) VALUES (13, N'Họ hàng')
SET IDENTITY_INSERT [dbo].[LoaiQuanHe] OFF
SET IDENTITY_INSERT [dbo].[NguoiThan] ON 

INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (10, 10, 2, 20)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (11, 10, 1, 22)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (13, 13, 1, 26)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (14, 13, 8, 27)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (15, 13, 8, 28)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (16, 14, 9, 30)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (17, 14, 1, 31)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (18, 16, 1, 34)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (19, 16, 9, 35)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (20, 17, 1, 37)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (21, 17, 1, 38)
INSERT [dbo].[NguoiThan] ([NguoiThanID], [KhachHangID], [LoaiQuanHeID], [ConNguoiID]) VALUES (1017, 13, 12, 2031)
SET IDENTITY_INSERT [dbo].[NguoiThan] OFF
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([NhanVienID], [ConNguoiID]) VALUES (2, 0)
INSERT [dbo].[NhanVien] ([NhanVienID], [ConNguoiID]) VALUES (3, 18)
INSERT [dbo].[NhanVien] ([NhanVienID], [ConNguoiID]) VALUES (4, 19)
INSERT [dbo].[NhanVien] ([NhanVienID], [ConNguoiID]) VALUES (5, 20)
INSERT [dbo].[NhanVien] ([NhanVienID], [ConNguoiID]) VALUES (6, 2032)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
SET IDENTITY_INSERT [dbo].[QuaTang] ON 

INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (1, 13, N'qua 1', 50000, CAST(N'2015-07-10' AS Date), 1, 2, 0, CAST(N'2015-11-10' AS Date))
INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (2, 13, N'qua 2', 60000, CAST(N'2015-07-10' AS Date), 1, 3, 0, CAST(N'2015-10-11' AS Date))
INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (1002, 10, N'Gấu Bông', 50000, NULL, 0, 4, 1, CAST(N'2015-11-10' AS Date))
INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (1003, 15, N'Mứt Dâu', 50000, NULL, 0, 4, 1, CAST(N'2015-11-10' AS Date))
INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (1004, 1014, N'Kẹo Dừa', 50000, NULL, 0, 4, 1, CAST(N'2015-11-10' AS Date))
INSERT [dbo].[QuaTang] ([QuaTangID], [KHID], [TenQua], [GiaTri], [NgayGui], [DaNhan], [NhanVienID], [DaGui], [NgayLenKeHoach]) VALUES (1006, 13, NULL, 50000, NULL, 0, 5, 0, CAST(N'0001-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[QuaTang] OFF
SET IDENTITY_INSERT [dbo].[Quyen] ON 

INSERT [dbo].[Quyen] ([QuyenID], [PhanQuyen], [HoTroGiaoTiepKH], [XoaCuocGoi], [QLTTCaNhanKH], [SuaTTKH], [XoaTTKH], [QLTTNV], [BaoCaoThongKe], [QLTTQuaTang], [XemTTQuaTang], [XemTTNhanVien]) VALUES (2, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1)
INSERT [dbo].[Quyen] ([QuyenID], [PhanQuyen], [HoTroGiaoTiepKH], [XoaCuocGoi], [QLTTCaNhanKH], [SuaTTKH], [XoaTTKH], [QLTTNV], [BaoCaoThongKe], [QLTTQuaTang], [XemTTQuaTang], [XemTTNhanVien]) VALUES (3, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1)
INSERT [dbo].[Quyen] ([QuyenID], [PhanQuyen], [HoTroGiaoTiepKH], [XoaCuocGoi], [QLTTCaNhanKH], [SuaTTKH], [XoaTTKH], [QLTTNV], [BaoCaoThongKe], [QLTTQuaTang], [XemTTQuaTang], [XemTTNhanVien]) VALUES (4, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0)
SET IDENTITY_INSERT [dbo].[Quyen] OFF
ALTER TABLE [dbo].[ChucVu]  WITH CHECK ADD  CONSTRAINT [FK_ChucVu_Quyen] FOREIGN KEY([QuyenID])
REFERENCES [dbo].[Quyen] ([QuyenID])
GO
ALTER TABLE [dbo].[ChucVu] CHECK CONSTRAINT [FK_ChucVu_Quyen]
GO
ALTER TABLE [dbo].[CuocGoi]  WITH CHECK ADD  CONSTRAINT [FK_CuocGoi_LoaiCuocGoi] FOREIGN KEY([LoaiCuocGoiID])
REFERENCES [dbo].[LoaiCuocGoi] ([LoaiCuocGoiID])
GO
ALTER TABLE [dbo].[CuocGoi] CHECK CONSTRAINT [FK_CuocGoi_LoaiCuocGoi]
GO
ALTER TABLE [dbo].[CuocGoi]  WITH CHECK ADD  CONSTRAINT [FK_CuocGoi_NhanVien] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[NhanVien] ([NhanVienID])
GO
ALTER TABLE [dbo].[CuocGoi] CHECK CONSTRAINT [FK_CuocGoi_NhanVien]
GO
ALTER TABLE [dbo].[CuocGoi]  WITH CHECK ADD  CONSTRAINT [KhachHang_CuocGoi] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
GO
ALTER TABLE [dbo].[CuocGoi] CHECK CONSTRAINT [KhachHang_CuocGoi]
GO
ALTER TABLE [dbo].[DanhSachChucVu]  WITH CHECK ADD  CONSTRAINT [FK_DanhSachChucVu_ChucVu] FOREIGN KEY([ChucVuID])
REFERENCES [dbo].[ChucVu] ([ChucVuID])
GO
ALTER TABLE [dbo].[DanhSachChucVu] CHECK CONSTRAINT [FK_DanhSachChucVu_ChucVu]
GO
ALTER TABLE [dbo].[DanhSachChucVu]  WITH CHECK ADD  CONSTRAINT [FK_DanhSachChucVu_NhanVien] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[NhanVien] ([NhanVienID])
GO
ALTER TABLE [dbo].[DanhSachChucVu] CHECK CONSTRAINT [FK_DanhSachChucVu_NhanVien]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_ConNguoi] FOREIGN KEY([ConNguoiID])
REFERENCES [dbo].[ConNguoi] ([ConNguoiID])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_ConNguoi]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_LoaiKhachHang] FOREIGN KEY([LoaiKhachHangID])
REFERENCES [dbo].[LoaiKhachHang] ([LoaiKhachHangID])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_LoaiKhachHang]
GO
ALTER TABLE [dbo].[MuaHang]  WITH CHECK ADD  CONSTRAINT [FK_MuaHang_KhachHang] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
GO
ALTER TABLE [dbo].[MuaHang] CHECK CONSTRAINT [FK_MuaHang_KhachHang]
GO
ALTER TABLE [dbo].[NguoiThan]  WITH CHECK ADD  CONSTRAINT [FK_NguoiThan_ConNguoi] FOREIGN KEY([ConNguoiID])
REFERENCES [dbo].[ConNguoi] ([ConNguoiID])
GO
ALTER TABLE [dbo].[NguoiThan] CHECK CONSTRAINT [FK_NguoiThan_ConNguoi]
GO
ALTER TABLE [dbo].[NguoiThan]  WITH CHECK ADD  CONSTRAINT [KhachHang_NguoiThan] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NguoiThan] CHECK CONSTRAINT [KhachHang_NguoiThan]
GO
ALTER TABLE [dbo].[NguoiThan]  WITH CHECK ADD  CONSTRAINT [LoaiQuanHe_NguoiThan] FOREIGN KEY([LoaiQuanHeID])
REFERENCES [dbo].[LoaiQuanHe] ([LoaiQuanHeID])
GO
ALTER TABLE [dbo].[NguoiThan] CHECK CONSTRAINT [LoaiQuanHe_NguoiThan]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_ConNguoi] FOREIGN KEY([ConNguoiID])
REFERENCES [dbo].[ConNguoi] ([ConNguoiID])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_ConNguoi]
GO
ALTER TABLE [dbo].[QuaTang]  WITH CHECK ADD  CONSTRAINT [FK_QuaTang_KhachHang] FOREIGN KEY([KHID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
GO
ALTER TABLE [dbo].[QuaTang] CHECK CONSTRAINT [FK_QuaTang_KhachHang]
GO
ALTER TABLE [dbo].[QuaTang]  WITH CHECK ADD  CONSTRAINT [FK_QuaTang_NhanVien] FOREIGN KEY([NhanVienID])
REFERENCES [dbo].[NhanVien] ([NhanVienID])
GO
ALTER TABLE [dbo].[QuaTang] CHECK CONSTRAINT [FK_QuaTang_NhanVien]
GO
ALTER TABLE [dbo].[SinhNhat]  WITH CHECK ADD  CONSTRAINT [KhachHang_SinhNhat] FOREIGN KEY([KhachHangID])
REFERENCES [dbo].[KhachHang] ([KhachHangID])
GO
ALTER TABLE [dbo].[SinhNhat] CHECK CONSTRAINT [KhachHang_SinhNhat]
GO
/****** Object:  StoredProcedure [dbo].[BackUpDB]    Script Date: 08-Jul-16 1:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[BackUpDB] @FileName  nvarchar(250) AS BEGIN BACKUP DATABASE[QLKH] TO  DISK = @Filename END

GO
USE [master]
GO
ALTER DATABASE [QLKH] SET  READ_WRITE 
GO
