using System.Collections.Generic;
using RawNotification.Models;

namespace RawNotification.BusinessLogic.BLInterfaces
{
    public interface INotificationBL : IBaseBL
    {
        BaseServiceResult AddNotification(byte[] addedNotification, byte[] NotificationPreviewContent, IEnumerable<string> oldReceiverIDList);
        BaseServiceResult<byte[]> GetNotificationContent(long notificationId, string NotificationAccessKey);
        BaseServiceResult<bool> CheckIfNotificationRead(long receiverNotificationID);
    }
}
