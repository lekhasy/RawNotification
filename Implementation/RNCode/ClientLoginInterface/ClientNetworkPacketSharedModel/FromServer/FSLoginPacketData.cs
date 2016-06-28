using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ClientNetworkPacketSharedModels.FromServer
{
    [DataContract(Namespace = "CNPSM", Name = "FSLoginPacketData")]
    public class FSLoginPacketData
    {
        [DataMember]
        public LoginResult Result { get; private set; }

        [DataMember]
        public string OldID { get; private set; }

        public FSLoginPacketData(LoginResult result, string oldid)
        {
            Result = result;
            OldID = oldid;
        }
    }

    public enum LoginResult
    {
        Success,
        NotFound,
        Error,
    }

}
