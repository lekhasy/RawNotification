using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.MobileClient.SharedModels.NetworkPackets.FromClient
{
    [DataContract(Namespace ="CC.FC", Name = "RegisterPacketData")]
    public sealed class RegisterPacketData
    {
        [DataMember]
        public string DeviceID { get; private set; }
        [DataMember]
        public string URI { get; private set; }
        [DataMember]
        public OperatingSystemIDTemplates OSID { get; private set; }
        [DataMember]
        public string ReceiverOldID { get; private set; }

        public RegisterPacketData(string deviceID, string uri, OperatingSystemIDTemplates osid, string receiverOldID)
        {
            DeviceID = deviceID;
            URI = uri;
            OSID = osid;
            ReceiverOldID = receiverOldID;
        }
    }
}
