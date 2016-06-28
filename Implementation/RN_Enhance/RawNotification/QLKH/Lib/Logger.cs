using System;
using System.Text;

namespace QLKH.Lib
{
    public class Logger
    {
        System.Threading.Semaphore _Smp = new System.Threading.Semaphore(1, 1);
        System.IO.StreamWriter _Stream;
        StringBuilder _Builder = new StringBuilder();
        public string LogFoler { get; private set; }

        public string CurrentFile
        {
            get; private set;
        }

        /// <summary>
        /// Append string to log file
        /// </summary>
        /// <param name="Log">string that used to append</param>
        public void AppendLog(params string[] Logs)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                _Smp.WaitOne();
                _Builder.AppendLine("=====================================");
                _Builder.Append(DateTime.Now.TimeOfDay.ToString());
                _Builder.AppendLine("Type : String");
                foreach (var item in Logs)
                {
                    _Builder.AppendLine(item);
                }
                _Stream.WriteLine(_Builder.ToString());
                _Builder.Clear();
                _Smp.Release();
            }));
        }

        public void AppendLog(Exception ex)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(((o) =>
            {
                _Smp.WaitOne();
                _Builder.AppendLine("===================================");
                _Builder.AppendLine(DateTime.Now.TimeOfDay.ToString());
                _Builder.AppendLine(ex.Message);
                string[] stacktrace = ex.StackTrace.Split('\n');
                _Builder.AppendLine(stacktrace[stacktrace.Length - 1]);
                _Stream.WriteLine(_Builder.ToString());
                _Builder.Clear();
                _Smp.Release();
            }));
        }

        public Logger(System.IO.DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                directory.Create();
            }
            string LogFolderPath = directory.FullName;
            System.IO.FileInfo File;
            do
            {
                string FileName = LogFolderPath + DateTime.Now.ToString("MMM dd yyyy HH mm ss fffffff") + ".log";
                File = new System.IO.FileInfo(FileName);
            } while (File.Exists);
            CurrentFile = File.FullName;
            _Stream = File.CreateText();
            _Stream.AutoFlush = true;
        }
    }
}