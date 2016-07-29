using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.DotNetCoreLibs
{
    public class DeviceInfo
    {
        public static string GetDeviceIMEI()
        {
            return new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation().Id.ToString();
        }
    }
}
