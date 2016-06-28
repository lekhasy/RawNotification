using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.Params
{
    /// <summary>
    /// Chứa các tham số dùng để khởi tạo việc giao tiếp với ứng dụng client
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RawNotificationClientCommunicatorParams
    {
        internal int ListenPort { get; private set; }

        internal Action<Exception> UnhandledErrorOccurred { get; private set; }

        internal Action<Exception> DBInteractionErrorOccurred { get; private set; }

        /// <summary>
        /// tạo thể hiện mới của lớp RawNotificationClientCommunicatorParams
        /// </summary>
        /// <param name="listenPort">Port dùng để lắng nghe giao tiếp với mobile client</param>

        public RawNotificationClientCommunicatorParams(int listenPort,Action<Exception> unhandledErrorOccurred, Action<Exception> dbInteractionErrorOccurred)
        {
            ListenPort = listenPort;
            UnhandledErrorOccurred = unhandledErrorOccurred;
            DBInteractionErrorOccurred = dbInteractionErrorOccurred;
        }

    }
}
