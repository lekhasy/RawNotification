using System.Linq;
using System;

namespace QLKH.ViewModels
{
    public class LoginViewModel : SuperViewModel
    {
        public Models.NhanVien nv = new Models.NhanVien();
        public event EventHandler<bool> LoginResultEvent = null;
        public void Login(string UserName, string Password)
        {
            try
            {
                nv = db.NhanViens.Single(tt => tt.NhanVienID.ToString() == UserName && tt.ConNguoi.MatKhau == Password);

                // Tạo một đối tượng quyền mà tất cả các quyền của nó đều bằng false
                Models.Quyen VTquyen = new Models.Quyen
                {
                    BaoCaoThongKe = false,
                    HoTroGiaoTiepKH = false,
                    PhanQuyen = false,
                    QLTTCaNhanKH = false,
                    QLTTNV = false,
                    QLTTQuaTang = false,
                    SuaTTKH = false,
                    XemTTNhanVien = false,
                    XemTTQuaTang = false,
                    XoaCuocGoi = false,
                    XoaTTKH = false,
                };

                // chạy trong danh sách các quyền mà người này có để lấ ra một đối tượng quyền là sự kết hợp của các quyền khác
                var quyens = from cvs in db.DanhSachChucVus
                             join cv in db.ChucVus on cvs.ChucVuID equals cv.ChucVuID
                             join q in db.Quyens on cv.QuyenID equals q.QuyenID
                             where cvs.NhanVienID == nv.NhanVienID
                             select q;
                foreach (var q in quyens)
                {
                    if (q.BaoCaoThongKe == true) { VTquyen.BaoCaoThongKe = true; }
                    if (q.HoTroGiaoTiepKH == true) { VTquyen.HoTroGiaoTiepKH = true; }
                    if (q.PhanQuyen == true) { VTquyen.PhanQuyen = true; }
                    if (q.QLTTCaNhanKH == true) { VTquyen.QLTTCaNhanKH = true; }
                    if (q.QLTTNV == true) { VTquyen.QLTTNV = true; }
                    if (q.QLTTQuaTang == true) { VTquyen.QLTTQuaTang = true; }
                    if (q.SuaTTKH == true) { VTquyen.SuaTTKH = true; }
                    if (q.XemTTNhanVien == true) { VTquyen.XemTTNhanVien = true; }
                    if (q.XemTTQuaTang == true) { VTquyen.XemTTQuaTang = true; }
                    if (q.XoaCuocGoi == true) { VTquyen.XoaCuocGoi = true; }
                    if (q.XoaTTKH == true) { VTquyen.XoaTTKH = true; }
                }
                App.CurrentPermissions = VTquyen;
                App.CurentHuman = nv.ConNguoi;
                App.CurentEmployee = nv;

                LoginResultEvent(this, true);
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException)
                {
                    LoginResultEvent(this, false);
                }
                else if (ex is System.Data.SqlClient.SqlException)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
            }
        }
    }
}
