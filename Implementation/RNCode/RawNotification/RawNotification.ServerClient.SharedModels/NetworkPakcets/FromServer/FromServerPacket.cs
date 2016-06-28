using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer
{
    [DataContract(Namespace ="SC.FS", Name = "FromServerPacket"), KnownType(typeof(AddNotificationFSPacketData)), KnownType(typeof(ServerInfo))]
    public sealed class FromServerPacket
    {
        [DataMember]
        public FromServerPacketType PacketType { get; private set; }
        [DataMember]
        public object Data { get; private set; }

        public FromServerPacket(FromServerPacketType type, object data)
        {
            this.PacketType = type;
            this.Data = data;
        }
    }

    public enum FromServerPacketType
    {
        ServerInfo,
        AddNotificationResponse,
        AddLoginToken,
        DataCorrupted,
    }
}
