using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class ExpandEmployeeViewModel : SuperViewModel
    {

        #region Attributes

        Models.NhanVien _Employee;
        ObservableCollection<Models.DanhSachChucVu> _DSChucVu = new ObservableCollection<Models.DanhSachChucVu>();
        ObservableCollection<Models.ChucVu> _ChucVu = new ObservableCollection<Models.ChucVu>();

        #endregion

        #region Properties

        public ObservableCollection<Models.DanhSachChucVu> DSChucVu
        {
            get { return _DSChucVu; }
            set
            {
                _DSChucVu = value;
                _DSChucVu.CollectionChanged += (s, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        foreach (var item in e.OldItems)
                        {
                            db.DanhSachChucVus.DeleteOnSubmit(item as Models.DanhSachChucVu);
                        }
                    }
                    else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    {
                        foreach (var item in e.NewItems)
                        {
                            db.DanhSachChucVus.InsertOnSubmit(item as Models.DanhSachChucVu);
                        }
                    }
                };
                OnPropertyChanged("DSChucVu");
            }
        }

        public ObservableCollection<Models.ChucVu> ChucVu
        {
            get { return _ChucVu; }
            set
            {
                _ChucVu = value;
                OnPropertyChanged("ChucVu");
            }
        }

        #endregion

        public ExpandEmployeeViewModel(Models.NhanVien nv)
        {
            _Employee = nv;
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                try
                {
                    _Employee = db.NhanViens.Single(em => em.NhanVienID == nv.NhanVienID);
                    ChucVu = new ObservableCollection<Models.ChucVu>(db.ChucVus);
                }
                catch (Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
                RefreshAsync();
            }));
        }


        #region Methods

        public void AddChucVu(Models.ChucVu cv)
        {
            foreach (var item in DSChucVu)
            {
                if (item.ChucVuID == cv.ChucVuID)
                {
                    return;
                }
            }
            Models.DanhSachChucVu newcv = new Models.DanhSachChucVu { ChucVu = db.ChucVus.Single(c => c.ChucVuID == cv.ChucVuID), NhanVienID = _Employee.NhanVienID };
            DSChucVu.Add(newcv);
        }

        public void Save()
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

        public void RefreshAsync()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                try
                {
                    db = new Models.DBDataContext();
                    DSChucVu = new ObservableCollection<Models.DanhSachChucVu>(db.DanhSachChucVus.Where(cv => cv.NhanVienID == _Employee.NhanVienID));
                }
                catch(Exception ex)
                {
                    FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex));
                }
            }));
        }

        #endregion
    }
}