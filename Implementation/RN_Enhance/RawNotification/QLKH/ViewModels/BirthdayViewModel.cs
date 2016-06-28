using System;
using System.Linq;

namespace QLKH.ViewModels
{
    public class BirthdayViewModel : SuperViewModel
    {
        object khachhangs = new object();
        protected System.Threading.Semaphore _Smp = new System.Threading.Semaphore(1, 1);
        public object KhachHangs
        {
            get { return khachhangs; }
            set { khachhangs = value;
                OnPropertyChanged("KhachHangs");
            }
        }

        public BirthdayViewModel()
        {
            Refresh();
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

        public void Refresh()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!_Smp.WaitOne(0)) return;
                try
                {
                    Models.SinhNhat.renewBirthDay();
                    db = new Models.DBDataContext();
                    KhachHangs = from sn in db.SinhNhats
                                 select new
                                 {
                                     sn.KhachHang.ConNguoi.HoTen,
                                     sn.KhachHang.ConNguoi.Phone,
                                     sn.KhachHang.ConNguoi.Phone2,
                                     sn
                                 };
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
    }
}