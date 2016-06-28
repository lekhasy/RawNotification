using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QLKH
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Models.Quyen CurrentPermissions;
        public static Models.ConNguoi CurentHuman;
        public static Models.NhanVien CurentEmployee;
        public static string appPath = Path.GetDirectoryName(Application.ResourceAssembly.Location);
        public static Models.AppSetting Setting;
        public static Lib.Logger Log;

        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            Setting = new Models.AppSetting();
            WorkExcuter.WorksManager.Initialize(new TimeSpan(0, 0, 3));
            Setting.CheckAndFixError();
        }

        public static void AppInitialize()
        {
            App.Log = new Lib.Logger(new DirectoryInfo(@"Log/"));
            Models.DBDataContext db = new Models.DBDataContext();
            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
                Models.ChucVu ChucManager = new Models.ChucVu { Quyen = new Models.Quyen { PhanQuyen = false, HoTroGiaoTiepKH = true, XoaCuocGoi = true, QLTTCaNhanKH = true, SuaTTKH = true, XoaTTKH = true, QLTTNV = false, BaoCaoThongKe = true, QLTTQuaTang = true, XemTTQuaTang = true, XemTTNhanVien = true } };
                Models.ChucVu ChucAdmin = new Models.ChucVu { Quyen = new Models.Quyen { PhanQuyen = true, HoTroGiaoTiepKH = false, XoaCuocGoi = false, QLTTCaNhanKH = false, SuaTTKH = false, XoaTTKH = false, QLTTNV = true, BaoCaoThongKe = false, QLTTQuaTang = false, XemTTQuaTang = false, XemTTNhanVien = true } };
                Models.ChucVu ChucStaff = new Models.ChucVu { Quyen = new Models.Quyen { PhanQuyen = false, HoTroGiaoTiepKH = true, XoaCuocGoi = true, QLTTCaNhanKH = true, SuaTTKH = false, XoaTTKH = false, QLTTNV = false, BaoCaoThongKe = false, QLTTQuaTang = false, XemTTQuaTang = true, XemTTNhanVien = false } };
                db.ChucVus.InsertAllOnSubmit(new List<Models.ChucVu>
                {
                    ChucManager, ChucAdmin, ChucStaff
                });

                db.LoaiQuanHes.InsertAllOnSubmit(new List<Models.LoaiQuanHe>
                {
                    new Models.LoaiQuanHe { TenQuanHe = "Vợ"},
                    new Models.LoaiQuanHe { TenQuanHe = "Chồng"},
                    new Models.LoaiQuanHe { TenQuanHe = "Cha"},
                    new Models.LoaiQuanHe { TenQuanHe = "Mẹ"},
                    new Models.LoaiQuanHe { TenQuanHe = "Ông"},
                    new Models.LoaiQuanHe { TenQuanHe = "Bà"},
                    new Models.LoaiQuanHe { TenQuanHe = "Anh"},
                    new Models.LoaiQuanHe { TenQuanHe = "Chị"},
                    new Models.LoaiQuanHe { TenQuanHe = "Con"},
                    new Models.LoaiQuanHe { TenQuanHe = "Cháu"},
                    new Models.LoaiQuanHe { TenQuanHe = "Em"},
                    new Models.LoaiQuanHe { TenQuanHe = "Bạn"},
                    new Models.LoaiQuanHe { TenQuanHe = "Họ Hàng"},
                    new Models.LoaiQuanHe { TenQuanHe = "Người Quen"}
                });

                db.LoaiCuocGois.InsertAllOnSubmit(new List<Models.LoaiCuocGoi>
                {
                    new Models.LoaiCuocGoi { TenLoaiCuocGoi = "Yêu cầu"},
                    new Models.LoaiCuocGoi { TenLoaiCuocGoi = "Thắc mắc"},
                    new Models.LoaiCuocGoi { TenLoaiCuocGoi = "Phàn nàn"},
                });

                Models.NhanVien Admin = new Models.NhanVien
                {
                    ConNguoi = new Models.ConNguoi { HoTen = "Admin được khởi tạo tự động", MatKhau = "123" },
                };

                Admin.DanhSachChucVus.Add(new Models.DanhSachChucVu { ChucVu = ChucAdmin});

                db.NhanViens.InsertOnSubmit(Admin);

                db.SubmitChanges();
            }
        }
    }
}