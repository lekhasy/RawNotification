using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.DBModels
{
    public class Notification : BaseModel
    {
        public byte[] NotificationContent { get; set; }
        public string NotificationAccessKey { get; set; }
        public byte[] NotificationPreviewContent { get; set; }
    }
}