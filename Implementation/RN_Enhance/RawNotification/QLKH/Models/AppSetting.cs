using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace QLKH.Models
{
    public class AppSetting
    {
        private string _FilePath;
        
        public bool _AutoBackupEnable;
        public CustomObservableCollection<BackupSchedule> _BackupSchedules = new CustomObservableCollection<BackupSchedule>(new List<BackupSchedule>());
        public bool AutoBackupEnable
        {
            get { return _AutoBackupEnable; }
            set { _AutoBackupEnable = value;
                if (value)  // Khi chức năng được bật trở lại
                {
                    foreach (var item in BackupSchedules)
                    {
                        BackupSchedule newschedule = new BackupSchedule(item);
                        RemoveBackupSchedule(item);
                        Addschedule(newschedule);
                    }
                }
                else // nếu chức năng bị tắt thì ta terminate tất cả các work
                {
                    foreach (var item in BackupSchedules)
                    {
                        foreach (var workid in item.Works)
                        {
                            WorkExcuter.WorksManager.GetWorkByID(workid).Terminate();
                        }
                    }
                }
                Save();
            }
        }

        public CustomObservableCollection<BackupSchedule> BackupSchedules
        {
            get { return _BackupSchedules; }
            set
            {
                _BackupSchedules = value;
                Save();
            }
        }

        public AppSetting(string AppConfigFilePath = "Settings/AppSetting.Setting")
        {
            BackupSchedule.AScheduleDone += BackupSchedule_AScheduleDone;
            Load(AppConfigFilePath);
        }

        private void BackupSchedule_AScheduleDone(object sender, EventArgs e)
        {
            // chạy xong rồi thì xóa nó khỏi list
            RemoveBackupSchedule(sender as BackupSchedule);
        }

        public void Addschedule(BackupSchedule Schedule)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                BackupSchedules.Add(Schedule);
                Save();
                
            });
        }

        public void Load(string AppConfigFilePath)
        {
            _FilePath = AppConfigFilePath;
            FileInfo file = new FileInfo(AppConfigFilePath);
            if (file.Exists)
            {
                AppSettingSaveData setting = WorkExcuter.ObjectSerization.Deserization(AppConfigFilePath) as AppSettingSaveData;
                AutoBackupEnable = setting._AutoBackupEnable;
                BackupSchedules = new CustomObservableCollection<BackupSchedule>(setting._BackupSchedules);
                
            }
            else
            {
                Save();
                Load(AppConfigFilePath);
            }
        }
        /// <summary>
        /// Hàm này sẽ kiểm tra và sửa các lỗi có thể có khi lưu file xuống trong phiên làm việc trước
        /// Hàm này chỉ được gọi khi Package WorkExcuter đã Initialize
        /// </summary>
        /// <returns></returns>
        public void CheckAndFixError()
        {
            foreach (var item in BackupSchedules)
            {
                if (item.IsError()) // nếu bị lỗi thì terminate xóa nó đi
                {
                    item.Terminate();
                    BackupSchedule newschedule = new BackupSchedule(item);
                    Addschedule(newschedule);
                }
            }            
        }
        public void Save()
        {
            WorkExcuter.ObjectSerization.Serization(new AppSettingSaveData { _AutoBackupEnable = _AutoBackupEnable, _BackupSchedules = _BackupSchedules.ToList()}, _FilePath);
        }

        private void RemoveBackupSchedule(BackupSchedule schedule)
        {
            /// Code remove này đang thay đổi một ObservalueCollection
            /// nhưng loại collection này chỉ được thay đổi ở thread mà nó được tạo
            /// Vậy ta phải dùng dispatcher để gọi code này
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                _BackupSchedules.Remove(schedule);
                Save();
            });
        }

        [Serializable]
        private class AppSettingSaveData
        {
            public bool _AutoBackupEnable;
            public List<BackupSchedule> _BackupSchedules;
        }

    }
}
