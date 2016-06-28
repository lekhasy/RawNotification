using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace RawNotification.ServerClient.SharedModels.NetworkPackets.FromServer
{
    [DataContract(Namespace = "SC.FS", Name = "ServerInfo")]
    public sealed class ServerInfo
    {
        /// <summary>
        /// Cho biết có lỗi xảy ra trong server hay không
        /// </summary>
        [DataMember]
        public bool ErrorExit { get; private set; }

        /// <summary>
        /// Nguyên nhân gây ra lỗi gần đây nhất
        /// </summary>
        [DataMember]
        public SenderServerErrorType LastestErrorReason { get; private set; }

        /// <summary>
        /// Lỗi gần đây nhất
        /// </summary>
        [DataMember]
        public string LastestException { get; private set; }

        /// <summary>
        /// Thời gian xảy ra lỗi gần đây nhất
        /// </summary>
        [DataMember]
        public DateTime LastestErrorOccurredTime { get; private set; }

        public ServerInfo()
        {
            LastestErrorOccurredTime = DateTime.Now;
        }

        public void ChangeErrorInfo(Exception exception, SenderServerErrorType errorReson)
        {
            System.Threading.Monitor.Enter(this);
            ErrorExit = true;
            LastestException = exception.Message;
            LastestErrorReason = errorReson;
            LastestErrorOccurredTime = DateTime.Now;
            System.Threading.Monitor.Exit(this);
        }

        public void Reset()
        {
            System.Threading.Monitor.Enter(this);
            ErrorExit = false;
            LastestException = null;
            System.Threading.Monitor.Exit(this);
        }

        public ServerInfo Clone()
        {
            System.Threading.Monitor.Enter(this);
            ServerInfo temp = new ServerInfo
            {
                LastestErrorReason = this.LastestErrorReason,
                LastestErrorOccurredTime = this.LastestErrorOccurredTime,
                LastestException = this.LastestException,
                ErrorExit = this.ErrorExit
            };
            System.Threading.Monitor.Exit(this);
            return temp;
        }
    }

    public enum SenderServerErrorType
    {
        DBInteractionError,
        InternetError,
        W10_NotAcceptableError,
        W10_UnknowError,
        W10_WrongPackageSIDOrSecretKeyError,
    }

}
