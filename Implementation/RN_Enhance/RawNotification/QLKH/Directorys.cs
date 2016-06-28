using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace QLKH
{
    public class Directorys
    {
        public static string BackupDirectory
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "Backup\\"; }
        }
        public static string GetBackupPath()
        {
            return BackupDirectory + DateTime.Now.ToString("yyyy MMM dd in h-m-s") + ".bak"; 
        }

        public static bool IsFileExited()
        {
            DirectoryInfo info = new DirectoryInfo(BackupDirectory);
            return info.GetFiles().Any(tt => tt.Name == DateTime.Now.ToString("MMM dd yyyy in h-m-s") + ".bak");
        }
    }
}
