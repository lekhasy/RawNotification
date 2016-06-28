using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class CustomerAddViewModel : SuperViewModel
    {
        #region Attributes
        Models.KhachHang KhachHang = new Models.KhachHang();
        public ObservableCollection<Models.NguoiThan> nguoithan = new ObservableCollection<Models.NguoiThan>();
        object loaiquanhes;
        #endregion

        #region Properties
        public ObservableCollection<Models.NguoiThan> NguoiThans
        {
            get { return nguoithan; }
            set
            {
                nguoithan = value;
                nguoithan.CollectionChanged += Nguoithan_CollectionChanged;
                OnPropertyChanged("NguoiThans");
            }
        }
        public object LoaiQuanHes
        {
            get { return loaiquanhes; }
            set { loaiquanhes = value;
                OnPropertyChanged("LoaiQuanHes");
            }
        }

        #endregion

        public CustomerAddViewModel()
        {
            try
            {
                LoaiQuanHes = db.LoaiQuanHes;
            }
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
            
        }

        private void Nguoithan_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems!=null)
            {
                foreach (Models.NguoiThan item in e.NewItems)
                {
                    KhachHang.NguoiThans.Add(item);
                }
            }
            
            if (e.OldItems!=null)
            {
                foreach (Models.NguoiThan item in e.OldItems)
                {
                    KhachHang.NguoiThans.Remove(item);
                }
            }
        }
        public void AddNguoiThan(string name, string phone, int QuanHeID, DateTime? birthday)
        {
            Models.NguoiThan nt = new Models.NguoiThan { LoaiQuanHeID = QuanHeID, ConNguoi = new Models.ConNguoi { HoTen = name, Phone = phone, NgaySinh = birthday } };
            NguoiThans.Add(nt);
        }
        

        public void AddCustomer(string name, string address, bool? gioitinh, DateTime? ngaysinh,
            string phone1, string phone2, string CMND, string email)
        {
            KhachHang.ConNguoi = new Models.ConNguoi { HoTen = name, DiaChi = address, GioiTinh = gioitinh,
                NgaySinh = ngaysinh, Phone = phone1, Phone2 = phone2, CMND = CMND, Email = email };
            db.KhachHangs.InsertOnSubmit(KhachHang);
            try
            {
                db.SubmitChanges();
                KhachHang = new Models.KhachHang();
                NguoiThans = new ObservableCollection<Models.NguoiThan>();
            }
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }
    }
}