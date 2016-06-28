using System.Collections.ObjectModel;
using System.Threading;
using System;

namespace QLKH.ViewModels
{
    public class AddGiftViewModel : SuperViewModel
    {
        ObservableCollection<Models.KhachHang> _KhachHangs = new ObservableCollection<Models.KhachHang>();

        public ObservableCollection<Models.KhachHang> KhachHangs
        {
            get { return _KhachHangs; }
            set { _KhachHangs = value; }
        }

        public AddGiftViewModel(System.Collections.IList KHs)
        {
            foreach (var item in KHs)
            {
                KhachHangs.Add(item as Models.KhachHang);
            }
        }

        public void AddGift(int giatri,Models.NhanVien Emp)
        {
            foreach (var item in KhachHangs)
            {
                db.QuaTangs.InsertOnSubmit(new Models.QuaTang { DaNhan = false, GiaTri = giatri, KHID = item.KhachHangID, NhanVienID = Emp.NhanVienID });
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback((o) =>
            {
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
            }));
        }
    }
}