using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System.Profile;

namespace RawNotification.DotNetCoreLibs
{
    public class DeviceInfo
    {
        public static string GetDeviceIMEI()
        {
            var deviceid = LibSetting.DeviceID;
            if (deviceid == null)
            {
                deviceid =  Guid.NewGuid().ToString();
                LibSetting.DeviceID = deviceid;
            }
            return deviceid;
            //return new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation().Id.ToString();
        }
    }
}
