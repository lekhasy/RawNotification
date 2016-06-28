using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.DBModels
{
    public class ReceiverNotification : BaseModel
    {
        public long ReceiverNewId { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
    }
}