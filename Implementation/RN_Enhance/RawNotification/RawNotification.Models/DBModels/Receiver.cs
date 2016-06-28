using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.DBModels
{
    public class Receiver : BaseModel
    {
        public string OldId { get; set; }
        public int DeviceCount { get; set; }
        public string AccessToken { get; set; }
        public DateTime? TokenExpiredTime { get; set; }
    }
}