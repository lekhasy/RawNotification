using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.DotNetCoreLibs
{
    class LibSetting
    {   
        public static string DeviceID
        {
            get
            {
                return GetValue<string>("RN_DeviceToken");
            }
            set
            {
                SetValue("RN_DeviceToken", value);
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