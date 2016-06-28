using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.DotNetCoreUserCodeModel
{
    public class PreviewModel
    {
        public long NotificationId { get; set; }
        public string Content { get; set; }
        public string NotificationAccessKey { get; set; }
    }
}
