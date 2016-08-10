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
                IEnumerable<Models.ConNguoi> changedentity = db.GetChangeSet().Updates as IEnumerable<Models.ConNguoi>;
                IEnumerable<Models.ConNguoi> before = from c in db.ConNguois
                                                      where changedentity.Any(ce => ce.ConNguoiID == c.ConNguoiID)
                                                      select c;

                db.SubmitChanges();

                IEnumerable<Models.ConNguoi> after = from c in db.ConNguois
                                                     where changedentity.Any(ce => ce.ConNguoiID == c.ConNguoiID)
                                                     select c;

                Dictionary<string, string> senddata = new Dictionary<string, string>();

                for (int i = 0; i < changedentity.Count(); i++)
                {
                    StringBuilder builder = new StringBuilder();

                    Models.ConNguoi bf_enitty = before.ElementAt(i);
                    Models.ConNguoi af_entity = after.ElementAt(i);

                    #region Build string
                    if (bf_enitty.CMND != af_entity.CMND)
                        builder.AppendLine("CMND: " + af_entity.CMND);

                    if (bf_enitty.DiaChi != af_entity.DiaChi)
                        builder.AppendLine("Địa chỉ: " + af_entity.DiaChi);

                    if (bf_enitty.Email != af_entity.Email)
                        builder.AppendLine("Email: " + af_entity.Email);

                    if (bf_enitty.GioiTinh != af_entity.GioiTinh)
                        builder.AppendLine("Giới tính: " + (af_entity.GioiTinh.Value == true ? "Nam" : "Nữ"));

                    if (bf_enitty.HoTen != af_entity.HoTen)
                        builder.AppendLine("Họ tên: " + af_entity.HoTen);

                    if (bf_enitty.NgaySinh != af_entity.NgaySinh)
                        builder.AppendLine("Ngày sinh: " + af_entity.NgaySinh.Value.ToShortDateString());

                    if (bf_enitty.Phone != af_entity.Phone)
                        builder.AppendLine("Phone: " + af_entity.Phone);

                    if (bf_enitty.Phone2 != af_entity.Phone2)
                        builder.AppendLine("Phone 2: " + af_entity.Phone2);
                    #endregion

                    string sendcontent = builder.ToString().Trim();

                    senddata.Add(af_entity.KhachHangs.First().KhachHangID.ToString(), sendcontent);
                }


                using (var service = AppGlobal.getRNServerService())
                {
                    RawNotification.SharedLibs.JSONObjectSerializer<string> serializer = new RawNotification.SharedLibs.JSONObjectSerializer<string>();
                    foreach (var item in senddata)
                    {
                        var result = service.AddNotification
                            (
                            serializer.ObjectToBytes(item.Value),
                            serializer.ObjectToBytes("Hồ sơ của bạn đã được thay đổi"),
                            new string[] { item.Key }
                            );

                        if (result.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                        {
                            throw new Exception(result.Message);
                        }
                    }

                    var sendresult = service.SendAllNotification();

                    if (sendresult.StatusCode != RawNotification.Models.ResultStatusCodes.OK)
                    {
                        throw new Exception(sendresult.Message);
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
