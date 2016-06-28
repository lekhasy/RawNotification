using System;
using System.Collections.Generic;
using System.Text;

namespace RawNotification.Models.ServerBusinessModels
{
    public enum EnumWNSSendResult
    {
        Success,
        BadURI,
        OtherErrors,
        NetWorkError,
    }
}