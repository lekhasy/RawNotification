using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace QLKH.ViewModels
{
    public class SearchEmployeeViewModel : SuperViewModel
    {
        #region Attributes

        Semaphore _Smp = new Semaphore(1, 1);
        Visibility _SearchProgresBarVisibility = Visibility.Hidden;
        ObservableCollection<Models.NhanVien> searchresult = new ObservableCollection<Models.NhanVien>();
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
        public ObservableCollection<Models.NhanVien> SearchResult
        {
            get { return searchresult; }
            set
            {
                searchresult = value;
                searchresult.CollectionChanged += (o, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        foreach (var item in e.OldItems)
                        {
                            db.NhanViens.DeleteOnSubmit(item as Models.NhanVien);
                        }
                    }
                };
                OnPropertyChanged("SearchResult");
            }
        }
        #endregion

        #region Methods

        public void Search(String Key)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!_Smp.WaitOne()) return;
                try
                {
                    SearchProgresBarVisibility = Visibility.Visible;
                    db = new Models.DBDataContext();
                    double temp = 0;
                    if (double.TryParse(Key, out temp))
                    { // key vào là số thì có thể họ đang tìm kiếm bằng ID hoặc số CMND
                        SearchResult = new ObservableCollection<Models.NhanVien>(db.NhanViens.Where(kh => kh.NhanVienID == temp || kh.ConNguoi.CMND.Contains(Key) || kh.ConNguoi.Phone.Contains(Key) || kh.ConNguoi.Phone2.Contains(Key)));
                        SearchProgresBarVisibility = Visibility.Hidden;
                        return;
                    }
                    Key = Key.ToLower();
                    // nếu họ tìm bằng chuỗi thông thường thì có thể họ đang tìm kiếm bằng địa chỉ hoặc họ tên
                    SearchResult = new ObservableCollection<Models.NhanVien>(db.NhanViens.Where(kh => kh.ConNguoi.DiaChi.ToLower().Contains(Key) || kh.ConNguoi.HoTen.ToLower().Contains(Key)));
                    SearchProgresBarVisibility = Visibility.Hidden;
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                finally
                {
                    _Smp.Release();
                }
            }));
        }

        public void Save()
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }
        #endregion
    }
}
