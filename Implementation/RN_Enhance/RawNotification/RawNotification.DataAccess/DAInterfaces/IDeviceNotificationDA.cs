using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface IDeviceNotificationDA
    {
        void DeleteDeviceNotification(long deviceNotificatioId);
        void InsertDeviceNotification(long receiverNotificationId, long deviceId);
    }
}