using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ClientNetworkPacketSharedModels.FromClient
{
    [DataContract(Namespace ="CNSPM", Name = "FromClientPacket"), KnownType(typeof(FCLoginPacketData))]
    class FromClientPacket
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
        Login,
    }

}
