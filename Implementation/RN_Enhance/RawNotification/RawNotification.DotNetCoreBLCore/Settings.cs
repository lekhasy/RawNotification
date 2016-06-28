using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.DotNetCoreBLCore
{
    public class Settings
    {
        public static string Token
        {
            get
            {
                return GetValue<string>("RN_Token");
            }
            set
            {
                SetValue("RN_Token", value);
            }
        }

        public static long UserNewId
        {
            get
            {
                return GetValue<long>("RN_UserNewId");
            }
            set
            {
                SetValue("RN_UserNewId", value);
            }
        }

        public static bool IsFirstTimeRun
        {
            get
            {
                bool tryValue;
                // if this value exists means the application has been run before.
                return !TryGetValue<bool>("RN_IsFirstTimeRun", out tryValue);
            }
            set
            {
                SetValue("RN_IsFirstTimeRun", value);
            }
        }

        private static T GetValue<T>(string valueName)
        {
            return (T)Windows.Storage.ApplicationData.Current.LocalSettings.Values[valueName];
        }

        private static void SetValue(string valueName, object newValue)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[valueName] = newValue;
        }

        private static bool TryGetValue<T>(string valueName, out T value)
        {
            object tryvalue;
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.TryGetValue(valueName, out tryvalue))
            {
                value = (T)tryvalue;
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }
    }
}
