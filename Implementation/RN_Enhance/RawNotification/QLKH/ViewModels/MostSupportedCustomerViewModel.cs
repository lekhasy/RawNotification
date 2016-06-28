using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKH.ViewModels
{
    public class MostSupportedCustomerViewModel : SuperViewModel
    {
        ObservableCollection<object> searchresult = new ObservableCollection<object>();
        System.Threading.Semaphore _Smp = new System.Threading.Semaphore(1, 1);

        public ObservableCollection<object> SearchResult
        {
            get { return searchresult; }
            set
            {
                searchresult = value;
                OnPropertyChanged("SearchResult");
            }
        }


        public MostSupportedCustomerViewModel()
        {
            Update();
        }

        public void Update()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!_Smp.WaitOne(0)) return;
                try
                {
                    DateTime motnamtruoc = DateTime.Today.AddDays(-365);
                    db = new Models.DBDataContext();

                    var se = (from kh in db.KhachHangs
                              join cg in db.CuocGois on kh.KhachHangID equals cg.KhachHangID
                              join cn in db.ConNguois on kh.ConNguoiID equals cn.ConNguoiID
                              where cg.ThoiDiemGoi > motnamtruoc
                              group kh by new
                              {
                                  ID = kh.KhachHangID,
                                  cn.HoTen,
                                  cn.GioiTinh,
                                  cn.CMND,
                                  cn.Phone,
                                  cn.Phone2,
                                  cn.DiaChi,
                                  TongGT = kh.CuocGois.Count
                              } into st
                              orderby st.Key.TongGT descending
                              select new
                              {
                                  st.Key.DiaChi,
                                  st.Key.GioiTinh,
                                  st.Key.HoTen,
                                  st.Key.CMND,
                                  st.Key.ID,
                                  st.Key.Phone,
                                  st.Key.Phone2,
                                  st.Key.TongGT
                              }).Take(10).ToList();

                    SearchResult = new ObservableCollection<object>(se);
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                _Smp.Release();
            }));
        }
    }
}
