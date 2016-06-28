using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace QLKH.ViewModels
{
    public class AdminSettingsViewModel
    {
        
        public Models.CustomObservableCollection<Models.BackupSchedule> BackupList
        {
            get
            {
                return App.Setting.BackupSchedules;
            }
        }

        
        public bool AutoBackupEnable
        {
            get { return App.Setting.AutoBackupEnable; }
            set {
                App.Setting.AutoBackupEnable = value;
                App.Setting.Save();
            }
        }

        public void AddBackupSchedule(DateTime time, bool repeat, bool cn, bool t2, bool t3, bool t4, bool t5, bool t6, bool t7)
        {
            App.Setting.Addschedule(new Models.BackupSchedule(time, repeat, cn, t2, t3, t4, t5, t6, t7));
        }

        public void RemoveBackupSchedule(Models.BackupSchedule schedule)
        {
            schedule.Terminate();
        }
    }
}