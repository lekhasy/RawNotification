using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace ClientNetworkPacketSharedModels.FromClient
{
    [DataContract(Namespace = "CNSPM", Name = "FCLoginPacketData")]
    public class FCLoginPacketData
    {
        [DataMember]
        public string ID { get; private set; }
        [DataMember]
        public string Password { get; private set; }

        public FCLoginPacketData(string id, string password)
        {
            ID = id;
            Password = password;
        }
    }
}
