using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Notification_Helper_Client
{
    public class Device
    {
        public static string ID { get { return new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation().Id.ToString(); } }
    }
}
