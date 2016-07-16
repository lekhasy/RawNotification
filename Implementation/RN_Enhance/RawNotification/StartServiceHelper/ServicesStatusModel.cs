using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartServiceHelper
{
    public class RNSStatusModel
    {
        public ServicesStatusModel ServerCommunicator { get; private set; }
        public ServicesStatusModel ClientCommunicator { get; private set; }
        public ServicesStatusModel LoginService { get; private set; }
        public ServicesStatusModel QLKHDataService { get; private set; }


        public RNSStatusModel()
        {
            ServerCommunicator = new ServicesStatusModel(
                ConfigMapper.ServerFolderPath,
                ConfigMapper.ServerExeFileName,
                ConfigMapper.ServerConfigFileName,
                ConfigMapper.ServerLogFolderPath
            );

            ClientCommunicator = new ServicesStatusModel(
                ConfigMapper.ClientFolderPath,
                ConfigMapper.ClientExeFileName,
                ConfigMapper.ClientConfigFileName,
                ConfigMapper.ClientLogFolderPath
            );

            LoginService = new ServicesStatusModel(
                ConfigMapper.LoginFolderPath,
                ConfigMapper.LoginExeFileName,
                ConfigMapper.LoginConfigFileName,
                ConfigMapper.LoginLogFolderPath
            );

            QLKHDataService = new ServicesStatusModel(
                ConfigMapper.QLKHFolderPath,
                ConfigMapper.QLKHExeFileName,
                ConfigMapper.QLKHConfigFileName,
                ConfigMapper.QLKHLogFolderPath
            );
        }

        internal void CloseAll()
        {
            ServerCommunicator.CloseService();
            ClientCommunicator.CloseService();
            LoginService.CloseService();
            QLKHDataService.CloseService();
        }

        internal void StartAll()
        {
            ServerCommunicator.StartService();
            ClientCommunicator.StartService();
            LoginService.StartService();
            QLKHDataService.StartService();
        }
    }

    public class ServicesStatusModel
    {

        public ServicesStatusModel(string folderpath, string exefile, string configfile, string logfolder)
        {
            this.FolderPath = folderpath;
            this.ExeFileName = exefile;
            this.ConfigFileName = configfile;
            this.LogFolderName = logfolder;
        }

        /// <summary>
        /// path of folder that contain exe file
        /// </summary>
        private string FolderPath { get; set; }

        /// <summary>
        /// the exe file name used for starting service
        /// </summary>
        private string ExeFileName { get; set; }

        /// <summary>
        /// FileNameOfConfigFile
        /// </summary>
        private string ConfigFileName { get; set; }

        private string LogFolderName { get; set; }

        public void StartService()
        {
            ProcessHelper.StartProcess(Directories.RN_BuildFolder + FolderPath + ExeFileName);
        }

        public void CloseService()
        {
            ProcessHelper.CloseProcess(Directories.RN_BuildFolder + FolderPath + ExeFileName);
        }

        public void ViewLog()
        {
            try
            {
                DirectoryInfo LogFolder = new DirectoryInfo(Directories.RN_BuildFolder + FolderPath + LogFolderName);
                var file = LogFolder.GetFiles("*.log", SearchOption.TopDirectoryOnly).OrderBy(f => f.LastWriteTime).LastOrDefault();
                if (file != null)
                {
                    Process.Start(file.FullName);
                }
                else
                {
                    MessageBox.Show("There are no logs to view");
                }
            }
            catch
            {
                MessageBox.Show("There are no logs to view");
            }
        }

        public void OpenConfig()
        {
            Process.Start(Directories.RN_BuildFolder + FolderPath + ConfigFileName);
        }

        public ServiceStatus GetStatus()
        {
            string processName = Path.GetFileNameWithoutExtension(ExeFileName);
            return ProcessHelper.IsProcessRunning(processName) ? ServiceStatus.Running : ServiceStatus.Stopped;
        }
    }

    public enum ServiceStatus
    {
        Running = 0,
        Stopped = 1
    }

    public static class ExtensionMethods
    {
        public static Color GetColor(this ServiceStatus status)
        {
            switch (status)
            {
                case ServiceStatus.Running: return Color.Green;
                case ServiceStatus.Stopped: return Color.Red;
            }
            throw new NotImplementedException();
        }
    }
}
