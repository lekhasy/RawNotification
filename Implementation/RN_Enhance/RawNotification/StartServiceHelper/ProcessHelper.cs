using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartServiceHelper
{
    public static class ProcessHelper
    {
        public static bool IsProcessRunning(string ProcessName)
        {
            if (Process.GetProcessesByName(ProcessName).Count() > 0) return true;
            return false;
        }

        public static void StartProcess(string fileName)
        {
            string processName = Path.GetFileNameWithoutExtension(fileName);
            if (IsProcessRunning(processName))
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var item in processes)
                {
                    item.Kill();
                }
            }

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = fileName;
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;

            Process.Start(info);
        }
        
        internal static void CloseProcess(string fileName)
        {
            string processName = Path.GetFileNameWithoutExtension(fileName);
            var processes = Process.GetProcessesByName(processName);
            foreach (var item in processes)
            {
                item.Kill();
            }
        }
    }
}
