using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.MobileClient.SharedModels.NetworkPackets.FromClient
{
    [DataContract(Namespace ="CC.FC", Name = "GetNotificationContentPacketData")]
    public sealed class GetNotificationContentPacketData
    {
        [DataMember]
        public long NotificationID { get; private set; }

        public GetNotificationContentPacketData(long notificationID)
        {
            this.NotificationID = notificationID;
        }
    }
}
