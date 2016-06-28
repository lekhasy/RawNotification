using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;
using System.Windows;

namespace QLKH.ViewModels
{
    public class SearchGiftViewModel : SuperViewModel
    {

        ObservableCollection<Models.QuaTang> _QuaTangs = new ObservableCollection<Models.QuaTang>();

        Semaphore _Smp = new Semaphore(1, 1);

        Visibility _SearchProgresBarVisibility = Visibility.Hidden;

        public Visibility SearchProgresBarVisibility
        {
            get { return _SearchProgresBarVisibility; }
            set
            {
                _SearchProgresBarVisibility = value;
                OnPropertyChanged("SearchProgresBarVisibility");
            }
        }

        public ObservableCollection<Models.QuaTang> QuaTangs
        {
            get { return _QuaTangs; }
            set
            {
                _QuaTangs = value;
                _QuaTangs.CollectionChanged += (s, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        foreach (var item in e.OldItems)
                        {
                            db.QuaTangs.DeleteOnSubmit(item as Models.QuaTang);
                        }
                    }
                };
                OnPropertyChanged("QuaTangs");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notsendyet"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void Search(bool notsendyet, DateTime from, DateTime to)
        {
            ThreadPool.QueueUserWorkItem(((o) =>
            {
                if (!_Smp.WaitOne(0)) return;
                try
                {
                    db = new Models.DBDataContext();
                    SearchProgresBarVisibility = Visibility.Visible;
                    var SearchResult = db.QuaTangs.Where(qt => notsendyet ? !qt.DaGui : true && qt.NgayLenKeHoach <= to && qt.NgayLenKeHoach >= from);
                    QuaTangs = new ObservableCollection<Models.QuaTang>(SearchResult);
                    SearchProgresBarVisibility = Visibility.Hidden;
                }
                catch(Exception ex)
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
            catch(Exception ex)
            {
                FireHandledExeptionAndLogErorrAsync(ErrorTemplates.GetDBInteractionError(ex)); 
            }
            
        }
    }
}
