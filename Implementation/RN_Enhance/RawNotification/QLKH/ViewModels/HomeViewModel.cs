using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public class HomeViewModel : SuperViewModel
    {
        string NumberOfBirthDay = "0";
        System.Threading.Semaphore _Smp = new System.Threading.Semaphore(1, 1);
        public string Num
        {
            get { return NumberOfBirthDay; }
            private set
            {
                NumberOfBirthDay = value;
                OnPropertyChanged("Num");
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
                        Num = db.SinhNhats.Where(sn => sn.DaGoi == false).Count().ToString();
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