using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer
{
    [DataContract(Namespace ="SC.FS", Name = "AddNotificationFSPacketData")]
    public sealed class AddNotificationFSPacketData
    {
        [DataMember]
        public bool IsSuccess { get; private set; }
        [DataMember]
        public ComminucateServerErrorType ErrorType { get; private set; }
        [DataMember]
        public string InnerException { get; private set; }

        public AddNotificationFSPacketData(bool isSuccess, ComminucateServerErrorType errorType, Exception ex)
        {
            IsSuccess = isSuccess;
            if (ex!=null)
            {
                InnerException = ex.Message;
            }
            ErrorType = errorType;
        }
    }

    public enum ComminucateServerErrorType
    {
        SqlError,
        BadRequestPack,
        None,
        Unknow,
    }
}
