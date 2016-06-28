using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace QLKH.ViewModels
{
    public class BackupViewModel : INotifyPropertyChanged
    {
        Models.DBDataContext db = new Models.DBDataContext();

        private ObservableCollection<string> _Filelist = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> FileList
        {
            get { return _Filelist; }
            set { _Filelist = value;
                if (PropertyChanged!=null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FileList"));
                }
            }
        }

        public void RefreshList()
        {
            FileList.Clear();
            DirectoryInfo dir = new DirectoryInfo(Directorys.BackupDirectory);
            if (!dir.Exists)
            {
                // thư mục không tồn tại
                System.Security.AccessControl.DirectorySecurity s = new System.Security.AccessControl.DirectorySecurity();
                s.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule("Authenticated Users", System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow));
                try
                {
                    dir.Create(s);
                    // Tạo folder xong
                }
                catch
                {
                    // Không thể tạo thư mục do vấn đề bảo mật của hệ thống
                }
            }
            FileInfo[] files = dir.GetFiles("*.bak", SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                FileList.Add(item.Name);
            }
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("FileList"));
            }
        }
        public bool DeleteBackUp(string filename)
        {
            FileInfo file = new FileInfo(Directorys.BackupDirectory + filename);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                    RefreshList();
                    return true;
                }
                catch
                {
                    return false; // file bị xóa ngay khi vừa kiểm tra exit
                }
            }
            else
            {
                return false;
            }
        }

        public void BackUp()
        {
            if (!Directorys.IsFileExited())
            {
                db.BackUpDB(Directorys.GetBackupPath());
                RefreshList();
            }
        }
    }
}
