using RawNotification.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface IDeviceDA
    {
        IEnumerable<Device> GetAllReceiverDevice(long receiverId);
        void DeleteDevice(long deviceId, long receiverId);
        void InsertDevice(Device deviceInfo);
        Device GetDeviceByIMEI(string IMEI);
        void UpdateDevice(Device Device);
    }
}
