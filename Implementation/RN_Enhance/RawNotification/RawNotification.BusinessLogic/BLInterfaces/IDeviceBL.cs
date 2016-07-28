using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models;
using RawNotification.Models.DBModels;

namespace RawNotification.BusinessLogic.BLInterfaces
{
    public interface IDeviceBL : IBaseBL
    {
        BaseServiceResult<long, string> AddDevice(Device deviceInfo, TimeSpan NewDeviceTokenPeriod);
        BaseServiceResult RemoveDeviceByIMEI(string DeviceIMEI);
        bool CheckDeviceTokenValid(string DeviceToken, string DeviceIMEI);
    }
}
