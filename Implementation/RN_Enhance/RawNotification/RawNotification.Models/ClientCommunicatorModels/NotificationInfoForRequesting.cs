using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ClientCommunicatorModels
{
    /// <summary>
    /// this class contain all infomations that send to client via Push Notification channel
    /// </summary>
    public class NotificationInfoForRequesting
    {
        public long NotificationId { get; set; }
        public string NotificationAccessKey { get; set; }
        public byte[] NotificationPreviewContent { get; set; }
        public long ReiceiverNotificationID { get; set; }
    }
}