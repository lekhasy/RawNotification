using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKH.Models
{
    public partial class SinhNhat
    {
        public static void renewBirthDay()
        {
            if (IsUpdated)
            {
                return;
            }
            DBDataContext db = new DBDataContext();
            // xóa tất cả bảng sinh nhật cũ đi
            db.SinhNhats.DeleteAllOnSubmit(db.SinhNhats);
            db.SubmitChanges();

            // lấy dữ liệu cho bảng sinh nhật mới
            var newBirthday = db.KhachHangs.Where(kh => kh.ConNguoi.NgaySinh.Value.DayOfYear == DateTime.Today.DayOfYear).Select(tt => tt.KhachHangID);            
            foreach (var item in newBirthday)
            {
                db.SinhNhats.InsertOnSubmit(new SinhNhat { KhachHangID = item, DaGoi = false });
            }
            db.SubmitChanges();
        }

        public static bool IsUpdated
        {
            get
            {
                DBDataContext db = new DBDataContext();
                var result = db.SinhNhats;
                if (result.Count() > 0)
                {// có sẵn trước đó
                    if (result.First().KhachHang.ConNguoi.NgaySinh.Value.DayOfYear < DateTime.Today.DayOfYear)
                    { // đây là danh sách sinh nhật của ngày hôm trước
                        return false;
                    }
                    { return true; }
                    // ngược lại thì là danh sách mới, không phải làm j hết
                }
                else
                { // danh sách trống thì làm mới lại danh sách
                    return false;
                }
            }
        }
    }
}
