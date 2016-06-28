using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QLKH.ViewModels
{
    public abstract class SuperViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<HandledError> HandledExceptionOccured = null;

        protected Models.DBDataContext db = new Models.DBDataContext();

        

        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected void FireHandledExeptionAndLogErorrAsync(HandledError e)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback((o) =>
            {
                if (App.Log != null)
                {
                    App.Log.AppendLog(e.ErrorException);
                }
                HandledExceptionOccured?.Invoke(this, e);
            }));
        }
    }
}