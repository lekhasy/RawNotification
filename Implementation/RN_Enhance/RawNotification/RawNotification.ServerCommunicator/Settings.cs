using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace RawNotification.ServerCommunicator
{
    public static class Settings
    {
        public static readonly Lazy<ILog> Logger = new Lazy<ILog>(() => {
            return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        });

        public static readonly Lazy<TimeSpan> NewTokenPeriod = new Lazy<TimeSpan>(() => {
            int TimeInHours = int.Parse(ConfigurationManager.AppSettings["TokenPeriodInHours"]);
            int TimeInMinutes = int.Parse(ConfigurationManager.AppSettings["TokenPeriodInMinutes"]);
            int TimeInSeconds = int.Parse(ConfigurationManager.AppSettings["TokenPeriodInSeconds"]);
            return new TimeSpan(TimeInHours, TimeInMinutes, TimeInSeconds);
        });

       

        #region Service Section
        public static readonly Lazy<int> PortNumber = new Lazy<int>(() => {
            return int.Parse(ConfigurationManager.AppSettings["PortNumber"]);
        });

        public static readonly Lazy<string> HostNameOrIP = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["HostNameOrIP"];
        });
        #endregion

        #region DB Section
        public static readonly Lazy<string> DBSerNameOrIP = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["DBSerNameOrIP"];
        });

        public static readonly Lazy<string> DBName = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["DBName"];
        });

        public static readonly Lazy<string> DBUserId = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["DBUserId"];
        });

        public static readonly Lazy<string> DBUserPassword = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["DBUserPassword"];
        });
        #endregion

        #region WNS Section
        public static readonly Lazy<string> WNSSecretKey = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["WNSSecretKey"];
        });

        public static readonly Lazy<string> WNSPackageSID = new Lazy<string>(() => {
            return ConfigurationManager.AppSettings["WNSPackageSID"];
        });

        public static string Token = string.Empty;

        public static readonly Func<string> GetWNSToken = new Func<string>(() => {
            return Token;
        });

        public static readonly Action<string> SetWNSToken = new Action<string>((value) => {
            Token = value;
        });
        #endregion
    }
}
