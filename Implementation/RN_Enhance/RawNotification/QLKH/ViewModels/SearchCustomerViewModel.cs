using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace QLKH.ViewModels
{
    public class SearchCustomerViewModel : SuperViewModel
    {
        #region Attributes
        System.Threading.Semaphore smp = new System.Threading.Semaphore(1, 1);
        ObservableCollection<Models.KhachHang> searchresult = new ObservableCollection<Models.KhachHang>();
        Visibility _SearchProgresBarVisibility = Visibility.Hidden;
        #endregion

        #region Properties
        public Visibility SearchProgresBarVisibility
        {
            get
            {
                return _SearchProgresBarVisibility;
            }
            set
            {
                _SearchProgresBarVisibility = value;
                OnPropertyChanged("SearchProgresBarVisibility");
            }
        }

        public bool IsEditable
        {
            get { return !App.CurrentPermissions.SuaTTKH; }
        }

        public bool CanAddGift
        {
            get { return App.CurrentPermissions.QLTTQuaTang; }
        }

        public bool IsEnable
        {
            get { return App.CurrentPermissions.SuaTTKH; }
        }
        public ObservableCollection<Models.KhachHang> SearchResult
        {
            get { return searchresult; }
            set
            {
                searchresult = value;
                searchresult.CollectionChanged += (o, e) =>
                  {
                      if (e.Action== System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                      {
                          foreach (var item in e.OldItems)
                          {
                              db.KhachHangs.DeleteOnSubmit(item as Models.KhachHang);
                          }
                      }
                  };
                OnPropertyChanged("SearchResult");
            }
        }
        public bool CanDeleteRows
        {
            get { return App.CurrentPermissions.XoaTTKH; }
        }
        int _CbbSelectedIndex = 2;
        public int CbbSelectedIndex
        {
            get
            {
                return _CbbSelectedIndex;
            }
            set
            {
                _CbbSelectedIndex = value;
                OnPropertyChanged("CbbSelectedIndex");
            }
        }
        #endregion

        #region Methods
        public void SearchCustomer(String Key)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!smp.WaitOne(0)) return;
                try
                {
                    SearchProgresBarVisibility = Visibility.Visible;
                    db = new Models.DBDataContext();
                    double temp = 0;
                    if (double.TryParse(Key, out temp))
                    { // key vào là số thì có thể họ đang tìm kiếm bằng ID hoặc số CMND
                        SearchResult = new ObservableCollection<Models.KhachHang>(db.KhachHangs.Where(kh => kh.KhachHangID == temp || kh.ConNguoi.CMND.Contains(Key) || kh.ConNguoi.Phone.Contains(Key) || kh.ConNguoi.Phone2.Contains(Key)));
                        SearchProgresBarVisibility = Visibility.Hidden;
                        return;
                    }
                    Key = Key.ToLower();
                    // nếu họ tìm bằng chuỗi thông thường thì có thể họ đang tìm kiếm bằng địa chỉ hoặc họ tên
                    SearchResult = new ObservableCollection<Models.KhachHang>(db.KhachHangs.Where(kh => kh.ConNguoi.DiaChi.ToLower().Contains(Key) || kh.ConNguoi.HoTen.ToLower().Contains(Key)));
                    SearchProgresBarVisibility = Visibility.Hidden;
                }
                catch(Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                finally
                {
                    smp.Release();
                }
            }));
        }
        public void SaveChange()
        {
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