using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_UWP
{
    public class Settings
    {
        public static bool LoginSuccessed
        {
            get
            {
                try
                {
                    return GetValue<bool>("LoginSuccessed");
                } catch
                {
                    return false;
                }
            }
            set
            {
                SetValue("LoginSuccessed", value);
            }
        }
        public static string UserName
        {
            get
            {
                return GetValue<string>("UserName");
            }
            set
            {
                SetValue("UserName", value);
            }
        }

        public static string Pasword
        {
            get
            {
                return GetValue<string>("Password");
            }
            set
            {
                SetValue("Password", value);
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
    }
}
