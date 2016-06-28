using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.RawNotificationServer.Sender
{
    internal class ServerCommunicateResponsePackets
    {
        internal PacketType PacketType { get; private set; }

        internal object Data { get; private set; }
    }

    [DataContract(Namespace = "ServerCommunicate", Name = "AddNotificationRequestResult")]
    internal class AddNotificationRequestResult
    {
        [DataMember]
        internal bool IsSuccess { get; private set; }
        [DataMember]
        internal ErrorName ErrorName { get; private set; }
        [DataMember]
        internal Exception InnerException { get; private set; }

        internal AddNotificationRequestResult(bool isSuccess, ErrorName errorName, Exception ex)
        {
            IsSuccess = isSuccess;
            InnerException = ex;
            ErrorName = errorName;
        }
    }


    enum ErrorName
    {
        SqlError,
        BadRequestPack,
    }

    enum PacketType
    {
        AddNotificationResponse,
        ServerInfoResponse,
    }

}
