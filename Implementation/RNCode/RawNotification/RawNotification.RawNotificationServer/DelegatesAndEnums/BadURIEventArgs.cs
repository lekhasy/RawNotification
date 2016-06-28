using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawNotification.RawNotificationServer.DelegatesAndEnums
{
    internal class BadURIEventArgs : EventArgs
    {
        internal Exception InternalException { get; private set; }
        internal Device Device { get; private set; }
        internal BadURIEventArgs(Exception ex, Device device)
        {
            InternalException = ex;
            Device = device;
        }
    }
}
