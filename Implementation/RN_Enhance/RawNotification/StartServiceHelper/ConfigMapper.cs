using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartServiceHelper
{
    public static class ConfigMapper
    {
        public static string ServerFolderPath = ConfigurationManager.AppSettings["ServerFolderPath"];
        public static string ServerExeFileName = ConfigurationManager.AppSettings["ServerExeFileName"];
        public static string ServerConfigFileName = ConfigurationManager.AppSettings["ServerConfigFileName"];
        public static string ServerLogFolderPath = ConfigurationManager.AppSettings["ServerLogFolderPath"];

        public static string ClientFolderPath = ConfigurationManager.AppSettings["ClientFolderPath"];
        public static string ClientExeFileName = ConfigurationManager.AppSettings["ClientExeFileName"];
        public static string ClientConfigFileName = ConfigurationManager.AppSettings["ClientConfigFileName"];
        public static string ClientLogFolderPath = ConfigurationManager.AppSettings["ClientLogFolderPath"];

        public static string LoginFolderPath = ConfigurationManager.AppSettings["LoginFolderPath"];
        public static string LoginExeFileName = ConfigurationManager.AppSettings["LoginExeFileName"];
        public static string LoginConfigFileName = ConfigurationManager.AppSettings["LoginConfigFileName"];
        public static string LoginLogFolderPath = ConfigurationManager.AppSettings["LoginLogFolderPath"];
    }
}
