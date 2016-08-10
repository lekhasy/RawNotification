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
                      if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
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
                catch (Exception ex)
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

                List<int> changedlistId = new List<int>();

                foreach (var item in db.GetChangeSet().Updates)
                    changedlistId.Add((item as Models.ConNguoi).ConNguoiID);

                Models.DBDataContext DBnew = new Models.DBDataContext();
                IEnumerable<Models.ConNguoi> beforeList = DBnew.ConNguois.Where(c => changedlistId.Contains(c.ConNguoiID));
                int length = beforeList.Count();

                db.SubmitChanges();

                IEnumerable<Models.ConNguoi> afterList = db.ConNguois.Where(c => changedlistId.Contains(c.ConNguoiID));
                Dictionary<string, string> sendList = new Dictionary<string, string>();

                for (int i = 0; i < length; i++)
                {
                    StringBuilder builder = new StringBuilder();

                    Models.ConNguoi entity_bf = beforeList.ElementAt(i);
                    Models.ConNguoi entity_after = afterList.ElementAt(i);

                    if (entity_bf.CMND != entity_after.CMND)
                        builder.AppendLine("CMND: " + entity_after.CMND);

                    if (entity_bf.DiaChi != entity_after.DiaChi)
                        builder.AppendLine("Địa chỉ: " + entity_after.DiaChi);

                    if (entity_bf.Email != entity_after.Email)
                        builder.AppendLine("Email: " + entity_after.Email);

                    if (entity_bf.GioiTinh.Value != entity_after.GioiTinh.Value)
                        builder.AppendLine("Giới tính: " + (entity_after.GioiTinh.Value == true ? "Nam" : "Nữ"));

                    if (entity_bf.HoTen != entity_after.HoTen)
                        builder.AppendLine("Họ tên: " + entity_after.HoTen);

                    if (entity_bf.NgaySinh.Value != entity_after.NgaySinh.Value)
                        builder.AppendLine("Ngày sinh: " + entity_after.NgaySinh.Value.ToShortDateString());

                    if (entity_bf.Phone != entity_after.Phone)
                        builder.AppendLine("Phone: " + entity_after.Phone);

                    if (entity_bf.Phone2 != entity_after.Phone2)
                        builder.AppendLine("Phone 2: " + entity_after.Phone2);

                    string sendData = builder.ToString().Trim();

                    sendList.Add(entity_bf.KhachHangs.First().KhachHangID.ToString(), sendData);
                }

                using (var service = AppGlobal.getRNServerService())
                {
                    RawNotification.SharedLibs.JSONObjectSerializer<string> serializer = new RawNotification.SharedLibs.JSONObjectSerializer<string>();

                    foreach (var item in sendList)
                    {
                        var result = service.AddNotification
                        (
                        serializer.ObjectToBytes(item.Value),
                        serializer.ObjectToBytes("Hồ sơ của bạn đã được cập nhật"),
                        new string[] { item.Key }
                        );

                        if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                        {
                            throw new Exception(result.Message);
                        }
                    }

                    var sendResult = service.SendAllNotification();

                    if (sendResult.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                    {
                        throw new Exception(sendResult.Message);
                    }
                    else
                    {
                        MessageBox.Show("Sending notification successfull", "Success");
                    }
                }

            }
            catch (Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }
        #endregion
    }
}