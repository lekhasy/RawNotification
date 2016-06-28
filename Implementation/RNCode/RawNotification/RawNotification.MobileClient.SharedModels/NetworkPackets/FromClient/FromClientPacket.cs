using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.MobileClient.SharedModels.NetworkPackets.FromClient
{

    [DataContract(Namespace ="CC.FC", Name = "FromClientPacket"), KnownType(typeof(GetNotificationContentPacketData)), KnownType(typeof(RegisterPacketData))]
    public sealed class FromClientPacket
    {
        [DataMember]
        public FromClientPacketType Type { get; private set; }

        [DataMember]
        public object Data { get; private set; }
        public FromClientPacket(FromClientPacketType type, object data)
        {
            Type = type;
            Data = data;
        }
    }

    public enum FromClientPacketType
    {
        Register,
        GetNotificationContent,
    }
}
