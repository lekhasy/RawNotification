using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models;
using RawNotification.Models.ServerBusinessModels;
using RawNotification.Models.DBModels;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface INotificationDA
    {
        void InsertNotification(Notification notifi);
        void RemoveAllReadNotification();
        IEnumerable<PendingNotificationInfo> GetAllPendingDeviceNotificationInfo();
        Notification GetNotificationById(long Id);
    }
}
