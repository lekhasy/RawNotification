using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace QLKH.ViewModels
{
    public class MostGiftTakerCustomerViewModel : SuperViewModel
    {
        ObservableCollection<object> searchresult = new ObservableCollection<object>();

        public ObservableCollection<object> SearchResult
        {
            get { return searchresult; }
            set
            {
                searchresult = value;
                OnPropertyChanged("SearchResult");
            }
        }


        public MostGiftTakerCustomerViewModel()
        {
            Update();
        }

        public void Update()
        {
            DateTime motnamtruoc = DateTime.Today.AddDays(-365);
            db = new Models.DBDataContext();

            var se = (from kh in db.KhachHangs
                      join qt in db.QuaTangs on kh.KhachHangID equals qt.KHID
                      join cn in db.ConNguois on kh.ConNguoiID equals cn.ConNguoiID
                      where qt.NgayGui.Value > motnamtruoc
                      group kh by new
                      {
                          ID = kh.KhachHangID,
                          HoTen = cn.HoTen,
                          GioiTinh = cn.GioiTinh,
                          cn.Phone,
                          cn.CMND,
                          cn.Phone2,
                          cn.DiaChi,
                          TongGT = kh.QuaTangs.Sum(tt => tt.GiaTri)
                     } into st
                     orderby st.Key.TongGT descending
                     select new { st.Key.DiaChi,
                         st.Key.GioiTinh,
                         st.Key.HoTen,
                         st.Key.ID,
                         st.Key.CMND,
                         st.Key.Phone,
                         st.Key.Phone2,
                         st.Key.TongGT}).Take(10).ToList();

            SearchResult = new ObservableCollection<object>(se);
        }
    }
}
