using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.Params
{
    /// <summary>
    /// Chứa các tham số dùng cho việc khởi tạo Server
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RawNotificationServerParam
    {
        internal Params.RawNotificationSenderParams RawNotificationSenderParam { get; private set; }

        internal Params.RawNotificationClientCommunicatorParams ClientCommunicatorParam { get; private set; }

        /// <summary>
        /// Tạo một thể hiện mới của lớp RawNotificationServerParam
        /// </summary>
        /// <param name="senderParam">Tham số dùng để khởi tạo cho việc gửi thông báo</param>
        /// <param name="clientCommunicatorParam">Tham số dùng để khởi tạo cho client Communicate Server</param>
        /// <param name="serverCommunicateListenPort">Tham số dùng để khởi tạo cho việc gửi thông báo</param>
        public RawNotificationServerParam(RawNotificationSenderParams senderParam,
            RawNotificationClientCommunicatorParams clientCommunicatorParam)
        {
            RawNotificationSenderParam = senderParam;
            this.ClientCommunicatorParam = clientCommunicatorParam;
        }

    }
}
