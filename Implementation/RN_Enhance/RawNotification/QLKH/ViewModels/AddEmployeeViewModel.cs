using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKH.ViewModels
{
    public class AddEmployeeViewModel : SuperViewModel
    {

        #region Attributes
        object _ChucVus = new object();
        #endregion

        #region Properties

        public object ChucVus
        {
            get { return _ChucVus; }
            set
            {
                _ChucVus = value;
            }
        }

        #endregion

        public AddEmployeeViewModel()
        {
            try
            {
                ChucVus = db.ChucVus;
            }
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }

        #region Functions

        public void AddEmployee(string Hoten, string Diachi, bool Gioitinh, DateTime? NgaySinh, String Phone1, String Phone2, String CMND, String Email, Models.ChucVu chucvu, string password)
        {
            Models.NhanVien nv = new Models.NhanVien { ConNguoi = new Models.ConNguoi { HoTen = Hoten, DiaChi = Diachi, GioiTinh = Gioitinh, NgaySinh = NgaySinh, Phone = Phone1, Phone2 = Phone2, CMND = CMND, Email = Email, MatKhau = password} };
            nv.DanhSachChucVus.Add(new Models.DanhSachChucVu { NhanVien = nv, ChucVu = chucvu });
            db.NhanViens.InsertOnSubmit(nv);
            try
            {
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }
        #endregion
    }
}