using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class GiftViewViewModel : SuperViewModel
    {
        ObservableCollection<Models.QuaTang> _Result = new ObservableCollection<Models.QuaTang>();
        System.Threading.Semaphore _Smp = new System.Threading.Semaphore(1, 1);
        public ObservableCollection<Models.QuaTang> Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                OnPropertyChanged("Result");
            }
        }

        public GiftViewViewModel()
        {
            RefreshAsync();
        }

        public void RefreshAsync()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!_Smp.WaitOne(0)) return;
                try
                {
                    db = new Models.DBDataContext();
                    Result = new ObservableCollection<Models.QuaTang>(db.QuaTangs.Where(qt => qt.DaGui == false && qt.NhanVienID == App.CurentEmployee.NhanVienID));
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
            db.SubmitChanges();
        }
    }
}
