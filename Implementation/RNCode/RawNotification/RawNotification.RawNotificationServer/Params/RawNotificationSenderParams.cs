using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.Params
{
    public class RawNotificationSenderParams
    {
        internal Params.RawNotificationSenderParamForWindows10 Windows10Params { get; private set; }

        /// <summary>
        /// Tạo một đối tượng RawNotificationSenderParams
        /// </summary>
        /// <param name="windows10Params">Đối tượng dùng để khởi tạo việc gửi nhận dữ liệu với app Windows 10</param>
        public RawNotificationSenderParams(RawNotificationSenderParamForWindows10 windows10Params)
        {
            Windows10Params = windows10Params;
        }
    }
}
