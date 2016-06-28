using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DapperExtensions;
using RawNotification.Models.DBModels;

namespace RawNotification.DataAccess.DAImplements
{
    public class DeviceNotificationDA : BaseDA, DAInterfaces.IDeviceNotificationDA
    {
        public DeviceNotificationDA(IDbTransaction trans) : base(trans)
        {
        }

        public void DeleteDeviceNotification(long deviceNotificationId)
        {
            connection.Delete(new DeviceNotification { Id = deviceNotificationId}, transaction: transaction);
        }

        public void InsertDeviceNotification(long receiverNotificationId, long deviceId)
        {
            connection.Insert(new DeviceNotification { ReceiverNotificationId = receiverNotificationId, DeviceId = deviceId }, transaction: transaction);   
        }

    }
}
