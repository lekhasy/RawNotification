using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models;
using RawNotification.Models.DBModels;
using RawNotification.Models;

namespace RawNotification.BusinessLogic.BLInterfaces
{
    public interface IDeviceBL : IBaseBL
    {
        BaseServiceResult AddDevice(Device deviceInfo);
        BaseServiceResult RemoveDeviceByIMEI(string DeviceIMEI);
    }
}
