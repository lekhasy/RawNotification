using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading;

namespace QLKH.ViewModels
{
    class ExpandViewModel : SuperViewModel
    {

        #region Attributes

        private Models.KhachHang _Customer;

        private ObservableCollection<Models.NguoiThan> _NguoiThans = new ObservableCollection<Models.NguoiThan>();
        private object _CuocGois = new object();
        private string _WindowTitle = string.Empty;
        private dynamic _CbbLQHData = new object();
        private dynamic _CbbLoaiCuocGoiData = new object();
        private Semaphore smp1 = new Semaphore(1, 1);
        private Semaphore smp2 = new Semaphore(1, 1);
        object loaiquanhes;

        private Models.DBDataContext dbcg = new Models.DBDataContext();

        #endregion

        #region Properties

        public Models.KhachHang Customer
        {
            get
            {
                return _Customer;
            }
            set
            {
                _Customer = value;
                OnPropertyChanged("Customer");
            }
        }
        public String WindowTitle
        {
            get { return _WindowTitle; }
            set
            {
                _WindowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }
        public ObservableCollection<Models.NguoiThan> NguoiThans
        {
            get { return _NguoiThans; }
            set
            {
                _NguoiThans = value;
                OnPropertyChanged("NguoiThans");
            }
        }
        public object CuocGois
        {
            get { return _CuocGois; }
            set
            {
                _CuocGois = value;
                OnPropertyChanged("CuocGois");
            }
        }
        public dynamic CbbLQHData
        {
            get { return _CbbLQHData; }
            set
            {
                _CbbLQHData = value;
                OnPropertyChanged("CbbLQHData");
            }
        }
        public dynamic CbbLoaiCuocGoiData
        {
            get { return _CbbLoaiCuocGoiData; }
            set
            {
                _CbbLoaiCuocGoiData = value;
                OnPropertyChanged("CbbLoaiCuocGoiData");
            }
        }
        public object LoaiQuanHes
        {
            get { return loaiquanhes; }
            set
            {
                loaiquanhes = value;
                OnPropertyChanged("LoaiQuanHes");
            }
        }

        #endregion

        public ExpandViewModel(Models.KhachHang kh)
        {
            Customer = kh;
            try
            {
                LoaiQuanHes = dbcg.LoaiQuanHes;
                WindowTitle = Customer.ConNguoi.HoTen;
                CbbLQHData = dbcg.LoaiQuanHes;
                CbbLoaiCuocGoiData = dbcg.LoaiCuocGois;
            }
            catch (Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
            RefreshNTAsync();
            RefreshCGAsync();
        }

        #region Functions

        public void RefreshNTAsync()
        {
            // tạo một thread chạy ngầm cho đỡ phải đợi
            ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!smp1.WaitOne(0)) return;
                try
                {

                    db = new Models.DBDataContext();
                    NguoiThans = new ObservableCollection<Models.NguoiThan>(db.NguoiThans.Where(nt => nt.KhachHangID == Customer.KhachHangID));
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                finally
                {
                    smp1.Release();
                }
            }));
        }

        public void RefreshCGAsync()
        {
            // tạo một thread chạy ngầm cho đỡ phải đợi
            ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!smp2.WaitOne(0)) return;
                try
                {
                    dbcg = new Models.DBDataContext();
                    CuocGois = dbcg.CuocGois.Where(cg => cg.KhachHangID == Customer.KhachHangID);
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                finally
                {
                    smp2.Release();
                }
            }));
        }

        public void SaveNT()
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

        public void SaveCG()
        {
            try
            {
                dbcg.SubmitChanges();
            }catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
            }
        }

        public void AddNguoiThan(string name, string phone, int QuanHeID, DateTime? birthday)
        {
            Models.NguoiThan nt = new Models.NguoiThan { KhachHangID = Customer.KhachHangID, LoaiQuanHeID = QuanHeID, ConNguoi = new Models.ConNguoi { HoTen = name, Phone = phone, NgaySinh = birthday } };
            NguoiThans.Add(nt);
            db.NguoiThans.InsertOnSubmit(nt);
        }
        #endregion
    }
}