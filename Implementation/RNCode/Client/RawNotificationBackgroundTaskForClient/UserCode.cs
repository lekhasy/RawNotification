using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RawNotificationBackgroundTaskForClient
{
    /// <summary>
    /// 
    /// </summary>
    internal static class UserCode
    {
        internal static void Run(SendObject notifyObj)
        {
            Notification_Helper_Client.Toast_Notification.Show(notifyObj.type, notifyObj.data);
        }
    }

    //[DataContract(Namespace = "CommunicatePacketNamespace", Name = "SendObject")]
    internal class SendObject
    {
      //  [DataMember]
        public string type = "Đây là type";
        //[DataMember]
        public string data = "Đây là data";
    }
}
