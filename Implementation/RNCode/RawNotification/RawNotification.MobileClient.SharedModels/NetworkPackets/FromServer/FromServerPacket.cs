using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RawNotification.MobileClient.SharedModels.NetworkPackets.FromServer
{
    [DataContract(Namespace ="CC.FS", Name = "FromServerPacket")]
    public sealed class FromServerPacket
    {
        [DataMember]
        public ServerResult ResultType { get; private set; }
        [DataMember]
        public byte[] Data { get; private set; }

        public FromServerPacket(ServerResult resultType,[ReadOnlyArray()] byte[] notificationContent)
        {
            ResultType = resultType;
            Data = notificationContent;
        }
    }



    public enum ServerResult
    {
        Error,
        NotFound,
        Success,
        DataCorrupt,
    }
}
