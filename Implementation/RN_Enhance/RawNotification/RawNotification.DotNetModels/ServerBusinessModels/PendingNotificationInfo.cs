using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ServerBusinessModels
{
    public class PendingNotificationInfo
    {
        public long ReceiverId { get; set; }
        public long DeviceId { get; set; }
        public long ReceiverNotificationId { get; set; }
        public string DeviceIMEI { get; set; }
        public int DeviceOSId { get; set; }
        public string DeviceURI { get; set; }
        public long NotificationId { get; set; }
        public long DeviceNotificationId { get; set; }
        public string NotificationAccessKey { get; set; }
        public byte[] NotificationPreviewContent { get; set; }
    }
}
