using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.ClientCommunicator
{
    public class Settings
    {
        public static readonly Lazy<TimeSpan> NewDeviceTokenPeriod = new Lazy<TimeSpan>(() => {
            int TimeInHours = int.Parse(ConfigurationManager.AppSettings["DeviceTokenPeriodInHours"]);
            int TimeInMinutes = int.Parse(ConfigurationManager.AppSettings["DeviceTokenPeriodInMinutes"]);
            int TimeInSeconds = int.Parse(ConfigurationManager.AppSettings["DeviceTokenPeriodInSeconds"]);
            return new TimeSpan(TimeInHours, TimeInMinutes, TimeInSeconds);
        });
    }
}
