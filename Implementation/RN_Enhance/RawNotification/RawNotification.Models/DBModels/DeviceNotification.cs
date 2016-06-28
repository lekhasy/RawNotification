using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.DBModels
{
    public class DeviceNotification : BaseModel
    {
        public long ReceiverNotificationId { get; set; }
        public long DeviceId { get; set; }
    }
}