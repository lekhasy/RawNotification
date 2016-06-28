using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.MobileClient.LocalSettings
{
    public class LocalSettingsManager
    {
        public static string RNSClientPort
        {
            get
            {
                return GetValue("RNSClientPort");
            }
            set
            {
                SetValue("RNSClientPort", value);
            }
        }

        public static string UserName
        {
            get
            {
                return GetValue("UserName");
            }
            set
            {
                SetValue("UserName", value);
            }
        }

        public static string ServerIP
        {
            get
            {
                return GetValue("ServerIP");
            }
            set
            {
                SetValue("ServerIP", value);
            }
        }

        private static string GetValue(string valueName)
        {
                return Windows.Storage.ApplicationData.Current.LocalSettings.Values[valueName].ToString();
        }

        private static void SetValue(string valueName, string newValue)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[valueName] = newValue;
        }

    }
}
