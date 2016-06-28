using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.ServerClient.SharedModels.NetworkPackets.FromClient
{
    [DataContract(Namespace ="SC.FC", Name = "FromClientPacket")]
    public class AddNotificationPacketData
    {
        
        [DataMember]
        public byte[] NotificationContent { get; private set; }
        [DataMember]
        public List<string> ReceiversOldID { get; private set; }

        public AddNotificationPacketData(byte[] notificationContent, List<string> receiversOldID)
        {
            this.NotificationContent = notificationContent;
            this.ReceiversOldID = receiversOldID;
        }
    }
}