using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.ServerClient.SharedModels.NetworkPackets.FromClient
{
    [DataContract(Namespace = "SC.FC", Name = "FromClientPacket"), KnownType(typeof(AddNotificationPacketData))]
    public class FromClientPacket
    {
        [DataMember]
        public FromClientPacketType PacketType { get; private set; }
        [DataMember]
        public object Data { get; private set; }

        public FromClientPacket(FromClientPacketType type, object data)
        {
            this.PacketType = type;
            this.Data = data;
        }
    }

    public enum FromClientPacketType
    {
        AddNotification,
        RequestServerInfo,
        SendAllNotification,
    }

}
