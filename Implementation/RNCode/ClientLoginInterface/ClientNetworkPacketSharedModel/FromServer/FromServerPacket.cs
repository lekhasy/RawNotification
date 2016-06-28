using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ClientNetworkPacketSharedModels.FromServer
{
    [DataContract(Namespace = "CNPSM", Name = "FomServerPacket"), KnownType(typeof(FSLoginPacketData))]
    public class FromServerPacket
    {
        [DataMember]
        public FromServerPacketType Type { get; private set; }
        [DataMember]
        public object Data { get; private set; }

        public FromServerPacket(FromServerPacketType type, object data)
        {
            Type = type;
            Data = data;
        }
    }

    public enum FromServerPacketType
    {
        LoginResult,
        DataCorrupted,
    }
}
