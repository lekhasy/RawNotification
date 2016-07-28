using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.DBModels
{
    public class LoginToken : BaseModel
    {
        public long ReceiverId { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpiredTime { get; set; }
    }
}
