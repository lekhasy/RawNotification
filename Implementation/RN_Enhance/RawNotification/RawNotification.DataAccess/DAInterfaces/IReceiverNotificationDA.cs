using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RawNotification.Models.DBModels;

namespace RawNotification.DataAccess.DAInterfaces
{
    public interface IReceiverNotificationDA
    {
        void InsertReceiverNotification(ReceiverNotification entity);

        ReceiverNotification GetReceiverNotificationById(long ReceiverNotificationId);
    }
}
