using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class MakeCallViewModel : SuperViewModel
    {
        #region Attributes

        private Models.KhachHang Customer;
        private object _LoaiCuocGois = new object();
        private int _SelectedLoaiCuocGoiID = 0;
        private int _MinimunStatus = 1;
        private int _MaximunStatus = 10;
        private DateTime StartTime = DateTime.Now;

        #endregion

        #region Properties
        public int MinimunStatus
        {
            get { return _MinimunStatus; }
        }
        public int MaximunStatus
        {
            get { return _MaximunStatus; }
        }
        public object LoaiCuocGois
        {
            get { return _LoaiCuocGois; }
            set
            {
                _LoaiCuocGois = value;
                OnPropertyChanged("LoaiCuocGois");
            }
        }
        public int SelectedLoaiCuocGoiID
        {
            get
            {
                return _SelectedLoaiCuocGoiID;
            }
            set
            {
                _SelectedLoaiCuocGoiID = value;
                OnPropertyChanged("SelectedLoaiCuocGoiID");
            }
        }

        #endregion

        public MakeCallViewModel( Models.KhachHang kh)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                try
                {
                    Customer = kh;
                    LoaiCuocGois = db.LoaiCuocGois;
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
            }));
        }

        public void AddCall(int LoaiCGoiID,int TThaiKH, String Note)
        {
            try
            {
                // Nhân viên ID chưa sửa, cần sửa lại
                db.CuocGois.InsertOnSubmit(new Models.CuocGoi { KhachHangID = Customer.KhachHangID, ThoiDiemGoi = StartTime, ThoiDiemKetThuc = DateTime.Now, LoaiCuocGoiID = LoaiCGoiID, TragThaiKH = TThaiKH, GhiChu = Note, NhanVienID = 2 });
                db.SubmitChanges();
            }
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
            
        }
    }
}