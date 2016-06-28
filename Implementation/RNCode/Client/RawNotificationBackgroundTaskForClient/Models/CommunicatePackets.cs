using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RawNotificationBackgroundTaskForClient.Models
{
    [DataContract(Namespace = "CommunicatePacketNamespace", Name = "CommunicatePacket")]
    internal class CommunicatePacket
    {
        [DataMember]
        internal CommunicateType Type { get; private set; }

        [DataMember]
        internal object Data { get; private set; }
        internal CommunicatePacket(CommunicateType type, object data)
        {
            Type = type;
            Data = data;
        }
    }

    internal enum CommunicateType
    {
        Register,
        GetNotificationContent,
    }


    [DataContract(Namespace = "CommunicatePacketNamespace", Name = "GetNotificationContentCommunicateData")]
    internal class GetNotificationContentCommunicateData
    {
        [DataMember]
        internal long NotificationID { get; private set; }

        internal GetNotificationContentCommunicateData(long notificationID)
        {
            this.NotificationID = notificationID;
        }
    }
    [DataContract(Namespace = "CommunicatePacketNamespace", Name = "RegisterCommunicateData")]
    internal class RegisterCommunicateData
    {
        [DataMember]
        internal string DeviceID { get; private set; }
        [DataMember]
        internal string URI { get; private set; }
        [DataMember]
        internal int OSID { get; private set; }
        [DataMember]
        internal string ReceiverOldID { get; private set; }
        [DataMember]
        internal double Token { get; private set; }

        internal RegisterCommunicateData(string deviceID, string uri, int osid, string receiverOldID, double token)
        {
            DeviceID = deviceID;
            URI = uri;
            OSID = osid;
            ReceiverOldID = receiverOldID;
            Token = token;
        }
    }
}
